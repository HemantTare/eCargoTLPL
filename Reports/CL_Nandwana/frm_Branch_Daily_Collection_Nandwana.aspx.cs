using System;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_CL_Nandwana_frm_Branch_Daily_Collection_Nandwana : System.Web.UI.Page
{
    int Main_Id, Year_Code;
    string Hirearchy_Code;

    DataSet ds = new DataSet();
    DataSet ds_Booking = new DataSet();
    DataSet ds_Credit_Memo = new DataSet();
    DataSet ds_Delivery = new DataSet();
    DataSet ds_Expenses = new DataSet();
    DataSet ds_Income = new DataSet();
    DataSet ds_Total = new DataSet();

    decimal Total_Amount_Credit_Memo, Cash_Amount_Booking, Cheque_Amount_Booking, Cash_Amount_Delivery, Cheque_Amount_Delivery;
    decimal Total_Booking_Amount, Total_Delivery_Amount;
    decimal Amount_Expenses, Amount_Income;

    decimal Amount_Income_Cash, Amount_Income_Chq;
    decimal Amount_Expenses_Cash, Amount_Expenses_Chq;
    string Total = "Total";
    Boolean Export = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        Hirearchy_Code = (string)UserManager.getUserParam().HierarchyCode;
        Main_Id = (int)UserManager.getUserParam().MainId;
        Year_Code = UserManager.getUserParam().YearCode;

        if (IsPostBack == false)
        {
            Bind_DDL();

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid_Booking);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Delivery);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Credit_Memo);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Expenses);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Income);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Total);
        }
    }

    protected void Bind_DDL()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@Hirearchy_Code", SqlDbType.Char,25,Hirearchy_Code),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,Main_Id)
        };

        objDAL.RunProc("EC_RPT_BRANCH_ONLY", objSqlParam, ref ds);

        ddl_Branch.DataSource = ds;
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_ID";
        ddl_Branch.DataBind();
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            //pnl_Booking.Visible = true;
            //pnl_Delivery.Visible = true;
            //pnl_Expenses.Visible = true;
            //pnl_Income.Visible = true;
            //pnl_Total.Visible = true;

            dg_Grid_Booking.Visible = true;
            dg_Grid_Booking.CurrentPageIndex = 0;
            BindGrid_Booking("form", e);

            dg_Grid_Credit_Memo.Visible = true;
            dg_Grid_Credit_Memo.CurrentPageIndex = 0;
            BindGrid_Credit_Memo("form", e);

            dg_Grid_Delivery.Visible = true;
            dg_Grid_Delivery.CurrentPageIndex = 0;
            BindGrid_Delivery("form", e);

            dg_Grid_Expenses.Visible = true;
            dg_Grid_Expenses.CurrentPageIndex = 0;
            BindGrid_Expenses("form", e);

            dg_Grid_Income.Visible = true;
            dg_Grid_Income.CurrentPageIndex = 0;
            BindGrid_Income("form", e);

            dg_Grid_Total.Visible = true;
            BindGrid_Total("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid_Booking.Visible = false;
            dg_Grid_Credit_Memo.Visible = false;
            dg_Grid_Delivery.Visible = false;
            dg_Grid_Expenses.Visible = false;
            dg_Grid_Income.Visible = false;
            dg_Grid_Total.Visible = false;

            //pnl_Booking.Visible = false;
            //pnl_Delivery.Visible = false;
            //pnl_Expenses.Visible = false;
            //pnl_Income.Visible = false;
            //pnl_Total.Visible = false;
        }
    }

    protected void btn_Export_To_Export_Click(object sender, EventArgs e)
    {
        Export = true;

        BindGrid_Booking("exporttoexcelusercontrol", e);
        BindGrid_Delivery("exporttoexcelusercontrol", e);
        BindGrid_Expenses("exporttoexcelusercontrol", e);
        BindGrid_Income("exporttoexcelusercontrol", e);
        BindGrid_Total("exporttoexcelusercontrol", e);

        ds_Booking = (DataSet)Session["ds_Booking"];
        ds_Delivery = (DataSet)Session["ds_Delivery"];
        ds_Expenses = (DataSet)Session["ds_Expenses"];
        ds_Income = (DataSet)Session["ds_Income"];
        ds_Total = (DataSet)Session["ds_Total"];

        dg_Grid_Booking.AllowPaging = false;
        dg_Grid_Booking.DataSource = ds_Booking;
        dg_Grid_Booking.DataBind();

        dg_Grid_Delivery.AllowPaging = false;
        dg_Grid_Delivery.DataSource = ds_Delivery;
        dg_Grid_Delivery.DataBind();

        dg_Grid_Expenses.AllowPaging = false;
        dg_Grid_Expenses.DataSource = ds_Expenses;
        dg_Grid_Expenses.DataBind();

        dg_Grid_Income.AllowPaging = false;
        dg_Grid_Income.DataSource = ds_Income;
        dg_Grid_Income.DataBind();

        dg_Grid_Total.DataSource = ds_Total;
        dg_Grid_Total.DataBind();

        string Branch = ddl_Branch.SelectedItem.Text;
        string From = Wuc_From_To_Datepicker1.SelectedFromDate.ToString("dd/MM/yy");
        string To = Wuc_From_To_Datepicker1.SelectedToDate.ToString("dd/MM/yy");
        string FileName = "BranchDailyCollection" + "-" + Branch;//+ "-From:" +From + "-To:" + To;

        StringWriter strWtr = new StringWriter();
        HtmlTextWriter htmlWtr = new HtmlTextWriter(strWtr);

        this.pnl_Booking.RenderControl(htmlWtr);
        this.pnl_Delivery.RenderControl(htmlWtr);
        this.pnl_Expenses.RenderControl(htmlWtr);
        this.pnl_Income.RenderControl(htmlWtr);
        this.pnl_Total.RenderControl(htmlWtr);

        Response.Clear();
        Response.Charset = "";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
        Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Write(strWtr.ToString());
        Response.End();
    }

    public void ClearVariables()
    {
        ds_Booking = null;
        ds_Credit_Memo = null;
        ds_Delivery = null;
        ds_Expenses = null;
        ds_Income = null;
        ds_Total = null;
    }


    //-----------------------------------------Booking---------------------------------------------
    protected void dg_Grid_Booking_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID, MR_ID;
            Label lbl_GC_No_Booking, lbl_MR_No_Booking;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            MR_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "MR_ID").ToString());

            lbl_GC_No_Booking = (Label)e.Item.FindControl("lbl_GC_No");
            lbl_MR_No_Booking = (Label)e.Item.FindControl("lbl_MR_No");

            lbl_GC_No_Booking.Attributes.Add("onclick", "return viewwindow_general('GC','" + GC_ID + "')");
            lbl_MR_No_Booking.Attributes.Add("onclick", "return viewwindow_general('BKMR','" + MR_ID + "')");

            if (Export == false)
            {
                lbl_GC_No_Booking.CssClass = "LEVEL2ITEMHOVER";
                lbl_MR_No_Booking.CssClass = "LEVEL2ITEMHOVER";

                lbl_GC_No_Booking.Font.Underline = true;
                lbl_MR_No_Booking.Font.Underline = true;

                lbl_GC_No_Booking.Font.Bold = true;
                lbl_MR_No_Booking.Font.Bold = true;

                lbl_GC_No_Booking.ForeColor = Color.Blue;
                lbl_MR_No_Booking.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Cash_Amount_Booking, lbl_Cheque_Amount_Booking;

            lbl_Cash_Amount_Booking = (Label)e.Item.FindControl("lbl_Cash_Amount");
            lbl_Cheque_Amount_Booking = (Label)e.Item.FindControl("lbl_Cheque_Amount");

            lbl_Cash_Amount_Booking.Text = Cash_Amount_Booking.ToString();
            lbl_Cheque_Amount_Booking.Text = Cheque_Amount_Booking.ToString();
        }
    }

    protected void dg_Grid_Booking_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Booking.CurrentPageIndex = e.NewPageIndex;
        BindGrid_Booking("form", e);
    }

    private void BindGrid_Booking(object sender, EventArgs e)
    {
        ds_Booking = null;
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_Booking_currentpageindex = dg_Grid_Booking.CurrentPageIndex;
        int grid_Booking_PageSize = dg_Grid_Booking.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_Booking_currentpageindex = 0;
            grid_Booking_PageSize = 0;
        }

        int Branch_id = Convert.ToInt32(ddl_Branch.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
           
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_Booking_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_Booking_PageSize)
        };

        objDAL.RunProc("EC_RPT_BOOKING_BRANCHWISE_DAILY_COLLECTION_NANDWANA", objSqlParam, ref ds_Booking);

        dg_Grid_Booking.VirtualItemCount = Util.String2Int(ds_Booking.Tables[2].Rows[0][0].ToString());
        string TotalRecords_Booking = ds_Booking.Tables[2].Rows[0][0].ToString();
        calculate_totals_Booking();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid_Booking, ds_Booking.Tables[0], CallFrom, lbl_Error);

        Total_Booking_Amount = Cash_Amount_Booking + Cheque_Amount_Booking;
        lbl_Total_Booking_Amount.Text = Total_Booking_Amount.ToString();

        if (CallFrom == "exporttoexcelusercontrol")
        {
            Session["ds_Booking"] = ds_Booking;
        }
    }

    private void calculate_totals_Booking()
    {
        DataRow dr_Booking = ds_Booking.Tables[1].Rows[0];

        Cash_Amount_Booking = Util.String2Decimal(dr_Booking["Cash Amount"].ToString());
        Cheque_Amount_Booking = Util.String2Decimal(dr_Booking["Cheque Amount"].ToString());
    }

    //-----------------------------------------CREDIT MEMO---------------------------------------------
    protected void dg_Grid_Credit_Memo_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lbl_GC_No_Credit_Memo, lbl_Credit_Memo_No;

            int GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            int Credit_Memo_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Credit_Memo_ID").ToString());

            lbl_GC_No_Credit_Memo = (Label)e.Item.FindControl("lbl_GC_No");
            lbl_Credit_Memo_No = (Label)e.Item.FindControl("lbl_Credit_Memo_No");

            lbl_GC_No_Credit_Memo.Attributes.Add("onclick", "return viewwindow_general('GC','" + GC_ID + "')");
            lbl_Credit_Memo_No.Attributes.Add("onclick", "return viewwindow_general('CMEMO','" + Credit_Memo_ID + "')");

            if (Export == false)
            {
                lbl_GC_No_Credit_Memo.CssClass = "LEVEL2ITEMHOVER";
                lbl_Credit_Memo_No.CssClass = "LEVEL2ITEMHOVER";

                lbl_GC_No_Credit_Memo.Font.Underline = true;
                lbl_Credit_Memo_No.Font.Underline = true;

                lbl_GC_No_Credit_Memo.Font.Bold = true;
                lbl_Credit_Memo_No.Font.Bold = true;

                lbl_GC_No_Credit_Memo.ForeColor = Color.Blue;
                lbl_Credit_Memo_No.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total_Amount_Credit_Memo;

            lbl_Total_Amount_Credit_Memo = (Label)e.Item.FindControl("lbl_Total_Amount");
            lbl_Total_Amount_Credit_Memo.Text = Total_Amount_Credit_Memo.ToString();
        }
    }

    protected void dg_Grid_Credit_Memo_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Credit_Memo.CurrentPageIndex = e.NewPageIndex;
        BindGrid_Credit_Memo("form", e);
    }

    private void BindGrid_Credit_Memo(object sender, EventArgs e)
    {
        ds_Credit_Memo = null;
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_Credit_Memo_currentpageindex = dg_Grid_Credit_Memo.CurrentPageIndex;
        int grid_Credit_Memo_PageSize = dg_Grid_Credit_Memo.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_Credit_Memo_currentpageindex = 0;
            grid_Credit_Memo_PageSize = 0;
        }

        int Branch_id = Convert.ToInt32(ddl_Branch.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_date),           
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_Credit_Memo_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_Credit_Memo_PageSize)
        };

        objDAL.RunProc("EC_RPT_CREDIT_MEMO_BRANCHWISE_DAILY_COLLECTION_NANDWANA", objSqlParam, ref ds_Credit_Memo);

        dg_Grid_Credit_Memo.VirtualItemCount = Util.String2Int(ds_Credit_Memo.Tables[2].Rows[0][0].ToString());
        string TotalRecords_Credit_Memo = ds_Credit_Memo.Tables[2].Rows[0][0].ToString();
        calculate_totals_Credit_Memo();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid_Credit_Memo, ds_Credit_Memo.Tables[0], CallFrom, lbl_Error);

        Label2.Text = Total_Amount_Credit_Memo.ToString();

        if (CallFrom == "exporttoexcelusercontrol")
        {
            Session["ds_Credit_Memo"] = ds_Credit_Memo;
        }
    }

    private void calculate_totals_Credit_Memo()
    {
        DataRow dr_Credit_Memo = ds_Credit_Memo.Tables[1].Rows[0];
        Total_Amount_Credit_Memo = Util.String2Decimal(dr_Credit_Memo["Total Amount"].ToString());
    }

    //-----------------------------------------Delivery---------------------------------------------
    protected void dg_Grid_Delivery_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID, MR_ID;
            Label lbl_GC_No_Delivery, lbl_MR_No_Delivery;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            MR_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "MR_ID").ToString());

            lbl_GC_No_Delivery = (Label)e.Item.FindControl("lbl_GC_No");
            lbl_MR_No_Delivery = (Label)e.Item.FindControl("lbl_Ref_No");

            lbl_GC_No_Delivery.Attributes.Add("onclick", "return viewwindow_general('GC','" + GC_ID + "')");
            lbl_MR_No_Delivery.Attributes.Add("onclick", "return viewwindow_general('DLMR','" + MR_ID + "')");

            if (Export == false)
            {
                lbl_GC_No_Delivery.CssClass = "LEVEL2ITEMHOVER";
                lbl_MR_No_Delivery.CssClass = "LEVEL2ITEMHOVER";

                lbl_GC_No_Delivery.Font.Underline = true;
                lbl_MR_No_Delivery.Font.Underline = true;

                lbl_GC_No_Delivery.Font.Bold = true;
                lbl_MR_No_Delivery.Font.Bold = true;

                lbl_GC_No_Delivery.ForeColor = Color.Blue;
                lbl_MR_No_Delivery.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Cash_Amount_Delivery, lbl_Cheque_Amount_Delivery;

            lbl_Cash_Amount_Delivery = (Label)e.Item.FindControl("lbl_Cash_Amount");
            lbl_Cheque_Amount_Delivery = (Label)e.Item.FindControl("lbl_Cheque_Amount");

            lbl_Cash_Amount_Delivery.Text = Cash_Amount_Delivery.ToString();
            lbl_Cheque_Amount_Delivery.Text = Cheque_Amount_Delivery.ToString();
        }
    }

    protected void dg_Grid_Delivery_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Delivery.CurrentPageIndex = e.NewPageIndex;
        BindGrid_Delivery("form", e);
    }

    private void BindGrid_Delivery(object sender, EventArgs e)
    {
        ds_Delivery = null;
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_Delivery_currentpageindex = dg_Grid_Delivery.CurrentPageIndex;
        int grid_Delivery_PageSize = dg_Grid_Delivery.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_Delivery_currentpageindex = 0;
            grid_Delivery_PageSize = 0;
        }

        int Branch_id = Convert.ToInt32(ddl_Branch.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
           
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_Delivery_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_Delivery_PageSize)
        };

        objDAL.RunProc("EC_RPT_DELIVERY_BRANCHWISE_DAILY_COLLECTION_NANDWANA", objSqlParam, ref ds_Delivery);

        dg_Grid_Delivery.VirtualItemCount = Util.String2Int(ds_Delivery.Tables[2].Rows[0][0].ToString());
        string TotalRecords_Delivery = ds_Delivery.Tables[2].Rows[0][0].ToString();
        calculate_totals_Delivery();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid_Delivery, ds_Delivery.Tables[0], CallFrom, lbl_Error);

        Total_Delivery_Amount = Cash_Amount_Delivery + Cheque_Amount_Delivery;
        lbl_Total_Delivery_Amount.Text = Total_Delivery_Amount.ToString();

        if (CallFrom == "exporttoexcelusercontrol")
        {
            Session["ds_Delivery"] = ds_Delivery;
        }
    }

    private void calculate_totals_Delivery()
    {
        DataRow dr_Delivery = ds_Delivery.Tables[1].Rows[0];

        Cash_Amount_Delivery = Util.String2Decimal(dr_Delivery["Cash Amount"].ToString());
        Cheque_Amount_Delivery = Util.String2Decimal(dr_Delivery["Cheque Amount"].ToString());
    }


    //-----------------------------------------Expenses---------------------------------------------
    protected void dg_Grid_Expenses_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Voucher_ID;
            Label lbl_Voucher_ID_Expenses;

            Voucher_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_ID").ToString());

            lbl_Voucher_ID_Expenses = (Label)e.Item.FindControl("lbl_Voucher_No");

            lbl_Voucher_ID_Expenses.Attributes.Add("onclick", "return viewwindow_Voucher('" + Util.EncryptInteger(Voucher_ID) + "')");

            if (Export == false)
            {
                lbl_Voucher_ID_Expenses.CssClass = "LEVEL2ITEMHOVER";
                lbl_Voucher_ID_Expenses.Font.Underline = true;
                lbl_Voucher_ID_Expenses.Font.Bold = true;
                lbl_Voucher_ID_Expenses.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Amount_Expenses_Cash, lbl_Amount_Expenses_Chq;

            lbl_Amount_Expenses_Cash = (Label)e.Item.FindControl("lbl_Amount_Expenses_Cash");
            lbl_Amount_Expenses_Chq = (Label)e.Item.FindControl("lbl_Amount_Expenses_Chq");

            lbl_Amount_Expenses_Cash.Text = Amount_Expenses_Cash.ToString();
            lbl_Amount_Expenses_Chq.Text = Amount_Expenses_Chq.ToString();
        }
    }

    protected void dg_Grid_Expenses_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Expenses.CurrentPageIndex = e.NewPageIndex;
        BindGrid_Expenses("form", e);
    }

    private void BindGrid_Expenses(object sender, EventArgs e)
    {
        ds_Expenses = null;
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_Expenses_currentpageindex = dg_Grid_Expenses.CurrentPageIndex;
        int grid_Expenses_PageSize = dg_Grid_Expenses.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_Expenses_currentpageindex = 0;
            grid_Expenses_PageSize = 0;
        }

        int Branch_id = Convert.ToInt32(ddl_Branch.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),

            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_Expenses_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_Expenses_PageSize)
        };

        objDAL.RunProc("EC_RPT_EXPENSES_BRANCHWISE_DAILY_COLLECTION_NANDWANA", objSqlParam, ref ds_Expenses);

        dg_Grid_Expenses.VirtualItemCount = Util.String2Int(ds_Expenses.Tables[2].Rows[0][0].ToString());
        string TotalRecords_Expenses = ds_Expenses.Tables[2].Rows[0][0].ToString();
        calculate_totals_Expenses();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid_Expenses, ds_Expenses.Tables[0], CallFrom, lbl_Error);

        lbl_Total_Expenses_Amount.Text = Amount_Expenses.ToString();

        if (CallFrom == "exporttoexcelusercontrol")
        {
            Session["ds_Expenses"] = ds_Expenses;
        }
    }

    private void calculate_totals_Expenses()
    {
        DataRow dr_Expenses = ds_Expenses.Tables[1].Rows[0];

        Amount_Expenses_Cash = Util.String2Decimal(dr_Expenses["TotalCashAmount"].ToString());
        Amount_Expenses_Chq = Util.String2Decimal(dr_Expenses["TotalChqAmount"].ToString());
        Amount_Expenses = Util.String2Decimal(dr_Expenses["TotalExpenseAmt"].ToString());
    }


    //-----------------------------------------Income---------------------------------------------
    protected void dg_Grid_Income_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Voucher_ID;
            Label lbl_Voucher_ID_Income;

            Voucher_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_ID").ToString());

            lbl_Voucher_ID_Income = (Label)e.Item.FindControl("lbl_Voucher_No");

            lbl_Voucher_ID_Income.Attributes.Add("onclick", "return viewwindow_Voucher('" + Util.EncryptInteger(Voucher_ID) + "')");

            if (Export == false)
            {
                lbl_Voucher_ID_Income.CssClass = "LEVEL2ITEMHOVER";
                lbl_Voucher_ID_Income.Font.Underline = true;
                lbl_Voucher_ID_Income.Font.Bold = true;
                lbl_Voucher_ID_Income.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Amount_Income_Cash, lbl_Amount_Income_Chq;

            lbl_Amount_Income_Cash = (Label)e.Item.FindControl("lbl_Amount_Income_Cash");
            lbl_Amount_Income_Chq = (Label)e.Item.FindControl("lbl_Amount_Income_Chq");

            lbl_Amount_Income_Cash.Text = Amount_Income_Cash.ToString();
            lbl_Amount_Income_Chq.Text = Amount_Income_Chq.ToString();
        }
    }

    protected void dg_Grid_Income_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Income.CurrentPageIndex = e.NewPageIndex;
        BindGrid_Income("form", e);
    }

    private void BindGrid_Income(object sender, EventArgs e)
    {
        ds_Income = null;
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_Income_currentpageindex = dg_Grid_Income.CurrentPageIndex;
        int grid_Income_PageSize = dg_Grid_Income.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_Income_currentpageindex = 0;
            grid_Income_PageSize = 0;
        }

        int Branch_id = Convert.ToInt32(ddl_Branch.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),

            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_Income_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_Income_PageSize)
        };

        objDAL.RunProc("EC_RPT_INCOME_BRANCHWISE_DAILY_COLLECTION_NANDWANA", objSqlParam, ref ds_Income);

        dg_Grid_Income.VirtualItemCount = Util.String2Int(ds_Income.Tables[2].Rows[0][0].ToString());
        string TotalRecords_Income = ds_Income.Tables[2].Rows[0][0].ToString();
        calculate_totals_Income();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid_Income, ds_Income.Tables[0], CallFrom, lbl_Error);

        lbl_Total_Other_Income_Amount.Text = Amount_Income.ToString();

        if (CallFrom == "exporttoexcelusercontrol")
        {
            Session["ds_Income"] = ds_Income;
        }
    }

    private void calculate_totals_Income()
    {
        DataRow dr_Income = ds_Income.Tables[1].Rows[0];

        Amount_Income_Cash = Util.String2Decimal(dr_Income["TotalCashAmount"].ToString());
        Amount_Income_Chq = Util.String2Decimal(dr_Income["TotalChqAmount"].ToString());
        Amount_Income = Util.String2Decimal(dr_Income["TotalIncomeAmt"].ToString());
    }


    //-----------------------------------------Total---------------------------------------------
    private void BindGrid_Total(object sender, EventArgs e)
    {
        //ds_Total = null;
        DataSet ds_Balance = new DataSet();

        DataTable dt_Total = new DataTable();
        DataRow dr_Total;

        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int Branch_id = Convert.ToInt32(ddl_Branch.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,Year_Code),

            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.Char,25,"BO"),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,Branch_id),

            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date)
        };
        objDAL.RunProc("EC_RPT_CURRENT_BALANCE_AVAILABLE_NANDWANA", objSqlParam, ref ds_Balance);

        //decimal Total_Income = Total_Booking_Amount + Total_Delivery_Amount + Amount_Income;
        decimal Total_Income = Total_Booking_Amount + Total_Amount_Credit_Memo + Total_Delivery_Amount + Amount_Income;
        decimal Total_Expense = Amount_Expenses;
        decimal Total_Cash_Amount = (Cash_Amount_Booking + Cash_Amount_Delivery) - Amount_Expenses;
        decimal Total_Cheque_Amount = Cheque_Amount_Booking + Cheque_Amount_Delivery;

        dt_Total.TableName = "Table1";
        dt_Total.Columns.Add("Total_1");
        dt_Total.Columns.Add("Total_2");
        dt_Total.Columns.Add("Total_3");
        dt_Total.Columns.Add("Total_4");
        dt_Total.Columns.Add("Total_5");

        dr_Total = dt_Total.NewRow();

        dr_Total["Total_1"] = Total_Income;
        dr_Total["Total_2"] = Total_Expense;
        dr_Total["Total_3"] = Total_Cash_Amount;
        dr_Total["Total_4"] = Total_Cheque_Amount;
        dr_Total["Total_5"] = ds_Balance.Tables[0].Rows[0][0].ToString();

        dt_Total.Rows.Add(dr_Total);
        dt_Total.AcceptChanges();

        ds_Total.Tables.Add(dt_Total);
        ds_Total.AcceptChanges();

        dg_Grid_Total.DataSource = ds_Total;
        dg_Grid_Total.DataBind();

        if (CallFrom == "exporttoexcelusercontrol")
        {
            Session["ds_Total"] = ds_Total;
        }
    }

}
