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
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using ClassLibraryMVP;


public partial class Finance_Reports_Frm_Rpt_Vehicle_Profitability : System.Web.UI.Page
{
    DAL objDal = new DAL();
    DataSet ds = new DataSet();
    DataSet ds_Export = new DataSet();
    DataSet ds1 = new DataSet();
    int main_id;
    string Hierarchy_Code;
    protected void Page_Load(object sender, EventArgs e)
    {
        Hierarchy_Code = (string)UserManager.getUserParam().HierarchyCode;
        main_id = (int)UserManager.getUserParam().MainId;
        if (!IsPostBack)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            Fill_VehicleNo();
        }
        Wuc_Export_To_Excel1.FileName = "Vehicle Profitability";
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }

    protected void Fill_VehicleNo()
    {
        DAL objDAL = new DAL();
        ds1 = objDAL.RunQuery("Select Vehicle_Id,Vehicle_No from dbo.EF_Master_Vehicle where Is_Active = 1");
        dd_Vehicle.DataSource = ds1;
        dd_Vehicle.DataValueField = "Vehicle_Id";
        dd_Vehicle.DataTextField = "Vehicle_No";
        dd_Vehicle.DataBind();
        dd_Vehicle.Items.Insert(0, new ListItem("Select All","0"));
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
      
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0,Convert.ToInt32(dd_Vehicle.SelectedValue)),
            objDAL.MakeInParams("@From_date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
            };

        objDal.RunProc("[FA_Rpt_Vehicle_Profitability]", sqlParam, ref ds);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
            string TotalRecords = ds.Tables[1].Rows[0][0].ToString();
            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);
        }
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void PrepareDTForExportToExcel()
    {
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            DataTable dtExcel = new DataTable(); 
            dtExcel = ds.Tables[0].Copy();          
            DataRow TotalRow = dtExcel.NewRow();
            dtExcel.Columns.Remove("ROWID");
            dtExcel.Columns.Remove("LHPO_ID");
            TotalRow["Vehicle_No"] = "Total"; //ds.Tables[2].Rows[0][0].ToString();
            TotalRow["Total Truck Hire"] = ds.Tables[2].Rows[0][0].ToString();
            TotalRow["Total Freight Income"] = ds.Tables[2].Rows[0][1].ToString();
            TotalRow["Other Charges"] = ds.Tables[2].Rows[0][2].ToString();
            TotalRow["Profit_Loss"] = ds.Tables[2].Rows[0][3].ToString();
            dtExcel.Rows.Add(TotalRow);   
            Wuc_Export_To_Excel1.SessionExporttoExcel = dtExcel;
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            lbl_Error.Text = "";
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
        }
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total, lbl_Total_Truck_Hire, lbl_Total_Fright_Income, lbl_Other_Charges, lbl_Profit_And_Loss;

            lbl_Total = (Label)e.Item.FindControl("lbl_Total");
            lbl_Total_Truck_Hire = (Label)e.Item.FindControl("lbl_Total_Truck_Hire");
            lbl_Total_Fright_Income = (Label)e.Item.FindControl("lbl_Total_Fright_Income");
            lbl_Other_Charges = (Label)e.Item.FindControl("lbl_Other_Charges");
            lbl_Profit_And_Loss = (Label)e.Item.FindControl("lbl_Profit_And_Loss");

            lbl_Total.Text = "Total : ";
            lbl_Total_Truck_Hire.Text = Convert.ToString(ds.Tables[2].Compute("SUM([Total Truck Hire])", ""));
            lbl_Total_Fright_Income.Text = Convert.ToString(ds.Tables[2].Compute("SUM([Total Freight Income])", ""));
            lbl_Other_Charges.Text = Convert.ToString(ds.Tables[2].Compute("SUM([Other Charges])", ""));
            lbl_Profit_And_Loss.Text = Convert.ToString(ds.Tables[2].Compute("SUM(Profit_Loss)", ""));
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btn_Total_Fright_Income;
            string LHPO_Id;

            btn_Total_Fright_Income = (LinkButton)e.Item.FindControl("btn_Total_Fright_Income");
            LHPO_Id = ((HiddenField)e.Item.FindControl("hdn_LHPO_Id")).Value;

            btn_Total_Fright_Income.Attributes.Add("onclick", "return Open_GC_Details('" + LHPO_Id + "')");
        }
    }
}
