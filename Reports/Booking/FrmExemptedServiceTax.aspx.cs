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
public partial class Reports_Booking_FrmExemptedServiceTax : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int Region_Id, Area_Id, Branch_Id, Report_Type_Id,Round_Off;
    DateTime From_Date, To_Date; 
    string GC_No_For_Print, TotalRecords;
    decimal Sub_Total, Service_Tax_Amount, Total_GC, Total_GC_Amount; 
  
    #endregion

    #region EventClick
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid); 
        Get_Parameters();  
        Wuc_Export_To_Excel1.FileName = "ExemptedServiceTax-" + ddl_ReportType.SelectedItem.Text; 

        if (IsPostBack == false)
        { 
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid_Details); 
            BindGrid("form_and_pageload", e); 
        }
    }
        
    protected void dg_Grid_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total_GC_Amount, lbl_TotalRound_Off, lbl_Sub_Total, lbl_Service_Tax_Amount;
    
            lbl_Total_GC_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_GC_Amount");
            lbl_TotalRound_Off = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalRound_Off"); 
            lbl_Sub_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_Total");
            lbl_Service_Tax_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Service_Tax_Amount");
                  
            lbl_Total_GC_Amount.Text = Total_GC_Amount.ToString();
            lbl_TotalRound_Off.Text = Round_Off.ToString(); 
            lbl_Sub_Total.Text = Sub_Total.ToString();
            lbl_Service_Tax_Amount.Text = Service_Tax_Amount.ToString();
        }
    }

    protected void dg_Grid_Details_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_Summary_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total_GC_Amount, lbl_TotalRound_Off, lbl_Sub_Total, lbl_Service_Tax_Amount;
           
            lbl_Total_GC_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_GC_Amount");
            lbl_TotalRound_Off = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalRound_Off"); 
            lbl_Sub_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_Total");
            lbl_Service_Tax_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Service_Tax_Amount"); 
            
            lbl_Total_GC_Amount.Text = Total_GC_Amount.ToString();
            lbl_TotalRound_Off.Text = Round_Off.ToString(); 
            lbl_Sub_Total.Text = Sub_Total.ToString();
            lbl_Service_Tax_Amount.Text = Service_Tax_Amount.ToString();
        } 
    } 

    protected void btn_View_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = ""; 

            dg_Grid_Details.CurrentPageIndex = 0;
            dg_Grid_Details.Visible = true; 
        }
            BindGrid("form", e);
    
    }
    #endregion

    #region Other_Functions

    private void Get_Parameters()
    {
        Region_Id = Wuc_Region_Area_Branch1.RegionID;
        Area_Id = Wuc_Region_Area_Branch1.AreaID;
        Branch_Id = Wuc_Region_Area_Branch1.BranchID;
        Report_Type_Id = Convert.ToInt32(ddl_ReportType.SelectedValue);  
        From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        To_Date = Wuc_From_To_Datepicker1.SelectedToDate;
         
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0]; 
        
            GC_No_For_Print = "Total"; 
            Sub_Total = Util.String2Decimal(dr["TotalSub_Total"].ToString());
            Service_Tax_Amount = Util.String2Decimal(dr["TotalService_Tax_Amount"].ToString());
            Round_Off = Util.String2Int(dr["TotalRound_Off"].ToString());
            Total_GC_Amount = Util.String2Decimal(dr["TotalTotal_GC_Amount"].ToString()); 
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex, grid_PageSize;

        grid_currentpageindex = dg_Grid_Details.CurrentPageIndex;
        grid_PageSize = dg_Grid_Details.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        Get_Parameters();

        SqlParameter[] ObjSqlParam = {
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,4, Region_Id),
            objDAL.MakeInParams("@Area_ID", SqlDbType.Int,4,Area_Id),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,4,Branch_Id),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Todate", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Report_Type_Id",SqlDbType.Int,4,Report_Type_Id), 
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize) 
            };

        
            objDAL.RunProc("EC_RPT_Exempted_Service_Tax_Grid", ObjSqlParam, ref ds);
            if (CallFrom == "form_and_pageload") return;
            dg_Grid_Details.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            TotalRecords = ds.Tables[2].Rows[0][0].ToString();
           
            calculate_totals();
            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid_Details, ds.Tables[0], CallFrom, lbl_Error,TotalRecords);
 
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow(); 

            dr["Sub_Total"] = Sub_Total;
            dr["Service_Tax_Amount"] = Service_Tax_Amount;
            dr["Round_Off"] = Round_Off; 
            dr["Total_GC_Amount"] = Total_GC_Amount; 

            ds.Tables[0].Rows.Add(dr);

            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
            
        }
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

