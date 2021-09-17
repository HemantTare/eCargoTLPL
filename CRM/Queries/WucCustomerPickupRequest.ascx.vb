Imports Raj.EC
Imports System.Data
Imports System.Web.UI
Imports ClassLibraryMVP
Imports Raj.CRM.QueryView
Imports Raj.CRM.QueryPresenter

Partial Class CRM_Queries_WucCustomerPickupRequest
    Inherits System.Web.UI.UserControl
    Implements ICustomerPickupRequestView

    Dim objCustomerPickupRequestPresenter As CustomerPickupRequestPresenter
    Dim Pc As New PageControls()
    Dim Mode As String

#Region "Controls Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AjaxPro.Utility.RegisterTypeForAjax(GetType(Raj.EC.CRM.CallBack))

        ddl_Branch.DataValueField = "Branch_Id"
        ddl_Branch.DataTextField = "Branch_Name"

        Mode = Util.DecryptToString(Request.QueryString("Mode").ToString())

        If Page.IsPostBack = False Then
            Pc.AddAttributes(Me.Controls)
            Pc.Txtbox_Add_Attributes(Me.Controls)

            set_text_and_Value()
            Fill_Time()
            PickUpNo = "AUTO GENERATION"
            lbl_GCText.Text = CompanyManager.getCompanyParam().GcCaption + " No :"
        End If

        objCustomerPickupRequestPresenter = New CustomerPickupRequestPresenter(Me, Page.IsPostBack)
        ddl_GC_No.OtherColumns = PickUpDate

        If (IsPostBack = False) Then
            If KeyId <= 0 Or Mode = "4" Then
                tr_Manual_Closing.Style.Add("display", "none")
            Else
                tr_Manual_Closing.Style.Add("display", "inline")
                ddl_StaffVA_SelectedIndexChanged(sender, e)
            End If
        End If

        DisabeControls(Mode)
    End Sub

    Private Sub set_text_and_Value()
        lbl_BookingLabel.Text = "Booking Type:"
        lbl_GCText.Text = "GC No:"
        ddl_GC_No.DataValueField = "GC_Id"
        ddl_GC_No.DataTextField = "GC_No"
    End Sub
    Private Sub DisabeControls(ByVal mode As String)
        Dim txt_Branch As TextBox
        txt_Branch = ddl_Branch.FindControl("txtBoxddl_Branch")
        If (Util.String2Int(mode) = 4) Then
            txt_Orgin.Enabled = False
            txt_Destination.Enabled = False
            txt_Weight.Enabled = False
            txt_Pkgs.Enabled = False
            ddl_BookingTypeMode.Enabled = False
            ddl_PackingType.Enabled = False
            txt_Consignor.Enabled = False
            ddl_CommodityType.Enabled = False
            txt_ContactName.Enabled = False
            txt_MobileNo.Enabled = False
            txt_Address.Enabled = False
            txt_TelephoneNo.Enabled = False
            txt_EmailId.Enabled = False
            txt_City.Enabled = False
            txt_State.Enabled = False
            txt_Branch.Enabled = False
            ddl_StaffVA.Enabled = False
            btn_Save.Visible = False
            dtp_PickUpDate.disableForView = False
            tp_PickUPTime.SetEnabled = False
        End If
    End Sub

    Protected Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        objCustomerPickupRequestPresenter.Save()
    End Sub

    Protected Sub ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Branch.TxtChange
        objCustomerPickupRequestPresenter.Fill_VA_On_Branch_Selection()
        upnl_Staff.Update()
    End Sub
#End Region

