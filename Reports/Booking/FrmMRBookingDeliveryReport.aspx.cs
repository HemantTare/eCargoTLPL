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

public partial class Reports_Booking_FrmMRBookingDeliveryReport : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    Common objcommon = new Common();
    Decimal Total_MRAmount, Total_GCAmount;
    #endregion

    #region ControlsValue
    public int MRTypeId
    {
        get { return Util.String2Int(ddl_MRtype.SelectedValue); }
        set { ddl_MRtype.SelectedValue = Util.Int2String(value); }
    }
    public int BranchId
    {
        get { return Util.String2Int(ddl_Branch.SelectedValue); }
        set { ddl_Branch.SelectedValue = Util.Int2String(value); }
    }
    #endregion

    #region OtherMethods
    private void FillMRType()
    {
        DataSet ds = new DataSet();
        string Query = "";
        string Query2 = "";

        Query = "select 0 as MR_Type_Id,'All' as MR_Type Union select MR_Type_Id,MR_Type from ec_master_MR_type order by MR_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_MRtype.DataSource = ds;
        ddl_MRtype.DataTextField = "MR_Type";
        ddl_MRtype.DataValueField = "MR_Type_Id";
        ddl_MRtype.DataBind();

       
       
        Query2 = "select distinct MR_Branch_ID,Branch_Name from FA_Opr_MR inner join EC_Master_Branch on EC_Master_Branch.Branch_Id=Fa_opr_MR.MR_Branch_Id order by Branch_Name";
        ds = objcommon.EC_Common_Pass_Query(Query2);
        ddl_Branch.DataSource = ds;
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "MR_Branch_ID";
        ddl_Branch.DataBind(); 

            



    }
    #endregion 

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {

        

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "MRBooking/DeliveryReport";


        if (IsPostBack == false)
        {
            FillMRType();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (ddl_Approval.SelectedValue == "0")
        {
            e.Item.Cells[11].Visible = false;
        }
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_MRAmount,lbl_gccaptionAmount;

            lbl_MRAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_MRAmount");
            lbl_gccaptionAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_gccaptionAmount");

            lbl_MRAmount.Text = Total_MRAmount.ToString();
            lbl_gccaptionAmount.Text = Total_GCAmount.ToString();
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
        Total_MRAmount = Util.String2Decimal(dr["MR Amount"].ToString());
        Total_GCAmount = Util.String2Decimal(dr["gc_caption Amount"].ToString());
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

        //int FromRegionId = 0;
        //int FromAreaid = 0;
        //int FromBranchid = Wuc_Region_Area_Branch1.BranchID;
        

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;


        SqlParameter[] objSqlParam ={            
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,BranchId),            
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@MRTypeId",SqlDbType.Int,0,MRTypeId),
            objDAL.MakeInParams("@Approval",SqlDbType.Int,0,ddl_Approval.SelectedValue),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[dbo].[EC_RPT_MR_Booking_Delivery_Report]", objSqlParam, ref ds);



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

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["MR Amount"] = Total_MRAmount;
        dr["gc_caption Amount"] = Total_GCAmount;
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
    #endregion
}
