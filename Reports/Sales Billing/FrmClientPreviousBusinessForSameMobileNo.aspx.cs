using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;
using System.Text;



public partial class Reports_Sales_Billing_FrmClientPreviousBusinessForSameMobileNos : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal TotalParcls, TotalInvValue, TotalFreight;
    decimal TotalNoOfLR2, TotalParcls2, TotalFreight2;

    #endregion

    public int Consignee_Client_ID
    {
        get
        {
            return Convert.ToInt32(ViewState["_Consignee_Client_ID"]);
        }
        set
        {
            ViewState["_Consignee_Client_ID"] = value;
        }
    }
    public Boolean Is_Consignee_Regular_Client
    {
        get
        {
            return Convert.ToBoolean(ViewState["_Is_Consignee_Regular_Client"]);
        }
        set
        {
            ViewState["_Is_Consignee_Regular_Client"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            Consignee_Client_ID = Convert.ToInt32(Request.QueryString["Consignee_Client_ID"]);
            Is_Consignee_Regular_Client = Convert.ToBoolean(Request.QueryString["Is_Consignee_Regular_Client"]);

            GetData("form", e);
        }
    }


    private void GetData(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        
        SqlParameter[] objSqlParam ={  
           objDAL.MakeInParams("@Consignee_Client_ID",SqlDbType.Int,0,Consignee_Client_ID),
           objDAL.MakeInParams("@Is_Consignee_Regular_Client",SqlDbType.Bit,0,Is_Consignee_Regular_Client)

        };
        objDAL.RunProc("EC_RPT_ClientPreviousBusiness_ForSameMobileNo", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {

            lnk_ClientName.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Client_ID) + "','" + Is_Consignee_Regular_Client + "')");

            lnk_ClientName.Text = ds.Tables[0].Rows[0][2].ToString();
            lbl_DeliveryArea.Text = ds.Tables[0].Rows[0][3].ToString();
            lbl_Category.Text = ds.Tables[0].Rows[0][4].ToString();
            lbl_RegisteredOn.Text = ds.Tables[0].Rows[0][5].ToString();

            calculate_totals_10Bkg();

            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[1], "form_and_pageload", lbl_Error);

            calculate_totals_YearBusiness();
            Common objcommon2 = new Common();
            objcommon2.ValidateReportForm(dg_Grid2, ds.Tables[3], "form_and_pageload", lbl_Error);
           
            lbl_Error.Visible = false;
        }
    }


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_TotalArticles, lbl_TotalInvValue, lbl_TotalFreight;

            lbl_TotalArticles = (Label)e.Item.FindControl("lbl_TotalArticles");
            lbl_TotalInvValue = (Label)e.Item.FindControl("lbl_TotalInvValue");
            lbl_TotalFreight = (Label)e.Item.FindControl("lbl_TotalFreight");

            lbl_TotalArticles.Text = TotalParcls.ToString();
            lbl_TotalInvValue.Text = TotalInvValue.ToString();
            lbl_TotalFreight.Text = TotalFreight.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID,SrNo;
            LinkButton lnk_GCNo;

            GC_ID  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_Id").ToString());
            SrNo = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "SrNo").ToString());

            lnk_GCNo = (LinkButton)e.Item.FindControl("lnk_GCNo");

            lnk_GCNo.Attributes.Add("onclick", "return viewwindow_GC('" + GC_ID + "')");
        }
    }

    private void calculate_totals_10Bkg()
    {
        DataRow dr = ds.Tables[2].Rows[0];
        TotalParcls = Util.String2Decimal(dr["Parcls"].ToString());
        TotalInvValue = Util.String2Decimal(dr["InvoiceValue"].ToString());
        TotalFreight = Util.String2Decimal(dr["TotalFreight"].ToString());
    }

    private void calculate_totals_YearBusiness()
    {
        DataRow dr = ds.Tables[4].Rows[0];
        TotalParcls2 = Util.String2Decimal(dr["Parcls"].ToString());
        TotalNoOfLR2 = Util.String2Decimal(dr["NoOfLR"].ToString());
        TotalFreight2 = Util.String2Decimal(dr["TotalFreight"].ToString());
    }

    protected void dg_Grid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_TotalArticles2, lbl_TotalNoOfLR2, lbl_TotalFreight2;

            lbl_TotalArticles2 = (Label)e.Item.FindControl("lbl_TotalArticles2");
            lbl_TotalNoOfLR2 = (Label)e.Item.FindControl("lbl_TotalNoOfLR2");
            lbl_TotalFreight2 = (Label)e.Item.FindControl("lbl_TotalFreight2");

            lbl_TotalArticles2.Text = TotalParcls2.ToString();
            lbl_TotalNoOfLR2.Text = TotalNoOfLR2.ToString();
            lbl_TotalFreight2.Text = TotalFreight2.ToString();
        }


        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            String  MonthName,Year;
            LinkButton lnk_Month;

            int Client_Id;
            String Is_Consignee_Regular_Client;

            Client_Id = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            Is_Consignee_Regular_Client = ds.Tables[0].Rows[0][0].ToString();


            MonthName  = DataBinder.Eval(e.Item.DataItem, "MonthName").ToString();
            Year = DataBinder.Eval(e.Item.DataItem, "Year").ToString();

            lnk_Month = (LinkButton)e.Item.FindControl("lnk_Month");
            
            lnk_Month.Attributes.Add("onclick", "return viewwindow_Month('"  +  lnk_ClientName.Text +  "','" + Client_Id + "','" + Is_Consignee_Regular_Client + "','" + MonthName + "','"
            + Year + "')");

        }
    }
}
