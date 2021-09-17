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


public partial class Operations_VehicleTripExpense_Frm_Trip_Expense_Driver_Trip_History : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;


    private int TripExpenseID
    {
        set { hdnTripExpenseID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnTripExpenseID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnTripExpenseID.Value);
        }
    }

    private int Driver_ID
    {
        set { hdnDriver_ID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnDriver_ID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnDriver_ID.Value);
        }
    }


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DriverTripHistory";

        if (IsPostBack == false)
        {

            Driver_ID = Util.DecryptToInt(Request.QueryString["Driver_ID"]);
            TripExpenseID = Util.DecryptToInt(Request.QueryString["TripExpenseID"]);

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }
    }


    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int TripExpenseAprovalID,TripExpenseID;

            LinkButton lnk_TripNo;

            TripExpenseAprovalID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseAprovalID").ToString());
            TripExpenseID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseID").ToString());

            lnk_TripNo = (LinkButton)e.Item.FindControl("lnk_TripNo");


            StringBuilder PathTrip = new StringBuilder(Util.GetBaseURL());

            PathTrip.Append("/");
            PathTrip.Append("Operations/Vehicle Trip Expense/FrmVehicleTripExpenseApprovalView.aspx?Id=" + ClassLibraryMVP.Util.EncryptInteger(TripExpenseID) + "&Menu_Item_Id=MwAxADcA&Mode=NAA=&FromHistory=" + ClassLibraryMVP.Util.EncryptInteger(1));

            lnk_TripNo.Attributes.Add("onclick", "return viewwindow_TripExpenseAproval('" + PathTrip + "')");

        }
    }

    #endregion

    #region Other Function


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Details.CurrentPageIndex;
        int grid_PageSize = dg_Details.PageSize;


        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@DriverID", SqlDbType.Int, 0,Driver_ID),
            objDAL.MakeInParams("@TripExpenseID", SqlDbType.Int, 0,TripExpenseID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EF_Opr_Trip_Expense_Driver_Trip_History", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }


    private void PrepareDTForExportToExcel()
    {
        ds.Tables[0].Columns.Remove("TripExpenseAprovalID");
        ds.Tables[0].Columns.Remove("TripExpenseID");
        ds.Tables[0].Columns.Remove("Driver_ID");
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
    #endregion


}
