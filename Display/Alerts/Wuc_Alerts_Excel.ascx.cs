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
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Alerts_Wuc_Alerts_Excel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel")
                GetVehicleAlerts();
            else
                this.Visible = false;
        }

    }

    private void GetVehicleAlerts()
    {
        string IsFromDesktop;
        IsFromDesktop = "y";
        lnk_btnIncomingTruck.Attributes.Add("onclick", "return OpenIncomingTrucksAlert('" + IsFromDesktop + "')");


        DataSet ds = new DataSet();
        DAL objDAL = new DAL();

        string hierarchy_code = UserManager.getUserParam().HierarchyCode;
        int MainID = UserManager.getUserParam().MainId;



        int RegionId = 0;
        int AreaId = 0;
        int BranchId = 0;

        if (hierarchy_code == "BO")
            BranchId = MainID;
        else if (hierarchy_code == "AO")
            AreaId = MainID;
        else if (hierarchy_code == "RO")
            RegionId = MainID;

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,RegionId),   
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,AreaId),   
            objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,BranchId),
            objDAL.MakeInParams("@TruckNo",SqlDbType.VarChar,20,""),            
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@is_for_userdesk",SqlDbType.VarChar,1,IsFromDesktop),            
        };

        objDAL.RunProc("[dbo].[EC_RPT_Incoming_Trucks_Alert_Excel]", objSqlParam, ref ds);
        int TotalRecords = Util.String2Int(ds.Tables[0].Rows[0]["TotalRecords"].ToString());

        lnk_btnIncomingTruck.Text = "Incoming Vehicles(" + TotalRecords + ")";
    }
}
