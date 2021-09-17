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

public partial class Finance_Accounting_Vouchers_WucMRBookingDetails : System.Web.UI.UserControl,IMRBookingDetailsView
{
    MRBookingDetailsPresenter objMRBookingDetailsPresenter;
    MRCashChequePresenter objMRCashChequePresenter;
   
    Common objcommon = new Common();
    string Mode = "0";
    string _flag = "";

    #region Control Values

    public IMRCashChequeDetailsView MRCashChequeDetailsView 
    {
        get { return (IMRCashChequeDetailsView)WucMRCashChequeDetails1; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    public IMRGeneralDetailsView MRGeneralDetailsView 
    {
        get { return (IMRGeneralDetailsView)WucMRGeneralDetails1; }
    }
    
    public decimal RebookedCharges 
    {
        get { return Util.String2Decimal(txt_RebookedCharges.Text);}
        set { txt_RebookedCharges.Text = value.ToString();}
    }
    
    public decimal TotalReceivables 
    {
        get { return Util.String2Decimal(txt_TotalReceivables.Text); }
        set { txt_TotalReceivables.Text = value.ToString(); }
    }

    //public String MRChequeDetailsXML
    //{
    //    get
    //    {
    //        if (WucMRCashChequeDetails1.ChequeAmount <= 0)
    //        {
    //            return "<newdataset/>";
    //        }

    //        DataSet _objDs = new DataSet();
    //        DataTable dt = new DataTable();
    //        dt = MRCashChequeDetailsView.Session_ChequeDetailsGrid;
    //        _objDs.Tables.Add(dt.Copy());

    //        _objDs.Tables[0].TableName = "mrchequedetails";
    //        return _objDs.GetXml().ToLower();
    //    }
    //}


    //public int Document_Allocation_ID 
    //{
    //    set { hdn_Document_Allocation_ID.Value = value.ToString();}
    //    get { return Util.String2Int(hdn_Document_Allocation_ID.Value);} 
    //}
    
    //public int Start_No 
    //{
    //    set { hdn_Start_No.Value = value.ToString();}
    //    get { return Util.String2Int(hdn_Start_No.Value);} 
    //}
    
    //public int End_No 
    //{
    //    set { hdn_End_No.Value = value.ToString(); }
    //    get { return Util.String2Int(hdn_End_No.Value); } 
    //}
    
    //public int Next_No 
    //{
    //    set { hdn_Next_No.Value = value.ToString(); }
    //    get { return Util.String2Int(hdn_Next_No.Value); } 
    //}

    //public string Padded_Next_No
    //{
    //    set { hdn_Padded_Next_No.Value = value; }
    //    get { return hdn_Padded_Next_No.Value; }
    //}

    #endregion



    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value; ;
        }
    }

    public bool validateUI()
    {
        if (!WucMRGeneralDetails1.validateGeneralDetails(lbl_Errors))
        {
            return false;
        }
        else if (MRGeneralDetailsView.MRDate < Convert.ToDateTime(MRGeneralDetailsView.BookingDate))
        {
            lbl_Errors.Text = "MR date can't be less than Booking date";
            return false;
        }
        else if (Datemanager.IsValidProcessDate("MR_BKG", MRGeneralDetailsView.MRDate) == false)
        {
            errorMessage = "Please Select Valid MR Date";
            return false;
        }
        else if (!WucMRCashChequeDetails1.validateWUCChequeDetails(lbl_Errors))
        {
            return false;
        }
        //else if (WucMRCashChequeDetails1.CashAmount >= 20000)
        //{
        //    errorMessage = "Cash Amount Should Be Less Than 20000.";
        //    return false;
        //}
        else if (WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = "Please Enter Valid Cheque Amount";
            return false;
        }
        else
        {
            return true;
        }


    }

    #endregion
    #region Other Methods

    public void ClearVariables()
    {
        MRCashChequeDetailsView.Session_ChequeDetailsGrid = null;
        MRCashChequeDetailsView.Session_ddl_DepositIn = null;         
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WucMRGeneralDetails1.MRDate = DateTime.Now;
        }

        MRGeneralDetailsView.MR_Type_ID = 1;

        WucMRCashChequeDetails1.Scmcheque = ScriptManager1;
        objMRCashChequePresenter = new MRCashChequePresenter(WucMRCashChequeDetails1,IsPostBack);
        objMRBookingDetailsPresenter = new MRBookingDetailsPresenter(this, IsPostBack);

        btn_Save.Attributes.Add("onclick", objcommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Print, btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objcommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print,btn_Save, btn_Close));

         Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        //if (!IsPostBack)
        //{
          
        //    if (keyID <= 0)
        //    {
        //        Get_MR_No();
        //    }
        //}

       
        WucMRGeneralDetails1.OnGetDetails +=  new EventHandler(SetValues);
    
        

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objMRBookingDetailsPresenter.Save();
    }

    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        objMRBookingDetailsPresenter.Save();
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    public void SetValues(object sender,EventArgs e)
    {
        if (MRGeneralDetailsView.GCNo == string.Empty || MRGeneralDetailsView.GC_ID == 0)
        {
            txt_TotalReceivables.Text = "";
            MRCashChequeDetailsView.CashAmount = 0;
            MRCashChequeDetailsView.ChequeAmount = 0;
            //MRCashChequeDetailsView.hdn_pr_CashAmount = 0;
            //MRCashChequeDetailsView.hdn_pr_ChequeAmount = 0;
            MRGeneralDetailsView.Total_Receivables = 0;
            MRGeneralDetailsView.GC_ID = 0;
            MRCashChequeDetailsView.Session_ChequeDetailsGrid.Clear();
            MRCashChequeDetailsView.Bind_ChequeDetailsGrid = MRCashChequeDetailsView.Session_ChequeDetailsGrid;
            MRCashChequeDetailsView.Total_ChequeAmount = 0;

        }
        else
        {
            TotalReceivables = MRGeneralDetailsView.Total_Receivables;
        
        }

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    
    }

   

    //public void Get_MR_No()
    //{

    //    objcommon.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, 3);
    //    Document_Allocation_ID = _Document_Allocation_ID;
    //    Start_No = _Start_No;
    //    End_No = _End_No;
    //    Next_No = _Next_No;

    //    _Padded_Next_No = _Next_No.ToString("0000000");
    //    Padded_Next_No = _Padded_Next_No;
    //    WucMRGeneralDetails1.MRNo = Padded_Next_No;

    //    WucMRGeneralDetails1.Start_End_No = "(" + Start_No + " - " + End_No + ")";
    
    //}
}
