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

public partial class Reports_CL_Nandwana_User_Desk_FrmPendingDeliveryAddress : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private int Branch_ID, Area_ID, Region_ID, IsUpdated,IsBkg;
    private string DlyLocation;
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
        DlyLocation = Request.QueryString["DlyLocation"].ToString();

        if (DlyLocation == "0")
        {
            DlyLocation = UserManager.getUserParam().MainName;
        }

        lbl_DlyLocation.Text = DlyLocation;

        if (IsPostBack == false)
        {

            BindGrid("form_and_pageload", e);

        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingDeliveryAddress";

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

        int LoginBranch_ID;

        LoginBranch_ID = UserManager.getUserParam().MainId;

        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@ReportTypeID",SqlDbType.Int,0,ReportType),
            objDAL.MakeInParams("@LoginBranch_ID",SqlDbType.Int,0,LoginBranch_ID )};

        objDAL.RunProc("dbo.COM_UserDesk_Pending_Process_Details", objSqlParam, ref ds);

        if (IsUpdated == 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                dg_Grid.DataSource = ds.Tables[0];
                dg_Grid.DataBind();
            }
        }
        else
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                dg_Grid.DataSource = ds.Tables[1];
                dg_Grid.DataBind();
            }

        }

        if (IsUpdated == 1)
        {
            dg_Grid.Columns[5].Visible = false;
            dg_Grid.Columns[6].Visible = false;
            dg_Grid.Columns[7].Visible = true;
            dg_Grid.Columns[8].Visible = true;
            dg_Grid.Columns[9].Visible = true;
        }
        else
        {
            dg_Grid.Columns[7].Visible = false;
            dg_Grid.Columns[8].Visible = false;

        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }


    private void PrepareDTForExportToExcel()
    {

        if (IsUpdated == 0)
        {
            ds.Tables[0].Columns.Remove("GC_ID");
            ds.Tables[0].Columns.Remove("Consignee_Client_ID");
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
        }
        else
        {
            ds.Tables[1].Columns.Remove("GC_ID");
            ds.Tables[1].Columns.Remove("Consignee_Client_ID");
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[1];
        }
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

            int GC_ID, Consignee_Client_ID, Consignor_Client_ID;
            LinkButton lbtn_ConsigneeName, lbtn_GCNo,lbtn_ConsignorName;
            Label lbl_ConsigneeName,lbl_ConsignorName;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());

            lbtn_ConsigneeName = (LinkButton)e.Item.FindControl("lbtn_ConsigneeName");
            lbl_ConsigneeName = (Label)e.Item.FindControl("lbl_ConsigneeName");

            lbtn_ConsignorName = (LinkButton)e.Item.FindControl("lbtn_ConsignorName");
            lbl_ConsignorName = (Label)e.Item.FindControl("lbl_ConsignorName");

            lbtn_GCNo = (LinkButton)e.Item.FindControl("lbtn_GCNo");

            Consignee_Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignee_Client_Id").ToString());
            Consignor_Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignor_Client_Id").ToString());

            string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

            if (IsUpdated == 0)
            {
                lbtn_ConsigneeName.Attributes.Add("onclick", "return viewwindow_Client('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Client_ID) + "','" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");
            }
            else
            {
                lbtn_ConsigneeName.Attributes.Add("onclick", "return viewwindow_ClientConsignor('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Client_ID) + "')");
            }
            
            lbtn_ConsignorName.Attributes.Add("onclick", "return viewwindow_ClientConsignor('" + ClassLibraryMVP.Util.EncryptInteger(Consignor_Client_ID) + "')");

            lbtn_GCNo.Attributes.Add("onclick", "return viewwindow_GC('" + GC_ID + "')");

            if (IsUpdated == 1)
            {
                lbtn_ConsigneeName.Visible = true;
                lbl_ConsigneeName.Visible = false;

                lbl_ConsignorName.Visible = false ;
                lbl_ConsignorName.Visible = false; 

            }
            else
            {
                lbtn_ConsigneeName.Visible = true;
                lbl_ConsigneeName.Visible = false;

                lbtn_ConsignorName.Visible = true;
                lbl_ConsignorName.Visible = false;
            }


        }

    }

}



