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
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_GC_AllUndeliveredReason : System.Web.UI.Page
{

    #region Declaration
    private DataSet ds;


    public int GC_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_GC_Id"]);
        }
        set
        {
            ViewState["_GC_Id"] = value;
        }
    }

    public string GC_No
    {
        get
        {
            return Convert.ToString(ViewState["_GC_No"]);
        }
        set
        {
            ViewState["_GC_No"] = value;
            lbl_GCNo.Text = value;

        }
    }


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {

            GC_Id = Convert.ToInt32(Request.QueryString["GC_Id"]);
            GC_No = Request.QueryString["GC_No"];

            


            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }
    }


    #endregion

    #region Other Function


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0,GC_Id)};

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Stock_GC_AllUndeliveredReason", objSqlParam, ref ds);


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
    #endregion



}
