using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Operation_Frm_Route_Load_Performance : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    int No_of_trip;
    decimal Truck_Hire, Truck_Capacity_In_Kg, Actual_Capacity_Used, Actual_Capacity_rate_per_kg,
        Trip_Capacity_In_Kg, Truck_Hire_Charge_For_One_Trip, Trip_Capacity_rate_per_kg, Variance;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.SetRegionCaption = "Loading Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Loading Area:";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Loading Branch:";

        Wuc_Region_Area_Branch2.SetRegionCaption = "Unloading Region:";
        Wuc_Region_Area_Branch2.SetAreaCaption = "Unloading Area:";
        Wuc_Region_Area_Branch2.SetBranchCaption = "Unloading Branch:";

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
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);



        }
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_No_of_Trips, lbl_Truck_Hire_Charge, lbl_Truck_Capacity_in_KG, lbl_Actual_Capacity_Used;
            System.Web.UI.WebControls.Label lbl_Trip_Capacity_in_Kg, lbl_Truck_Hire_Charge_For_One_Trip,
                lbl_Trip_Capacity_rate_per_Kg, lbl_Actual_Capacity_rate_per_Kg, lbl_Variance, lbl_Total;

            lbl_No_of_Trips = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_No_of_Trips");
            lbl_Truck_Hire_Charge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Truck_Hire_Charge");
            lbl_Truck_Capacity_in_KG = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Truck_Capacity_in_KG");
            lbl_Actual_Capacity_Used = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Actual_Capacity_Used");
            lbl_Trip_Capacity_in_Kg = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Trip_Capacity_in_Kg");
            lbl_Truck_Hire_Charge_For_One_Trip = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Truck_Hire_Charge_For_One_Trip");
            lbl_Trip_Capacity_rate_per_Kg = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Trip_Capacity_rate_per_Kg");
            lbl_Actual_Capacity_rate_per_Kg = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Actual_Capacity_rate_per_Kg");
            lbl_Variance = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Variance");
            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");           
 
            lbl_Total.Text = "Total";
            lbl_No_of_Trips.Text = No_of_trip.ToString();
            lbl_Truck_Hire_Charge.Text = Truck_Hire.ToString();
            lbl_Truck_Capacity_in_KG.Text = Truck_Capacity_In_Kg.ToString();
            lbl_Actual_Capacity_Used.Text = Actual_Capacity_Used.ToString();
            lbl_Trip_Capacity_in_Kg.Text = Trip_Capacity_In_Kg.ToString();
            lbl_Truck_Hire_Charge_For_One_Trip.Text = Truck_Hire_Charge_For_One_Trip.ToString();
            lbl_Trip_Capacity_rate_per_Kg.Text = Trip_Capacity_rate_per_kg.ToString();
            lbl_Actual_Capacity_rate_per_Kg.Text = Actual_Capacity_rate_per_kg.ToString();
            lbl_Variance.Text=Variance.ToString();
        }
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

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        No_of_trip = Util.String2Int(dr["No of Trips"].ToString());
        Truck_Hire = Util.String2Decimal(dr["Total Truck Hire"].ToString());
        Truck_Capacity_In_Kg = Util.String2Decimal(dr["Truck Capacity in KG"].ToString());
        Actual_Capacity_Used = Util.String2Decimal(dr["Actual Capacity Used"].ToString());
        Trip_Capacity_In_Kg = Util.String2Decimal(dr["Trip Capacity In Kg"].ToString());
        Truck_Hire_Charge_For_One_Trip = Util.String2Decimal(dr["Truck Hire Charge For One Trip"].ToString());
        Trip_Capacity_rate_per_kg = Util.String2Decimal(dr["Trip Capacity rate per Kg"].ToString());
        Actual_Capacity_rate_per_kg = Util.String2Decimal(dr["Actual Capacity rate per Kg"].ToString());
        Variance = Util.String2Decimal(dr["Variance"].ToString());
        
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

        int Lo_Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Lo_Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Lo_Branch_id = Wuc_Region_Area_Branch1.BranchID;

        int Un_Region_Id = Wuc_Region_Area_Branch2.RegionID;
        int Un_Area_id = Wuc_Region_Area_Branch2.AreaID;
        int Un_Branch_id = Wuc_Region_Area_Branch2.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int Vehicle_Type_Id = Convert.ToInt32(ddl_Truck.SelectedValue);

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Lo_Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Lo_Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Lo_Branch_id),

            objDAL.MakeInParams("@Un_loading_Region_id", SqlDbType.Int,0,Un_Region_Id),
            objDAL.MakeInParams("@Un_loading_Area_id", SqlDbType.Int,0,Un_Area_id),
            objDAL.MakeInParams("@Un_loading_Branch_id", SqlDbType.Int,0,Un_Branch_id),


            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
          
            objDAL.MakeInParams("@Vehicle_Type",SqlDbType.Int,0,ddl_Truck.SelectedValue),

             objDAL.MakeInParams("@Vehicle_No",SqlDbType.VarChar,0,Txt_Vehicle_No.Text),
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

        objDAL.RunProc("[EC_RPT_Route_Load_Performance]", objSqlParam, ref ds);
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
        dr["Trip From AO"] = "Total";
        dr["No of Trips"] = No_of_trip;
        dr["Total Truck Hire"] = Truck_Hire;
        dr["Truck Capacity In Kg"] = Truck_Capacity_In_Kg;
        dr["Actual Capacity Used"] = Actual_Capacity_Used;
        dr["Actual Capacity rate per kg"] = Actual_Capacity_rate_per_kg;
        dr["Trip Capacity In Kg"] = Trip_Capacity_In_Kg;
        dr["Truck Hire Charge For One Trip"] = Truck_Hire_Charge_For_One_Trip;
        dr["Trip Capacity rate per kg"] = Trip_Capacity_rate_per_kg;        
        dr["Variance"] = Variance;
        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    public void ClearVariables()
    {
        ds = null;
    }
}
