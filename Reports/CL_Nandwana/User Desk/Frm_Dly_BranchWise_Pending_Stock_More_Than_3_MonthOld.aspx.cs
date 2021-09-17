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

public partial class Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_More_Than_3_MonthOld : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Articles, Total_Gc;
    decimal Total_Freight; 
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
            BindGrid("form_and_pageload", e);
            
        }

        if (UserManager.getUserParam().MainId > 0)
        {
            Wuc_Export_To_Excel1.Visible = false;
        }
    }
 




    protected void dg_Details3_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumArticles3, lbl_SumTotal_Gc3, lbl_SumTotalFreight3;


            lbl_SumArticles3 = (Label)e.Item.FindControl("lbl_SumArticles3");
            lbl_SumTotal_Gc3 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc3");
            lbl_SumTotalFreight3 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight3");

            lbl_SumArticles3.Text = Articles.ToString();
            lbl_SumTotal_Gc3.Text = Total_Gc.ToString();
            lbl_SumTotalFreight3.Text = Total_Freight.ToString();

            if (UserManager.getUserParam().MainId > 0)
            {
                e.Item.Visible = false;
            }
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Delivery_branch_Id;
            LinkButton lbtn_DlyBranch;


            Delivery_branch_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Delivery_branch_Id").ToString());

            lbtn_DlyBranch = (LinkButton)e.Item.FindControl("lbtn_DlyBranch3");

            int Branch_id = UserManager.getUserParam().MainId;


            DateTime As_On_Date = DateTime.Now;
            int Division_Id = WucDivisions1.Division_ID;


            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Summary.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(lbtn_DlyBranch.Text) + "&IsOld=3");

            lbtn_DlyBranch.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDlyStk + "')");
        }
    }

    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[4].Rows[0];
        Total_Gc = Util.String2Decimal(dr["Total_GC"].ToString());
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total_Freight"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        int Branch_id = UserManager.getUserParam().MainId; ;
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@DeliveryArea_Id", SqlDbType.Int, 0,0),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,200),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),  
            objDAL.MakeInParams("@IsDetailed",SqlDbType.Bit,0,false), 
            objDAL.MakeInParams("@IsOld",SqlDbType.Int,0,3),
            objDAL.MakeInParams("@AllLR",SqlDbType.Int,0,0),
        };

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Stock_More_Than_3_Months", objSqlParam, ref ds);
       
        calculate_totals();

        Common objcommon = new Common();
        
        objcommon.ValidateReportForm(dg_Details3, ds.Tables[3], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[3].NewRow();
        dr["gc_caption Date"] = "Total";
        dr["gc_caption No"] = Total_Gc;
        dr["Articles"] = Articles;
        dr["Total Freight"] = Total_Freight;

        ds.Tables[3].Rows.Add(dr);

        DataRow dr1 = ds.Tables[3].NewRow();
        dr1["gc_caption Date"] = "Payment Total";
       
        ds.Tables[3].Rows.Add(dr1);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[4];
        
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
