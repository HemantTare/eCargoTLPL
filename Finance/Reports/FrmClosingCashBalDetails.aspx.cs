using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Finance_Reports_FrmClosingCashBalDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    int TotalPkgs, TotalCount, Total_Articles;
    decimal TotalCash, TotalAmount; 

    public string DetailType
    {
        get { return ViewState["_DetailType"].ToString(); }
        set { ViewState["_DetailType"] = value; }
    }
    public string BranchID
    {
        get { return ViewState["_BranchID"].ToString(); }
        set { ViewState["_BranchID"] = value; }
    }
    public DateTime AsOnDate
    {
        get { return Convert.ToDateTime(ViewState["_AsOnDate"]); }
        set { ViewState["_AsOnDate"] = value; }
    }


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {  
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "ClosingCashBalance"; 

        if (IsPostBack == false)
        {
            DetailType = Request.QueryString["DetailType"];
            BranchID = Request.QueryString["BranchID"];
            AsOnDate = Convert.ToDateTime(Request.QueryString["AsOnDate"]);

            if (DetailType == "Paid Booking ")
            {
                tr_PaidBooking.Visible = true;
                tr_ToPayRecovery.Visible = false;
                tr_OtherCashReceipt.Visible = false;
                tr_PendingPDS.Visible = false;
            }
            else if (DetailType == "To Pay Recovery (Cash) ")
            {
                tr_PaidBooking.Visible = false;
                tr_ToPayRecovery.Visible = true;
                tr_OtherCashReceipt.Visible = false;
                tr_PendingPDS.Visible = false; 
            }
            else if ((DetailType == "Other Cash Receipt ") || (DetailType == "Tempo Freight ") || (DetailType == "Other ") || (DetailType == "Cash Paid To Others ") || (DetailType == "Inter Branch Transfer ") || (DetailType == "Deposit In Bank ")) 
            {
                tr_PaidBooking.Visible = false;
                tr_ToPayRecovery.Visible = false;
                tr_OtherCashReceipt.Visible = true;
                tr_PendingPDS.Visible = false;
            }
            else if ((DetailType == "Total PDS ") || (DetailType == "PDS Freight (ToPay) "))
            {
                tr_PaidBooking.Visible = false;
                tr_ToPayRecovery.Visible = false;
                tr_OtherCashReceipt.Visible = false;
                tr_PendingPDS.Visible = true;
            }
            else if ((DetailType == "Cash ") || (DetailType == "Cheque ") || (DetailType == "Debit A/c ") || (DetailType == "Credit A/c ") || (DetailType == "Swipe Card ") || (DetailType == "Mobile Pay "))
            {
                tr_PaidBooking.Visible = false;
                tr_ToPayRecovery.Visible = true;
                tr_OtherCashReceipt.Visible = false;
                tr_PendingPDS.Visible = false;
            }

            BindGrid("form_and_pageload", e);
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
            DAL objDAL = new DAL();
            string CallFrom = (string)(sender);

            SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,BranchID), 
            objDAL.MakeInParams("@DetailType", SqlDbType.VarChar,100,DetailType), 
            objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,AsOnDate)  
        };

            objDAL.RunProc("FA_Opr_BranchWiseDailyClosingCash_Detailed_Report", objSqlParam, ref ds);

            calculate_totals();

            Common objcommon = new Common();
            if (DetailType == "Paid Booking ")
            {
                objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
            }
            else if (DetailType == "To Pay Recovery (Cash) ")
            {
                objcommon.ValidateReportForm(dg_GridToPayRecovery, ds.Tables[0], CallFrom, lbl_Error);
            }
            else if ((DetailType == "Other Cash Receipt ") || (DetailType == "Tempo Freight ") || (DetailType == "Other ") || (DetailType == "Cash Paid To Others ") || (DetailType == "Inter Branch Transfer ") || (DetailType == "Deposit In Bank ")) 
            {
                objcommon.ValidateReportForm(dg_GridOtherCashReceipt, ds.Tables[0], CallFrom, lbl_Error);
            }
            else if ((DetailType == "Total PDS ") || (DetailType == "PDS Freight (ToPay) "))
            {
                objcommon.ValidateReportForm(dg_PendingPDS, ds.Tables[0], CallFrom, lbl_Error);
            }
            else if ((DetailType == "Cash ") || (DetailType == "Cheque ") || (DetailType == "Debit A/c ") || (DetailType == "Credit A/c ") || (DetailType == "Swipe Card ") || (DetailType == "Mobile Pay "))
            {
                objcommon.ValidateReportForm(dg_GridToPayRecovery, ds.Tables[0], CallFrom, lbl_Error);
            }

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            } 
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        if (DetailType == "Paid Booking ")
        {
            TotalPkgs = Util.String2Int(dr["TotalPkgs"].ToString());
            TotalCash = Util.String2Decimal(dr["TotalCash"].ToString());
            TotalCount = Util.String2Int(ds.Tables[2].Rows[0]["TotalCount"].ToString());
        }
        else if (DetailType == "To Pay Recovery (Cash) ")
        {
            TotalAmount = Util.String2Decimal(dr["TotalAmount"].ToString());
            TotalCount = Util.String2Int(ds.Tables[2].Rows[0]["TotalCount"].ToString());
        }
        else if ((DetailType == "Other Cash Receipt ") || (DetailType == "Tempo Freight ") || (DetailType == "Other ") || (DetailType == "Cash Paid To Others ") || (DetailType == "Inter Branch Transfer ") || (DetailType == "Deposit In Bank ")) 
        {
            TotalAmount = Util.String2Decimal(dr["Amount"].ToString());
            TotalCount = Util.String2Int(ds.Tables[2].Rows[0]["TotalCount"].ToString());
        }
        else if ((DetailType == "Total PDS ") || (DetailType == "PDS Freight (ToPay) "))
        {
            TotalAmount = Util.String2Decimal(dr["Freight"].ToString());
            Total_Articles = Util.String2Int(dr["Total_Articles"].ToString());
            TotalCount = Util.String2Int(ds.Tables[2].Rows[0]["TotalCount"].ToString());
        }
        else if ((DetailType == "Cash ") || (DetailType == "Cheque ") || (DetailType == "Debit A/c ") || (DetailType == "Credit A/c ") || (DetailType == "Swipe Card ") || (DetailType == "Mobile Pay "))
        {
            TotalAmount = Util.String2Decimal(dr["TotalAmount"].ToString());
            TotalCount = Util.String2Int(ds.Tables[2].Rows[0]["TotalCount"].ToString());
        }
       
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();

        if (DetailType == "Paid Booking ")
        {
            dr["LRNo"] = "Total";
            dr["DlyBranch"] = TotalCount;
            dr["Pkgs"] = TotalPkgs;
            dr["Cash"] = TotalCash;
        }
        else if (DetailType == "To Pay Recovery (Cash) ")
        {
            dr["DlyType"] = "Total";
            dr["DlyNo"] = TotalCount;
            dr["TotalAmount"] = TotalAmount;
        }
        else if ((DetailType == "Other Cash Receipt ") || (DetailType == "Tempo Freight ") || (DetailType == "Other ") || (DetailType == "Cash Paid To Others ") || (DetailType == "Inter Branch Transfer ") || (DetailType == "Deposit In Bank ")) 
        {
            dr["VoucherType"] = "Total";
            dr["VoucherNo"] = TotalCount;
            dr["Amount"] = TotalAmount;
        }
        else if ((DetailType == "Total PDS ") || (DetailType == "PDS Freight (ToPay) "))
        {
            dr["PDS_No_For_Print"] = "Total";
            dr["PDS_Date"] = TotalCount;
            dr["Total_Articles"] = Total_Articles;
            dr["Freight"] = TotalAmount;
        }
        else if ((DetailType == "Cash ") || (DetailType == "Cheque ") || (DetailType == "Debit A/c ") || (DetailType == "Credit A/c ") || (DetailType == "Swipe Card ") || (DetailType == "Mobile Pay "))
        {
            dr["DlyType"] = "Total";
            dr["DlyNo"] = TotalCount;
            dr["TotalAmount"] = TotalAmount;
        }
        

        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            if (DetailType == "Paid Booking ")
            {
                Label lbl_TotalPkgs, lbl_TotalCash, lbl_Total_Count;

                lbl_TotalPkgs = (Label)e.Item.FindControl("lbl_TotalPkgs");
                lbl_TotalCash = (Label)e.Item.FindControl("lbl_TotalCash");
                lbl_Total_Count = (Label)e.Item.FindControl("lbl_Total_Count");

                lbl_TotalPkgs.Text = TotalPkgs.ToString();
                lbl_TotalCash.Text = TotalCash.ToString();
                lbl_Total_Count.Text = TotalCount.ToString();
            } 
        }
    }
 
    #endregion 

    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dg_GridToPayRecovery_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_GridToPayRecovery.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e); 
    }

    protected void dg_GridToPayRecovery_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            if ((DetailType == "To Pay Recovery (Cash) ") || (DetailType == "Cash ")  || (DetailType == "Cheque ") || (DetailType == "Debit A/c ") || (DetailType == "Credit A/c ") || (DetailType == "Swipe Card ") || (DetailType == "Mobile Pay "))
            {
                Label lbl_TotalAmount, lbl_ToPayTotal_Count;

                lbl_TotalAmount = (Label)e.Item.FindControl("lbl_TotalAmount");
                lbl_ToPayTotal_Count = (Label)e.Item.FindControl("lbl_ToPayTotal_Count");

                lbl_TotalAmount.Text = TotalAmount.ToString();
                lbl_ToPayTotal_Count.Text = TotalCount.ToString();

            }
        }
    }
    protected void dg_GridOtherCashReceipt_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_GridOtherCashReceipt.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e); 

    }
    protected void dg_GridOtherCashReceipt_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            if ((DetailType == "Other Cash Receipt ") || (DetailType == "Tempo Freight ") || (DetailType == "Other ") || (DetailType == "Cash Paid To Others ") || (DetailType == "Inter Branch Transfer ") || (DetailType == "Deposit In Bank ")) 
            {
                Label lbl_TotalOtherCashAmount, lbl_VoucherNoTotal_Count;

                lbl_TotalOtherCashAmount = (Label)e.Item.FindControl("lbl_TotalOtherCashAmount");
                lbl_VoucherNoTotal_Count = (Label)e.Item.FindControl("lbl_VoucherNoTotal_Count");

                lbl_TotalOtherCashAmount.Text = TotalAmount.ToString();
                lbl_VoucherNoTotal_Count.Text = TotalCount.ToString();

            }
        }

    }
    protected void dg_PendingPDS_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_PendingPDS.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e); 

    }
    protected void dg_PendingPDS_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            if ((DetailType == "Total PDS ") || (DetailType == "PDS Freight (ToPay) "))
            {
                Label lbl_Total_Articles, lbl_TotalFreight, lbl_PDS_Date;

                lbl_Total_Articles = (Label)e.Item.FindControl("lbl_Total_Articles");
                lbl_TotalFreight = (Label)e.Item.FindControl("lbl_TotalFreight");
                lbl_PDS_Date = (Label)e.Item.FindControl("lbl_PDS_Date");

                lbl_Total_Articles.Text = Total_Articles.ToString();
                lbl_TotalFreight.Text = TotalAmount.ToString();
                lbl_PDS_Date.Text = TotalCount.ToString();

            }
        }


    }
}
