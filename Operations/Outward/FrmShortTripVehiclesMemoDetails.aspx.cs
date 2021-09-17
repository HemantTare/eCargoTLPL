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


public partial class Operations_Outward_FrmShortTripVehiclesMemoDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    private int VehicleID;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        VehicleID = Convert.ToInt32(Request.QueryString["Vehicle_ID"].ToString());
        lblVehicleNo.Text = Request.QueryString["Vehicle_No"].ToString();

        if (IsPostBack == false)
        {

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }
    }

    #endregion

    #region Other Function

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();

        string CallFrom = (string)(sender);


        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, VehicleID) };

        objDAL.RunProc("EC_Opr_Short_Trip_Vehicles_Memo_Details", objSqlParam, ref ds);

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

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



    protected void dg_Details_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            string TripNo = ds.Tables[0].Rows[e.Item.ItemIndex]["TripNo"].ToString();

            if (TripNo.Trim() != "")
            {
                e.Item.BackColor = System.Drawing.Color.Cyan;
            }

            if ((e.Item.Cells[0].Text.Trim()) != "")
            {
                e.Item.Font.Bold = true;
            }
        }
    }



    #endregion


}
