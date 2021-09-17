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

public partial class Reports_SalesBilling_FrmWalkInRegularClientLablesViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    
    int  Printer_ID = 0;
    string Paper_Size,From_Date, To_Date,FilterTypeId,SearchFor,IsCreatedBy;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        From_Date = Request.QueryString["From_Date"];

        To_Date = Request.QueryString["To_Date"];

        FilterTypeId = Request.QueryString["FilterTypeId"];

        SearchFor = Request.QueryString["SearchFor"];

        IsCreatedBy = Request.QueryString["IsCreatedBy"];

        ds = Get_DataSet(Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date),Convert.ToInt32(FilterTypeId),SearchFor); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(DateTime From_Date,DateTime To_Date,int FilterTypeId,String SearchFor)
    {
        SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@IsCreateBy", SqlDbType.Int,0,Convert.ToInt32(IsCreatedBy)),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@FilterTypeID", SqlDbType.Int ,0,FilterTypeId),
            objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar,25,SearchFor),
             objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,100000)  
            };

        objDAL.RunProc("EC_RPT_WalkIn_Regular_Client_Details", objSqlParam, ref ds); 
        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {

            //myReportDocument.SetParameterValue("@FromDate", From_Date);
            //myReportDocument.SetParameterValue("@ToDate", To_Date);
				
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
         
                Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/WalkInRegularClientLables.rpt";

        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {

        //dsSchema.WriteXmlSchema("C:/FrmWalkInRegularClientLables.xsd");
           
    }
}

