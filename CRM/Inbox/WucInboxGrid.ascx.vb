Imports Raj.EC
Imports System.Data
Imports ClassLibraryMVP
Imports ClassLibraryMVP.Security
Imports System.IO


Partial Class WucInboxGrid
    Inherits System.Web.UI.UserControl

    Dim CommonObj As New Common()
    Dim Type As String
    Dim Is_ForCA As Boolean = False
    Dim is_CSO As Boolean = UserManager.getUserParam().Is_CSA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(Raj.EC.Common.GetMenuItemId()).QueryString)
        'Type = Session("QueryString")

        Dim Query() As String
        Query = Split(Session("QueryString"), "&")
        If (Query.Length > 1) Then
            Type = Query(0)
            Is_ForCA = Query(1)
        Else
            Type = Session("QueryString")
        End If
        If IsPostBack = False Then
            sortCriteria = "Col1"
            sortDir = "Asc"
            If Type = "In Progress" Then
                lbl_Heading.Text = "In Process"
            ElseIf Type = "Closed Tickets" And Is_ForCA = True Then
                lbl_Heading.Text = "Complaints Analysis"
            Else
                lbl_Heading.Text = Type
            End If

            BuildGrid()
            FillCombo()
        End If

    End Sub

    Private Function addColumn(ByVal columnName As String, ByVal colType As String) As DataColumn
        Dim dcol As DataColumn = New DataColumn(columnName)
        dcol.DataType = System.Type.GetType(colType)
        Return dcol
    End Function

    'Private ReadOnly Property Is_ForCA() As Boolean
    '    Get
    '        If Not Request.QueryString("Is_ForCA") Is Nothing Then
    '            Return Request.QueryString("Is_ForCA")
    '        Else
    '            Return False
    '        End If
    '    End Get
    'End Property

    Private Sub BuildGrid()

        Dim tbl As New DataTable
        Dim DS1 As DataSet = Nothing
        Dim DS2 As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim dRow As DataRow
        Dim TotCol As Integer
        Dim ColCount As Integer = 10

        tbl.Columns.Add(addColumn("Col1", "System.String"))
        tbl.Columns.Add(addColumn("Col2", "System.String"))
        tbl.Columns.Add(addColumn("Col3", "System.Boolean"))
        tbl.Columns.Add(addColumn("Col4", "System.String"))
        tbl.Columns.Add(addColumn("Col5", "System.String"))
        tbl.Columns.Add(addColumn("Col6", "System.String"))
        tbl.Columns.Add(addColumn("Col7", "System.String"))
        tbl.Columns.Add(addColumn("Col8", "System.String"))
        tbl.Columns.Add(addColumn("Col9", "System.String"))
        tbl.Columns.Add(addColumn("Col10", "System.String"))
        tbl.Columns.Add(addColumn("Col11", "System.String"))
        tbl.Columns.Add(addColumn("Col12", "System.String"))

        tbl.Columns.Add(addColumn("Col13", "System.String"))

        DS1 = CommonObj.FillCRMInboxGrid(Type)

        If DS1.Tables(0).Rows.Count > 0 Then
            TotCol = DS1.Tables(0).Columns.Count

            For j = 0 To DS1.Tables(0).Columns.Count - 1
                Grid.Columns(j).HeaderText = DS1.Tables(0).Columns(j).ColumnName
            Next

            For i = 0 To DS1.Tables(0).Rows.Count - 1
                dRow = tbl.NewRow()
                For j = 0 To DS1.Tables(0).Columns.Count - 1
                    dRow.Item(j) = DS1.Tables(0).Rows(i)(j)
                Next
                tbl.Rows.Add(dRow)
            Next

            DS2.Tables.Add(tbl)

            Grid.Columns(0).Visible = False
            Grid.Columns(1).Visible = False
            Grid.Columns(11).Visible = False

            Grid.Columns(5).HeaderText = CompanyManager.getCompanyParam().GcCaption + " No"

            If Type = "Archived" Then
                Grid.Columns(12).Visible = True
            Else
                Grid.Columns(12).Visible = False
            End If

            If is_CSO Then
                Grid.Columns(8).Visible = True
                Grid.Columns(9).Visible = True

            Else
                Grid.Columns(8).Visible = False
                Grid.Columns(9).Visible = False

            End If

            For i = TotCol + 1 To ColCount
                Grid.Columns(i - 1).Visible = False
            Next

            Session("k") = DS2

            If IsPostBack = True Then
                Call MakeDataView()
            Else
                Grid.DataSource = DS2
                Grid.DataBind()
            End If
        Else
            'Response.Redirect("~/Display/frm_Error.aspx?Error_Code=" & ClassLibraryMVP.Util.EncryptInteger(1016))
            Raj.EC.Common.DisplayErrors(1014)
        End If

    End Sub

    Protected Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim Id, GcDocId As Integer
        Dim PriorityId As Integer
        Dim Url, UserIds As String
        Dim TrackTraceUrl As String

        If e.Item.ItemIndex <> -1 Then

            Id = (CType(e.Item.Cells(0).FindControl("Label1"), Label).Text)
            PriorityId = (CType(e.Item.Cells(1).FindControl("Label2"), Label).Text)
            UserIds = (CType(e.Item.Cells(1).FindControl("lbl_UserIds"), Label).Text)

            GcDocId = ClassLibraryMVP.Util.String2Int((CType(e.Item.FindControl("lbl_GcId"), Label).Text))

            If Type = "Pending For Assignment" Then
                Url = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Transaction/frm_Complaint_Assignment.aspx?Ticket_Id=" & ClassLibraryMVP.Util.EncryptInteger(Id) & "&Type=" & ClassLibraryMVP.Util.EncryptString(Type)
            ElseIf Is_ForCA Then
                Url = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Transaction/frm_Complaint_Analysis.aspx?Ticket_Id=" & ClassLibraryMVP.Util.EncryptInteger(Id)
            Else
                Url = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Transaction/FrmTicketHistory.aspx?Id=" & ClassLibraryMVP.Util.EncryptInteger(Id) & "&Type=" & ClassLibraryMVP.Util.EncryptString(Type)

            End If

            If Type = "Close Tickets" Then
                Url = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Transaction/FrmTicketResolution.aspx?Ticket_Id=" & ClassLibraryMVP.Util.EncryptInteger(Id) & "&Type=" & ClassLibraryMVP.Util.EncryptString(Type)
            End If


            TrackTraceUrl = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Queries/frm_CRM_Track_And_Trace.aspx?GcDocId=" & ClassLibraryMVP.Util.EncryptInteger(GcDocId)

            e.Item.Cells(3).Attributes.Add("onclick", "return Show_Ticket_History('" & Url & "');")
            e.Item.Cells(3).Attributes.Add("onmouseover", "this.style.cursor='hand'")
            e.Item.Cells(3).Style.Add("color", "ActiveCaption")

            e.Item.Cells(5).Attributes.Add("onmouseover", "this.style.cursor='hand'")
            e.Item.Cells(5).Attributes.Add("onclick", "return Show_Ticket_History('" & TrackTraceUrl & "');")
            e.Item.Cells(5).Style.Add("color", "ActiveCaption")

            If Type = "In Progress" And UserIds.Trim() <> "" Then
                Url = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Transaction/FrmDisplayInfo.aspx?Id=" & ClassLibraryMVP.Util.EncryptString(UserIds) & "&Type=" & ClassLibraryMVP.Util.EncryptString("ProfileUserInfo") & "&Ticket_Id=" & ClassLibraryMVP.Util.EncryptInteger(Id)
                e.Item.Cells(10).Attributes.Add("onmouseover", "this.style.cursor='hand'")
                e.Item.Cells(10).Attributes.Add("onclick", "return Show_Ticket_History('" & Url & "');")
                e.Item.Cells(10).Style.Add("color", "ActiveCaption")
            ElseIf Type = "Closed Tickets" Or Type = "Archived" Then
                Url = ClassLibraryMVP.Util.GetBaseURL & "/CRM/Transaction/FrmDisplayInfo.aspx?Id=" & ClassLibraryMVP.Util.EncryptString(UserIds) & "&Type=" & ClassLibraryMVP.Util.EncryptString("UserInfo")
                e.Item.Cells(10).Attributes.Add("onmouseover", "this.style.cursor='hand'")
                e.Item.Cells(10).Attributes.Add("onclick", "return Show_Ticket_History('" & Url & "');")
                e.Item.Cells(10).Style.Add("color", "ActiveCaption")
            End If

            If PriorityId = 1 Then
                e.Item.CssClass = "LOWPRIORITY"
            ElseIf PriorityId = 2 Then
                e.Item.CssClass = "MEDIUMPRIORITY"
            ElseIf PriorityId = 3 Then
                e.Item.CssClass = "HIGHPRIORITY"
            ElseIf PriorityId = 4 Then
                e.Item.CssClass = "URGENTPRIORITY"
            ElseIf PriorityId = 5 Then
                e.Item.CssClass = "CRITICALPRIORITY"
            End If

        End If

    End Sub

    Protected Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Grid.CurrentPageIndex = e.NewPageIndex
        Search()
    End Sub

    Private Sub FillCombo()
        Dim i As Integer
        Dim DS As New DataSet
        Dim DRow As DataRow

        DS.Tables.Add("ComboTable")
        DS.Tables(0).Columns.Add("Col_ID")
        DS.Tables(0).Columns.Add("Col_Name")

        For i = 0 To Grid.Columns.Count - 4
            If Grid.Columns(i).Visible = True Then
                DRow = DS.Tables(0).NewRow
                DRow("Col_Id") = i + 1
                DRow("Col_Name") = Grid.Columns(i).HeaderText
                DS.Tables(0).Rows.Add(DRow)
            End If
        Next

        ddl_Serach.DataSource = DS
        ddl_Serach.DataTextField = "Col_Name"
        ddl_Serach.DataValueField = "Col_ID"
        ddl_Serach.DataBind()

        ddl_Serach.Items.RemoveAt(0)
    End Sub

    Private Sub MakeDataView()
        Dim das As New DataSet
        Dim DataType As System.Type
        das = Session("k")

        Dim view As DataView = New DataView
        With view
            .Table = das.Tables(0)
            DataType = .Table.Columns("Col" & ddl_Serach.SelectedValue).DataType
            .AllowNew = True
            Dim k As String
            k = CStr(txt_Search.Text)

            If DataType.Name = "Integer" Or DataType.Name = "datetime" Then
                .RowFilter = "Col" & ddl_Serach.SelectedValue & " = '" & k & "'"
            Else
                .RowFilter = "Col" & ddl_Serach.SelectedValue & " Like '" & k & "*'"
            End If

            .RowStateFilter = DataViewRowState.CurrentRows
            .Sort = sortCriteria & " " & sortDir
        End With
        Grid.DataSource = view
        Grid.DataBind()

        ' Simple-bind to a TextBox control
        ''Text1.DataBindings.Add("Text", view, "CompanyName")
    End Sub

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        Grid.CurrentPageIndex = 0
        Search()
    End Sub

    Private Sub Search()
        If txt_Search.Text <> "" Then
            MakeDataView()
        Else
            BuildGrid()
        End If
    End Sub

    Protected Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
        If sortCriteria = e.SortExpression Then
            If sortDir = "Desc" Then
                sortDir = "Asc"
            Else
                sortDir = "Desc"
            End If
        End If
        ' Assign the column clicked to the sortCriteria property    
        sortCriteria = e.SortExpression

        Call MakeDataView()
    End Sub

    Protected Sub txt_Search_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Search.TextChanged
        Grid.CurrentPageIndex = 0
        Search()
    End Sub

    Private Property sortCriteria() As String
        Get
            Return CStr(ViewState("sortCriteria"))
        End Get
        Set(ByVal Value As String)
            ViewState("sortCriteria") = Value
        End Set
    End Property

    Private Property sortDir() As String

        Get
            Return CStr(ViewState("sortDir"))
        End Get

        Set(ByVal Value As String)
            ViewState("sortDir") = Value
        End Set
    End Property
End Class
