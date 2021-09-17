using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceModel;
using Raj.EC;


/// <summary>
/// Author : Ankit champaneriya
/// Date Created : 11/24/2008
/// Description : This page is For Add And Edit AdvancePayment Transactions, 
/// Change Hostory :
/// Changed By    Date(DD/MM/YYYY)      Description
/// ================================================
/// </summary>

public partial class FA_WucAdvancePayment : System.Web.UI.UserControl, IAdvancePaymentView
{
    #region ClassVariables
    AdvancePaymentPresenter objAdvancePaymentPresenter;
    //private bool _isVT = Convert.ToBoolean(Param.getUserParam().Is_VT);
    private int _Division_Id = Convert.ToInt32(UserManager.getUserParam().DivisionId);
    Common objCommon = new Common();
    #endregion

    #region ControlsValues

    public string PaymentNo
    {
        set { lbl_PaymentNo.Text = value; }
    }

    public ITDSPaymentDeductionView GetITDSPaymentDeductionView
    {
        get { return (ITDSPaymentDeductionView)WucTDSPaymentDeduction1; }
    }

    public DateTime PaymentDate
    {
        set { dtp_VoucheDate.SelectedDate = value; }
        get { return dtp_VoucheDate.SelectedDate; }
    }


    public string RefNo
    {
        set { txt_RefNo.Text = value; }
        get { return txt_RefNo.Text; }
    }

    public string ChequeNo
    {
        set { txt_ChequeNo.Text = value; }
        get { return txt_ChequeNo.Text; }
    }

    public string RefNoPartyLedger
    {
        set { txt_RefNoPartyLedger.Text = value; }
        get { return txt_RefNoPartyLedger.Text; }
    }

    public string RefNoTDSLedger
    {
        set { txt_RefNoTDSLedger.Text = value; }
        get { return txt_RefNoTDSLedger.Text; }
    }
    public string Narration
    {
        set { txt_Narration.Text = value; }
        get { return txt_Narration.Text; }
    }

    public int CashBankLedgerId
    {
        set { ddl_CashBankLedger.SelectedValue = value.ToString(); }
        get { return ddl_CashBankLedger.SelectedValue == "0" ? 0 : Util.String2Int(ddl_CashBankLedger.SelectedValue.Split(new char[] { 'Ö' })[0]); }
    }

    public bool IsCheckNo
    {
        get
        {
            if (CashBankLedgerId > 0)
            {
                string[] splitted = ddl_CashBankLedger.SelectedValue.Split(new char[] { 'Ö' });
                return Util.String2Int(splitted[1]) == 1 ? true : false;
            }
            else { return false; }
        }
    }

    public bool IsTdsApplicable
    {
        get
        {
            if (PartyLedgerId > 0)
            {
                return Util.String2Int(ddl_PartyLedger.GetValueAt(1)) == 1 ? true : false;
            }
            else { return false; }
        }
    }

    public int PartyLedgerId
    {
        get { return Util.String2Int(ddl_PartyLedger.SelectedValue); }
    }

