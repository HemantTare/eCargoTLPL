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

public partial class Reports_Delivery_Frm_Unsubmitted_CN_Register : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal gc_amount;
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "Unsubmitted CN Register";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
        Select_Document_Type();
    }
    protected void WucDatePicker1_Load(object sender, EventArgs e)
    {

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        //if ((objDateCommon.Vaildate_Date(WucDatePicker1.SelectedDate,WucDatePicker2.SelectedDate, ref msg)) == true)
        //{
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_Grid.Visible = false;
        //}
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
            Label lbl_gccaption_Amount;
            lbl_gccaption_Amount = (Label)e.Item.FindControl("lbl_gccaption_Amount");
            lbl_gccaption_Amount.Text = gc_amount.ToString();           
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        gc_amount = Util.String2Decimal(dr["gc_caption Amount"].ToString());
       
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        DateTime From_Date;
        DateTime To_date; 
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
        if (ddl_document_type.SelectedValue == "0")
        {
             From_Date = WucDatePicker1.SelectedDate;
             To_date = WucDatePicker2.SelectedDate;
        }
        else
        {
             From_Date = Convert.ToDateTime("1900-01-01");
             To_date =WucDatePicker1.SelectedDate;
        }
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@flag", SqlDbType.Int, 0,ddl_document_type.SelectedValue),
            objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int, 0,ddl_booking_type.SelectedValue),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),          
            objDAL.MakeInParams("@Client_Name", SqlDbType.VarChar, 0,Txt_Client_name.Text),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_RPT_Unsubmitted_CN_Register_Detail_Reach]", objSqlParam, ref ds);

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
        dr["gc_caption Amount"] = gc_amount;

        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    private void Select_Document_Type()
    {
        if (ddl_document_type.SelectedValue == "0")
        {
            lbl_from.Text = "From Date";
            lbl_to.Text = "To Date";
            lbl_to.Visible = true;
            WucDatePicker2.Visible = true;
        }
        else
        {
            lbl_from.Text = "As On Date";
            lbl_to.Visible = false;
            WucDatePicker2.Visible = false;
        }
    }
    protected void ddl_document_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        Select_Document_Type();
    }
}
