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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using Raj.EC.Printers;
//using System.Drawing.Printing;

public partial class Operations_GTLB_Loading_Frm_Rpt_GTLB_Loading_FORM_1_Viewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    int Printer_ID = 0;
    DateTime LoadingDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        LoadingDate  =Convert.ToDateTime(Request.QueryString["LoadingDate"]);

        ds = Get_DataSet(LoadingDate); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds); 
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet( DateTime LoadingDate)
    {

        SqlParameter[] objSqlParam ={ 
        objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,LoadingDate)};

        objDAL.RunProc("EC_Rpt_GTLB_LoadingDetails_DateWise_FORM_1", objSqlParam, ref ds);
            
        return ds;
    }     

    private void Bind_To_Viewer()
    {
        //Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, "A4");

        try
        {

            //myReportDocument.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

            myReportDocument.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
            myReportDocument.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
            myReportDocument.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

            CrystalReportViewer1.ReportSource = myReportDocument;

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

    private string Get_Crystal_Report()
    {
        string Path = "";

        Path = "~/Operations/GTLB Loading/EC_Rpt_GTLB_Loading_FORM_1.rpt";
        
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {

        //dsSchema.WriteXmlSchema("C:/EC_Rpt_GTLB_Loading_FORM_1.xsd");
           
    }
}