    public int TDSLedgerId
    {
        set { ddl_TDSLedger.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_TDSLedger.SelectedValue); }
    }

    public decimal Amount
    {
        set { txt_Amount.Text = value.ToString(); }
        get { return Util.String2Decimal(txt_Amount.Text); }
    }

    public void SetPartyLedgerId(string value, string text)
    {
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_PartyLedger);
    }

    public DataTable bind_ddl_CashBankLedger
    {
        set
        {
            ddl_CashBankLedger.DataTextField = "Ledger_Name";
            ddl_CashBankLedger.DataValueField = "Ledger_Id";
            ddl_CashBankLedger.DataSource = value;
            ddl_CashBankLedger.DataBind();
            ddl_CashBankLedger.Items.Insert(0, new ListItem("---Select One---", "0"));
        }
    }

    public DataTable bind_ddl_TDSLedger
    {
        set
        {
            ddl_TDSLedger.DataTextField = "Ledger_Name";
            ddl_TDSLedger.DataValueField = "Ledger_Id";
            ddl_TDSLedger.DataSource = value;
            ddl_TDSLedger.DataBind();
            ddl_TDSLedger.Items.Insert(0, new ListItem("---Select One---", "0"));
        }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        if (PaymentDate < (DateTime)UserManager.getUserParam().StartDate || PaymentDate > (DateTime)UserManager.getUserParam().EndDate)
        {
            errorMessage = "Please Select Valid Date";
        }
        else if (CashBankLedgerId <= 0)
        {
            errorMessage = "Please Select Cash/Bank Ledger";
        }
        else if (PartyLedgerId < 0)
        {
            errorMessage = "Please Select Party Ledger";
        }
        else if (Amount <= 0)
        {
            errorMessage = "Please Enter Amount";
        }
        else if (IsTdsApplicable && TDSLedgerId <= 0)
        {
            errorMessage = "Please Select TDS Ledger";
        }
        else if (IsTdsApplicable && RefNoTDSLedger.Trim() == "")
        {
            errorMessage = "Please Enter TDS Ledger Ref No";
        }
        else if (objAdvancePaymentPresenter.ValidateRefNo(RefNoPartyLedger, PartyLedgerId) == false)
        {
            errorMessage = "Duplicate Party Ledger Ref No. Found";
        }
        else if (objAdvancePaymentPresenter.ValidateRefNo(RefNoTDSLedger, TDSLedgerId) == false)
        {
            errorMessage = "Duplicate TDS Ledger Ref No. Found";
        }
        else { _isValid = true; }
        return _isValid;
    }

    private bool OnOkValidateUI()
    {
        bool _isValid = false;

        if (CashBankLedgerId <= 0)
        {
            errorMessage = "Please Select Cash/Bank Ledger";
        }
        else if (PartyLedgerId < 0)
        {
            errorMessage = "Please Select Party Ledger";
        }
        else if (Amount <= 0)
        {
            errorMessage = "Please Enter Amount";
        }
        else if (IsTdsApplicable && TDSLedgerId <= 0)
        {
            errorMessage = "Please Select TDS Ledger";
        }
        else { _isValid = true; }
        return _isValid;
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            // return 29;
        }
    }

    #endregion

    #region OtherProperties

    #endregion

    #region OtherMethods

    #endregion

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick",objCommon.ClickedOnceScript_For_JS_Validation(Page,btn_Save));

        ddl_PartyLedger.DataTextField = "Ledger_Name";
        ddl_PartyLedger.DataValueField = "Ledger_Id";
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                
                //Common objCommon = new Common();
                //if ((bool)Param.getUserParam().Is_VT)
                //{
                //    PaymentNo = objCommon.FA_VT_Generate_Voucher_No((string)UserManager.getUserParam().HierarchyCode , (int)Param.getUserParam().MainId, (int)Param.getUserParam().YearCode, 8);
                //}
                //else { PaymentNo = objCommon.FA_VX_Generate_Voucher_No((string)Param.getUserParam().HierarchyCode, (int)Param.getUserParam().MainId, (int)Param.getUserParam().YearCode, 8); }

                PaymentDate = DateTime.Now; 
            }
        }

        objAdvancePaymentPresenter = new AdvancePaymentPresenter(this, IsPostBack);
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.FA.CallBack));

        if (IsCheckNo)
        { tbl_ChequeNo.Style.Add("visibility", "visible"); }
        else { tbl_ChequeNo.Style.Add("visibility", "hidden"); }


        if (IsTdsApplicable)
        { tbl_TDS.Style.Add("visibility", "visible"); }
        else { tbl_TDS.Style.Add("visibility", "hidden"); }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            btn_Ok_Click(sender, e);
            objAdvancePaymentPresenter.Save();
        }
    }
    #endregion

    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        if (OnOkValidateUI())
        {
            GetITDSPaymentDeductionView.Amount = Amount;
            GetITDSPaymentDeductionView.Ledger_Id = PartyLedgerId;
            GetITDSPaymentDeductionView.TDS_Ledger_Id = TDSLedgerId;
            GetITDSPaymentDeductionView.Voucher_Date = PaymentDate;
            WucTDSPaymentDeduction1.UpdateWuc();
        }
    }
}














 


   
 