#Region "Validation"
    Public Function ValidateUI() As Boolean Implements ICustomerPickupRequestView.validateUI

        Dim IsValid As Boolean = False
        ErrorMessage = ""

        If Orgin = String.Empty And Pc.Control_Is_Mandatory(txt_Orgin) = True Then
            ErrorMessage = "Please Enter PickUp Point"
            txt_Orgin.Focus()
        ElseIf Destination = String.Empty And Pc.Control_Is_Mandatory(txt_Orgin) = True Then
            ErrorMessage = "Please Enter Destination"
            txt_Destination.Focus()
        ElseIf PickUpDate < Date.Now.Date Then
            ErrorMessage = "Pick Up Date Can't be less than Today's Date"
        ElseIf Weight <= 0 And Pc.Control_Is_Mandatory(txt_Weight) = True Then
            ErrorMessage = "Please Enter Weight"
            txt_Weight.Focus()
        ElseIf Packeges <= 0 And Pc.Control_Is_Mandatory(txt_Pkgs) = True Then
            ErrorMessage = "Please Enter Packeges"
            txt_Pkgs.Focus()
        ElseIf Consigner = String.Empty And Pc.Control_Is_Mandatory(txt_Consignor) = True Then
            ErrorMessage = "Please Enter Consigner"
            txt_Consignor.Focus()
        ElseIf ContactName = String.Empty And Pc.Control_Is_Mandatory(txt_ContactName) = True Then
            ErrorMessage = "Please Enter Contact Name"
            txt_ContactName.Focus()
        ElseIf ContactMobile = String.Empty And Pc.Control_Is_Mandatory(txt_MobileNo) = True Then
            ErrorMessage = "Please Enter Mobile No"
            txt_MobileNo.Focus()
        ElseIf txt_MobileNo.Text.Length < 10 And Pc.Control_Is_Mandatory(txt_MobileNo) = True Then
            ErrorMessage = "Invalid Mobile No"
            txt_MobileNo.Focus()
        ElseIf ContactAddress = String.Empty And Pc.Control_Is_Mandatory(txt_Address) = True Then
            ErrorMessage = "Please Enter Pickup Address"
            txt_Address.Focus()
        ElseIf txt_Address.Text.Length < 10 And Pc.Control_Is_Mandatory(txt_Address) = True Then
            ErrorMessage = "Pickup Address must be greater than 10 character"
            txt_Address.Focus()
        ElseIf ContactTelephone = String.Empty And Pc.Control_Is_Mandatory(txt_TelephoneNo) = True Then
            ErrorMessage = "Please Enter Telephone No"
            txt_TelephoneNo.Focus()
        ElseIf txt_TelephoneNo.Text.Length < 8 And Pc.Control_Is_Mandatory(txt_TelephoneNo) = True Then
            ErrorMessage = "Invalid Telephone No"
            txt_TelephoneNo.Focus()
        ElseIf ContactEmailId = String.Empty And Pc.Control_Is_Mandatory(txt_EmailId) = True Then
            ErrorMessage = "Please Enter Email Id"
            txt_EmailId.Focus()
        ElseIf ForwardBranchId > 0 And VendorId <= 0 Then
            ErrorMessage = "Please Select Staff/VA"
            ddl_StaffVA.Focus()
        ElseIf KeyId > 0 And chk_Close.Checked = True And GC_ID <= 0 And Reason = "" Then
            ErrorMessage = "Please Enter GC or Valid Reason For closing Pickup Request"
            txt_Reason.Focus()
        Else
            IsValid = True
        End If

        Return IsValid

    End Function
#End Region

#Region "IView"
    Public ReadOnly Property KeyId() As Integer Implements ICustomerPickupRequestView.keyID
        Get
            Return Util.DecryptToInt(Request.QueryString("Id"))
            'Return 4
        End Get
    End Property

    Public WriteOnly Property ErrorMessage() As String Implements ICustomerPickupRequestView.errorMessage
        Set(ByVal value As String)
            lbl_Errors.Text = value
        End Set
    End Property
#End Region

