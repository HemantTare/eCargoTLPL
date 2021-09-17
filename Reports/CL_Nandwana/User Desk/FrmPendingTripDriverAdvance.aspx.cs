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


public partial class Reports_CL_Nandwana_UserDesk_FrmPendingTripDriverAdvance : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal NoOfTrips, TotalAdvance;

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
            Label lbl_NoOfTrips, lbl_TotalAdvance;

            lbl_NoOfTrips = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOfTrips");
            lbl_TotalAdvance = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalAdvance");

            lbl_NoOfTrips.Text = NoOfTrips.ToString();
            lbl_TotalAdvance.Text = TotalAdvance.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int TripExpenseID, TripExpenseAprovalID;
            LinkButton lbtn_Advance;

            string Vehicle_No, Driver_Name;

            int Branch_ID;

            Branch_ID = UserManager.getUserParam().MainId;


            TripExpenseID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseID").ToString());
            TripExpenseAprovalID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseAprovalID").ToString());

            Vehicle_No = DataBinder.Eval(e.Item.DataItem, "Vehicle_No").ToString();
            Driver_Name = DataBinder.Eval(e.Item.DataItem, "Driver_Name").ToString();

            lbtn_Advance = (LinkButton)e.Item.FindControl("lbtn_Advance");



            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Operations/Vehicle Trip Expense/FrmDriverTripAdvance.aspx?Id=MAA=&Menu_Item_Id=MwAxADgA&Mode=MQA=&TripExpenseAprovalID="
                + Util.EncryptInteger(TripExpenseAprovalID) + "&Vehicle_No=" + Util.EncryptString(Vehicle_No) + "&Driver_Name=" + Util.EncryptString(Driver_Name)
                + "&Advance=" + Util.EncryptString(lbtn_Advance.Text));

            if (Branch_ID > 0)
            {
                lbtn_Advance.Attributes.Add("onclick", "return DriverTripAdvance('" + PathF4 + "')");
            }


        }
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        dg_Details.CurrentPageIndex = 0;
        BindGrid("form", e);
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

        int Branch_ID;

        Branch_ID = UserManager.getUserParam().MainId;

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, Branch_ID),
        objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
        objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)};

        objDAL.RunProc("COM_UserDesk_Get_Pending_Trip_Driver_Advance", objSqlParam, ref ds);

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
        TotalAdvance = Util.String2Decimal(dr["TotalAdvance"].ToString());

    }

    public void ClearVariables()
    {
        ds = null;
    }
}
