using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;

using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;
using Raj.EC.ControlsView;
using Raj.EC;

using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

using System.Data.SqlClient;

/// <summary>
/// author pankaj
/// created on 25 apr
/// this user control is used in first tab of driver
/// </summary>
public partial class Master_Driver_WucDriverDetails : System.Web.UI.UserControl, IDriverDetailsView
{
    DriverDetailsPresenter objDriverDetailsPresenter;
    PageControls pc = new PageControls();

    #region Iview Implementation

    public string DriverName
    {
        set { Txt_Driver_Name.Text = value; }
        get { return Txt_Driver_Name.Text; }
    }
    public string DriverNickName
    {
        set { txt_NickName.Text = value; }
        get { return txt_NickName.Text; }
    }

    public string DriverMobile1
    {
        set { txt_MobileNo1.Text = value; }
        get { return txt_MobileNo1.Text; }
    }

    public string DriverMobile2
    {
        set { txt_MobileNo2.Text = value; }
        get { return txt_MobileNo2.Text; }
    }

    public string AadharNo
    {
        set { txt_AadharNo.Text = value; }
        get { return txt_AadharNo.Text; }
    }

    public string HistoryRemarks
    {
        set { txt_History.Text = value; }
        get { return txt_History.Text; }
    }


    public string ReferenceName2
    {
        set { txt_Refence_Name2.Text = value; }
        get { return txt_Refence_Name2.Text; }
    }
    public string ReferencePhone2
    {
        set { txt_Refence_Phone2.Text = value; }
        get { return txt_Refence_Phone2.Text; }
    }
    public string ReferenceMobile2
    {
        set { txt_Refence_Mobile2.Text = value; }
        get { return txt_Refence_Mobile2.Text; }
    }

    public DateTime ReferenceDate
    {
        set { wuc_ReferedDate.SelectedDate = Convert.ToDateTime(value); }
        get { return Convert.ToDateTime(wuc_ReferedDate.SelectedDate); }
    }

    public DateTime ReferenceDate2
    {
        set { wuc_ReferedDate2.SelectedDate = Convert.ToDateTime(value); }
        get { return Convert.ToDateTime(wuc_ReferedDate2.SelectedDate); }
    }


