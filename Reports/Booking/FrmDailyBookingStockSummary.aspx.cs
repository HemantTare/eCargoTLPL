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


public partial class Reports_Booking_FrmDailyBookingStockSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    #endregion

    public int RegionID
    {
        get { return Convert.ToInt32(ViewState["_RegionID"]); }
        set
        {
            ViewState["_RegionID"] = value;
        }
    }

    public int AreaID
    {
        get { return Convert.ToInt32(ViewState["_AreaID"]); }
        set { ViewState["_AreaID"] = value; }
    }

    public int BranchID
    {
        get { return Convert.ToInt32(ViewState["_BranchID"]); }
        set { ViewState["_BranchID"] = value; }
    }

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DailyBookingStockSummary";

        RegionID = Convert.ToInt32(Request.QueryString["Region_ID"]);

        AreaID = Convert.ToInt32(Request.QueryString["Area_ID"]);

        BranchID = Convert.ToInt32(Request.QueryString["Branch_ID"]);

        Common objcommon = new Common();
        BindGrid("form_and_pageload", e);

    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@BkgRegion_ID", SqlDbType.Int,0,RegionID ),
            objDAL.MakeInParams("@BkgArea_ID", SqlDbType.Int,0,AreaID ),
            objDAL.MakeInParams("@BkgBranch_ID", SqlDbType.Int,0,BranchID ), 
        };


        objDAL.RunProc("[EC_RPT_Current_Stock_OMBharat_Summary]", objSqlParam, ref ds);


        // if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[0].Rows.Count.ToString();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void PrepareDTForExportToExcel()
    {
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Copy();
        dt.Columns.Remove("Region_Id");
        dt.Columns.Remove("Area_Id");
        dt.Columns.Remove("BkgBranchID");
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
            _TableCell.Text = "Bkg Branch";
            _TableCell.HorizontalAlign = HorizontalAlign.Left;
            _TableCell.ColumnSpan = 1;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "SURAT";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 2;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "VAPI";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 2;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "GUJARAT TOTAL";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 2;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "PUNE";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 2;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "TOTAL";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 2;
            _DataGridItem.Cells.Add(_TableCell);

            _DataGrid.Controls[0].Controls.AddAt(0, _DataGridItem);
        }


        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            string Branch_Name;

            int Region_Id, Area_Id, Branch_Id;
            LinkButton lbtn_Branch;

            Branch_Name = DataBinder.Eval(e.Item.DataItem, "Branch_Name").ToString();

            lbtn_Branch = (LinkButton)e.Item.FindControl("lbtn_Branch");

            Region_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Region_Id").ToString());
            Area_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Area_Id").ToString());
            Branch_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "BkgBranchID").ToString());


            if (Branch_Name == "Total")
            {
                e.Item.BackColor = System.Drawing.Color.Pink;
                e.Item.Font.Bold = true;
            }
            else if (Branch_Name == "GrandTotal")
            {
                e.Item.BackColor = System.Drawing.Color.Cyan;
                e.Item.Font.Bold = true;
            }
            else
            {
                e.Item.Cells[1].BackColor = System.Drawing.Color.Bisque;
                e.Item.Cells[2].BackColor = System.Drawing.Color.Bisque;

                e.Item.Cells[3].BackColor = System.Drawing.Color.Khaki;
                e.Item.Cells[4].BackColor = System.Drawing.Color.Khaki;


                e.Item.Cells[5].BackColor = System.Drawing.Color.Gold;
                e.Item.Cells[6].BackColor = System.Drawing.Color.Gold;

                e.Item.Cells[7].BackColor = System.Drawing.Color.Aquamarine;
                e.Item.Cells[8].BackColor = System.Drawing.Color.Aquamarine;

                e.Item.Cells[9].BackColor = System.Drawing.Color.Wheat;
                e.Item.Cells[10].BackColor = System.Drawing.Color.Wheat;
            }

            if (Branch_Id > 0)
            {
                StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
                PathDlyStk.Append("/");
                PathDlyStk.Append("Reports/Booking/FrmDailyBookingStock.aspx?Region_Id=" + ClassLibraryMVP.Util.EncryptInteger(Region_Id)
                    + "&Area_Id=" + ClassLibraryMVP.Util.EncryptInteger(Area_Id) + "&Branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_Id)
                    + "&Branch_Name=" + ClassLibraryMVP.Util.EncryptString(Branch_Name) + "&FromSummary=1");

                lbtn_Branch.Attributes.Add("onclick", "return viewwindow_ParentWindow('" + PathDlyStk + "')");
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
