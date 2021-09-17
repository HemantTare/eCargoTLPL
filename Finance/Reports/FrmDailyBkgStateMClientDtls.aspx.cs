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

public partial class Finance_Reports_FrmDailyBkgStateMClientDtls : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, Region_Id, Area_id, Branch_id, User_ID, Client_ID, GC_Count, Printer_ID = 0;
    string Paper_Size, Client_Code, From_Date, To_Date;

    String scripts;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        Client_Code = CompanyManager.getCompanyParam().ClientCode;
        User_ID = UserManager.getUserParam().UserId;


        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Region_Id"];
        Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Area_id"];
        Area_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Branch_id"];
        Branch_id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Client_ID"];
        Client_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        From_Date = Request.QueryString["From_Date"];

        To_Date = Request.QueryString["To_Date"];

        ds = Get_DataSet(Menu_Item_ID, Region_Id, Area_id, Branch_id, Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date), Client_ID);

        dg_GridTBB.DataSource = ds;
        dg_GridTBB.DataBind();  
        
    }

    private DataSet Get_DataSet(int Menu_Item_ID, int Region_Id, int Area_id, int Branch_id, DateTime From_Date, DateTime To_Date, int Client_ID)
    {
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date), 
            objDAL.MakeInParams("@Client_ID", SqlDbType.Int,0,Client_ID) 
        };

        objDAL.RunProc("EC_Branchwise_Booking_Register_TBBClient", objSqlParam, ref ds); 
        return ds;
    }

    protected void dg_GridTBB_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID;
            LinkButton lnk_GC_No;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");
        }
    }
}

