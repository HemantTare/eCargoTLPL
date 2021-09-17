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



public partial class Reports_CL_Nandwana_UserDesk_FrmeWayBill_PartB_Updated_Vehicles_PDS : System.Web.UI.Page
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
            int Vehicle_Id, Unverified, PDS_Id;

            LinkButton lbtn_Unverified;

            PDS_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "PDS_Id").ToString());
            Unverified = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UnVerifiedeWayBills").ToString());

            Vehicle_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Vehicle_Id").ToString());

            lbtn_Unverified = (LinkButton)e.Item.FindControl("lbtn_Unverified");


            if (Vehicle_Id > 0)
            {
                e.Item.BackColor = System.Drawing.Color.LemonChiffon;
                e.Item.ForeColor = System.Drawing.Color.Maroon;
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

            lbtn_Unverified.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(PDS_Id) + "')");

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

        string HierarchyCode = UserManager.getUserParam().HierarchyCode;
        int MainId = UserManager.getUserParam().MainId;
        int Branch_ID, Area_ID, Region_ID;


        Branch_ID = 0;
        Area_ID = 0;
        Region_ID = 0;


        if (HierarchyCode == "AD" || HierarchyCode == "HO")
        {
            Branch_ID = 0;
            Area_ID = 0;
            Region_ID = 0;
        }
        else if (HierarchyCode == "RO")
        {
            Branch_ID = 0;
            Area_ID = 0;
            Region_ID = MainId;
        }
        else if (HierarchyCode == "AO")
        {
            Branch_ID = 0;
            Area_ID = MainId;
            Region_ID = 0;
        }
        else if (HierarchyCode == "BO")
        {
            Branch_ID = MainId;
            Area_ID = 0;
            Region_ID = 0;
        }

        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@From_Date",SqlDbType.DateTime ,0,From_Date),
            objDAL.MakeInParams("@To_Date",SqlDbType.DateTime ,0,To_Date)
        };

        objDAL.RunProc("EC_Opr_eWayBill_PartB_Updated_Vehicles_PDS", objSqlParam, ref ds);

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
