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



public partial class Finance_Accounting_Vouchers_FrmOnAccountReceipt : System.Web.UI.Page
{
    #region Declaration


    #endregion

    #region EventClick




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
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));


        if (IsPostBack == false)
        {
            dtpReceiptDate.SelectedDate = DateTime.Now;
            dtpBankDate.SelectedDate = DateTime.Now;

            FillBankPaymentType();
            FillBankLedger(UserManager.getUserParam().MainId);

            lbl_BankPaymentBy.Attributes.Add("style", "display:none");
            ddl_BankPaymentType.Attributes.Add("style", "display:none");
            lbl_CheckRefNo.Attributes.Add("style", "display:none");
            txt_RefNo.Attributes.Add("style", "display:none");
            td_ChequeDate.Attributes.Add("style", "display:none");
            txt_ReceiptBank.Attributes.Add("style", "display:none");
            lbl_ReceiptBank.Attributes.Add("style", "display:none");
            lbl_Deposited_Bank.Attributes.Add("style", "display:none");
            ddl_BankLedger.Attributes.Add("style", "display:none");

            txt_Client.Focus();
        }

        lst_Client.Style.Add("visibility", "hidden");


    }

    public void BindBankLedger(DataTable dt)
    {
        ddl_BankLedger.DataTextField = "Ledger_Name";
        ddl_BankLedger.DataValueField = "Ledger_ID";
        ddl_BankLedger.DataSource = dt;
        ddl_BankLedger.DataBind();
    }

    #endregion




    public void ClearVariables()
    {
        //ds = null;
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

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId) };
        objDAL.RunProc("FA_Mst_Fill_Bank_Ledger_For_Branch", objSqlParam, ref ds);

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
        //else if (Util.String2Decimal(hdn_ChequeAmount.Value) > 0 && txt_ReceiptBank.Text.ToString().Trim() == "")
        //{
        //    lbl_Error.Text = "Please Enter Received From Bank.";
        //    txt_ReceiptBank.Focus();
        //}
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



    private Message SaveReceipt()
    {

        DAL objDAL = new DAL();


        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar ,5,UserManager.getUserParam().HierarchyCode), 
            objDAL.MakeInParams("@MainId",SqlDbType.Int,0,UserManager.getUserParam().MainId), 
            objDAL.MakeInParams("@Client_ID",SqlDbType.Int,0,Util.String2Int(hdn_ClientId.Value )), 
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

        objDAL.RunProc("dbo.FA_Opr_CreditDebit_Customer_Receipt_From_Voucher_Save", objSqlParam);

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
