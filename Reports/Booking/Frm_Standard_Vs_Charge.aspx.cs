using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Booking_Frm_Standard_Vs_Charge : System.Web.UI.Page
{
    private DataSet ds;

    decimal Charged_Weight, Gc_Amount_Chg, Basic_chg, Basic_Std, Basic_Var, GC_Amt_Std, Var_Per;
    int Var_Amt; 

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "StandardVsCharged";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Charged_Weight = Util.String2Decimal(dr["Charged Weight"].ToString());
        Gc_Amount_Chg = Util.String2Decimal(dr["gc_caption Amt Charged"].ToString());
        Basic_chg = Util.String2Decimal(dr["Basic Freight"].ToString());
        Basic_Std = Util.String2Decimal(dr["Standard Freight"].ToString());
        Basic_Var = Util.String2Decimal(dr["Basic Variance"].ToString());
        GC_Amt_Std = Util.String2Decimal(dr["gc_caption Amt Standard"].ToString());
        Var_Amt = Util.String2Int(dr["Variance Amount"].ToString());
        Var_Per = Util.String2Decimal(dr["Variance Percent"].ToString());
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
        int From_Var = Util.String2Int(Txt_From_Var.Text.Trim() == "" ? "0" : Txt_From_Var.Text.Trim());
        int To_Var = Util.String2Int(Txt_To_Var.Text.Trim() == "" ? "0" : Txt_To_Var.Text.Trim());
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int check;
        if (Txt_From_Var.Text == "" && Txt_To_Var.Text == "")
        {
            check = 1;
        }
        else
        {
            check = 0;
        }
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@FromVar", SqlDbType.Int,0,From_Var), 
            objDAL.MakeInParams("@ToVar", SqlDbType.Int,0,To_Var), 
            objDAL.MakeInParams("@Check", SqlDbType.Int,0,check),            
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

        objDAL.RunProc("[EC_RPT_Standard_VS_Charged_Amount]", objSqlParam, ref ds);
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
        DataRow dr = ds.Tables[0].NewRow();
            dr["gc_caption No"] = "Total";
            dr["Charged Weight"] = Charged_Weight;
            dr["gc_caption Amt Charged"] = Gc_Amount_Chg;
            dr["Basic Freight"] = Basic_chg;
            dr["Standard Freight"] = Basic_Std;
            dr["Basic Variance"] = Basic_Var;
            dr["gc_caption Amt Standard"] = GC_Amt_Std;
            dr["Variance Amount"] = Var_Amt;
            dr["Variance Percent"] = Var_Per;

        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Charged_Weight, lbl_Gc_Amount,
            lbl_BasicChrged, lbl_BasicStnderd, lbl_BasicVariance, lbl_GCAmtStanderd,
            lbl_var_amt, lbl_var_per;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Charged_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Charged_Weight");
            lbl_Gc_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_GC_Amt");
            lbl_BasicChrged = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicChrged");
            lbl_BasicStnderd = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicStnderd");
            lbl_BasicVariance = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicVariance");
            lbl_GCAmtStanderd = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_GCAmtStanderd");
            lbl_var_amt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_var_amt");
            lbl_var_per = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_var_per");

            lbl_Total.Text = "Total";
            lbl_Charged_Weight.Text = Charged_Weight.ToString();
            lbl_Gc_Amount.Text = Gc_Amount_Chg.ToString();
            lbl_BasicChrged.Text =Basic_chg.ToString();
            lbl_BasicStnderd.Text = Basic_Std.ToString();
            lbl_BasicVariance.Text = Basic_Var.ToString();
            lbl_GCAmtStanderd.Text =GC_Amt_Std.ToString();
            lbl_var_amt.Text = Var_Amt.ToString();
            lbl_var_per.Text = Var_Per.ToString();
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
            dg_Grid.Visible = false;
            lbl_Error.Text = msg;
        }
    }

    protected void dg_Grid_SelectedIndexChanged(object sender, EventArgs e)
    {
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
