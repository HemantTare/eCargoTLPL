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

public partial class Finance_Reports_FrmClosingCashBalNewViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt;
    int  Branch_id,  Printer_ID = 0;
    string date, Branch_Name, Paper_Size;
    
    protected void Page_Load(object sender, EventArgs e)
    { 
       



        Crypt = Request.QueryString["Branch_id"];
        Branch_id = ClassLibraryMVP.Util.DecryptToInt(Crypt); 


        Crypt = Request.QueryString["Branch_Name"];
        Branch_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);


        date  = Request.QueryString["date"];


        ds = Get_DataSet( Branch_id, Convert.ToDateTime(date)); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
       
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(int Branch_id, DateTime date)
    {
        
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id), 
                objDAL.MakeInParams("@Date",SqlDbType.DateTime,0,date)
                };
            { objDAL.RunProc("FA_Opr_BranchWiseDailyClosingCashReport_New", objSqlParam, ref ds); }  
          

        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            myReportDocument.SetParameterValue("BranchName", Branch_Name);

            myReportDocument.SetParameterValue("AsOnDate", Convert.ToDateTime(date));

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

        Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/FA_RPT_ClosingCashBalNew.rpt";
 
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
//        dsSchema.WriteXmlSchema("C:/XMLSchema_FA_RPT_ClosingCashBalNew.xsd");
    }
}

