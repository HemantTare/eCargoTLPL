Imports System.Data
Imports System.Data.SqlClient
Imports Raj.EC.CRM
Imports ClassLibraryMVP
Imports Raj.EC

Partial Class CRM_Queries_frm_CRM_Track_And_Trace
    Inherits ClassLibraryMVP.UI.Page

    Dim objcom As New Common
    Dim objCrm As New CRMTrackNTrace
    Dim ds As DataSet
    Dim GcDocId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        GcDocId = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString("GcDocId"))

        If Not IsPostBack Then
            txt_Trace_No.Focus()
            tr_Ticket_Details.Visible = False
            tr_GC_Details.Visible = False
            TR_TicketNo.Visible = False

            If Val(GcDocId) > 0 Then
                ds = objcom.Get_Values_Where("EC_Opr_Vtrans_GC", "GC_No", "GC_Id =" & GcDocId, "GC_No", False)
                txt_Trace_No.Text = ds.Tables(0).Rows(0)("GC_No")
                If ds.Tables(0).Rows.Count > 0 Then
                    fill_Show_Details()
                    Fill_Details()
                End If
            End If
            SetStandardCaption()
        End If
    End Sub

    Private Sub SetStandardCaption()
        rbtnl.Items(0).Text = CompanyManager.getCompanyParam().GcCaption + " Wise"
        pnl_gc_Details.GroupingText = CompanyManager.getCompanyParam().GcCaption + " Details"
        lbl_TD_GC_No.Text = CompanyManager.getCompanyParam().GcCaption + " No:"
        lbl_TD_GC_Date_Time.Text = CompanyManager.getCompanyParam().GcCaption + " Date & Time:"
        lbl_TD_GC_Status.Text = CompanyManager.getCompanyParam().GcCaption + " Current Status:"
        lbl_TD_GC_Current_Branch.Text = CompanyManager.getCompanyParam().GcCaption + " Current Branch:"
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If AllowToTrack() = True Then
            fill_Show_Details()
        End If
        GetStatus()
    End Sub

    Protected Sub ddl_TicketNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TicketNo.SelectedIndexChanged
        Fill_Details()
    End Sub

    Private Sub GetStatus()
        Dim status As Int16
        status = ds.Tables(0).Rows(0)("status_id")

        If lbl_Booking_Branch.Text = lbl_GC_Current_Branch.Text And status <= 5 Then
            lbl_status.Text = "Booking Stock"
        ElseIf status = 3000 Or status = 3010 Or status = 3020 Or status = 3030 Then
            lbl_status.Text = "UGC"
        ElseIf ((lbl_Delivery_Branch.Text <> lbl_GC_Current_Branch.Text) And (lbl_Booking_Branch.Text <> lbl_GC_Current_Branch.Text) And status <> 40) Then
            lbl_status.Text = "Crossing Stock"
        ElseIf status = 40 Then
            lbl_status.Text = "In Transit"
        ElseIf ((lbl_Delivery_Branch.Text = lbl_GC_Current_Branch.Text) And status <> 200) Then
            lbl_status.Text = "Delivery Stock"
        ElseIf status = 200 Then
            lbl_status.Text = "Delivered"
        End If
    End Sub

    Private Sub fill_Show_Details()
        Dim ds_ticket As New DataSet

        If (rbtnl.Items(0).Selected = True) Then
            ds_ticket = objcom.Get_Values_Where("EC_CRM_Complaint", "Ticket_Id,Ticket_No", "GC_No =" & Val(txt_Trace_No.Text.Trim()), "Ticket_No", False)
            SET_TICKET_NO = ds_ticket
        Else
            ds_ticket = objcom.Get_Values_Where("EC_CRM_Complaint", "Ticket_Id,Ticket_No", "Ticket_No like '" & Trim(txt_Trace_No.Text) & "%'", "Ticket_No", False)
            SET_TICKET_NO = ds_ticket
        End If
        Session("ds_ticket") = ds_ticket

        If (ds_ticket.Tables(0).Rows.Count > 1) Then
            TR_TicketNo.Visible = True
        Else
            TR_TicketNo.Visible = False
        End If

        If (ds_ticket.Tables(0).Rows.Count > 0) Then
            tr_Ticket_Details.Visible = True
            tr_GC_Details.Visible = True
            Fill_Details()
        Else
            tr_Ticket_Details.Visible = False
            tr_GC_Details.Visible = False
        End If
    End Sub

    Private Function AllowToTrack() As Boolean
        Dim als As Boolean = False
        tr_Ticket_Details.Visible = False
        tr_GC_Details.Visible = False
        TR_TicketNo.Visible = False

        If Trim(txt_Trace_No.Text) = "" Then
            lbl_Error.Text = "Please Enter Valid " + CompanyManager.getCompanyParam().GcCaption + " No"
            txt_Trace_No.Focus()
        ElseIf (rbtnl.Items(0).Selected = True And Val(txt_Trace_No.Text.Trim()) <= 0) Then
            lbl_Error.Text = "Please Enter Valid " + CompanyManager.getCompanyParam().GcCaption + " No"
            txt_Trace_No.Focus()
        Else
            als = True
            tr_Ticket_Details.Visible = True
            tr_GC_Details.Visible = True
            TR_TicketNo.Visible = True
        End If
        Return als
    End Function

