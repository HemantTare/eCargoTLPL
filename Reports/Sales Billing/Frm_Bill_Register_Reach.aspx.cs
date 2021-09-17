using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Sales_Billing_Frm_Bill_Register_Reach : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

    decimal bill_amt,recd_amt,deduction;

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "Bill Register";
        Wuc_Region_Area_Branch1.SelectedLocationsOnlyVisibility = true;
        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            //Division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

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
    protected void Wuc_Export_To_Excel1_Load(object sender, EventArgs e)
    {

    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Deduction, lbl_bill_amount, lbl_Recd_Amount,
            lbl_Total;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_bill_amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_bill_amount");
            lbl_Recd_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Recd_Amount");
            lbl_Deduction = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Deduction");
          
            lbl_Total.Text = "Total";
            lbl_bill_amount.Text = bill_amt.ToString();
            lbl_Recd_Amount.Text = recd_amt.ToString();
            lbl_Deduction.Text = deduction.ToString();          
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
        int region_only;
        int area_only;
        string Client_Name;
        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }
        if (Wuc_Region_Area_Branch1.Selected_Region_Only == true)
        {
            region_only = 1;
        }
        else
        {
            region_only = 0;
        }
        if (Wuc_Region_Area_Branch1.Selected_Region_Only == true)
        {
            area_only = 1;
        }
        else
        {
            area_only = 0;
        }    
        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int Division_ID = WucDivisions1.Division_ID;
        int booking_type =Convert.ToInt32(ddl_booking_type.SelectedValue);
        if (Txt_Consignor_name.Text == "")
        {
            Client_Name = "";
        }
        else
        {
            Client_Name=Txt_Consignor_name.Text;
        }
       
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_Only", SqlDbType.Int,0,region_only),
            objDAL.MakeInParams("@Area_Only", SqlDbType.Int,0,area_only),
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),            
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),         
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@Booking_Type_Id",SqlDbType.Int,0,booking_type),
            objDAL.MakeInParams("@Client_Name",SqlDbType.VarChar,0,Client_Name)
        };

        objDAL.RunProc("[EC_RPT_Bill_Register_Reach]", objSqlParam, ref ds);
        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        bill_amt = Util.String2Decimal(dr["bill Amount"].ToString());
        recd_amt = Util.String2Decimal(dr["Recd Amount"].ToString());
        deduction = Util.String2Decimal(dr["deduction"].ToString());
    }
    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["Bill_No_For_Print"] = "Total";
        dr["bill amount"] = bill_amt;
        dr["recd amount"] = recd_amt;
        dr["Deduction"] = deduction;
          
        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
