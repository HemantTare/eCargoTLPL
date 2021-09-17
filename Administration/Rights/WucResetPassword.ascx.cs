
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;
using Raj.EC;

public partial class Admin_Rights_WucResetPassword : System.Web.UI.UserControl 
{    
    #region ClassVariables
    #endregion

    #region ControlsValues

    public int UserID
    {
          
        set { ddl_User.SelectedValue = Util.Int2String(value); }
        get{ return Util.String2Int(ddl_User.SelectedValue);}
    }

    public bool IsEmployee
    {
        set { rbl_Employee.Checked = value; }
        get
        {
            if (ddl_UserType.SelectedValue == "2")
            {
                return rbl_Employee.Checked;
            }
            else
            {
                return false;
            }
        }
    }

    public bool IsNonEmployee
    {
        set { rbl_NonEmployee.Checked = value; }
        get
        {
            if (ddl_UserType.SelectedValue == "2")
            {
                return rbl_NonEmployee.Checked;
            }
            else
            {
                return false;
            }
        }
    }
    public int UserTypeId
    {
        set { hdn_user_type_id.Value = value.ToString(); }
        get { return Util.String2Int(hdn_user_type_id.Value); }
    }

    public bool IsClient
    {
        set { rbl_Client.Checked = value; }
        get
        {
            if (ddl_UserType.SelectedValue == "2")
            {
                return rbl_Client.Checked;
            }
            else
            {
                return false;
            }
        }
    }
    
