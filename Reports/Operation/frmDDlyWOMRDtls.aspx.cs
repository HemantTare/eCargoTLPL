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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

public partial class Reports_Operation_frmDDlyWOMRDtls : System.Web.UI.Page
{ 
    string Mode = "";
    private DAL objDAL = new DAL();
    public String Calendar_Img_Path;
    public string _setAttribute;

    #region ControlsBind

    public DateTime Transaction_Date
    {
        set
        {
            wucTransaction_Date.SelectedDate = value;
        }
        get { return wucTransaction_Date.SelectedDate; }
    }
    public String GC_No_For_Print
    {
        set
        { 
            ViewState["_GC_No_For_Print"] = value;
            txtlblLRNo.Text = value; 
        }
        get 
        { 
            return ViewState["_GC_No_For_Print"].ToString();
        }
    }

    public int GC_Id
    {
        set { ViewState["_GC_Id"] = value; }
        get { return Util.String2Int(ViewState["_GC_Id"].ToString()); }
    }

    public int LHPO_Id
    {
        set { ViewState["_LHPO_Id"] = value; }
        get { return Util.String2Int(ViewState["_LHPO_Id"].ToString()); }
    }

    public int Memo_Id
    {
        set { ViewState["_Memo_Id"] = value; }
        get { return Util.String2Int(ViewState["_Memo_Id"].ToString()); }
    }

    public int Payment_Type_Id
    {
        set { ViewState["_Payment_Type_Id"] = value; }
        get { return Util.String2Int(ViewState["_Payment_Type_Id"].ToString()); }
    }

    public int From_Service_Location_ID
    {
        set { ViewState["_From_Service_Location_ID"] = value; }
        get { return Util.String2Int(ViewState["_From_Service_Location_ID"].ToString()); }
    }

    public int To_Service_Location_ID
    {
        set { ViewState["_To_Service_Location_ID"] = value; }
        get { return Util.String2Int(ViewState["_To_Service_Location_ID"].ToString()); }
    }

    public string From_Service_Location
    {
        set 
        { 
            ViewState["_From_Service_Location"] = value;
            txtlblBkgBranch.Text = value; 
        }
        get { return ViewState["_From_Service_Location"].ToString(); }
    }

    public string To_Service_Location
    {
        set 
        { 
            ViewState["_To_Service_Location"] = value;
            txtlblDlyLocation.Text = value; 
        }
        get { return ViewState["_To_Service_Location"].ToString(); }
    }

    public int Vehicle_ID
    {
        set { ViewState["_Vehicle_ID"] = value; }
        get { return Util.String2Int(ViewState["_Vehicle_ID"].ToString()); }
    }

    public string Vehicle_No
    {
        set 
        {
            ViewState["_Vehicle_No"] = value;
            txtlblVehicleNo.Text = value; 
        }
        get { return ViewState["_Vehicle_No"].ToString(); }
    }

    public String GC_Date
    {
        set
        {
            ViewState["_GC_Date"] = value;
            txtlblLRDate.Text = value;
        }
        get { return ViewState["_GC_Date"].ToString(); }
    }

    public String Memo_Date
    {
        set
        { 
            ViewState["_Memo_Date"] = value;
            txtlblInvoiceDate.Text = value; 
        }
        get { return ViewState["_Memo_Date"].ToString(); }
    } 
 
    public String Memo_No_For_Print
    {
        set 
        { 
            ViewState["_Memo_No_For_Print"] = value;
            txtlblInvoiceNo.Text = value; 
        }
        get { return ViewState["_Memo_No_For_Print"].ToString(); }
    }
    
    public int Memo_Branch_Id
    {
        set { ViewState["_Memo_Branch_Id"] = value; }
        get { return Util.String2Int(ViewState["_Memo_Branch_Id"].ToString()); }
    }

    public String MemoFromBranch_Name
    {
        set
        { 
            ViewState["_MemoFromBranch_Name"] = value;
            txtlblInvoiceFrom.Text = value; 
        }
        get { return ViewState["_MemoFromBranch_Name"].ToString(); }
    }

    public String MemoTo_Name
    {
        set
        { 
            ViewState["_MemoTo_Name"] = value; 
            txtlblInvoiceTo.Text = value; 
        }
        get { return ViewState["_MemoTo_Name"].ToString(); }
    }  

    public String LHPO_NO_For_Print
    {
        set 
        { 
            ViewState["_LHPO_NO_For_Print"] = value;
        }
        get { return ViewState["_LHPO_NO_For_Print"].ToString(); }
    }

