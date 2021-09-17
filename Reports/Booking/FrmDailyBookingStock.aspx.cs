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
    int SummaryBranchID, SummaryAreaID, SummaryRegionID;
    string SummaryCallFrom;
    #endregion

 
    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DailyBookingStock";
        Wuc_Region_Area_Branch1.SetRegionCaption = "Booking Region";
        Wuc_Region_Area_Branch1.SetAreaCaption= "Booking Area";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Booking Branch";

        if (IsPostBack == false)
        {

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);


            string Crypt;

            Crypt = System.Web.HttpContext.Current.Request.QueryString["FromSummary"];

            SummaryCallFrom = Crypt;

            if (Crypt == null)
            {
                BindGrid("form_and_pageload", e);
            }
            else
            {
                tbl_input_screen1.Visible = false;
                tbl_input_screen2.Visible = false;
                tbl_input_screen3.Visible = true;

                SummaryRegionID = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Region_Id"]);
                SummaryAreaID = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Area_Id"]);
                SummaryBranchID = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Branch_Id"]);

                lbl_BranchName.Text = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["Branch_Name"]);

                BindGrid("FromSummary", e);
            }
        }

        StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4.Append("/Reports/Booking/FrmDailyBookingStockSummary.aspx?Region_ID=" + Wuc_Region_Area_Branch1.RegionID +  "&Area_ID=" + Wuc_Region_Area_Branch1.AreaID  +  "&Branch_ID=" + Wuc_Region_Area_Branch1.BranchID );
        btn_Summary.Attributes.Add("onclick", "return OpenSummary('" + PathF4 + "')");

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


        int Region_Id, Area_id, Branch_id;

        if (CallFrom == "FromSummary")
        {
            Region_Id = SummaryRegionID;
            Area_id = SummaryAreaID;
            Branch_id = SummaryBranchID; 
        }
        else
        {
            Region_Id = Wuc_Region_Area_Branch1.RegionID;
            Area_id = Wuc_Region_Area_Branch1.AreaID;
            Branch_id = Wuc_Region_Area_Branch1.BranchID; 
        }


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@BkgRegion_ID", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@BkgArea_ID", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@BkgBranch_ID", SqlDbType.Int,0,Branch_id), 
        };



        objDAL.RunProc("[EC_RPT_Current_Stock_OMBharat]", objSqlParam, ref ds);
         

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[0].Rows.Count.ToString();
        //calculate_totals();

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
        dt.Columns.Remove("SrNo");
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
        HiddenField hdn_DeliveryArea, hdn_DeliveryLocation;
        string strdlyAreaid = "", strdlylocationid;
        if ((e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) || (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem))
        {

            lbtn_DeliveryArea = (LinkButton)e.Item.FindControl("lbtn_DeliveryArea");
            lbtn_DeliveryLocation = (LinkButton)e.Item.FindControl("lbtn_DeliveryLocation");


            int Region_Id, Area_id, Branch_id;

            if (SummaryCallFrom == "1")
            {
                Region_Id = SummaryRegionID;
                Area_id = SummaryAreaID;
                Branch_id = SummaryBranchID;
            }
            else
            {
                Region_Id = Wuc_Region_Area_Branch1.RegionID;
                Area_id = Wuc_Region_Area_Branch1.AreaID;
                Branch_id = Wuc_Region_Area_Branch1.BranchID;
            }


            hdn_DeliveryArea = (HiddenField)e.Item.FindControl("hdn_DeliveryArea");
            hdn_DeliveryLocation = (HiddenField)e.Item.FindControl("hdn_DeliveryLocation");
            strdlyAreaid = hdn_DeliveryArea.Value;
            strdlylocationid = hdn_DeliveryLocation.Value;

            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");
            Path.Append("Reports/Booking/FrmDetailedDailyBkgStk.aspx?strdlyAreaid=" + strdlyAreaid + "&strdlylocationid=" + strdlylocationid + "&Region_Id=" + Region_Id + "&Area_id=" + Area_id + "&Branch_id=" + Branch_id);
            lbtn_DeliveryArea.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");
            lbtn_DeliveryLocation.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");


         if ((Convert.ToInt32(e.Item.Cells[1].Text)) > 0)
         {
             e.Item.BackColor = System.Drawing.Color.LightGreen;
             e.Item.Cells[3].Font.Bold = true;
             
             e.Item.Cells[4].Font.Bold = true;
             
             e.Item.Cells[5].Font.Bold = true;
             
             e.Item.Cells[6].Font.Bold = true;
             
             e.Item.Cells[7].Font.Bold = true;  
             
         }
         if ((e.Item.Cells[3].Text) == "Total")
         {
             e.Item.BackColor = System.Drawing.Color.Green;
             e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
             e.Item.Font.Bold = true;   
         }
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

    protected void dg_Grid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string strdlyAreaid = "", strdlylocationid;
        //lbtn_DeliveryArea = (LinkButton)e.Item.FindControl("lbtn_DeliveryArea");
        //lbtn_DeliveryLocation = (LinkButton)e.Item.FindControl("lbtn_DeliveryLocation");

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID; 

        //if (e.CommandName == "deliveryarea")
        //{  
        //    strdlyAreaid = Convert.ToString(e.CommandArgument.ToString());  
        //    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Booking/FrmDetailedDailyBkgStk.aspx?strdlyAreaid=" + strdlyAreaid + "&Region_Id=" + Region_Id + "&Area_id=" + Area_id + "&Branch_id=" + Branch_id));
        // }
        //else if (e.CommandName == "deliverylocation")
        //{

        //    strdlylocationid = Convert.ToString(e.CommandArgument.ToString());  
        //    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Booking/FrmDetailedDailyBkgStk.aspx?strdlylocationid=" + strdlylocationid + "&Region_Id=" + Region_Id + "&Area_id=" + Area_id + "&Branch_id=" + Branch_id));
        //}
    }
}
