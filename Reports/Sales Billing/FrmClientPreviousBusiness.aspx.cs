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



public partial class Reports_Sales_Billing_FrmClientPreviousBusiness : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal TotalParcls, TotalInvValue, TotalFreight;
    decimal TotalNoOfLR2, TotalParcls2, TotalFreight2;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            tbl_Header.Visible = false;
            tbl_LastBusiness.Visible = false;
            tbl_OtherClient.Visible = false;

            txt_GCNo.Focus();
        }
    }


    private void GetData(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        int Client_Id;
        String  Is_Consignee_Regular_Client;
        SqlParameter[] objSqlParam ={  
           objDAL.MakeInParams("@GC_No",SqlDbType.VarChar ,10,txt_GCNo.Text)
        };

        objDAL.RunProc("EC_RPT_ClientPreviousBusiness", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            tbl_Header.Visible = true;
            tbl_LastBusiness.Visible = true;

            Client_Id = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            Is_Consignee_Regular_Client = ds.Tables[0].Rows[0][0].ToString();

            lnk_ClientName.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Client_Id) + "','" + Is_Consignee_Regular_Client + "')");

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

            Common objcommon3 = new Common();
            objcommon3.ValidateReportForm(dg_Grid3, ds.Tables[5], "form_and_pageload", lbl_Error);

            if (ds.Tables[5].Rows.Count > 0)
            {
                tbl_OtherClient.Visible = true;
            }
            else
            {
                tbl_OtherClient.Visible = false;
            }

            lbl_Error.Visible = false;
        }
        else
        {
            tbl_Header.Visible = false;
            tbl_LastBusiness.Visible = false;
            tbl_OtherClient.Visible = false;
            lbl_Error.Visible = true;
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        GetData("form", e);

        ds = null;
    
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

            if (SrNo == 1)
            {
                e.Item.BackColor = System.Drawing.Color.Cyan;

            }

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


    protected void dg_Grid3_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int OtherClientID;
            LinkButton lnk_OtherClientName, lnk_OtherClientBusiness;
            String Is_Consignee_Regular_Client_Other;

            OtherClientID  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_Id").ToString());
            Is_Consignee_Regular_Client_Other = DataBinder.Eval(e.Item.DataItem, "Is_Consignee_Regular_Client").ToString();

            lnk_OtherClientName = (LinkButton)e.Item.FindControl("lnk_OtherClientName");
            
            lnk_OtherClientName.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(OtherClientID) + "','" + Is_Consignee_Regular_Client_Other + "')");

            lnk_OtherClientBusiness = (LinkButton)e.Item.FindControl("lnk_OtherClientBusiness");
            lnk_OtherClientBusiness.Attributes.Add("onclick", "return viewwindow_OtherClientBusiness('" + OtherClientID + "','" + Is_Consignee_Regular_Client_Other + "')");
        }
    }
}
