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

public partial class Reports_CL_Nandwana_UserDesk_FrmPendingFreightDeliveryConsigneeSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_Gc, Total_Gc2, Total_GcHO;
    decimal Total_Freight, Total_Freight2, Total_FreightHO;
    decimal Total_Articles, Total_Articles2, Total_ArticlesHO;


    public int Delivery_branch_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_Delivery_branch_Id"]);
        }
        set
        {
            ViewState["_Delivery_branch_Id"] = value;
        }
    }
    public string DlyBranch
    {
        get
        {
            return Convert.ToString(ViewState["_DlyBranch"]);
        }
        set
        {
            ViewState["_DlyBranch"] = value;
            txtlblDlyBranch.Text = value;
        }
    }
    #endregion


    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingFreightDelivery";

        if (IsPostBack == false)
        {

            Delivery_branch_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Delivery_branch_Id"]);
            DlyBranch = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["DlyBranch"]);


            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);
            BindGridHO("form_and_pageload", e);
            BindGrid2("form_and_pageload", e);


        }


    }

    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);

    }

    protected void dg_Details2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details2.CurrentPageIndex = e.NewPageIndex;
        BindGrid2("form", e);

    }

    protected void dg_DetailsHO_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_detailsHO.CurrentPageIndex = e.NewPageIndex;
        BindGridHO("form", e);

    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label  lbl_SumTotal_Gc,lbl_SumTotal_Articles,lbl_SumTotalFreight;

            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotal_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Articles");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");

            lbl_SumTotal_Gc.Text = Total_Gc.ToString();
            lbl_SumTotal_Articles.Text = Total_Articles.ToString();
            lbl_SumTotalFreight.Text = Total_Freight.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int Consignee_Id;
            LinkButton lbtn_Consignee, lbtn_TotalFreight;
            bool Is_Consignee_Regular;
            

            Consignee_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignee_Client_Id").ToString());
            Is_Consignee_Regular = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignee_Regular_Client").ToString());
            lbtn_Consignee = (LinkButton)e.Item.FindControl("lbtn_Consignee");
            lbtn_TotalFreight = (LinkButton)e.Item.FindControl("lbtn_TotalFreight");

            lbtn_Consignee.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Id) + "','" + Is_Consignee_Regular + "')");

            StringBuilder PathDetails = new StringBuilder(Util.GetBaseURL());
            PathDetails.Append("/");
            PathDetails.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliveryDetails.aspx?&Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(txtlblDlyBranch.Text) + "&Consignee_Client_Id=" + Consignee_Id + "&Consignee_Name=" + lbtn_Consignee .Text + "&Is_Consignee_Regular=" + Is_Consignee_Regular + "&IsDetailed=True");

            lbtn_TotalFreight.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDetails + "')");

        }
    }

    protected void dg_DetailsHO_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumTotal_Gc, lbl_SumTotal_Articles, lbl_SumTotalFreight;

            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotal_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Articles");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");

            lbl_SumTotal_Gc.Text = Total_GcHO.ToString();
            lbl_SumTotal_Articles.Text = Total_ArticlesHO.ToString();
            lbl_SumTotalFreight.Text = Total_FreightHO.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int Consignee_Id;
            LinkButton lbtn_Consignee, lbtn_TotalFreight;
            bool Is_Consignee_Regular;


            Consignee_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignee_Client_Id").ToString());
            Is_Consignee_Regular = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignee_Regular_Client").ToString());
            lbtn_Consignee = (LinkButton)e.Item.FindControl("lbtn_Consignee");
            lbtn_TotalFreight = (LinkButton)e.Item.FindControl("lbtn_TotalFreight");

            lbtn_Consignee.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Id) + "','" + Is_Consignee_Regular + "')");

            StringBuilder PathDetails = new StringBuilder(Util.GetBaseURL());
            PathDetails.Append("/");
            PathDetails.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliveryDetails.aspx?&Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(txtlblDlyBranch.Text) + "&Consignee_Client_Id=" + Consignee_Id + "&Consignee_Name=" + lbtn_Consignee.Text + "&Is_Consignee_Regular=" + Is_Consignee_Regular + "&IsDetailed=True");

            lbtn_TotalFreight.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDetails + "')");

        }
    }

    protected void dg_Details2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumTotal_Gc, lbl_SumTotal_Articles, lbl_SumTotalFreight;

            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotal_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Articles");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");

            lbl_SumTotal_Gc.Text = Total_Gc2.ToString();
            lbl_SumTotal_Articles.Text = Total_Articles2.ToString();
            lbl_SumTotalFreight.Text = Total_Freight2.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int Consignee_Id;
            LinkButton lbtn_Consignee, lbtn_TotalFreight;
            bool Is_Consignee_Regular;
            

            Consignee_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignee_Client_Id").ToString());
            Is_Consignee_Regular = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignee_Regular_Client").ToString());
            lbtn_Consignee = (LinkButton)e.Item.FindControl("lbtn_Consignee");
            lbtn_TotalFreight = (LinkButton)e.Item.FindControl("lbtn_TotalFreight");

            lbtn_Consignee.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Id) + "','" + Is_Consignee_Regular + "')");

            StringBuilder PathDetails = new StringBuilder(Util.GetBaseURL());
            PathDetails.Append("/");
            PathDetails.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliveryDetails.aspx?&Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(txtlblDlyBranch.Text) + "&Consignee_Client_Id=" + Consignee_Id + "&Consignee_Name=" + lbtn_Consignee.Text + "&Is_Consignee_Regular=" + Is_Consignee_Regular + "&IsDetailed=True");

            lbtn_TotalFreight.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDetails + "')");

        }
    }
    #endregion

    #region Other Function


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Gc = Util.String2Decimal(dr["TotalLR"].ToString());
        Total_Articles = Util.String2Decimal(dr["TotalArticles"].ToString());
        Total_Freight = Util.String2Decimal(dr["TotalFreight"].ToString());

    }

    private void calculate_totalsHO()
    {

        DataRow dr = ds.Tables[3].Rows[0];
        Total_GcHO = Util.String2Decimal(dr["TotalLR"].ToString());
        Total_ArticlesHO = Util.String2Decimal(dr["TotalArticles"].ToString());
        Total_FreightHO = Util.String2Decimal(dr["TotalFreight"].ToString());

    }

    private void calculate_totals2()
    {

        DataRow dr = ds.Tables[5].Rows[0];
        Total_Gc2 = Util.String2Decimal(dr["TotalLR"].ToString());
        Total_Articles2 = Util.String2Decimal(dr["TotalArticles"].ToString());
        Total_Freight2 = Util.String2Decimal(dr["TotalFreight"].ToString());

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


        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Delivery_branch_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom)  
        };

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Freight_Consignee_Summary_New", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        calculate_totals();
        calculate_totalsHO();
        calculate_totals2();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void BindGridHO(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_detailsHO.CurrentPageIndex;
        int grid_PageSize = dg_detailsHO.PageSize;
        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }


        int Branch_id = UserManager.getUserParam().MainId; ;
        DateTime As_On_Date = DateTime.Now;


        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Delivery_branch_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom)  
        };

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Freight_Consignee_Summary_New", objSqlParam, ref ds);

        dg_detailsHO.VirtualItemCount = Util.String2Int(ds.Tables[3].Rows[0][0].ToString());

        calculate_totals();
        calculate_totalsHO();
        calculate_totals2();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_detailsHO, ds.Tables[2], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void BindGrid2(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Details2.CurrentPageIndex;
        int grid_PageSize = dg_Details2.PageSize;
        
        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }


        int Branch_id = UserManager.getUserParam().MainId; ;
        

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Delivery_branch_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom)  
        };

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Freight_Consignee_Summary_New", objSqlParam, ref ds);

        dg_Details2.VirtualItemCount = Util.String2Int(ds.Tables[5].Rows[0][0].ToString());

        calculate_totals();
        calculate_totalsHO();
        calculate_totals2();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details2, ds.Tables[4], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[6].NewRow();
        dr["Consignee_Name"] = "Total";
        dr["TotalLR"] = (Total_Gc + Total_Gc2);
        dr["TotalArticles"] = (Total_Articles + Total_Articles2);
        dr["TotalFreight"] = (Total_Freight + Total_Freight2);

        ds.Tables[6].Rows.Add(dr);

        DataRow dr1 = ds.Tables[6].NewRow();

        ds.Tables[6].Rows.Add(dr1);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[6];

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