#Region "Fill_Details"

    Private Sub Fill_Details()
        lbl_Error.Text = ""

        ds = objCrm.Fill_Details_Track_And_Trace(GET_TICKET_NO, UserManager.getUserParam().YearCode)

        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)

            tr_Ticket_Details.Visible = True
            tr_GC_Details.Visible = True

            hdn_GC_Id.Value = dr("GC_Id")
            lbtn_GC_No.Text = dr("GC_No_for_print")
            lbl_GC_Date_Time.Text = dr("GC_Date_Time")
            lbl_BookingType.Text = dr("Booking_Type")
            lbl_DeliveryType.Text = dr("Delivery_Type")
            hdn_TicketId.Value = dr("Ticket_Id")
            lbl_PaymentType.Text = dr("Payment_Type")
            lbtn_TicketNo.Text = dr("Ticket_No")
            lbl_Ticket_Date.Text = dr("Ticket_Date")
            lbl_Ticket_Status.Text = dr("Status_Name")
            lbl_Ticket_Priority.Text = dr("priority")
            lbtn_Assigned_To.Text = "Show"
            lbl_Pending_Hours.Text = dr("Pending_Hours")
            lbtn_Next_Escalated_To.Text = dr("Next_Escalated_To")
            lbl_Complaint_By.Text = dr("Complaint_By")
            lbl_Name.Text = dr("Name")
            lbl_Booking_Branch.Text = dr("Booking_Branch")
            lbl_Delivery_Branch.Text = dr("Delivery_Branch")
            lbl_No_Of_Packeges.Text = dr("No_Of_Articles")
            lbl_Charge_Weight.Text = dr("Charged_weight")
            lbl_GC_Status.Text = dr("status")
            lbl_Consignor_Name.Text = dr("consignor_name")
            lbl_Consignee_Name.Text = dr("consignee_name")
            lbl_GC_Current_Branch.Text = dr("current_branch_name")
            lbl_Delivery_Date_Time.Text = dr("Delivery_Date")
            lbl_Remarks.Text = dr("Remarks")
        Else
            lbl_Error.Text = "No Data Found"
            tr_Ticket_Details.Visible = False
            tr_GC_Details.Visible = False
        End If

        lbtn_GC_No.Attributes.Add("onclick", "return GetGCDetails('" & hdn_GC_Id.Value & "');")
        lbtn_TicketNo.Attributes.Add("onclick", "return open_ticket_history('" & Util.EncryptInteger(Val(hdn_TicketId.Value)) & "');")
        lbtn_Assigned_To.Attributes.Add("onclick", "return viewwindow_general('" & ClassLibraryMVP.Util.EncryptInteger(Val(hdn_TicketId.Value)) & "');")

    End Sub
#End Region

#Region "Bind DDL"
    Private WriteOnly Property SET_TICKET_NO() As DataSet
        Set(ByVal value As DataSet)
            ddl_TicketNo.DataSource = value
            ddl_TicketNo.DataTextField = "Ticket_No"
            ddl_TicketNo.DataValueField = "Ticket_Id"
            ddl_TicketNo.DataBind()
        End Set
    End Property

    Private ReadOnly Property GET_TICKET_NO() As String
        Get
            Return ddl_TicketNo.SelectedItem.Text
        End Get
    End Property
#End Region

    Private Sub fill_Details_not_used()
        '    Dim tr As HtmlTableRow = Nothing
        '    Dim tc As HtmlTableCell

        '    For i = 0 To ds.Tables(0).Columns.Count - 1
        '        tr = New HtmlTableRow()
        '        tc = New HtmlTableCell()
        '        tc.EnableViewState = True
        '        tc.InnerText = ds.Tables(0).Columns(i).ColumnName & ":"
        '        tc.Style.Add("font-weight", "bold")
        '        tc.Attributes.Add("class", "TD1")
        '        tr.Cells.Insert(0, tc)
        '        tc = New HtmlTableCell()
        '        tc.EnableViewState = True
        '        tc.InnerText = ds.Tables(0).Rows(0)(ds.Tables(0).Columns(i).ColumnName)
        '        tr.Cells.Insert(1, tc)
        '        i = i + 1
        '        If i < ds.Tables(0).Columns.Count Then
        '            tc = New HtmlTableCell()
        '            tc.EnableViewState = True
        '            tc.InnerText = ds.Tables(0).Columns(i).ColumnName & ":"
        '            tc.Style.Add("font-weight", "bold")
        '            tc.Attributes.Add("class", "TD1")
        '            tr.Cells.Insert(2, tc)
        '            tc = New HtmlTableCell()
        '            tc.EnableViewState = True
        '            tc.InnerText = ds.Tables(0).Rows(0)(ds.Tables(0).Columns(i).ColumnName)
        '            tr.Cells.Insert(3, tc)
        '        End If
        '        tr_Trace.Rows.Add(tr)
        '    Next
    End Sub


End Class
