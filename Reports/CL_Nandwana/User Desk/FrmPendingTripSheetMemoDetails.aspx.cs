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


public partial class Reports_CL_Nandwana_UserDesk_FrmPendingTripSheetMemoDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int DVLPID;

    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {

            DVLPID = Util.String2Int(Request.QueryString["DVLPID"]);


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

        }
    }


    protected void dg_Details2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
        }
    }


    #endregion

    #region Other Function



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@DVLPID", SqlDbType.Int, 0,DVLPID)};

        objDAL.RunProc("COM_UserDesk_Get_Pending_Trip_Sheet_Details_Memo_Details", objSqlParam, ref ds);


        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

        objcommon.ValidateReportForm(dg_Details2, ds.Tables[1], CallFrom, lbl_Error);


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
