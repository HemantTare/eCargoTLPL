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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

public partial class Operations_Renewals_FrmVehicleInsurancePremium : ClassLibraryMVP.UI.Page
{
    #region ClassVariables
    private DAL objDAL = new DAL();
    Common objCommon = new Common();
    DropDownList ddl_PremiumType;
    TextBox txt_Amount;
    private DataSet objDS;
    DataTable objDT;
    bool isValid = false;
    decimal ServiceTaxPercent;
    int Vehicle_Id;
    #endregion

    #region ControlsValues

    public string Insurance_No
    {
        set { lbl_Insurance_No.Text = value; }
        get { return lbl_Insurance_No.Text; }
    }

    public DateTime InsuranceDate
    {
        set { Wuc_Insurance_Date.SelectedDate = value; }
        get { return Wuc_Insurance_Date.SelectedDate; }
    }

    public string ChequeNo
    {
        set { txt_Cheque_No.Text = value; }
        get { return txt_Cheque_No.Text; }
    }

    public DateTime ChequeDate
    {
        set { Wuc_Cheque_Date.SelectedDate = value; }
        get
        {
            if (rdl_Paid_By.SelectedValue == "0")
            {
                return DateTime.Today.Date;
            }
            else
                return Wuc_Cheque_Date.SelectedDate;
        }
    }
    public bool Is_Cheque
    {
        set
        {
            Boolean IsChk;
            IsChk = value;
            if (IsChk == false)
                rdl_Paid_By.SelectedValue = "0";
            else
                rdl_Paid_By.SelectedValue = "1";
        }
        get { return rdl_Paid_By.SelectedValue == "1" ? true : false; }
    }

