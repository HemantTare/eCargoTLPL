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
using Raj.EC;
using System.Text;

public partial class Reports_CL_Nandwana_User_Desk_FrmeWayBillsExpiringTodaySummary : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private int Branch_ID, Area_ID, Region_ID;  
    #endregion

    #region ControlsValue
    #endregion
   
    protected void Page_Load(object sender, EventArgs e)
    {


        Branch_ID = Convert.ToInt32(Request.QueryString["Branch_ID"].ToString());
        Area_ID = Convert.ToInt32(Request.QueryString["Area_ID"].ToString());
        Region_ID = Convert.ToInt32(Request.QueryString["Region_ID"].ToString());

        if (IsPostBack == false)
        {
            BindGrid("form_and_pageload", e);
        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "eWayBillsExpiringSummary";


    }
 

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }



    private void BindGrid(object sender, EventArgs e)
    {
        DateTime @FromDate = DateTime.Now.Date;
        DateTime @ToDate = DateTime.Now.Date;
        
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

         SqlParameter[] objSqlParam = {objDAL.MakeInParams("@RegionID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@AreaID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@BranchID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate)
         };

         objDAL.RunProc("dbo.COM_UserDesk_Get_eWayBills_Expiring_Today", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dg_Grid.DataSource = ds.Tables[0];
                dg_Grid.DataBind(); 
            }

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            }
    }

    private void PrepareDTForExportToExcel()
    {
        ds.Tables[0].Columns.Remove("DlyBranchIndex");
        ds.Tables[0].Columns.Remove("DlyBranchId");
        
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];


    }
 
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int BranchId;
            string Branch_Name;
            LinkButton lbtn_Branch;

            Branch_ID  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DlyBranchId").ToString());
            Branch_Name =DataBinder.Eval(e.Item.DataItem, "DlyBranch").ToString();

            lbtn_Branch = (LinkButton)e.Item.FindControl("lbtn_Branch");

            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBillsExpiringTodayList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&Branch_Name=" + Branch_Name);
            lbtn_Branch.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");


        }

       
    }
}



