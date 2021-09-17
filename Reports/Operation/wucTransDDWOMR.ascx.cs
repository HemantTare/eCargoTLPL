
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
using System.Drawing;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationModel;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;

using Raj.EC.ControlsView;


public partial class Reports_Operation_wucTransDDWOMR : System.Web.UI.UserControl 
{ 
     
    bool isValid = false;
    Raj.EC.Common objComm = new Raj.EC.Common();
    string _flag = "";
    string Mode = "0";

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["GC_Id"]); }
    }
    public string Flag
    {
        get { return _flag; }
    }
    #endregion

    #region InitInterface
  
    #endregion

    #region ControlsBind

    public String GC_No_For_Print
    {
        set { ViewState["_GC_No_For_Print"] = value; }
        get { return ViewState["_GC_No_For_Print"].ToString(); }
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
        }
        get
        {
            return Convert.ToDecimal(hdn_Total_GC_Amount.Value);
        }
    }
    

    public int ReceivedBy
    {
        get { return Util.String2Int(Rbl_Receivedby.SelectedValue); }
        set
        {
            Rbl_Receivedby.SelectedValue = value.ToString();
        }
    }
    public int Debit_To_Ledger_ID
    {
        get { return Util.String2Int(ddl_DebitTo.SelectedValue); }
    }

    public int Debit_To_Branch_ID
    {
        get { return Util.String2Int(ddl_BillingBranch.SelectedValue); }
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

    #endregion
    #region Function


    public bool validateUI()
    {
        isValid = false;
        TextBox Txt_DebitLedger, Txt_DebitBranch;
        errorMessage = "";

        Txt_DebitLedger = (TextBox)ddl_DebitTo.FindControl("txtBoxddl_DebitTo");
        Txt_DebitBranch = (TextBox)ddl_BillingBranch.FindControl("txtBoxddl_BillingBranch");

        if (GC_Id <= 0)
        {
            errorMessage = "Please Select one " + CompanyManager.getCompanyParam().GcCaption;
        } 
        else if (LHPO_Id <= 0)
        {
            errorMessage = "Please Select LHPO.";
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
            isValid = true;
        } 

        return isValid;
    }
    public void ClearVariables()
    {
        //MRCashChequeDetailsView.Session_ChequeDetailsGrid = null;
        //MRCashChequeDetailsView.Session_ddl_DepositIn = null;

    }
 

    private void SetStandardCaption()
    {        
        //lbl_GcNo.Text = "Enter " + CompanyManager.getCompanyParam().GcCaption + "  No:";
        //lbl_TotalGCAmount.Text = "Total " + CompanyManager.getCompanyParam().GcCaption + " Amount:";
        //lbl_LHPONo.Text= CompanyManager.getCompanyParam().LHPOCaption + "  No:";
        //lbl_LHPODate.Text= CompanyManager.getCompanyParam().LHPOCaption + " Date:";
        //lblLHPOFrom.Text= CompanyManager.getCompanyParam().LHPOCaption + " From:";
        //lbl_LHPOTo.Text = CompanyManager.getCompanyParam().LHPOCaption + " To:";        
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //if (Mode == "4")
        //{
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
          
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        GC_No_For_Print = Request.QueryString["GC_No_For_Print"].ToString();
        GC_Id = Convert.ToInt32(Request.QueryString["GC_Id"].ToString());
        LHPO_Id = Convert.ToInt32(Request.QueryString["LHPO_Id"].ToString());
        Memo_Id = Convert.ToInt32(Request.QueryString["Memo_Id"].ToString());
        Payment_Type_Id = Convert.ToInt32(Request.QueryString["Payment_Type_Id"].ToString());
        Total_Articles = Convert.ToInt32(Request.QueryString["Total_Articles"].ToString());
        Total_GC_Amount = Convert.ToDecimal(Request.QueryString["Total_GC_Amount"].ToString());

        On_Load();


        ddl_BillingBranch.Enabled = false;


        if (ReceivedBy == 1)
        {
            Set_DebitTo_LedgerID("", "0");
            Set_DebitTo_BranchID("", "0");

        }
        else
        {

        }
        //string Branch_Name = UserManager.getUserParam().MainName;
        //int Branch_Id = UserManager.getUserParam().MainId;
        //Set_DebitTo_BranchID(Branch_Name.ToString(), Branch_Id.ToString()); 

        if (!IsPostBack)
        {

            if (keyID <= 0)
            {


                hdn_gc_caption.Value = CompanyManager.getCompanyParam().GcCaption;
                hdn_lhpo_caption.Value = CompanyManager.getCompanyParam().LHPOCaption;
            }
        }

        SetStandardCaption();       
    }
    public void On_Load()
    {
        
       

        String scripts;
        scripts = "<script type='text/javascript' language='javascript'>" +
                    "  HideReceivedByControl();" +       
                    "</script>";

        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
         
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
         
    }

    private void disable_Textbox(TextBox txtbox1, TextBox txtbox2)
    {
        txtbox1.BackColor = Color.Transparent;
        txtbox1.BorderColor = Color.Transparent;
        txtbox1.ReadOnly = true;

        txtbox2.BackColor = Color.Transparent;
        txtbox2.BorderColor = Color.Transparent;
        txtbox2.ReadOnly = true;
    }
  
    
    public void Clear_Data()
    {
         
        //Payment_Type = "";
        //Payment_Type_Id = 0;
        //Total_GC_Amount = 0;
        //Booking_Articles = 0;
        //Booking_Articles_Weight = 0;
       
        //SetGC("0", "0");
       

     
        //LHPO_Id = 0; 
        //LHPO_To = "";

        //Memo_Id = 0;
        //Memo_No = ""; 
         
        //Remarks = "";
    } 
    
     
     
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
     
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