    public int Bank_ID
    {
        set { ddl_Bank_Name.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Bank_Name.SelectedValue); }
    }

    public int VehicleTypeID
    {
        get { return Util.String2Int(hdn_VehicleTypeID.Value); }
    }

    public int VehicleID
    {
        set { WucVehicleSearch1.VehicleID = value; }
        get { return WucVehicleSearch1.VehicleID; }
    }
    public int VehicleInsuranceID
    {
        set { hdn_Vehicle_InsuranceID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Vehicle_InsuranceID.Value); }
    }
    public int VehicleNo
    {
        set
        {
            TextBox txt_Vehicle = (TextBox)WucVehicleSearch1.FindControl("txt_Vehicle_Last_4_Digits");
            txt_Vehicle.Text = value.ToString();
        }
    }

    public int InsuranceCompanyID
    {
        set { ddl_InsuranceCompany.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_InsuranceCompany.SelectedValue); }
    }
    public int IssuingBranchID
    {
        set { ddl_IssuingBranch.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_IssuingBranch.SelectedValue); }
    }
    public string PolicyNo
    {
        set { txt_PolicyNo.Text = value; }
        get { return txt_PolicyNo.Text; }
    }
    public int AgentID
    {
        set { ddl_Agent.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Agent.SelectedValue); }
    }

    public DateTime CommenceDate
    {
        set { Wuc_CommenceDate.SelectedDate = value; }
        get { return Wuc_CommenceDate.SelectedDate; }
    }
    public DateTime ExpiryDate
    {
        set { Wuc_ExpiryDate.SelectedDate = value; }
        get { return Wuc_ExpiryDate.SelectedDate; }
    }

    public decimal IDV
    {
        set { txt_IDV.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_IDV.Text); }
    }
    public string EngineNo
    {
        set { hdn_EngineNo.Value = value; }
        get { return hdn_EngineNo.Value; }
    }
    public string ChasisNo
    {
        set { hdn_ChasisNo.Value = value; }
        get { return hdn_ChasisNo.Value; }
    }

    public decimal FirstPartyPremium
    {
        set { txt_FPPremium.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_FPPremium.Text) == -1 ? 0 : Util.String2Decimal(txt_FPPremium.Text); }
    }
    public decimal ThirdPartyPremium
    {
        set { txt_TPPremium.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_TPPremium.Text); }
    }
    public decimal LoadingPercentTPP
    {
        set { txt_LoadingTPP.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_LoadingTPP.Text) == -1 ? 0 : Util.String2Decimal(txt_LoadingTPP.Text); }
    }
    public decimal LoadingAmountTPP
    {
        set
        {
            hdn_LoadingTPP.Value = Util.Decimal2String(value);
            lbl_TPPAmount.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_LoadingTPP.Value) == -1 ? 0 : Util.String2Decimal(hdn_LoadingTPP.Value); }
    }
    public decimal LoadingPercentFPP
    {
        set { txt_LoadingFPP.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_LoadingFPP.Text) == -1 ? 0 : Util.String2Decimal(txt_LoadingFPP.Text); }
    }
    public decimal LoadingAmountFPP
    {
        set
        {
            hdn_LoadingFPP.Value = Util.Decimal2String(value);
            lbl_FPPAmount.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_LoadingFPP.Value) == -1 ? 0 : Util.String2Decimal(hdn_LoadingFPP.Value); }
    }
    public decimal NCBPercentFPP
    {
        set { txt_NCBFPP.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_NCBFPP.Text) == -1 ? 0 : Util.String2Decimal(txt_NCBFPP.Text); }
    }
    public decimal NCBAmount
    {
        set
        {
            hdn_NCBFPP.Value = Util.Decimal2String(value);
            lbl_NCBAmount.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_NCBFPP.Value) == -1 ? 0 : Util.String2Decimal(hdn_NCBFPP.Value); }
    }
    public decimal NetPremium
    {
        set
        {
            hdn_NetPremium.Value = Util.Decimal2String(value);
            lbl_NetPremium.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_NetPremium.Value); }
    }
    public decimal ServiceTaxPercentage
    {
        set { TxtServiceTaxRate.Text = value.ToString(); }
        get { return Util.String2Decimal(TxtServiceTaxRate.Text); }
    }
    public decimal ServiceTaxAmount
    {
        set
        {
            hdn_ServiceTax.Value = Util.Decimal2String(value);
            lbl_ServiceTax.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ServiceTax.Value); }
    }
    public decimal NetPayable
    {
        set
        {
            hdn_NetPayable.Value = Util.Decimal2String(value);
            lbl_NetPayable.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_NetPayable.Value); }
    }

    public String InsurancePremiumDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionPremiumDetailsGrid.Copy());

            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        TextBox txt_Vehicle = (TextBox)WucVehicleSearch1.FindControl("txt_Vehicle_Last_4_Digits");

        bool _isValid = false;
        bool Is_ValidDate = true;
        DateTime Expiry_Date = DateTime.Now;

        if (Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty)
        {
            SqlParameter[] objSqlParam = {
                objDAL.MakeInParams("@Vehicle_Insurance_Id", SqlDbType.Int, 0, keyID),
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
                objDAL.MakeInParams("@CommenceDate", SqlDbType.DateTime, 0, CommenceDate),
                objDAL.MakeInParams("@ExpiryDate", SqlDbType.DateTime,0 ,ExpiryDate)};

            objDAL.RunProc("rstil42.EF_Trn_VehicleInsurance_GetCommenceExpiryDate", objSqlParam, ref objDS);
            objDT = objDS.Tables[0];

            Is_ValidDate = Util.String2Bool(objDT.Rows[0]["Is_Valid_Date"].ToString());
            Expiry_Date = Convert.ToDateTime(objDT.Rows[0]["To_Date"].ToString());
        }
        if (Datemanager.IsValidProcessDate("VIP_ID", InsuranceDate) == false)
        {
            errorMessage = "Please Select insurance Premium Date";
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_Vehicle.Text == string.Empty && Vehicle_Id < 0)
        {
            errorMessage = "Please Enter Vehicle No";
            scm_VehicleInsurance.SetFocus(txt_Vehicle);
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && VehicleID == -1 && Vehicle_Id < 0)
        {
            errorMessage = "Please add vehicle type";
            scm_VehicleInsurance.SetFocus(txt_Vehicle);
        }
        else if (InsuranceCompanyID <= 0)
        {
            errorMessage = "Please Select Insurance Company";
            scm_VehicleInsurance.SetFocus(ddl_InsuranceCompany);
        }
        else if (IssuingBranchID <= 0)
        {
            errorMessage = "Please Select Issuing Branch";
            scm_VehicleInsurance.SetFocus(ddl_IssuingBranch);
        }
        else if (PolicyNo == string.Empty)
        {
            errorMessage = "Please Enter Policy No";
            scm_VehicleInsurance.SetFocus(txt_PolicyNo);
        }
        else if (AgentID <= 0)
        {
            errorMessage = "Please Select Agent";
            scm_VehicleInsurance.SetFocus(ddl_Agent);
        }
        else if (CommenceDate > ExpiryDate)
        {
            errorMessage = "Expiry Date can not be less than Commence Date";
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_EngineNo.Text == string.Empty)
        {
            errorMessage = "Please Enter Engine No";
            scm_VehicleInsurance.SetFocus(txt_EngineNo);
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_EngineNo.Text != EngineNo)
        {
            errorMessage = "Please Enter Correct Engine No";
            scm_VehicleInsurance.SetFocus(txt_EngineNo);
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_ChasisNo.Text == string.Empty)
        {
            errorMessage = "Please Enter Correct Chasis No";
            scm_VehicleInsurance.SetFocus(txt_ChasisNo);
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_ChasisNo.Text != ChasisNo)
        {
            errorMessage = "Please Enter Correct Chasis No";
            scm_VehicleInsurance.SetFocus(txt_ChasisNo);
        }
        else if (IDV <= 0)
        {
            errorMessage = "Please Enter IDV";
            scm_VehicleInsurance.SetFocus(txt_IDV);
        }
        else if (ThirdPartyPremium <= 0)
        {
            errorMessage = "Please Enter Third Party Premium";
            scm_VehicleInsurance.SetFocus(txt_TPPremium);
        }
        else if (rdl_Paid_By.SelectedValue == "1" && ChequeNo == string.Empty)
        {
            errorMessage = "Please Enter Cheque No";
            scm_VehicleInsurance.SetFocus(txt_Cheque_No);
        }
        else if (rdl_Paid_By.SelectedValue == "1" && txt_Cheque_No.Text.Length < 6)
        {
            errorMessage = "Please Enter Valid Cheque No";
            scm_VehicleInsurance.SetFocus(txt_Cheque_No);
        }
        else if (rdl_Paid_By.SelectedValue == "1" && Bank_ID == 0)
        {
            errorMessage = "Please Enter Bank Name";
            scm_VehicleInsurance.SetFocus(ddl_Bank_Name);
        }
        else if (CheckChequeDate() == false)
        {
            _isValid = false;
            return _isValid;
        }
        else if (rdl_Paid_By.SelectedValue == "1" && ChequeDate < InsuranceDate)
        {
            errorMessage = "Cheque Date can not be less than Insurance Date";
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && Is_ValidDate == false)
        {
            errorMessage = "Commence Date should not be less than "+ Expiry_Date.ToShortDateString();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion
    #region ControlsBind

    public DataTable BindBankName
    {
        set
        {
            ddl_Bank_Name.DataTextField = "Bank_Name";
            ddl_Bank_Name.DataValueField = "Bank_ID";
            ddl_Bank_Name.DataSource = value;
            ddl_Bank_Name.DataBind();
            ddl_Bank_Name.Items.Insert(0, new ListItem("--Select Bank --", "0"));
        }
    }

    public DataTable BindInsuranceCompany
    {
        set
        {
            ddl_InsuranceCompany.DataTextField = "Insurance_Company";
            ddl_InsuranceCompany.DataValueField = "Insurance_Company_ID";
            ddl_InsuranceCompany.DataSource = value;
            ddl_InsuranceCompany.DataBind();
            ddl_InsuranceCompany.Items.Insert(0, new ListItem("--Select Insurance Company --", "0"));
        }
    }
    public DataTable BindIssuingBranch
    {
        set
        {
            ddl_IssuingBranch.DataTextField = "Branch_Name";
            ddl_IssuingBranch.DataValueField = "Branch_Id";
            ddl_IssuingBranch.DataSource = value;
            ddl_IssuingBranch.DataBind();
            ddl_IssuingBranch.Items.Insert(0, new ListItem("--Select Branch --", "0"));
        }
    }
    public DataTable BindAgent
    {
        set
        {
            ddl_Agent.DataTextField = "Vendor_Name";
            ddl_Agent.DataValueField = "Vendor_ID";
            ddl_Agent.DataSource = value;
            ddl_Agent.DataBind();
            ddl_Agent.Items.Insert(0, new ListItem("--Select Agent --", "0"));
        }
    }
    public DataTable BindPremiumType
    {
        set
        {
            ddl_PremiumType.DataTextField = "Premium_Type";
            ddl_PremiumType.DataValueField = "Premium_Type_ID";
            ddl_PremiumType.DataSource = value;
            ddl_PremiumType.DataBind();
            ddl_PremiumType.Items.Insert(0, new ListItem("--Select Premium Type --", "0"));
        }
    }

    public DataTable SessionPremiumTypeDropdown
    {
        get { return StateManager.GetState<DataTable>("PremiumType"); }
        set { StateManager.SaveState("PremiumType", value); }
    }

    public DataTable BindPremiumDetailsGrid
    {
        set
        {
            SessionPremiumDetailsGrid = value;
            Set_Sr_No();
            Calculate_PremiumAmount();
            Calculate_PremiumDetails();
            dg_PremiumDetails.DataSource = value;
            dg_PremiumDetails.DataBind();
        }
    }

    public DataTable SessionPremiumDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("PremiumDetails"); }
        set { StateManager.SaveState("PremiumDetails", value); }
    }

    #endregion

    #region Others Function

    public ScriptManager SetScriptManager
    {
        set { scm_VehicleInsurance = value; }
    }

    private void Set_Sr_No()
    {
        int Sr_No;
        objDT = SessionPremiumDetailsGrid;
        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= objDT.Rows.Count - 1; Sr_No++)
        {
            DR = objDT.Rows[Sr_No];
            DR["Sr_No"] = Sr_No + 1;
        }
        SessionPremiumDetailsGrid = objDT;
    }
    private void Calculate_PremiumAmount()
    {
        int Sr_No = 0;
        objDT = SessionPremiumDetailsGrid;
        //if (Sr_No != objDT.Rows.Count-1)
        hdn_TotalPremiumAmount.Value = "0";

        for (; Sr_No <= objDT.Rows.Count - 1; Sr_No++)
        {
            hdn_TotalPremiumAmount.Value = Util.Decimal2String(Util.String2Decimal(hdn_TotalPremiumAmount.Value.ToString()) + Util.String2Decimal(objDT.Rows[Sr_No]["Amount"].ToString()));
        }
    }
    private void Calculate_TPP_FPP()
    {
        LoadingAmountTPP = (ThirdPartyPremium * LoadingPercentTPP) / 100;
        LoadingAmountFPP = (FirstPartyPremium * LoadingPercentFPP) / 100;
        NCBAmount = (FirstPartyPremium * NCBPercentFPP) / 100;

        LoadingAmountTPP = Math.Round(LoadingAmountTPP, 2);
        LoadingAmountFPP = Math.Round(LoadingAmountFPP, 2);
        NCBAmount = Math.Round(NCBAmount, 2);

        Calculate_PremiumDetails();
    }

    private void Calculate_PremiumDetails()
    {
        decimal TotalPremiumAmount = Util.String2Decimal(hdn_TotalPremiumAmount.Value);
        if (FirstPartyPremium == -1) FirstPartyPremium = 0;
        if (ThirdPartyPremium == -1) ThirdPartyPremium = 0;
        if (NCBAmount == -1) NCBAmount = 0;
        if (LoadingAmountTPP == -1) LoadingAmountTPP = 0;
        if (LoadingAmountFPP == -1) LoadingAmountFPP = 0;

        NetPremium = (FirstPartyPremium - NCBAmount) + ThirdPartyPremium + LoadingAmountTPP + LoadingAmountFPP + TotalPremiumAmount;
        ServiceTaxAmount = (NetPremium * ServiceTaxPercentage) / 100;
        NetPayable = NetPremium + ServiceTaxAmount;

        if (NetPremium < 0) NetPremium = 0;
        if (ServiceTaxAmount < 0) ServiceTaxAmount = 0;
        if (NetPayable < 0) NetPayable = 0;

        NetPremium = Math.Round(NetPremium, 2);
        ServiceTaxAmount = Math.Round(ServiceTaxAmount, 2);
        NetPayable = Math.Round(NetPayable, 2);
    }
    private bool CheckChequeDate()
    {
        int DayDifference = 0;
        int YearDifference = 0;
        int PreviousYearDays = 0;
        int NextYearDays = 0;
        int TotalDaysofPrevYear = 0;
        int TotalDaysofNextYear = 0;
        int TotalDays = 0;
        string isLess = "1";
        if (rdl_Paid_By.SelectedValue == "1")
        {
            YearDifference = ChequeDate.Year - CommenceDate.Year;
            if (YearDifference > 0)
            {
                TotalDaysofPrevYear = 365;
                if (DateTime.IsLeapYear(CommenceDate.Year))
                {
                    TotalDaysofPrevYear = 366;
                }
                PreviousYearDays = TotalDaysofPrevYear - CommenceDate.DayOfYear;
                if (YearDifference > 1)
                {
                    TotalDaysofNextYear = 365;
                    if (DateTime.IsLeapYear(ChequeDate.Year))
                    {
                        TotalDaysofNextYear = 366;
                    }
                    YearDifference = YearDifference - 1;
                    NextYearDays = TotalDaysofNextYear * YearDifference;
                }
                NextYearDays = NextYearDays + ChequeDate.DayOfYear;
                TotalDays = NextYearDays + PreviousYearDays;
                DayDifference = TotalDays;
                isLess = "0";
            }
            else
            {
                if (ChequeDate > CommenceDate)
                {
                    DayDifference = ChequeDate.DayOfYear - CommenceDate.DayOfYear;
                    isLess = "0";
                }
                else
                {
                    DayDifference = CommenceDate.DayOfYear - ChequeDate.DayOfYear;
                    isLess = "1";
                }
            }
            if (DayDifference > 90)
            {
                if (isLess == "1")
                {
                    errorMessage = "Cheque Date Must be Within three Months before the Insurance Commencement Date";
                    return false;
                }
                else if (isLess == "0")
                {
                    errorMessage = "Cheque Date Must be Within three Months after the Insurance Commencement Date";
                    return false;
                }
            }
        }
        return true;
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdn_VehicleTypeID.Value = StateManager.GetState<string>("QueryString");
            Vehicle_Id = Util.DecryptToInt(Request.QueryString["VehicleId"]);
            hdn_VehicleId.Value = Vehicle_Id.ToString();
        }
        if (!IsPostBack)
        {
            InsuranceDate = DateTime.Now;
            ChequeDate = DateTime.Now;
            CommenceDate = DateTime.Now;
            ExpiryDate = DateTime.Now;

            fillValues();
            initValues();
        }

        if (Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == "")
        {
            WucVehicleSearch1.SetAutoPostBack = true;
            WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);
            if (IsPostBack == false && keyID > 0)
                VehicleIndexChange(this, e);
        }

        if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == "") && Vehicle_Id > 0)
        {
            WucVehicleSearch1.VehicleID = Vehicle_Id;
            WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);
            VehicleIndexChange(this, e);
            WucVehicleSearch1.SetEnabled = false;
        }

        if (!IsPostBack)
        {
            hdf_ResourecString.Value = objCommon.GetResourceString("Transactions/Renewals/App_LocalResources/WucVehicleInsurancePremium.ascx.resx");
            if (keyID < 0)
            {
                lbl_Insurance_No.Text = objCommon.Get_Next_Number();
            }
            if (Util.String2Int(hdn_VehicleTypeID.Value) > 0)
            {
                tr_Insurance_no.Visible = false;
                tr_Heading.Visible = false;
                tbl_save.Visible = false;
                tr_labelerror.Visible = false;
                tr_vehicle_no.Visible = false;
                tr_EngineChasis_no.Visible = false;
            }
        }
    }

    private void VehicleIndexChange(object sender, EventArgs e)
    {
        ChasisNo = WucVehicleSearch1.GetVehicleParameter("Chasis_No");
        EngineNo = WucVehicleSearch1.GetVehicleParameter("Engine_No");
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Calculate_TPP_FPP();
        if (validateUI())
        {
            Save_Details();
        } 
    }
    private void Save_Details()
    {
        SqlParameter[] objSqlParam = { 
               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
               objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),                                      
               objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
               objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode), 
               objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId ),
               objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,UserManager.getUserParam().HierarchyCode),
               objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int , 0,keyID), 
               objDAL.MakeInParams("@Vehicle_Insurance_Date", SqlDbType.DateTime , 0,InsuranceDate), 
               objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int , 0,VehicleID), 
               objDAL.MakeInParams("@Insurance_Company_ID", SqlDbType.Int , 0,InsuranceCompanyID), 
               objDAL.MakeInParams("@Insurance_Company_Branch_ID", SqlDbType.Int, 0, IssuingBranchID),
               objDAL.MakeInParams("@Policy_No",SqlDbType.NVarChar,25,PolicyNo),
               objDAL.MakeInParams("@Agent_ID", SqlDbType.Int, 0,AgentID), 
               objDAL.MakeInParams("@Commence_Date", SqlDbType.DateTime,0, CommenceDate),
               objDAL.MakeInParams("@Expiry_Date",SqlDbType.DateTime,0,ExpiryDate),
               objDAL.MakeInParams("@IDV", SqlDbType.Decimal, 0,IDV), 
               objDAL.MakeInParams("@First_Party_Premium", SqlDbType.Decimal, 0, FirstPartyPremium),
               objDAL.MakeInParams("@Third_Party_Premium", SqlDbType.Decimal, 0,ThirdPartyPremium), 
               objDAL.MakeInParams("@Loading_Percent_TPP", SqlDbType.Decimal,0, LoadingPercentTPP),
               objDAL.MakeInParams("@Loading_Amount_TPP", SqlDbType.Decimal,0, LoadingAmountTPP),
               objDAL.MakeInParams("@Loading_Percent_FPP",SqlDbType.Decimal,0,LoadingPercentFPP),
               objDAL.MakeInParams("@Loading_Amount_FPP",SqlDbType.Decimal,0,LoadingAmountFPP),
               objDAL.MakeInParams("@NCB_Percent_FPP", SqlDbType.Decimal , 0,NCBPercentFPP), 
               objDAL.MakeInParams("@NCB_Amount", SqlDbType.Decimal,0, NCBAmount),
               objDAL.MakeInParams("@Net_Premium",SqlDbType.Decimal , 0,NetPremium),
               objDAL.MakeInParams("@Service_Tax_Percentage",SqlDbType.Decimal,0,ServiceTaxPercentage),
               objDAL.MakeInParams("@Service_Tax_Amount", SqlDbType.Decimal, 0,ServiceTaxAmount), 
               objDAL.MakeInParams("@Net_Payable", SqlDbType.Decimal,0, NetPayable),
               objDAL.MakeInParams("@Is_Cheque", SqlDbType.Bit,0, Is_Cheque),
               objDAL.MakeInParams("@Cheque_No", SqlDbType.NVarChar,50, ChequeNo),
               objDAL.MakeInParams("@Cheque_Date", SqlDbType.DateTime,0, ChequeDate),
               objDAL.MakeInParams("@Bank_ID", SqlDbType.Int,0, Bank_ID),
               objDAL.MakeInParams("@Document_Status_ID", SqlDbType.Int,0, 100),
               objDAL.MakeInParams("@Insurance_Premium_Details", SqlDbType.Xml,0, InsurancePremiumDetailsXML),
               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, UserManager.getUserParam().UserId)
            //,
            //   objDAL.MakeInParams("@Is_From_Transaction", SqlDbType.Bit, 0, true)
            };

        objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }
    }

    protected void ddl_InsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranchOnInsuCompanyChange();
    }
    public void fillBranchOnInsuCompanyChange()
    {
        SqlParameter[] objSqlParam = { 
            objDAL.MakeInParams("@Insurance_Company_Id", SqlDbType.Int, 0, InsuranceCompanyID),
            objDAL.MakeInParams("@Menu_ItemID", SqlDbType.Int, 0, Common.GetMenuItemId()) ,
            objDAL.MakeInParams("@Key_ID", SqlDbType.Int, 0, keyID)};
        objDAL.RunProc("rstil41.EF_Mst_Get_Branch_On_Insurance_Company_Changed", objSqlParam, ref objDS);

        BindIssuingBranch = objDS.Tables[0];
    }

    private void fillValues()
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, keyID),
                    objDAL.MakeInParams("@Menu_ItemID", SqlDbType.Int, 0, Common.GetMenuItemId())};
        objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_FillValues", objSqlParam, ref objDS);

        BindInsuranceCompany = objDS.Tables[0];
        BindAgent = objDS.Tables[1];
        SessionPremiumTypeDropdown = objDS.Tables[2];
        BindBankName = objDS.Tables[3];

        fillBranchOnInsuCompanyChange();
    }

    private void initValues()
    {
        SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int, 0, keyID), 
                objDAL.MakeInParams("@IsFromVehicle", SqlDbType.Int, 0, VehicleTypeID) };
        objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_ReadValues", objSqlParam, ref objDS);

        BindPremiumDetailsGrid = objDS.Tables[1];

        if (keyID > 0)
        {
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                Insurance_No = objDR["Vehicle_Insurance_No"].ToString();
                InsuranceDate = Convert.ToDateTime(objDR["Vehicle_Insurance_Date"].ToString());

                VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                VehicleNo = Util.String2Int(objDR["Number_Part4"].ToString());
                VehicleInsuranceID = Util.String2Int(objDR["Vehicle_Insurance_Id"].ToString());
                InsuranceCompanyID = Util.String2Int(objDR["Insurance_Company_ID"].ToString());
                fillBranchOnInsuCompanyChange();
                IssuingBranchID = Util.String2Int(objDR["Insurance_Company_Branch_ID"].ToString());
                PolicyNo = objDR["Policy_No"].ToString();
                AgentID = Util.String2Int(objDR["Agent_ID"].ToString());
                CommenceDate = Convert.ToDateTime(objDR["Commence_Date"].ToString());
                ExpiryDate = Convert.ToDateTime(objDR["Expiry_Date"].ToString());
                IDV = Util.String2Decimal(objDR["IDV"].ToString());
                EngineNo = objDR["Engine_No"].ToString();
                ChasisNo = objDR["Chasis_No"].ToString();
                FirstPartyPremium = Util.String2Decimal(objDR["First_Party_Premium"].ToString());
                ThirdPartyPremium = Util.String2Decimal(objDR["Third_Party_Premium"].ToString());
                LoadingPercentTPP = Util.String2Decimal(objDR["Loading_Percent_TPP"].ToString());
                LoadingAmountTPP = Util.String2Decimal(objDR["Loading_Amount_TPP"].ToString());
                LoadingPercentFPP = Util.String2Decimal(objDR["Loading_Percent_FPP"].ToString());
                LoadingAmountFPP = Util.String2Decimal(objDR["Loading_Amount_FPP"].ToString());
                NCBPercentFPP = Util.String2Decimal(objDR["NCB_Percent_FPP"].ToString());
                NCBAmount = Util.String2Decimal(objDR["NCB_Amount"].ToString());
                NetPremium = Util.String2Decimal(objDR["Net_Premium"].ToString());
                ServiceTaxPercentage = Util.String2Decimal(objDR["Service_Tax_Percentage"].ToString());
                ServiceTaxAmount = Util.String2Decimal(objDR["Service_Tax_Amount"].ToString());
                NetPayable = Util.String2Decimal(objDR["Net_Payable"].ToString());

                Is_Cheque = Util.String2Bool(objDR["Is_Cheque"].ToString());
                ChequeNo = objDR["Cheque_No"].ToString();
                ChequeDate = Convert.ToDateTime(objDR["Cheque_Date"].ToString());
                Bank_ID = Util.String2Int(objDR["Bank_ID"].ToString());
            }
        }
    }

    protected void dg_PremiumDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_PremiumDetails.EditItemIndex = -1;
        dg_PremiumDetails.ShowFooter = true;
        BindPremiumDetailsGrid = SessionPremiumDetailsGrid;
    }

    protected void dg_PremiumDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionPremiumDetailsGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionPremiumDetailsGrid = objDT;
            dg_PremiumDetails.EditItemIndex = -1;
            dg_PremiumDetails.ShowFooter = true;
            BindPremiumDetailsGrid = SessionPremiumDetailsGrid;
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDT = SessionPremiumDetailsGrid;
        DataRow DR = null;
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        ddl_PremiumType = (DropDownList)(e.Item.FindControl("ddl_PremiumType"));

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Amount"] = txt_Amount.Text;
            DR["Insurance_Premium_Type_ID"] = ddl_PremiumType.SelectedValue;
            DR["PremiumType"] = ddl_PremiumType.SelectedItem.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionPremiumDetailsGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (Convert.ToInt32(ddl_PremiumType.SelectedValue) == 0)
        {
            errorMessage = "Please Select Premium Type";
            scm_VehicleInsurance.SetFocus(ddl_PremiumType);
        }
        else if (txt_Amount.Text == string.Empty)
        {
            errorMessage = "Please Enter Amount";
            scm_VehicleInsurance.SetFocus(txt_Amount);
        }
        else
            isValid = true;

        return isValid;
    }

    protected void dg_PremiumDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            objDT = SessionPremiumDetailsGrid;
            try
            {
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["Insurance_Premium_Type_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindPremiumDetailsGrid = SessionPremiumDetailsGrid;
                    dg_PremiumDetails.EditItemIndex = -1;
                    dg_PremiumDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                lbl_Errors.Text = "Duplicate Premium added";
                scm_VehicleInsurance.SetFocus(ddl_PremiumType);
                return;
            }
        }
    }

    protected void dg_PremiumDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionPremiumDetailsGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["Insurance_Premium_Type_ID"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_PremiumDetails.EditItemIndex = -1;
                dg_PremiumDetails.ShowFooter = true;
                BindPremiumDetailsGrid = SessionPremiumDetailsGrid;
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            lbl_Errors.Text = "Duplicate Premium added";
            scm_VehicleInsurance.SetFocus(ddl_PremiumType);
            return;
        }
    }

    protected void dg_PremiumDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                ddl_PremiumType = (DropDownList)(e.Item.FindControl("ddl_PremiumType"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindPremiumType = SessionPremiumTypeDropdown;
                Calculate_PremiumDetails();
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionPremiumDetailsGrid;
                DataRow DR = objDT.Rows[e.Item.ItemIndex];

                txt_Amount.Text = DR["Amount"].ToString();
                ddl_PremiumType.SelectedValue = DR["Insurance_Premium_Type_ID"].ToString();
            }
        }
    }

    protected void dg_PremiumDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        lbtn_Delete.Enabled = false;
        dg_PremiumDetails.EditItemIndex = e.Item.ItemIndex;
        dg_PremiumDetails.ShowFooter = false;
        BindPremiumDetailsGrid = SessionPremiumDetailsGrid;
    }

   
    private void CalculateServiceTax()
    {
        decimal stRate = Util.String2Decimal(TxtServiceTaxRate.Text);
        decimal premiumAmt = Util.String2Decimal(lbl_NetPremium.Text);

        decimal stAmt = Math.Round((stRate / 100 * premiumAmt), 2);

        lbl_ServiceTax.Text = stAmt.ToString();
    }
}
