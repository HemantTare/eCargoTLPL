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

public partial class Finance_Reports_FrmDailyBookingStatementPaidPrinOrExport : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt;
    int Menu_Item_ID, Region_Id, Area_id, Branch_id;
    string From_Date, To_Date;


    protected void Page_Load(object sender, EventArgs e)
    {
        

        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Region_Id"];
        Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Area_id"];
        Area_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Branch_id"];
        Branch_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);


        From_Date = Request.QueryString["From_Date"];

        To_Date = Request.QueryString["To_Date"];

        Wuc_Export_To_Excel.FileName = "DailyBookingRegisterPaid";

        string BkgRegPaidTBBViewerUrl;

        BkgRegPaidTBBViewerUrl = ClassLibraryMVP.Util.GetBaseURL() +
                     "/Reports/Direct_Printing/FrmBrchWiseBkgRegPaidTBBViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(269) +
                     "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_id) + "&Branch_id=" + Util.EncryptInteger(Branch_id) + "&From_Date=" + From_Date + "&To_Date=" + To_Date;


        btn_Print.Attributes.Add("onclick", "return GridPaidToPay('" + BkgRegPaidTBBViewerUrl + "');");


        if (IsPostBack == true)
        {
            PrepareDTForExportToExcel();
        }

    }

    private DataSet Get_DataSet(int Menu_Item_ID, int Region_Id, int Area_id, int Branch_id, DateTime From_Date, DateTime To_Date)
    {
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date), 
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int,0,Menu_Item_ID) 
        };

        objDAL.RunProc("EC_RPT_Branchwise_Booking_Register_PaidTBB", objSqlParam, ref ds);
        return ds;

        
    }


    private void PrepareDTForExportToExcel()
    {

        ds = Get_DataSet(Menu_Item_ID, Region_Id, Area_id, Branch_id, Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date));

        ds.Tables[0].Columns.Remove("Vehicle_No");
        ds.Tables[0].Columns.Remove("Invoice_No");
    
        Wuc_Export_To_Excel.SessionExporttoExcel = ds.Tables[0];

        ClearVariables();
    }

    public void ClearVariables()
    {
        ds = null;
    }

}

