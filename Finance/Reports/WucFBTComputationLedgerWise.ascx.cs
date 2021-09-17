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
using Raj.FA.ReportsView;
using Raj.FA.ReportsPresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.FA;
using System.IO;


public partial class FA_Common_Reports_WucFBTComputationLedgerWise : System.Web.UI.UserControl,IFBTComputationLedgerWiseView
{
    #region ClassVariables
    FBTComputationLedgerWisePresenter objFBTComputationLedgerWisePresenter;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    #endregion

    #region ControlBind
   
    public DataTable BindFBTLedgerWiseGrid
    {
        set
        {
            dg_FBTComputationLedgerWise.DataSource = value;
            dg_FBTComputationLedgerWise.DataBind();
            
        }

    }
    #endregion

    #region ControlsValue
    public DateTime Start_Date
    {
        get { return WucStartEndDate1.Start_Date; }
        set { WucStartEndDate1.Start_Date = value; }
    }

    public DateTime End_Date
    {
        get { return WucStartEndDate1.End_Date; }
        set { WucStartEndDate1.End_Date = value; }
    }
    public string Hierarchy_Code
    {
        get { return HierarchyCode; }
        set { HierarchyCode = value; }
    }

    public int Main_Id
    {
        get { return MainId; }
        set { MainId = value; }
    }
    public Boolean Is_Consol
    {
        get { return IsConsol; }
        set { IsConsol = value; }
    }  
    public string FBTCategoryName
    {
        set { lbl_FBTCategoryName.Text = value; }
    }
    public string FBTSectionName
    {
        set { lbl_FBTSection.Text = value; }
    }

    public int FBTCategoryId
    {
        get { return Util.DecryptToInt(Request.QueryString["FBTCategoryId"]); }
        
    }

    ////public string FBTSection
    ////{
    ////    get { return Util.DecryptToInt(Request.QueryString["FBTSection"]); }
       
    ////}
    public string TotExpenditureAmount
    {
        set { lbl_TotExpenditureAmt.Text = value; }
    }
    public string TotNetExpenditure
    {
        set { lbl_TotNetExpenditure.Text = value; }
    }
    public string TotPercentageAsPerSec
    {
        set { lbl_TotPercentageAsPerSec.Text = value; }
    }
    public string TotValueOfFringeBenefit
    {
        set { lbl_TotValueOfFringeBenefit.Text = value; }
    }
    public string TotAmountRecovered
    {
        set { lbl_TotAmtRecovered.Text = value; }
    }
    public string TotalTax
    {
        set { lbl_TotalTax.Text = value; }
    }
    public string TotalFBT
    {
        set { lbl_TotalFBT.Text = value; }
    }
    public string TotEducationCess
    {
        set { lbl_EducationCess.Text = value; }
    }
    public string TotAddlEducationCess
    {
        set { lbl_AddlEducationCess.Text = value; }
    }
    public int Ledger_Id
    {

        get
        {
            int Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Id"]);
            return Id;
           
        }
    }
  
   
    #endregion
    #region IView
    public bool validateUI()
    {
        return true;
    }
    public int keyID
    {
        get { return ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
           lbl_Error.Text = value;
        }
    }

     #endregion
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        Is_Consol = Convert.ToBoolean(Request.QueryString["Is_Consol"]);
        WucStartEndDate1.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            Start_Date = Convert.ToDateTime(Request.QueryString["StartDate"]);
            End_Date = Convert.ToDateTime(Request.QueryString["EndDate"]);
        }

        objFBTComputationLedgerWisePresenter = new FBTComputationLedgerWisePresenter(this, IsPostBack);

    }
     #endregion

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objFBTComputationLedgerWisePresenter.FillLabelValues();
        objFBTComputationLedgerWisePresenter.GetValues();
    }
}
