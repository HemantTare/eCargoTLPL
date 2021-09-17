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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC;

using ClassLibraryMVP.Security;
/// <summary>
/// Author        : Sushant Kajrolkar
/// Created On    : 20-03-2014
/// Description   : This Is The Form For Cash Expenses Voucher Details
/// </summary>


public partial class Finance_Accounting_Vouchers_TempoPayment : System.Web.UI.UserControl, ITempoPaymentView
{
    #region ClassVariables
    Common objCommon = new Common();
    TempoPaymentPresenter objTempoPaymentPresenter;


    string _flag;
    int Vehicle_Id;

    #endregion

    #region ControlsValues

    public string Mode
    {
        get {return Request.QueryString["Mode"] ;}
    }
    public string Flag
    {
        get { return _flag; }
    }

    public string Menu_Item_Id
    {
        get { return Request.QueryString["Menu_Item_Id"];}
    }

    public DateTime VoucherDate
    {
        get { return dtp_VoucherDate.SelectedDate; }
        set { dtp_VoucherDate.SelectedDate = value; }
    }
    public DateTime ChequeDate
    {
        get { return dtp_ChequeDate.SelectedDate; }
        set { 
            dtp_ChequeDate.SelectedDate = value;
            //hdn_Cheque_Date.Value = dtp_ChequeDate.SelectedDate.ToString();
        }
    }

    public string RefNo
    {
        set { txt_RefNo.Text = value; }
        get { return txt_RefNo.Text; }
    } 
    public int LedgerId
    { 
        set { ddl_Ledger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Ledger.SelectedValue); }
    }

    public string PaidToWhom
    {
        set { txt_PaidToWhom.Text = value; }
        get { return txt_PaidToWhom.Text; }
    }

    public string Details
    {
        set { txt_Details.Text = value; }
        get { return txt_Details.Text; }
    }

    public decimal Amount
    {
        set { txt_Amount.Text = value.ToString(); }
        get
        {
            return convertToDecimal(txt_Amount.Text);
        }
    } 

    public int Is_CashCheque
    {
        get { return Util.String2Int(rdbtnCashCheque.SelectedValue); }
        set
        {
            rdbtnCashCheque.SelectedValue = value.ToString();
        }

    }

    public string ChequeNo
    {
        set { txt_ChequeNo.Text = value; }
        get { return txt_ChequeNo.Text; }
    }

    public int ClosingBalance
    {
        set { hdn_ClosingBalance.Value = Util.Decimal2String(value); lbl_ClosinBalance.Text = Util.Decimal2String(value); }
        get { return Util.String2Int(hdn_ClosingBalance.Value); }

    }

    public string ClosingBalanceText
    {
        set { hdn_ClosingBalanceText.Value = value; lbl_ClosinBalance.Text = value; }
        get { return hdn_ClosingBalanceText.Value; }

    }

    public int DebitBalLimmit
    {
        set { hdn_DebitBalLimmit.Value = Util.Decimal2String(value); }
        get { return Util.String2Int(hdn_DebitBalLimmit.Value); }
    }

    public void SetLedgerId(string value, string text)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);
    } 

    public int convertToInt(object value)
    {
        if (value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToInt32(value); }
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }

    

    #endregion

    #region ControlsBind
 
  
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (LedgerId <= 0)
        {
            errorMessage = "Please Enter Vendor";
            _isValid = false;
        }
        else if (Amount <= 0)
        {
            errorMessage = "Please Enter Amount";
            _isValid = false;
        }  
        else if (VoucherDate < UserManager.getUserParam().StartDate || VoucherDate > UserManager.getUserParam().EndDate)
        {
            errorMessage = "Voucher Date should be in current Financial Date";
            _isValid = false;
        }
        else if (-1 * convertToDecimal(hdn_DebitBalLimmit.Value) > (convertToDecimal(hdn_ClosingBalance.Value) + (-1 * Amount)))
        {
            errorMessage = "Limmit Se Bahar Hai. Payment Nahi Ho Sakta";
            _isValid = false;
        }
        else if (validateCheque() == false)
        {
            _isValid = false;
        }      
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    private bool validateCheque()
    {
       bool _isValid = false;
        if (Is_CashCheque == 1)
        {
            if (ChequeNo == "")
            {
                errorMessage = "Please Enter Cheque No";
                _isValid = false;
            } 
            else if (ChequeDate < VoucherDate)
            {
                errorMessage = "Please Select Cheque Date Greater then Voucher Date";
                _isValid = false;
            }
            else
            {
                _isValid = true;
            }

            return _isValid;
        }
        else
        { return true; }
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
           // return -1;// 434981;
        }
    }

    #endregion
 
 

    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        //btn_SaveNew.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_SaveNew));
        //btn_SaveExit.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_SaveExit));

        objTempoPaymentPresenter = new TempoPaymentPresenter(this, IsPostBack);
             
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {


            string Crypt = "";

            Crypt = System.Web.HttpContext.Current.Request.QueryString["Vehicle_Id"];
            Vehicle_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            
            if (keyID <= 0)
            {
                ddl_Ledger.DataTextField = "Ledger_Name";
                ddl_Ledger.DataValueField = "Ledger_Id";
                rdbtnCashCheque.SelectedValue = "0";

                if (Vehicle_Id > 0)
                {
                    objTempoPaymentPresenter.GetVendorLedgerDetails(Vehicle_Id);
                }


            }
            else
            {
                if (Is_CashCheque == 1)
                {
                    rdbtnCashCheque.SelectedValue = "1";
                }
            }
            
        }

        
        ddl_Ledger.OtherColumns = "Payment";
        //rdbtnCashCheque_CheckedSelected();
        txt_RefNo.Focus();


    }

    protected void ddl_Ledger_TxtChange(object sender, EventArgs e)
    {
        objTempoPaymentPresenter.readValues();
    }
     
    protected void btn_SaveNew_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            _flag = "SaveAndNew";
            objTempoPaymentPresenter.Save();
        }
    } 

    protected void btn_SaveExit_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            _flag = "SaveAndExit";
            objTempoPaymentPresenter.Save();
        }
    }


    #endregion
}
 