using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Booking_FrmDailyBookingStock : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds; 
    int dlyAreaid, dlylocationid, Region_Id, Area_Id, Branch_Id;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DailyBookingStock";
        dlyAreaid =  Convert.ToInt32(Request.QueryString["strdlyAreaid"]);
        dlylocationid =  Convert.ToInt32(Request.QueryString["strdlylocationid"]);
        Region_Id =  Convert.ToInt32(Request.QueryString["Region_Id"]);
        Area_Id =  Convert.ToInt32(Request.QueryString["Area_id"]);
        Branch_Id =  Convert.ToInt32(Request.QueryString["Branch_id"]);

        if (IsPostBack == false)
        {
            //lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            //lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            
            
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e);
            //WucFilter1.setddldatasource(ds);
        }
    }
    
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        //string msg = "";
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        //}
        //else
        //{
            //lbl_Error.Text = msg;
            //dg_Grid.Visible = false;
        //}
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        } 

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@BkgRegion_ID", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@BkgArea_ID", SqlDbType.Int,0,Area_Id),
            objDAL.MakeInParams("@BkgBranch_ID", SqlDbType.Int,0,Branch_Id), 
            objDAL.MakeInParams("@DlyAreaID", SqlDbType.Int,0,dlyAreaid), 
            objDAL.MakeInParams("@DlyLocationID", SqlDbType.Int,0,dlylocationid), 
        }; 
        objDAL.RunProc("[EC_RPT_Current_Stock_OMBharat_Details]", objSqlParam, ref ds);
         

        //if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[0].Rows.Count.ToString(); 

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Copy();
        dt.Columns.Remove("GC_ID");
        dt.Columns.Remove("DlyAreaID");
        dt.Columns.Remove("DlyLocationID");
        Wuc_Export_To_Excel1.SessionExporttoExcel = dt;
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton lbtn_DeliveryArea, lbtn_DeliveryLocation;

        //if ((e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) || (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem))
        //{

           

        // if ((Convert.ToInt32(e.Item.Cells[1].Text)) > 0)
        // {
        //     e.Item.BackColor = System.Drawing.Color.LightGreen;
        //     e.Item.Cells[3].Font.Bold = true;
             
        //     e.Item.Cells[4].Font.Bold = true;
             
        //     e.Item.Cells[5].Font.Bold = true;
             
        //     e.Item.Cells[6].Font.Bold = true;
             
        //     e.Item.Cells[7].Font.Bold = true;  
             
        // }
        // if ((e.Item.Cells[3].Text) == "Total")
        // {
        //     e.Item.BackColor = System.Drawing.Color.Green;
        //     e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
        //     e.Item.Font.Bold = true;   
        // }
        //}
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
