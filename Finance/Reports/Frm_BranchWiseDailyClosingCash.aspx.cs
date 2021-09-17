using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Finance_Reports_Frm_BranchWiseDailyClosingCash : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
  
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

       
        Wuc_Export_To_Excel1.FileName = "Branch Wise Daily Closing Cash";
        

        if (IsPostBack == false)
        {
            BindGrid("form_and_pageload", e);
        }
    }

    public void ValidateReportForm(GridView dg, DataTable dt,string CallFrom, Label lblError)
    {
        if (CallFrom != "exporttoexcelusercontrol")
        {
            dg.DataSource = dt;
            dg.DataBind();
        }

        if (dt.Rows.Count > 0)
        {
            lblError.Text = ""; 
            dg.Visible = true;
        }
        else
        {
            lblError.Text = "No Record(s) Found";
            dg.Visible = false;
        }
    }


    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";

        int Branch_id2 = Wuc_Region_Area_Branch1.BranchID;
        if (Branch_id2 <= 0)
        {
            lbl_Error.Text = "Please Select One Branch";
        }
        else
        {

            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.PageIndex = 0;
            BindGrid("form", e);
        }

    }
    private void BindGrid(object sender, EventArgs e)
    {
       
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.PageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
             objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,To_date)
        };

        objDAL.RunProc("FA_Rpt_BranchWiseDailyCashBalance", objSqlParam, ref ds);


        if (CallFrom == "form_and_pageload") return;

       

        ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }



    private void PrepareDTForExportToExcel()
    { 
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
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

   
    protected void dg_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg_Grid.PageIndex = e.NewPageIndex; 
        BindGrid("form", e); 
    }

    protected void dg_Grid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;

            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell.Text = "Date";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Opening";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Receipt";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Payment";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Balance";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "NonCashReceipt";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            dg_Grid.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }

}
