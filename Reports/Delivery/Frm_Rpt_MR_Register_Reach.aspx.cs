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

public partial class Reports_Delivery_Frm_Rpt_MR_Register_Reach : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

    decimal sub_total,Total_MR_Amount,Deduction,Advance,Cash_Amount,Cheque_Amount;

   

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "MR Register";
        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
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
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total_Gc_Amount, lbl_Total_MR_Amount, lbl_Deduction,
                lbl_Advance_Amount, lbl_Cash_Amount, lbl_Cheque_Amount, lbl_Total;


            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Total_Gc_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Gc_Amount");
            lbl_Total_MR_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_MR_Amount");
            lbl_Deduction = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Deduction");
            lbl_Advance_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Advance_Amount");
            lbl_Cash_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cash_Amount");
            lbl_Cheque_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cheque_Amount");



            lbl_Total.Text = "Total";
            lbl_Total_Gc_Amount.Text = sub_total.ToString();
            lbl_Total_MR_Amount.Text = Total_MR_Amount.ToString();
            lbl_Deduction.Text = Deduction.ToString();
            lbl_Advance_Amount.Text = Advance.ToString();
            lbl_Cash_Amount.Text = Cash_Amount.ToString();
            lbl_Cheque_Amount.Text = Cheque_Amount.ToString();
        }
           
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
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


        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              

            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
          
         
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EF_RPT_MR_Register_GRD]", objSqlParam, ref ds);


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
        sub_total = Util.String2Decimal(dr["sub_total"].ToString());
        Total_MR_Amount = Util.String2Decimal(dr["Total_MR_Amount"].ToString());
        Advance = Util.String2Decimal(dr["Advance_Amount"].ToString());
        Cash_Amount = Util.String2Decimal(dr["Cash_Amount"].ToString());
        Cheque_Amount = Util.String2Decimal(dr["Cheque_Amount"].ToString());      

    }
    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["MR_No_For_Print"] = "Total";
        dr["Sub_Total"] = sub_total;
        dr["Total_MR_Amount"] = Total_MR_Amount;
        dr["Deduction"] = Deduction;
        dr["Advance_Amount"] = Advance;
        dr["Cash_Amount"] = Cash_Amount;
        dr["Cheque_Amount"] = Cheque_Amount;
      
        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
