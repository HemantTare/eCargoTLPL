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


public partial class FA_Common_Reports_WucFBTComputation : System.Web.UI.UserControl,IFBTComputationView
{
    #region ClassVariables
    FBTComputationPresenter objFBTComputationPresenter;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    #endregion

    #region ControlBind

    //public DataTable SessionFBTComputationGrid
    //{
    //    get { return StateManager.GetState<DataTable>("FBTComputationGrid"); }
    //    set { StateManager.SaveState("FBTComputationGrid", value); }
    //}
    public DataTable BindFBTGrid
    {
        set
        {
            dg_FBTComputation.DataSource = value;
            dg_FBTComputation.DataBind();
           
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


    public string FringeBenefitPercent
    {
        set { lbl_FringeBenefitPercent.Text = value; }
    }
    public string FringeBenefitAmount
    {
        set { lbl_FringeBenefitAmount.Text = value; }
    }
    public string SurchargePercent
    {
        set { lbl_SurchargePercent.Text = value; }
    }
    public string SurchargeAmount
    {
        set { lbl_SurchargeAmount.Text = value; }
    }
    public string EducationCessPercent
    {
        set { lbl_EducationCessPercent.Text = value; }
    }
    public string EducationCessAmount
    {
        set { lbl_EducationCessAmt.Text = value; }
    }
    public string AddlEducationCessPercent
    {
        set { lbl_AddEducationCessPercent.Text = value; }
    }
    public string AddlEducationCessAmount
    {
        set { lbl_AddEducationCessAmt.Text = value; }
    }
    public string TotExpenditureAmount
    {
        set { lbl_TotExpenditureAmt.Text = value; }
    }
    public string TotAmountRecovered
    {
        set { lbl_TotAmtRecovered.Text = value; }
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
    public string TotalTaxPayable
    {
        set { lbl_TotalTaxPayableAmt.Text = value; }
    }


    #endregion

    #region IView
    public bool validateUI()
    {
        bool _Is_Valid = true;
        return _Is_Valid;
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    #endregion

    #region GridEvents     

    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        WucStartEndDate1.OnDateChange += new EventHandler(FillOnDateChange);
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);


        if (!IsPostBack)
        {
            Start_Date = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["StartDate"]));
            End_Date = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["EndDate"]));
        }
        objFBTComputationPresenter = new FBTComputationPresenter(this, IsPostBack);

    }
    #endregion

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objFBTComputationPresenter.GetValues();
       
    }
}
