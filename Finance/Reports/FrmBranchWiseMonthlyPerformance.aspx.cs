using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Finance_Reports_FrmBranchWiseMonthlyPerformance : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth;
    decimal ChargedWeight, Actual_Weight, Articles, Basic_Freight, FOV_Charges, Discount;
    decimal ODA_Charges,Other_Charges,Sub_Freight,STax_Amt,Total_Freight,Invoice_Value;
    decimal Hamali_Charge,DD_Charge,Bilti_Charges;
    int TotalGC;
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (rdl_BookingRegister.SelectedValue == "0")
            Wuc_Export_To_Excel1.FileName = "BookingBranchWise";
        else
            Wuc_Export_To_Excel1.FileName = "DeliveryBranchWise";


        if (IsPostBack == false)
        {  
             
            SetStandardCaptionForGrid(dg_Grid);
            Fill_AllDropDown();
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

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.PageIndex = 0;
            BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_Grid.Visible = false;
        //}
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
        Boolean IsBookingBranchWise;

        if (Convert.ToInt32(rdl_BookingRegister.SelectedValue) > 0)
        {
            IsBookingBranchWise = false;
         }
        else
        {
            IsBookingBranchWise = true;
        }

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MonthID),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,UserManager.getUserParam().StartDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,UserManager.getUserParam().EndDate),
            objDAL.MakeInParams("@IsBookingBranchWise",SqlDbType.Bit,0,IsBookingBranchWise)
             
        };



        objDAL.RunProc("EC_RPT_Branch_Monthly_Performance_GRD", objSqlParam, ref ds);
        

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

    protected void dg_Grid_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        dg_Grid.Columns[0].HeaderText = "Branch Name";
 
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
            
            HeaderCell.Text = "Branch Name";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.RowSpan = 2;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center; 
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Selected Month";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center; 
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Previous Monthly Avg";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center; 
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Difference";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center; 
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);

            dg_Grid.Controls[0].Controls.AddAt(0, HeaderGridRow);


            GridViewRow HeaderGridRow2 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
           
            HeaderCell = new TableCell();
            HeaderCell.Text = "LR";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Articles";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Freight";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "LR";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Articles";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Freight";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "LR";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Articles";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Freight";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.LightSkyBlue;
            HeaderGridRow2.Cells.Add(HeaderCell);


            dg_Grid.Controls[0].Controls.AddAt(1, HeaderGridRow2);

        }
    }
    protected void dg_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TableCell myCell = new TableCell();
        //    myCell = new TableCell();
        //    myCell.Text = e.Row.DataItemIndex.ToString();
        //    myCell.Style["color"] = decimal.Parse(myCell.Text) < 0 ? "Red" : "OtherColor";
        //    e.Row.Cells.Add(myCell);
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((Convert.ToDecimal(e.Row.Cells[9].Text)) < 0)
            {
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
            }
            if ((Convert.ToDecimal(e.Row.Cells[8].Text)) < 0)
            {
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
            }
            if ((Convert.ToDecimal(e.Row.Cells[7].Text)) < 0)
            {
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[0].Text == "Total :")
            {
                e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                e.Row.Font.Bold = true;
            }
            //Total :
        }
    }
}
