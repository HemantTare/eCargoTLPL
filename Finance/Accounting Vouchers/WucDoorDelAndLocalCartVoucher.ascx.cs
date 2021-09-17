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
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;
using System.Text;
using ClassLibraryMVP.Security;

public partial class Finance_Accounting_Vouchers_WucDoorDelAndLocalCartVoucher : System.Web.UI.UserControl,IDoorDelAndLocalCartVoucherView 
{

    #region ClassVariables
    DoorDelAndLocalCartVoucherPresenter objDoorDelAndLocalCartVoucherPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    DataSet DS=new DataSet();
    private string _GCXML;
    private string _GetDetailsXML;
    private string _VoucherType;
    #endregion

    #region ControlsValue

    public string VoucherNo
    {
        set { lbl_Vch_No.Text = value; }
        get { return lbl_Vch_No.Text; }
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
    public string VoucherType
    {
        set { _VoucherType = Convert.ToString(Rights.GetObject().GetLinkDetails(Raj.EC.Common.GetMenuItemId()).QueryString); }
        get { return Convert.ToString(Rights.GetObject().GetLinkDetails(Raj.EC.Common.GetMenuItemId()).QueryString); }
    }
    
    public string RefNo
    {
        set { txt_RefNo.Text = value; }
        get { return txt_RefNo.Text; }
    }
    public string ChequeNo
    {
        set { txt_Cheque.Text = value; }
        get { return txt_Cheque.Text; }
    }
    public string ChequeInFavour
    {
        set { txt_ChequeInFavourOf.Text = value; }
        get { return txt_ChequeInFavourOf.Text; }
    }
    public string Remark
    {
        set { txt_Remark.Text = value; }
        get { return txt_Remark.Text; }
    }
    public int CreditToLedgerID
    {
        set
       {
           ddl_GetLedgers.SelectedValue = Util.Int2String(value);            
            if (value > 0)
            {
                RadioButtonList1.SelectedValue = "CreditTo";
            }
        }
        get
        {
            if (RadioButtonList1.SelectedValue == "CreditTo")
            {
                return Util.String2Int(ddl_GetLedgers.SelectedValue);
            }
            else
            {
                return 0;
            }
            
       }
    }
    public int TotalGC
    {
        set { lbl_TotalGC.Text = Util.Int2String(value); }
        get { return Util.String2Int(lbl_TotalGC.Text); }
    }
    public bool IsCash
    {
        set 
        {

            if (value==true)
            {
                RadioButtonList1.SelectedValue = "Cash";
            }
            
        }
        get 
        {
            bool _isFlag;
            if (RadioButtonList1.SelectedValue == "Cash")
            {
                _isFlag= true;
            }
            else
            {               
                _isFlag= false;
            }
            return _isFlag;
        }
    }
    public bool IsCreditTo
    {
        set
        {

            if (value == true)
            {
                RadioButtonList1.SelectedValue = "CreditTo";
            }

        }
        get
        {
            bool _isFlag;
            if (CreditToLedgerID>0)
            {
                _isFlag = true;
            }
            else
            {
                _isFlag = false;
            }
            return _isFlag;
        }
    }
    public bool IsCheque
    {
        
        set 
        {
            if (value == true)
            {
                RadioButtonList1.SelectedValue = "Bank";
            }
            
        }
        get
        {
            bool _isFlag;
            if (RadioButtonList1.SelectedValue == "Bank")
            {
                _isFlag= true;
            }
            else
            {
                ChequeNo = "";
                ChequeInFavour = "";
                ChequeDate = DateTime.Now;
                _isFlag= false;
            }
            return _isFlag;
        }
    }
   
    public decimal TotalAmount
    {
        set { lbl_Total_Spent.Text = Util.Decimal2String(value);
              hdn_Total_Spent_Add.Value = Util.Decimal2String(value);  
            }
        get { return Util.String2Decimal(hdn_Total_Spent_Add.Value); }
    }
    public DateTime VoucherDate
    {
        set { WucVoucherDate.SelectedDate = value; }
        get { return WucVoucherDate.SelectedDate; }
    }
    public DateTime ChequeDate
    {
        set { WucChequeDate.SelectedDate = value; }
        get { return WucChequeDate.SelectedDate; }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (RadioButtonList1.SelectedValue == "CreditTo")
        {
            if (ddl_GetLedgers.SelectedValue == "")
            {
                errorMessage = "Please Select Ledger";
                return _isValid;
            }
        }
        else if (TotalGC <= 0)
        {
            errorMessage = "Please Select Atleast One "+ CompanyManager.getCompanyParam().GcCaption;
        }
        else if (CheckAmountSpent() == false)
        {

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
    public DataTable Bind_dg_Voucher
    {
        set
        {            
            dg_Voucher.DataSource = value;
            dg_Voucher.DataBind();
        }
    }
    public DataTable SessionVoucherGrid
    {
        get { return StateManager.GetState<DataTable>("VoucherGrid"); }
        set
        {
            StateManager.SaveState("VoucherGrid", value);

            if (StateManager.Exist("VoucherGrid"))
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
    public void SetLedgerID(string Ledger_Name, string LedgerID)
    {
        ddl_GetLedgers.DataTextField = "Ledger_Name";
        ddl_GetLedgers.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(Ledger_Name, LedgerID, ddl_GetLedgers);
    }

    private void Get_Items_XML()
    {
        StringBuilder EnteredItemsXml = new StringBuilder();

        string Comma_Seperated_Items = "";
        Comma_Seperated_Items = txt_GCNo.Text;

        if (Comma_Seperated_Items == "")
        {
            EnteredItemsXml.Append("<parentroot><root><gc>0</gc></root></parentroot>");
        }
        else
        {
            string[] EnteredItems = Comma_Seperated_Items.Split(new char[] { ',' });

            EnteredItemsXml.Append("<parentroot>");
            for (int i = 0; i <= EnteredItems.Length - 1; i++)
            {
                EnteredItemsXml.Append("<root><gc>");
                EnteredItemsXml.Append(EnteredItems[i].Trim());
                EnteredItemsXml.Append("</gc></root>");
            }
            EnteredItemsXml.Append("</parentroot>");
        }
        GCXML = EnteredItemsXml.ToString();
        //SelectedItemsXML = EnteredItemsXml.ToString();
        //return EnteredItemsXml.ToString();
    }


    public void Get_Not_Selected_Items()
    {
        StringBuilder EnteredItemsXml = new StringBuilder();
        string NotSelectedItems = "";

        string Comma_Seperated_Items = "";
        Comma_Seperated_Items = txt_GCNo.Text;

        DataRow dr = null;

        int i = 0;
        int j = 0;
        bool Found = false;
        int Dataset_count = 0;

        Dataset_count = SessionVoucherGrid.Rows.Count;
        if (Dataset_count > 0)
        {
            string[] EnteredItems = Comma_Seperated_Items.Split(new char[] { ',' });

            for (i = 0; i <= EnteredItems.Length - 1; i++)
            {
                Found = false;
                for (j = 0; j <= Dataset_count - 1; j++)
                {
                    dr = SessionVoucherGrid.Rows[j];
                    if (Util.String2Int(EnteredItems[i]) == Util.String2Int(dr["GC_No"].ToString()))
                    {
                        Found = true;
                        break;
                    }

                    if (j == Dataset_count - 1)
                    {
                        if (Found == false)
                        {
                            if (NotSelectedItems == string.Empty)
                            {
                                NotSelectedItems = EnteredItems[i];
                            }
                            else
                            {
                                NotSelectedItems = NotSelectedItems + "," + EnteredItems[i];
                            }
                        }
                    }
                }
            }

            txt_GCNotFound.Text = NotSelectedItems;
        }
        else
        {
            txt_GCNotFound.Text = txt_GCNo.Text;
        }
    }
    private void GetDetailsTableXML()
    {
        DataSet _objDs = new DataSet();
        int i;
        
        CheckBox chk_Attach;
        TextBox txt_Amount_Spent_Add, txt_Remark;
        DataRow _dr;
        _objDs.Tables.Add(SessionVoucherGrid.Clone());
        for (i = 0; i < dg_Voucher.Items.Count; i++)
        {
            chk_Attach = (CheckBox)dg_Voucher.Items[i].FindControl("chk_Attach");
            txt_Amount_Spent_Add = (TextBox)dg_Voucher.Items[i].FindControl("txt_Amount_Spent_Add");
            txt_Remark = (TextBox)dg_Voucher.Items[i].FindControl("txt_Remark");
            if (chk_Attach.Checked == true)
            {                
                _dr = _objDs.Tables[0].NewRow();
                _dr = SessionVoucherGrid.Rows[i];
                _dr["AmountSpent"] =Convert.ToDecimal(txt_Amount_Spent_Add.Text);
                _dr["Remark"] = txt_Remark.Text;
                _objDs.Tables[0].ImportRow(_dr);
                
            }
        }
        TotalGC = _objDs.Tables[0].Rows.Count;
        GetDetailsXML = _objDs.GetXml();
        //return _objDs.GetXml();
    }
    private bool CheckAmountSpent()
    {
        CheckBox chk_Attach;
        TextBox txt_Amount_Spent_Add, txt_Remark;
        int i;
        for (i = 0; i < dg_Voucher.Items.Count; i++)
        {
            chk_Attach = (CheckBox)dg_Voucher.Items[i].FindControl("chk_Attach");
            txt_Amount_Spent_Add = (TextBox)dg_Voucher.Items[i].FindControl("txt_Amount_Spent_Add");
            txt_Remark = (TextBox)dg_Voucher.Items[i].FindControl("txt_Remark");
            if (chk_Attach.Checked == true)
            {
                if(Util.String2Decimal(txt_Amount_Spent_Add.Text)<=0)
                {
                    errorMessage = "Amount Spent Should be Greater then Zero";
                    txt_Amount_Spent_Add.Focus();
                    return false;
                }
            }
        }
        return true;
    }
    public void ClearVariables()
    {
        SessionVoucherGrid = null;
    }
    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_GetLedgers.DataTextField = "Ledger_Name";
        ddl_GetLedgers.DataValueField = "Ledger_Id";
        SetStandardCaption();
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
      
        if (!IsPostBack)
        {
            
            if(VoucherType=="Door")
            {
                lbl_heading_name.Text = "DOOR DELIVERY VOUCHER";
                Page.Title = "DOOR DELIVERY VOUCHER";
            }
            else
            {
                lbl_heading_name.Text = "LOCAL CARTAGE VOUCHER";
                Page.Title = "LOCAL CARTAGE VOUCHER";
            }
            Get_Items_XML();            
            VoucherNo = ObjCommon.Get_Next_Number();
        }
        objDoorDelAndLocalCartVoucherPresenter = new DoorDelAndLocalCartVoucherPresenter(this, IsPostBack);
        Upd_Pnl_Grid.Update();
        Upd_Pnl_txt_GCNotFound.Update();
    }

    private void SetStandardCaption()
    {
        const int GCNoCaption = 2;
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        lbl_gc_no.Text = "Enter  " + hdn_GCCaption.Value + "  No(s):";
        lbl_gc_not_found.Text = hdn_GCCaption.Value + "  Not Found :";
        dg_Voucher.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No.";
        Panel1.GroupingText = hdn_GCCaption.Value + "  Details";
        lbl_TotalGCText.Text = "Total " + hdn_GCCaption.Value + " :";
    }

    protected void btn_Get_Details_Click(object sender, EventArgs e)
    {
        Get_Items_XML();
        objDoorDelAndLocalCartVoucherPresenter.FillGrid();
        Get_Not_Selected_Items();
        string script = "<script language='javascript'> " + "Txt_Enable();" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        GetDetailsTableXML();
        objDoorDelAndLocalCartVoucherPresenter.Save();
    }

    
  
  
    protected void dg_Voucher_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex!=-1)
        {
            TextBox txt_Amount_Spent_Add=(TextBox)e.Item.FindControl("txt_Amount_Spent_Add");
            CheckBox chk_Attach = (CheckBox)e.Item.FindControl("chk_Attach");
            txt_Amount_Spent_Add.Attributes.Add("onblur", "CheckAmountSpent('"+ txt_Amount_Spent_Add.ClientID  +"','"+ chk_Attach.ClientID +"')");
        }
    }
    #endregion
}
