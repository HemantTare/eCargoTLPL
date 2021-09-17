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
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;
using ClassLibraryMVP;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_WucPartyRecieptVoucher : System.Web.UI.UserControl,IPartyRecieptVoucherView
{
    #region ClassVariables
    Common objCommon = new Common();
    PartyRecieptVoucherPresenter objPartyRecieptVoucherPresenter;

    #endregion

    #region Bill Grid Controls

    ClassLibrary.UIControl.DDLSearch ddl_RefNo;
    Label lbl_BillDate;
    TextBox txt_RefNo;
    TextBox txt_BillAmount;
    TextBox txt_TDSAmount;
    TextBox txt_FrieghtAmount;
    TextBox txt_ReceivedAmount;
    Label lbl_PendingAmount;
    Label lbl_AdjustmentAmount;
    DropDownList ddl_RefType;


    #endregion

    #region Other Grid Controls

    ClassLibrary.UIControl.DDLSearch ddl_OtherDeductionLedger;
    TextBox txt_Amount;

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        if (VoucherDate < UserManager.getUserParam().StartDate || VoucherDate > UserManager.getUserParam().EndDate)
        {
            errorMessage = "Please Enter Voucher Date in current Financial Year";
        }
        else if (CashBankLedgerID == 0)
        {
            errorMessage = "Please Select Cash Bank Ledger";
        }
        else if (AmountRecieved == 0)
        {
            errorMessage = "Please Enter Amount Received";
        }
        else if (IsBankApp(CashBankLedgerID) == true && ChequeNo == 0)
        {
            errorMessage = "Please Enter Cheque No";
        }
        //else if (IsBankApp(CashBankLedgerID) == true && ChequeBankName == "")
        //{
        //    errorMessage = "Please Enter Cheque Bank";
        //}
        else if (ClientLedgerID == 0)
        {
            errorMessage = "Please Enter Client Ledger";
        }
        else if (Table_Grid.Visible == true && Session_BillGrid.Rows.Count <= 0)
        {
            errorMessage = "Please Enter Bill Details";
        }
        else if ((TotalAdjustedAmount != (AmountRecieved + TotalDeduction)) && Table_Grid.Visible == true)
        {
            errorMessage = "(Total Adjusted Amount) Does Match (Amount Recieved) and (Total Deduction)";
        }
        else if (!validateOtherDetails())
        { }
        else if (objPartyRecieptVoucherPresenter.IsDupicateRefNo() == false)
        { }
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
        }
    }

    #endregion

    public string Mode
    {
        get { return Request.QueryString["Mode"]; }
    }

    public string Menu_Item_Id
    {
        get { return Request.QueryString["Menu_Item_Id"]; }
    }

    public decimal OtherAmount
    {
        get { return txt_Amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_Amount.Text); }
        set { txt_Amount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public int OtherLedgerId
    {
        get
        {
            return ddl_OtherDeductionLedger.SelectedValue == "" ? 0 : Convert.ToInt32(ddl_OtherDeductionLedger.SelectedValue);
        }
    }

    public string BillLedgerName
    {
        set
        {
            lbl_BillLegend.Text = value;
        }
    }

    public DateTime BillDate
    {
        get {
                if (ddl_RefNo.GetValueAt(3) != "")
                {
                    return Convert.ToDateTime(ddl_RefNo.GetValueAt(3));
                }
                else
                {
                    return Convert.ToDateTime(lbl_BillDate.Text);
                }
            }
        set { lbl_BillDate.Text = value.ToString(); }
    }

    public string ManualRefNo
    {
        get { return txt_RefNo1.Text.Trim();}
        set { txt_RefNo1.Text = value;}
    }

    public decimal BillAmount
    {
        get {
                if (ddl_RefNo.GetValueAt(2) != "")
                {
                    return Convert.ToDecimal(ddl_RefNo.GetValueAt(2));
                }
                else
                {
                    return Convert.ToDecimal(txt_BillAmount.Text);
                }
            }
        set { txt_BillAmount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public decimal TDSAmount
    {
        get { return txt_TDSAmount.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_TDSAmount.Text); }
        set { txt_TDSAmount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public decimal FrieghtAmount
    {
        get { return txt_FrieghtAmount.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_FrieghtAmount.Text); }
        set { txt_FrieghtAmount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public decimal ReceivedAmount
    {
        get { return txt_ReceivedAmount.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_ReceivedAmount.Text); }
        set { txt_ReceivedAmount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public decimal PendingAmount
    {
        get {
                if (ddl_RefNo.GetValueAt(1) != "")
                {
                    return Convert.ToDecimal(ddl_RefNo.GetValueAt(1));
                }
                else
                {
                    return Convert.ToDecimal(lbl_PendingAmount.Text);
                }
            }
        set { lbl_PendingAmount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public decimal AdjustedAmount
    {
        get { return lbl_AdjustmentAmount.Text == "" ? 0 : Convert.ToDecimal(lbl_AdjustmentAmount.Text); }
        set { lbl_AdjustmentAmount.Text = Convert.ToString(Math.Abs(value)); }
    }

    public string VoucherNo
    {
        set { txt_VoucherNo.Text = value; }
        get { return txt_VoucherNo.Text.Trim(); }
    }

    public DateTime VoucherDate
    {
        get { return dtp_VoucherDate.SelectedDate; }
        set { dtp_VoucherDate.SelectedDate = value; }
    }

    public int CashBankLedgerID
    {
        get { return ddl_CashBankLedger.SelectedValue == "" ? 0 : Convert.ToInt32(ddl_CashBankLedger.SelectedValue) ;}
    }

    public string ChequeBankName
    {
        get { return txt_ChequeBank.Text.Trim(); }
        set { txt_ChequeBank.Text = value; }
    }

    public int ChequeNo
    {
        get { return txt_ChequeNo.Text.Trim() == "" ? 0 : Convert.ToInt32(txt_ChequeNo.Text); }
        set { txt_ChequeNo.Text = value.ToString(); }
    }

    public DateTime ChequeDate
    {
        get { return Convert.ToDateTime(dtp_ChequeDate.SelectedDate); }
        set { dtp_ChequeDate.SelectedDate = value; }
    }

    public int ClientLedgerID
    {
        get { return ddl_ClientLedger.SelectedValue == "" ? 0 : Convert.ToInt32(ddl_ClientLedger.SelectedValue); }
    }

    public void SetCashBankLedger(string text, string value)
    {
        ddl_CashBankLedger.DataTextField = "Ledger_Name";
        ddl_CashBankLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_CashBankLedger);
    }

    public void SetClientLedger(string text, string value)
    {
        ddl_ClientLedger.DataTextField = "Ledger_Name";
        ddl_ClientLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_ClientLedger);
    }

    public DataTable Bind_BillGrid
    {
        set
        {
            Session_BillGrid = value;
            dg_BillGrid.DataSource = value;
            dg_BillGrid.DataBind();
            RefTypeId = 2;
            EventArgs e = new EventArgs();
            ddl_RefType_OnSelectedIndexChanged(ddl_RefType,e);
        }
    }

    public string GetClientBillDetailXML
    {
        get 
        {
            DataSet objDS = new DataSet();
            Session_BillGrid.TableName = "ClientBillDetail";
            objDS.Tables.Add(Session_BillGrid.Copy());
            objDS.AcceptChanges();

            if (objDS.Tables[0].Rows.Count <= 0)
            {
                return "<NewDataSet></NewDataSet>";
            }
            else
            {
                return objDS.GetXml();
            }
        }
    }

    public string GetOtherDeductionXML
    {
        get
        {
            DataSet objDS = new DataSet();
            Session_OtherGrid.TableName = "OtherDeduction";
            objDS.Tables.Add(Session_OtherGrid.Copy());
            objDS.AcceptChanges();

            if (objDS.Tables[0].Rows.Count <= 0)
            {
                return "<NewDataSet></NewDataSet>";
            }
            else
            {
                return objDS.GetXml();
            }
        }
    }

    public string GetBillByBillXML
    {
        get
        {
            DataSet objDS = new DataSet();
            SessionBillByBillDT.TableName = "BillByBill";
            Delete_UnwantedRows_in_SessionBillByBill(SessionBillByBillDT);
            objDS.Tables.Add(SessionBillByBillDT.Copy());
            objDS.AcceptChanges();

            if (objDS.Tables[0].Rows.Count <= 0)
            {
                return "<NewDataSet></NewDataSet>";
            }
            else
            {
                return objDS.GetXml();
            }

        }
    }

    public string GetCostCentreXML
    {
        get
        {
            DataSet objDS = new DataSet();
            SessionCostCentreDT.TableName = "CostCentre";
            Delete_UnwantedRows_in_SessionBillByBill(SessionCostCentreDT);
            objDS.Tables.Add(SessionCostCentreDT.Copy());
            objDS.AcceptChanges();

            if (objDS.Tables[0].Rows.Count <= 0)
            {
                return "<NewDataSet></NewDataSet>";
            }
            else
            {
                return objDS.GetXml();
            }
        }
    }

    public DataTable SessionBillByBillDT
    {
        set { StateManager.SaveState("VoucherBillByBill_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherBillByBill_DT"); }
    }

    public DataTable SessionDropDownRefType
    {
        set { StateManager.SaveState("SessionDropDownRefType_DT", value); }
        get { return StateManager.GetState<DataTable>("SessionDropDownRefType_DT"); }
    }

    public DataTable SessionCostCentreDT
    {
        set { StateManager.SaveState("VoucherCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherCostCentre_DT"); }
    }

    public DataTable SessionDropDownCostCentre
    {
        set { StateManager.SaveState("DropDownCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("DropDownCostCentre_DT"); }
    }

    public DataTable Session_BillGrid
    {
        set { StateManager.SaveState("VoucherBillGrid", value); }
        get { return StateManager.GetState<DataTable>("VoucherBillGrid"); }
    }

    public decimal AmountRecieved
    {
        get { return txt_AmountReceived.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_AmountReceived.Text); }
        set { txt_AmountReceived.Text = value.ToString(); }
    }

    public decimal TotalAdjustedAmount
    {
        get { return txt_TotalAdjustedAmount.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_TotalAdjustedAmount.Text); }
        set { txt_TotalAdjustedAmount.Text = value.ToString(); }
    }

    public DataTable Bind_OtherGrid
    {
        set
        {
            Session_OtherGrid = value;
            dg_OtherGrid.DataSource = value;
            dg_OtherGrid.DataBind();
        }
    }

    public DataTable Session_OtherGrid
    {
        set { StateManager.SaveState("VoucherOtherGrid", value); }
        get { return StateManager.GetState<DataTable>("VoucherOtherGrid"); }
    }

    public decimal TotalDeduction
    {
        get { return txt_TotalDeduction.Text.Trim() == "" ? 0 : Convert.ToDecimal(txt_TotalDeduction.Text); }
        set { txt_TotalDeduction.Text = value.ToString(); }
    }

    public string Narration
    {
        set { txt_Narration.Text = value; }
        get { return txt_Narration.Text.Trim(); }
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }

    public string RefNo
    {
        set
        {
            if (RefTypeId == 2)
            {
                Raj.EC.Common.SetValueToDDLSearch(value, value, ddl_RefNo);
            }
            else
            {
                txt_RefNo.Text = value;
            }
        }
        get
        {
            if (RefTypeId == 2)
            {
                return ddl_RefNo.SelectedItem;
            }
            else
            {
                return txt_RefNo.Text;
            }
        }
    }

    public DataTable Bind_ddl_RefType
    {
        set
        {
            ddl_RefType.DataTextField = "Ref_Type";
            ddl_RefType.DataValueField = "Ref_Type_Id";
            ddl_RefType.DataSource = value;
            ddl_RefType.DataBind();
        }
    }

    private int RefTypeId
    {
        get { return Convert.ToInt32(ddl_RefType.SelectedValue); }
        set { ddl_RefType.SelectedValue = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        objPartyRecieptVoucherPresenter = new PartyRecieptVoucherPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            Session["Bill_Voucher_Id"] = keyID.ToString();

            if (keyID <= 0)
            {
                VoucherNo = objCommon.Get_Next_Number();
                VoucherDate = DateTime.Now;
                Table_Grid.Visible = false;
                Table_Amount.Visible = false;
            }

            if (keyID > 0)
            {
                DAL objDal = new DAL();
                DataSet ds = new DataSet();
                ds = objDal.RunQuery("select dbo.IsMainLedgerBill(" + ClientLedgerID + ")");
                
                if (Convert.ToBoolean(ds.Tables[0].Rows[0][0]) == false)
                {
                    Table_Grid.Visible = false;
                    Table_Amount.Visible = false;
                    BillLedgerName = "";
                }

                Session["Bill_Ledger_Id"] = ClientLedgerID.ToString();
            }
                        
        }
    }

    protected void dg_BillGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            findControlsBillGrid(e.Item);
            Bind_ddl_RefType = SessionDropDownRefType;

            if (RefTypeId != 2)
            {
                lbl_AdjustmentAmount.Text = "0";
                lbl_AdjustmentAmount.Enabled = false;
            }
            else
            { lbl_AdjustmentAmount.Enabled = true; }

            ddl_RefNo.DataTextField = "Ref_No";
            ddl_RefNo.DataValueField = "Ref_No1";

            lbl_BillDate.Text = VoucherDate.ToString("dd MMM yyyy");
           
            

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                scm_Voucher.SetFocus(ddl_RefType);
                RefTypeId = Util.String2Int(Session_BillGrid.Rows[e.Item.ItemIndex]["Ref_Type_Id"].ToString());
                RefNo = Session_BillGrid.Rows[e.Item.ItemIndex]["Bill_No"].ToString();
                lbl_BillDate.Text = Convert.ToDateTime(Session_BillGrid.Rows[e.Item.ItemIndex]["Bill_Date"]).ToString("dd MMM yyyy");
                BillAmount = Util.String2Decimal(Session_BillGrid.Rows[e.Item.ItemIndex]["Bill_Amount"].ToString());
                PendingAmount = Util.String2Decimal(Session_BillGrid.Rows[e.Item.ItemIndex]["Pending_Amount"].ToString());
                AdjustedAmount = Util.String2Decimal(Session_BillGrid.Rows[e.Item.ItemIndex]["Adjustment_Amount"].ToString());

                if (RefTypeId != 2)
                {
                    lbl_AdjustmentAmount.Text = "0";
                    lbl_AdjustmentAmount.Enabled = false;
                }
                else
                { lbl_AdjustmentAmount.Enabled = true; }

                if (RefTypeId == 2)
                {
                    txt_BillAmount.Enabled = false;
                    lbl_AdjustmentAmount.Enabled = true;
                    txt_TDSAmount.Enabled = true;
                    txt_FrieghtAmount.Enabled = true;
                    txt_ReceivedAmount.Enabled = true;
                }
                else
                {
                    txt_BillAmount.Enabled = true;
                    lbl_AdjustmentAmount.Enabled = false;
                    txt_TDSAmount.Enabled = false;
                    txt_FrieghtAmount.Enabled = false;
                    txt_ReceivedAmount.Enabled = false;
                }
                
            }

            desableControls();
            hdf_GridControlID.Value = lbl_BillDate.ClientID + "Ö" + txt_BillAmount.ClientID + "Ö" +
                                      lbl_PendingAmount.ClientID + "Ö" + lbl_AdjustmentAmount.ClientID;
                                                  
         }
    }

    protected void dg_BillGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        findControlsBillGrid(e.Item);
        InsertUpdateBillGrid(e);
    }

    protected void dg_BillGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_BillGrid.EditItemIndex = -1;
        Bind_BillGrid = Session_BillGrid;
        dg_BillGrid.ShowFooter = true;
    }

    protected void dg_BillGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Session_BillGrid.Rows[e.Item.ItemIndex].Delete();
        Session_BillGrid.AcceptChanges();
        Bind_BillGrid = Session_BillGrid;

        TotalAdjustedAmount = Convert.IsDBNull(Session_BillGrid.Compute("Sum(Adjustment_Amount)", "")) == true ? 0 : Convert.ToDecimal(Session_BillGrid.Compute("Sum(Adjustment_Amount)", ""));
        string FilterString = "Ref_Type_Id <> 2";
        TotalAdjustedAmount = TotalAdjustedAmount + (Convert.IsDBNull(Session_BillGrid.Compute("Sum(Bill_Amount)", FilterString)) == true ? 0 : Convert.ToDecimal(Session_BillGrid.Compute("Sum(Bill_Amount)", FilterString)));
        scm_Voucher.SetFocus(ddl_RefType);
    }

    protected void dg_BillGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        findControlsBillGrid(e.Item);
        dg_BillGrid.EditItemIndex = e.Item.ItemIndex;
        Bind_BillGrid = Session_BillGrid;
        dg_BillGrid.ShowFooter = false;
        scm_Voucher.SetFocus(ddl_RefType);
     }

    protected void dg_BillGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        findControlsBillGrid(e.Item);

        if (e.CommandName == "Add")
        {
            InsertUpdateBillGrid(e);
        }
    }

    private void findControlsBillGrid(DataGridItem item)
    {
        ddl_RefNo = (ClassLibrary.UIControl.DDLSearch)item.FindControl("ddl_RefNo");
        ddl_RefType = (DropDownList)item.FindControl("ddl_RefType");
        txt_RefNo = (TextBox)item.FindControl("txt_RefNo");
        txt_BillAmount = (TextBox)item.FindControl("txt_BillAmount");
        lbl_PendingAmount = (Label)item.FindControl("lbl_PendingAmount");
        lbl_AdjustmentAmount = (Label)item.FindControl("lbl_AdjustmentAmount");
        lbl_BillDate = (Label)item.FindControl("lbl_BillDate");
        txt_TDSAmount = (TextBox)item.FindControl("txt_TDSAmount");
        txt_FrieghtAmount = (TextBox)item.FindControl("txt_FrieghtAmount");
        txt_ReceivedAmount = (TextBox)item.FindControl("txt_ReceivedAmount");
    }

    private void findControlsOtherGrid(DataGridItem item)
    {
        ddl_OtherDeductionLedger = (ClassLibrary.UIControl.DDLSearch)item.FindControl("ddl_OtherDeductionLedger");
        txt_Amount = (TextBox)item.FindControl("txt_Amount");
    }

    protected void ddl_RefType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RefType = (DropDownList)sender;
        DataGridItem item = (DataGridItem)ddl_RefType.Parent.Parent;
        findControlsBillGrid(item);

       if (RefTypeId == 2)
       {
           txt_BillAmount.Enabled = false;
           lbl_AdjustmentAmount.Enabled = true;
           txt_TDSAmount.Enabled = true;
           txt_FrieghtAmount.Enabled = true;
           txt_ReceivedAmount.Enabled = true;
       }
       else
       {
           txt_BillAmount.Enabled = true;
           lbl_AdjustmentAmount.Enabled = false;
           txt_TDSAmount.Enabled = false;
           txt_FrieghtAmount.Enabled = false;
           txt_ReceivedAmount.Enabled = false;
       }

        //if (RefTypeId != 2)
        //{ txt_AdjustmentAmount.Enabled = false; }
        //else
        //{ txt_AdjustmentAmount.Enabled = true; }

        ddl_RefNo.DataTextField = "Ref_No";
        ddl_RefNo.DataValueField = "Ref_No1";

        
        RefNo = "";
        BillAmount = 0;
        PendingAmount = 0;
        TDSAmount = 0;
        FrieghtAmount = 0;
        ReceivedAmount = 0;
        AdjustedAmount = 0;
        

        desableControls();
        scm_Voucher.SetFocus(ddl_RefType);
    }

    private void desableControls()
    {
       ddl_RefNo.OtherColumns = ClientLedgerID.ToString() + "Ö" + keyID.ToString();

        if (RefTypeId == 2)  //Agst Ref
        {
            ddl_RefNo.Visible = true;
            txt_RefNo.Visible = false;
        }
        else
        {
            ddl_RefNo.Visible = false;
            txt_RefNo.Visible = true;
        }
    }

    protected void ddl_RefNo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        DataGridItem item = (DataGridItem)txt.Parent.Parent.Parent;
        DataSet ds = new DataSet();

        findControlsBillGrid(item);

        if (RefNo != "")
        {
            ds = objPartyRecieptVoucherPresenter.SetCreditDaysAmount();

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow Dr = ds.Tables[0].Rows[0];
                lbl_BillDate.Text = Convert.ToDateTime(Dr["Bill_Date"]).ToString("dd MMM yyyy");
                BillAmount = Convert.ToDecimal(Dr["NewRefAmount"].ToString());
                PendingAmount = Convert.ToDecimal(Dr["Amount"].ToString());
                //AdjustedAmount = Convert.ToDecimal(Dr["Amount"].ToString());
            }
            else
            {
                BillAmount = 0;
                PendingAmount = 0;
                AdjustedAmount = 0;
            }
        }
        ddl_RefNo.OtherColumns = ClientLedgerID.ToString() + "Ö" + keyID.ToString();
        ddl_RefNo.Focus();
    }

    protected void ddl_ClientLedger_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DAL objDal = new DAL();
        DataSet ds = new DataSet();

            ds = objDal.RunQuery("select dbo.IsMainLedgerBill(" + ClientLedgerID + ")");

            Session_BillGrid.Clear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToBoolean(ds.Tables[0].Rows[0][0]) == false)
                {
                    Table_Grid.Visible = false;
                    Table_Amount.Visible = false;
                    BillLedgerName = "";
                }
                else
                {
                    Table_Grid.Visible = true;
                    Table_Amount.Visible = true;
                    BillLedgerName = "Bill Details for " + ddl_ClientLedger.SelectedText + " :";
                }
            }
           

        TotalAdjustedAmount = 0;
        Bind_BillGrid = Session_BillGrid;

        if (ClientLedgerID == 0)
        {
            Session_BillGrid.Clear();

            Table_Grid.Visible = false;
            Table_Amount.Visible = false;
            BillLedgerName = "Bill Details :";

            TotalAdjustedAmount = 0;
            Bind_BillGrid = Session_BillGrid;
        }

        Session["Bill_Ledger_Id"] = ClientLedgerID.ToString();
    }


    private void InsertUpdateBillGrid(DataGridCommandEventArgs e)
    {
        findControlsBillGrid(e.Item);

        if (RefTypeId == 2)
        {
            ddl_RefNo.OtherColumns = ClientLedgerID.ToString() + "Ö" + keyID.ToString();
        }

        if (ValidateBillGridValues() == false)
        {            
            return; 
        }

        DataTable objDT = Session_BillGrid;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[e.Item.ItemIndex];
        }


        try
        {
            objDR["Bill_No"] = RefNo;
            objDR["Ref_Type"] = ddl_RefType.SelectedItem.Text;
            objDR["Ref_Type_Id"] = RefTypeId;
            objDR["Bill_Amount"] = BillAmount.ToString("0.00");
            objDR["Pending_Amount"] = PendingAmount.ToString("0.00");
            objDR["TDS_Deduction_Amount"] = TDSAmount.ToString("0.00");
            objDR["Frieght_Amount"] = FrieghtAmount.ToString("0.00");
            objDR["Received_Amount"] = ReceivedAmount.ToString("0.00");

            decimal Amount = 0;
            Amount = TDSAmount + FrieghtAmount + ReceivedAmount;
            objDR["Adjustment_Amount"] = Amount.ToString("0.00");
            objDR["Bill_Date"] = RefTypeId != 2 ? VoucherDate.ToString("dd MMM yyyy") : BillDate.ToString("dd MMM yyyy");
                      

            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(objDR);
            }

            if (e.CommandName == "Update")
            {
                dg_BillGrid.EditItemIndex = -1;
                dg_BillGrid.ShowFooter = true;
            }

            objDT.AcceptChanges();
            Bind_BillGrid = objDT;

        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Bill No";
        }

        string FilterString = "Ref_Type_Id <> 2";
        TotalAdjustedAmount = Convert.IsDBNull(Session_BillGrid.Compute("Sum(Adjustment_Amount)", "")) == true ? 0 : Convert.ToDecimal(Session_BillGrid.Compute("Sum(Adjustment_Amount)", ""));
        TotalAdjustedAmount = TotalAdjustedAmount + (Convert.IsDBNull(Session_BillGrid.Compute("Sum(Bill_Amount)", FilterString)) == true ? 0 : Convert.ToDecimal(Session_BillGrid.Compute("Sum(Bill_Amount)", FilterString)));
        scm_Voucher.SetFocus(ddl_RefType);
    }


    private bool ValidateBillGridValues()
    {
        lbl_BillDate.Text = BillDate.ToString("dd MMM yyyy");
        PendingAmount = PendingAmount;
        BillAmount = BillAmount;

        bool _isValid = false;
        if (RefNo.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Bill No";

            if (txt_RefNo.Visible == true)
            {
                scm_Voucher.SetFocus(txt_RefNo);
            }
            else
            {
                scm_Voucher.SetFocus(ddl_RefNo);
            }
            
        }
        else if (BillAmount == 0)
        {
            errorMessage = "Please Enter Bill Amount";
            scm_Voucher.SetFocus(txt_BillAmount);
        }
        else if (ReceivedAmount == 0 && TDSAmount == 0 && FrieghtAmount == 0 && RefTypeId == 2)
        {
            errorMessage = "Please Enter Adjusted Amount";
            scm_Voucher.SetFocus(txt_ReceivedAmount);
        }
        else
        {
            _isValid = true;
        }
        //up_Error.Update();
        return _isValid;
    }


    private void InsertUpdateOtherGrid(DataGridCommandEventArgs e)
    {
        findControlsOtherGrid(e.Item);

        if (ValidateOtherGridValues() == false)
        { return; }

        DataTable objDT = Session_OtherGrid;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[e.Item.ItemIndex];
        }


        try
        {
            DataSet Ds = objPartyRecieptVoucherPresenter.GetLedgerParam();
            DataRow Dr = Ds.Tables[0].Rows[0];

            objDR["IsBillByBill"] = Convert.ToBoolean(Dr["IsBillByBill"]);
            objDR["IsCostCentre"] = Convert.ToBoolean(Dr["IsCostCentre"]);

            objDR["OtherLedgerId"] = OtherLedgerId;
            objDR["OtherLedgerName"] = ddl_OtherDeductionLedger.SelectedText;
            objDR["Amount"] = OtherAmount.ToString("0.00");

            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(objDR);
            }

            if (e.CommandName == "Update")
            {
                dg_OtherGrid.EditItemIndex = -1;
                dg_OtherGrid.ShowFooter = true;
            }

            objDT.AcceptChanges();
            Bind_OtherGrid = objDT;

        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Other Deduction Ledger";
        }

        TotalDeduction = Convert.IsDBNull(Session_OtherGrid.Compute("Sum(Amount)", "")) == true ? 0 : Convert.ToDecimal(Session_OtherGrid.Compute("Sum(Amount)", ""));
       
    }


    private bool ValidateOtherGridValues()
    {
        bool _isValid = false;
        if (OtherLedgerId == 0)
        {
            errorMessage = "Please Enter Other Deduction Ledger";
            scm_Voucher.SetFocus(ddl_OtherDeductionLedger);
        }
        else if (OtherAmount == 0)
        {
            errorMessage = "Please Enter Other Deduction Ledger Amount";
            scm_Voucher.SetFocus(txt_Amount);
        }
        else
        {
            _isValid = true;
        }

        //up_Error.Update();
        return _isValid;
    }

    protected void dg_OtherGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        findControlsOtherGrid(e.Item);
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateOtherGrid(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_OtherGrid.EditItemIndex = e.Item.ItemIndex;
            Bind_OtherGrid = Session_OtherGrid;
            dg_OtherGrid.ShowFooter = false;
        }

        else if (e.CommandName == "Cancel")
        {
            dg_OtherGrid.EditItemIndex = -1;
            Bind_OtherGrid = Session_OtherGrid;
            dg_OtherGrid.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
        {
            Session_OtherGrid.Rows[e.Item.ItemIndex].Delete();
            Session_OtherGrid.AcceptChanges();
            Bind_OtherGrid = Session_OtherGrid;
            TotalDeduction = Convert.IsDBNull(Session_OtherGrid.Compute("Sum(Amount)", "")) == true ? 0 : Convert.ToDecimal(Session_OtherGrid.Compute("Sum(Amount)", ""));
        }

        ddl_OtherDeductionLedger.Focus();

    }

    protected void dg_OtherGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            LinkButton lnk_CostCentre = (LinkButton)e.Item.FindControl("lnk_CostCentre");
            LinkButton lnk_BillbyBill = (LinkButton)e.Item.FindControl("lnk_BillbyBill");

            int LedgerID = Convert.ToInt32(Session_OtherGrid.Rows[e.Item.ItemIndex]["OtherLedgerId"]);
            string LedgerName = Session_OtherGrid.Rows[e.Item.ItemIndex]["OtherLedgerName"].ToString();
            string VoucherType = "";
            decimal Dr = Convert.ToDecimal(Session_OtherGrid.Rows[e.Item.ItemIndex]["Amount"]);
            decimal Cr = Convert.ToDecimal(Session_OtherGrid.Rows[e.Item.ItemIndex]["Amount"]);
            string CrDr = "Cr";
            
            string qryString = "Ledger_Id=" + Util.EncryptInteger(LedgerID) + "&Voucher_Id=" + Util.EncryptInteger(keyID) + "&Ledger_Name=" + Util.EncryptString(LedgerName) + "&Voucher_Type=" + Util.EncryptString(VoucherType) + "&Credit=" + Util.EncryptDecimal(Cr) + "&Debit=" + Util.EncryptDecimal(Dr) + "&CrDr=" + Util.EncryptString(CrDr) + "&Mode=" + Mode + "&Menu_Item_Id=" + Menu_Item_Id;  

            lnk_CostCentre.OnClientClick = " return OpenPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmVoucherCostCentre.aspx?" + qryString + "')";
            lnk_BillbyBill.OnClientClick = "return OpenPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmVoucherBillByBill.aspx?" + qryString + "')";
        }

        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            findControlsOtherGrid(e.Item);

            ddl_OtherDeductionLedger.DataTextField = "OtherLedgerName";
            ddl_OtherDeductionLedger.DataValueField = "OtherLedgerId";

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                Raj.EC.Common.SetValueToDDLSearch(Session_OtherGrid.Rows[e.Item.ItemIndex]["OtherLedgerName"].ToString(), Session_OtherGrid.Rows[e.Item.ItemIndex]["OtherLedgerId"].ToString(), ddl_OtherDeductionLedger);
                OtherAmount = Util.String2Decimal(Session_OtherGrid.Rows[e.Item.ItemIndex]["Amount"].ToString());
            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objPartyRecieptVoucherPresenter.Save();
    }

    private bool IsBankApp(int Ledger_Id)
    {
        DAL objDal = new DAL();
        DataSet ds = new DataSet();
        ds = objDal.RunQuery("select dbo.CheckIsBankAppl("+ CashBankLedgerID +")");

        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
    }

    private bool validateOtherDetails()
    {
        foreach (DataRow Dr in Session_OtherGrid.Rows)
        {
            DataView Dv;
            string filterStr = "Ledger_Id=" + Dr["OtherLedgerId"].ToString();

            if (Convert.ToBoolean(Dr["IsCostCentre"]))
            {
                if (convertToDecimal(Dr["Amount"]) > 0 && convertToDecimal(SessionCostCentreDT.Compute("Sum(Amount)", filterStr)) != convertToDecimal(Dr["Amount"]))
                {
                    errorMessage = "Please Enter Cost Centre Details for Ledger  '" + Dr["OtherLedgerName"].ToString() + "'";
                    return false;
                }
            }
            else if (Convert.ToBoolean(Dr["IsBillByBill"]))
            {
                if (convertToDecimal(Dr["Amount"]) > 0 && Math.Abs(convertToDecimal(SessionBillByBillDT.Compute("Sum(Amount)", filterStr))) != convertToDecimal(Dr["Amount"]))
                {
                    errorMessage = "Please Enter Bill By Bill Details for Ledger  '" + Dr["OtherLedgerName"].ToString() + "'";
                    return false;

                }
            }
        }
        //up_Error.Update();
        return true;
    }


    protected void On_VoucherDateChange(object sender, EventArgs e)
    {
        foreach (DataRow dr in Session_BillGrid.Rows)
        {
            if (Convert.ToInt32(dr["Ref_Type_Id"]) != 2)
            {
                dr["Bill_Date"] = VoucherDate.ToString("dd MMM yyyy");
            }
        
        }

        Session_BillGrid.AcceptChanges();
        Bind_BillGrid = Session_BillGrid;
        
    }

    public void Delete_UnwantedRows_in_SessionBillByBill(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            int exist = 0;

            foreach (DataRow dr1 in Session_OtherGrid.Rows)
            {
                if (Convert.ToInt32(dr["Ledger_Id"]) == Convert.ToInt32(dr1["OtherLedgerId"]))
                {
                    exist = 1;
                    break;
                }
            }

            if (exist == 0)
            {
                dr.Delete();
            }
        }
    }

    protected void ddl_OtherDeductionLedger_TxtChange(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        DataGridItem item = (DataGridItem)txt.Parent.Parent.Parent;

        findControlsOtherGrid(item);
        ddl_OtherDeductionLedger.Focus();
    }
}
