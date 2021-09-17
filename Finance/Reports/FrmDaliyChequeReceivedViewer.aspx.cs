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

public partial class Finance_Reports_FrmDaliyChequeReceivedViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_Id, Region_Id, Area_id, Branch_id, User_ID,  Printer_ID = 0,ReportType =0;
    string Fromdate, Todate, Paper_Size, Region_Name, Area_Name, Branch_Name;
    
    protected void Page_Load(object sender, EventArgs e)
    { 
       
        User_ID = UserManager.getUserParam().UserId; 

        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Region_Id"];
        Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Area_id"];
        Area_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Branch_id"];
        Branch_id = ClassLibraryMVP.Util.DecryptToInt(Crypt); 

        Crypt = Request.QueryString["Region_Name"];
        Region_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["Area_Name"];
        Area_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["Branch_Name"];
        Branch_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Fromdate = Request.QueryString["Fromdate"];

        Todate = Request.QueryString["Todate"];

        ReportType = Convert.ToInt32(Request.QueryString["ReportType"]);

        ds = Get_DataSet(Menu_Item_Id, Region_Id, Area_id, Branch_id, Convert.ToDateTime(Fromdate), Convert.ToDateTime(Todate)); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
       
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(int Menu_Item_Id, int Region_Id, int Area_id, int Branch_id, DateTime Fromdate, DateTime Todate)
    {
        
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id), 
                objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id), 
                objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
                objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,Fromdate), 
                objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,Todate),
                objDAL.MakeInParams("@ReportType",SqlDbType.Int,0,ReportType)
                };
            { objDAL.RunProc("FA_RPT_DailyChequeReceived", objSqlParam, ref ds); }  
          

        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            if (ReportType == 0)
            {
                myReportDocument.SetParameterValue("ReportName", "Daily Cheque Received - BranchWise");
            }
            else
            {
                myReportDocument.SetParameterValue("ReportName", "Daily Cheque Received - DateWise");
            }

            myReportDocument.SetParameterValue("Fromdate", Convert.ToDateTime(Fromdate));
            myReportDocument.SetParameterValue("Todate", Convert.ToDateTime(Todate));

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

        if (ReportType == 0)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/FA_RPT_DailyChequeReceivedBranchWise.rpt";
        }
        else
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/FA_RPT_DailyChequeReceivedDateWise.rpt";
        }
 
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
        //dsSchema.WriteXmlSchema("C:/XMLSchema_FA_RPT_DailyChequeReceived.xsd");
    }
}

