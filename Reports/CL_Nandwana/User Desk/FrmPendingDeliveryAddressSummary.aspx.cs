using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;

public partial class Reports_CL_Nandwana_User_Desk_FrmPendingDeliveryAddressSummary : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private int Branch_ID, Area_ID, Region_ID, IsUpdated,IsBkg;
    #endregion

    #region ControlsValue
    public string Is_FromDesktop
    {
        get
        {
            //if (Request.QueryString["IsFromDesktop"] == null)
            //    return "N";
            //else
            //{
            return (Request.QueryString["IsFromDesktop"]);
            //}
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        Branch_ID = Convert.ToInt32(Request.QueryString["Branch_ID"].ToString());
        Area_ID = Convert.ToInt32(Request.QueryString["Area_ID"].ToString());
        Region_ID = Convert.ToInt32(Request.QueryString["Region_ID"].ToString());
        IsUpdated = Convert.ToInt32(Request.QueryString["IsUpdated"].ToString());
        IsBkg = Convert.ToInt32(Request.QueryString["IsBkg"].ToString());

        if (IsPostBack == false)
        {

            BindGrid("form_and_pageload", e);

        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingDeliveryAddressMain";

        Wuc_Export_To_Excel2.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel2.FileName = "PendingDeliveryAddressOthers";


    }


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int ReportType;

        if (IsBkg == 1)
        {
            ReportType = 5;
        }
        else
        {
            ReportType = 6;
        }

        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@ReportTypeID",SqlDbType.Int,0,ReportType)};

        objDAL.RunProc("dbo.COM_UserDesk_Pending_Process_Details", objSqlParam, ref ds);

        if (ds.Tables[2].Rows.Count > 0)
        {
            dg_Grid.DataSource = ds.Tables[2];
            dg_Grid.DataBind();

            if (IsUpdated == 1)
            {
                dg_Grid.Columns[1].Visible = false;
            }
            else
            {
                dg_Grid.Columns[2].Visible = false;
            }
        }


        if (ds.Tables[3].Rows.Count > 0)
        {
            dg_Grid2.DataSource = ds.Tables[3];
            dg_Grid2.DataBind();

            if (IsUpdated == 1)
            {
                dg_Grid2.Columns[1].Visible = false;
            }
            else
            {
                dg_Grid2.Columns[2].Visible = false;
            }
        }


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
            PrepareDTForExportToExcelOther();
        }

    }


    private void PrepareDTForExportToExcel()
    {

        ds.Tables[2].Columns.Remove("Branch_ID");
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[2];

    }

    private void PrepareDTForExportToExcelOther()
    {

        ds.Tables[3].Columns.Remove("Branch_ID");
        Wuc_Export_To_Excel2.SessionExporttoExcel = ds.Tables[3];

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
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int Branch_ID;
            LinkButton lbtn_NotUpdatedMain, lbtn_UpdatedMain;
            string DlyLocation;

            Branch_ID  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Branch_Id").ToString());
            DlyLocation = DataBinder.Eval(e.Item.DataItem, "Branch_Name").ToString();

            lbtn_NotUpdatedMain = (LinkButton)e.Item.FindControl("lbtn_NotUpdatedMain");
            lbtn_UpdatedMain = (LinkButton)e.Item.FindControl("lbtn_UpdatedMain");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=0&Region_ID=0&IsUpdated=0&IsBkg=" + IsBkg + "&DlyLocation=" + DlyLocation);
            lbtn_NotUpdatedMain.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=0&Region_ID=0&IsUpdated=1&IsBkg=" + IsBkg + "&DlyLocation=" + DlyLocation);
            lbtn_UpdatedMain.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");

            if (Convert.ToInt32(lbtn_NotUpdatedMain.Text) <= 0)
            {
                lbtn_NotUpdatedMain.Visible = false; 
            }

            if (Convert.ToInt32(lbtn_UpdatedMain.Text) <= 0)
            {
                lbtn_UpdatedMain.Visible = false;
            }

        }

    }

    protected void dg_Grid2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF5 = new StringBuilder(Util.GetBaseURL());
            int Branch_ID;
            LinkButton lbtn_NotUpdatedOther, lbtn_UpdatedOther;
            string DlyLocation;

            DlyLocation = DataBinder.Eval(e.Item.DataItem, "Branch_Name").ToString();

            Branch_ID  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Branch_Id").ToString());

            lbtn_NotUpdatedOther = (LinkButton)e.Item.FindControl("lbtn_NotUpdatedOther");
            lbtn_UpdatedOther = (LinkButton)e.Item.FindControl("lbtn_UpdatedOther");

            PathF5 = new StringBuilder(Util.GetBaseURL());
            PathF5.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=0&Region_ID=0&IsUpdated=0&IsBkg=" + IsBkg + "&DlyLocation=" + DlyLocation);
            lbtn_NotUpdatedOther.Attributes.Add("onclick", "return OpenF4Menu('" + PathF5 + "')");

            PathF5 = new StringBuilder(Util.GetBaseURL());
            PathF5.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=0&Region_ID=0&IsUpdated=1&IsBkg=" + IsBkg + "&DlyLocation=" + DlyLocation);
            lbtn_UpdatedOther.Attributes.Add("onclick", "return OpenF4Menu('" + PathF5 + "')");

            if (Convert.ToInt32(lbtn_NotUpdatedOther.Text) <= 0)
            {
                lbtn_NotUpdatedOther.Visible = false;
            }

            if (Convert.ToInt32(lbtn_UpdatedOther.Text) <= 0)
            {
                lbtn_UpdatedOther.Visible = false;
            }


        }

    }

}



