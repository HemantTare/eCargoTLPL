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

//Author : Harshal Sapre
//Desc   : Delivery Stock List Report
//Date   : 14-01-09

public partial class Reports_CL_Nandwana_UserDesk_Frm_Bkg_BranchWise_Dly_Stock : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Total_Gc;
    decimal Basic_Freight, Total_Freight, Invoice_Value; 
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DeliveryStockList";

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
            Label lbl_SumArticles, lbl_SumTotal_Gc, lbl_SumTotalFreight;

            lbl_SumArticles = (Label)e.Item.FindControl("lbl_SumArticles");
            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");
            
            lbl_SumArticles.Text = Articles.ToString();
            lbl_SumTotal_Gc.Text = Total_Gc.ToString();
            lbl_SumTotalFreight.Text = Total_Freight.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int   Delivery_branch_Id;
            LinkButton lnk_GC_No, lbtn_DlyBranch;

            
            Delivery_branch_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Delivery_branch_Id").ToString());
            
            lbtn_DlyBranch = (LinkButton)e.Item.FindControl("lbtn_DlyBranch");
 
            int Branch_id = UserManager.getUserParam().MainId;


            DateTime As_On_Date = DateTime.Now;
            int Division_Id = WucDivisions1.Division_ID;

        
            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Bkg_BranchWise_Dly_Stock_Summary.aspx?Branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_id)
                + "&As_On_Date=" + As_On_Date.ToString()
                + "&Division_Id=" + ClassLibraryMVP.Util.EncryptInteger(Division_Id)
                + "&Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)  
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(lbtn_DlyBranch.Text));

            lbtn_DlyBranch.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDlyStk + "')");
        }
    }
    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Gc = Util.String2Decimal(dr["Total_GC"].ToString());
        ChargedWeight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        Actual_Weight = Util.String2Decimal(dr["Total_Actual_Wt"].ToString());
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total_Freight"].ToString());
        Invoice_Value = Util.String2Decimal(dr["Total_Invoice_Value"].ToString());
       
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
            objDAL.MakeInParams("@As_On_Date", SqlDbType.DateTime, 0,As_On_Date),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),  
            objDAL.MakeInParams("@Dly_Branch_Id",SqlDbType.Int,20,0), 
            objDAL.MakeInParams("@IsDetailed",SqlDbType.Bit,0,false)  
        };

        objDAL.RunProc("EC_Opr_Booking_BranchWise_Delivery_Stock", objSqlParam, ref ds);
       
        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        
        objcommon.ValidateReportForm(dg_Details, ds.Tables[3], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["gc_caption Date"] = "Total";
        dr["gc_caption No"] = Total_Gc;
        dr["Charged Weight"] = ChargedWeight;
        dr["Actual Weight"] = Actual_Weight;
        dr["Articles"] = Articles;
        dr["Basic Freight"] = Basic_Freight;
        dr["Total Freight"] = Total_Freight;
        dr["Invoice Value"] = Invoice_Value;

        ds.Tables[0].Rows.Add(dr);

        DataRow dr1 = ds.Tables[0].NewRow();
        dr1["gc_caption Date"] = "Payment Total";
       
        ds.Tables[0].Rows.Add(dr1);

       
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[3];
        
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
