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

public partial class Reports_Booking_Frm_NewClientDlyStockViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, BranchID, User_ID,  Printer_ID = 0;
    string Paper_Size, Client_Code, BranchText, AreaText, RegionText; 
    String scripts;

    public DataSet SessionBindNewClientDlyStock
    {
        get { return StateManager.GetState<DataSet>("BindNewClientDlyStock"); }
        set
        {
            StateManager.SaveState("BindNewClientDlyStock", value);

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Client_Code = CompanyManager.getCompanyParam().ClientCode;
        User_ID = UserManager.getUserParam().UserId;


        Crypt = Request.QueryString["BranchID"];
        BranchID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["BranchText"];
        BranchText = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["AreaText"];
        AreaText = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["RegionText"];
        RegionText = ClassLibraryMVP.Util.DecryptToString(Crypt);

        //ds = Get_DataSet(Menu_Item_ID, Region_Id, Area_id, BranchID, Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date));
        ds = SessionBindNewClientDlyStock;


        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
        //myReportDocument.SetParameterValue("dlyType", dlyType);
        Bind_To_Viewer();
    }

    //private DataSet Get_DataSet(int Menu_Item_ID, int Region_Id, int Area_id, int BranchID, DateTime From_Date,DateTime To_Date)
    //{
    //    SqlParameter[] objSqlParam ={ 
    //        objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
    //        objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
    //        objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,BranchID), 
    //        objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
    //        objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date), 
    //        objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int,0,Menu_Item_ID) 
    //    };

    //    objDAL.RunProc("EC_RPT_Branchwise_Booking_Register_PaidTBB", objSqlParam, ref ds); 
    //    return ds;
 
    //}     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {

            myReportDocument.SetParameterValue("@BranchText", BranchText);
            myReportDocument.SetParameterValue("@AreaText", AreaText);
            myReportDocument.SetParameterValue("@RegionText", RegionText);
				
            //myReportDocument.PrintOptions.PaperSize = pkSizeObj.Papersize;
            //myReportDocument.PrintOptions.PrinterName = pkSizeObj.PrinterName.ToString();
            
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
                Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_NewClientDlyStock.rpt";
                //Paper_Size = "Turanth_DlyMemo";
                //Printer_ID = 1;
               
            }  
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
        //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_NewClientDlyStock.xsd"); 
    }
}

