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


public partial class Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_Summary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal TotalParcls, TotalNoOfLR, TotalFreight;


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

    public int IsOld
    {
        get
        {
            return Convert.ToInt32(ViewState["_IsOld"]);
        }
        set
        {
            ViewState["_IsOld"] = value;
        }
    }

    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DeliveryStockList_Dly_Area_Summary";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Delivery_branch_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Delivery_branch_Id"]);
            DlyBranch = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["DlyBranch"]);

            IsOld = Convert.ToInt32(Request.QueryString["IsOld"]);

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);


            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Details.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DeliveryArea_Id=" + ClassLibraryMVP.Util.EncryptInteger(0)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(DlyBranch) + "&IsOld=" + IsOld + "&IsDetailed=True" + "&AllLR=1");

            btn_AllLR_DateWise.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathF4 + "')");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Details.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DeliveryArea_Id=" + ClassLibraryMVP.Util.EncryptInteger(0)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(DlyBranch) + "&IsOld=" + IsOld + "&IsDetailed=True" + "&AllLR=2");

            btn_AllLR_LRNoWise.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathF4 + "')");


            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Details.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DeliveryArea_Id=" + ClassLibraryMVP.Util.EncryptInteger(0)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(DlyBranch) + "&IsOld=" + IsOld + "&IsDetailed=True" + "&AllLR=3");

            btn_AllLR_ReasoneWise.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathF4 + "')");

            if (IsOld == 0)
            {
                btn_AllLR_ReasoneWise.Visible = false;
            }
        }

        if (IsOld == 3)
        {
            tr_BranchName.BgColor = "Navy";
            txtlblDlyBranch.ForeColor = System.Drawing.Color.Yellow;
        }
        else if (IsOld == 2)
        {
            tr_BranchName.BgColor = "Red";
            txtlblDlyBranch.ForeColor = System.Drawing.Color.Yellow;
        }
        else if (IsOld == 1 || IsOld == 8 || IsOld == 9 || IsOld == 10 || IsOld == 11)
        {
            tr_BranchName.BgColor = "Yellow";
        }
        else if (IsOld == 0 || IsOld == 4 || IsOld == 5 || IsOld == 6 || IsOld == 7 )
        {
            tr_BranchName.BgColor = "Lime";
        }

    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();

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
            Label lbl_TotalNoOfLR, lbl_TotalParcls, lbl_TotalFreight;

            lbl_TotalNoOfLR = (Label)e.Item.FindControl("lbl_TotalNoOfLR");
            lbl_TotalParcls = (Label)e.Item.FindControl("lbl_TotalParcls");
            lbl_TotalFreight = (Label)e.Item.FindControl("lbl_TotalFreight");

            lbl_TotalNoOfLR.Text = TotalNoOfLR.ToString();
            lbl_TotalParcls.Text = TotalParcls.ToString();
            lbl_TotalFreight.Text = TotalFreight.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int DlyAreaID;
            LinkButton lnk_DlyArea;

            DlyAreaID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DeliveryAreaID").ToString());

            lnk_DlyArea = (LinkButton)e.Item.FindControl("lnk_DlyArea");


            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Details.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DeliveryArea_Id=" + ClassLibraryMVP.Util.EncryptInteger(DlyAreaID) 
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(DlyBranch) + "&IsOld=" + IsOld  + "&IsDetailed=True" + "&AllLR=0");

            lnk_DlyArea.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDlyStk + "')");

        }
    }

    #endregion

    #region Other Function


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotalNoOfLR = Util.String2Decimal(dr["Total_GC"].ToString());
        TotalParcls = Util.String2Decimal(dr["Total_Articles"].ToString());
        TotalFreight = Util.String2Decimal(dr["Total_Freight"].ToString());
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


        int Branch_id = UserManager.getUserParam().MainId; ;
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Delivery_branch_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),  
            objDAL.MakeInParams("@IsOld",SqlDbType.Int,0,IsOld),
        };

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Stock_DlyArea_Summary", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

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
        dr["Dly Area"] = "Total";
        dr["NoOfLR"] = TotalNoOfLR;
        dr["Parcls"] = TotalParcls;
        dr["TotalFreight"] = TotalFreight;

        ds.Tables[0].Rows.Add(dr);



        ds.Tables[0].Columns.Remove("SrNo");
        ds.Tables[0].Columns.Remove("DeliveryAreaID");
        ds.Tables[0].Columns.Remove("Delivery_branch_Id");
        ds.Tables[0].Columns.Remove("Dly Branch");
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
