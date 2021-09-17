using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Operation_Frm_Real_Time_Loading_Unloading_Excel : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
   
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.SetRegionCaption = "Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Area:";
        
        if (rbtn_type.SelectedValue == "0")
        {
            Wuc_Region_Area_Branch1.SetBranchCaption = "Unloading Branch:";
        }
        else
        {
            Wuc_Region_Area_Branch1.SetBranchCaption = "Delivery Branch:";
        }
       
        Wuc_From_To_Datepicker1.Set_FromDate_Caption = "From Date:";
        Wuc_From_To_Datepicker1.Set_ToDate_Caption = "To Date:";
        Wuc_Export_To_Excel1.FileName = "RouteLoadPerformance";
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

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
   
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
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
        int type;
        if (rbtn_type.SelectedValue == "0")
        {
            type = 0;
        }
        else
        {
            type = 1;
        }
        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Lo_Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Lo_Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Lo_Branch_id = Wuc_Region_Area_Branch1.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Lo_Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Lo_Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Lo_Branch_id),       
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Type",SqlDbType.Int,0,type),
            
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID)
        };

        objDAL.RunProc("EC_RPT_Real_Time_Loading_Unloading_Excel", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

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
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
