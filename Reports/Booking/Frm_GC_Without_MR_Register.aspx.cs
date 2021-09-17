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

public partial class Reports_Booking_Frm_GC_Without_MR_Register : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Total_GC;
    decimal Basic_Freight, Total_Freight, Invoice_Amount;
    #endregion

  
    protected void Page_Load(object sender, EventArgs e)
    {

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "GC Without MR";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total_GC;
                      
            lbl_Total_GC = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_GC");

            lbl_Total_GC.Text = Convert.ToString(ds.Tables[2].Rows[0][0]);
        }
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

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
        int Rpt_Type_Id = Convert.ToInt32(ddl_Rpt_Type.SelectedValue);
        int Branch_Type_Id = Convert.ToInt32(rdl_Is_Group_Ledger.SelectedValue);

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime, 0,From_Date),
            objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Rpt_Type_Id", SqlDbType.Int, 0,Rpt_Type_Id),
            objDAL.MakeInParams("@Branch_Type_Id", SqlDbType.Int, 0,Branch_Type_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)     
        };
        objDAL.RunProc("[dbo].[EC_RPT_GC_Without_MR]", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;
        if (ds.Tables.Count == 0) return; 
        if (ds.Tables[0].Rows.Count > 0)
        {
            dg_Grid.VirtualItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);   
        }

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
        dr["GC_No"] = "Total";
        dr["GC_Total"] = Convert.ToString(ds.Tables[0].Compute("SUM(GC_Total)", ""));            
        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void ddl_Rpt_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)"form_and_pageload";
        int grid_currentpageindex = 0;
        int grid_PageSize = 0;

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Convert.ToDateTime("01/01/1900");
        DateTime To_Date = Convert.ToDateTime("01/01/1900");
        int Rpt_Type_Id = Convert.ToInt32(ddl_Rpt_Type.SelectedValue);
        int Branch_Type_Id = Convert.ToInt32(rdl_Is_Group_Ledger.SelectedValue);

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime, 0,From_Date),
            objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Rpt_Type_Id", SqlDbType.Int, 0,Rpt_Type_Id),
            objDAL.MakeInParams("@Branch_Type_Id", SqlDbType.Int, 0,Branch_Type_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)     
        };
        objDAL.RunProc("[dbo].[EC_RPT_GC_Without_MR]", objSqlParam, ref ds);

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
        lbl_Error.Text = ""; 
    }
}
