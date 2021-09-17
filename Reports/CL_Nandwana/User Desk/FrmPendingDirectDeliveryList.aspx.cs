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

public partial class Reports_CL_Nandwana_User_Desk_FrmPendingDirectDeliveryList : System.Web.UI.Page
{
#region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private Decimal LoadedWeight,LoadedWtForUs;
    private int Branch_ID, Area_ID, Region_ID;
    decimal Count, Freight,Parcels;
    string PayingParty;
#endregion

#region ControlsValue


#endregion
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Branch_ID = Convert.ToInt32(Request.QueryString["Branch_ID"].ToString());
        Area_ID = Convert.ToInt32(Request.QueryString["Area_ID"].ToString());
        Region_ID = Convert.ToInt32(Request.QueryString["Region_ID"].ToString());

        string Crypt = "";

        Crypt = System.Web.HttpContext.Current.Request.QueryString["PayingParty"];

        if (Crypt == null)
        {
            PayingParty = "";
        }
        else
        {
            PayingParty = Util.DecryptToString(Crypt).Replace("`", "&");
        }

        if (IsPostBack == false)
        {
            BindGrid("form_and_pageload", e);

            btn_Summary.Attributes.Add("onclick", "return Summary(" + Util.Int2String(Branch_ID) + "," + Util.Int2String(Area_ID) + "," + Util.Int2String(Region_ID) + ")"); 

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
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@PayingParty",SqlDbType.VarChar ,100,PayingParty)};

         objDAL.RunProc("dbo.COM_UserDesk_Pending_Direct_Delivery", objSqlParam, ref ds);

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
        ds.Tables[0].Columns.Remove("GC_ID");
        ds.Tables[0].Columns.Remove("Vehicle_ID");
        
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
            string GCNo;
            int GCId,VehicleId;
            LinkButton lbtn_GCNo;

            GCId = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_Id").ToString());
            GCNo = DataBinder.Eval(e.Item.DataItem, "GCNo").ToString();
            VehicleId = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Vehicle_Id").ToString());
            lbtn_GCNo = (LinkButton)e.Item.FindControl("lbtn_GCNo");

            string _hierarchyCode = UserManager.getUserParam().HierarchyCode;


            if (_hierarchyCode == "BO")
            {
                lbtn_GCNo.Attributes.Add("onclick", "return viewwindow_DirectDelivery('" + ClassLibraryMVP.Util.EncryptInteger(GCId) + "','" + ClassLibraryMVP.Util.EncryptString(GCNo) + "','" + ClassLibraryMVP.Util.EncryptInteger(VehicleId) + "')");
            }
            else
            {
                lbtn_GCNo.Attributes.Add("onclick", "return viewwindow_GCView('" + GCId + "')");
            }
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



