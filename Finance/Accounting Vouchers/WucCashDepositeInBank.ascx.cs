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


public partial class Finance_Accounting_Vouchers_CashDepositeInBank : System.Web.UI.UserControl, ICashDepositeInBankView
{
    #region ClassVariables
    Common objCommon = new Common();
    CashDepositeInBankPresenter objCashDepositeInBankPresenter;

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

    public int Default_Bank_Ledger_Id
    {
        set { hdn_Default_Bank_Ledger_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Default_Bank_Ledger_Id.Value); }
    }

    public string Default_Bank_Ledger_Name
    {
        set { hdn_Default_Bank_Ledger_Name.Value = value; }
        get { return hdn_Default_Bank_Ledger_Name.Value; }
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
            errorMessage = "Please Enter Bank Ledger";
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
        else
        {
            _isValid = true;
        }
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
           // return -1;// 434981;
        }
    }

    #endregion
 
 

    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_SaveNew.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_SaveNew));
        btn_SaveExit.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_SaveExit));

        objCashDepositeInBankPresenter = new CashDepositeInBankPresenter(this, IsPostBack);
             
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                ddl_Ledger.DataTextField = "Ledger_Name";
                ddl_Ledger.DataValueField = "Ledger_Id";

                Raj.EC.Common.SetValueToDDLSearch(hdn_Default_Bank_Ledger_Name.Value, hdn_Default_Bank_Ledger_Id.Value , ddl_Ledger);
            } 
        }
        ddl_Ledger.OtherColumns = "Contra";

        txt_PaidToWhom.Focus();
    }
     
    protected void btn_SaveNew_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            _flag = "SaveAndNew";
            objCashDepositeInBankPresenter.Save();
        }
    } 

    protected void btn_SaveExit_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            _flag = "SaveAndExit";
            objCashDepositeInBankPresenter.Save();
        }
    }
    

    #endregion
}
 