using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_POD_Frm_Pending_POD_Without_Forwarding : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    private Common objCommon = new Common();
    decimal Total_Invoice_Value;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "PendingPODWithoutForwarding";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;


        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Total_Invoice_Value;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Total_Invoice_Value = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Invoice_Value");

            lbl_Total.Text = "Total";
            lbl_Total_Invoice_Value.Text = Total_Invoice_Value.ToString();
        }
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
            objDAL.MakeInParams("@Check", SqlDbType.Int,0,DDL_Type.SelectedValue),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_RPT_WithoutForwarding_Pending_POD_List]", objSqlParam, ref ds);

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
        Total_Invoice_Value = Util.String2Decimal(dr["Total Invoice Value"].ToString());     
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
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
    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["Booking Region"]="Total";
        dr["Total Invoice Value"] = Total_Invoice_Value;
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