#Region "Implement Property"

    Public Property Packeges() As Integer Implements ICustomerPickupRequestView.Packeges
        Get
            Return Val(txt_Pkgs.Text)
        End Get
        Set(ByVal value As Integer)
            txt_Pkgs.Text = value
        End Set
    End Property

    Public Property BookingTypeModeId() As Integer Implements ICustomerPickupRequestView.BookingTypeModeId
        Get
            Return Val(ddl_BookingTypeMode.SelectedValue)
        End Get
        Set(ByVal value As Integer)
            ddl_BookingTypeMode.SelectedValue = value
        End Set
    End Property

    Public Property PackingTypeId() As Integer Implements ICustomerPickupRequestView.PackingTypeId
        Get
            Return Val(ddl_PackingType.SelectedValue)
        End Get
        Set(ByVal value As Integer)
            ddl_PackingType.SelectedValue = value
        End Set
    End Property

    Public ReadOnly Property PackingType() As String Implements ICustomerPickupRequestView.PackingType
        Get
            Return ddl_PackingType.SelectedItem.Text
        End Get
    End Property

    Public Property CommodityId() As Integer Implements ICustomerPickupRequestView.CommodityId
        Get
            Return Val(ddl_CommodityType.SelectedValue)
        End Get
        Set(ByVal value As Integer)
            ddl_CommodityType.SelectedValue = value
        End Set
    End Property

    Public ReadOnly Property Commodity() As String Implements ICustomerPickupRequestView.Commodity
        Get
            Return ddl_CommodityType.SelectedItem.Text
        End Get
    End Property

    Public ReadOnly Property ForwardBranchId() As Integer Implements ICustomerPickupRequestView.ForwardBranchId
        Get
            Return Val(ddl_Branch.SelectedValue)
        End Get
    End Property

    Public Property VendorId() As Integer Implements ICustomerPickupRequestView.VendorId
        Get
            Return Val(ddl_StaffVA.SelectedValue)
        End Get
        Set(ByVal value As Integer)
            ddl_StaffVA.SelectedValue = value
        End Set
    End Property

    Public Property VAMobileNo() As Long Implements ICustomerPickupRequestView.VAMobileNo
        Get
            Return Val(lbl_MobileNo.Text)
        End Get
        Set(ByVal value As Long)
            lbl_MobileNo.Text = value.ToString()
        End Set
    End Property

    Public Property Weight() As Decimal Implements ICustomerPickupRequestView.Weight
        Get
            Return Val(txt_Weight.Text)
        End Get
        Set(ByVal value As Decimal)
            txt_Weight.Text = value
        End Set
    End Property

    Public WriteOnly Property PickUpNo() As String Implements ICustomerPickupRequestView.PickUpNo

        Set(ByVal value As String)
            lbl_Pickup_No.Text = value
        End Set
    End Property

    Public Property Orgin() As String Implements ICustomerPickupRequestView.Orgin
        Get
            Return txt_Orgin.Text
        End Get
        Set(ByVal value As String)
            txt_Orgin.Text = value
        End Set
    End Property

    Public Property Destination() As String Implements ICustomerPickupRequestView.Destination
        Get
            Return txt_Destination.Text
        End Get
        Set(ByVal value As String)
            txt_Destination.Text = value
        End Set
    End Property

    Public Property PickUpTime() As String Implements ICustomerPickupRequestView.PickUpTime
        Get
            Return tp_PickUPTime.getTime
        End Get
        Set(ByVal value As String)
            tp_PickUPTime.setTime(value)
        End Set
    End Property

    Public Property Consigner() As String Implements ICustomerPickupRequestView.Consigner
        Get
            Return txt_Consignor.Text
        End Get
        Set(ByVal value As String)
            txt_Consignor.Text = value
        End Set
    End Property

    Public Property ContactName() As String Implements ICustomerPickupRequestView.ContactName
        Get
            Return txt_ContactName.Text
        End Get
        Set(ByVal value As String)
            txt_ContactName.Text = value
        End Set
    End Property

    Public Property ContactMobile() As String Implements ICustomerPickupRequestView.ContactMobile
        Get
            Return txt_MobileNo.Text
        End Get
        Set(ByVal value As String)
            txt_MobileNo.Text = value
        End Set
    End Property

    Public Property ContactAddress() As String Implements ICustomerPickupRequestView.ContactAddress
        Get
            Return txt_Address.Text
        End Get
        Set(ByVal value As String)
            txt_Address.Text = value
        End Set
    End Property

    Public Property ContactTelephone() As String Implements ICustomerPickupRequestView.ContactTelephone
        Get
            Return txt_TelephoneNo.Text
        End Get
        Set(ByVal value As String)
            txt_TelephoneNo.Text = value
        End Set
    End Property

    Public Property ContactEmailId() As String Implements ICustomerPickupRequestView.ContactEmailId
        Get
            Return txt_EmailId.Text
        End Get
        Set(ByVal value As String)
            txt_EmailId.Text = value
        End Set
    End Property

    Public Property ContactCity() As String Implements ICustomerPickupRequestView.ContactCity
        Get
            Return txt_City.Text
        End Get
        Set(ByVal value As String)
            txt_City.Text = value
        End Set
    End Property

    Public Property ContactState() As String Implements ICustomerPickupRequestView.ContactState
        Get
            Return txt_State.Text
        End Get
        Set(ByVal value As String)
            txt_State.Text = value
        End Set
    End Property

    'Public Property ForwardTime() As String Implements ICustomerPickupRequestView.ForwardTime
    '    Get
    '        Return tp_ForwardTime.getTime
    '    End Get
    '    Set(ByVal value As String)
    '        tp_ForwardTime.setTime(value)
    '    End Set
    'End Property

    Public Property PickUpDate() As DateTime Implements ICustomerPickupRequestView.PickUpDate
        Get
            Return dtp_PickUpDate.SelectedDate
        End Get
        Set(ByVal value As DateTime)
            dtp_PickUpDate.SelectedDate = value
        End Set
    End Property

    'Public Property ForwardDate() As DateTime Implements ICustomerPickupRequestView.ForwardDate
    '    Get
    '        Return dtp_ForwardDate.Selected_Date
    '    End Get
    '    Set(ByVal value As DateTime)
    '        dtp_ForwardDate.Selected_Date = value
    '    End Set
    'End Property

    Public ReadOnly Property PickUpDateAndTime() As DateTime Implements ICustomerPickupRequestView.PickUpDateAndTime
        Get
            Dim pt() As String = tp_PickUPTime.getTime.Split(":")
            Dim pd As New DateTime
            pd = dtp_PickUpDate.SelectedDate

            Dim pdt As New DateTime(pd.Year, pd.Month, pd.Day, Val(pt(0)), Val(pt(1)), 0)
            Return pdt
        End Get
    End Property

    'Private ReadOnly Property ForwardDateAndTime() As DateTime
    '    Get
    '        Dim ft() As String = tp_ForwardTime.getTime.Split(":")
    '        Dim fd As New DateTime
    '        fd = dtp_ForwardDate.Selected_Date

    '        Dim fdt As New DateTime(fd.Year, fd.Month, fd.Day, Val(ft(0)), Val(ft(1)), 0)
    '        Return fdt
    '    End Get
    'End Property

    Private Property Reason() As String Implements ICustomerPickupRequestView.Reason
        Get
            Return txt_Reason.Text
        End Get
        Set(ByVal value As String)
            txt_Reason.Text = value
        End Set
    End Property

    Public ReadOnly Property GC_Docket_No() As Integer Implements ICustomerPickupRequestView.GC_Docket_No
        Get
            Return Val(ddl_GC_No.SelectedText)
        End Get
    End Property

    Public ReadOnly Property GC_ID() As Integer Implements ICustomerPickupRequestView.GC_Id
        Get
            Return Val(ddl_GC_No.SelectedValue)
        End Get
    End Property

    Public Sub SetForwardBranchId(ByVal value As String, ByVal text As String) Implements ICustomerPickupRequestView.SetForwardBranchId
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Branch)
    End Sub
