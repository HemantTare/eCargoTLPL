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

public partial class Printing_Frm_LR_PDF_Creation_Viewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();
    string Crypt, DocumentNo, GCNos;
    bool IsDocumentNoWise;
    int DocumentTypeId;


    protected void Page_Load(object sender, EventArgs e)
    {


        IsDocumentNoWise = Util.String2Bool(Request.QueryString["IsDocumentNoWise"]);

        DocumentTypeId = Util.String2Int(Request.QueryString["DocumentTypeId"]);

        DocumentNo = Request.QueryString["DocumentNo"];

        GCNos =Request.QueryString["GCNos"];
        


        ds = Get_DataSet();
        

        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
    

        Bind_To_Viewer();

        /*********************** Enable This Code To Export it in PDF  *******************/

        string PDFFileName;

        if (IsDocumentNoWise == true)
        {
            PDFFileName = DocumentNo;
        }
        else
        {
            PDFFileName = "OmTuranthLR";
        }

        myReportDocument.ExportToHttpResponse
        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, PDFFileName);

    }

    private DataSet Get_DataSet()
    {
        SqlParameter[] objSqlParam =  { objDAL.MakeInParams("@IsDocumentNoWise", SqlDbType.Bit , 0, IsDocumentNoWise),
            objDAL.MakeInParams("@DocumentTypeID", SqlDbType.Int, 0, DocumentTypeId),
            objDAL.MakeInParams("@DocumentNo", SqlDbType.VarChar, 20, DocumentNo),
            objDAL.MakeInParams("@GetGCXML", SqlDbType.VarChar,1000,GCNos)

        };
        objDAL.RunProc("dbo.EC_Opr_Get_GC_NosFor_PDF_Creation", objSqlParam, ref ds);
        return ds; 
    }
     

    private void Bind_To_Viewer()
    {
        //Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {

            Printer_Settings pkSizeObj = new Printer_Settings(0, "OMBharat_GC");

            myReportDocument.PrintOptions.PaperSize = pkSizeObj.Papersize;

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

