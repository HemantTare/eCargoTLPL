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


public partial class Operations_GTLB_Loading_Frm_Rpt_GTLB_Loading_DateWise_Summary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int MonthId, Period;

    #endregion

   

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "GTLBLoadingSummary";

        MonthId  = Convert.ToInt32(Request.QueryString["MonthId"]);

        Period  = Convert.ToInt32(Request.QueryString["Period"]);

        Common objcommon = new Common();
        BindGrid("form_and_pageload", e);

    }
    
    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@MonthId", SqlDbType.Int,0,MonthId),
            objDAL.MakeInParams("@Period", SqlDbType.Int,0,Period)
        };

        objDAL.RunProc("[EC_Rpt_GTLB_LoadingDetails_DateWise_Summary]", objSqlParam, ref ds);

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
        dt.Columns.Remove("SrNo");
        dt.Columns.Remove("Date");
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
            _TableCell.Text = "THAPPI";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 4;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "LOADING";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 4;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "WARFER [MB]";
            _TableCell.HorizontalAlign = HorizontalAlign.Center;
            _TableCell.ColumnSpan = 3;
            _DataGridItem.Cells.Add(_TableCell);

            _TableCell = new TableCell();
            _TableCell.Text = "";
            _TableCell.HorizontalAlign = HorizontalAlign.Right;
            _TableCell.ColumnSpan = 1;
            _DataGridItem.Cells.Add(_TableCell);

            _DataGrid.Controls[0].Controls.AddAt(0, _DataGridItem);

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            string DateToDisplay;

            DateToDisplay = DataBinder.Eval(e.Item.DataItem, "DateToDisplay").ToString();


            if (DateToDisplay == "Total :")
            {
                e.Item.BackColor = System.Drawing.Color.LightSalmon;
                e.Item.Font.Bold = true;
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
