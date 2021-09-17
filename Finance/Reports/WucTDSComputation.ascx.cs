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

public partial class FA_Common_Reports_WucTDSComputation : System.Web.UI.UserControl,ITDSComputationView
{
    #region ClassVariables
    TDSComputationPresenter objTDSComputationPresenter;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    #endregion

    #region ControlBind
    public DataSet BindTDSGrid
    {
        set
        {
            dg_TDSComputation.DataSource = value;
            dg_TDSComputation.DataBind();
           
           
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
        get { return Util.String2Int(Request.QueryString["Id"]); }
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
            Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);
            WucStartEndDate1.OnDateChange += new EventHandler(FillOnDateChange);

            if (!IsPostBack)
            {
                Start_Date = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["StartDate"]));
                End_Date = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["EndDate"]));
            }
       
        objTDSComputationPresenter = new TDSComputationPresenter(this, IsPostBack);

    }
    #endregion

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objTDSComputationPresenter.FillGrid();
    }
}
