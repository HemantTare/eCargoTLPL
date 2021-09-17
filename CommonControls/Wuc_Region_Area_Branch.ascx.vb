Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System

Imports Raj.EC
Imports Raj.EC.Security

Imports Raj.EC.ControlsView

Partial Class Controls_Wuc_Region_Area_Branch
    Inherits System.Web.UI.UserControl

    Dim objcommon As New Common
    Dim Query As String = ""
    Public BranchIndexChange As EventHandler
    Public AreaIndexChange As EventHandler
    Public RegionIndexChange As EventHandler
    Public AfterLoad As EventHandler

    Dim _Region_ID As Integer
    Dim _Area_ID As Integer
    Dim _Branch_ID As Integer

    Private Property _RegionID() As Integer
        Get
            Return Val(ViewState("Region_ID"))
        End Get
        Set(ByVal value As Integer)
            ViewState("Region_ID") = value
        End Set
    End Property

    Private Property _AreaID() As Integer
        Get
            Return Val(ViewState("Area_ID"))
        End Get
        Set(ByVal value As Integer)
            ViewState("Area_ID") = value
        End Set
    End Property

    Private Property _BranchID() As Integer
        Get
            Return Val(ViewState("Branch_ID"))
        End Get
        Set(ByVal value As Integer)
            ViewState("Branch_ID") = value
        End Set
    End Property

    Public WriteOnly Property SetDDLBranchAutoPostback() As Boolean
        Set(ByVal value As Boolean)
            ddl_Branch.AutoPostBack = value
        End Set
    End Property

    Public WriteOnly Property SetRegionCaption() As String
        Set(ByVal value As String)
            lbl_region_caption.Text = value
        End Set
    End Property

    Public WriteOnly Property SetAreaCaption() As String
        Set(ByVal value As String)
            lbl_area_caption.Text = value
        End Set
    End Property

    Public WriteOnly Property SetBranchCaption() As String
        Set(ByVal value As String)
            lbl_branch_cation.Text = value
        End Set
    End Property

    Public ReadOnly Property BranchID() As Integer
        Get
            Return Val(ddl_Branch.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property SelectedBranchText() As String
        Get
            Return ddl_Branch.SelectedItem.Text
        End Get
    End Property

    Public ReadOnly Property AreaID() As Integer
        Get
            Return Val(ddl_Area.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property SelectedAreaText() As String
        Get
            Return ddl_Area.SelectedItem.Text
        End Get
    End Property

    Public ReadOnly Property RegionID() As Integer
        Get
            Return Val(ddl_region.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property SelectedRegionText() As String
        Get
            Return ddl_region.SelectedItem.Text
        End Get
    End Property

    Public ReadOnly Property IsHo() As Boolean
        Get
            Return chk_IsHO.Checked
        End Get
    End Property

    Public ReadOnly Property Selected_Region_Only() As Boolean
        Get
            Return chk_selected_region_only.Checked
        End Get
    End Property

    Public ReadOnly Property Selected_Area_Only() As Boolean
        Get
            Return chk_selected_area_only.Checked
        End Get
    End Property


    Public WriteOnly Property HoVisibility() As Boolean
        Set(ByVal value As Boolean)
            tr_ho.Visible = value
        End Set
    End Property

    Public WriteOnly Property SelectedLocationsOnlyVisibility() As Boolean
        Set(ByVal value As Boolean)
            tr_selected_locations_only.Visible = value
        End Set
    End Property

    Public WriteOnly Property ShowAll() As Boolean
        Set(ByVal value As Boolean)
            chk_show_all.Checked = value
        End Set
    End Property

    Public WriteOnly Property visibilityBranch() As Boolean
        Set(ByVal value As Boolean)
            td_branch_caption.Visible = value
            td_branch_data.Visible = value
        End Set
    End Property

    Public WriteOnly Property visibilityRegion() As Boolean
        Set(ByVal value As Boolean)
            td_region_caption.Visible = value
            td_region_data.Visible = value
        End Set
    End Property

    Public WriteOnly Property Set_TD_Caption_Width() As String
        Set(ByVal value As String)
            td_ho_caption.Style.Add("width", value)
            td_region_caption.Style.Add("width", value)
            td_area_caption.Style.Add("width", value)
            td_branch_caption.Style.Add("width", value)
            td_region_only_caption.Style.Add("width", value)
            td_area_only_caption.Style.Add("width", value)
            td_region_only_caption.Style.Add("width", value)
        End Set
    End Property

    Public WriteOnly Property Set_TD_Data_Width() As String
        Set(ByVal value As String)
            td_ho_data.Style.Add("width", value)
            td_region_data.Style.Add("width", value)
            td_area_data.Style.Add("width", value)
            td_branch_data.Style.Add("width", value)
            td_area_only_data.Style.Add("width", value)
            td_region_only_data.Style.Add("width", value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            PageLoad(sender, e)
        End If
    End Sub

    Public Sub PageLoad(ByVal sender As Object, ByVal e As System.EventArgs)
        Set_Hierarchy_IDs()
        Fill_Region()
        Fill_Area()
        Fill_Branch()
        If ddl_Branch.AutoPostBack = True Then
            If BranchIndexChange <> Nothing Then
                BranchIndexChange(sender, e)
            End If
        End If

        If AfterLoad <> Nothing Then
            AfterLoad(sender, e)
        End If
    End Sub

    Private Sub Set_Hierarchy_IDs()

        If UserManager.getUserParam.HierarchyCode = "AD" Or UserManager.getUserParam.HierarchyCode = "HO" Or chk_show_all.Checked = True Then
            _Region_ID = 0
            _Area_ID = 0
            _Branch_ID = 0

            ddl_region.Enabled = True
            ddl_Area.Enabled = True
            ddl_Branch.Enabled = True
        Else
            Dim ds As New DataSet

            If UserManager.getUserParam.HierarchyCode = "RO" Then
                _Region_ID = UserManager.getUserParam.MainId
                ddl_region.Enabled = False
            ElseIf UserManager.getUserParam.HierarchyCode = "AO" Then
                _Area_ID = UserManager.getUserParam.MainId
                Query = "select region_ID from ec_master_Area where area_id = " & UserManager.getUserParam.MainId
                ds = objcommon.EC_Common_Pass_Query(Query)
                _Region_ID = ds.Tables(0).Rows(0)("Region_ID")

                ddl_region.Enabled = False
                ddl_Area.Enabled = False
            ElseIf UserManager.getUserParam.HierarchyCode = "BO" Then
                _Branch_ID = UserManager.getUserParam.MainId
                Query = "select Area_ID,region_ID from ec_master_branch where branch_id = " & UserManager.getUserParam.MainId
                ds = objcommon.EC_Common_Pass_Query(Query)
                _Region_ID = ds.Tables(0).Rows(0)("Region_ID")
                _Area_ID = ds.Tables(0).Rows(0)("Area_ID")

                ddl_region.Enabled = False
                ddl_Area.Enabled = False
                ddl_Branch.Enabled = False
            End If
        End If
    End Sub

    Private Sub Set_Selected_Value()
        If chk_show_all.Checked = False Then
            If UserManager.getUserParam.HierarchyCode = "RO" Then
                ddl_region.SelectedValue = _Region_ID
            ElseIf UserManager.getUserParam.HierarchyCode = "AO" Then
                ddl_region.SelectedValue = _Region_ID
                ddl_Area.SelectedValue = _Area_ID
            ElseIf UserManager.getUserParam.HierarchyCode = "BO" Then
                ddl_region.SelectedValue = _Region_ID

                Try
                    ddl_Area.SelectedValue = _Area_ID
                    ddl_Branch.SelectedValue = _Branch_ID
                Catch ex As Exception


                End Try



            End If
        End If
    End Sub

    Private Sub Fill_Region()
        Query = "select region_ID,region_name from ec_master_region where is_active = 1"
        Query = Query & " and Region_Id = case " & _Region_ID & " when 0 then Region_ID else " & _Region_ID & " end"
        Query = Query & " order by region_name"

        ddl_region.DataValueField = "region_id"
        ddl_region.DataTextField = "region_name"
        ddl_region.DataSource = objcommon.EC_Common_Pass_Query(Query)
        ddl_region.DataBind()
        ddl_region.Items.Insert(0, New ListItem("All", "0"))

        Set_Selected_Value()
    End Sub

    Private Sub Fill_Area()
        ddl_Area.Items.Clear()
        If Val(ddl_region.SelectedValue) > 0 Then
            Query = "select area_ID,area_name from ec_master_area where is_active = 1 And region_id = " & Val(ddl_region.SelectedValue)
            Query = Query & " and Area_Id = case " & _Area_ID & " when 0 then Area_Id else " & _Area_ID & " end"
            Query = Query & " order by area_name"

            ddl_Area.DataValueField = "area_ID"
            ddl_Area.DataTextField = "area_name"
            ddl_Area.DataSource = objcommon.EC_Common_Pass_Query(Query)
            ddl_Area.DataBind()
        End If
        ddl_Area.Items.Insert(0, New ListItem("All", "0"))

        Set_Selected_Value()
    End Sub

    Private Sub Fill_Branch()
        ddl_Branch.Items.Clear()
        If Val(ddl_Area.SelectedValue) > 0 Then
            Query = "select branch_ID,branch_name from ec_master_branch where is_active = 1 And Area_id = " & Val(ddl_Area.SelectedValue)
            Query = Query & " and branch_ID = case " & _Branch_ID & " when 0 then branch_ID else " & _Branch_ID & " end"
            Query = Query & " order by branch_name"

            ddl_Branch.DataValueField = "branch_ID"
            ddl_Branch.DataTextField = "branch_name"
            ddl_Branch.DataSource = objcommon.EC_Common_Pass_Query(Query)
            ddl_Branch.DataBind()
        End If
        ddl_Branch.Items.Insert(0, New ListItem("All", "0"))

        Set_Selected_Value()


        If ddl_Branch.AutoPostBack = True Then
            If BranchIndexChange <> Nothing Then
                Dim e As EventArgs
                BranchIndexChange("d", e)
            End If
        End If
    End Sub

    Protected Sub ddl_region_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_region.SelectedIndexChanged
        Fill_Area()
        Fill_Branch()
        If RegionIndexChange <> Nothing Then
            RegionIndexChange(sender, e)
        End If
    End Sub

    Protected Sub ddl_Area_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Area.SelectedIndexChanged
        Fill_Branch()
        If AreaIndexChange <> Nothing Then
            AreaIndexChange(sender, e)
        End If
    End Sub

    Protected Sub ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Branch.SelectedIndexChanged
        If ddl_Branch.AutoPostBack = True Then
            If BranchIndexChange <> Nothing Then
                BranchIndexChange(sender, e)
            End If
        End If
    End Sub

    Protected Sub chk_IsHO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_IsHO.CheckedChanged
        If chk_IsHO.Checked = True Then
            Fill_Region()
            Fill_Area()
            Fill_Branch()

            ddl_region.Enabled = False
            ddl_Area.Enabled = False
            ddl_Branch.Enabled = False

            chk_selected_area_only.Checked = False
            chk_selected_region_only.Checked = False

            chk_selected_area_only.Enabled = False
            chk_selected_region_only.Enabled = False
        Else
            ddl_region.Enabled = True
            ddl_Area.Enabled = True
            ddl_Branch.Enabled = True
            chk_selected_area_only.Enabled = True
            chk_selected_region_only.Enabled = True
        End If
    End Sub

    Protected Sub chk_selected_region_only_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_selected_region_only.CheckedChanged
        If chk_selected_region_only.Checked = True Then
            ddl_Area.SelectedValue = 0
            ddl_Branch.SelectedValue = 0
            chk_selected_area_only.Checked = False
        End If

        chk_selected_area_only.Enabled = Not (chk_selected_region_only.Checked)
        ddl_Area.Enabled = Not (chk_selected_region_only.Checked)
        ddl_Branch.Enabled = Not (chk_selected_region_only.Checked)
    End Sub

    Protected Sub chk_selected_area_only_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_selected_area_only.CheckedChanged
        If chk_selected_area_only.Checked = True Then
            ddl_Branch.SelectedValue = 0
        End If
        ddl_Branch.Enabled = Not (chk_selected_area_only.Checked)
    End Sub
End Class
