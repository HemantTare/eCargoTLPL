using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Booking_Frm_TBB_Pending_Gc_Details_Excel : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();



    decimal Charged_Weight, delay,Total_Articles,Actual_Weight;
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "ClientwiseBookingRegister";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Total_Articles, lbl_Charged_Weight,
                   lbl_Actual_Weight, lbl_Delay;


            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Total_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Articles");
            lbl_Charged_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Charged_Weight");
            lbl_Actual_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Actual_Weight");
            //lbl_Delay = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Delay");

         


            lbl_Total.Text = "Total";
            lbl_Total_Articles.Text = Total_Articles.ToString();
            lbl_Charged_Weight.Text = Charged_Weight.ToString();
            //lbl_Delay.Text = delay.ToString();           
            lbl_Actual_Weight.Text = Actual_Weight.ToString();
           

        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Articles = Util.String2Int(dr["Total Articles"].ToString());
        Charged_Weight = Util.String2Decimal(dr["Charged Weight"].ToString());
        Actual_Weight = Util.String2Decimal(dr["Actual Weight"].ToString());
      

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


        DateTime To_date = WucDatePicker1.SelectedDate;
        string Cnr_name = Txt_Consignor_name.Text;

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              
            objDAL.MakeInParams("@As_On_Date ", SqlDbType.DateTime,0,To_date),           
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@Client_name",SqlDbType.VarChar,100,Cnr_name),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)
        };

        objDAL.RunProc("[EC_RPT_TBB_Pending_GC_Details_Excel]", objSqlParam, ref ds);
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
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
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
    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["gc_caption No"] = "Total";
        dr["Total_Articles"] = Total_Articles;
        dr["Charged_Weight"] = Charged_Weight;
        dr["Total_Actual_Weight"] = Actual_Weight;  
        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
