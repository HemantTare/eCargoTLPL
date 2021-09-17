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


public partial class Operations_GTLB_Loading_Frm_Rpt_GTLB_Loading_FORM_5_Viewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    int Printer_ID = 0, Month, Year;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Month  = Convert.ToInt32(Request.QueryString["Month"]);
        Year = Convert.ToInt32(Request.QueryString["Year"]);
       

        ds = Get_DataSet(); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds); 
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet()
    {

        SqlParameter[] objSqlParam ={ 
        objDAL.MakeInParams("@Month", SqlDbType.Int ,0,Month),
        objDAL.MakeInParams("@Year", SqlDbType.Int ,0,Year)};

        objDAL.RunProc("EC_Rpt_GTLB_Loading_FORM_5", objSqlParam, ref ds);
            
        return ds;
    }     

    private void Bind_To_Viewer()
    {

        try
        {

            //myReportDocument.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

            myReportDocument.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
            myReportDocument.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

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

        Path = "~/Operations/GTLB Loading/EC_Rpt_GTLB_Loading_FORM_5.rpt";
        
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {

        //dsSchema.WriteXmlSchema("C:/EC_Rpt_GTLB_Loading_FORM_5.xsd");
           
    }
}