#End Region

#Region "Control Bind"

    Public WriteOnly Property BindBookingTypeMode() As DataTable Implements ICustomerPickupRequestView.BindBookingTypeMode
        Set(ByVal value As DataTable)
            ddl_BookingTypeMode.DataSource = value
            ddl_BookingTypeMode.DataValueField = "Booking_Type_Id"
            ddl_BookingTypeMode.DataTextField = "Booking_Type"
            ddl_BookingTypeMode.DataBind()
        End Set
    End Property

    Public WriteOnly Property BindPackingType() As DataTable Implements ICustomerPickupRequestView.BindPackingType
        Set(ByVal value As DataTable)
            ddl_PackingType.DataSource = value
            ddl_PackingType.DataValueField = "Packing_Type_Id"
            ddl_PackingType.DataTextField = "Packing_Type"
            ddl_PackingType.DataBind()
        End Set
    End Property

    Public WriteOnly Property BindCommodity() As DataTable Implements ICustomerPickupRequestView.BindCommodity
        Set(ByVal value As DataTable)
            ddl_CommodityType.DataSource = value
            ddl_CommodityType.DataValueField = "Commodity_Id"
            ddl_CommodityType.DataTextField = "Commodity_Name"
            ddl_CommodityType.DataBind()
        End Set
    End Property

    Public WriteOnly Property BindForwardVA() As DataTable Implements ICustomerPickupRequestView.BindForwardVA
        Set(ByVal value As DataTable)
            ddl_StaffVA.DataSource = value
            ddl_StaffVA.DataValueField = "VA_Id"
            ddl_StaffVA.DataTextField = "VA_Name"
            ddl_StaffVA.DataBind()
            ddl_StaffVA.Items.Insert(0, New ListItem("Select One", "0"))
        End Set
    End Property

    Private Sub Fill_Time()
        Dim current_time As String = CStr(Format(Date.Now, "HH:mm"))
        'tp_ForwardTime.setFormat("24")
        'tp_ForwardTime.setTime(current_time)

        tp_PickUPTime.setFormat("24")
        tp_PickUPTime.setTime(current_time)
    End Sub
#End Region

    Protected Sub ddl_StaffVA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lbl_MobileNo.Text = objCustomerPickupRequestPresenter.GetVaMobNo()
    End Sub
End Class