    public int Total_Articles
    {
        set
        {
            hdn_Total_Articles.Value = value.ToString();
        }
        get
        {
            return Convert.ToInt32(hdn_Total_Articles.Value);
        }
    }

    public decimal Total_GC_Amount
    {
        set
        {
            hdn_Total_GC_Amount.Value = value.ToString();
            txtlblTotalGCAmountValue.Text = value.ToString();
        }
        get
        {
            return Convert.ToDecimal(hdn_Total_GC_Amount.Value);
        }
    }

    public String Payment_Type
    {
        set
        {
            ViewState["_Payment_Type"] = value;
        }
        get { return ViewState["_Payment_Type"].ToString(); }
    } 

    public int ReceivedBy
    {
        get { return Util.String2Int(Rbl_Receivedby.SelectedValue); }
        set
        {
            Rbl_Receivedby.SelectedValue = value.ToString();
        }
    }
    public String ChequeNo
    {
        set
        {
            txtChequeNo.Text = value;
        }
        get { return txtChequeNo.Text; }
    }
    public DateTime ChequeDate
    {
        set
        {
            wucChequeDate.SelectedDate = value;
        }
        get { return wucChequeDate.SelectedDate; }
    }
    public String BankName
    {
        set
        {
            txtBankName.Text = value;
        }
        get { return txtBankName.Text; }
    } 

    public int Debit_To_Ledger_ID
    {
        get { return Util.String2Int(ddl_DebitTo.SelectedValue); }
    }

    public int Debit_To_Branch_ID
    {
        get { return Util.String2Int(ddl_BillingBranch.SelectedValue); }
    }
    
    public int BranchID
    {
        set { ViewState["_BranchID"] = value; }
        get { return Util.String2Int(ViewState["_BranchID"].ToString()); }
    }

    public String BranchName
    {
        set
        { 
            ViewState["_BranchName"] = value;
            txtlblBkgBranch.Text = value; 
        }
        get { return ViewState["_BranchName"].ToString(); }
    }
    public void Set_DebitTo_LedgerID(string text, string value)
    {
        ddl_DebitTo.DataTextField = "Ledger_Name";
        ddl_DebitTo.DataValueField = "Ledger_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_DebitTo);
    }

    public void Set_DebitTo_BranchID(string text, string value)
    {
        ddl_BillingBranch.DataTextField = "Branch_Name";
        ddl_BillingBranch.DataValueField = "Branch_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_BillingBranch);
    }


    public decimal TDSDeduction
    {
        set
        {
            txt_TDSDeduction.Text = value.ToString();
        }
        get
        {
            return Convert.ToDecimal(txt_TDSDeduction.Text);
        }
    }

