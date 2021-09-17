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


public partial class Reports_CL_Nandwana_UserDesk_FrmeWayBillChecking_Error_eWayBills : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int VehicleId;
    string  VehicleNo;
    DateTime MemoDate;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {

            VehicleId  = Util.String2Int(Request.QueryString["VehicleId"]);
            VehicleNo = Request.QueryString["VehicleNo"];
            MemoDate = Convert.ToDateTime(Request.QueryString["Date"]);


            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }
    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int GC_ID;
            LinkButton lbtn_GCNo;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());

            lbtn_GCNo = (LinkButton)e.Item.FindControl("lbtn_GCNo");

            lbtn_GCNo.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");


        }
    }


    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Date",SqlDbType.DateTime ,0,MemoDate),
        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,VehicleId)};

        objDAL.RunProc("COM_UserDesk_eWayBills_Checking_Error_eWayBills", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();


        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        ClearVariables();
    }


    public void ClearVariables()
    {
        ds = null;
    }
}
