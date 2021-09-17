using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Operation_Frm_Truck_Unloading_Report : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal Actual_Weight, Truck_Hire_Charge, Sub_Freight, Advance;
    int No_of_Memos, No_of_Gcs, Truck_Capacity;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.SetRegionCaption = "Unloading Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Unloading Area:";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Unloading Branch:";

        Wuc_Region_Area_Branch2.SetRegionCaption = "Loading Region:";
        Wuc_Region_Area_Branch2.SetAreaCaption = "Loading Area:";
        Wuc_Region_Area_Branch2.SetBranchCaption = "Loading Branch:";
        Wuc_Region_Area_Branch2.ShowAll = true;
        Wuc_Export_To_Excel1.FileName = "TruckUnloadingReport";
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
            System.Web.UI.WebControls.Label lbl_ActualWeight, lbl_No_of_Memos, lbl_No_of_Gc, lbl_Truck_Hire_Charge;
            System.Web.UI.WebControls.Label lbl_Advance, lbl_Truck_Capacity, lbl_Sub_Freight, lbl_Total;

            lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_No_of_Memos = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_No_of_Memos");
            lbl_No_of_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_No_of_Gc");
            lbl_Truck_Hire_Charge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Truck_Hire_Charge");
            lbl_Advance = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Advance");
            lbl_Truck_Capacity = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Truck_Capacity");
            lbl_Sub_Freight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_Freight");
            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");

            lbl_Total.Text = "Total";
            lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_No_of_Memos.Text = No_of_Memos.ToString();
            lbl_No_of_Gc.Text = No_of_Gcs.ToString();
            lbl_Truck_Hire_Charge.Text = Truck_Hire_Charge.ToString();
            lbl_Advance.Text = Advance.ToString();
            lbl_Truck_Capacity.Text = Truck_Capacity.ToString();
            lbl_Sub_Freight.Text = Sub_Freight.ToString();
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
        Actual_Weight = Util.String2Decimal(dr["Actual Weight"].ToString());
        No_of_Memos = Util.String2Int(dr["No of Memos"].ToString());
        No_of_Gcs = Util.String2Int(dr["No of gc_caption's"].ToString());
        Truck_Hire_Charge = Util.String2Decimal(dr["Truck Hire Charge"].ToString());
        Advance = Util.String2Decimal(dr["Advance"].ToString());
        Truck_Capacity = Util.String2Int(dr["Truck Capacity"].ToString());
        Sub_Freight = Util.String2Decimal(dr["Sub Freight"].ToString());
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
            objDAL.MakeInParams("@Vehcle_Type",SqlDbType.Int,0,ddl_Truck.SelectedValue),
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

        objDAL.RunProc("[EF_RPT_Truck_UnLoading_GRD]", objSqlParam, ref ds);
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
        dr["Actual Weight"] = Actual_Weight;
        dr["No of Memos"] = No_of_Memos;
        dr["No of gc_caption's"] = No_of_Gcs;
        dr["Truck Hire Charge"] = Truck_Hire_Charge;
        dr["Advance"] = Advance;
        dr["Truck Capacity"] = Truck_Capacity;
        dr["Sub Freight"] = Sub_Freight;
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
