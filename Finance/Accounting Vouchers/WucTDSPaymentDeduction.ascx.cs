using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;

using ClassLibraryMVP;


public partial class FA_Common_Accounting_Vouchers_WucTDSPaymentDeduction : System.Web.UI.UserControl, ITDSPaymentDeductionView
{
    #region ClassVariable
    TDSPaymentDeductionPresenter objTDSPaymentDeductionPresenter;

    private decimal _Amount;
    private int _TDS_Ledger_Id;
    private int _Ledger_Id;
    private DateTime _Voucher_Date;

    #endregion

    #region ControlsValue

    public decimal Amount
    {
        get { return _Amount; }
        set { _Amount = value; }
    }
    public int TDS_Ledger_Id
    {
        get { return _TDS_Ledger_Id; }
        set { _TDS_Ledger_Id = value; }
    }
    public int Ledger_Id
    {
        get { return _Ledger_Id; }
        set { _Ledger_Id = value; }
    }
    public DateTime Voucher_Date
    {
        get { return _Voucher_Date; }
        set { _Voucher_Date = value; }
    }

    public string PartyName
    {
        set { lbl_party.Text = value; }
    }
    public string DeducteeTypeName
    {
        set { lbl_DeducteeType.Text = value; }
    }

    //public int PartyId
    //{
    //    get { return Util.DecryptToInt(Request.QueryString["PartyId"]); }
    //    //get { return 17; }
    //}

    //public int DeducteeTypeId
    //{
    //    get { return Util.DecryptToInt(Request.QueryString["DeducteeTypeId"]); }
    //}

    public string AmountOfThisVoucher
    {
        set { lbl_AmtOfThisVoucher.Text = value; }

    }
    public string AmountPaidPayableTillDate
    {
        set { lbl_AmtPaidPayableTillDate.Text = value; }

    }
    public string TotalAmountPaidPayable
    {
        set { lbl_TotAmtPaidPayable.Text = value; }
    }
    public string TaxPercent
    {
        set { lbl_TaxPercent.Text = value; }
    }
    public string TaxAmount
    {
        set { lbl_TaxAmount.Text = value; }
    }
    public string SurchargePercent
    {
        set { lbl_SurcharePercent.Text = value; }
    }
    public string SurchargeAmount
    {
        set { lbl_SurchargeAmount.Text = value; }
    }
    public string AdditionalSurchargePercent
    {
        set { lbl_AddSurchargePercent.Text = value; }
    }
    public string AdditionalSurchargeAmount
    {
        set { lbl_AddSurchargeAmount.Text = value; }
    }
    public string AdditionalCessPercent
    {
        set { lbl_AddCessPercent.Text = value; }
    }
    public string AdditionalCessAmount
    {
        set { lbl_AddCessAmount.Text = value; }
    }
    public string TotalTDS
    {
        set { lbl_TotTDSAmount.Text = value; }
    }
    public string TDSDeductedTillDate
    {
        set { lbl_TDSDeductedTillDate.Text = value; }
    }
    public string NetTDSDeducted
    {
        set { lbl_NetTDSToDeduct.Text = value; }
        get { return lbl_NetTDSToDeduct.Text; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        return true;
    }
    public int keyID
    {
        get { return Util.String2Int(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
            // lbl_Errors.Text = value;
        }
    }

    #endregion

    #region PageEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        objTDSPaymentDeductionPresenter = new TDSPaymentDeductionPresenter(this, IsPostBack);
    }

    public void UpdateWuc()
    {
        objTDSPaymentDeductionPresenter.ReadValues();
    }
    #endregion
}