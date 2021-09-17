using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
  
 
public partial class Finance_Reports_FrmClosingCashBal : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    //decimal Articles, Sub_Total, Basic_Freight, STax_Amt, Total_Freight, GC_Amount, Discount;
    //int NoOf_GC;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "ClosingCashBalance"; 

        if (IsPostBack == false)
        {
            Dtp_AsOnDate.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date);
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid); 
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
        else 
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
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
        DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;
        
        
            SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,AsOnDate)  
        };

            objDAL.RunProc("FA_Opr_BranchWiseDailyClosingCashReport", objSqlParam, ref ds);

            if (CallFrom == "form_and_pageload") return;

            //dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
            //string TotalRecords = ds.Tables[0].Rows[0][0].ToString(); 

            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

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

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
 
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        int RegionID = Wuc_Region_Area_Branch1.RegionID;
        int AreaID = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;
        int DivisionId = UserManager.getUserParam().DivisionId;
        int DeliveryAreaID = 0;
        string RegionText = Wuc_Region_Area_Branch1.SelectedRegionText;
        string AreaText = Wuc_Region_Area_Branch1.SelectedAreaText;
        string BranchText = Wuc_Region_Area_Branch1.SelectedBranchText;
        string DlyArea = "";
               

        if ((e.Item.Cells[0].Text) == "Opening Balance : ")
        {
            e.Item.BackColor = System.Drawing.Color.Brown;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true; 
           
        }
        if ((e.Item.Cells[1].Text) == "Paid Booking ") 
        {
            e.Item.BackColor = System.Drawing.Color.Gold;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");

        }
        if ((e.Item.Cells[1].Text) == "To Pay Recovery (Cash)")
        {
            e.Item.BackColor = System.Drawing.Color.GreenYellow;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");

        }
        if ((e.Item.Cells[1].Text) == "Other Cash Receipt ")
        {
            e.Item.BackColor = System.Drawing.Color.BurlyWood;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");

        }
        if ((e.Item.Cells[1].Text) == "          Total :  ")
        {
            e.Item.BackColor = System.Drawing.Color.Bisque;
            e.Item.ForeColor = System.Drawing.Color.DarkRed;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[0].Text) == "Expenses : ")
        {
            e.Item.BackColor = System.Drawing.Color.Green;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true; 
        }
        if ((e.Item.Cells[1].Text) == "Tempo Freight ")
        {
            e.Item.BackColor = System.Drawing.Color.PaleGreen;
            //e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");
        }
        if ((e.Item.Cells[1].Text) == "Other ")
        {
            e.Item.BackColor = System.Drawing.Color.SpringGreen;
            //e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");
        }
        if ((e.Item.Cells[1].Text) == "          Total : ")
        {
            e.Item.BackColor = System.Drawing.Color.LightGreen;
            e.Item.ForeColor = System.Drawing.Color.DarkSlateGray;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[1].Text) == "Cash Paid To Others " || (e.Item.Cells[1].Text) == "Inter Branch Transfer " || (e.Item.Cells[1].Text) == "Deposit In Bank ")
        {
            e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
            //e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");
        }
        if ((e.Item.Cells[0].Text) == "Closing Balance : ")
        {
            e.Item.BackColor = System.Drawing.Color.MediumSlateBlue;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[0].Text) == "Pending PDS : ")
        {
            e.Item.BackColor = System.Drawing.Color.Firebrick;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }

        if ((e.Item.Cells[1].Text).Length >=9 )
        {
            if ((e.Item.Cells[1].Text).Substring(0,9) == "Total PDS")
            {
                e.Item.BackColor = System.Drawing.Color.Pink;
                e.Item.ForeColor = System.Drawing.Color.Black;
                //e.Item.Cells[2].Font.Bold = true;
                e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
                e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim().Substring(0,9) + " ','" + Branch_id + "','" + AsOnDate + "');");
            }           
        }
        if ((e.Item.Cells[1].Text) == "PDS Freight (ToPay)")
        {
            e.Item.BackColor = System.Drawing.Color.Wheat;
            e.Item.ForeColor = System.Drawing.Color.Black;
            //e.Item.Cells[2].Font.Bold = true;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");

        }
        if ((e.Item.Cells[0].Text) == "Delivery Stock : ")
        {
            e.Item.BackColor = System.Drawing.Color.DarkViolet;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }

        if ((e.Item.Cells[1].Text).Length >= 8)
        {
            if ((e.Item.Cells[1].Text).Substring(0,8) == "Total LR")
            {
                e.Item.BackColor = System.Drawing.Color.Violet;
                e.Item.ForeColor = System.Drawing.Color.Black;
                //e.Item.Cells[2].Font.Bold = true;
                e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
                e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                e.Item.Cells[1].Attributes.Add("onclick", "return viewwindowDlyStkList('" + e.Item.Cells[1].Text.Trim().Substring(0,8) + " ','" + Branch_id + "','" + AsOnDate + "');");

            }
        }

        if ((e.Item.Cells[1].Text).Length >= 13)
        {
            if ((e.Item.Cells[1].Text).Substring(0,13) == "Total Parcels")
            {
                e.Item.BackColor = System.Drawing.Color.Violet;
                e.Item.ForeColor = System.Drawing.Color.Black;
                //e.Item.Cells[2].Font.Bold = true;
                e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
                e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                e.Item.Cells[1].Attributes.Add("onclick", "return viewwindowDlyStkList('" + e.Item.Cells[1].Text.Trim().Substring(0,13) + " ','" + Branch_id + "','" + AsOnDate + "');");

            }
        }
        if ((e.Item.Cells[1].Text) == "Total Freight (ToPay)")
        {
            e.Item.BackColor = System.Drawing.Color.Plum;
            e.Item.ForeColor = System.Drawing.Color.Black;
            //e.Item.Cells[2].Font.Bold = true;
            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");

        }
        if ((e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) || (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem))
        {
            if ((e.Item.Cells[1].Text) == "Total Freight (ToPay)") 
            {
                e.Item.Cells[1].Attributes.Add("onclick", "return viewwindowDlyStkList('" + e.Item.Cells[1].Text.Trim() + " ','" + Util.EncryptInteger(RegionID) + "','" + Util.EncryptInteger(AreaID) + "','" + Util.EncryptInteger(Branch_id) + "','" + AsOnDate + "','" + Util.EncryptInteger(DivisionId) + "','" + Util.EncryptInteger(DeliveryAreaID) + "','" + Util.EncryptString(RegionText) + "','" + Util.EncryptString(AreaText) + "','" + Util.EncryptString(BranchText) + "','" + Util.EncryptString(DlyArea) + "');");
            }

            if ((e.Item.Cells[1].Text).Length >= 13)
            {
                if ((e.Item.Cells[1].Text).Substring(0, 13) == "Total Parcels")
                {
                    e.Item.Cells[1].Attributes.Add("onclick", "return viewwindowDlyStkList('" + e.Item.Cells[1].Text.Trim().Substring(0, 13) + " ','" + Util.EncryptInteger(RegionID) + "','" + Util.EncryptInteger(AreaID) + "','" + Util.EncryptInteger(Branch_id) + "','" + AsOnDate + "','" + Util.EncryptInteger(DivisionId) + "','" + Util.EncryptInteger(DeliveryAreaID) + "','" + Util.EncryptString(RegionText) + "','" + Util.EncryptString(AreaText) + "','" + Util.EncryptString(BranchText) + "','" + Util.EncryptString(DlyArea) + "');");
                }
            }

            if ((e.Item.Cells[1].Text).Length >= 8)
            {
                if ((e.Item.Cells[1].Text).Substring(0, 8) == "Total LR")
                {
                    e.Item.Cells[1].Attributes.Add("onclick", "return viewwindowDlyStkList('" + e.Item.Cells[1].Text.Trim().Substring(0, 8) + " ','" + Util.EncryptInteger(RegionID) + "','" + Util.EncryptInteger(AreaID) + "','" + Util.EncryptInteger(Branch_id) + "','" + AsOnDate + "','" + Util.EncryptInteger(DivisionId) + "','" + Util.EncryptInteger(DeliveryAreaID) + "','" + Util.EncryptString(RegionText) + "','" + Util.EncryptString(AreaText) + "','" + Util.EncryptString(BranchText) + "','" + Util.EncryptString(DlyArea) + "');");
                }
            }
        }

        if ((e.Item.Cells[0].Text) == "ToPay Recovery : ")
        {
            e.Item.BackColor = System.Drawing.Color.Goldenrod;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Font.Bold = true;
        }

        if ((e.Item.Cells[1].Text) == "Cash" || (e.Item.Cells[1].Text) == "Cheque" || (e.Item.Cells[1].Text) == "Credit A/c" || (e.Item.Cells[1].Text) == "Debit A/c" || (e.Item.Cells[1].Text) == "Swipe Card" || (e.Item.Cells[1].Text) == "Mobile Pay")
        {
            e.Item.BackColor = System.Drawing.Color.Bisque;
            e.Item.ForeColor = System.Drawing.Color.DarkRed;

            e.Item.Cells[1].CssClass = "SHOWSELECTEDLINK";
            e.Item.Cells[1].Attributes.Add("onmouseover", "this.style.cursor='hand'");
            e.Item.Cells[1].Attributes.Add("onclick", "return viewwindow('" + e.Item.Cells[1].Text.Trim() + " ','" + Branch_id + "','" + AsOnDate + "');");
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
