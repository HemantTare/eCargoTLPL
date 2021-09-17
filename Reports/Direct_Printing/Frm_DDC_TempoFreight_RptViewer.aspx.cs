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

public partial class Reports_Direct_Printing_Frm_DDC_TempoFreight_RptViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, Region_Id, Area_id, Branch_id, User_ID, GC_Count, Printer_ID = 0;
    string Region_Name, Area_Name, Branch_Name, Paper_Size, Client_Code, From_Date, To_Date;

    String scripts;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        Client_Code = CompanyManager.getCompanyParam().ClientCode;
        User_ID = UserManager.getUserParam().UserId;


        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Region_Id"];
        Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Region_Name"];
        Region_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["Area_id"];
        Area_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Area_Name"];
        Area_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["Branch_id"];
        Branch_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Branch_Name"];
        Branch_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);
 
        From_Date = Request.QueryString["Fromdate"];

        To_Date = Request.QueryString["Todate"];

        ds = Get_DataSet(Menu_Item_ID, Region_Id, Area_id, Branch_id, Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date)); 


        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds); 
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(int Menu_Item_ID, int Region_Id, int Area_id, int Branch_id, DateTime From_Date,DateTime To_Date)
    {
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date) 
        };

        objDAL.RunProc("EC_RPT_DDC_TempoFreight", objSqlParam, ref ds); 
        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {

            myReportDocument.SetParameterValue("@FromDate", From_Date);
            myReportDocument.SetParameterValue("@ToDate", To_Date);
            myReportDocument.SetParameterValue("@Region", Region_Name);
            myReportDocument.SetParameterValue("@Area", Area_Name);
            myReportDocument.SetParameterValue("@Branch", Branch_Name); 
            
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
                Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_DDC_TempoFreight_Rpt.rpt";
                //Paper_Size = "Turanth_DlyMemo";
                //Printer_ID = 1;
               
            }  
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {

        //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_DDC_TempoFreight.xsd");
           
    }
}

