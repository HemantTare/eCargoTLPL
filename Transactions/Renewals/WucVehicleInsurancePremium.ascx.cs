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

using Raj.EF.TransactionsPresenter;
using Raj.EF.TransactionsView;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 06/05/2008
/// Description   : This Page is For Master Vehicle Insurance Premium
/// 
/// Author        : Pankaj
/// Created On    : 13/05/2008
/// Description   : Added New Control for Vehicle Search
/// </summary>
/// 

public partial class Transactions_Renewals_WucVehicleInsurancePremium : System.Web.UI.UserControl, IVehicleInsurancePremiumView 
{
    #region ClassVariables
    Common objCommon = new Common();
    VehicleInsurancePremiumPresenter objVehicleInsurancePremiumPresenter;
    private ScriptManager scm_VehicleInsurance;
    DropDownList ddl_PremiumType;
    TextBox txt_Amount;
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
        set{Wuc_Insurance_Date.SelectedDate = value;}
        get{return Wuc_Insurance_Date.SelectedDate;}
    }

    public string ChequeNo
    {
        set { txt_Cheque_No.Text = value; }
        get { return txt_Cheque_No.Text; }
    }

    public DateTime ChequeDate
    {
        set{Wuc_Cheque_Date.SelectedDate = value;}
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
        set{
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
        set{WucVehicleSearch1.VehicleID = value;}
        get{return WucVehicleSearch1.VehicleID;}
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
        set { ServiceTaxPercent = value; }
        get { return ServiceTaxPercent; }
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
        TextBox txt_Vehicle = (TextBox )WucVehicleSearch1.FindControl("txt_Vehicle_Last_4_Digits");

        bool _isValid = false;
        bool Is_ValidDate = true;
        DateTime Expiry_Date = DateTime.Now;

        if (Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty)
        {
            objDT = objVehicleInsurancePremiumPresenter.IsValidExpiryDate();

            Is_ValidDate = Util.String2Bool(objDT.Rows[0]["Is_Valid_Date"].ToString());
            Expiry_Date = Convert.ToDateTime(objDT.Rows[0]["To_Date"].ToString()); 
        }

        //if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && InsuranceDate < DateTime.Now.Date)
        //{
        //    ErrorMessage = "Insurance Date can't be less than Today's date";
        //}
        //else 
        if (Datemanager.IsValidProcessDate("VIP_ID", InsuranceDate) == false)
        {
            errorMessage = GetLocalResourceObject("Msg_InsuPremiumDate").ToString();
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_Vehicle.Text == string.Empty && Vehicle_Id < 0)
        {
            errorMessage = "Please Enter Vehicle No";
            scm_VehicleInsurance.SetFocus(txt_Vehicle);
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && VehicleID == -1 && Vehicle_Id < 0)
        {
            errorMessage = GetLocalResourceObject("Msg_VehicleTypeID").ToString();
            scm_VehicleInsurance.SetFocus(txt_Vehicle);
        }
        else if (InsuranceCompanyID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_InsuranceCompanyID").ToString();
            scm_VehicleInsurance.SetFocus(ddl_InsuranceCompany);
        }
        else if (IssuingBranchID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_IssuingBranchID").ToString();
            scm_VehicleInsurance.SetFocus(ddl_IssuingBranch);
        }
        else if (PolicyNo == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_PolicyNo").ToString();
            scm_VehicleInsurance.SetFocus(txt_PolicyNo);
        }
        else if (AgentID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_AgentID").ToString();
            scm_VehicleInsurance.SetFocus(ddl_Agent);
        }
        else if (CommenceDate > ExpiryDate)
        {
            errorMessage = GetLocalResourceObject("Msg_ExpiryDate").ToString();
        }
        //else if (objVehicleInsurancePremiumPresenter.IsExpiryDateValid() == false)
        //{
        //    ErrorMessage = lbl_Errors.Text;
        //}
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_EngineNo.Text == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_EngineNo").ToString();
            txt_EngineNo.Focus();
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_EngineNo.Text != EngineNo)
        {
            errorMessage = GetLocalResourceObject("Msg_Correct Engine No").ToString();
           txt_EngineNo.Focus();
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_ChasisNo.Text == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_ChasisNo").ToString();
            txt_ChasisNo.Focus();
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && txt_ChasisNo.Text != ChasisNo)
        {
            errorMessage = GetLocalResourceObject("Msg_CorrectChasisNo").ToString();
            txt_ChasisNo.Focus();
        }
        else if (IDV <=0)
        {
            errorMessage = GetLocalResourceObject("Msg_IDV").ToString();
            scm_VehicleInsurance.SetFocus(txt_IDV);
        }
        //else if (FirstPartyPremium<=0)
        //{
        //    ErrorMessage = "Please Enter First Party Premium";
        //    scm_VehicleInsurance.SetFocus(txt_FPPremium);
        //}
        else if (ThirdPartyPremium<=0)
        {
            errorMessage = GetLocalResourceObject("Msg_ThirdPartyPremium").ToString();
            scm_VehicleInsurance.SetFocus(txt_TPPremium);
        }
        //else if (LoadingAmountTPP<=0)
        //{
        //    ErrorMessage = "Please Enter Loading TPP";
        //    scm_VehicleInsurance.SetFocus(txt_LoadingTPP);
        //}
        //else if (LoadingPercentFPP<=0)
        //{
        //    ErrorMessage = "Please Enter Loading FPP";
        //    scm_VehicleInsurance.SetFocus(txt_LoadingFPP);
        //}
        //else if (NCBAmount<=0)
        //{
        //    ErrorMessage = "Please Enter NCB On FPP";
        //    scm_VehicleInsurance.SetFocus(txt_NCBFPP);
        //}
        else if (rdl_Paid_By.SelectedValue == "1" && ChequeNo == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_ChequeNo").ToString();
            scm_VehicleInsurance.SetFocus(txt_Cheque_No);
        }
        else if (rdl_Paid_By.SelectedValue == "1" && txt_Cheque_No.Text.Length < 6)
        {
            errorMessage = GetLocalResourceObject("Msg_ChequeNoLength").ToString();
            scm_VehicleInsurance.SetFocus(txt_Cheque_No);
        }
        else if (rdl_Paid_By.SelectedValue == "1" && Bank_ID == 0)
        {
            errorMessage = GetLocalResourceObject("Msg_Bank").ToString();
            scm_VehicleInsurance.SetFocus(ddl_Bank_Name);
        }
      
        else if (CheckChequeDate()==false)            
        {
            _isValid = false;
            return _isValid;
        }
        else if (rdl_Paid_By.SelectedValue == "1" && ChequeDate < InsuranceDate)
        {
            errorMessage = GetLocalResourceObject("msg_chequedate").ToString();
        }
        else if ((Util.String2Int(hdn_VehicleTypeID.Value) <= 0 || hdn_VehicleTypeID.Value == string.Empty) && Is_ValidDate == false)
        {
            errorMessage = GetLocalResourceObject("Msg_CommenceDate").ToString() + " " + Expiry_Date.ToShortDateString();
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
        LoadingAmountFPP = Math.Round(LoadingAmountFPP,2);
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
                    DayDifference = CommenceDate.DayOfYear-ChequeDate.DayOfYear;
                    isLess = "1";
                }
            }                  
            if (DayDifference > 90)
            {
                if (isLess == "1")
                {
                    errorMessage = GetLocalResourceObject("Msg_ChequeDateLessCommenceDate").ToString();
                    return false;
                }
                else if (isLess == "0")
                {
                    errorMessage = GetLocalResourceObject("Msg_ChequeDateGreaterCommenceDate").ToString();
                    return false;
                }
            }
        }
        return true;
    }
    #endregion

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdn_VehicleTypeID.Value = StateManager.GetState<string>("QueryString");
            Vehicle_Id = Util.DecryptToInt(Request.QueryString["VehicleId"]);
            hdn_VehicleId.Value = Vehicle_Id.ToString();

            ServiceTaxPercentage = Convert.ToDecimal(10.30);
        }
        objVehicleInsurancePremiumPresenter = new VehicleInsurancePremiumPresenter(this, IsPostBack);
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
        objVehicleInsurancePremiumPresenter.Save();
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
            errorMessage = GetLocalResourceObject("Msg_Dg_ddl_PremiumType").ToString();
            scm_VehicleInsurance.SetFocus(ddl_PremiumType);
        }
        else if (txt_Amount.Text == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Amount").ToString();
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
                lbl_Errors.Text = GetLocalResourceObject("Msg_DuplicatePremium").ToString();
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
            lbl_Errors.Text = GetLocalResourceObject("Msg_DuplicatePremium").ToString();
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

    protected void ddl_InsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        objVehicleInsurancePremiumPresenter.fillBranchOnInsuCompanyChange();
    }
    #endregion
  
}
