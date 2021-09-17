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
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_CL_Nandwana_User_Desk_FrmIncompleteProcess : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private Decimal Total_Total_Articles, TotalTotalGCAmount, Total_Advance_To_Be_Paid, Total_Balance_Payble_Amount;
    private Decimal TotalTotalGC, TotalTotalArticles;
    private Decimal TotalTotalPDSGC, TotalTotalPDSArticles;

    private int RecordCount;
    Common objcommon = new Common();
    #endregion

    #region ControlsValue
    
    #endregion
    int year_code;
    protected void Page_Load(object sender, EventArgs e)
    {
        year_code = UserManager.getUserParam().YearCode;
        
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "IncompleteProcess";
        
        Wuc_Region_Area_Branch1.SetRegionCaption = "Memo Region";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Memo Area";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Memo Branch";
            objcommon.SetStandardCaptionForGrid(dg_GridPendingMEMOforLHPO);
            objcommon.SetStandardCaptionForGrid(dg_GridLHPOforUnLoading);
            objcommon.SetStandardCaptionForGrid(dg_GridPDSforDlyConfirm);
        if (IsPostBack == false)
        {         
            Fill_AllDropDown();


            ddlReportType_SelectedIndexChanged(sender, e);

            BindGrid("form_and_pageload", e); 
            
        }
    }
    private void Fill_AllDropDown()
    {
        Common objcommon = new Common();
        DataSet ds = new DataSet();
        string Query = "";

        Query = "select 1 as ReportTypeId,'Pending Trip Memo For Unloading' as ReportType union select 2 as ReportTypeId,'Pending Invoice For Trip Memo' as ReportType union select 3 as ReportTypeId,'Pending PDS for Dly Confirm' as ReportType ";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddlReportType.DataSource = ds;
        ddlReportType.DataTextField = "ReportType";
        ddlReportType.DataValueField = "ReportTypeId";
        ddlReportType.DataBind();

        Query = "select 0 as Memo_Type_Id,'--- Select One ---' as Memo_Type union select Memo_Type_Id,Memo_Type  from EC_Master_Memo_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddlMemoType.DataSource = ds;
        ddlMemoType.DataTextField = "Memo_Type";
        ddlMemoType.DataValueField = "Memo_Type_Id";
        ddlMemoType.DataBind();
 
    }

    private void Wuc_Region_Area_Branch_PostLoad(object sender, EventArgs e)
    {
        btn_view_Click(sender, e);
    }

    protected void dg_GridPendingMEMOforLHPO_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_GridPendingMEMOforLHPO.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_GridLHPOforUnLoading_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_GridLHPOforUnLoading.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_GridPDSforDlyConfirm_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_GridPDSforDlyConfirm.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_GridPendingMEMOforLHPO_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lblTotalRecordCount,lblTotalTotal_Articles, lblTotalTotalGCAmount, lblTotal_Advance_To_Be_Paid, lblTotal_Balance_Payble_Amount;
            
            lblTotalRecordCount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalRecordCount");
            lblTotalTotal_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalTotal_Articles");
            lblTotalTotalGCAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalTotalGCAmount");
           
            lblTotalRecordCount.Text = RecordCount.ToString();
            lblTotalTotal_Articles.Text = Total_Total_Articles.ToString();
            lblTotalTotalGCAmount.Text = TotalTotalGCAmount.ToString(); 
        }
    } 
    protected void dg_GridLHPOforUnLoading_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lblUnLoadingTotalRecordCount, lblTotalTotalGC, lblTotalTotalArticles;

            lblUnLoadingTotalRecordCount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblUnLoadingTotalRecordCount");
            lblTotalTotalGC = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalTotalGC");
            lblTotalTotalArticles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalTotalArticles");

            lblUnLoadingTotalRecordCount.Text = RecordCount.ToString();
            lblTotalTotalGC.Text = TotalTotalGC.ToString();
            lblTotalTotalArticles.Text = TotalTotalArticles.ToString(); 
           
        }
    }

    protected void dg_GridPDSforDlyConfirm_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lblDlyConfirmTotalRecordCount, lblTotalTotalPDSGC, lblTotalTotalPDSArticles;

            lblDlyConfirmTotalRecordCount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDlyConfirmTotalRecordCount");
            lblTotalTotalPDSGC = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalTotalPDSGC");
            lblTotalTotalPDSArticles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTotalTotalPDSArticles");

            lblDlyConfirmTotalRecordCount.Text = RecordCount.ToString();
            lblTotalTotalPDSGC.Text = TotalTotalPDSGC.ToString();
            lblTotalTotalPDSArticles.Text = TotalTotalPDSArticles.ToString(); 
           
        }

    }

    protected void btn_view_Click(object sender, EventArgs e)
    {       
            lbl_Error.Text = "";
        
        int ReportTypeID = Convert.ToInt32(ddlReportType.SelectedValue);

        if (ReportTypeID == 1)
        {
            dg_GridPendingMEMOforLHPO.Visible = true;
            dg_GridPendingMEMOforLHPO.CurrentPageIndex = 0;
        }
        else if (ReportTypeID == 2)
        {
            dg_GridLHPOforUnLoading.Visible = true;
            dg_GridLHPOforUnLoading.CurrentPageIndex = 0;
        }
        else if (ReportTypeID == 3)
        {
            dg_GridPDSforDlyConfirm.Visible = true;
            dg_GridPDSforDlyConfirm.CurrentPageIndex = 0;
        }


           
            BindGrid("form", e);       
    }

    private void calculate_totals()
    {
        int ReportTypeID = Convert.ToInt32(ddlReportType.SelectedValue);
        if (ds.Tables.Count > 0)
        {
            DataRow dr = ds.Tables[1].Rows[0];
            DataRow drTotal = ds.Tables[2].Rows[0];
            if (ReportTypeID == 1)
            {
                Total_Total_Articles = Util.String2Decimal(dr["TotalTotal_Articles"].ToString());
                TotalTotalGCAmount = Util.String2Decimal(dr["TotalTotalGCAmount"].ToString());
                //Total_Advance_To_Be_Paid = Util.String2Decimal(dr["Total_Advance_To_Be_Paid"].ToString());
                //Total_Balance_Payble_Amount = Util.String2Decimal(dr["Total_Balance_Payble_Amount"].ToString());
                RecordCount = Util.String2Int(drTotal["RecordCount"].ToString());
            }
            if (ReportTypeID == 2)
            {
                TotalTotalGC = Util.String2Decimal(dr["TotalTotalGC"].ToString());
                TotalTotalArticles = Util.String2Decimal(dr["TotalTotalArticles"].ToString());
                RecordCount = Util.String2Int(drTotal["RecordCount"].ToString());
            }
            if (ReportTypeID == 3)
            {
                TotalTotalPDSGC = Util.String2Decimal(dr["TotalTotalPDSGC"].ToString());
                TotalTotalPDSArticles = Util.String2Decimal(dr["TotalTotalPDSArticles"].ToString());
                RecordCount = Util.String2Int(drTotal["RecordCount"].ToString());
            }
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int ReportTypeID = Convert.ToInt32(ddlReportType.SelectedValue);  
        int RegionId = Wuc_Region_Area_Branch1.RegionID;
        int AreaId = Wuc_Region_Area_Branch1.AreaID;
        int BranchId = Wuc_Region_Area_Branch1.BranchID;
        string MemoToLocation = txtMemoToLocation.Text.Trim();   
        int MemoTypeID = Convert.ToInt32(ddlMemoType.SelectedValue);

        DateTime MemoFromDate = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime MemoToDate = Wuc_From_To_Datepicker1.SelectedToDate;

        int grid_currentpageindex=0;
        int grid_PageSize=0;

        

        if (ReportTypeID == 1)
        {
            grid_currentpageindex = dg_GridPendingMEMOforLHPO.CurrentPageIndex;
            grid_PageSize = dg_GridPendingMEMOforLHPO.PageSize;

            if (CallFrom == "exporttoexcelusercontrol")
            {
                grid_currentpageindex = 0;
                grid_PageSize = 0;
            }
        }
        else if (ReportTypeID == 2)
        {
            grid_currentpageindex = dg_GridLHPOforUnLoading.CurrentPageIndex;
            grid_PageSize = dg_GridLHPOforUnLoading.PageSize;

            if (CallFrom == "exporttoexcelusercontrol")
            {
                grid_currentpageindex = 0;
                grid_PageSize = 0;
            }
        }
        else if (ReportTypeID == 3)
        {
            grid_currentpageindex = dg_GridPDSforDlyConfirm.CurrentPageIndex;
            grid_PageSize = dg_GridPDSforDlyConfirm.PageSize;

            if (CallFrom == "exporttoexcelusercontrol")
            {
                grid_currentpageindex = 0;
                grid_PageSize = 0;
            }
        } 

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@ReportTypeID",SqlDbType.Int,0,ReportTypeID),   
            objDAL.MakeInParams("@MemoRegionID",SqlDbType.Int,0,RegionId),   
            objDAL.MakeInParams("@MemoAreaID",SqlDbType.Int,0,AreaId),   
            objDAL.MakeInParams("@MemoBranchID",SqlDbType.Int,0,BranchId),
            objDAL.MakeInParams("@MemoToLocation",SqlDbType.VarChar,20,MemoToLocation),            
            objDAL.MakeInParams("@MemoTypeID",SqlDbType.Int,0,MemoTypeID),
            objDAL.MakeInParams("@MemoFromDate", SqlDbType.DateTime,0,MemoFromDate),
            objDAL.MakeInParams("@MemoToDate", SqlDbType.DateTime,0,MemoToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize) 

        };

        objDAL.RunProc("EC_RPT_IncompleteProcess_Alert", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;


        //dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();
        if (ReportTypeID == 1)
        {
            dg_GridPendingMEMOforLHPO.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            objcommon.ValidateReportForm(dg_GridPendingMEMOforLHPO, ds.Tables[0], CallFrom, lbl_Error);
        }
        else if (ReportTypeID == 2)
        {
            dg_GridLHPOforUnLoading.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            objcommon.ValidateReportForm(dg_GridLHPOforUnLoading, ds.Tables[0], CallFrom, lbl_Error);
        }
        else if (ReportTypeID == 3)
        {
            dg_GridPDSforDlyConfirm.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            objcommon.ValidateReportForm(dg_GridPDSforDlyConfirm, ds.Tables[0], CallFrom, lbl_Error);
        }
         
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }



    private void PrepareDTForExportToExcel()
    {  

        int ReportTypeID = Convert.ToInt32(ddlReportType.SelectedValue);
        if (ReportTypeID == 0) return;

        DataRow dr;
        dr = ds.Tables[0].NewRow();

        if (ReportTypeID == 1)
        {
            dr["TripMemoNo"] = RecordCount;
            dr["NoOfParcel"] = Total_Total_Articles;
            dr["TotalGCAmount"] = TotalTotalGCAmount;
            //dr["Total_Advance_To_Be_Paid"] = Total_Advance_To_Be_Paid;
            //dr["Balance_Payble_Amount"] = Total_Balance_Payble_Amount;
        }
        else if (ReportTypeID == 2)
        {
            dr["InvoiceFrom"] = RecordCount;
            dr["TotalGC"] = TotalTotalGC;
            dr["NoOfParcel"] = TotalTotalArticles;
        }
        else if (ReportTypeID == 3)
        {
            dr["PDSBranch"] = RecordCount;
            dr["TotalPDSGC"] = TotalTotalPDSGC;
            dr["TotalPDSArticles"] = TotalTotalPDSArticles;
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
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportType.SelectedValue == "1")
        {
            ddlMemoType.Visible = true;
            lblMemoType.Visible = true;
            lblMemoToLocation.Visible = true;
            txtMemoToLocation.Visible = true;
        }
        else
        {
            ddlMemoType.Visible = false;
            lblMemoType.Visible = false;
            lblMemoToLocation.Visible = false;
            txtMemoToLocation.Visible = false;

        }
        if (ddlReportType.SelectedValue == "1")
        {            
            dg_GridPendingMEMOforLHPO.Visible = true;
            dg_GridLHPOforUnLoading.Visible = false;
            dg_GridPDSforDlyConfirm.Visible = false;
        }
        else if (ddlReportType.SelectedValue == "2")
        {          
            dg_GridPendingMEMOforLHPO.Visible = false;
            dg_GridLHPOforUnLoading.Visible = true;
            dg_GridPDSforDlyConfirm.Visible = false;
        }
        else if (ddlReportType.SelectedValue == "3")
        {          
            dg_GridPendingMEMOforLHPO.Visible = false;
            dg_GridLHPOforUnLoading.Visible = false;
            dg_GridPDSforDlyConfirm.Visible = true;
        }
    }


}



