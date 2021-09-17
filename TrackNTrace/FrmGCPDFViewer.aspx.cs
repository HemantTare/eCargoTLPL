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
using Raj.EC.Printers;
using System.Drawing.Printing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class TrackNTrace_FrmGCPDFViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();
    string  Crypt;
    string GC_No;
    int GC_ID;
    

    protected void Page_Load(object sender, EventArgs e)
    {   
        Crypt  = Request.QueryString["GC_No"];
        GC_No = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["GC_ID"];
        GC_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);


        ds = Get_DataSet();
        

        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);

        Printer_Settings pkSizeObj = new Printer_Settings(0, "OMBharat_GC");

        myReportDocument.PrintOptions.PaperSize = pkSizeObj.Papersize;
     

        Bind_To_Viewer();

        /*********************** Enable This Code To Export it in PDF  *******************/
        myReportDocument.ExportToHttpResponse
        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, GC_No);

    }

    private DataSet Get_DataSet()
    {
        SqlParameter[] objSqlParam =  { objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, 30),
            objDAL.MakeInParams("@Document_ID", SqlDbType.Int, 0, GC_ID ),
        };
        objDAL.RunProc("dbo.EC_RPT_Direct_Printing_GC_OMBharat", objSqlParam, ref ds);
        return ds; 
    }
     

    private void Bind_To_Viewer()
    {
        //Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {  
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
            Response.Write(ex.Message);
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

        Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_GC_OMBharatPDF.rpt";

        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
       ////dsSchema.WriteXmlSchema("C:/XMLSchema_SDVTII_RPT_PracticalMarks_Certificate.xsd"); 
    }
     
}

