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

public partial class Reports_Operation_Frm_Lhpo_Register_Reach : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal LHS_Amt, TDS_on_LHS, Charity,Munishana_Deduction,
        Total_BTH_Amt,Other_Charges_Paid,TDS_on_Other_Charges,
        Other_Charges_Deducted,Total_ATH, ATH_Amt, ATH_Amount_Paid;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "LHPO Register";

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
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_LHS_Amt, lbl_TDS_on_LHS, lbl_Charity, lbl_Munishana_Deduction,
        lbl_Total_BTH_Amt, lbl_Other_Charges_Paid, lbl_TDS_on_Other_Charges,
        lbl_Other_Charges_Deducted, lbl_Total_ATH, lbl_ATH_Amt, lbl_ATH_Amount_Paid;

            lbl_LHS_Amt = (Label)e.Item.FindControl("lbl_LHS_Amt");
            lbl_TDS_on_LHS = (Label)e.Item.FindControl("lbl_TDS_on_LHS");
            lbl_Charity = (Label)e.Item.FindControl("lbl_Charity");
            lbl_Munishana_Deduction = (Label)e.Item.FindControl("lbl_Munishana_Deduction");
            lbl_Total_BTH_Amt = (Label)e.Item.FindControl("lbl_Total_BTH_Amt");
            lbl_Other_Charges_Paid = (Label)e.Item.FindControl("lbl_Other_Charges_Paid");
            lbl_TDS_on_Other_Charges = (Label)e.Item.FindControl("lbl_TDS_on_Other_Charges");

            lbl_Other_Charges_Deducted = (Label)e.Item.FindControl("lbl_Other_Charges_Deducted");

            lbl_Total_ATH = (Label)e.Item.FindControl("lbl_Total_ATH");
            lbl_ATH_Amt = (Label)e.Item.FindControl("lbl_ATH_Amt");
            lbl_ATH_Amount_Paid = (Label)e.Item.FindControl("lbl_ATH_Amount_Paid");


            lbl_LHS_Amt.Text = LHS_Amt.ToString();
            lbl_TDS_on_LHS.Text = TDS_on_LHS.ToString();
            lbl_Charity.Text = Charity.ToString();
            lbl_Munishana_Deduction.Text = Munishana_Deduction.ToString();
            lbl_Total_BTH_Amt.Text = Total_BTH_Amt.ToString();

            lbl_Other_Charges_Paid.Text = Other_Charges_Paid.ToString();
            lbl_TDS_on_Other_Charges.Text =TDS_on_Other_Charges.ToString();
            lbl_Other_Charges_Deducted.Text =Other_Charges_Deducted.ToString();
            lbl_Total_ATH.Text = Total_ATH.ToString();
            lbl_ATH_Amt.Text = ATH_Amt.ToString();
            lbl_ATH_Amount_Paid.Text = ATH_Amount_Paid.ToString();

        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
       

            LHS_Amt = Util.String2Decimal(dr["LHS Amt"].ToString());
            TDS_on_LHS = Util.String2Decimal(dr["TDS on LHS"].ToString());
            Charity = Util.String2Decimal(dr["Charity"].ToString());
            Munishana_Deduction= Util.String2Decimal(dr["Munishana Deduction"].ToString());
            Total_BTH_Amt = Util.String2Decimal(dr["Total BTH Amt"].ToString());
            Other_Charges_Paid  = Util.String2Decimal(dr["Other Charges Paid"].ToString());

            TDS_on_Other_Charges= Util.String2Decimal(dr["TDS on Other Charges"].ToString());
            Other_Charges_Deducted = Util.String2Decimal(dr["Other Charges Deducted"].ToString());
            Total_ATH = Util.String2Decimal(dr["Total ATH"].ToString());
            ATH_Amt = Util.String2Decimal(dr["ATH Amt"].ToString());
            ATH_Amount_Paid = Util.String2Decimal(dr["ATH Amount Paid"].ToString());
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
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Vehcle_Type", SqlDbType.Int, 0,ddl_Truck.SelectedValue),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
            objDAL.MakeInParams("@Truck_No", SqlDbType.VarChar, 0,Txt_Vehicle_No.Text),
            objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0,ddl_Veh_Cat.SelectedValue),
            objDAL.MakeInParams("@Broker_Name", SqlDbType.VarChar, 0,Txt_Consignor_name.Text),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EF_RPT_LHPO_Hire_Register_GRD]", objSqlParam, ref ds);

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
        dr["lhpo_caption No"] = "Total";

        dr["LHS Amt"] = LHS_Amt;
        dr["TDS on LHS"] = TDS_on_LHS;
        dr["Charity"] = Charity;
        dr["Munishana Deduction"] = Munishana_Deduction;
        dr["Total BTH Amt"] = Total_BTH_Amt;
        dr["Other Charges Paid"] = Other_Charges_Paid;

        dr["TDS on Other Charges"] = TDS_on_Other_Charges;
        dr["Other Charges Deducted"] = Other_Charges_Deducted;
        dr["Total ATH"] = Total_ATH;
        dr["ATH Amt"] = ATH_Amt;
        dr["ATH Amount Paid"] = ATH_Amount_Paid;     

        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

 
}
