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

public partial class Reports_Booking_FrmStockTransferRegister : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    int No_of_Gcs;

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.SetRegionCaption = "From Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "From Area:";
        Wuc_Region_Area_Branch1.SetBranchCaption = "From Branch:";

       
        Wuc_Region_Area_Branch2.SetRegionCaption = "To Region:";
        Wuc_Region_Area_Branch2.SetAreaCaption = "To Area:";
        Wuc_Region_Area_Branch2.SetBranchCaption = "To Branch:";

        Wuc_Region_Area_Branch2.ShowAll = true;
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "StockTransferRegisterReport";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
 

    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_No_of_Gc;

             lbl_No_of_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_No_of_Gc");
            //lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");

            //lbl_Total.Text = "Total";
            lbl_No_of_Gc.Text = No_of_Gcs.ToString();
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
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

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        No_of_Gcs = Util.String2Int(dr["Column1"].ToString());
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

        int FromRegionId = Wuc_Region_Area_Branch1.RegionID;
        int FromAreaid = Wuc_Region_Area_Branch1.AreaID;
        int FromBranchid = Wuc_Region_Area_Branch1.BranchID;
        int ToRegionId = Wuc_Region_Area_Branch2.RegionID;
        int ToAreaid = Wuc_Region_Area_Branch2.AreaID;
        int ToBranchid = Wuc_Region_Area_Branch2.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
       

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@FromRegionId", SqlDbType.Int,0,FromRegionId),
            objDAL.MakeInParams("@FromAreaId", SqlDbType.Int,0,FromAreaid),
            objDAL.MakeInParams("@FromBranchId", SqlDbType.Int,0,FromBranchid),
            objDAL.MakeInParams("@ToRegionId", SqlDbType.Int,0,ToRegionId),
            objDAL.MakeInParams("@ToAreaId", SqlDbType.Int,0,ToAreaid),
            objDAL.MakeInParams("@ToBranchId", SqlDbType.Int,0,ToBranchid),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),            
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)
        };

        objDAL.RunProc("[EC_RPT_StockTransferRegister_Details_Excel]", objSqlParam, ref ds);
        if (CallFrom == "form_and_pageload") return;
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

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["gc_Caption No"] = No_of_Gcs;
        ds.Tables[0].Rows.Add(dr);
        ds.Tables[0].Columns.Remove("GC_Id");              
        

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
