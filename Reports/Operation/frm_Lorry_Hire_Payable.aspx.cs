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

public partial class Reports_Operation_frm_Lorry_Hire_Payable : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal Total_Truck_Hire, Advance_Amount, Balance_Amount, Advance_Amount_As_On, Balance_Amount_As_On;
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
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_Total_Truck_Hire, lbl_Advance_Amount, lbl_Balance_Amount, lbl_Advance_Amount_As_On,
                lbl_Balance_Amount_As_On;

            lbl_Total_Truck_Hire = (Label)e.Item.FindControl("lbl_Total_Truck_Hire");
            //lbl_Advance_Amount = (Label)e.Item.FindControl("lbl_Advance_Amount");
            //lbl_Balance_Amount = (Label)e.Item.FindControl("lbl_Balance_Amount");
            lbl_Advance_Amount_As_On = (Label)e.Item.FindControl("lbl_Advance_Amount_As_On");
            lbl_Balance_Amount_As_On = (Label)e.Item.FindControl("lbl_Balance_Amount_As_On");

            lbl_Total_Truck_Hire.Text = Total_Truck_Hire.ToString();
            //lbl_Advance_Amount.Text = Advance_Amount.ToString();
            //lbl_Balance_Amount.Text = Balance_Amount.ToString();
            lbl_Advance_Amount_As_On.Text=Advance_Amount_As_On.ToString();
            lbl_Balance_Amount_As_On.Text = Balance_Amount_As_On.ToString();
        }
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Truck_Hire = Util.String2Decimal(dr["Total Lorry Hire"].ToString());
        //Advance_Amount = Util.String2Decimal(dr["Advance Amount"].ToString());
        //Balance_Amount = Util.String2Decimal(dr["Balance Amount"].ToString());
        Advance_Amount_As_On = Util.String2Decimal(dr["Advance Amount Payable"].ToString());
        Balance_Amount_As_On = Util.String2Decimal(dr["Balance Amount Payable"].ToString());
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
        DateTime As_On_Date = WucDatePicker1.SelectedDate;
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
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@As_On_Date",SqlDbType.DateTime,0,As_On_Date)
        };

        objDAL.RunProc("[EC_RPT_LHPO_Hire_Payable_GRD_Nandwana]", objSqlParam, ref ds);

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
        dr["Total Lorry Hire"] = Total_Truck_Hire;
        //dr["Advance Amount"] = Advance_Amount;
        //dr["Balance Amount"] = Balance_Amount;
        dr["Advance Amount Payable"] = Advance_Amount_As_On;
        dr["Balance Amount Payable"] = Balance_Amount_As_On ;

        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

}
