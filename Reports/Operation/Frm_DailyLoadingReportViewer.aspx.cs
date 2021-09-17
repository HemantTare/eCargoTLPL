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

public partial class Reports_Operation_Frm_DailyLoadingReportViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, User_ID, GC_Count, Printer_ID = 0, Region_Id, Area_Id, Branch_Id;
    string VehicleNo, Paper_Size, Client_Code, From_Date, To_Date;

    String scripts;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        Client_Code = CompanyManager.getCompanyParam().ClientCode;
        User_ID = UserManager.getUserParam().UserId; 

        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["VehicleNo"];
        VehicleNo = Crypt; 

        From_Date = Request.QueryString["From_Date"];

        To_Date = Request.QueryString["To_Date"];

        Crypt = Request.QueryString["Region_Id"];
        Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Area_Id"];
        Area_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Branch_Id"];
        Branch_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);


        ds = Get_DataSet(Menu_Item_ID, VehicleNo, Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date)); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds); 
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(int Menu_Item_ID, string VehicleNo, DateTime From_Date, DateTime To_Date)
    {
        if (Menu_Item_ID == 5229)
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Vehicle_No", SqlDbType.VarChar,50,VehicleNo),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Region_Id", SqlDbType.Int ,0,Region_Id),
            objDAL.MakeInParams("@Area_Id", SqlDbType.Int ,0,Area_Id ),            
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int ,0,Branch_Id )};

            objDAL.RunProc("EC_RPT_DailyLoadingReport", objSqlParam, ref ds);
            
        }
        else if (Menu_Item_ID == 5256)
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Vehicle_No", SqlDbType.VarChar,50,VehicleNo),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,To_Date)};

            objDAL.RunProc("EC_RPT_DailyVehicleWeighingReport", objSqlParam, ref ds);
            
        }
        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {

            myReportDocument.SetParameterValue("@FromDate", From_Date);
            myReportDocument.SetParameterValue("@ToDate", To_Date); 
            
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
         
            if (Client_Code.ToLower() == "nandwana")
            {
                if (Menu_Item_ID == 5229)
                {
                    Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_DailyLoadingReport.rpt";
                }
                else
                {
                    Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_DailyVehicleWeighingReport.rpt";
                }
            }  
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {

        //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_DailyLoadingReport.xsd");
        //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_DailyVehicleWeighingReport.xsd");
           
    }
}

