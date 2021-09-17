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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Reports_Direct_Printing_Frm_BankRecoStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisplayReports();
    }

    private void DisplayReports()
    {
        DataSet ds = new DataSet();
        ds = (DataSet)HttpContext.Current.Session["FIN_DS"];

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(MapPath("~/Reports/Direct_Printing/FA_RPT/CR_BankRecoStatement.rpt"));
        this.Title = "Bank Reconcilation Statement";
        rptDoc.SetDataSource(ds);

        
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
