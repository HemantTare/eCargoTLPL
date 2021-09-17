using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_Inward_Frm_Leakage_Breakage_Report : System.Web.UI.Page
{
    decimal Charged_Weight,Freight,Invoice_Value,Received_Weight,Damaged_Value;
    int Articles,Damaged_Articles;
    private DataSet ds;
    private DAL objDAL = new DAL();         
            
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "Leakage-Breakage";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total,lbl_damaged_Articles,lbl_damaged_Value, lbl_Weight, lbl_Total_Articles, lbl_Frieght, lbl_V_Val, lbl_Rec_Wght;


            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Weight");
            lbl_Total_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Articles");
            lbl_Frieght = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Frieght");
            lbl_V_Val = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_V_Val");
            lbl_Rec_Wght = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Rec_Wght");
            lbl_damaged_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_damaged_Articles");
            lbl_damaged_Value = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_damaged_Value");

            lbl_Total.Text = "Total";
            lbl_Weight.Text = Charged_Weight.ToString();
            lbl_Total_Articles.Text=Articles.ToString();
            lbl_Frieght.Text=Freight.ToString();
            lbl_V_Val.Text=Invoice_Value.ToString();
            lbl_Rec_Wght.Text= Received_Weight.ToString();
            lbl_damaged_Articles.Text=Damaged_Articles.ToString();
            lbl_damaged_Value.Text = Damaged_Value.ToString();

        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Charged_Weight = Util.String2Decimal(dr["Charged Weight"].ToString());
        Articles=Util.String2Int(dr["Total Articles"].ToString());
        Freight = Util.String2Decimal(dr["Frieght"].ToString());
        Invoice_Value = Util.String2Decimal(dr["Invoice Value"].ToString());
        Received_Weight = Util.String2Decimal(dr["Received Weight"].ToString());
        Damaged_Articles = Util.String2Int(dr["Damaged Articles"].ToString());
        Damaged_Value = Util.String2Decimal(dr["Damaged Value"].ToString());
    }
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
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

        int type = Convert.ToInt32(ddl_Type.SelectedValue);
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              

            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
          
            objDAL.MakeInParams("@status_type",SqlDbType.Int,0,type),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_RPT_LeakageBreakege_Material]", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

            
        
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
           

        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    DataRow dr;
        //    calculate_totals();
        //    dr = ds.Tables[0].NewRow();
        //    dr["gc_caption No"] = "Total";
        //    dr["Charged Weight"] = Charged_Weight;
        //    dr["Total Articles"] = Articles;
        //    dr["Frieght"] = Freight;
        //    dr["Invoice Value"] = Invoice_Value;
        //    dr["Received Weight"] = Received_Weight;
        //    dr["Damaged Articles"] = Damaged_Articles;
        //    dr["Damaged Value"] = Damaged_Value;

        //    ds.Tables[0].Rows.Add(dr);
        //    Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Copy();
        //    ds.Tables[0].Rows.RemoveAt(ds.Tables[0].Rows.Count - 1);
        //    dg_Grid.DataSource = ds;
        //    dg_Grid.DataBind();
        //    SessionLeakageBreakage = ds;
        //    dg_Grid.Visible = true;
        //    lbl_Error.Text = "";
        //}
        //else
        //{
        //    dg_Grid.Visible = false;
        //    lbl_Error.Text = "Record Not Found!";
        //}
    }
    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["gc_caption No"] = "Total";
        dr["Charged Weight"] = Charged_Weight;
        dr["Total Articles"] = Articles;
        dr["Frieght"] = Freight;
        dr["Invoice Value"] = Invoice_Value;
        dr["Received Weight"] = Received_Weight;
        dr["Damaged Articles"] = Damaged_Articles;
        dr["Damaged Value"] = Damaged_Value;
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
