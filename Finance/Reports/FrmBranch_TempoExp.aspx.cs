using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Finance_Reports_FrmBranch_TempoExp : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    decimal Articles, Sub_Total, Basic_Freight, STax_Amt, Total_Freight, GC_Amount, Discount;
    int NoOf_GC;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "BranchWiseFreightVsTempoExpense";
        Wuc_Region_Area_Branch1.SetRegionCaption = "Delivery Region";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Delivery Area";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Delivery Branch";

        if (IsPostBack == false)
        { 

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            //BindGrid("form_and_pageload", e); 
        }
    }
    
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        if (Branch_id == 0)
        {

            msg = "Please Select One Branch";
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;  
        }
        else if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        
            SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Todate ", SqlDbType.DateTime,0,To_date) 
        };

            objDAL.RunProc("EC_RPT_Branch_TempoExp_GRD", objSqlParam, ref ds);

            if (CallFrom == "form_and_pageload") return;

            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
            string TotalRecords = ds.Tables[0].Rows[0][0].ToString();
            //calculate_totals();

            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[1], CallFrom, lbl_Error, TotalRecords);

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            } 
    }

    private void PrepareDTForExportToExcel()
    {
        //string BranchName = Wuc_Region_Area_Branch1.SelectedBranchText;
        //DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        //DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        //DataSet DSTemp = new DataSet();
        //DataTable DTTemp = new DataTable();
        //DTTemp.Columns.Add("Selection Criteria", typeof(string));
        //DataRow dr1 = DTTemp.NewRow();
        //dr1["Selection Criteria"] = "Branch Name :" + BranchName + " From Date: " + From_Date.ToString("dd-MMM-yyyy") + " - " + " To Date: " + To_date.ToString("dd-MMM-yyyy");
        //DTTemp.Rows.Add(dr1);
        //DSTemp.Tables.Add(DTTemp);
        //DSTemp.Merge(ds);
        //Wuc_Export_To_Excel1.SessionExporttoExcel = DSTemp.Tables[0];

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[1];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        NoOf_GC = Util.String2Int(dr["Total_NoOf_GC"].ToString()); 
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        Sub_Total = Util.String2Decimal(dr["Total_Sub_Total"].ToString());
        STax_Amt = Util.String2Decimal(dr["Total_STax"].ToString());
        GC_Amount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());
        Discount = Util.String2Decimal(dr["Total_Discount"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
      
        if ((e.Item.Cells[0].Text) == "Total :")
        {
            e.Item.BackColor = System.Drawing.Color.Green;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
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
