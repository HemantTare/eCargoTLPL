using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Finance_Reports_FrmClosingCashBalDetailsNew : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal TotalAmount;

    public string DetailType
    {
        get { return ViewState["_DetailType"].ToString(); }
        set { ViewState["_DetailType"] = value; }
    }
    public string BranchID
    {
        get { return ViewState["_BranchID"].ToString(); }
        set { ViewState["_BranchID"] = value; }
    }
    public DateTime AsOnDate
    {
        get { return Convert.ToDateTime(ViewState["_AsOnDate"]); }
        set { ViewState["_AsOnDate"] = value; }
    }


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "ClosingCashBalanceDetails";

        if (IsPostBack == false)
        {
            DetailType = Request.QueryString["DetailType"];
            BranchID = Request.QueryString["BranchID"];
            AsOnDate = Convert.ToDateTime(Request.QueryString["AsOnDate"]);

            BindGrid("form_and_pageload", e);
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,BranchID), 
            objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,AsOnDate),
            objDAL.MakeInParams("@Type", SqlDbType.VarChar,100,DetailType)};

        objDAL.RunProc("FA_Opr_BranchWiseDailyClosingCashReport_New_Details", objSqlParam, ref ds);

        calculate_totals();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
        
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        TotalAmount = Util.String2Decimal(dr["Total"].ToString());

    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }



    #endregion

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

            string TransactionType;
            LinkButton lnk_TransactionNo;

            int TransactionID;


            TransactionType = DataBinder.Eval(e.Item.DataItem, "TransactionType").ToString();

            TransactionID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString());


            lnk_TransactionNo = (LinkButton)e.Item.FindControl("lnk_TransactionNo");

            if (TransactionType == "Booking")
            {
                lnk_TransactionNo.Attributes.Add("onclick", "return viewwindow_GC('" + ClassLibraryMVP.Util.EncryptInteger(TransactionID) + "')");
            }
            else if (TransactionType == "Voucher")
            {
                lnk_TransactionNo.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }

        }

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
                Label lbl_Total;

                lbl_Total = (Label)e.Item.FindControl("lbl_Total");

                lbl_Total.Text = TotalAmount.ToString();
        }


    }
}
