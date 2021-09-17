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


public partial class Reports_SalesBilling_UserDesk_Frm_Discount_Coupon_Active_Client_Details : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_Client,Total_CouponsUsed,Total_CouponDiscount;
   
    public int City_Id  
    {
        get
        {
            return Convert.ToInt32(ViewState["_CityId"]);
        }
        set
        {
            ViewState["_CityId"] = value;
        }
    }


#endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "Discount_Coupon_Active_Client_Details";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            City_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["CityId"]);
            
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
         
        
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
    }
    
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_Total_Client, lbl_SumCouponsUsed, lbl_Total_Discount;

            lbl_Total_Client = (Label)e.Item.FindControl("lbl_Total_Client");
            lbl_SumCouponsUsed = (Label)e.Item.FindControl("lbl_SumCouponsUsed");
            lbl_Total_Discount = (Label)e.Item.FindControl("lbl_Total_Discount");

            lbl_Total_Client.Text = Total_Client.ToString();
            lbl_SumCouponsUsed.Text = Total_CouponsUsed.ToString();
            lbl_Total_Discount.Text = Total_CouponDiscount.ToString();

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int CityID;
            String ClientName;

            LinkButton lbtn_CouponsUsed;


            CityID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CityID").ToString());
            ClientName = DataBinder.Eval(e.Item.DataItem, "ClientName").ToString();

            lbtn_CouponsUsed = (LinkButton)e.Item.FindControl("lbtn_CouponsUsed");

            StringBuilder PathCouponsUsed = new StringBuilder(Util.GetBaseURL());
            PathCouponsUsed.Append("/");
            PathCouponsUsed.Append("Reports/Sales Billing/Frm_Discount_Coupon_Client_Coupon_Details.aspx?CityId=" + ClassLibraryMVP.Util.EncryptInteger(CityID)
            + "&ClientName=" + ClientName.ToString());
            lbtn_CouponsUsed.Attributes.Add("onclick", "return viewwindow_ClientCouponDetails('" + PathCouponsUsed + "')");

        }

    }

   
    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Client= Util.String2Decimal(dr["TotalClient"].ToString());
        Total_CouponsUsed = Util.String2Decimal(dr["CouponsUsed"].ToString());
        Total_CouponDiscount = Util.String2Decimal(dr["CouponDiscount"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;
        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ReportType", SqlDbType.VarChar, 20,"Active Client"),
            objDAL.MakeInParams("@CityID",SqlDbType.Int,0,City_Id),
            objDAL.MakeInParams("@ClientName", SqlDbType.VarChar, 100,""),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };
        objDAL.RunProc("EC_Rpt_Discount_Coupon_Details", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        txtlblCity.Text = ds.Tables[0].Rows[0][1].ToString();

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["City"] = "Total";
        dr["ClientName"] = Total_Client;
        dr["CouponsUsed"] = Total_CouponsUsed;
        dr["CouponDiscount"] = Total_CouponDiscount;
        ds.Tables[0].Rows.Add(dr);

        dr.Table.Columns.Remove("CityID");
        dr.Table.Columns.Remove("City");

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
