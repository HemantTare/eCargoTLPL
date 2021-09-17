using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC;
//using Raj.eCargo.Init;
//using ClassLibrary;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Infragistics.WebUI.UltraWebGrid;

public partial class CRM_WucChart : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    DataSet dsGridDetails;
    DataSet dsPieGrid ;
    DataSet ds;

    int  Link_ID;
    string Rpt, LinkId, Type, Link, flag;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Link_ID = Convert.ToInt32(StateManager.GetState<string>("QueryString"));
        Link_ID = Common.GetMenuItemId();

        ViewState["Link_ID"] = Link_ID;

        if (Convert.ToInt32(ViewState["Link_ID"].ToString()) != 126)
        { tbl_rangetype.Visible=false ; }
        else
        { tbl_rangetype.Visible = true ; }

        if (!IsPostBack)
        {
            From_Date.SelectedDate = DateTime.Now.Date;
            To_Date.SelectedDate = DateTime.Now.Date;
            Fill_Complaint();
            Fill_Grid(0, 0);
            Set_Label();
        }
    }

     private void Set_Link_Details()
     {

        Rpt = ClassLibraryMVP.Util.EncryptString("");
        flag = ClassLibraryMVP.Util.EncryptString("operation");
        Link = ClassLibraryMVP.Util.EncryptString("Pickup Request");
        LinkId = ClassLibraryMVP.Util.EncryptInteger(10002);
        Type = ClassLibraryMVP.Util.EncryptString("Pickup Request VT");
     }

    private void Fill_Grid(int location_Id, int ReportID)
    {
         DAL objDAL = new DAL();
         DataSet dsgrid = new DataSet();
         dsgrid = null;
         Session["GridDetails"] = null;
         int Range_Type;

         int Report_ID = 0;
         if (location_Id > 0 && ReportID > 0)
         {
             Report_ID = ReportID;
         }
         else
         {
             Report_ID = Convert.ToInt32(ddl_ChartComplaint.SelectedValue);
         }

         if (rbtn_Range_Type.SelectedValue == "0")
         {
             Range_Type = 1;
         }
         else
         {
             Range_Type = 2;
         }
        SqlParameter[] sqlpar = {
              objDAL.MakeInParams("@Report_ID", SqlDbType.Int, 0,Report_ID),
              objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,Convert.ToInt16(UserManager.getUserParam().YearCode)),
              objDAL.MakeInParams("@From_Date", SqlDbType.DateTime, 0, From_Date.SelectedDate),
              objDAL.MakeInParams("@To_Date", SqlDbType.DateTime, 0, To_Date.SelectedDate),
              objDAL.MakeInParams("@LocationID", SqlDbType.Int, 0, location_Id),
              objDAL.MakeInParams("@DateRange_Type_ID", SqlDbType.Int,0,Range_Type) };

        objDAL.RunProc("dbo.EC_Rpt_CRM_Dashboard_Reports", sqlpar, ref dsgrid);
       

        if (dsgrid.Tables[0].Rows.Count > 0)
        {
            int ChartTypeID = Convert.ToInt32(dsgrid.Tables[2].Rows[0]["Chart_Type"].ToString());
            SetChartType = ChartTypeID;
            Session["ChartType"] = ChartTypeID;
            BindChart = dsgrid.Tables[0];
        }

        BindChartGrid = dsgrid.Tables[0];
        BindGridDetails = dsgrid.Tables[1];
        Session["GridDetails"] = dsgrid;
    }

    #region "BTN Click and DDL Select"

    protected void ddl_ChartComplaint_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Grid(0,0);
    }

    protected void btn_Display_Click(object sender, EventArgs e)
    {
        Fill_Grid(0,0);
    }

    #endregion

    #region "BindChartGrid"

    public DataTable BindChartGrid
    {
        set
        {
            PieChartGrid.Clear();
            PieChartGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
            PieChartGrid.DataSource = value;
            PieChartGrid.DataBind();
        }
    }

    public DataTable BindGridDetails
    {
        set
        {
            GridDetails.Clear();
            GridDetails.DisplayLayout.Bands[0].SortedColumns.Clear();
            GridDetails.DataSource = value;
            GridDetails.DataBind();
        }
    }

    public DataTable BindChart
    {
        set
        {
            cht_PieChart.Data.DataSource = value;
            cht_PieChart.Data.DataBind();
        }
    }

  
    #endregion

    #region "SetChartType and FillDDL"

    private void Fill_Complaint()
    {
        ddl_ChartComplaint.DataSource = objCommon.Get_Values_Where("EC_CRM_Dashboard_Reports", "Report_ID,Report_Name,Chart_Type", "Link_ID = " + Link_ID, "Report_ID", false);
        ddl_ChartComplaint.DataValueField = "Report_ID";
        ddl_ChartComplaint.DataTextField = "Report_Name";
        ddl_ChartComplaint.DataBind();
    }

    private int SetChartType
    {
        set
        {
            if (value == 1)
            {
                cht_PieChart.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.PieChart3D;
                cht_PieChart.Data.SwapRowsAndColumns = false;
                cht_PieChart.Data.ZeroAligned = false;
            }
            else if (value == 2)
            {
                cht_PieChart.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.ColumnChart;
                cht_PieChart.Data.SwapRowsAndColumns = true;
                cht_PieChart.Data.ZeroAligned = true;
            }
            else if (value == 3)
            {
                cht_PieChart.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.PieChart;
            }
            else if (value == 4)
            {
                cht_PieChart.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.StackBarChart;
            }
        }      
    }

    #endregion

    #region "Grid Page index"

    protected void GridDetails_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        GridDetails.DisplayLayout.Bands[0].Columns[1].Hidden = true;
    }

    protected void GridDetails_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        Set_Link_Details();

        if (Convert.ToInt32(ViewState["Link_ID"].ToString()) != 127)
        {
            e.Row.Cells.FromKey("Ticket No").Value = "<a href=# style='text-decoration:none;cursor:hand;z-index:200' onClick=" + (char)34 + "window.open('../Transaction/FrmTicketHistory.aspx?Id=" + ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(e.Row.Cells.FromKey("ticket_id").Value)) + "&Type=TwB0AGgAZQByAA==', 'HistoryTicket', 'toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=yes, width=850, height=600,left=50,top=20')" + (char)34 + ">" + e.Row.Cells.FromKey("Ticket No").Value.ToString() + "</a>";
            e.Row.Cells.FromKey("View").Value = "<a href=# style='text-decoration:none;cursor:hand;z-index:100' onClick=" + (char)34 + "window.open('../Transaction/FrmDisplayInfo.aspx?Ticket_Id=" + ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(e.Row.Cells.FromKey("ticket_id").Value)) + "&Type=QQBzAHMAaQBnAG4AZQBkAFUAcwBlAHIASQBuAGYAbwA=', 'HistoryUser', 'toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=yes, width=850, height=600,left=50,top=20')" + (char)34 + ">" + e.Row.Cells.FromKey("View").Value.ToString() + "</a>";
        }
        else
        {
            e.Row.Cells.FromKey("Pickup No").Value = "<a href=# style='text-decoration:none;cursor:hand' onClick=" + (char)34 + "window.open('../../ViewDetails/frm_View_Mst_And_Details.aspx?id=" + ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(e.Row.Cells.FromKey("pickup_id").Value)) + "&Rpt=" + Rpt + "&LinkId=" + LinkId + "&Type=" + Type + "&Link=" + Link + "&flag=" + flag + "', 'HistoryTicket', 'toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=yes, width=850, height=600,left=50,top=20')" + (char)34 + ">" + e.Row.Cells.FromKey("Pickup No").Value.ToString() + "</a>";
        }
    }

    protected void GridDetails_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        dsGridDetails = (DataSet)Session["GridDetails"];
        if (dsGridDetails != null)
        {
            GridDetails.DataSource = dsGridDetails.Tables[1];
            GridDetails.DataBind();
        }
    }

    protected void PieChartGrid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        dsPieGrid = (DataSet)Session["GridDetails"];
        if (dsPieGrid != null)
        {
            PieChartGrid.DataSource = dsPieGrid.Tables[0];
            PieChartGrid.DataBind();
        }
    }
     
    protected void GridDetails_PageIndexChanged(object sender, PageEventArgs e)
    {
        dsGridDetails = (DataSet)Session["GridDetails"];
        GridDetails.DataSource = dsGridDetails.Tables[1];
        GridDetails.DataBind();
    }
    protected void PieChartGrid_PageIndexChanged(object sender, PageEventArgs e)
    {
        dsPieGrid = (DataSet)Session["GridDetails"];
        PieChartGrid.DataSource = dsPieGrid.Tables[0];
        PieChartGrid.DataBind();
    }
    #endregion

    #region "Set Label"
    private void Set_Label()
    {
        if (Convert.ToInt16(ViewState["Link_ID"]) == 126)
        {
            lbl_Select.Text = "Select Complaint By:";
        }
        else if (Convert.ToInt16(ViewState["Link_ID"]) == 127)
        {
            lbl_Select.Text = "Select Pickup By:";
        }
        else if (Convert.ToInt16(ViewState["Link_ID"]) == 128)
            lbl_Select.Text = "Select Fault By:";
    }
    # endregion

    #region "Export Button"

    protected void IB_ExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        dsGridDetails= (DataSet)Session["GridDetails"];
        if (dsGridDetails != null)
        {
            if (dsGridDetails.Tables[1].Rows.Count > 0)
            {
                ExportGrid.DataSource = dsGridDetails.Tables[1];
                ExportGrid.DataBind();
                ExportGrid.DisplayLayout.Bands[0].Columns[1].Hidden = true;
                if (Convert.ToInt32(ViewState["Link_ID"].ToString()) != 127)
                {
                    ExportGrid.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                }
                UltraWebGridExcelExporter1.Export(ExportGrid);
             }
        }
    }

