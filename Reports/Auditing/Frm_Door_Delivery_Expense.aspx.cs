using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_Auditing_Frm_Door_Delivery_Expense : System.Web.UI.Page
{
    private DataSet ds;

    decimal Charged_Weight, Freight_Amt, As_Per_decided, DD_in_GC;

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.SetRegionCaption = "Delivery Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Delivery Area:";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Delivery Branch:";

        Wuc_Region_Area_Branch2.SetRegionCaption = "Booking Region:";
        Wuc_Region_Area_Branch2.SetAreaCaption = "Booking Area:";
        Wuc_Region_Area_Branch2.SetBranchCaption = "Booking Branch:";

        Wuc_From_To_Datepicker1.Set_FromDate_Caption = "From Date:";
        Wuc_From_To_Datepicker1.Set_ToDate_Caption = "To Date:";

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DoorDeliveryExpences";

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

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Charged_Weight, lbl_Freight_Amt,
            lbl_As_Per_Decided, lbl_DD_in_Gc;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Charged_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Charged_Weight");
            lbl_As_Per_Decided = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_As_Per_Decided");
            lbl_DD_in_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DD_in_Gc");
            lbl_Freight_Amt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight_Amount");
            
            lbl_Total.Text = "Total";
       
            lbl_Charged_Weight.Text = Charged_Weight.ToString();          
            lbl_Freight_Amt.Text = Freight_Amt.ToString();
            lbl_As_Per_Decided.Text=As_Per_decided.ToString();
            lbl_DD_in_Gc.Text=DD_in_GC.ToString();
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
        Charged_Weight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        Freight_Amt = Util.String2Decimal(dr["Total_Freight_Amt"].ToString());
        As_Per_decided = Util.String2Decimal(dr["Total_As_Per_decided"].ToString());
        DD_in_GC = Util.String2Decimal(dr["Total_DD_in_GC"].ToString());
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
        int Bkg_Region_Id = Wuc_Region_Area_Branch2.RegionID;
        int Bkg_Area_Id = Wuc_Region_Area_Branch2.AreaID;
        int Bkg_Branch_Id = Wuc_Region_Area_Branch2.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Delivery_Region_ID", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Delivery_Area_ID", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Delivery_Branch_ID", SqlDbType.Int,0,Branch_id),   
            objDAL.MakeInParams("@Booking_Region_ID", SqlDbType.Int,0,Bkg_Region_Id),
            objDAL.MakeInParams("@Booking_Area_ID", SqlDbType.Int,0,Bkg_Area_Id),
            objDAL.MakeInParams("@Booking_Branch_ID", SqlDbType.Int,0,Bkg_Branch_Id),             
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),           
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

        objDAL.RunProc("[EC_RPT_Door_Delivery_Expenses]", objSqlParam, ref ds);
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
            DataRow dr = ds.Tables[0].NewRow();
            dr["gc_caption No"] = "Total";
            dr["Charged Weight"] = Charged_Weight;
            dr["Freight Amount"] = Freight_Amt;
            dr["As Per Decided"] = As_Per_decided;
            dr["DD in gc_caption"] = DD_in_GC;
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