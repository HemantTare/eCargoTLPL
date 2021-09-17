using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using ClassLibraryMVP.General;



public partial class Reports_CL_Nandwana_UserDesk_FrmUserDeskCreditDebitCustomerBalancePendingBillsNew : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    string TotalUnAdjusted, TotalToBeAdjusted;

    #endregion

    #region EventClick



    public int Client_Id
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["ClientId"]);
        }
    }

    public int Ledger_Id
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Ledger_Id"]);
        }
    }

    public DataTable SessionBindGridOnAccount
    {
        get { return StateManager.GetState<DataTable>("OnAccountDetails"); }
        set { StateManager.SaveState("OnAccountDetails", value); }
    }

    public DataTable SessionBindGridPendingReferences
    {
        get { return StateManager.GetState<DataTable>("PendingReferencesDetails"); }
        set { StateManager.SaveState("PendingReferencesDetails", value); }
    }

    private string Bank_Ledger_ID
    {
        set
        {

            ddl_BankLedger.SelectedValue = value;
        }
        get
        {
            return ddl_BankLedger.SelectedValue;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            dtpReceiptDate.SelectedDate = DateTime.Now;
            dtpBankDate.SelectedDate = DateTime.Now;


            lbl_ClientName.Text = Request.QueryString["ClientName"];


            FillBankPaymentType();
            FillBankLedger(Client_Id);

            Common objcommon = new Common();

            BindGrid("form_and_pageload", e);

            tr_References.Attributes.Add("style", "display:none");
            tr_ReferencesSave.Attributes.Add("style", "display:none");
            tr_ReferencesTotal.Attributes.Add("style", "display:none");

            lbl_BankPaymentBy.Attributes.Add("style", "display:none");
            ddl_BankPaymentType.Attributes.Add("style", "display:none");
            lbl_CheckRefNo.Attributes.Add("style", "display:none");
            txt_RefNo.Attributes.Add("style", "display:none");
            td_ChequeDate.Attributes.Add("style", "display:none");
            txt_ReceiptBank.Attributes.Add("style", "display:none");
            lbl_ReceiptBank.Attributes.Add("style", "display:none");
            lbl_Deposited_Bank.Attributes.Add("style", "display:none");
            ddl_BankLedger.Attributes.Add("style", "display:none");

        }

    }

    public void BindBankLedger(DataTable dt)
    {
        ddl_BankLedger.DataTextField = "Ledger_Name";
        ddl_BankLedger.DataValueField = "Ledger_ID";
        ddl_BankLedger.DataSource = dt;
        ddl_BankLedger.DataBind();
    }

    protected void dg_OnAccountAdjustment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            //Label lbl_TotalPendingAmount;

            //lbl_TotalPendingAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalPendingAmount");

            //lbl_TotalPendingAmount.Text = TotalUnAdjusted.ToString();

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            TextBox txtReceivedAmount = (TextBox)(e.Item.FindControl("txtReceivedAmount"));
            TextBox txtTDS = (TextBox)(e.Item.FindControl("txtTDS"));
            TextBox txtFrtDeduction = (TextBox)(e.Item.FindControl("txtFrtDeduction"));
            TextBox txtClaimDeduction = (TextBox)(e.Item.FindControl("txtClaimDeduction"));
            TextBox txtPendingAmount = (TextBox)(e.Item.FindControl("txtPendingAmount"));

            if (Util.String2Int(SessionBindGridPendingReferences.Rows[e.Item.ItemIndex]["Att"].ToString()) == 0)
            {
                txtReceivedAmount.Enabled = false;
                txtTDS.Enabled = false;
                txtFrtDeduction.Enabled = false;
                txtClaimDeduction.Enabled = false;

            }
            else
            {
                txtReceivedAmount.Enabled = true;
                txtTDS.Enabled = true;
                txtFrtDeduction.Enabled = true;
                txtClaimDeduction.Enabled = true;
            }



            HiddenField hdn_ReceivedAmount = (HiddenField)(e.Item.FindControl("hdn_ReceivedAmount"));
            HiddenField hdn_TDS = (HiddenField)(e.Item.FindControl("hdn_TDS"));
            HiddenField hdn_FrtDeduction = (HiddenField)(e.Item.FindControl("hdn_FrtDeduction"));
            HiddenField hdn_ClaimDeduction = (HiddenField)(e.Item.FindControl("hdn_ClaimDeduction"));
            HiddenField hdn_TDPercent = (HiddenField)(e.Item.FindControl("hdn_TDPercent"));



            txtReceivedAmount.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_ReceivedAmount('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

            txtTDS.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_TDS('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

            txtFrtDeduction.Attributes.Add("txtbox_onlostfocus(this); onblur", "Onblur_FrtDeduction('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

            txtClaimDeduction.Attributes.Add("txtbox_onlostfocus(this); onblur", "Onblur_ClaimDeduction('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

        }
    }


    protected void dg_Adjusted_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            //Label lblTotalBillAmount;

            //lblTotalBillAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalBillAmount");

            //lblTotalBillAmount.Text = TotalToBeAdjusted.ToString();

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            TextBox txtAdjustedAmount = (TextBox)(e.Item.FindControl("txtAdjustedAmount"));


            if (Util.String2Int(SessionBindGridOnAccount.Rows[e.Item.ItemIndex]["Att"].ToString()) == 0)
            {
                txtAdjustedAmount.Enabled = false;
            }
            else
            {
                txtAdjustedAmount.Enabled = true;
            }

        }
    }



    #endregion



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);



        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,Client_Id),
            objDAL.MakeInParams("@LedgerID", SqlDbType.Int, 0,Ledger_Id)
        };

        objDAL.RunProc("FA_Opr_CreditDebit_Customer_Balance_Bill_Details_New", objSqlParam, ref ds);

        calculate_totals();


        SessionBindGridPendingReferences = ds.Tables[0];
        SessionBindGridOnAccount = ds.Tables[1];



        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_OnAccountAdjustment, ds.Tables[0], CallFrom, lbl_Error);

        objcommon.ValidateReportForm(dg_Adjusted, ds.Tables[1], CallFrom, lbl_Error);


    }


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[2].Rows[0];
        TotalUnAdjusted = dr["TotalUnAdjusted"].ToString();
        TotalToBeAdjusted = dr["TotalToBeAdjusted"].ToString();

        lbl_TotalBillAmunt.Text = TotalToBeAdjusted;
        lbl_TotalPendingAmount.Text = TotalUnAdjusted;
    }


    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    private int PaymentByID
    {

        get { return Util.String2Int(ddl_BankPaymentType.SelectedValue); }
        set
        {
            ddl_BankPaymentType.SelectedValue = Util.Int2String(value);
        }
    }

    private void FillBankPaymentType()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();

        objDAL.RunProc("EC_Mst_Fill_Bank_Payment_Type", ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Bind_BankPaymentType(ds.Tables[0]);
        }
    }

    private void FillBankLedger(int ClientId)
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,Client_Id)};
        objDAL.RunProc("FA_Mst_Fill_Bank_Ledger", objSqlParam , ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            BindBankLedger(ds.Tables[0]);
        }
    }


    public void Bind_BankPaymentType(DataTable dt)
    {
        ddl_BankPaymentType.DataTextField = "PaymentBy";
        ddl_BankPaymentType.DataValueField = "PaymentByID";
        ddl_BankPaymentType.DataSource = dt;
        ddl_BankPaymentType.DataBind();

    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (dtpReceiptDate.SelectedDate < UserManager.getUserParam().StartDate || dtpReceiptDate.SelectedDate > UserManager.getUserParam().EndDate)

        {
            lbl_Error.Text = "Invalid Date! Select Date Within Financial Year.";
            dtpReceiptDate.Focus();
        }

        else if (dtpReceiptDate.SelectedDate > DateTime.Now)
        {
            lbl_Error.Text = "Invalid Date! Postdated Date Not Allowed.";
            dtpReceiptDate.Focus();
        }

        else if (Util.String2Decimal(hdn_TotalReceipt.Value) <= 0)
        {
            lbl_Error.Text = "Total Receipt Amount Can Not Be Zero";
            txt_CashAmount.Focus();
        }
        else if (Util.String2Decimal(hdn_ChequeAmount.Value) > 0 && txt_RefNo.Text.ToString().Trim() == "")
        {
            lbl_Error.Text = "Please Enter Bank Ref No.";
            txt_RefNo.Focus();
        }
        else if (Util.String2Decimal(hdn_ChequeAmount.Value) > 0 && txt_ReceiptBank.Text.ToString().Trim() == "")
        {
            lbl_Error.Text = "Please Enter Received From Bank.";
            txt_ReceiptBank.Focus();
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }


    public bool validateUIAdjustment()
    {
        bool ATS;
        ATS = false;

        if (Util.String2Decimal(hdn_TotalToBeAdjusted.Value) <= 0)
        {
            lbl_Error.Text = "Please Select Any One Reference From OnAccount";
        }
        else if (Util.String2Decimal(hdn_TotalRecTDSFrtDeduction.Value) <= 0)
        {
            lbl_Error.Text = "Please Select Any One Reference From Pending Refrences";
        }
        else if (Util.String2Decimal(hdn_TotalRecTDSFrtDeduction.Value) != Util.String2Decimal(hdn_TotalToBeAdjusted.Value))
        {
            lbl_Error.Text = "Amount in OnAccount References And Adjusted References Dose Not Match";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    protected void btn_Save_Receipt_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            SaveReceipt();
        }
    }


    private void SetSessionDetailsFromGrid()
    {

        int i;

        CheckBox chk_Adjusted;
        TextBox txtAdjustedAmount;

        if (dg_Adjusted.Items.Count > 0)
        {
            for (i = 0; i <= dg_Adjusted.Items.Count - 1; i++)
            {

                chk_Adjusted = (CheckBox)dg_Adjusted.Items[i].FindControl("chk_Adjusted");
                txtAdjustedAmount = (TextBox)dg_Adjusted.Items[i].FindControl("txtAdjustedAmount");

                SessionBindGridOnAccount.Rows[i]["Att"] = chk_Adjusted.Checked;
                SessionBindGridOnAccount.Rows[i]["AdjustedAmount"] = txtAdjustedAmount.Text == "" ? "0" : txtAdjustedAmount.Text;  

            }
        }

        CheckBox chk_Unadjusted;
        TextBox txtReceivedAmount, txtTDS, txtFrtDeduction, txtClaimDeduction;

        if (dg_OnAccountAdjustment.Items.Count > 0)
        {
            for (i = 0; i <= dg_OnAccountAdjustment.Items.Count - 1; i++)
            {

                chk_Unadjusted = (CheckBox)dg_OnAccountAdjustment.Items[i].FindControl("chk_Unadjusted");
                txtReceivedAmount = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtReceivedAmount");
                txtTDS = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtTDS");
                txtFrtDeduction = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtFrtDeduction");
                txtClaimDeduction = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtClaimDeduction");


                SessionBindGridPendingReferences.Rows[i]["Att"] = chk_Unadjusted.Checked;
                SessionBindGridPendingReferences.Rows[i]["ReceivedAmount"] = txtReceivedAmount.Text == "" ? "0" : txtReceivedAmount.Text;
                SessionBindGridPendingReferences.Rows[i]["TDSAmount"] = txtTDS.Text == "" ? "0" : txtTDS.Text;
                SessionBindGridPendingReferences.Rows[i]["FrtDeduction"] = txtFrtDeduction.Text == "" ? "0" : txtFrtDeduction.Text;
                SessionBindGridPendingReferences.Rows[i]["ClaimDeduction"] = txtClaimDeduction.Text == "" ? "0" : txtClaimDeduction.Text;  
            }
        }


    }


    public String DetailsXMLOnAccount
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridOnAccount.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridOnAccount";
            return _objDs.GetXml().ToLower();
        }
    }

    public String DetailsXMLPendingReferences
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridPendingReferences.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridPendingReferences";
            return _objDs.GetXml().ToLower();
        }
    }

    protected void btn_SaveAdjustment_Click(object sender, EventArgs e)
    {
        SetSessionDetailsFromGrid();

        if (validateUIAdjustment())
        {
            SaveAdjustment();
        }
    }

    private Message SaveAdjustment()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int ,0,UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Ledger_ID",SqlDbType.Int ,0,Ledger_Id ), 
            objDAL.MakeInParams("@DetailsXMLOnAccount",SqlDbType.Xml,40000,DetailsXMLOnAccount),
            objDAL.MakeInParams("@DetailsXMLPendingReferences",SqlDbType.Xml,40000,DetailsXMLPendingReferences),
            objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.FA_Opr_CreditDebit_Customer_Adjustment_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {


            String popupScript = "";
            string _Msg = "Saved SuccessFully";

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(341).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&DecryptUrl='No'");

            //System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lbl_Error.Text = objMessage.message;
        }

        return objMessage;
    }


    private Message SaveReceipt()
    {

        DAL objDAL = new DAL();


        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Client_ID",SqlDbType.Int,0,Client_Id), 
            objDAL.MakeInParams("@Ledger_ID",SqlDbType.Int,0,Ledger_Id), 
            objDAL.MakeInParams("@Voucher_Date",SqlDbType.DateTime,0,dtpReceiptDate.SelectedDate),
            objDAL.MakeInParams("@CashAmount",SqlDbType.Decimal ,0,Util.String2Decimal(txt_CashAmount.Text)),
            objDAL.MakeInParams("@ChequeAmount",SqlDbType.Decimal ,0,Util.String2Decimal(txt_ChequeAmount.Text)),
            objDAL.MakeInParams("@BankPaymentById",SqlDbType.Int  ,0,ddl_BankPaymentType.SelectedValue),
            objDAL.MakeInParams("@BankRefNo",SqlDbType.VarChar,30,txt_RefNo.Text),
            objDAL.MakeInParams("@BankDate",SqlDbType.DateTime,0,dtpBankDate.SelectedDate),
            objDAL.MakeInParams("@Bank",SqlDbType.VarChar ,100,txt_ReceiptBank.Text),
            objDAL.MakeInParams("@DepositedInBank",SqlDbType.Int  ,0,Bank_Ledger_ID),
            objDAL.MakeInParams("@TotalReceiptAmount",SqlDbType.Decimal ,0,Util.String2Decimal(hdn_TotalReceipt.Value)),
            objDAL.MakeInParams("@Narration",SqlDbType.VarChar ,250,txt_Narration.Text),
            objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.FA_Opr_CreditDebit_Customer_Receipt_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";


            Response.Redirect("~/Display/CloseForm.aspx");

        }
        else
        {
            lbl_Error.Text = objMessage.message;
        }

        return objMessage;
    }

}
