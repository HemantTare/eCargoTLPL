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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using Raj.EC.ControlsView;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 13/10/2008
/// Description   : This Page is For Master Client Finance Details
/// </summary>
/// 
public partial class Master_Sales_WucClientFinanceDetails : System.Web.UI.UserControl, IClientFinanceView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    ClientFinancePresenter objClientFinancePresenter;
    private ScriptManager scm_ClientFinance;
    private int _ClientGroupId = 0;
    #endregion

    #region ControlsValues

    public int ClientGroupId
    {
        set { _ClientGroupId = value; }
        get { return _ClientGroupId; }
    }

    public bool EnableControls
    {
        set
        {
            rbtn_UseExistingLedger.Enabled = value;
            ddl_Ledger.Enabled = value;
        }
    }

    public bool Is_ExistingLedger
    {
        set
        {
            Boolean IsChk = value;

            if (IsChk == false)
                rbtn_UseExistingLedger.SelectedValue = "1";
            else
                rbtn_UseExistingLedger.SelectedValue = "0";
        }
        get { return rbtn_UseExistingLedger.SelectedValue == "0" ? true : false; }
    }

    public bool Is_CreditParty
    {
        set
        {
            Boolean IsChk = value;

            if (IsChk == false)
                rbtn_CreditOrAdvanceParty.SelectedValue = "1";
            else
                rbtn_CreditOrAdvanceParty.SelectedValue = "0";
        }
        get { return rbtn_CreditOrAdvanceParty.SelectedValue == "0" ? true : false; }
    }

    public bool Is_LoadingTypeId
    {
        set
        {
            Boolean IsChk1 = value;
            if (IsChk1 == false)
                rbl_LoadingType.SelectedValue = "0";
            else
                rbl_LoadingType.SelectedValue = "1";
        }
        get { return rbl_LoadingType.SelectedValue == "1" ? true : false; }
    }

    public int LedgerID
    {
        get { return Util.String2Int(ddl_Ledger.SelectedValue); }
    }
    public int CreditDays
    {
        set { txt_CreditDays.Text = Util.Int2String(value); }
        get { return txt_CreditDays.Text == string.Empty ? 0 : Util.String2Int(txt_CreditDays.Text); }
    }
    public decimal CreditLimit
    {
        set { txt_CreditLimit.Text = Util.Decimal2String(value); }
        get { return txt_CreditLimit.Text == string.Empty ? 0 : Util.String2Decimal(txt_CreditLimit.Text); }
    }
    public decimal MinimumBalance
    {
        set { txt_MinimumBalance.Text = Util.Decimal2String(value); }
        get { return txt_MinimumBalance.Text == string.Empty ? 0 : Util.String2Decimal(txt_MinimumBalance.Text); }
    }
    public decimal IntrestPercent
    {
        set { txt_InterestPercentage.Text = Util.Decimal2String(value); }
        get { return txt_InterestPercentage.Text == string.Empty ? 0 : Util.String2Decimal(txt_InterestPercentage.Text); }
    }
    public int GraceDays
    {
        set { txt_GraceDays.Text = Util.Int2String(value); }
        get { return txt_GraceDays.Text == string.Empty ? 0 : Util.String2Int(txt_GraceDays.Text); }
    }
    public bool IsServiceTaxPayByClient
    {
        set { chk_ServiceTaxPayableByClient.Checked = value; }
        get { return chk_ServiceTaxPayableByClient.Checked; }
    }
    public DateTime RegistrationDate
    {
        set { dtp_RegistrationDate.SelectedDate = value; }
        get { return dtp_RegistrationDate.SelectedDate; }
    }
    public string BusinessHours
    {
        set { txt_BusinessHrs.Text = value; }
        get { return txt_BusinessHrs.Text; }
    }
    public bool IseCargoUser
    {
        set { chk_CreateeCargouser.Checked = value; }
        get { return chk_CreateeCargouser.Checked; }
    }
    public int UserProfileId
    {
        set { ddl_UserProfile.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_UserProfile.SelectedValue); }
    }

    public int MarketingExcutiveId
    {
        get { return Util.String2Int(ddl_MarketingExecutive.SelectedValue); }
    }
    public void SetMarketingExcutiveId(string text, string value)
    {
        ddl_MarketingExecutive.DataTextField = "Emp_Name";
        ddl_MarketingExecutive.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_MarketingExecutive);
    }
    public void SetLedgerId(string text, string value)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);
    }
    public bool IsPrintFrtOnLR
    {
        set { chk_PrintFrtOnLR.Checked = value; }
        get { return chk_PrintFrtOnLR.Checked; }
    }

    #endregion

    #region ControlsBind

    public DataTable BindUserProfile
    {
        set
        {
            ddl_UserProfile.DataTextField = "Profile_Name";
            ddl_UserProfile.DataValueField = "Profile_ID";
            ddl_UserProfile.DataSource = value;
            ddl_UserProfile.DataBind();
            Raj.EC.Common.InsertItem(ddl_UserProfile);
        }
    }

    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_ClientFinance = value; }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        errorMessage = "";
        bool _isValid = false;

        TextBox txt_Ledger;
        TextBox txt_Mechanics;

        txt_Ledger = (TextBox)ddl_Ledger.FindControl("txtBoxddl_Ledger");
        txt_Mechanics = (TextBox)ddl_MarketingExecutive.FindControl("txtBoxddl_MarketingExecutive");

        if (rbtn_UseExistingLedger.SelectedValue == "0" && LedgerID <= 0)
        {
            errorMessage = "Please Select Ledger";// GetLocalResourceObject("Msg_ddl_Ledger").ToString();
            scm_ClientFinance.SetFocus(txt_Ledger);
        }
        else if (CreditDays < 0 && pc.Control_Is_Mandatory(txt_CreditDays) == true)
        {
            errorMessage = "Please Enter Credit Days";// GetLocalResourceObject("Msg_txt_CreditDays").ToString();
            scm_ClientFinance.SetFocus(txt_CreditDays);
        }
        else if (CreditLimit < 0 && pc.Control_Is_Mandatory(txt_CreditLimit) == true)
        {
            errorMessage = "Please Enter Credit Limit";// GetLocalResourceObject("Msg_txt_CreditLimit").ToString();
            scm_ClientFinance.SetFocus(txt_CreditLimit);
        }
        //////else if (MinimumBalance < 0 && pc.Control_Is_Mandatory(txt_MinimumBalance) == true)
        //////{
        //////    errorMessage = "Please Enter Minimum Balance";// GetLocalResourceObject("Msg_txt_CreditLimit").ToString();
        //////    scm_ClientFinance.SetFocus(txt_MinimumBalance);
        //////}
        else if (IntrestPercent < 0 && pc.Control_Is_Mandatory(txt_InterestPercentage) == true)
        {
            errorMessage = "Please Enter Intrest Percent";// GetLocalResourceObject("Msg_txt_IntrestPercent").ToString();
            scm_ClientFinance.SetFocus(txt_InterestPercentage);
        }
        else if (GraceDays <= 0 && pc.Control_Is_Mandatory(txt_GraceDays) == true)
        {
            errorMessage = "Please Enter Grace Days";// GetLocalResourceObject("Msg_txt_GraceDays").ToString();
            scm_ClientFinance.SetFocus(txt_GraceDays);
        }
        else if (RegistrationDate > System.DateTime.Now && pc.Control_Is_Mandatory(RegistrationDate) == true)
        // else if(Datemanager.IsValidProcessDate("CFD_REG", RegistrationDate ) == false)
        {
            errorMessage = "Registration Date Shoult not be greater than System date";// GetLocalResourceObject("Msg_Dtp_RegistrationDate").ToString();
        }
        else if (BusinessHours == string.Empty && pc.Control_Is_Mandatory(txt_BusinessHrs) == true)  //added by Ankit champaneriya
        {
            errorMessage = "Please Enter Business Hours";// GetLocalResourceObject("Msg_Txt_BusinessHours").ToString();
            scm_ClientFinance.SetFocus(txt_BusinessHrs);
        }
        else if (MarketingExcutiveId <= 0)
        {
            errorMessage = "Please Select Marketing Executive";// GetLocalResourceObject("Msg_DDL_MarketingExcutive").ToString();
            scm_ClientFinance.SetFocus(txt_Mechanics);
        }

        else if (chk_CreateeCargouser.Checked == true)
        {
            if (UserProfileId <= 0)
            {
                errorMessage = "Please Select User Profile";
                scm_ClientFinance.SetFocus(ddl_UserProfile);
                _isValid = false;
            }
            else
            {
                _isValid = true;
            }
        }
        else if (rbtn_CreditOrAdvanceParty.SelectedValue == "0" && CreditLimit == 0)
        {
            errorMessage = "Please Eneter Credit Limit";
            scm_ClientFinance.SetFocus(txt_CreditLimit);
        }

        else if (rbtn_CreditOrAdvanceParty.SelectedValue == "1" && MinimumBalance == 0)
        {
            errorMessage = "Please Eneter Minimum Balance";
            scm_ClientFinance.SetFocus(txt_MinimumBalance);
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
            //return 12;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        objClientFinancePresenter = new ClientFinancePresenter(this, IsPostBack);

        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_ID";

        ddl_MarketingExecutive.DataTextField = "Emp_Name";
        ddl_MarketingExecutive.DataValueField = "Emp_ID";

        ddl_Ledger.OtherColumns = ClientGroupId.ToString();
        upnl_Ledger.Update();

        if (!IsPostBack)
        {
            Setenabledisable();
            SetAsPerCreditAdvanceParty();
            if (keyID > 0)
            {
                if (IseCargoUser == true)
                {
                    tr_IseCargoUser.Style.Add("display", "none");
                }
                rbtn_UseExistingLedger.Items[1].Enabled = false;
            }
        }

        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }

    protected void ddl_Ledger_SelectedIndexChanged(object sender, EventArgs e)
    {
        objClientFinancePresenter.FillLedgerDetails();
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }

    protected void rbtn_UseExistingLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        Setenabledisable();
    }

    protected void rbtn_CreditOrAdvanceParty_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetAsPerCreditAdvanceParty();
    }

    public void HandelsServiceTax(object o, EventArgs e)
    {
        IsServiceTaxPayByClient = Convert.ToBoolean(o);
        UpdatePanel3.Update();
    }
    private void Setenabledisable()
    {
        if (rbtn_UseExistingLedger.SelectedValue == "0")
        {
            tr_ledger.Visible = true;
            txt_CreditDays.Enabled = false;
            txt_CreditLimit.Enabled = false;
        }
        else
        {
            tr_ledger.Visible = false;
            txt_CreditDays.Enabled = true;
            txt_CreditLimit.Enabled = true;
        }
    }

    private void SetAsPerCreditAdvanceParty()
    {
        if (rbtn_CreditOrAdvanceParty.SelectedValue == "0")
        {
            lbl_CreditDays.Visible = true;
            txt_CreditDays.Visible = true;
            lbl_CreditLimit.Visible = true;
            txt_CreditLimit.Visible = true;
            lbl_InterestPercentage.Visible = true;
            txt_InterestPercentage.Visible = true;
            lbl_GraceDays.Visible = true;
            txt_GraceDays.Visible = true;

            lblMinimumBalance.Visible = false;
            txt_MinimumBalance.Visible = false;
            txt_MinimumBalance.Text = "";

        }
        else
        {
            lblMinimumBalance.Visible = true;
            txt_MinimumBalance.Visible = true;

            lbl_CreditDays.Visible = false;
            txt_CreditDays.Visible = false;
            lbl_CreditLimit.Visible = false;
            txt_CreditLimit.Visible = false;
            lbl_InterestPercentage.Visible = false;
            txt_InterestPercentage.Visible = false;
            lbl_GraceDays.Visible = false;
            txt_GraceDays.Visible = false;

            txt_CreditDays.Text = "";
            txt_CreditLimit.Text = "";
            txt_InterestPercentage.Text = "";
            txt_GraceDays.Text = "";


        }
    }
}
