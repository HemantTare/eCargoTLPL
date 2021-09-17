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

public partial class Reports_CL_Nandwana_UserDesk_FrmPendingFreightBookingDetailsViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int  Printer_ID = 0;
    string Paper_Size,GC_Ids;
    

    String scripts;

    protected void Page_Load(object sender, EventArgs e)
    { 
       

        Crypt = Request.QueryString["GC_Ids"];
        GC_Ids  = ClassLibraryMVP.Util.DecryptToString(Crypt);

        ds = Get_DataSet(GC_Ids); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
       
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(String GC_Ids)
    {
        
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,""), 
                objDAL.MakeInParams("@GC_Ids",SqlDbType.VarChar ,1000, GC_Ids)};
            { objDAL.RunProc("EC_Opr_Pending_Freight_Booking_Print", objSqlParam, ref ds); }  
          

        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            
            //myReportDocument.SetParameterValue("Fromdate", Convert.ToDateTime(Fromdate));
            //myReportDocument.SetParameterValue("Todate", Convert.ToDateTime(Todate));

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

        Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_Pending_Freight_Booking_Print.rpt"; 
             
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
        //dsSchema.WriteXmlSchema("C:/XMLSchema_Pending_Freight_Booking_Print.xsd");
    }
}

