using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_CL_Nandwana_User_Desk_FrmUserDeskDlyBranchToPayRecovery : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int BranchID;

    #endregion



    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DlyBranchToPayRecovery";

        BranchID = Convert.ToInt32(Request.QueryString["BranchID"]);

        Common objcommon = new Common();

        if (!IsPostBack)
        {
            BindGrid("form_and_pageload", e);
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,BranchID)
        };

        objDAL.RunProc("[EC_Opr_Dly_BranchWise_Total_Recovery]", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
            string TotalRecords = ds.Tables[0].Rows.Count.ToString();

            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            }
        }
    }

    private void PrepareDTForExportToExcel()
    {
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Copy();
        dt.Columns.Remove("Branch_ID");
        Wuc_Export_To_Excel1.has_last_row_as_total = false;
        Wuc_Export_To_Excel1.SessionExporttoExcel = dt;
    }


    #endregion


    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Header)
        {
            DataGrid _DataGrid = (DataGrid)sender;
            DataGridItem _DataGridItem = new DataGridItem(0, 0, ListItemType.Header);
            TableCell _TableCell = new TableCell();

            //---- Create Header------

            _TableCell = new TableCell();
            _TableCell.Text = "";
            _TableCell.HorizontalAlign = HorizontalAlign.Left;
            _TableCell.ColumnSpan = 1;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "DLY STOCK";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 3;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "PENDING PDS";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 3;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "PENDING FRT";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 3;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "TOTAL";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 3;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "";
            _TableCell.HorizontalAlign = HorizontalAlign.Right;
            _TableCell.ColumnSpan = 2;
            _DataGridItem.Cells.Add(_TableCell);
            _TableCell.BackColor = System.Drawing.Color.Gold;

            _DataGrid.Controls[0].Controls.AddAt(0, _DataGridItem);
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            string DateToDisplay;

            DateToDisplay = DataBinder.Eval(e.Item.DataItem, "Branch_Name").ToString();

            if (DateToDisplay == "Total")
            {
                e.Item.BackColor = System.Drawing.Color.Aquamarine;
                e.Item.Font.Bold = true;
            }

            e.Item.Cells[13].BackColor = System.Drawing.Color.Gold;

            LinkButton lnk_StockFrt, lnk_PDSFrt, lnk_PendingFrtFrt, lnk_ClosingCash;

            lnk_StockFrt = (LinkButton)e.Item.FindControl("lnk_StockFrt");
            lnk_PDSFrt = (LinkButton)e.Item.FindControl("lnk_PDSFrt");
            lnk_PendingFrtFrt = (LinkButton)e.Item.FindControl("lnk_PendingFrtFrt");
            lnk_ClosingCash = (LinkButton)e.Item.FindControl("lnk_ClosingCash");

            string Branch_ID, Branch_Name;

            Branch_ID = DataBinder.Eval(e.Item.DataItem, "Branch_ID").ToString();
            Branch_Name = DataBinder.Eval(e.Item.DataItem, "Branch_Name").ToString();

            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");

            if (Branch_ID == "0")
            {
                PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock.aspx?BranchID=0");
                lnk_StockFrt.Attributes.Add("onclick", "return PendingDlyStock('" + PathDlyStk + "')");
            }
            else
            {
                PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock.aspx?BranchID=" + Branch_ID);
                lnk_StockFrt.Attributes.Add("onclick", "return PendingDlyStock('" + PathDlyStk + "')");

            }

            StringBuilder PathPendingPDS = new StringBuilder(Util.GetBaseURL());
            PathPendingPDS.Append("/");
            if (Branch_ID == "0")
            {
                PathPendingPDS.Append("Reports/CL_Nandwana/User Desk/Frm_Pending_PDS_Summary.aspx?Branch_ID=0&Area_ID=0&Region_ID=0");
                lnk_PDSFrt.Attributes.Add("onclick", "return PendingPDSSummary('" + PathPendingPDS + "')");
            }
            else
            {
                PathPendingPDS.Append("Reports/CL_Nandwana/User Desk/FrmPendingPDSList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=0&Region_ID=0");
                lnk_PDSFrt.Attributes.Add("onclick", "return PendingPDSSummary('" + PathPendingPDS + "')");
            }

            StringBuilder PathPendingFrt = new StringBuilder(Util.GetBaseURL());
            PathPendingFrt.Append("/");
            if (Branch_ID == "0")
            {
                PathPendingFrt.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliverySummary.aspx?Branch_ID=0&Area_ID=0&Region_ID=0");
                lnk_PendingFrtFrt.Attributes.Add("onclick", "return PendingFRTSummary('" + PathPendingFrt + "')");
            }
            else
            {
                PathPendingFrt.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliveryConsigneeSummary.aspx?Delivery_branch_Id=" + Util.EncryptString(Branch_ID) + "&DlyBranch=" + Util.EncryptString(Branch_Name) + "&IsDetailed=True");
                lnk_PendingFrtFrt.Attributes.Add("onclick", "return PendingFRTSummary('" + PathPendingFrt + "')");
            }



            string DetailUrl;
            int Region_Id = 0;
            int Area_Id = 0;

            DateTime Booking_Date = DateTime.Today;
            DateTime Today = Booking_Date.Date;

            int Menu_Item_Id = 301;

            //DetailUrl = ClassLibraryMVP.Util.GetBaseURL() +
            //    "/Finance/Reports/FrmDailyCashBook.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
            //    "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_Id) + "&Branch_id=" + Util.EncryptInteger(Util.String2Int(Branch_ID)) + "&Branch_Name=" + Util.EncryptString(Branch_Name) + "&From_Date=" + Today + "&To_Date=" + Today;

            //if (Util.String2Int(Branch_ID) > 0)
            //{
            //    lnk_ClosingCash.Attributes.Add("onclick", "return GridDetails('" + DetailUrl + "');");
            //}

            DetailUrl = ClassLibraryMVP.Util.GetBaseURL() +
            "/Finance/Reports/FrmClosingCashBalNew.aspx?Branch_id=" + Util.EncryptInteger(Util.String2Int(Branch_ID)) + "&Date=" + Today + "&IsFromUserDesk=1" + "&Branch_Name=" + Util.EncryptString(Branch_Name);

            if (Util.String2Int(Branch_ID) > 0)
            {
                lnk_ClosingCash.Attributes.Add("onclick", "return GridDetails('" + DetailUrl + "');");
            }

        }
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

}
