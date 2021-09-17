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



public partial class Reports_CL_Nandwana_UserDesk_FrmeWayBill_Pending_Vehicle_Update_PDS : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = DateTime.Now;
            Wuc_From_To_Datepicker1.SelectedToDate = DateTime.Now;

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }


    }

    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);

    }


    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Vehicle_Id, Unverified;
            string Vehicle_No, Current_PDS_ID;
            LinkButton lbtn_CreateJson, lbtn_Unverified;



            Vehicle_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Vehicle_Id").ToString());
            Vehicle_No = DataBinder.Eval(e.Item.DataItem, "Vehicle_No").ToString();

            Current_PDS_ID = DataBinder.Eval(e.Item.DataItem, "Current_PDS_ID").ToString();
            Unverified = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UnVerifiedeWayBills").ToString());

            lbtn_CreateJson = (LinkButton)e.Item.FindControl("lbtn_CreateJson");
            lbtn_Unverified = (LinkButton)e.Item.FindControl("lbtn_Unverified");

            int Branch_id = UserManager.getUserParam().MainId;

            if (Vehicle_Id <= 0)
            {
                lbtn_CreateJson.Visible = false;
            }

            if (Vehicle_Id > 0)
            {
                e.Item.BackColor = System.Drawing.Color.Cyan;
                e.Item.ForeColor = System.Drawing.Color.DarkBlue;
                e.Item.Font.Bold = true;
            }
            else
            {
                e.Item.BackColor = System.Drawing.Color.White;
            }

            if (Unverified == 0)
            {
                lbtn_Unverified.Visible = false;
            }

            lbtn_Unverified.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptString(Current_PDS_ID) + "')");


            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/FrmeWayBill_Pending_Vehicle_Update_Json_Creation_PDS.aspx?Vehicle_Id=" + ClassLibraryMVP.Util.EncryptInteger(Vehicle_Id)
                + "&Vehicle_No=" + ClassLibraryMVP.Util.EncryptString(Vehicle_No) + "&Current_PDS_ID=" + ClassLibraryMVP.Util.EncryptString(Current_PDS_ID));

            lbtn_CreateJson.Attributes.Add("onclick", "return viewwindow_Vehicle_Update_Json_Creation_PDS('" + PathDlyStk + "')");
        }
    }
    #endregion

    #region Other Function



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;

        int Branch_id = UserManager.getUserParam().MainId; ;


        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,0),
            objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,0),
            objDAL.MakeInParams("@From_Date",SqlDbType.DateTime ,0,From_Date),
            objDAL.MakeInParams("@To_Date",SqlDbType.DateTime ,0,To_Date )};

        objDAL.RunProc("EC_Opr_eWayBill_Pending_Vehicle_Update_PDS", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

    }


    protected void btn_view_Click(object sender, EventArgs e)
    {
        lbl_Error.Visible = false;
        lbl_Error.Text = "";
        BindGrid("", e);

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
