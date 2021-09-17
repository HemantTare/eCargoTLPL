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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;

public partial class Reports_CL_Nandwana_User_Desk_frm_PrevDayPendingeWayBillVerification : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private int Branch_ID, Area_ID, Region_ID;  
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

         objDAL.RunProc("dbo.COM_UserDesk_Get_Pending_PreviousDay_Unverified_eWayBills", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dg_Grid.DataSource = ds.Tables[0];
                dg_Grid.DataBind(); 
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

            int GC_ID, Consignor_Id;
            LinkButton lbtn_LRNo, lbtn_ConsignorName;
            bool Is_Consignor_Regular;

            GC_ID   = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());

            Consignor_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignor_Client_Id").ToString());
            Is_Consignor_Regular = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignor_Regular_Client").ToString());

            lbtn_LRNo = (LinkButton)e.Item.FindControl("lbtn_LRNo");
            lbtn_ConsignorName = (LinkButton)e.Item.FindControl("lbtn_ConsignorName");

            lbtn_ConsignorName.Attributes.Add("onclick", "return viewwindow_ClientConsignor('" + ClassLibraryMVP.Util.EncryptInteger(Consignor_Id) + "','" + Is_Consignor_Regular + "')");

            StringBuilder PathGCID = new StringBuilder(Util.GetBaseURL());
            PathGCID.Append("/");
            PathGCID.Append("Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=" + ClassLibraryMVP.Util.EncryptInteger(GC_ID));
            lbtn_LRNo.Attributes.Add("onclick", "return viewwindow_general('" + PathGCID + "')");


        }

       
    }
}



