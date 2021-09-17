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



public partial class Reports_Sales_Billing_FrmClientPreviousBusinessMonthlyDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal TotalLR,TotalParcls, TotalInvValue, TotalFreight;
   

    #endregion

    public String ClientName
    {
        get
        {
            return Convert.ToString(ViewState["_ClientName"]);
        }
        set
        {
            ViewState["_ClientName"] = value;
        }
    }

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
    public Boolean  Is_Consignee_Regular_Client
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

    public String MonthName
    {
        get
        {
            return Convert.ToString(ViewState["_MonthName"]);
        }
        set
        {
            ViewState["_MonthName"] = value;
        }
    }

    public String Year
    {
        get
        {
            return Convert.ToString(ViewState["_Year"]);
        }
        set
        {
            ViewState["_Year"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            ClientName = Request.QueryString["ClientName"];
            Consignee_Client_ID = Convert.ToInt32(Request.QueryString["Consignee_Client_ID"]);
            Is_Consignee_Regular_Client = Convert.ToBoolean(Request.QueryString["Is_Consignee_Regular_Client"]);

            MonthName  = Request.QueryString["MonthName"];
            Year = Request.QueryString["Year"];


            lbl_ClientName.Text = ClientName;
            lbl_Month.Text = MonthName + " - " + Year;

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotalLR  = Util.String2Decimal(dr["TotalLR"].ToString());
        TotalParcls = Util.String2Decimal(dr["TotalParcls"].ToString());
        TotalInvValue = Util.String2Decimal(dr["TotalInvValue"].ToString());
        TotalFreight = Util.String2Decimal(dr["TotalFreight"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;


        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Consignee_Client_ID", SqlDbType.Int, 0,Consignee_Client_ID),
            objDAL.MakeInParams("@Is_Consignee_Regular_Client", SqlDbType.Bit, 0,Is_Consignee_Regular_Client),
            objDAL.MakeInParams("@MonthName",SqlDbType.VarChar,20,MonthName),  
            objDAL.MakeInParams("@Year",SqlDbType.VarChar,4,Year),  
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom)
        };

        objDAL.RunProc("EC_RPT_ClientPreviousBusiness_MonthlyDetails", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
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
            Label lbl_TotalLR, lbl_TotalArticles, lbl_TotalInvValue, lbl_TotalFreight;

            lbl_TotalLR = (Label)e.Item.FindControl("lbl_TotalLR");
            lbl_TotalArticles = (Label)e.Item.FindControl("lbl_TotalArticles");
            lbl_TotalInvValue = (Label)e.Item.FindControl("lbl_TotalInvValue");
            lbl_TotalFreight = (Label)e.Item.FindControl("lbl_TotalFreight");

            lbl_TotalLR.Text = TotalLR.ToString();
            lbl_TotalArticles.Text = TotalParcls.ToString();
            lbl_TotalInvValue.Text = TotalInvValue.ToString();
            lbl_TotalFreight.Text = TotalFreight.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID;
            LinkButton lnk_GCNo;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_Id").ToString());

            lnk_GCNo = (LinkButton)e.Item.FindControl("lnk_GCNo");

            lnk_GCNo.Attributes.Add("onclick", "return viewwindow_GC('" + GC_ID + "')");
        }
    }


    public void ClearVariables()
    {
        ds = null;
    }
}
