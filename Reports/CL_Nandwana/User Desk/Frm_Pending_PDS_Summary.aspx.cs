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

public partial class Reports_CL_Nandwana_UserDesk_Frm_Pending_PDS_Summary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal PDS, Articles, Total_Gc;
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
            objcommon.SetStandardCaptionForGrid(dg_Details);
            BindGrid("form_and_pageload", e);
            
        }
             
         
    }
 
    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_PDS, lbl_SumArticles, lbl_SumTotal_Gc, lbl_SumTotalFreight;

            lbl_PDS = (Label)e.Item.FindControl("lbl_SumTotal_PDS");
            lbl_SumArticles = (Label)e.Item.FindControl("lbl_SumArticles");
            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");

            lbl_PDS.Text = PDS.ToString();
            lbl_SumArticles.Text = Articles.ToString();
            lbl_SumTotal_Gc.Text = Total_Gc.ToString();
            lbl_SumTotalFreight.Text = Total_Freight.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int   Delivery_branch_Id;
            LinkButton  lbtn_DlyBranch;

            
            Delivery_branch_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Branch_Id").ToString());
            
            lbtn_DlyBranch = (LinkButton)e.Item.FindControl("lbtn_DlyBranch");
 
            int Branch_id = UserManager.getUserParam().MainId;


            DateTime As_On_Date = DateTime.Now;
            int Division_Id = WucDivisions1.Division_ID;


            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingPDSList.aspx?Branch_ID=" + Delivery_branch_Id + "&Area_ID=0" + "&Region_ID=0");
            lbtn_DlyBranch.Attributes.Add("onclick", "return OpenF4MenuPDS('" + PathF4 + "')");
        }
    }


    

    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        PDS = Util.String2Decimal(dr["NoOfPDS"].ToString());
        Total_Gc = Util.String2Decimal(dr["NoOfLR"].ToString());
        Articles = Util.String2Decimal(dr["Parcls"].ToString());
        Total_Freight = Util.String2Decimal(dr["Amount"].ToString());

    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        int Branch_id = UserManager.getUserParam().MainId; ;
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0,0),
            objDAL.MakeInParams("@Area_ID", SqlDbType.Int, 0,0),
            objDAL.MakeInParams("@Region_ID", SqlDbType.Int, 0,0)
        };

        objDAL.RunProc("COM_UserDesk_Get_Pending_PDS_Summary", objSqlParam, ref ds);
       
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
        dr["NoOfPDS"] = PDS;
        dr["NoOfLR"] = Total_Gc;
        dr["Parcls"] = Articles;
        dr["Amount"] = Total_Freight;

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
    #endregion

   
}
