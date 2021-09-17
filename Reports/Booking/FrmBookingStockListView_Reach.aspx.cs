using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Harshal Sapre
//Desc   : Booking Stock List Report
//Date   : 14-01-09

public partial class Reports_Booking_FrmBookingStockListView : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Total_Gc;
    decimal Basic_Freight, Total_Freight, Invoice_Value;
    #endregion

    #region EventClick


    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "BookingStockList";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(WucDatePicker1.SelectedDate, ref msg)) == true)
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

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_ChargedWeight, lbl_ActualWeight, lbl_Articles, lbl_Total_Gc;
            System.Web.UI.WebControls.Label lbl_BasicFreight, lbl_TotalFreight, lbl_InvoiceValue;

            lbl_ChargedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ChargedWeight");
            lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_BasicFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicFreight");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_InvoiceValue = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_InvoiceValue");
            lbl_Total_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Gc");

            lbl_Total_Gc.Text = Total_Gc.ToString();
            lbl_ChargedWeight.Text = ChargedWeight.ToString();
            lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_BasicFreight.Text = Basic_Freight.ToString();
            lbl_TotalFreight.Text = Total_Freight.ToString();
            lbl_InvoiceValue.Text = Invoice_Value.ToString();

        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Gc = Util.String2Decimal(dr["Total_GC"].ToString());
        ChargedWeight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        Actual_Weight = Util.String2Decimal(dr["Total_Actual_Wt"].ToString());
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total_Freight"].ToString());
        Invoice_Value = Util.String2Decimal(dr["Total_Invoice_Value"].ToString());
    }
    #endregion

    #region Other Function

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
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
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;

        DateTime As_On_Date = WucDatePicker1.SelectedDate;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime, 0,From_Date),
            objDAL.MakeInParams("@To_Date", SqlDbType.DateTime, 0,To_Date),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
        };

        objDAL.RunProc("[EC_RPT_Booking_Stock_List_Reach_GRD]", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["gc_caption Date"] = "Total";
        dr["gc_caption No"] = Total_Gc;
        dr["Charged Weight"] = ChargedWeight;
        dr["Actual Weight"] = Actual_Weight;
        dr["Articles"] = Articles;
        dr["Basic Freight"] = Basic_Freight;
        dr["Total Freight"] = Total_Freight;
        dr["Invoice Value"] = Invoice_Value;

        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
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
    #endregion
    