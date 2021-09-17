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
    Common objcommon = new Common();
    decimal gc_amount,CN_Amt,No_of_CN;
    DateCommon objDateCommon;
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "Unsubmitted CN Register";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption + ":";
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            if (Rbtn_Type.SelectedValue == "0")
            {
                dg_Grid.Visible = true;
                DataGrid1.Visible = false;
            }
            else
            {
                dg_Grid.Visible = false;
                DataGrid1.Visible =true;
            }
        }
        //Select_Document_Type();
    }
    protected void WucDatePicker1_Load(object sender, EventArgs e)
    {

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        //objDateCommon = new DateCommon();
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
            Label lbl_Total, lbl_Total1,lbl_gccaption_Amount, lbl_No_of_CN, lbl_CN_Amount;
            if (Rbtn_Type.SelectedValue == "0")
            {
                lbl_Total = (Label)e.Item.FindControl("lbl_Total");
                lbl_gccaption_Amount = (Label)e.Item.FindControl("lbl_gccaption_Amount");
                lbl_gccaption_Amount.Text = gc_amount.ToString();
                lbl_Total.Text = "Total";
            }
            else
            {
                lbl_Total1 = (Label)e.Item.FindControl("lbl_Total1");
                lbl_No_of_CN = (Label)e.Item.FindControl("lbl_No_of_CN");
                lbl_CN_Amount = (Label)e.Item.FindControl("lbl_CN_Amount");

                lbl_Total1.Text = "Total";
                lbl_No_of_CN.Text = No_of_CN.ToString();
                lbl_CN_Amount.Text = CN_Amt.ToString();
            }
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        if (Rbtn_Type.SelectedValue == "0")
        {
            gc_amount = Util.String2Decimal(dr["CN Amount"].ToString());
        }
        else
        {
            No_of_CN = Util.String2Decimal(dr["No of CN"].ToString());
            CN_Amt = Util.String2Decimal(dr["CN Amount"].ToString());
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        DateTime From_Date;
        DateTime To_date;
        int grid_PageSize;
        int grid_currentpageindex;
        

        if (Rbtn_Type.SelectedValue == "0")
        {
            grid_currentpageindex = dg_Grid.CurrentPageIndex;
            grid_PageSize = dg_Grid.PageSize;

        }
        else
        {
            grid_currentpageindex = DataGrid1.CurrentPageIndex;
            grid_PageSize = DataGrid1.PageSize;
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        From_Date = WucDatePicker1.SelectedDate;
        To_date = WucDatePicker2.SelectedDate;       
        int Division_Id = WucDivisions1.Division_ID;
        DateTime As_On_Date = WucDatePicker3.SelectedDate;
      
            SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int, 0,ddl_booking_type.SelectedValue),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),          
            objDAL.MakeInParams("@Client_Name", SqlDbType.VarChar, 0,Txt_Client_name.Text),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@As_On_Date", SqlDbType.DateTime,0,As_On_Date),
            objDAL.MakeInParams("@Billing_Branch_Wise", SqlDbType.Int,0,Convert.ToInt32(rbtn_wise.SelectedValue))
        };
            if (Rbtn_Type.SelectedValue == "0")
            {
                objDAL.RunProc("[EC_RPT_Unsubmitted_CN_Register_Detail_Nandwana]", objSqlParam, ref ds);
            }
            else
            {
                objDAL.RunProc("[EC_RPT_Unsubmitted_CN_Register_Summary_Nandwana]", objSqlParam, ref ds);
            }

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        if (Rbtn_Type.SelectedValue == "0")
        {
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
        }
        else
        {
            objcommon.ValidateReportForm(DataGrid1, ds.Tables[0], CallFrom, lbl_Error);
            dg_Grid.Visible = false;
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        if (Rbtn_Type.SelectedValue == "0")
        {
            dr["CN No"] = "Total";
            dr["CN Amount"] = gc_amount;
        }
        else
        {
            dr["Client Name"] = "Total";
            dr["No of CN"] = No_of_CN;
            dr["CN Amount"] = CN_Amt;
        }
        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

   
    protected void ddl_document_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Select_Document_Type();
    }
    protected void Rbtn_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rbtn_Type.SelectedValue == "0")
        {
            dg_Grid.Visible = true;
            DataGrid1.Visible = false;
            
        }
        else
        {
            dg_Grid.Visible = false;
            DataGrid1.Visible = true;
           
        }
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
       
    }

    protected void rbtn_wise_SelectedIndexChanged(object sender, EventArgs e)
    {
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
    }
}
