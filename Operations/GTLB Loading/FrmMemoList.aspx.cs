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


public partial class Operations_GTLB_Loading_FrmMemoList : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int Vehicle_Id;
    DateTime LoadingDate, ToBeIncludeDate;

    #endregion




    protected void Page_Load(object sender, EventArgs e)
    {


        if (IsPostBack == false)
        {

            Vehicle_Id = Convert.ToInt32(Request.QueryString["Vehicle_Id"].ToString());
            LoadingDate = Convert.ToDateTime(Request.QueryString["LoadingDate"].ToString());
            lbl_VehicleNo.Text = Request.QueryString["VehicleNo"].ToString();

            ToBeIncludeDate = Convert.ToDateTime(Request.QueryString["ToBeIncludeDate"].ToString());


            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }

    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {


        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {



        }

    }


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);


        SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@Vehicle_Id",SqlDbType.Int,0,Vehicle_Id),
            objDAL.MakeInParams("@LoadingDate",SqlDbType.DateTime ,0,LoadingDate),
         objDAL.MakeInParams("@ToBeIncludeDate",SqlDbType.DateTime ,0,ToBeIncludeDate)};

        objDAL.RunProc("EC_Opr_GTLB_LoadingDetails_Memo_Details", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();


        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        ClearVariables();
    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {

        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }


    public void ClearVariables()
    {
        ds = null;
    }

}
