using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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



public partial class Reports_CL_Nandwana_UserDesk_FrmPendingFreightBookingSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_Gc;
    decimal Total_Freight;
    decimal Total_Articles;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingFreightBooking";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Details);
            BindGrid("form_and_pageload", e);
            
        }
             
         
    }
 
    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
         
    }

     
    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumArticles, lbl_SumTotal_Gc, lbl_SumTotalFreight, lbl_SumTotalArticles;

            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");
            lbl_SumTotalArticles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalArticles");

            lbl_SumTotal_Gc.Text = Total_Gc.ToString();
            lbl_SumTotalFreight.Text = Total_Freight.ToString();
            lbl_SumTotalArticles.Text = Total_Articles.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int   Booking_branch_Id;
            LinkButton lbtn_BkgBranch;


            Booking_branch_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Branch_Id").ToString());
            
            lbtn_BkgBranch = (LinkButton)e.Item.FindControl("lbtn_BkgBranch");
 
            int Branch_id = UserManager.getUserParam().MainId;


            DateTime As_On_Date = DateTime.Now;
            int Division_Id = WucDivisions1.Division_ID;

        
            StringBuilder PathBkgStk = new StringBuilder(Util.GetBaseURL());
            PathBkgStk.Append("/");
            PathBkgStk.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightBookingConsignorSummary.aspx?&Booking_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Booking_branch_Id)  
                + "&BkgBranch=" + ClassLibraryMVP.Util.EncryptString(lbtn_BkgBranch.Text));

            lbtn_BkgBranch.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathBkgStk + "')");
        }
    }
    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Gc = Util.String2Decimal(dr["TotalLR"].ToString());
        Total_Freight = Util.String2Decimal(dr["TotalFreight"].ToString());
        Total_Articles = Util.String2Decimal(dr["TotalArticles"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Details.CurrentPageIndex;
        int grid_PageSize = dg_Details.PageSize;
        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }


        int Branch_id = UserManager.getUserParam().MainId; ;
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),  
            objDAL.MakeInParams("@IsDetailed",SqlDbType.Bit,0,false)  
        };

        objDAL.RunProc("EC_Opr_Bkg_BranchWise_Pending_Freight", objSqlParam, ref ds);
       
        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        
        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["Branch_Name"] = "Total";
        dr["TotalLR"] = Total_Gc;
        dr["TotalFreight"] = Total_Freight;
        dr["TotalArticles"] = Total_Articles;

        ds.Tables[0].Rows.Add(dr);

        DataRow dr1 = ds.Tables[0].NewRow();
       
        ds.Tables[0].Rows.Add(dr1);

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
    #endregion

   
}
