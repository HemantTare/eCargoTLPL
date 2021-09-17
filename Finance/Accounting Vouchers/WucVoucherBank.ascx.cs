using System;
using System.Data;
using System.Data.SqlClient;
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
/// <summary>
/// Author        : Sunil Bhoyar
/// Created On    : 22/11/2008
/// Description   : This Is The Form For Leder VoucherBank Details
/// </summary>

public partial class Finance_Accounting_Vouchers_VoucherBank : System.Web.UI.UserControl
{
    #region ClassVariables

    Common objCommon = new Common();

    #endregion
    #region ControlsValues

    private string BankName
    {
        get { return txt_BankName.Text; }
        set { txt_BankName.Text = value; }
    }

    private string ChequeNo
    {
        get { return txt_ChequeNo.Text; }
        set { txt_ChequeNo.Text = value; }
    }

    private DateTime ChequeDate
    {
        get { return dtp_ChequeDate.SelectedDate; }
        set { dtp_ChequeDate.SelectedDate = value; }
    }

    
    #endregion

    #region ControlsBind



    public DataTable SesssionVoucherDT
    {
        set { StateManager.SaveState("Voucher_DT", value); }
        get { return StateManager.GetState<DataTable>("Voucher_DT"); }
    }

   
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        //if (BankName.Trim()==string.Empty)
        //{
        //    errorMessage = "please Enter Bank Name";
        //}
        //else 
        if ((ChequeNo.Trim() == string.Empty || Util.String2Int(ChequeNo.Trim()) <= 0) && Convert.ToBoolean(GetGlobalResourceObject("FA_Opr", "Validate_Voucher_ChequeNo").ToString()) == true)
        {
            errorMessage = "Please Enter Cheque No";
        }
        //else if (ChequeDate == string.Empty)
        //{
        //    errorMessage = "please Enter Cheque No";
        //}
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


    public int LedgerId
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Ledger_Id"]);
          // return -1;
        }
    }

    public string LedgerName
    {
        get
        {
           return Util.DecryptToString(Request.QueryString["Ledger_Name"]);
            //return "name";
        }
    }

    public decimal Debit
    {
        get { return Math.Abs(convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Debit"]))); }
    }

    public decimal Credit
    {
        get { return Math.Abs(convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Credit"]))); }
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }

    #endregion


    #region OtherProperties

    

    #endregion


    #region OtherMethods


    


    #endregion


    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick",objCommon.ClickedOnceScript_For_JS_Validation(Page,btn_Save));

        if (!IsPostBack)
        {
            DataRow Dr = SesssionVoucherDT.Rows.Find(LedgerId);

            if (Dr != null)
            {
                BankName = Dr["Bank_Name"].ToString();
                ChequeNo = Dr["Cheque_No"].ToString();
                ChequeDate = Convert.IsDBNull(Dr["Cheque_Date"]) == true ? DateTime.Now : Convert.ToDateTime(Dr["Cheque_Date"]);
            }
            else { ChequeDate = DateTime.Now;}
            
             lbl_LedgerName.Text = LedgerName;
                        
        }

        if (Credit > 0)
        {
            BankName = LedgerName;
            txt_BankName.Enabled = false;
        }
        else
        {
            txt_BankName.Enabled = true;
        }
    }

    #endregion


  



 

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            save();
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx");
        }
    }

    private void save()
    {
        DataRow Dr = SesssionVoucherDT.Rows.Find(LedgerId);

        if (Dr == null)
        {
            Dr = SesssionVoucherDT.NewRow();
            Dr["Bank_Name"] = BankName;
            Dr["Cheque_No"] = ChequeNo;
            Dr["Cheque_Date"] = ChequeDate;
            Dr["Ledger_Id"] = LedgerId;

            SesssionVoucherDT.Rows.Add(Dr);
        }
        else 
        {
            Dr["Bank_Name"] = BankName;
            Dr["Cheque_No"] = ChequeNo;
            Dr["Cheque_Date"] = ChequeDate;
            Dr["Ledger_Id"] = LedgerId;

        }
        SesssionVoucherDT.AcceptChanges();
    }

    
}














 


   
 