    #endregion    

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        if (Util.String2Int(ddl_User.SelectedValue) <= 0)
        {
            errorMessage = "Please Select User Name";
            setfocusonusername();
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    public string errorMessage
    {
        set{lbl_Errors.Text = value;}
    }
   
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_User.DataTextField = "Name";
        ddl_User.DataValueField = "ID";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));


        SetOthercolumns();
        if (!IsPostBack)
        {
            SetUser();
            setfocusonusername();
        }
    }

    private void SetOthercolumns()
    {
        string oc = "";
        if (ddl_UserType.SelectedValue == "1")
        {
            oc = "all";
        }
        else if (ddl_UserType.SelectedValue == "2" && rbl_Employee.Checked == true)
        {
            oc = "Employee";
        }
        else if (ddl_UserType.SelectedValue == "2" && rbl_NonEmployee.Checked == true)
        {
            oc = "Non_Employee";
        }
        else if (ddl_UserType.SelectedValue == "2" && rbl_Client.Checked == true)
        {
            oc = "Client";
        }

        ddl_User.OtherColumns = oc.ToString();
    }

    private void setfocusonusername()
    {
        TextBox txt_ddluser = (TextBox)ddl_User.FindControl("txtBoxddl_User");
        txt_ddluser.Focus();
    }


    protected void rbl_Employee_CheckedChanged(object sender, EventArgs e)
    {
        SetUser();
        SetUser("", "0");
        SetOthercolumns();
        setfocusonusername();
    }

    protected void rbl_NonEmployee_CheckedChanged(object sender, EventArgs e)
    {
        SetUser();
        SetUser("", "0");
        SetOthercolumns();
        setfocusonusername();
    }

    protected void rbl_Client_CheckedChanged(object sender, EventArgs e)
    {
        SetUser();
        SetUser("", "0");
        SetOthercolumns();
        setfocusonusername();
    }

    public void SetUser(string text, string value)
    {
        ddl_User.DataTextField = "Name";
        ddl_User.DataValueField = "ID";

        Common.SetValueToDDLSearch(text, value, ddl_User);
    }

    private void SetUser()
    {
        errorMessage = "";
        if (ddl_UserType.SelectedValue == "1")
        {
            rbl_Employee.Checked = false;
            rbl_NonEmployee.Checked = false;
            rbl_Client.Checked = false;

            tr_Employee.Visible = false;
            tr_user_details1.Visible = false;
            tr_user_details2.Visible = false;
            tr_user_details3.Visible = false;
        }
        else if (ddl_UserType.SelectedValue == "2" && rbl_Employee.Checked == true)
        {
            lbl_DepartmentValue.Text = "";
            lbl_ProfileValue.Text = "";
            lbl_HierarchyValue.Text = "";

            rbl_NonEmployee.Checked = false;
            rbl_Client.Checked = false;

            tr_Employee.Visible = true;
            tr_user_details1.Visible = false;
            tr_user_details2.Visible = false;
            tr_user_details3.Visible = false;
        }
        else if (ddl_UserType.SelectedValue == "2" && rbl_NonEmployee.Checked == true)
        {
            rbl_Employee.Checked = false;
            rbl_Client.Checked = false;

            tr_Employee.Visible = true;
            tr_user_details1.Visible = false;
            tr_user_details2.Visible = false;
            tr_user_details3.Visible = false;
        }
        else if (ddl_UserType.SelectedValue == "2" && rbl_Client.Checked == true)
        {
            rbl_Employee.Checked = false;
            rbl_NonEmployee.Checked = false;

            tr_Employee.Visible = true;
            tr_user_details1.Visible = false;
            tr_user_details2.Visible = false;
            tr_user_details3.Visible = false;
        }
    }

    protected void ddl_User_TxtChange(object sender, EventArgs e)
    {
        if (UserID > 0)
        {

            DataSet objDS = new DataSet();
            DAL objDAL = new DAL();

            SqlParameter[] param ={
                objDAL.MakeOutParams("@User_Type_ID",SqlDbType.Int,0),
                objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, UserID),
                objDAL.MakeInParams("@Is_Employee",SqlDbType.Bit,1, IsEmployee),
                objDAL.MakeInParams("@Is_non_Employee",SqlDbType.Bit,1,IsNonEmployee),
                objDAL.MakeInParams("@Is_Client",SqlDbType.Bit,1,IsClient),
                objDAL.MakeInParams("@vt",SqlDbType.VarChar,1,"y")};

            objDAL.RunProc("[dbo].[Reset_Password_Get_User_Details]", param, ref objDS);

            UserTypeId = Util.String2Int(param[0].Value.ToString());

            if (objDS.Tables[0].Rows.Count > 0)
            {
                errorMessage = "";
                tr_user_details1.Visible = true;
                tr_user_details2.Visible = true;
                tr_user_details3.Visible = true;

                if (UserTypeId == 1)
                {
                    lbl_Department.Text = "Employee Name";
                    lbl_Profile.Text = "Profile";
                    lbl_Hierarchy.Text = "Hierarchy";
                    lbl_username.Text = "Department";
                    lbl_user_id.Text = "Login ID";

                    lbl_DepartmentValue.Text = objDS.Tables[0].Rows[0]["Emp_Name"].ToString();
                    lbl_ProfileValue.Text = objDS.Tables[0].Rows[0]["Profile_Name"].ToString();
                    lbl_HierarchyValue.Text = objDS.Tables[0].Rows[0]["hierarchy"].ToString();
                    lbl_usernamevalue.Text = objDS.Tables[0].Rows[0]["Department_Name"].ToString();
                    lbl_user_id_value.Text = objDS.Tables[0].Rows[0]["user_name"].ToString();
                }
                else if (UserTypeId == 2)
                {
                    lbl_Department.Text = "User Name";
                    lbl_Profile.Text = "Login ID";
                    lbl_Hierarchy.Text = "";
                    lbl_username.Text = "";
                    lbl_user_id.Text = "";

                    lbl_DepartmentValue.Text = objDS.Tables[0].Rows[0]["Non_Emp_User_Name"].ToString();
                    lbl_ProfileValue.Text = objDS.Tables[0].Rows[0]["user_name"].ToString();
                    lbl_HierarchyValue.Text = "";
                    lbl_usernamevalue.Text = "";
                    lbl_user_id_value.Text = "";
                }
                else if (UserTypeId == 3)
                {

                    lbl_Department.Text = "Client Name";
                    lbl_Profile.Text = "Client Group";
                    lbl_Hierarchy.Text = "Client Branch";
                    lbl_username.Text = "";
                    lbl_user_id.Text = "Login ID";

                    lbl_DepartmentValue.Text = objDS.Tables[0].Rows[0]["client_name"].ToString();
                    lbl_ProfileValue.Text = objDS.Tables[0].Rows[0]["Client_Group_Name"].ToString();
                    lbl_HierarchyValue.Text = objDS.Tables[0].Rows[0]["branch_name"].ToString();
                    lbl_usernamevalue.Text = "";
                    lbl_user_id_value.Text = objDS.Tables[0].Rows[0]["user_name"].ToString();
                }

                btn_Reset.Focus();
            }
        }
        else
        {
            errorMessage = "Please Select User";
            setfocusonusername();
        }
    }
        
    protected void ddl_UserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_UserType.SelectedValue == "2")
            rbl_Employee.Checked = true;
        SetUser();
        SetUser("", "0");
        SetOthercolumns();
        setfocusonusername();
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            DataSet objDS = new DataSet();
            DAL objDAL = new DAL();

            int user_type_id = 0;
            if (ddl_UserType.SelectedValue != "1")
                user_type_id = UserTypeId;

            SqlParameter[] param ={ objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, UserID ),
                                    objDAL.MakeInParams("@User_type_Id",SqlDbType.Int,0,user_type_id)
                                   };

            objDAL.RunProc("[dbo].[Com_ADM_Reset_Password_Save]", param, ref objDS);
            lbl_Errors.Text = "Password Reset Successfully";
        }
    }
#endregion


}
