using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Operation_Frm_Vehicle_Monitor_Report : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal vehicle_Capacity,Total_Received_Weight;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.SetRegionCaption = "Unloading Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Unloading Area:";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Unloading Branch:";

        Wuc_Export_To_Excel1.FileName = "VehicleMonitorReport";

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);

        }
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total,lbl_vehicle_Capacity, lbl_Total_Received_Weight;


            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_vehicle_Capacity = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_vehicle_Capacity");
            lbl_Total_Received_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Received_Weight");

            lbl_Total.Text = "Total";
            lbl_vehicle_Capacity.Text = vehicle_Capacity.ToString();
            lbl_Total_Received_Weight.Text = Total_Received_Weight.ToString();
           
        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        vehicle_Capacity = Util.String2Decimal(dr["Vehicle Capacity"].ToString());
        Total_Received_Weight = Util.String2Decimal(dr["Total Received Weight"].ToString());
       
    }
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
       

        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
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

        int Un_Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Un_Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Un_Branch_id = Wuc_Region_Area_Branch1.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        string vehicle_no =Txt_Vehicle_No.Text;
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Un_Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Un_Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Un_Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@vehicle_No",SqlDbType.VarChar,10,vehicle_no),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
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

        objDAL.RunProc("[EC_RPT_Vehicle_Monitor]", objSqlParam, ref ds);
        if (CallFrom == "form_and_pageload") return;
        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error,TotalRecords);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
      
    }

    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["lhpo_caption No"] = "Total";
        dr["Vehicle Capacity"] = vehicle_Capacity;
        dr["Total Received Weight"] = Total_Received_Weight;  
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
