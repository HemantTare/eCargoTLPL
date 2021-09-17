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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_WucMRCashChequeDetails : System.Web.UI.UserControl,IMRCashChequeDetailsView
{
    MRCashChequePresenter objMRCashChequePresenter;
    TextBox txt_ChequeBank, txt_ChequeBranch, txt_ChequeNo, txt_chequeAmount;
    DropDownList ddl_depositIn;
    private ScriptManager _scm_cheque;
    ComponentArt.Web.UI.Calendar ChequeDate;
    public int _Menu_Item_ID = Common.GetMenuItemId();
    DataTable objDT;
    DataRow DR = null;
    Common objComm = new Common();
    bool isValid = false;    

    #region Control Values

    public ScriptManager Scmcheque
    {
        set {_scm_cheque = value;}
    }
    public decimal CashAmount 
    {
        get { return Util.String2Decimal(hdn_CashAmount.Value); }
        set { 
                txt_CashAmount.Text = value.ToString();
                hdn_CashAmount.Value = value.ToString();
            }
    }

    private decimal MaximumCashAmount
    {
        get { return Util.String2Decimal(hdn_Max_Cash_Amount.Value); }
        set { hdn_Max_Cash_Amount.Value = value.ToString(); }
    }

    private bool IsCashLimitValidationRequired
    {
        get { return Chk_IsMaxCashLimit_Required.Checked; }
        set { Chk_IsMaxCashLimit_Required.Checked = value; }
    }

    public decimal ChequeAmount 
    {
        get { return Util.String2Decimal(hdn_ChequeAmount.Value); }
        set {
                txt_ChequeAmount.Text = value.ToString();
                hdn_ChequeAmount.Value = value.ToString();
            }
    }

    public bool Is_AutoCalculate
    {
        get { return chk_Is_AutoCalculate.Checked;}
        set { chk_Is_AutoCalculate.Checked = value;}
    }

    public DataTable Bind_ddlCashLedger 
    {
        set 
        {
            if (CashLedgerID == 0)
            { }
            else
            {
                ddl_CashLedger.DataSource = value;
                ddl_CashLedger.DataTextField = "Ledger_Name";
                ddl_CashLedger.DataValueField = "Ledger_Id";
                ddl_CashLedger.DataBind();
                Raj.EC.Common.InsertItem(ddl_CashLedger);
                if (keyID <= 0)
                {
                    CashLedgerID = 1;
                }
            }
        }
    }

    public int CashLedgerID 
    {
        get {return Util.String2Int(ddl_CashLedger.SelectedValue) ;}
        set { ddl_CashLedger.SelectedValue = value.ToString();}
    }

    public DataTable Bind_ChequeDetailsGrid 
    {
        set 
        {
            Session_ChequeDetailsGrid = value;
            dg_ChequeDetails.DataSource = value;
            dg_ChequeDetails.DataBind();

            if (Session_ChequeDetailsGrid.Rows.Count > 0)
            {
                //Calculate_Total_Cheque_Amount();
                Total_ChequeAmount = Util.String2Decimal(Session_ChequeDetailsGrid.Compute("SUM(Cheque_Amount)","").ToString());
            }
            else
                Total_ChequeAmount = 0;
        }
    }
    
    public DataTable Session_ChequeDetailsGrid 
    {
        get { return StateManager.GetState<DataTable>("ChequeDetails");}
        set { StateManager.SaveState("ChequeDetails",value); }
    }
     
    public DataTable Session_ddl_DepositIn 
    {
        get { return StateManager.GetState<DataTable>("DepositIn"); }
        set { StateManager.SaveState("DepositIn", value); }
    }
    public DataTable Bind_ddlDepositIn 
    {
        set 
        { 
            ddl_depositIn.DataSource = value;
            ddl_depositIn.DataTextField = "Ledger_Name";
            ddl_depositIn.DataValueField = "Ledger_Id";
            ddl_depositIn.DataBind();

            Raj.EC.Common.InsertItem(ddl_depositIn);
        }
    }

    public int DepositInID 
    {
        get { return Util.String2Int(ddl_depositIn.SelectedValue) ;}
        set { ddl_depositIn.SelectedValue = value.ToString();}
    }

    public decimal Total_ChequeAmount 
    {
        get { return Util.String2Decimal(hdn_Total_Cheque_Amount.Value); }
        set { hdn_Total_Cheque_Amount.Value = value.ToString(); }
    }

    public String MRChequeDetailsXML
    {
        get
        {
            if (ChequeAmount <= 0)
            {
                return "<newdataset/>";
            }
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_ChequeDetailsGrid.Copy());

            _objDs.Tables[0].TableName = "mrchequedetails";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set{lbl_Errors.Text = value; }
    }

    public bool validateUI()
    {
        bool _isValid = false;

        if (CashLedgerID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_ddl_CashLedger").ToString();
            _scm_cheque.SetFocus(ddl_CashLedger);
            _isValid = false;
        }
        else if (CashAmount <= 0 && ChequeAmount <= 0 && Is_AutoCalculate == true)
        {
            errorMessage = "Please Enter Cash Amount Or Cheque Amount";
            _scm_cheque.SetFocus(txt_CashAmount);
            _isValid = false;
        }
        else if (ChequeAmount > 0)
        {
            if (Session_ChequeDetailsGrid.Rows.Count <= 0)
            {
                errorMessage = "Please Enter Atleast one record";
                _isValid = false;
            }
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_Menu_Item_ID == 113 || _Menu_Item_ID == 114 || _Menu_Item_ID == 202)
        {
            dg_ChequeDetails.Columns[4].HeaderText = "Bank Ledger";
        }

        if (StateManager.GetState<string>("QueryString") != null)
        {
            if (Util.String2Int(StateManager.GetState<string>("QueryString").ToString()) != 8) //8 means Credit Memo
            {
                objMRCashChequePresenter = new MRCashChequePresenter(this, IsPostBack);
            }
        }
        else
        {
            objMRCashChequePresenter = new MRCashChequePresenter(this, IsPostBack);
        }

        if (!IsPostBack)
        {
            Fill_Max_Cash_Amount();

            if (keyID <= 0)
            {
                Total_ChequeAmount = 0;
            }

            if (_Menu_Item_ID == 113 || _Menu_Item_ID == 114 || _Menu_Item_ID == 202)
            {
                dg_ChequeDetails.Columns[0].Visible = false;
                dg_ChequeDetails.Columns[1].Visible = false;
            }

            //if (_Menu_Item_ID == 106 || _Menu_Item_ID == 108)
            //{
            //    dg_ChequeDetails.Columns[5].Visible = false; //Cheque Date
            //}
        }

        up_grid.Update();

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }

    private void Fill_Max_Cash_Amount()
    {
        DataSet Ds = new DataSet();
        Ds = objComm.Get_Values_Where("EC_Master_Company_Parameters", "Maximum_Cash_Amount,Is_CashLimit_Validation_Req_On_MR", "", "Maximum_Cash_Amount", false);

        MaximumCashAmount = Util.String2Decimal(Ds.Tables[0].Rows[0]["Maximum_Cash_Amount"].ToString());
        IsCashLimitValidationRequired = Convert.ToBoolean(Ds.Tables[0].Rows[0]["Is_CashLimit_Validation_Req_On_MR"].ToString());
    }

    protected void dg_ChequeDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    Bind_ChequeDetailsGrid = Session_ChequeDetailsGrid;
                    dg_ChequeDetails.EditItemIndex = -1;
                    dg_ChequeDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                if (_Menu_Item_ID == 113 || _Menu_Item_ID == 114 || _Menu_Item_ID == 202)
                    errorMessage = "Duplicate Cheque No.";
                else
                    errorMessage = "Duplicate Bank Name and Cheque No."; ;
            }
        }
    }
    protected void dg_ChequeDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ChequeDetails.EditItemIndex = e.Item.ItemIndex;
        dg_ChequeDetails.ShowFooter = false;
        Bind_ChequeDetailsGrid = Session_ChequeDetailsGrid;
    }
    protected void dg_ChequeDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            //objDT = Session_ChequeDetailsGrid;

            //DataColumn[] _dtColumnPrimaryKey;
            //_dtColumnPrimaryKey = new DataColumn[2];
            //_dtColumnPrimaryKey[0] = objDT.Columns["bank_ledger_id"];
            //_dtColumnPrimaryKey[1] = objDT.Columns["cheque_no"];

            //objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_ChequeDetails.EditItemIndex = -1;
                dg_ChequeDetails.ShowFooter = true;

                Bind_ChequeDetailsGrid = Session_ChequeDetailsGrid;
            }
        }
        catch (ConstraintException)
        {
            if (_Menu_Item_ID == 113 || _Menu_Item_ID == 114 || _Menu_Item_ID == 202)
                errorMessage = "Duplicate Cheque No.";
            else
                errorMessage = "Duplicate Bank Name and Cheque No.";
        }
    }
    protected void dg_ChequeDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = Session_ChequeDetailsGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            Session_ChequeDetailsGrid = objDT;
            dg_ChequeDetails.EditItemIndex = -1;
            dg_ChequeDetails.ShowFooter = true;
            Bind_ChequeDetailsGrid = Session_ChequeDetailsGrid;
        }
    }
    protected void dg_ChequeDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ChequeDetails.EditItemIndex = -1;
        dg_ChequeDetails.ShowFooter = true;
        Bind_ChequeDetailsGrid = Session_ChequeDetailsGrid;
    }
    protected void dg_ChequeDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txt_ChequeBank = (TextBox)(e.Item.FindControl("txt_Chequebank"));
                txt_ChequeBranch = (TextBox)(e.Item.FindControl("txt_ChequeBranch"));
                txt_ChequeNo = (TextBox)(e.Item.FindControl("txt_ChequeNo"));
                txt_chequeAmount = (TextBox)(e.Item.FindControl("txt_ChequeAmount"));
                ddl_depositIn = (DropDownList)(e.Item.FindControl("ddl_DepositIn"));
                ChequeDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("wuc_ChequeDate"));
                ChequeDate.SelectedDate = DateTime.Now; 
                Bind_ddlDepositIn = Session_ddl_DepositIn;               
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = Session_ChequeDetailsGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                txt_ChequeBank.Text = DR["Cheque_Bank_Name"].ToString();
                txt_ChequeBranch.Text = DR["Cheque_Branch_Name"].ToString();
                txt_ChequeNo.Text = DR["Cheque_No"].ToString();
                txt_chequeAmount.Text = DR["Cheque_Amount"].ToString();
                DepositInID = Util.String2Int(DR["Bank_Ledger_ID"].ToString());
                ChequeDate.SelectedDate = Convert.ToDateTime(DR["Cheque_Date"]); 
            }
        }
    }


    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        txt_ChequeBank = (TextBox)(e.Item.FindControl("txt_Chequebank"));
        txt_ChequeBranch = (TextBox)(e.Item.FindControl("txt_ChequeBranch"));
        txt_ChequeNo = (TextBox)(e.Item.FindControl("txt_ChequeNo"));
        txt_chequeAmount = (TextBox)(e.Item.FindControl("txt_ChequeAmount"));
        ddl_depositIn = (DropDownList)(e.Item.FindControl("ddl_DepositIn"));
        ChequeDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("wuc_ChequeDate"));

        objDT = Session_ChequeDetailsGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update(source,e) == true)
        {
            DR["Cheque_Bank_Name"] = txt_ChequeBank.Text.Trim();
            DR["Cheque_Branch_Name"] = txt_ChequeBranch.Text.Trim();
            DR["Cheque_No"] = txt_ChequeNo.Text.Trim();
            DR["Cheque_Amount"] = Util.String2Decimal(txt_chequeAmount.Text.Trim());
            DR["Bank_Ledger_Name"] = ddl_depositIn.SelectedItem.Text;
            DR["Bank_Ledger_ID"] = Util.String2Int(ddl_depositIn.SelectedValue);
            DR["Cheque_Date"] = ChequeDate.SelectedDate.ToString("dd MMMM yyyy");

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            Session_ChequeDetailsGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        lbl_Errors.Text = "";

        if (txt_ChequeBank.Text.Trim() == string.Empty && (_Menu_Item_ID == 106 || _Menu_Item_ID == 108 || _Menu_Item_ID == 11131 || _Menu_Item_ID == 83))
        {
            errorMessage = GetLocalResourceObject("Msg_txt_ChequeBank").ToString();
            _scm_cheque.SetFocus(txt_ChequeBank);
        }
        else if (txt_ChequeBranch.Text.Trim() == string.Empty && (_Menu_Item_ID == 106 || _Menu_Item_ID == 108 || _Menu_Item_ID == 11131 || _Menu_Item_ID == 83))
        {
            errorMessage = GetLocalResourceObject("Msg_txt_ChequeBranch").ToString();
            _scm_cheque.SetFocus(txt_ChequeBranch);
        }
        else if (txt_ChequeNo.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_ChequeNo").ToString();
            _scm_cheque.SetFocus(txt_ChequeNo);
        }
        else if (txt_ChequeNo.Text.Length < 6)
        {
            errorMessage = "Cheque Number should not be less than Six Digit.";
            _scm_cheque.SetFocus(txt_ChequeNo);
        }
        else if (txt_chequeAmount.Text == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_ChequeAmount").ToString();
            _scm_cheque.SetFocus(txt_chequeAmount);
        }
        else if (Util.String2Decimal(txt_chequeAmount.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Valid_Cheque_Amount").ToString();
            _scm_cheque.SetFocus(txt_chequeAmount);
        }
        else if (Util.String2Int(ddl_depositIn.SelectedValue) == 0)
        {
            if (_Menu_Item_ID == 113)
                errorMessage = GetLocalResourceObject("Msg_ddl_Bankledger").ToString();
            else           
                errorMessage = GetLocalResourceObject("Msg_ddlDepositIn").ToString();

            _scm_cheque.SetFocus(ddl_depositIn);
        }
        else if (!Valid_CheckAmount(source, e))
        {
            errorMessage = GetLocalResourceObject("Msg_CompareChequeAmount").ToString();
        }
        else
        {
            isValid = true;
        }
        return isValid;
    }

    public bool Valid_CheckAmount(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        decimal Total_Amount = 0;

        if (Session_ChequeDetailsGrid.Rows.Count > 0)
        {
            foreach (DataRow dr in Session_ChequeDetailsGrid.Rows)
            {             
              Total_Amount = Total_Amount + Convert.ToDecimal(dr["Cheque_Amount"]);
            }        
        }

        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            if (e.CommandName == "Update")
            {
                Total_Amount = Total_Amount - Convert.ToDecimal(Session_ChequeDetailsGrid.Rows[e.Item.ItemIndex]["Cheque_Amount"]);
            }
            if (Util.String2Decimal(txt_chequeAmount.Text) > 0)
            {
                Total_Amount = Total_Amount + Util.String2Decimal(txt_chequeAmount.Text);
            }
        }

        if (Util.String2Decimal(txt_ChequeAmount.Text) < Total_Amount)
        {
            return false;
        }
        else
        {
            return true;
        }    
    }

    public bool validateWUCChequeDetails(Label lbl_Error)
    {
        bool _isValid = false;

        if (CashLedgerID <= 0 && (_Menu_Item_ID == 106 || _Menu_Item_ID == 108 || _Menu_Item_ID == 11131 || _Menu_Item_ID == 83) && CashAmount > 0)
        {
            lbl_Error.Text = GetLocalResourceObject("Msg_ddl_CashLedger").ToString();
            _scm_cheque.SetFocus(ddl_CashLedger);
            _isValid = false;
        }
        else if (CashAmount > 0 && (CashAmount > MaximumCashAmount && (_Menu_Item_ID == 113 || _Menu_Item_ID == 202)))
        {
            lbl_Error.Text = "Cash Amount Can't be greater than " + MaximumCashAmount;
            _isValid = false;
        }
        else if (CashAmount > 0 && (CashAmount > MaximumCashAmount && (_Menu_Item_ID == 106 || _Menu_Item_ID == 108 || _Menu_Item_ID == 11131 || _Menu_Item_ID == 83)) && IsCashLimitValidationRequired == true)
        {
            lbl_Error.Text = "Cash Amount Can't be greater than " + MaximumCashAmount;
            _isValid = false;
        }
        else if (CashAmount <= 0 && ChequeAmount <= 0 && Is_AutoCalculate == true)
        {
            lbl_Error.Text = GetLocalResourceObject("Msg_ddl_CashAmt_ChqAmt").ToString();
            _isValid = false;
        }
        else if (ChequeAmount > 0 && Session_ChequeDetailsGrid.Rows.Count <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Chqdetails").ToString();
            _isValid = false;
        }
        else if (ChequeAmount > 0 && ChequeAmount != Total_ChequeAmount)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Chqvalidation").ToString();
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
       
        return _isValid;
    }

    //public void Calculate_Total_Cheque_Amount()
    //{
    //    decimal Total_Amount = 0;
    //    if (Session_ChequeDetailsGrid.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in Session_ChequeDetailsGrid.Rows)
    //        {
    //            Total_Amount = Total_Amount + Convert.ToDecimal(dr["Cheque_Amount"]);
    //        }
    //    }
    //    Total_ChequeAmount = Total_Amount;       
    //}
}
