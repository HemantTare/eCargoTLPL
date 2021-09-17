Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System

Imports Raj.EC
Imports Raj.EC.Security

Imports Raj.EC.ControlsView

Partial Class Controls_WucHierarchyWithID
    Inherits System.Web.UI.UserControl
    Implements IHierarchiWithIdView

    Public DDLHierarchyChange As EventHandler
    Public DDLLocationChange As EventHandler
    Public _IsUsedForGrid As Boolean
    Dim _Is_For_Grid As Boolean
    Dim objcommon As New Common
    Dim Query As String = ""


    'Public Property IsUsedForGrid() As Boolean
    '    Set(ByVal value As Boolean)
    '        _IsUsedForGrid = value
    '    End Set
    '    Get
    '        Return _IsUsedForGrid
    '    End Get
    'End Property


    Public WriteOnly Property setDDLLocationAutoPostBack() As Boolean
        Set(ByVal value As Boolean)
            ddl_location.AutoPostBack = value
        End Set
    End Property
    Public WriteOnly Property SetEnabled() As Boolean
        Set(ByVal value As Boolean)
            ddl_location.Enabled = value
            ddl_hierarchy.Enabled = value
            lbl_Hierarchy_caption.Enabled = value
            lbl_location_caption.Enabled = value
        End Set
    End Property

    Public Property RegionID() As Integer Implements IHierarchiWithIdView.RegionID
        Get
            If ddl_hierarchy.SelectedValue = "RO" Then
                Return Val(ddl_location.SelectedValue)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If value > 0 Then
                Fill_hierarchy()
                ddl_hierarchy.SelectedValue = "RO"
                FillLocation()
                ddl_location.SelectedValue = value
            End If
        End Set
    End Property

    Public Property AreaID() As Integer Implements IHierarchiWithIdView.AreaID
        Get
            If ddl_hierarchy.SelectedValue = "AO" Then
                Return Val(ddl_location.SelectedValue)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If value > 0 Then
                Fill_hierarchy()
                ddl_hierarchy.SelectedValue = "AO"
                FillLocation()
                ddl_location.SelectedValue = value
            End If
        End Set
    End Property

    Public Property BranchID() As Integer Implements IHierarchiWithIdView.BranchID
        Get
            If ddl_hierarchy.SelectedValue = "BO" Then
                Return Val(ddl_location.SelectedValue)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If value > 0 Then
                Fill_hierarchy()
                ddl_hierarchy.SelectedValue = "BO"
                FillLocation()
                ddl_location.SelectedValue = value
            End If
        End Set
    End Property

    Public Property Is_Ho() As Boolean Implements IHierarchiWithIdView.Is_Ho
        Get
            If ddl_hierarchy.SelectedValue = "HO" Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            If value = True Then
                Fill_hierarchy()
                lbl_location_caption.Visible = False
                ddl_location.Visible = False
                ddl_hierarchy.SelectedValue = "HO"
            End If
        End Set
    End Property

    Public Property MainId() As Integer Implements IHierarchiWithIdView.MainId
        Get
            If ddl_hierarchy.SelectedValue <> "HO" And ddl_hierarchy.SelectedValue <> "0" Then
                Return Val(ddl_location.SelectedValue)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If value > 0 Then
                Fill_hierarchy()
                ddl_hierarchy.SelectedValue = HierarchyCode
                FillLocation()
                ddl_location.SelectedValue = value
            End If
        End Set
    End Property

    Public Property HierarchyCode() As String Implements IHierarchiWithIdView.HierarchyCode
        Get
            Return ddl_hierarchy.SelectedValue
        End Get
        Set(ByVal value As String)
            ddl_hierarchy.SelectedValue = value
        End Set
    End Property

    Public ReadOnly Property GetHierarchyText() As String
        Get
            Return ddl_hierarchy.SelectedItem.Text
        End Get
    End Property

    Public ReadOnly Property GetLocationText() As String
        Get
            Return ddl_location.SelectedItem.Text
        End Get
    End Property

    Public WriteOnly Property Set_TD_Caption_Width() As String
        Set(ByVal value As String)
            td_Hierarchy_caption.Style.Add("width", value)
            td_location_caption.Style.Add("width", value)
        End Set
    End Property

    Public Sub Set_hierarchy_Width()
        td_location_caption.Style.Add("width", "8%")
        td_location_data.Style.Add("width", "42%")
    End Sub


    Public WriteOnly Property Set_TD_Data_Width() As String
        Set(ByVal value As String)
            td_Hierarchy_data.Style.Add("width", value)
            td_location_data.Style.Add("width", value)
        End Set
    End Property

    Public WriteOnly Property Set_Hierarchy_Caption() As String
        Set(ByVal value As String)
            lbl_Hierarchy_caption.Text = value
        End Set
    End Property

    Public WriteOnly Property Set_Location_Caption() As String
        Set(ByVal value As String)
            lbl_location_caption.Text = value
        End Set
    End Property

    Public Property Allow_All_Hierarchy() As Boolean
        Set(ByVal value As Boolean)
            chk_allow_all_hierarchy.Checked = value
        End Set
        Get
            Return chk_allow_all_hierarchy.Checked
        End Get
    End Property

    Public Property Show_First_Oprtion_As_Select_One() As Boolean
        Set(ByVal value As Boolean)
            chk_selectone_or_all.Checked = value
        End Set
        Get
            Return chk_selectone_or_all.Checked
        End Get
    End Property


    Public WriteOnly Property Set_TD_Caption_Visible() As Boolean
        Set(ByVal value As Boolean)
            td_Hierarchy_caption.Visible = value
            td_location_caption.Visible = value
        End Set
    End Property
    Public WriteOnly Property Set_Location_Visible() As Boolean
        Set(ByVal value As Boolean)
            td_location_caption.Visible = value
        End Set
    End Property


    Public WriteOnly Property Set_Control_Enable() As Boolean
        Set(ByVal value As Boolean)
            ddl_hierarchy.Enabled = value
            ddl_location.Enabled = value
        End Set
    End Property

    Private ReadOnly Property keyID()
        Get
            Return Val(ClassLibraryMVP.Util.DecryptToInt(Request.QueryString("Id")))
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Set_Default_Values(sender, e)
        End If
    End Sub


    'Protected Sub Page_OnInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If IsUsedForGrid = True And IsPostBack = False Then
    '        Set_Default_Values(sender, e)
    '    End If
    'End Sub



    Public Sub Set_Default_Values(ByVal sender As Object, ByVal e As System.EventArgs)
        Fill_hierarchy()
        If DDLHierarchyChange <> Nothing Then
            DDLHierarchyChange(sender, e)
        End If
        If ddl_location.AutoPostBack = True And keyID <= 0 Then
            If DDLLocationChange <> Nothing Then
                DDLLocationChange(sender, e)
            End If
        End If
        FillLocation()
    End Sub

    Private Sub Fill_hierarchy()
        Dim ds_hierarchy As New DataSet

        If UserManager.getUserParam.HierarchyCode = "HO" Or UserManager.getUserParam.HierarchyCode = "AD" Or Allow_All_Hierarchy = True Then
            Query = "select Hierarchy_No,Hierarchy_Name,Hierarchy_Code from EC_Master_Hierarchy where Hierarchy_Code in ('HO','RO','AO','BO')"
        ElseIf UserManager.getUserParam.HierarchyCode = "RO" Then
            Query = "select Hierarchy_No,Hierarchy_Name,Hierarchy_Code from EC_Master_Hierarchy where Hierarchy_Code in ('RO','AO','BO')"
        ElseIf UserManager.getUserParam.HierarchyCode = "AO" Then
            Query = "select Hierarchy_No,Hierarchy_Name,Hierarchy_Code from EC_Master_Hierarchy where Hierarchy_Code in ('AO','BO')"
        ElseIf UserManager.getUserParam.HierarchyCode = "BO" Then
            Query = "select Hierarchy_No,Hierarchy_Name,Hierarchy_Code from EC_Master_Hierarchy where Hierarchy_Code in ('BO')"
        End If

        Query = Query & " Order by Hierarchy_No"

        ds_hierarchy = objcommon.EC_Common_Pass_Query(Query)

        Dim dr As DataRow
        dr = ds_hierarchy.Tables(0).NewRow
        dr("Hierarchy_Name") = "Select One"
        dr("Hierarchy_Code") = "0"
        dr("Hierarchy_No") = "0"
        ds_hierarchy.Tables(0).Rows.Add(dr)


        Dim dt_hierarchy As New DataTable
        dt_hierarchy = objcommon.Get_View_Table(ds_hierarchy.Tables(0), "", "Hierarchy_No").ToTable()

        ddl_hierarchy.DataSource = dt_hierarchy
        ddl_hierarchy.DataTextField = "Hierarchy_Name"
        ddl_hierarchy.DataValueField = "Hierarchy_Code"
        ddl_hierarchy.DataBind()
        'ddl_hierarchy.Items.Insert(0, New ListItem("Select One", "0"))
        ''Raj.EC.Common.InsertItem(ddl_hierarchy)
    End Sub

    Private Sub FillLocation()
        Dim Ds_location As New DataSet
        Dim dodatabind As Boolean
        dodatabind = True
        lbl_location_caption.Visible = True
        ddl_location.Visible = True
        td_MDddl.Visible = True

        If ddl_hierarchy.SelectedValue = "RO" Then
            Query = "select Region_Name as loc_Name,Region_ID as Loc_ID from EC_Master_region where Is_Active =1"
            lbl_location_caption.Text = "Region :"
        ElseIf ddl_hierarchy.SelectedValue = "AO" Then
            Query = "select Area_Name as loc_Name,Area_ID as Loc_ID from EC_Master_Area where Is_Active =1"
            lbl_location_caption.Text = "Area :"
        ElseIf ddl_hierarchy.SelectedValue = "BO" Then
            Query = "select Branch_Name as loc_Name,Branch_ID as Loc_ID from EC_Master_branch where Is_Active =1"
            lbl_location_caption.Text = "Branch :"
        Else
            dodatabind = False
            lbl_location_caption.Visible = False
            ddl_location.Visible = False
            td_MDddl.Visible = False
        End If

        If dodatabind = True Then
            Query = Query & " order by loc_Name"
            Ds_location = objcommon.EC_Common_Pass_Query(Query)
            ddl_location.DataSource = Ds_location
            ddl_location.DataTextField = "loc_Name"
            ddl_location.DataValueField = "Loc_ID"
            ddl_location.DataBind()
            If Show_First_Oprtion_As_Select_One = True Then
                ddl_location.Items.Insert(0, New ListItem("Select One", "0"))
            Else
                ddl_location.Items.Insert(0, New ListItem("All", "0"))
            End If
        End If

    End Sub

    Protected Sub ddl_hierarchy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_hierarchy.SelectedIndexChanged
        FillLocation()
        If DDLHierarchyChange <> Nothing Then
            DDLHierarchyChange(sender, e)
        End If
        If DDLLocationChange <> Nothing Then
            DDLLocationChange(sender, e)
        End If

    End Sub

    Protected Sub ddl_location_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_location.SelectedIndexChanged
        If ddl_location.AutoPostBack = True Then
            If DDLLocationChange <> Nothing Then
                DDLLocationChange(sender, e)
            End If
        End If
    End Sub


    Public Function validateHierarchyWithIdUI(ByVal lbl_error As Label) As Boolean  'added by Ankit  : 15-11-08 : 12.00 pm
        Dim isValid As Boolean

        If (HierarchyCode = "0") Then
            lbl_error.Text = "Please Select Hierarchy"
            ddl_hierarchy.Focus()
        ElseIf (Is_Ho = False And RegionID <= 0 And AreaID <= 0 And BranchID <= 0) Then
            lbl_error.Text = "Please Select Location"
            ddl_location.Focus()
        Else
            isValid = True
        End If

        Return isValid

    End Function

End Class
