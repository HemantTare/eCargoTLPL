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
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;


public partial class Reports_CL_Nandwana_UserDesk_FrmPendingTripExpenseApproval : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal NoOfTrips, TotalTripExpense;

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
            Label lbl_NoOfTrips, lbl_TotalTripExpense;

            lbl_NoOfTrips = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOfTrips");
            lbl_TotalTripExpense = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTripExpense");

            lbl_NoOfTrips.Text = NoOfTrips.ToString();
            lbl_TotalTripExpense.Text = TotalTripExpense.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int TripExpenseID, TripExpenseAprovalID;
            LinkButton lbtn_Approve, lbtn_Advance;
            Decimal AdvancePaid, AdvanceToBePaid;
            Boolean IsApproved;

            TripExpenseID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseID").ToString());

            TripExpenseAprovalID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseAprovalID").ToString());

            IsApproved  = Util.String2Bool (DataBinder.Eval(e.Item.DataItem, "IsApproved").ToString());

            AdvancePaid = Util.String2Decimal(DataBinder.Eval(e.Item.DataItem, "AdvancePaid").ToString());

            AdvanceToBePaid = Util.String2Decimal(DataBinder.Eval(e.Item.DataItem, "AdvanceToBePaid").ToString());

            lbtn_Approve = (LinkButton)e.Item.FindControl("lbtn_Approve");
            lbtn_Advance = (LinkButton)e.Item.FindControl("lbtn_Advance");

            if (AdvancePaid < AdvanceToBePaid)
            {
                e.Item.BackColor = System.Drawing.Color.SpringGreen;
            }


            if (TripExpenseAprovalID <= 0)
            {
                e.Item.BackColor = System.Drawing.Color.Orange;
            }


            if (IsApproved == false )
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Operations/Vehicle Trip Expense/FrmVehicleTripExpenseApproval.aspx?Id=" + Util.EncryptInteger(TripExpenseID) + "&Menu_Item_Id=MwAxADcA&Mode=MgA=&IsApprove=MQA=");
                lbtn_Approve.Attributes.Add("onclick", "return TripExpenseApproval('" + PathF4 + "')");
            }
            else
            {
                lbtn_Approve.Enabled = false;
            }

            
            if (AdvanceToBePaid == 0 || AdvancePaid < AdvanceToBePaid)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Operations/Vehicle Trip Expense/FrmVehicleTripExpenseApproval.aspx?Id=" + Util.EncryptInteger(TripExpenseID) + "&Menu_Item_Id=MwAxADcA&Mode=MgA=&IsApprove=MAA=");
                lbtn_Advance.Attributes.Add("onclick", "return TripExpenseApproval('" + PathF4 + "')");
            }
            else
            {
                lbtn_Advance.Enabled = false;
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

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 20, txt_SearchFor.Text),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)};

        objDAL.RunProc("COM_UserDesk_Get_Pending_Trip_Expense_For_Approval", objSqlParam, ref ds);

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
        TotalTripExpense = Util.String2Decimal(dr["TotalTripExpense"].ToString());

    }

    public void ClearVariables()
    {
        ds = null;
    }
}
