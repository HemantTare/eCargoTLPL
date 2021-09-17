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


public partial class Reports_CL_Nandwana_UserDesk_FrmDueForBillingDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal  NoOfLR,NoOfParcels,TotalFreight,SubTotal,GST,RoundOff; 
    private int Client_Id;

    #endregion

    

    protected void Page_Load(object sender, EventArgs e)
    {

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = lbl_Client.Text;

        Client_Id = Convert.ToInt32(Request.QueryString["Client_Id"].ToString());
        lbl_Client.Text = Request.QueryString["Client_Name"].ToString();
        if (IsPostBack == false)
        {

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Details);
            BindGrid("form_and_pageload", e);

        }

        btn_CreateBill.Attributes.Add("onclick", "return viewwindow_BillCreation('" + ClassLibraryMVP.Util.EncryptInteger(Client_Id) + "','" + ClassLibraryMVP.Util.EncryptString(lbl_Client.Text) + "','" + hdn_SpecialBillFormat.Value + "')");

        if (UserManager.getUserParam().HierarchyCode != "BO")
        {
            btn_CreateBill.Visible = false;
        }
    }

    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_NoOfLR, lbl_TotalFreight, lbl_NoOfParcels, lbl_SubTotal, lbl_GST;

            lbl_NoOfLR = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOfLR");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_NoOfParcels = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOfParcels");
            lbl_SubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubTotal");
            lbl_GST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_GST");


            lbl_NoOfLR.Text = NoOfLR.ToString();
            lbl_TotalFreight.Text = TotalFreight.ToString();
            lbl_NoOfParcels.Text  = NoOfParcels.ToString();
            //lbl_SubTotal.Text = SubTotal.ToString();
            //lbl_GST.Text = GST.ToString();

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int GC_Id;
            LinkButton lbtn_GC_No;


            GC_Id  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_Id").ToString());

            lbtn_GC_No = (LinkButton)e.Item.FindControl("lbtn_GC_No");


            lbtn_GC_No.Attributes.Add("onclick", "return viewwindow_GCView('" + GC_Id + "')");

            hdn_SpecialBillFormat.Value = DataBinder.Eval(e.Item.DataItem, "SpecialBillFormat").ToString();
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Details.CurrentPageIndex;
        int grid_PageSize = dg_Details.PageSize;


        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Client_Id", SqlDbType.Int, 0, Client_Id) };
        objDAL.RunProc("COM_UserDesk_Get_Due_For_Billing_Details", objSqlParam,ref ds);

        calculate_totals();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }


        ClearVariables();
    }

    private void PrepareDTForExportToExcel()
    {
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Copy();

        dt.Columns.Remove("GC_Id");
        dt.Columns.Remove("SpecialBillFormat");
        dt.Columns[0].ColumnName = "LRDate";
        dt.Columns[1].ColumnName = "LRNo";
        dt.Columns[10].ColumnName = "TotalFreight";


        Wuc_Export_To_Excel1.has_last_row_as_total = false;
        Wuc_Export_To_Excel1.SessionExporttoExcel = dt;
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        NoOfLR = Util.String2Decimal(dr["NoOfLR"].ToString());
        NoOfParcels = Util.String2Decimal(dr["NoOfParcels"].ToString());
        TotalFreight = Util.String2Decimal(dr["TotalFreight"].ToString());
        SubTotal = Util.String2Decimal(dr["SubTotal"].ToString());
        GST = Util.String2Decimal(dr["TotalGST"].ToString());
        RoundOff = Util.String2Decimal(dr["RoundOff"].ToString());
    }

    public void ClearVariables()
    {
        ds = null;
    }
}
