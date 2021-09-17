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

public partial class Reports_CL_Nandwana_User_Desk_FrmPendingDirectDeliverySummary : System.Web.UI.Page
{
#region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private int Branch_ID, Area_ID, Region_ID;
    decimal Count, Freight,Parcels;
#endregion

#region ControlsValue


#endregion
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Branch_ID = Convert.ToInt32(Request.QueryString["Branch_ID"].ToString());
        Area_ID = Convert.ToInt32(Request.QueryString["Area_ID"].ToString());
        Region_ID = Convert.ToInt32(Request.QueryString["Region_ID"].ToString());

        if (IsPostBack == false)
        {
            BindGrid("form_and_pageload", e);
        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingDirectDelivery";
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

         SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID)};

         objDAL.RunProc("dbo.COM_UserDesk_Pending_Direct_Delivery_Summary", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dg_Grid.DataSource = ds.Tables[0];
                dg_Grid.DataBind(); 
            }

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            }
    }

    private void PrepareDTForExportToExcel()
    {
        ds.Tables[0].Columns.Remove("Client_ID");
        ds.Tables[0].Columns.Remove("Is_Regular_Client");
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
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        calculate_totals();

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Count, lbl_Freight, lbl_Parcels;

            lbl_Count = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Count");
            lbl_Parcels = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Parcels");
            lbl_Freight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight");

            lbl_Count.Text = Count.ToString();
            lbl_Parcels.Text = Parcels.ToString();
            lbl_Freight.Text = Freight.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            LinkButton lbtn_PayingParty, lbtn_TotalFreight;

            lbtn_PayingParty = (LinkButton)e.Item.FindControl("lbtn_PayingParty");
            lbtn_TotalFreight = (LinkButton)e.Item.FindControl("lbtn_TotalFreight");

            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());

            string PayingParty;

            PayingParty =  lbtn_PayingParty.Text.Replace("&", "`");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDirectDeliveryList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&PayingParty=" + Util.EncryptString(PayingParty));
            lbtn_TotalFreight.Attributes.Add("onclick", "return PendingDirectDelivery('" + PathF4 + "')");

            int Client_ID;
            bool Is_Regular_Client;

            Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());
            Is_Regular_Client = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Regular_Client").ToString());

            lbtn_PayingParty.Attributes.Add("onclick", "return viewwindow_Client('" + ClassLibraryMVP.Util.EncryptInteger(Client_ID) + "','" + Is_Regular_Client + "')");
        }


    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Count = Util.String2Decimal(dr["GCCount"].ToString());
        Parcels = Util.String2Decimal(dr["TotalParcels"].ToString());
        Freight = Util.String2Decimal(dr["TotalFreight"].ToString());
    }
}



