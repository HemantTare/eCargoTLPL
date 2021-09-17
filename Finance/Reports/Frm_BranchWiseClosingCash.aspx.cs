using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Finance_Reports_Frm_BranchWiseClosingCash : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth,dsYear;

    HiddenField hdf_BranchId1;
    HiddenField hdf_BranchId2;

    LinkButton lbtn_BranchName;
    LinkButton lbtn_BranchName2;

    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

       
        Wuc_Export_To_Excel1.FileName = "Branch Wise Closing Cash";
        

        //if (IsPostBack == false)
        //{
            BindGrid("form_and_pageload", e); 
        //}
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

        DateTime As_On_Date = DateTime.Now;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,As_On_Date)
        };

        objDAL.RunProc("FA_Rpt_BranchWiseCashBalanceNew", objSqlParam, ref ds);
        

        //if (CallFrom == "form_and_pageload") return;

       

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

            HeaderCell.Text = "MAHARASHTRA";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.RowSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.Black;
            HeaderCell.ForeColor = System.Drawing.Color.Yellow;
            HeaderCell.Font.Size = 14;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GUJRAT";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.Black;
            HeaderCell.ForeColor = System.Drawing.Color.Yellow;
            HeaderCell.Font.Size = 14;
            HeaderGridRow.Cells.Add(HeaderCell);

            dg_Grid.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow2 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            
            HeaderCell = new TableCell();
            HeaderCell.Text = "Branch";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Opening";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Receipt";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Payment";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Closing";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Divider";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.White;

            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Branch";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Opening";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Receipt";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Payment";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Closing";
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderGridRow2.Cells.Add(HeaderCell);

            dg_Grid.Controls[0].Controls.AddAt(1, HeaderGridRow2);


        }
    }

    protected void dg_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string DetailUrl1, DetailUrl2;

            int Region_Id = 0;
            int Area_Id = 0;
            int Branch_Id = 0;

            DateTime Booking_Date = DateTime.Today;
            DateTime Today = Booking_Date.Date;

            int Menu_Item_Id = 301;

            hdf_BranchId1 = (HiddenField)e.Row.FindControl("hdfn_BranchId");
            hdf_BranchId2 = (HiddenField)e.Row.FindControl("hdfn_BranchId2");

            lbtn_BranchName = (LinkButton)e.Row.FindControl("lbtn_BranchName");
            lbtn_BranchName2 = (LinkButton)e.Row.FindControl("lbtn_BranchName2");

            DetailUrl1 = ClassLibraryMVP.Util.GetBaseURL() +
                         "/Finance/Reports/FrmDailyCashBook.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
                         "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_Id) + "&Branch_id=" + Util.EncryptInteger(Util.String2Int(hdf_BranchId1.Value)) + "&Branch_Name=" + Util.EncryptString(lbtn_BranchName.Text) + "&From_Date=" + Today + "&To_Date=" + Today;

            DetailUrl2 = ClassLibraryMVP.Util.GetBaseURL() +
                 "/Finance/Reports/FrmDailyCashBook.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
                 "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_Id) + "&Branch_id=" + Util.EncryptInteger(Util.String2Int(hdf_BranchId2.Value)) + "&Branch_Name=" + Util.EncryptString(lbtn_BranchName2.Text) + "&From_Date=" + Today + "&To_Date=" + Today;

            
            lbtn_BranchName.Attributes.Add("onclick", "return GridDetails('" + DetailUrl1 + "');");

            lbtn_BranchName2.Attributes.Add("onclick", "return GridDetails('" + DetailUrl2 + "');");


        }
    }
}
