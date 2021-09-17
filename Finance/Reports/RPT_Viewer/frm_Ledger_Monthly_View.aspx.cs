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
using getCrystalReport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ClassLibrary;
using Raj.EC.Printers;
using System.Drawing.Printing;  


public partial class FA_Common_Reports_RPT_Viewer_frm_Ledger_Monthly_View : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, Document_ID, User_ID, GC_Count, Printer_ID = 0;
    string Paper_Size, Client_Code;

    String scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataSet ds = new DataSet();
        
        //string cmp_name;
        //ArrayList ParamList = new ArrayList();
        //ParamList.Add(Session["St_Dt"].ToString());
        //ParamList.Add(Session["End_Dt"].ToString());
        //ParamList.Add(Session["Ledger_Name"].ToString());
        //cmp_name = CompanyManager.getCompanyParam().CompanyName;

        //ParamList.Add(cmp_name);

        //ds = (DataSet)HttpContext.Current.Session["LM_DS"];

        ////ds.WriteXmlSchema("C:/XML_Schema_Filter_FA_Rpt_Ledger_Monthly.xsd");

        //ClsCrystalReport.ShowReport(CrystalReportViewer1, "~/Finance/Reports/RPT_Files/FA_Rpt_Ledger_Monthly.rpt", ds, ParamList, "");
        DataSet ds = new DataSet();
        ds = (DataSet)HttpContext.Current.Session["LM_DS"];
        //ds.WriteXmlSchema("C:/XML_Schema_Filter_FA_Rpt_Ledger_Monthly.Xsd");
        string Path;
        Path = "~/Finance/Reports/RPT_Files/FA_Rpt_Ledger_Monthly.rpt";
        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);

        string St_Dt, End_Dt, Ledger_Name, cmp_name;
        St_Dt = (string)Session["St_Dt"].ToString();
        End_Dt = (string)Session["End_Dt"].ToString();
        Ledger_Name = (string)Session["Ledger_Name"].ToString();
        cmp_name = CompanyManager.getCompanyParam().CompanyName;


        myReportDocument.SetParameterValue("St_Date", St_Dt);
        myReportDocument.SetParameterValue("End_Date", End_Dt);
        myReportDocument.SetParameterValue("Ledger_Name", Ledger_Name);
        myReportDocument.SetParameterValue("Cmp_name", cmp_name);

        Bind_To_Viewer();
    }

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            CrystalReportViewer1.ReportSource = myReportDocument;

            //myReportDocument.PrintOptions.PaperSize = pkSizeObj.Papersize;
            //myReportDocument.PrintOptions.PrinterName = pkSizeObj.PrinterName.ToString();
            //myReportDocument.PrintToPrinter(1, true, 1, 1);

            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
            CrystalReportViewer1.HasZoomFactorList = false;
            CrystalReportViewer1.HasSearchButton = false;
            CrystalReportViewer1.HasCrystalLogo = false;
            CrystalReportViewer1.HasDrillUpButton = false;
            CrystalReportViewer1.HasViewList = false;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;

        }
        catch (Exception ex)
        {
            //Destroy_objects();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Destroy_objects();
    }

    private void Destroy_objects()
    {
        CrystalReportViewer1.Dispose();
        CrystalReportViewer1 = null;
        myReportDocument.Close();
        myReportDocument.Dispose();
        myReportDocument = null;
        GC.Collect();
    }

}
