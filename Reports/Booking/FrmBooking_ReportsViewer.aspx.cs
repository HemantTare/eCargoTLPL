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

public partial class Reports_Booking_FrmBooking_ReportsViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_Id, Region_Id, Area_id, Branch_id, BookingTypeId, PaymentTypeId, User_ID, GC_Count, Printer_ID = 0;
    string Paper_Size, Region_Name, Area_Name, Branch_Name, BookingTypeName, PaymentTypeName;
    bool Is_BookingRpt;

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

        Crypt = Request.QueryString["BookingTypeName"];
        BookingTypeName = ClassLibraryMVP.Util.DecryptToString(Crypt); 

        Crypt = Request.QueryString["PaymentTypeName"];
        PaymentTypeName = ClassLibraryMVP.Util.DecryptToString(Crypt);

        Crypt = Request.QueryString["BookingTypeId"];
        BookingTypeId = ClassLibraryMVP.Util.DecryptToInt(Crypt); 

        Crypt = Request.QueryString["PaymentTypeId"];
        PaymentTypeId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Is_BookingRpt"];
        Is_BookingRpt = ClassLibraryMVP.Util.DecryptToBool(Crypt);


        ds = Get_DataSet(Menu_Item_Id, Region_Id, Area_id, Branch_id, BookingTypeId, PaymentTypeId, Is_BookingRpt); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
       
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(int Menu_Item_Id, int Region_Id, int Area_id, int Branch_id, int BookingTypeId, int PaymentTypeId, bool Is_BookingRpt)
    {
        if (Menu_Item_Id == 5227)
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
                objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
                objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
                objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int,0,BookingTypeId), 
                objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,0,PaymentTypeId), 
                objDAL.MakeInParams("@StartDate",SqlDbType.DateTime,0,UserManager.getUserParam().StartDate), 
                objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,UserManager.getUserParam().EndDate),
                objDAL.MakeInParams("@Is_BookingRpt",SqlDbType.Bit,0,Is_BookingRpt)

                };
            { objDAL.RunProc("EC_RPT_MonthWise_Booking_Register", objSqlParam, ref ds); }  
        }  

        return ds;
    }     

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            if (Menu_Item_Id == 5227)
            {
                myReportDocument.SetParameterValue("@Region_Name", Region_Name);
                myReportDocument.SetParameterValue("@Area_Name", Area_Name);
                myReportDocument.SetParameterValue("@Branch_Name", Branch_Name);
                myReportDocument.SetParameterValue("@BookingTypeName", BookingTypeName);
                myReportDocument.SetParameterValue("@PaymentTypeName", PaymentTypeName);
                myReportDocument.SetParameterValue("@CompanyName", PaymentTypeName);
                myReportDocument.SetParameterValue("@PaymentTypeName", PaymentTypeName);
                myReportDocument.SetParameterValue("@CompanyName", UserManager.getUserParam().CompanyName);

                if (Menu_Item_Id == 5227 && Is_BookingRpt == true)
                {
                    myReportDocument.SetParameterValue("@ReportName", "MonthWise Booking Register (Bkg Branchwise)");
                }
                else if (Menu_Item_Id == 5227 && Is_BookingRpt == false)
                {
                    myReportDocument.SetParameterValue("@ReportName", "MonthWise Booking Register (Dly Branchwise)");
                }
            } 
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
        
        if (Menu_Item_Id == 5227)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_MonthWise_Booking_Register.rpt"; 
        } 
             
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    {
        if (Menu_Item_Id == 5227)
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_MonthWise_Booking_Register.xsd");
        } 
    }
}

