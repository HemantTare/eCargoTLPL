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

public partial class Godown_Delivery_Register_ReportsViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_Id, Region_Id, Area_id, Branch_id, User_ID,  Printer_ID = 0;
    string Fromdate, Todate, Paper_Size, Region_Name, Area_Name, Branch_Name;
    

    String scripts;

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
                objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
                objDAL.MakeInParams("@From_Date",SqlDbType.DateTime,0,Fromdate), 
                objDAL.MakeInParams("@To_Date",SqlDbType.DateTime,0,Todate)
                };
            { objDAL.RunProc("EC_RPT_Godown_Delivery_Register", objSqlParam, ref ds); }  
          

        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {

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

        Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_Godown_Delivery_Register.rpt"; 
             
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
        //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Godown_Delivery_Register.xsd");
    }
}

