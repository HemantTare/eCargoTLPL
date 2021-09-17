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


public partial class Reports_Operation_Frm_LHC_Loaded_To_Other_Than_Branches_Excel : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal debited_credited_to, bth_amt,Loaded_wt,Truck_Hire,To_pay_collection;
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "ClientwiseBookingRegister";
        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
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
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label  lbl_Balance_Payble_Amount, lbl_Total, lbl_Loaded_Wt,
                lbl_Total_Hire_Amount, lbl_To_Pay_Collection_Amount;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Balance_Payble_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Balance_Payble_Amount");
            lbl_Loaded_Wt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Loaded_Wt");
            lbl_Total_Hire_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Hire_Amount");
            lbl_To_Pay_Collection_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_To_Pay_Collection_Amount");
           
            lbl_Total.Text = "Total";
            lbl_Loaded_Wt.Text=Loaded_wt.ToString();
            lbl_Total_Hire_Amount.Text=Truck_Hire.ToString();
            lbl_To_Pay_Collection_Amount.Text = To_pay_collection.ToString();
            lbl_Balance_Payble_Amount.Text = bth_amt.ToString();

            
        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        bth_amt = Util.String2Decimal(dr["BTH Amount"].ToString());
        Loaded_wt = Util.String2Decimal(dr["Loaded Wt"].ToString());
        Truck_Hire = Util.String2Decimal(dr["Total Hire Amount"].ToString());
        To_pay_collection = Util.String2Decimal(dr["To Pay Collection Amount"].ToString());
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
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        //string Cnr_name = Txt_Consignor_name.Text;

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),         
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),       
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)
        };

        objDAL.RunProc("[EC_RPT_LHC_Loaded_To_Other_Than_Branches_Excel]", objSqlParam, ref ds);
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

    private void PrepareDTForExportToExcel()
    {
        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["lhpo_caption No"] = "Total";
        //dr["Debited/Credited To"] = debited_credited_to;
        dr["Loaded Wt"] = Loaded_wt;
        dr["Total Hire Amount"] = Truck_Hire;
        dr["To Pay collection Amount"] = To_pay_collection;
        dr["BTH Amount"] = bth_amt;
        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
   
}
