using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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


public partial class Reports_CL_Nandwana_UserDesk_FrmPendingTripSheet : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal NoOfTrips;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }
    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_NoOfTrips;

            lbl_NoOfTrips = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOfTrips");

            lbl_NoOfTrips.Text = NoOfTrips.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            
            int DVLPID;
            LinkButton lbtn_Vehicle;

            DVLPID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DailyVehicleLoadingPlanID").ToString());

            lbtn_Vehicle = (LinkButton)e.Item.FindControl("lbtn_Vehicle");

            
            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/FrmPendingTripSheetMemoDetails.aspx?DVLPID=" + DVLPID);

            lbtn_Vehicle.Attributes.Add("onclick", "return TripExpenseSheetMemoDetails('" + PathDlyStk + "')");

        }
    }


    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Details.CurrentPageIndex;
        int grid_PageSize = dg_Details.PageSize;


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
        objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)};

        objDAL.RunProc("COM_UserDesk_Get_Pending_Trip_Sheet_Details", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();

        calculate_totals();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        ClearVariables();
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        NoOfTrips = Util.String2Decimal(dr["NoOfTrips"].ToString());
    }

    public void ClearVariables()
    {
        ds = null;
    }
}
