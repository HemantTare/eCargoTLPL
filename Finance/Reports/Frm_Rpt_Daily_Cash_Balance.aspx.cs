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

public partial class Frm_Rpt_Daily_Cash_Balance : System.Web.UI.Page
{
    DAL objDal = new DAL();
    DataSet ds = new DataSet();
    DataSet ds_Export = new DataSet();
    private int main_id;
    private string Hierarchy_Code;
    private bool Is_Consol;

    protected void Page_Load(object sender, EventArgs e)
    {
        Hierarchy_Code = (string)WucHierarchyFiltration_FA1.HierarchyCode;
        main_id = (int)UserManager.getUserParam().MainId;
        if (!IsPostBack)
        {
        }
        if (Hierarchy_Code == "BO")
        {
            WucHierarchyFiltration_FA1.Set_Visible_Consolidate = false; 
        }
        else
        {
            WucHierarchyFiltration_FA1.Set_Visible_Consolidate = true; 
        }
        Wuc_Export_To_Excel1.FileName = "CancellationRegister";
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
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

        Is_Consol = WucHierarchyFiltration_FA1.Is_Consol;
        Hierarchy_Code = WucHierarchyFiltration_FA1.HierarchyCode;
        main_id = WucHierarchyFiltration_FA1.Main_Id;  
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit,0,Is_Consol),
            objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar,0,Hierarchy_Code),
            objDAL.MakeInParams("@MainId", SqlDbType.Int,0,main_id),
            objDAL.MakeInParams("@From_date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
            };

        objDal.RunProc("[FA_Rpt_Daywise_Cash_Balance]", sqlParam, ref ds);


        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
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
    private void PrepareDTForExportToExcel()
    {
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            DataRow TotalRow = ds.Tables[0].NewRow();
            TotalRow["Voucher_Date"] = "Total : ";
            TotalRow["Debit"] = Convert.ToString(ds.Tables[0].Compute("SUM(DEBIT)", ""));
            TotalRow["Credit"] = Convert.ToString(ds.Tables[0].Compute("SUM(CREDIT)", ""));

            ds.Tables[0].Rows.Add(TotalRow);

            ds.Tables[0].Columns.Remove("Rowid");
            ds.Tables[0].Columns.Remove("Op_Amt");
            ds.Tables[0].Columns.Remove("Voucher_Dt");
            Wuc_Export_To_Excel1.FileName = "Daily Cash Balance";
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
        }
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbl_Total, lbl_Debit, lbl_Credit, lbl_Opening_Amount;

                lbl_Total = (Label)e.Item.FindControl("lbl_Total");
                lbl_Debit = (Label)e.Item.FindControl("lbl_Debit");
                lbl_Credit = (Label)e.Item.FindControl("lbl_Credit");
                //lbl_Opening_Amount = (Label)e.Item.FindControl("lbl_Opening_Amount");

                lbl_Total.Text = "Total : ";  
                lbl_Debit.Text = Convert.ToString(ds.Tables[0].Compute("SUM(DEBIT)",""));
                lbl_Credit.Text = Convert.ToString(ds.Tables[0].Compute("SUM(CREDIT)", ""));
                //lbl_Opening_Amount.Text = "0";
            }       
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void Wuc_Export_To_Excel1_Load(object sender, EventArgs e)
    {

    }
}