    public decimal TotalReceivable
    {
        set
        {
            hdn_TotalReceivable.Value = value.ToString();
            lbl_TotalReceivable.Text = value.ToString();
        }
        get
        {
            return Convert.ToDecimal(hdn_TotalReceivable.Value);
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        String Base_Url;
        Base_Url = Util.GetBaseURL();
        Calendar_Img_Path = Base_Url + "/Images/btn_calendar.gif";
        wucTransaction_Date.Attributes.Add("Picker_OnSelectionChanged(picker)", "return SetDateToLable()");    

        GC_No_For_Print = Request.QueryString["GC_No_For_Print"].ToString();
        GC_Id = Convert.ToInt32(Request.QueryString["GC_Id"].ToString());
        LHPO_Id = Convert.ToInt32(Request.QueryString["LHPO_Id"].ToString());
        Memo_Id = Convert.ToInt32(Request.QueryString["Memo_Id"].ToString());
        Payment_Type_Id = Convert.ToInt32(Request.QueryString["Payment_Type_Id"].ToString());
        From_Service_Location_ID = Convert.ToInt32(Request.QueryString["From_Service_Location_ID"].ToString());
        To_Service_Location_ID = Convert.ToInt32(Request.QueryString["To_Service_Location_ID"].ToString());
        From_Service_Location = Request.QueryString["FromServiceLocation"].ToString();
        To_Service_Location = Request.QueryString["ToServiceLocation"].ToString();
        Vehicle_ID = Convert.ToInt32(Request.QueryString["Vehicle_ID"].ToString());
        Vehicle_No = Request.QueryString["Vehicle_No"].ToString();
        GC_Date = Request.QueryString["GC_Date"].ToString();
        Memo_Date = Request.QueryString["Memo_Date"].ToString();
        Memo_No_For_Print = Request.QueryString["Memo_No_For_Print"].ToString();
        Memo_Branch_Id = Convert.ToInt32(Request.QueryString["Memo_Branch_Id"].ToString());
        MemoFromBranch_Name = Request.QueryString["MemoFromBranch_Name"].ToString();
        MemoTo_Name = Request.QueryString["MemoTo_Name"].ToString();
        LHPO_NO_For_Print = Request.QueryString["LHPO_NO_For_Print"].ToString();
        Total_Articles = Convert.ToInt32(Request.QueryString["Total_Articles"].ToString());
        Total_GC_Amount = Convert.ToDecimal(Request.QueryString["Total_GC_Amount"].ToString());
        Payment_Type = Request.QueryString["Payment_Type"].ToString();
        BranchName = Request.QueryString["BranchName"].ToString();
        BranchID = Convert.ToInt32(Request.QueryString["BranchID"].ToString());

        //TDSDeduction = 0;
        TotalReceivable = Total_GC_Amount - TDSDeduction;

        Mode = Request.QueryString["Mode"].ToString();

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        On_Load();

    }
    public void On_Load()
    {
        if (!IsPostBack)
        {
            Transaction_Date = DateTime.Now;

            ChequeDate = DateTime.Now;
        }
        String scripts;
        scripts = "<script type='text/javascript' language='javascript'>" +
                    "  HideReceivedByControl();" +
                    "</script>";

        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
         

    }

    public bool validateUI()
    {
      
        bool _isValid = false;
        TextBox Txt_DebitLedger, Txt_DebitBranch;

        Txt_DebitLedger = (TextBox)ddl_DebitTo.FindControl("txtBoxddl_DebitTo");
        Txt_DebitBranch = (TextBox)ddl_BillingBranch.FindControl("txtBoxddl_BillingBranch");

        if (GC_Id <= 0)
        {
            errorMessage = "Invalid Record Updation";
        }
        else if (ReceivedBy == 3 && (txtChequeNo.Text == "" || txtChequeNo.Text.Length <=0))
        {
            errorMessage = "Please Enter Cheque Number";
            txtChequeNo.Focus();
            return false;
        }
        else if (ReceivedBy == 3 && (txtBankName.Text == "" || txtBankName.Text.Length <= 0))
        {
            errorMessage = "Please Enter Bank Name";
            txtBankName.Focus();
            return false;
        }  
        else if (ReceivedBy == 2 && Debit_To_Ledger_ID <= 0)
        {
            errorMessage = "Please Select Debit To Ledger";
            Txt_DebitLedger.Focus();
            return false;
        }
        else if (ReceivedBy == 2 && Debit_To_Branch_ID <= 0)
        {
            errorMessage = "Please Select Debit To Branch";
            Txt_DebitBranch.Focus();
            return false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    public Message Save()
    {
        Message objMessage = new Message();
        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@Transaction_Date", SqlDbType.DateTime, 0,Transaction_Date),
            objDAL.MakeInParams("@GC_ID", SqlDbType.Int,0,GC_Id),
            objDAL.MakeInParams("@Memo_ID", SqlDbType.Int, 0,Memo_Id),
            objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0,LHPO_Id),
            objDAL.MakeInParams("@Total_GC_Amount", SqlDbType.Decimal, 0,Total_GC_Amount),
            objDAL.MakeInParams("@TDSDeduction", SqlDbType.Decimal, 0,TDSDeduction ),
            objDAL.MakeInParams("@TotalReceivable", SqlDbType.Decimal, 0,TotalReceivable ),
            objDAL.MakeInParams("@ReceivedBy", SqlDbType.Int, 0,ReceivedBy),
            objDAL.MakeInParams("@ChequeNo", SqlDbType.VarChar, 0,ChequeNo),
            objDAL.MakeInParams("@ChequeDate", SqlDbType.DateTime, 0,ChequeDate),
            objDAL.MakeInParams("@BankName", SqlDbType.VarChar, 0,BankName), 
            objDAL.MakeInParams("@Debit_To_Ledger_ID", SqlDbType.Int, 0,Debit_To_Ledger_ID),
            objDAL.MakeInParams("@Billing_branch_id", SqlDbType.Int, 0,Debit_To_Branch_ID), 
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_RPT_DirectDlyWithoutMR_Save", objSqlParam);

        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");

        }
        else
        {
            lbl_Errors.Visible = true;
            lbl_Errors.Text = objMessage.message;
        
        }

        return objMessage;
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
