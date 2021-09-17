using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_CL_Nandwana_User_Desk_FrmUserDeskCreditDebitCustomerBalance : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    string TotClosingBal;

    #endregion
  
    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "CreditDebitCustomerBalance";
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
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

        bool IsConsolidated;
        int MainLedgerGroupId;


        if (UserManager.getUserParam().HierarchyCode == "AD" || UserManager.getUserParam().HierarchyCode == "HO")
        {
            IsConsolidated = true;
        }
        else
        {
            IsConsolidated = false;
        }

        if (rdl_CreditDebitCustomer.SelectedValue == "0")
        {
            MainLedgerGroupId = 366;
        }
        else if (rdl_CreditDebitCustomer.SelectedValue == "1")
        {
            MainLedgerGroupId = 365;
        }
        else         
        {
            MainLedgerGroupId = 24;
        }

        if (rdl_CreditDebitCustomer.SelectedValue == "1")
        {
            dg_Grid.Columns[4].HeaderText = "Minimum Balance";
        }
        else
        {
            dg_Grid.Columns[4].HeaderText = "Credit Limmit";
        }

        DateTime StartDate, EndDate;

        if (DateTime.Now > UserManager.getUserParam().EndDate)
        {
            StartDate = UserManager.getUserParam().EndDate;
            EndDate = UserManager.getUserParam().EndDate;
        }
        else
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@IsConsolidated",SqlDbType.Bit ,0,IsConsolidated),
            objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,3,UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@MainId",SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@StartDate",SqlDbType.DateTime,0,StartDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,EndDate),
            objDAL.MakeInParams("@MainLedgerGroupId",SqlDbType.Int,0,MainLedgerGroupId),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)};

        objDAL.RunProc("dbo.FA_Opr_CreditDebit_Customer_Balance", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void PrepareDTForExportToExcel()
    {
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Copy();

        dt.Columns.Remove("Category");
        dt.Columns.Remove("LedgerId");
        dt.Columns.Remove("Client_ID");
        dt.Columns.Remove("OpeningDr");
        dt.Columns.Remove("OpeningCr");
        dt.Columns.Remove("CurrentDr");
        dt.Columns.Remove("CurrentCr");
        dt.Columns.Remove("ClosingDr");
        dt.Columns.Remove("ClosingCr");

        Wuc_Export_To_Excel1.has_last_row_as_total = false;
        Wuc_Export_To_Excel1.SessionExporttoExcel = dt;
    }

 
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_TotClosingBal;

            lbl_TotClosingBal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotClosingBal");

            lbl_TotClosingBal.Text = TotClosingBal.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            LinkButton lnk_ClosingBalance, lnk_ClientName, lnk_LastBilled;

            lnk_ClosingBalance = (LinkButton)e.Item.FindControl("lnk_ClosingBalance");
            lnk_ClientName = (LinkButton)e.Item.FindControl("lnk_ClientName");
            lnk_LastBilled = (LinkButton)e.Item.FindControl("lnk_LastBilled");

            string Hierarchy_Code, Ledger_Name;
            int Main_Id, Ledger_Id, Menu_Item_Id,Mode;
            bool Is_Consol;

            Menu_Item_Id = 79;
            Mode = 4;

            Hierarchy_Code = UserManager.getUserParam().HierarchyCode;
            Main_Id = UserManager.getUserParam().MainId;

            if (UserManager.getUserParam().HierarchyCode == "AD" || UserManager.getUserParam().HierarchyCode == "HO")
            {
                Is_Consol = true;
            }
            else
            {
                Is_Consol = false;
            }

            Ledger_Id  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "LedgerId").ToString());
            Ledger_Name = DataBinder.Eval(e.Item.DataItem, "LedgerName").ToString();

            StringBuilder PathLedgerMonthly = new StringBuilder(Util.GetBaseURL());
            PathLedgerMonthly.Append("/");

            PathLedgerMonthly.Append("Finance/Reports/FrmLedgerMonthly.aspx?Id=" + Util.Int2String(Ledger_Id) + "&Name=" + Ledger_Name + "&Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) + "&Mode=" + Util.EncryptInteger(Mode)  +"&Is_Consol=" + Convert.ToString(Is_Consol) + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Convert.ToString(Main_Id));
            lnk_ClosingBalance.Attributes.Add("onclick", "return LedgerMonthly('" + PathLedgerMonthly + "')");

            int ClientId;

            ClientId = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());

            if (ClientId > 0)
            {
                lnk_ClientName.Attributes.Add("onclick", "return viewwindow_ClientRegular('" + ClassLibraryMVP.Util.EncryptInteger(ClientId) + "')");
            }

            if (ClientId > 0 && Ledger_Id > 0)
            {
                lnk_LastBilled.Attributes.Add("onclick", "return viewwindow_BillingDetails('" + ClassLibraryMVP.Util.EncryptInteger(ClientId) + "','" + ClassLibraryMVP.Util.EncryptInteger(Ledger_Id) + "','" + lnk_ClientName.Text  + "')");
            }
        }
    }


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotClosingBal = dr["TotClosingBal"].ToString();
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

}
