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

public partial class Reports_Operation_Frm_MIS_Report : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal No_Of_CN, Total_Weight, Total_Freight;       

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "MIS";

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
        Label lbl_Total_weight, lbl_Total_Freight, lbl_No_of_cn, lbl_Header, lbl_Total;
       
        if (e.Item.ItemType == ListItemType.Footer)
        {         

            lbl_No_of_cn = (Label)e.Item.FindControl("lbl_No_of_cn");
            lbl_Total_Freight = (Label)e.Item.FindControl("lbl_Total_Freight");
            lbl_Total_weight = (Label)e.Item.FindControl("lbl_Total_weight");
            lbl_Total = (Label)e.Item.FindControl("lbl_Total");

            lbl_Total.Text = "Total";
            lbl_No_of_cn.Text = No_Of_CN.ToString();
            lbl_Total_weight.Text = Total_Weight.ToString();
            lbl_Total_Freight.Text = Total_Freight.ToString();
            
        }

        if (e.Item.ItemType == ListItemType.Header)
        {
            lbl_Header = (Label)e.Item.FindControl("lbl_Header");

            if (rbtn_type.SelectedValue == "0")
            {
                lbl_Header.Text = "Branch Name";
            }
            else
            {
                lbl_Header.Text = "Booking Type";
            }

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
        No_Of_CN = Util.String2Decimal(dr["No of Cn"].ToString());
        Total_Weight = Util.String2Decimal(dr["Total Weight"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total Freight"].ToString());
    
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        int flag;
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        if (rbtn_type.SelectedValue == "0")
        {
            flag = 0;
        }
        else
        {
            flag = 1;
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
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
             objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };
        if (rbtn_type.SelectedValue == "0")
        {
            objDAL.RunProc("[EC_RPT_MIS_Report_Branchwise_Reach]", objSqlParam, ref ds);
        }
        else
        {
            objDAL.RunProc("[EC_RPT_MIS_Report_Bookingwise_Reach]", objSqlParam, ref ds);
        }
      

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
        dr["name"] = "Total";
        dr["No of CN"] = No_Of_CN;
        dr["Total Weight"] = Total_Weight;
        dr["Total Freight"] = Total_Freight;
     

        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
  
}
