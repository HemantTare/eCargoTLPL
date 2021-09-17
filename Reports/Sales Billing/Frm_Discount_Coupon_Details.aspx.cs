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

public partial class Reports_SalesBilling_Frm_Discount_Coupon_Details : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal TotalClient, TotalActive, CouponsUsed, ActualDiscount;
    
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "Discount_Coupon_Details";

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
            Label lbl_SumTotalClient, lbl_SumActive, lbl_SumCouponsUsed, lbl_SumActualDiscount;

            lbl_SumTotalClient = (Label)e.Item.FindControl("lbl_SumTotalClient");
            lbl_SumActive = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumActive");
            lbl_SumCouponsUsed = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumCouponsUsed");
            lbl_SumActualDiscount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumActualDiscount");


            lbl_SumTotalClient.Text = TotalClient.ToString();
            lbl_SumActive.Text = TotalActive.ToString();
            lbl_SumCouponsUsed.Text = CouponsUsed.ToString();
            lbl_SumActualDiscount.Text = ActualDiscount.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int   CityID;
            LinkButton lbtn_TotalClient, lbtn_Active, lbtn_CouponsUsed, lbtn_ActualDiscount;

            
            CityID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CityID").ToString());

            lbtn_TotalClient = (LinkButton)e.Item.FindControl("lbtn_TotalClient");
            lbtn_Active = (LinkButton)e.Item.FindControl("lbtn_Active");
            lbtn_CouponsUsed = (LinkButton)e.Item.FindControl("lbtn_CouponsUsed");
            lbtn_ActualDiscount = (LinkButton)e.Item.FindControl("lbtn_ActualDiscount");
 
            int Division_Id = WucDivisions1.Division_ID;


            StringBuilder PathTotalClient = new StringBuilder(Util.GetBaseURL());
            PathTotalClient.Append("/");
            PathTotalClient.Append("Reports/Sales Billing/Frm_Discount_Coupon_Total_Client_Details.aspx?CityId=" + ClassLibraryMVP.Util.EncryptInteger(CityID));
            lbtn_TotalClient.Attributes.Add("onclick", "return viewwindow_TotalClient('" + PathTotalClient + "')");

            StringBuilder PathActiveClient = new StringBuilder(Util.GetBaseURL());
            PathActiveClient.Append("/");
            PathActiveClient.Append("Reports/Sales Billing/Frm_Discount_Coupon_Active_Client_Details.aspx?CityId=" + ClassLibraryMVP.Util.EncryptInteger(CityID));
            lbtn_Active.Attributes.Add("onclick", "return viewwindow_ActiveClient('" + PathActiveClient + "')");

        }
    }
    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotalClient  = Util.String2Decimal(dr["TotalClient"].ToString());
        TotalActive  = Util.String2Decimal(dr["Active"].ToString());
        CouponsUsed  = Util.String2Decimal(dr["CouponsUsed"].ToString());
        ActualDiscount  = Util.String2Decimal(dr["ActualDiscount"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ReportType", SqlDbType.VarChar ,20,"Summary"),
            objDAL.MakeInParams("@CityID",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@ClientName",SqlDbType.VarChar,100,""),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,100)};

        objDAL.RunProc("EC_Rpt_Discount_Coupon_Details", objSqlParam, ref ds);
       
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
        dr["City"] = "Total";
        dr["TotalClient"] = TotalClient ;
        dr["Active"] = TotalActive;
        dr["CouponsUsed"] = CouponsUsed;
        dr["ActualDiscount"] = ActualDiscount;
        
        ds.Tables[0].Rows.Add(dr);

        dr.Table.Columns.Remove("CityID");

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
