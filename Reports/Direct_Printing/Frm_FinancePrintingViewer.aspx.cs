using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using getCrystalReport;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using Raj.EC;

public partial class Reports_Direct_Printing_Frm_FinancePrintingViewer : System.Web.UI.Page
{
    int Menu_Item_Id;
    string From_Date, To_Date, Loc_Name;
    DataSet ds1 = new DataSet();
    Common cs = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
           Menu_Item_Id = Convert.ToInt32(Request.QueryString["Menu_Item_Id"]);
           From_Date = Util.DecryptToString(Request.QueryString["Start_Date"]);
           To_Date = Util.DecryptToString(Request.QueryString["End_Date"]);     
           Bind_To_Viewer();    
        //}
    }

    private void Bind_To_Viewer()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)HttpContext.Current.Session["FIN_DS"];
            if (ds !=null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ReportDocument rptDoc = new ReportDocument();
                
		if (Request.QueryString["GRPSUMM"] != null)
                {
                    rptDoc.Load(MapPath("~/Reports/Direct_Printing/FA_RPT/CR_Trail_BalanceSheet_Grp_Summ.rpt"));
                    this.Title = "Trial Balance Group Summary";
                }
                else if (Convert.ToInt32(Menu_Item_Id) == 53) // Balance Sheet
                {
                    rptDoc.Load(MapPath("~/Reports/Direct_Printing/FA_RPT/BalanceSheet.rpt"));
                    this.Title = "Balance Sheet";     
                }
                else if (Convert.ToInt32(Menu_Item_Id) == 78) // Profit And Loss
                {
                    rptDoc.Load(MapPath("~/Reports/Direct_Printing/FA_RPT/CR_Profit_And_Loss.rpt"));
                    this.Title = "Profit And Loss";
                }
                else if (Convert.ToInt32(Menu_Item_Id) == 47) // Trial Balance
                {
                    rptDoc.Load(MapPath("~/Reports/Direct_Printing/FA_RPT/CR_Trail_BalanceSheet.rpt"));
                    this.Title = "Trial Balance";
                }
                else if (Convert.ToInt32(Menu_Item_Id) == 349) // Branch Wise Expenses
                {
                    rptDoc.Load(MapPath("~/Reports/Direct_Printing/FA_RPT/CR_BranchWise_Expenses.rpt"));
                    this.Title = "Branch Wise Expenses";
                }

                ds1 = cs.EC_Common_Pass_Query("Exec EC_RPT_BRANCH_ONLY '" + Session["HierarchyCode"] + "','" + Session["MainId"] + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Loc_Name = ds1.Tables[0].Rows[0]["Branch_Name"].ToString(); 
                }
                else
                {
                    Loc_Name = "All";
                }

                rptDoc.SetDataSource(ds);
                rptDoc.SetParameterValue("CompanyName", CompanyManager.getCompanyParam().CompanyName);
                rptDoc.SetParameterValue("FromDate", From_Date);
                rptDoc.SetParameterValue("ToDate", To_Date);
                if (Convert.ToInt32(Menu_Item_Id) != 349)
                {
                    rptDoc.SetParameterValue("Loc_Name", Loc_Name);
                }
                if (Request.QueryString["GRPSUMM"] != null)
                {
                    rptDoc.SetParameterValue("SummaryForGroup", Request.QueryString["GRPSUMM"]);
                }
                CrystalReportViewer1.ReportSource = rptDoc;                            
                CrystalReportViewer1.DataBind();
                
                CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                CrystalReportViewer1.HasZoomFactorList = false;
                CrystalReportViewer1.HasSearchButton = false;
                CrystalReportViewer1.HasCrystalLogo = false;
                CrystalReportViewer1.HasDrillUpButton = false;
                CrystalReportViewer1.HasViewList = false;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
        }
        catch (Exception ex)
        {
            //Destroy_objects();
        }
    }
}
