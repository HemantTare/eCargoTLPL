
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter; 
using ClassLibraryMVP;

using Raj.EC.ControlsView;

public partial class EC_Master_wucEmployee : System.Web.UI.UserControl, IEmployeeView
{
    #region Declaration
    private EmployeePresenter _EmployeePresenter;
    PageControls pc = new PageControls();
    bool isValid = false;
    public DateTime dt = new DateTime(1900, 1, 1);
    #endregion

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //  return 34;
        }
    }

    #endregion

    #region InitInterface


    public IHierarchiWithIdView HierarchiWithIdView
    {
        get { return (IHierarchiWithIdView)WucHierarchyWithID1; }
    }

    public int Branch_Id
    {
        set
        {
            hdn_Branch_Id.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_Branch_Id.Value);
        }
    }

    public int Area_Id
    {
        set
        {
            hdn_Area_Id.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_Area_Id.Value);
        }
    }

    public int Region_Id
    {
        set
        {
            hdn_Region_Id.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_Region_Id.Value);
        }
    }

    public Boolean Is_eCargoUser
    {
        set
        {
            chk_IseCargoUser.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IseCargoUser.Checked);
        }
    }

    public Boolean Is_HO
    {
        set
        {
            hdn_Is_HO.Value = Convert.ToString(value);
        }
        get
        {
            return Convert.ToBoolean(hdn_Is_HO.Value);
        }
    }

    public string EmployeeCode
    {
        set
        {
            txt_EmployeeCode.Text = value;
        }
        get
        {
            return txt_EmployeeCode.Text;
        }
    }

    public string FirstName
    {
        set
        {
            txt_FirstName.Text = value;
        }
        get
        {
            return txt_FirstName.Text;
        }
    }

    public string HierarchyCode
    {
        get
        {
            return WucHierarchyWithID1.HierarchyCode;
        }
    }

    public string MiddleName
    {
        set
        {
            txt_MiddleName.Text = value;
        }
        get
        {
            return txt_MiddleName.Text;
        }
    }

    public string LastName
    {
        set
        {
            txt_LastName.Text = value;
        }
        get
        {
            return txt_LastName.Text;
        }
    }

    public string Qualification
    {
        set
        {
            txt_Qualification.Text = value;
        }
        get
        {
            return txt_Qualification.Text;
        }
    }

    public string PreviousJobProfile
    {
        set
        {
            txt_PreviousJobProfile.Text = value;
        }
        get
        {
            return txt_PreviousJobProfile.Text;
        }

    }

    public string Email
    {
        set
        {
            txt_Email.Text = value;
        }
        get
        {
            return txt_Email.Text;
        }
    }

    public int Department_Id
    {
        set
        {
            ddl_Department.SelectedValue = Convert.ToString(value);
        }
        get
        {
            return Convert.ToInt32(ddl_Department.SelectedValue);
        }
    }

    public int UserProfile_Id
    {
        set
        {
            ddl_UserProfile.SelectedValue = Convert.ToString(value);
        }
        get
        {
            return Convert.ToInt32(ddl_UserProfile.SelectedValue);
        }
    }

    public Boolean Is_Active
    {
        get { return chk_IsActive.Checked; }
        set { chk_IsActive.Checked = value; }
    }

    //Added : Anita On: 05 March 09
    public Boolean Is_Account_Locked
    {
        get { return chk_Is_Account_Locked.Checked; }
        set { chk_Is_Account_Locked.Checked = value; }
    }
    //=============================Controls ADDED FOR PAYROLL 07 March 09=======================================
    public DateTime Birth_Date
    {
        get { return Convert.ToDateTime(birth_date.SelectedDate); }
        set { birth_date.SelectedDate = Convert.ToDateTime(value); }
    }
    public DateTime DOJ
    {
        get { return Convert.ToDateTime(date_Joinning.SelectedDate); }
        set { date_Joinning.SelectedDate = Convert.ToDateTime(value); }
    }
    public DateTime Confirmation_Date
    {
        get
        {
            if (chk_Confirmation.Checked == true)
            {
                return Convert.ToDateTime(Confirmation_date.SelectedDate);
            }
            else
            {
                return dt;
            }
        }
        set
        {
            if (value > dt)
            {
                chk_Confirmation.Checked = true;
                Confirmation_date.SelectedDate = Convert.ToDateTime(value);
            }
            else
            {
                chk_Confirmation.Checked = false;
                Confirmation_date.SelectedDate = System.DateTime.Now;
            }
        }

    }
    public int Weekly_Off
    {
        get { return Util.String2Int(ddl_Weekly_off.SelectedValue); }
        set { ddl_Weekly_off.SelectedValue = Util.Int2String(value); }
    }
    public decimal Basic_Rate
    {
        get { return Util.String2Decimal(txt_BasicRate.Text); }
        set { txt_BasicRate.Text = Util.Decimal2String(value); }
    }
    public int Probation_Period
    {
        get
        {
            if (Util.String2Int(txt_ProbationPeriod.Text) != -1)
            {
                return Util.String2Int(txt_ProbationPeriod.Text);
            }
            else
            {
                return 0;
            }

        }
        set
        {
            txt_ProbationPeriod.Text = Util.Int2String(value);
        }
    }
    public bool Consider_Payroll_Yes
    {
        get { return rdb_Consider_Payroll_Yes.Checked; }
        set { rdb_Consider_Payroll_Yes.Checked = Convert.ToBoolean(value); }
    }
    public bool Consider_Payroll_Not
    {
        set { rdb_Consider_Payroll_No.Checked = Convert.ToBoolean(value); }
    }
    //====================================================================================


    #endregion

    #region ControlsBind

    public DataTable BindDepartment
    {
        set
        {
            ddl_Department.DataSource = value;
            ddl_Department.DataTextField = "Department_Name";
            ddl_Department.DataValueField = "Department_Id";
            ddl_Department.DataBind();
            Raj.EC.Common.InsertItem(ddl_Department);
        }
    }


    public DataTable BindUserProfile
    {
        set
        {
            ddl_UserProfile.DataSource = value;
            ddl_UserProfile.DataTextField = "Profile_Name";
            ddl_UserProfile.DataValueField = "Profile_ID";
            ddl_UserProfile.DataBind();
            Raj.EC.Common.InsertItem(ddl_UserProfile);
        }
    }

    public DataTable BindDivision
    {
        set
        {
            ChkLst_Division.DataSource = value;
            ChkLst_Division.DataTextField = "Division_Name";
            ChkLst_Division.DataValueField = "Division_ID";
            ChkLst_Division.DataBind();

            DataSet ds_Applicable_Divisions_Details = new DataSet();

            ds_Applicable_Divisions_Details.Tables.Add(value.Copy());

            Session_Applicable_Divisions_Details = ds_Applicable_Divisions_Details;

            if (keyID <= 0) //Added: Ankit:10-01-09 :12.15 pm: Bydefault it will selected when new Emp add
            {
                int lst_cnt = 0;
                for (lst_cnt = 0; lst_cnt <= ChkLst_Division.Items.Count - 1; lst_cnt++)
                {
                    ChkLst_Division.Items[lst_cnt].Selected = true;
                }
            }
        }
    }


    public string Applicable_Divisions_Details_Xml
    {
        get { return Session_Applicable_Divisions_Details.GetXml().ToLower(); }
    }
    
    public DataSet Session_Applicable_Divisions_Details
    {
        get { return StateManager.GetState<DataSet>("Applicable_Divisions_Details"); }
        set { StateManager.SaveState("Applicable_Divisions_Details", value); }
    }

    #endregion
    
    #region Function
    
    public bool validateUI()
    {

        isValid = false;

        errorMessage = "";

        if (WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors) == false)
        {
        }
        else if (txt_EmployeeCode.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_EmployeeCode) == true)
        {
            errorMessage = "Please Enter Employee Code";// GetLocalResourceObject("Msg_txt_EmpCode").ToString();
            txt_EmployeeCode.Focus();
        }
        else if (txt_FirstName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter First Name"; // GetLocalResourceObject("Msg_txt_firstname").ToString();
            txt_FirstName.Focus();
        }
        else if (txt_MiddleName.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_MiddleName) == true)
        {
            errorMessage = "Please Enter Middle Name";// GetLocalResourceObject("Msg_txt_middlename").ToString();
            txt_MiddleName.Focus();
        }
        else if (txt_LastName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Last Name";// GetLocalResourceObject("Msg_txt_lastname").ToString();
            txt_LastName.Focus();
        }      
        else if (Util.String2Int(ddl_Department.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Department";//GetLocalResourceObject("Msg_ddl_department").ToString();
            ddl_Department.Focus();
        }
        else if (UserProfile_Id <= 0)   // ddl_UserProfile.Items.Count <= 0)
        {
            errorMessage = "Please Select User Profile";// GetLocalResourceObject("Msg_ddl_userprofile").ToString();
            ddl_UserProfile.Focus();
        }
        else if (txt_Qualification.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Qualification";// GetLocalResourceObject("Msg_txt_qualification").ToString();
            txt_Qualification.Focus();
        }
        else if (txt_Email.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Email) == true)  //added by Ankit champaneriya: 02-01-09
        {
            errorMessage = "Please Enter Email";// GetLocalResourceObject("Msg_txt_email").ToString();
            txt_Email.Focus();
        }
        else if (txt_PreviousJobProfile.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_PreviousJobProfile) == true)
        {
            errorMessage = "Please Enter Previous Job Details";// GetLocalResourceObject("Msg_txt_previousjobdetails").ToString();
            txt_PreviousJobProfile.Focus();
        }
        //================================================FOR PAYROLL====================================
        else if (rdb_Consider_Payroll_Yes.Checked == true)
        {
            if (Birth_Date > System.DateTime.Now)
            {
                errorMessage = "Birth Date Must Be Less Than Today's Date";
                TabStrip1.SelectedTab = TabStrip1.Tabs[1];
                birth_date.Focus();
                return false;
            }
            else if (DOJ < Birth_Date)
            {
                errorMessage = "Date Of Joining Must Be Greater Than Birth Date";
                TabStrip1.SelectedTab = TabStrip1.Tabs[1];
                date_Joinning.Focus();
                return false;
            }
            else if (Basic_Rate == -1 || Basic_Rate == 0)
            {
                errorMessage = "Please Enter The Basic Rate";
                TabStrip1.SelectedTab = TabStrip1.Tabs[1];
                txt_BasicRate.Focus();
                return false;
            }
            else if (Confirmation_Date < DOJ && Confirmation_Date > dt)
            {
                errorMessage = "Confirmation Date Must Be Greater Than Joining Date";
                TabStrip1.SelectedTab = TabStrip1.Tabs[1];
                Confirmation_date.Focus();
                return false;
            }
            else
            {
                isValid = true;
            }

        }
        //================================================FOR PAYROLL====================================
        else
        {
            isValid = true;
        }

        if (UserManager.getUserParam().IsDivisionReq && isValid == true)
        {
            Get_Applicable_Divisions_Details();
            if (Session_Applicable_Divisions_Details.Tables[0].Rows.Count <= 0)
            {
                if (ChkLst_Division.Visible == true)
                {
                    errorMessage = "Please Select Atleast ONE SBU.";
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
        }
        else
        {
            DataSet ds_ApplicableDivision = new DataSet();
            DataRow dr;

          

                ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

                dr = ds_ApplicableDivision.Tables[0].NewRow();
                dr["Division_Id"] = "1";
                dr["Division_Name"] = "Sundry";
                ds_ApplicableDivision.Tables[0].Rows.Add(dr);
            
            Session_Applicable_Divisions_Details = ds_ApplicableDivision;
            //isValid = true;
        }

        return isValid;

    }
    
    private void Get_Applicable_Divisions_Details()
    {
        DataSet ds_ApplicableDivision = new DataSet();
        DataRow dr;

        ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();
                
        int cnt = 0;
        for (cnt = 0; cnt <= ChkLst_Division.Items.Count - 1; cnt++)
        {
            if (ChkLst_Division.Items[cnt].Selected == true)
            {
                dr = ds_ApplicableDivision.Tables[0].NewRow();
                dr["Division_Id"] = ChkLst_Division.Items[cnt].Value;
                dr["Division_Name"] = ChkLst_Division.Items[cnt].Text;
                ds_ApplicableDivision.Tables[0].Rows.Add(dr);
            }
        }

        Session_Applicable_Divisions_Details = ds_ApplicableDivision;
    }

    public void SetApplicableDivisionsDetails()
    {
        Object sender = "";
        System.EventArgs e = null;

        DataSet objDS_EmployeeDivisionsDetails = new DataSet();
        _EmployeePresenter.Fill_Divisions(sender, e);

        objDS_EmployeeDivisionsDetails = _EmployeePresenter.ReadDivisionsValues();

        int lst_cnt = 0;
        int ds_cnt = 0;

        for (lst_cnt = 0; lst_cnt <= ChkLst_Division.Items.Count - 1; lst_cnt++)
        {
            ChkLst_Division.Items[lst_cnt].Selected = false;
        }

        if (objDS_EmployeeDivisionsDetails.Tables[0].Rows.Count > 0)
        {
            for (ds_cnt = 0; ds_cnt <= objDS_EmployeeDivisionsDetails.Tables[0].Rows.Count - 1; ds_cnt++)
            {

                for (lst_cnt = 0; lst_cnt <= ChkLst_Division.Items.Count - 1; lst_cnt++)
                {
                    if (Convert.ToInt32(ChkLst_Division.Items[lst_cnt].Value) == Convert.ToInt32(objDS_EmployeeDivisionsDetails.Tables[0].Rows[ds_cnt]["Division_ID"]))
                    {
                        ChkLst_Division.Items[lst_cnt].Selected = true;
                    }
                }
            }
        }
    }

    public void Show_Hide_Controls()
    {
        Object sender = "";
        if (keyID > 0)
        {
            if (chk_IseCargoUser.Checked == true)
            {
                //chk_IseCargoUser.Visible = false;
                //lbl_IseCargoUser.Visible = false;
                TR_ISeCargoUser.Visible = false;
            }
            else
            {
                //chk_IseCargoUser.Visible = true;
                //lbl_IseCargoUser.Visible = true;
                TR_ISeCargoUser.Visible = true;
            }
            //WucHierarchyWithID1.Set_Control_Enable = false; //added ANKIT
            TR_IsActive.Visible = true;  //added Ankit
        }
        else
        {
            WucHierarchyWithID1.Set_Control_Enable = true; //added ANkit
            TR_IsActive.Visible = false;  //added Ankit
        }
        if (UserManager.getUserParam().IsDivisionReq)
        {
            tr_Divisions.Visible = true;
            ChkLst_Division.Visible = true;
        }
        else
        {
            tr_Divisions.Visible = false;
            ChkLst_Division.Visible = false;

            //DataSet ds_ApplicableDivision = new DataSet();
            //DataRow dr;

            //    ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

            //    dr = ds_ApplicableDivision.Tables[0].NewRow();
            //    dr["Division_Id"] = "1";
            //    dr["Division_Name"] = "Sundry";
            //    ds_ApplicableDivision.Tables[0].Rows.Add(dr);

            //    Session_Applicable_Divisions_Details = ds_ApplicableDivision;
        }

    }

    public void Show_Payroll()
    {
        if (Consider_Payroll_Yes == false)
        {

            TabStrip1.Tabs[1].Visible = false;
        }
        else
        {
            TabStrip1.Tabs[1].Visible = true;
        }
    }
    #endregion

    #region event click
    protected void Page_Load(object sender, EventArgs e)
    {
        _EmployeePresenter = new EmployeePresenter(this, IsPostBack);

        upd_UserProfile.Update();

        WucHierarchyWithID1.Allow_All_Hierarchy = true;
        WucHierarchyWithID1.setDDLLocationAutoPostBack = true;

        WucHierarchyWithID1.DDLHierarchyChange += new EventHandler(_EmployeePresenter.Fill_Profile);

        WucHierarchyWithID1.DDLLocationChange += new EventHandler(_EmployeePresenter.Fill_Divisions);

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            ////Raj.EC.Common ObjCommon = new Raj.EC.Common();
            ////hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/General/App_LocalResources/wucEmployee.ascx.resx");
            Show_Payroll();
            if (keyID > 0)
            {
                SetApplicableDivisionsDetails();
               
            }

        }
       
        Show_Hide_Controls();
      
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Branch_Id = WucHierarchyWithID1.BranchID;
        Region_Id = WucHierarchyWithID1.RegionID;
        Area_Id = WucHierarchyWithID1.AreaID;
        Is_HO = WucHierarchyWithID1.Is_Ho;

        if (UserManager.getUserParam().IsDivisionReq)
        {
            Get_Applicable_Divisions_Details();
        }
        else
        {
            DataSet ds_ApplicableDivision = new DataSet();
            DataRow dr;

            ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

            dr = ds_ApplicableDivision.Tables[0].NewRow();
            dr["Division_Id"] = "1";
            dr["Division_Name"] = "Sundry";
            ds_ApplicableDivision.Tables[0].Rows.Add(dr);

            Session_Applicable_Divisions_Details = ds_ApplicableDivision;
        }
        _EmployeePresenter.save();
    }

    #endregion
    
}