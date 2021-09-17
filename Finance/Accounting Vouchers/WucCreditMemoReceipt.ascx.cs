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
using ClassLibraryMVP.DataAccess;
using Raj.EC;


/// <summary>
/// Author        : Aashish Lad
/// Created On    : 12th March 2009
/// Description   : This Is The Form Credit Memo Receipt
/// </summary>
/// 
public partial class Finance_Accounting_Vouchers_WucCreditMemoReceipt : System.Web.UI.UserControl,ICreditMemoReceiptView 
{

    #region ClassVariables
    CreditMemoReceiptPresenter objCreditMemoReceiptPresenter;    
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    DataSet DS = new DataSet();
    private string _GCXML;
    private string _GetDetailsXML;    
    #endregion

    #region ControlsValue

    public string ReceiptNo
    {
        set { lbl_ReceiptNoValue.Text = value; }
        get { return lbl_ReceiptNoValue.Text; }
    }
    public string GCXML
    {
        set { _GCXML = value; }
        get { return _GCXML; }
    }
    public string GetDetailsXML
    {
        set { _GetDetailsXML = value; }
        get { return _GetDetailsXML; }
    } 
    public string ChequeNo
    {
        set { txt_ChequeNo.Text = value; }
        get { return txt_ChequeNo.Text; }
    }
    public string BankName
    {
        set { txt_BankName.Text = value; }
        get { return txt_BankName.Text; }
    }
    public string Remarks
    {
        set { txt_Remark.Text = value; }
        get { return txt_Remark.Text; }
    }    
    public int TotalCreditMemo
    {
        set { hdn_TotalCreditMemo.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_TotalCreditMemo.Value); }
    }

    public int PartyNameID
    {
     
        get { return Util.String2Int(ddl_PartyName.SelectedValue); }
    }
    public decimal ChequeAmount
    {
        set
        {
            txt_ChequeAmount.Text = Util.Decimal2String(value);            
        }
        get {
            if (txt_ChequeAmount.Text.Trim() == "")
            {
                txt_ChequeAmount.Text = "0";
            }
                return Util.String2Decimal(txt_ChequeAmount.Text); 
        }
    }
    public decimal CashAmount
    {
        set
        {
            txt_CashAmount.Text = Util.Decimal2String(value);
        }
        get
        {
            if (txt_CashAmount.Text.Trim() == "")
            {
                txt_CashAmount.Text = "0";
            }
            return Util.String2Decimal(txt_CashAmount.Text); 
        }    
    }
    public decimal TotalAmount
    {
        set
        {
            lbl_TotalValue.Text = Util.Decimal2String(value);
            hdn_Total.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total.Value); }
    }
    public DateTime ReceiptDate
    {
        set { Wuc_ReceiptDate.SelectedDate = value; }
        get { return Wuc_ReceiptDate.SelectedDate; }
    }
    public DateTime ChequeDate
    {
        set { Wuc_ChequeDate.SelectedDate = value; }
        get { return Wuc_ChequeDate.SelectedDate; }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
      
        if (ddl_PartyName.SelectedValue == "")
        {
            errorMessage = "Please Select Party Name";
        }
        else if (TotalCreditMemo <= 0)
        {
            errorMessage = "Please Select Atleast One Credit Memo";
        }        
        else if (CheckAmountReceived() == false)
        {

        }
        else if (TotalAmount!=(CashAmount+ChequeAmount))
        {
            errorMessage = "Cash And Cheque Amount Should Be Match With Total Amount";
        }
        else if (ChequeAmount>0 && CheckCheque() == false)
        {
        }
        else if (ReceiptDate < ChequeDate)
        {
            errorMessage = "Cheque Date Should Be Greater Than Receipt Date";
        }
        else if (Remarks == "")
        {
            errorMessage = "Please Enter Remark";
            txt_Remark.Focus();
        }
        else
        {
            //GetDetailsTableXML();
            _isValid = true;
        }
        return _isValid;
    }


    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1;
        }
    }

    #endregion
    #region ControlsBind
    public DataTable Bind_dg_CreditMemoDetails
    {
        set
        {
            dg_CreditMemoDetails.DataSource = value;
            dg_CreditMemoDetails.DataBind();
        }
    }
    public DataTable SessionCreditMemoDetails
    {
        get { return StateManager.GetState<DataTable>("CreditMemoDetails"); }
        set
        {
            StateManager.SaveState("CreditMemoDetails", value);

            if (StateManager.Exist("CreditMemoDetails"))
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["Sr_No"] = i;
                }
            }
        }
    }

    #endregion
    #region OtherProperties
    #endregion

    #region OtherMethods
    public void SetPartyNameId(string text, string value)
    {
        ddl_PartyName.DataTextField = "Ledger_Name";
        ddl_PartyName.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_PartyName);
    }


    private void GetDetailsTableXML()
    {
        DataSet _objDs = new DataSet();
        int i;

        CheckBox chk_Attach;
        TextBox txt_AmountReceived ;
        DataRow _dr;
        _objDs.Tables.Add(SessionCreditMemoDetails.Clone());
        for (i = 0; i < dg_CreditMemoDetails.Items.Count; i++)
        {
            chk_Attach = (CheckBox)dg_CreditMemoDetails.Items[i].FindControl("chk_Attach");
            txt_AmountReceived = (TextBox)dg_CreditMemoDetails.Items[i].FindControl("txt_AmountReceived");            
            if (chk_Attach.Checked == true)
            {
                _dr = _objDs.Tables[0].NewRow();
                _dr = SessionCreditMemoDetails.Rows[i];
                _dr["Amount_Received"] = Convert.ToDecimal(txt_AmountReceived.Text);                
                _objDs.Tables[0].ImportRow(_dr);
            }
        }
        TotalCreditMemo = _objDs.Tables[0].Rows.Count;
        GetDetailsXML = _objDs.GetXml();
        //return _objDs.GetXml();
    }
    private bool CheckAmountReceived()
    {
        CheckBox chk_Attach;
        TextBox txt_AmountReceived, txt_Remark;
        int i;
        for (i = 0; i < dg_CreditMemoDetails.Items.Count; i++)
        {
            chk_Attach = (CheckBox)dg_CreditMemoDetails.Items[i].FindControl("chk_Attach");
            txt_AmountReceived = (TextBox)dg_CreditMemoDetails.Items[i].FindControl("txt_AmountReceived");            
            if (chk_Attach.Checked == true)
            {
                if (Util.String2Decimal(txt_AmountReceived.Text) <= 0)
                {
                    errorMessage = "Amount Received Should be Greater then Zero";
                    txt_AmountReceived.Focus();
                    return false;
                }
            }
        }
        return true;
    }
    //public void ClearVariables()
    //{
    //    SessionCreditMemoDetails = null;
    //}
     private bool  CheckCheque()
     {     
           if(ChequeNo=="")
           {
                errorMessage="Please Enter Cheque No.";
                txt_ChequeNo.Focus();
                return false;
           }
           else if (ChequeNo.Length<6)
           {
                errorMessage="Cheque No Should be Greater than Six";
                txt_ChequeNo.Focus();
                return false;
           }
 
        return true;
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_PartyName.DataTextField = "Ledger_Name";
        ddl_PartyName.DataValueField = "Ledger_Id";
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if(!IsPostBack)
        {
            ReceiptNo = ObjCommon.Get_Next_Number();
        }
        
        objCreditMemoReceiptPresenter = new CreditMemoReceiptPresenter(this, IsPostBack);
        Upd_Pnl_Grid.Update();
    }
    protected void ddl_PartyName_TxtChange(object sender, EventArgs e)
    {
        objCreditMemoReceiptPresenter.FillGrid();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        GetDetailsTableXML();
        objCreditMemoReceiptPresenter.Save();
    }
}
