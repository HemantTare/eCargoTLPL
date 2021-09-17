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


public partial class Finance_Accounting_Vouchers_CashBankReceipt : System.Web.UI.UserControl, ICashBankReceiptView
{
    #region ClassVariables
    Common objCommon = new Common();
    CashBankReceiptPresenter objCashBankReceiptPresenter;

    string _flag;  
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

    public string ChequeBank
    {
        set { txt_DrawneeBank.Text = value; }
        get { return txt_DrawneeBank.Text; }
    }
 

    private void SetLedgerId(string value,string text)
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
            errorMessage = "Please Enter Expense Ledger";
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
            else if (ChequeBank == "")
            {
                errorMessage = "Please Enter Drawnee Bank";
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
        btn_SaveNew.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_SaveNew));
        btn_SaveExit.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_SaveExit));

        objCashBankReceiptPresenter = new CashBankReceiptPresenter(this, IsPostBack);
             
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                ddl_Ledger.DataTextField = "Ledger_Name";
                ddl_Ledger.DataValueField = "Ledger_Id";
            }
            //dtp_ChequeDate.SelectedDate = DateTime.Now; 
                rdbtnCashCheque.SelectedValue = "0"; 
        }
        ddl_Ledger.OtherColumns = "Receipt";

        txt_RefNo.Focus();

        //rdbtnCashCheque_CheckedSelected();
    }
     
    protected void btn_SaveNew_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            _flag = "SaveAndNew";
            objCashBankReceiptPresenter.Save();
        }
    } 

    protected void btn_SaveExit_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            _flag = "SaveAndExit";
            objCashBankReceiptPresenter.Save();
        }
    }

    //protected void rdbtnCashCheque_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    rdbtnCashCheque_CheckedSelected();
    //}

    //private void rdbtnCashCheque_CheckedSelected()
    //{
    //    if (rdbtnCashCheque.SelectedValue == "1")
    //    {
    //        pnl_ChequeDetails.Visible = true;
    //        EnableDisableControls(true);
    //        dtp_ChequeDate.SelectedDate = DateTime.Now;
    //    }
    //    else
    //    {
    //        pnl_ChequeDetails.Visible = false;
    //        EnableDisableControls(false);
    //    }
    //}

    //private void EnableDisableControls(bool flag)
    //{
    //    lblChequeNo.Visible = flag;
    //    txt_ChequeNo.Visible = flag;
    //    lblChequeDate.Visible = flag;
    //    dtp_ChequeDate.Visible = flag;
    //    //UpdatePanel9.Visible = flag;
    //    lblDrawneeBank.Visible = flag;
    //    txt_DrawneeBank.Visible = flag;
    //    tr_cheque_Details.Visible = flag;
    //    tr_DrawneeBank.Visible = flag;

    //    if (flag == false)
    //    {
    //        txt_ChequeNo.Text = "";
    //        txt_DrawneeBank.Text = "";
    //    }
    //}

    #endregion
}
 