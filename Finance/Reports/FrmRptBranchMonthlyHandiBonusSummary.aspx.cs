using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Finance_Reports_FrmRptBranchMonthlyHandiBonusSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth,dsYear;
  
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

       
        Wuc_Export_To_Excel1.FileName = "Handi Bonus Summary";
        


        if (IsPostBack == false)
        {

            SetStandardCaptionForGrid(dg_Grid);
            Fill_AllDropDown(); 
            Fill_Year();
            BindGrid("form_and_pageload", e); 
        }
    }

    public void SetStandardCaptionForGrid(GridView dg_Grid)
    {
        string col_header = "";

        for (int i = 0; i <= dg_Grid.Columns.Count - 1; i++)
        {
            col_header = dg_Grid.Columns[i].HeaderText;

            if (col_header.ToLower().Contains("gc_caption"))
            {
                dg_Grid.Columns[i].HeaderText = col_header.Replace("gc_caption", CompanyManager.getCompanyParam().GcCaption);
            }
            else if (col_header.ToLower().Contains("lhpo_caption"))
            {
                dg_Grid.Columns[i].HeaderText = col_header.Replace("lhpo_caption", CompanyManager.getCompanyParam().LHPOCaption);
            }
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

    private void Fill_AllDropDown()
    {
        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Finacial_Month", objSqlParam, ref dsMonth);

        ddl_Month.DataSource = dsMonth;
        ddl_Month.DataTextField = "MonthName";
        ddl_Month.DataValueField = "MonthID";
        ddl_Month.DataBind();



    }

    private void Fill_Year()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@YearID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Year", objSqlParam, ref dsYear);

        ddl_Year.DataSource = dsYear;
        ddl_Year.DataTextField = "YearName";
        ddl_Year.DataValueField = "YearID";
        ddl_Year.DataBind();
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";

        int Branch_id2 = Wuc_Region_Area_Branch1.BranchID;

            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.PageIndex = 0;
            BindGrid("form", e);

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
        int MonthID = Convert.ToInt32(ddl_Month.SelectedItem.Value);
        int Year = Convert.ToInt32(ddl_Year.SelectedItem.Value); 

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Regionid", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Areaid", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branchid", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MonthID),
            objDAL.MakeInParams("@Year", SqlDbType.Int,0,Year)
        };

        objDAL.RunProc("EC_Rpt_Branch_HandiBonusSummary", objSqlParam, ref ds);
        

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

            HeaderCell.Text = "Branch";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Dest1Amount";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Dest2Amount";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TotalAmount";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);
            
            HeaderCell = new TableCell();
            HeaderCell.Text = "AllAmount";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            dg_Grid.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }

    protected void dg_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "Total ")
            {
                e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                e.Row.Font.Bold = true;
            }

        }
    }    
}