    public string DriverLicenseNo
    {
        set { txt_License_No.Text = value; }
        get { return txt_License_No.Text; }
    }
    public string DriverCode
    {
        set { txt_Driver_Code.Text = value; }
        get { return txt_Driver_Code.Text; }
    }
    public string ReferenceName
    {
        set { txt_Refence_Name.Text = value; }
        get { return txt_Refence_Name.Text; }
    }
    public string ReferencePhone
    {
        set { txt_Refence_Phone.Text = value; }
        get { return txt_Refence_Phone.Text; }
    }
    public string ReferenceMobile
    {
        set { txt_Refence_Mobile.Text = value; }
        get { return txt_Refence_Mobile.Text; }
    }
    public string DriverImage
    {
        set { hdn_DriverImageName.Value = value; }
        get { return hdn_DriverImageName.Value; }
    }
    public string NativeAddress1
    {
        set { txt_Native_Address_Line1.Text = value; }
        get { return txt_Native_Address_Line1.Text; }
    }
    public string NativeAddress2
    {
        set { txt_Native_Address_Line2.Text = value; }
        get { return txt_Native_Address_Line2.Text; }
    }
    public string NativeContactNo
    {
        set { txt_Contact_No.Text = value; }
        get { return txt_Contact_No.Text; }
    }
    public string Qualification
    {
        set { txt_Qualification.Text = value; }
        get { return txt_Qualification.Text; }
    }
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int LicenseIssueCityID
    {
        set { ddl_License_Issue_City.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_License_Issue_City.SelectedValue); }
    }
    public int LicenseIssueStateID
    {
        set { ddl_License_Issue_State.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_License_Issue_State.SelectedValue); }
    }

    public string  LicenseIssueStateCode
    {
        set { hdn_License_Issue_State_Code.Value  = value; }
        get { return hdn_License_Issue_State_Code.Value; }
    }

    public int LicenseCategoryID
    {
        set { ddl_License_Category.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_License_Category.SelectedValue); }
    }
    public int ReligionID
    {
        set { ddl_Religion.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Religion.SelectedValue); }
    }
    public int DriverCategoryID
    {
        set { ddl_Driver_Category.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Driver_Category.SelectedValue); }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }
    public bool IsReliable
    {
        set { chk_Is_reliable.Checked = value; }
        get { return chk_Is_reliable.Checked; }
    }
    public bool IsMarried
    {
        set { chk_Is_Married.Checked = value; }
        get { return chk_Is_Married.Checked; }
    }
    public bool IsCleaner
    {
        set { hdn_Is_Cleaner.Value = Convert.ToString(value); }
        get { return Util.String2Bool(hdn_Is_Cleaner.Value); }
    }
    public bool IsCompanyDriver
    {
        get { return true; }
    }
    public decimal OpeningBalance //modify :Ankit
    {
        set
        {
            if (value < 0)
            {
                ddl_Account_Effect_Type.SelectedValue = "Dr";
                txt_Opening_Balance.Text = Util.Decimal2String(value * -1);
            }
            if (value >= 0)
            {
                ddl_Account_Effect_Type.SelectedValue = "Cr";
                txt_Opening_Balance.Text = Util.Decimal2String(value);
            }
            //value = Math.Abs(value);
        }
        get
        {
            if (ddl_Account_Effect_Type.SelectedValue == "Dr")
                if (txt_Opening_Balance.Text == "")
                    return 0;
                else
                    return -(Util.String2Decimal(txt_Opening_Balance.Text));
            else
                return Util.String2Decimal(txt_Opening_Balance.Text);
        }
    }
    public DateTime BirthDate
    {
        set { picker_BirthDate.SelectedDate = Convert.ToDateTime(value); }
        get { return Convert.ToDateTime(picker_BirthDate.SelectedDate); }
    }
    public DateTime LicenseExpiryDate
    {
        set { picker_License_Expiry_Date.SelectedDate = Convert.ToDateTime(value); }
        get { return Convert.ToDateTime(picker_License_Expiry_Date.SelectedDate); }
    }
    public int Driver_Type_ID
    {
        set { Hdn_Driver_Type_ID.Value = Util.Int2String(value); }
        get { return Util.String2Int(Hdn_Driver_Type_ID.Value); }
    }
    public string BloodGroup
    {
        set { txt_BloodGroup.Text = value; }
        get { return txt_BloodGroup.Text; }
    }
    public bool IsLicenseAuthenticated
    {
        set { Chk_IsLicenseAuthencticated.Checked = value; }
        get { return Chk_IsLicenseAuthencticated.Checked; }
    }
    public string LicenseAuthenticatedBy
    {
        set { txt_LicenseAuthenticatedBy.Text = value; }
        get { return txt_LicenseAuthenticatedBy.Text; }
    }

    public bool IseCargoUser
    {
        set { chk_CreateeCargouser.Checked = value; }
        get { return chk_CreateeCargouser.Checked; }
    }

    public DataTable BindDDLDriverCategory
    {
        set
        {
            ddl_Driver_Category.DataSource = value;
            ddl_Driver_Category.DataValueField = "Driver_Category_ID";
            ddl_Driver_Category.DataTextField = "Driver_Category";
            ddl_Driver_Category.DataBind();
            ddl_Driver_Category.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindDDLReligion
    {
        set
        {
            ddl_Religion.DataSource = value;
            ddl_Religion.DataValueField = "Religion_ID";
            ddl_Religion.DataTextField = "Religion";
            ddl_Religion.DataBind();
            ddl_Religion.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindDDLLicenseIssueCity
    {
        set
        {
            ddl_License_Issue_City.DataSource = value;
            ddl_License_Issue_City.DataValueField = "City_ID";
            ddl_License_Issue_City.DataTextField = "City_Name";
            ddl_License_Issue_City.DataBind();
            ddl_License_Issue_City.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindDDLLicenseIssueState
    {
        set
        {
            ddl_License_Issue_State.DataSource = value;
            ddl_License_Issue_State.DataValueField = "State_ID";
            ddl_License_Issue_State.DataTextField = "State_Name";
            ddl_License_Issue_State.DataBind();
            ddl_License_Issue_State.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindDDLLicenseCategory
    {
        set
        {
            ddl_License_Category.DataSource = value;
            ddl_License_Category.DataValueField = "License_Category_ID";
            ddl_License_Category.DataTextField = "License_Category";
            ddl_License_Category.DataBind();
            ddl_License_Category.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindDDLLAccountEffectType
    {
        set
        {
            ddl_Account_Effect_Type.DataSource = value;
            ddl_Account_Effect_Type.DataValueField = "Account_Effect_Type";
            ddl_Account_Effect_Type.DataTextField = "Account_Effect_Type";
            ddl_Account_Effect_Type.DataBind();
        }
    }
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
    #endregion

    #region Validation
    public bool validateUI()
    {
        bool IsValid = false;

        //if (Driver_Type_ID > 0 && DriverCode == string.Empty && pc.Control_Is_Mandatory(txt_Driver_Code) == true)
        //{
        //    errorMessage = "Please Enter Driver Code";
        //    txt_Driver_Code.Focus();
        //}
        //else if (Driver_Type_ID == 0 && DriverCode == string.Empty && pc.Control_Is_Mandatory(txt_Driver_Code) == true)
        //{
        //    errorMessage = "Please Enter Cleaner Code";
        //    txt_Driver_Code.Focus();
        //}

        //else 

        if (Driver_Type_ID > 0 && DriverName == string.Empty)
        {
            errorMessage = "Please Enter Driver Name";
            Txt_Driver_Name.Focus();
        }
        else if (Driver_Type_ID == 0 && DriverName == string.Empty)
        {
            errorMessage = "Please Enter Cleaner Name";
            Txt_Driver_Name.Focus();
        }
        //else if (Driver_Type_ID == 1 && DriverCategoryID <= 0 && pc.Control_Is_Mandatory(ddl_Driver_Category) == true)
        //{
        //    errorMessage = "Please Select Driver Category";
        //    ddl_Driver_Category.Focus();
        //}
        //else if (Datemanager.IsValidProcessDate("DRI_BD", BirthDate) == false)
        else if (BirthDate > DateTime.Now)
        {
            errorMessage = "Invalid Birth Date";
            picker_BirthDate.Focus();
        }

        else if (( Driver_Type_ID == 1 || IsCleaner == true ) && (DateTime.Now.AddYears(-18) < BirthDate))
        {
            errorMessage = "Age Less Than 18 Years. Isko Nahi Lena Hai";
            picker_BirthDate.Focus();
        }

        else if ((Driver_Type_ID == 1 || IsCleaner == true) && (txt_AadharNo.Text.ToString().Length != 12))
        {
            errorMessage = "Please Enter Proper Aadhar No.";
            txt_AadharNo.Focus();
        }

        else if ((Driver_Type_ID == 1 || IsCleaner == true) && (txt_MobileNo1.Text.ToString().Length != 10))
        {
            errorMessage = "Please Enter Driver Mobile No.";
            txt_MobileNo1.Focus();
        }
        //else if (objDriverDetailsPresenter.Duplicate_Driver_Check() == "Driver Code" && Driver_Type_ID != 2 && pc.Control_Is_Mandatory(txt_Driver_Code) == true)
        //{
        //    if (Driver_Type_ID == 1 || Driver_Type_ID == 2)
        //        errorMessage = "Duplicate Driver Code";
        //    else if (Driver_Type_ID == 0)
        //        errorMessage = "Duplicate Cleaner Code";

        //    txt_Driver_Code.Focus();
        //}
        else if ((Driver_Type_ID == 1 || Driver_Type_ID == 2) && txt_License_No.Text != string.Empty && objDriverDetailsPresenter.Duplicate_Driver_Check() == "License" && pc.Control_Is_Mandatory(txt_License_No) == true)
        {
            errorMessage = "Duplicate License No";
            txt_License_No.Focus();
        }
        else if (Driver_Type_ID == 1 && ReligionID <= 0 && pc.Control_Is_Mandatory(ddl_Religion) == true)
        {
            errorMessage = "Please Select Driver Religion";
            ddl_Religion.Focus();
        }
        //else if (Driver_Type_ID == 1 && Qualification == string.Empty && pc.Control_Is_Mandatory(txt_Qualification) == true)
        //{
        //    errorMessage = "Please Enter Driver Qualification";
        //    txt_Qualification.Focus();
        //}
        else if ((Driver_Type_ID == 1 || Driver_Type_ID == 2) && DriverLicenseNo == string.Empty && pc.Control_Is_Mandatory(txt_License_No) == true)
        {
            errorMessage = "Please Enter Driver License Number";
            txt_License_No.Focus();
        }
        //else if (IsLicenseAuthenticated == true && LicenseAuthenticatedBy == string.Empty)
        //{
        //    errorMessage = "Please Enter License Authenticated By";
        //    txt_LicenseAuthenticatedBy.Focus();
        //}
        //else if (Driver_Type_ID == 1 && LicenseIssueCityID <= 0 && pc.Control_Is_Mandatory(ddl_License_Issue_City) == true)
        //{
        //    errorMessage = "Please Select License Issue City";
        //    ddl_License_Issue_City.Focus();
        //}

        else if (Driver_Type_ID == 1 && LicenseIssueStateID <= 0 && pc.Control_Is_Mandatory(ddl_License_Issue_State) == true)
        {
            errorMessage = "Please Select License Issue State";
            ddl_License_Issue_State.Focus();
        }

        else if (Driver_Type_ID == 1 && LicenseIssueStateID > 0 && LicenseIssueStateCode != txt_License_No.Text.Substring(0,2))
        {
            errorMessage = "License Issue State Code Is Not Match With License No.";
            txt_License_No.Focus();
        }
        else if (Driver_Type_ID == 1 && txt_License_No.Text.ToString().Trim().Length != 15 )
        {
            errorMessage = "Invalid License No. License No. Must Have Starting Two Characters And Then 13 Nos.";
            txt_License_No.Focus();
        }
        //else if ((Driver_Type_ID == 1 || Driver_Type_ID == 2) && LicenseCategoryID <= 0 && pc.Control_Is_Mandatory(ddl_License_Category) == true)
        //{
        //    errorMessage = "Please Select License Category";
        //    ddl_License_Category.Focus();
        //}

        else if (WucAddress1.ValidateWucAddress(lbl_Errors) == false) { }

        else if (Driver_Type_ID == 1 && ReferenceName == string.Empty && pc.Control_Is_Mandatory(txt_Refence_Name) == true)
        {
            errorMessage = "Please Enter Reference Name";
            txt_Refence_Name.Focus();
        }
        else if (Driver_Type_ID == 1 && ReferencePhone == string.Empty && pc.Control_Is_Mandatory(txt_Refence_Phone) == true)
        {
            errorMessage = "Please Enter Reference Phone Number";
            txt_Refence_Phone.Focus();
        }
        else if (ReferenceDate  > DateTime.Now)
        {
            errorMessage = "Invalid Reference Date";
            wuc_ReferedDate.Focus();
        }

        else
            IsValid = true;

        return IsValid;
    }
    #endregion

    #region private Procedures
    private void AddressControlFieldsVisibility()
    {
        WucAddress1.PhoneNoText1 = "Mobile No 2:";
        WucAddress1.MobileNoText = "Mobile No 1:";
        WucAddress1.VisibleStdCode = false;
        WucAddress1.VisiblePhoneNo2 = false;
        WucAddress1.VisibleFaxNo = false;
        WucAddress1.VisibleEmailId = false;

        WucAddress1.VisibleMobileNo = false;
        WucAddress1.VisiblePhoneNo1 = false;

    }
    private void TD_MandatoryVisiabilityFalse()
    {
        td_DriverNameMand.Visible = false;
        td_BloodGroupMand.Visible = false;
        td_MobileNo1Mand.Visible = false;
        TR_DriverCategory.Visible = false;
        Td_Mandatory_License_Category.Visible = false;
        TD_Mandatory_ReferenceName.Visible = false;
        Td_Mandatory_txt_Refence_Mobile.Visible = false;
        Td_Mandatory_License_Expiry_Date.Visible = false;
        TD_Mandatory_BirthDate.Visible = false;
        Td_Mandatory_Qualification.Visible = false;
        Td_Mandatory_Religion.Visible = false;
        TD_Mandatory_License_No.Visible = false;
        Td_Mandatory_LicenseIssueCity.Visible = false;
        lbl_Td_Mandatory_NativeAddressLine1.Visible = false;
        lbl_Td_Native_Address_Line2.Visible = false;
        lbl_Td_Mandatory_ContactNo.Visible = false;
        Td_Mandatory_ReferencePhone.Visible = false;
        Tr_Mandatory_Others.Visible = false;
        lbl_Mandatroy_OpeningBalance.Visible = false;

        Td_Mandatory_ReferencePhone2.Visible = false;
        TD_Mandatory_ReferenceName2.Visible = false;
        Td_Mandatory_txt_Refence_Mobile2.Visible = false;



    }
    private void TD_MandatoryVisiabilityTrue()
    {
        TR_DriverCategory.Visible = false ;
        Td_Mandatory_License_Category.Visible = true;
        Td_Mandatory_ReferencePhone.Visible = true;
        // Td_Mandatory_NativeAddressLine1.Visible = true;
        //  Td_Native_Address_Line2.Visible = true;
        // Td_Mandatory_ContactNo.Visible = true;
        TD_Mandatory_ReferenceName.Visible = true;
        Td_Mandatory_txt_Refence_Mobile.Visible = true;
        Td_Mandatory_License_Expiry_Date.Visible = true;
        TD_Mandatory_BirthDate.Visible = true;
        Td_Mandatory_Qualification.Visible = true;
        Td_Mandatory_Religion.Visible = false;
        TD_Mandatory_License_No.Visible = true;
        Td_Mandatory_LicenseIssueCity.Visible = true;
        Tr_Mandatory_Others.Visible = true;
        lbl_Mandatroy_OpeningBalance.Visible = false;

        Td_Mandatory_ReferencePhone2.Visible = true;
        TD_Mandatory_ReferenceName2.Visible = true;
        Td_Mandatory_txt_Refence_Mobile2.Visible = true;

    }
    private void TD_MandatoryVisiabilityMarketDriver()
    {
        TR_DriverCategory.Visible = false;
        lbl_IsMarried.Visible = false;
        chk_Is_Married.Visible = false;
        lbl_BirthDate_Mandatory.Visible = false;
        lbl_Religion_Mandatory.Visible = false;
        lbl_Qualification_Mandatory.Visible = false;
        TD_lbl_LicenseIssueCity.Visible = false;
        TD_ddl_LicenseIssueCity.Visible = false;
        Td_Mandatory_LicenseIssueCity.Visible = false;
        Panel4.Visible = false;
        Tr_Mandatory_Others.Visible = false;
        lbl_NikcNameMand.Visible = false;
    }

    private void SetDefaultOnPageLoad()
    {
        if (Driver_Type_ID == 0)
        {
            lbl_DriverCode.Text = "Cleaner Code :";
            lbl_DriverName.Text = "Cleaner Name:";
            lbl_DriverCategory.Text = "Cleaner Category :";
        }
        else if (Driver_Type_ID == 1 || Driver_Type_ID == 2)
        {
            lbl_DriverCode.Text = "Driver Code :";
            lbl_DriverName.Text = "Driver Name:";
            lbl_DriverCategory.Text = "Driver Category :";
        }

        if (Driver_Type_ID == 0)  // for cleaner
        {
            TD_MandatoryVisiabilityFalse();
            IsCleaner = true;
        }
        else if (Driver_Type_ID == 1) // FOR driver
        {
            TD_MandatoryVisiabilityTrue();
            IsCleaner = false;
        }
        else if (Driver_Type_ID == 2) //for Market Driver
        {
            TD_MandatoryVisiabilityMarketDriver();
            IsCleaner = false;
        }

    }

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        Driver_Type_ID = Util.String2Int(StateManager.GetState<String>("QueryString").ToString());

        if (!IsPostBack)
        {
            if (keyID <= 0)
                IsReliable = true;
        }
        if (Request.QueryString["Call_From"] == "LHPO")
        {
            Driver_Type_ID = 2; // for market Driver 280109 
            // bcos "QueryString" contain value of vehicle type  = 5 
            // while click on add driver link after vehicle serching.                        
        }

        SetDefaultOnPageLoad();
        objDriverDetailsPresenter = new DriverDetailsPresenter(this, IsPostBack);
        AddressControlFieldsVisibility();
    }


    protected void ddl_License_Issue_State_SelectedIndexChanged(object sender, EventArgs e)
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@State_ID", SqlDbType.Int, 0, ddl_License_Issue_State.SelectedValue) };
        objDAL.RunProc("dbo.EC_Master_Get_State_Code", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            LicenseIssueStateCode = objDR["State_Code"].ToString();

        }
        else
        {
            LicenseIssueStateCode = "";
        }

    }


    #endregion
}