using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
public partial class Reports_Inward_Frm_Shortage_Excess : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

    decimal recieved_wt, actual_wt, sub_total, total_invoice_value,Damaged_Value, Excess_Articles_Wt;
    int Actual_article,recieved_articles,Excess_Articles,Damaged_Articles;
  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (ddl_Type.SelectedValue == "0")
        {
            Wuc_Export_To_Excel1.FileName = "ShortageArticlesReport";
        }
        else
        {
            Wuc_Export_To_Excel1.FileName = "ExcessArticlesReport";
        }

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid_Short);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Excess);
        }

    }

    protected void dg_Grid_Short_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid_Short.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_Excess_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid_Excess.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_Short_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Actual_article, lbl_Recieved_Articles,
                lbl_Actual_Weight, lbl_Received_weight, lbl_Sub_total, lbl_Total_Invoice_Value;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Actual_article = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Actual_article");
            lbl_Recieved_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Recieved_Articles");
            lbl_Actual_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Actual_Weight");
            lbl_Received_weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Received_weight");
            lbl_Sub_total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_total");
            lbl_Total_Invoice_Value = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Invoice_Value");

            lbl_Total.Text = "Total";

            lbl_Actual_article.Text = Actual_article.ToString();
            lbl_Actual_Weight.Text = actual_wt.ToString();
            lbl_Recieved_Articles.Text = recieved_articles.ToString();
            lbl_Received_weight.Text = recieved_wt.ToString();
            lbl_Sub_total.Text = sub_total.ToString();
            lbl_Total_Invoice_Value.Text = total_invoice_value.ToString();
        }
    }

    protected void dg_Grid_Excess_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Excess_Articles_Wt, lbl_Damaged_Value,
                lbl_Damaged_Articles, lbl_Excess_Articles;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Excess_Articles_Wt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Excess_Articles_Wt");
            lbl_Damaged_Value = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Damaged_Value");
            lbl_Damaged_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Damaged_Articles");
            lbl_Excess_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Excess_Articles");

            lbl_Total.Text = "Total";
            lbl_Excess_Articles_Wt.Text = Excess_Articles.ToString();
            lbl_Damaged_Value.Text = Damaged_Value.ToString();
            lbl_Damaged_Articles.Text = Damaged_Articles.ToString();
            lbl_Excess_Articles.Text = Excess_Articles.ToString();
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
           
            if (ddl_Type.SelectedValue == "0")
            {
                dg_Grid_Short.CurrentPageIndex = 0;
                dg_Grid_Short.Visible = true;
                dg_Grid_Excess.Visible = false;
            }
            else
            {
                dg_Grid_Excess.CurrentPageIndex = 0;
                dg_Grid_Excess.Visible = true;
                dg_Grid_Short.Visible = false;
            }
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid_Short.Visible = false;
            dg_Grid_Short.Visible = false;
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        int grid_currentpageindex;
        int grid_PageSize;

        if (ddl_Type.SelectedValue == "0")
        {
            grid_currentpageindex = dg_Grid_Short.CurrentPageIndex;
            grid_PageSize = dg_Grid_Short.PageSize;
        }
        else
        {
            grid_currentpageindex = dg_Grid_Excess.CurrentPageIndex;
            grid_PageSize = dg_Grid_Excess.PageSize;
        }

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
            objDAL.MakeInParams("@Region_ID", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_ID", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,Branch_id),   
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@check", SqlDbType.Int,0,ddl_Type.SelectedValue),   
            objDAL.MakeInParams("@To_Date ", SqlDbType.DateTime,0,To_date),           
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_RPT_ShortExcessMaterial]", objSqlParam, ref ds);

        Common objcommon = new Common();

        if (ddl_Type.SelectedValue == "0")
        {
            dg_Grid_Short.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            calculate_totals();
            objcommon.ValidateReportForm(dg_Grid_Short, ds.Tables[0], CallFrom, lbl_Error);
        }
        else
        {
            dg_Grid_Excess.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            calculate_totals1();
            objcommon.ValidateReportForm(dg_Grid_Excess, ds.Tables[0], CallFrom, lbl_Error);
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void calculate_totals1()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Excess_Articles = Util.String2Int(dr["Excess Articles"].ToString());
        Damaged_Articles = Util.String2Int(dr["Damaged Articles"].ToString());
        Damaged_Value = Util.String2Decimal(dr["Damaged Values"].ToString());
        Excess_Articles_Wt = Util.String2Int(dr["Excess Articles Weight"].ToString());           
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        actual_wt = Util.String2Decimal(dr["Actual Weight"].ToString());
        Actual_article = Util.String2Int(dr["Actual Article"].ToString());
        recieved_wt = Util.String2Decimal(dr["Received Weight"].ToString());
        recieved_articles = Util.String2Int(dr["Recieved Articles"].ToString());
        sub_total = Util.String2Decimal(dr["Sub Total"].ToString());
        total_invoice_value = Util.String2Decimal(dr["Total Invoice Value"].ToString());
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr,dr1;
        dr = ds.Tables[0].NewRow();
        if (ddl_Type.SelectedItem.Text == "Shortage")
        {
            dr["gc_caption No"] = "Total";
            dr["Actual Article"] = Actual_article;
            dr["Recieved Articles"] = recieved_articles;
            dr["Actual Weight"] = actual_wt;
            dr["Received Weight"] = recieved_wt;
            dr["Sub Total"] = sub_total;
            dr["Total Invoice Value"] = total_invoice_value;          
        }
        else
        {
            dr["gc_caption No"] = "Total";
            dr["Excess Articles Weight"] = Excess_Articles;
            dr["Damaged Value"] = Damaged_Value;
            dr["Damaged Articles"] = Damaged_Articles;
            dr["Excess Articles"] = Excess_Articles;
        }
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