#endregion

    protected void cht_PieChart_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
    {
        int LocationId = 0;
        string Location = "";
        
        if (Convert.ToInt32(Session["ChartType"].ToString()) == 1)
        {
           Location = e.RowLabel;
        }
        else if (Convert.ToInt32(Session["ChartType"].ToString()) == 2)
        {
            Location = e.ColumnLabel;
        }

        if (Convert.ToInt32(ddl_ChartComplaint.SelectedValue) == 4)
        {
            ds = objCommon.Get_Values_Where("EC_Master_Region", "Region_ID", "Region_Name Like '" + Location + "%'", "Region_ID", false);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LocationId = Convert.ToInt32(ds.Tables[0].Rows[0]["Region_ID"].ToString());
                Fill_Grid(LocationId, 5);
            }
            else
                Fill_Grid(0, 5);
        }
        else if (Convert.ToInt32(ddl_ChartComplaint.SelectedValue) == 5)
        {
            ds = objCommon.Get_Values_Where("EC_Master_Area", "Area_ID", "Area_Name Like '" + Location + "%'", "Area_ID", false);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LocationId = Convert.ToInt32(ds.Tables[0].Rows[0]["Area_ID"].ToString());
                Fill_Grid(LocationId, 6);
            }
            else
                Fill_Grid(0, 6);
        }
        else
            Fill_Grid(0, 0);
    }
    protected void rbtn_Range_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["GridDetails"] = null;
        Fill_Grid(0, 0);
    }
}
