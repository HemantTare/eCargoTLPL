Imports Raj.eCargo
Imports System.Data.SqlClient
Imports System.Data
Imports ClassLibraryMVP
Imports Raj.EC
Imports ClassLibraryMVP.DataAccess

Partial Class Master_General_wucBranchLogin
    Inherits System.Web.UI.UserControl

    Dim objCommon As New Common()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Fill_Year()
            Init_Settings()
        End If
    End Sub

    Private Sub Fill_Year()
        ddl_Year.DataSource = objCommon.EC_Common_Pass_Query("select year_code,Financial_Year From ec_master_year Where Is_Active=1 order by year_code desc")
        ddl_Year.DataTextField = "Financial_Year"
        ddl_Year.DataValueField = "Year_Code"
        ddl_Year.DataBind()
        ddl_Year.SelectedValue = UserManager.getUserParam().YearCode
    End Sub

    Private Sub Init_Settings()

        trdivision.Visible = CompanyManager.getCompanyParam().IsActivateDivision
        lbl_division_caption.Text = "Select " & CompanyManager.getCompanyParam().DivisionCaption & " :"

        If UserManager.getUserParam().HierarchyCode = "BO" Then
            rbl_login_heirarchy.Items(0).Enabled = False
            rbl_login_heirarchy.Items(1).Enabled = False
            rbl_login_heirarchy.Items(2).Enabled = False
            btnlogin.Enabled = False
        ElseIf UserManager.getUserParam().HierarchyCode = "AO" Then
            rbl_login_heirarchy.Items(1).Enabled = False
            rbl_login_heirarchy.Items(2).Enabled = False
        ElseIf UserManager.getUserParam().HierarchyCode = "RO" Then
            rbl_login_heirarchy.Items(2).Enabled = False
        End If

        rbl_order_by.Enabled = False
        rbl_order_by.Items(0).Selected = False
        rbl_order_by.Items(1).Selected = False
        rbl_order_by.Items(2).Selected = False
    End Sub

    Protected Sub rbl_login_heirarchy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbl_login_heirarchy.SelectedIndexChanged
        Fill_Branch_Area_Region()
        FillUsers()
        getuserdetails()
        FillDivision()
        ddl_Year.Focus()
    End Sub


    Private Sub Fill_Branch_Area_Region()
        Dim WC As String = "1=1 "

        If UserManager.getUserParam().HierarchyCode = "BO" Then
            WC = "Branch_ID = " & UserManager.getUserParam().MainId
        ElseIf UserManager.getUserParam().HierarchyCode = "AO" Then
            WC = "Area_ID = " & UserManager.getUserParam().MainId
        ElseIf UserManager.getUserParam().HierarchyCode = "RO" Then
            WC = "Region_ID = " & UserManager.getUserParam().MainId
        End If

        If rbl_login_heirarchy.SelectedValue = "0" Then
            lblBranch.Text = "Select Branch:"
            ddlBranch.DataTextField = "Branch_Name"
            ddlBranch.DataValueField = "Branch_ID"
            ddlBranch.DataSource = objCommon.EC_Common_Pass_Query("Select Branch_ID, Branch_Name from EC_Master_Branch Where Is_Active=1 And (IsNull(Is_VTrans_Booking,0)=1 OR IsNull(Is_VTrans_Delivery,0)=1 OR IsNull(Is_Crossing,0)=1) And " & WC & " Order By Branch_Name")
        ElseIf rbl_login_heirarchy.SelectedValue = "1" Then
            lblBranch.Text = "Select Area:"
            ddlBranch.DataTextField = "Area_Name"
            ddlBranch.DataValueField = "Area_ID"
            ddlBranch.DataSource = objCommon.EC_Common_Pass_Query("Select Area_ID, Area_Name from EC_Master_Area Where Is_Active=1 And " & WC & " Order By Area_Name")
        ElseIf rbl_login_heirarchy.SelectedValue = "2" Then
            lblBranch.Text = "Select Region:"
            ddlBranch.DataTextField = "Region_Name"
            ddlBranch.DataValueField = "Region_ID"
            ddlBranch.DataSource = objCommon.EC_Common_Pass_Query("Select Region_ID, Region_Name from EC_Master_Region Where Is_Active=1 And " & WC & " Order By Region_Name")
        End If

        ddlBranch.DataBind()

        If ddlBranch.Items.Count <= 0 Then
            rbl_order_by.Items(0).Selected = False
            rbl_order_by.Items(1).Selected = False
            rbl_order_by.Items(2).Selected = False
            rbl_order_by.Enabled = False
        Else
            rbl_order_by.Enabled = True
            rbl_order_by.SelectedValue = "0"
        End If

    End Sub

    Private Sub FillUsers()
        Dim ds_employee_details As New DataSet
        Dim query As String = ""

        query = query & "select (user_name + ' - ' + First_Name + ' ' + Middle_Name + ' ' + Last_Name + ' / ' + Profile_Name) "
        query = query & "as User_Details,User_ID,password,user_name "
        query = query & "from com_adm_user a "
        query = query & "inner join ec_master_employee b "
        query = query & "on a.Employee_Id = b.emp_id "
        query = query & "inner join com_adm_profile c "
        query = query & "on a.profile_id = c.profile_id "
        query = query & "where(b.is_active = 1) "
        query = query & "and a.user_id <> " & UserManager.getUserParam().UserId

        If rbl_login_heirarchy.SelectedValue = "2" Then
            query = query & "and b.Region_id = " & Val(ddlBranch.SelectedValue)
        ElseIf rbl_login_heirarchy.SelectedValue = "1" Then
            query = query & "and b.Area_id = " & Val(ddlBranch.SelectedValue)
        ElseIf rbl_login_heirarchy.SelectedValue = "0" Then
            query = query & "and b.branch_id = " & Val(ddlBranch.SelectedValue)
        End If

        If rbl_order_by.SelectedValue = "0" Then
            query = query & " Order by a.User_Name"
        ElseIf rbl_order_by.SelectedValue = "1" Then
            query = query & " Order by b.First_Name"
        ElseIf rbl_order_by.SelectedValue = "2" Then
            query = query & " Order by c.Profile_Name"
        End If



        ds_employee_details = objCommon.EC_Common_Pass_Query(query)
        ddluser.DataTextField = "User_Details"
        ddluser.DataValueField = "User_ID"
        ddluser.DataSource = ds_employee_details
        ddluser.DataBind()

        If ddluser.Items.Count <= 0 Then
            rbl_order_by.Items(0).Selected = False
            rbl_order_by.Items(1).Selected = False
            rbl_order_by.Items(2).Selected = False
            rbl_order_by.Enabled = False
            btnlogin.Focus()
        Else
            rbl_order_by.Enabled = True 
        End If
    End Sub

    Protected Sub ddlBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBranch.SelectedIndexChanged
        FillUsers()
        getuserdetails()
        FillDivision()

        If ddluser.Items.Count > 0 Then
            rbl_order_by.Focus()
        Else
            btnlogin.Focus()
        End If

    End Sub

    Protected Sub btnlogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        If Allow_To_Save() = True Then

            Dim Machine_IP As String = Request.ServerVariables("REMOTE_ADDR")
            Dim objDAL As New DAL()


            Dim param() As SqlParameter = {objDAL.MakeInParams("@Actual_Logger", SqlDbType.Int, 0, UserManager.getUserParam().UserId), _
            objDAL.MakeInParams("@Logged_in_As", SqlDbType.Int, 0, Util.String2Int(ddluser.SelectedValue)), _
            objDAL.MakeInParams("@division_id", SqlDbType.Int, 0, Util.String2Int(ddl_division.SelectedValue)), _
            objDAL.MakeInParams("@Machine_IP", SqlDbType.VarChar, 50, Machine_IP)}

            objDAL.RunProc("[dbo].[Ec_Master_LoginAs_Add]", param)

            System.Web.HttpContext.Current.Session.RemoveAll()
            System.Web.HttpContext.Current.Session.Abandon()

            Response.Redirect("~/FrmLogin.aspx?SwitchTo=" + ddl_division.SelectedValue _
            + "&username=" + hdn_user_name.Value _
            + "&userpassword=" + hdn_password.Value _
            + "&useryearcode=" + ddl_Year.SelectedValue _
            + "&macid=" & hdn_CPUid.Value)

        End If
    End Sub


    Private Function Allow_To_Save() As Boolean
        If Val(ddlBranch.SelectedValue) <= 0 Then
            lbl_Errors.Text = "Please " & lblBranch.Text
            ddlBranch.Focus()
        ElseIf Val(ddluser.SelectedValue) <= 0 Then
            lbl_Errors.Text = "Please Select User"
            If ddluser.Items.Count > 0 Then
                ddluser.Focus()
            Else
                rbl_login_heirarchy.Focus()
            End If

        Else
            Allow_To_Save = True
        End If

            lbl_Errors.Visible = Not (Allow_To_Save)
    End Function

    Protected Sub rbl_order_by_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbl_order_by.SelectedIndexChanged
        FillUsers()
        getuserdetails()
        FillDivision()
        ddluser.Focus()
    End Sub

    Protected Sub ddluser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddluser.SelectedIndexChanged
        getuserdetails()
        FillDivision()
    End Sub

    Private Sub getuserdetails()
        If Val(ddluser.SelectedValue) > 0 Then
            Dim ds_user_details As New DataSet
            Dim query As String = ""
            query = "select user_name,password,Employee_Id from com_adm_user where user_id = " & ddluser.SelectedValue
            ds_user_details = objCommon.EC_Common_Pass_Query(query)

            Dim dr_user_details As DataRow
            dr_user_details = ds_user_details.Tables(0).Rows(0)
            hdn_user_name.Value = dr_user_details("user_name").ToString()
            hdn_password.Value = Util.EncryptString(dr_user_details("password").ToString())
            hdn_emp_id.Value = dr_user_details("Employee_Id").ToString()
        End If
    End Sub

    Private Sub FillDivision()
        If Val(ddluser.SelectedValue) > 0 Then
            Dim query As New StringBuilder

            query.Append("select distinct EmpDivison.Division_Id,Division_Name ")
            query.Append("From dbo.EC_Master_Employee_Divisions EmpDivison ")
            query.Append("inner join EC_Master_Division Division ")
            query.Append("on EmpDivison.Division_Id =Division.Division_Id ")
            query.Append("Where Emp_ID = " + hdn_emp_id.Value)

            ddl_division.DataSource = objCommon.EC_Common_Pass_Query(query.ToString())
            ddl_division.DataTextField = "Division_Name"
            ddl_division.DataValueField = "Division_Id"
            ddl_division.DataBind()
        End If
    End Sub
End Class
