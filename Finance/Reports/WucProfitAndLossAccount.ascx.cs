using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using System.Data.SqlClient;
using Raj.EC;
using Raj.EC.ControlsView;
using System.Text;

public partial class Finance_Reports_WucProfitAndLossAccount : System.Web.UI.UserControl,IProfitAndLossAccountView
{
    #region ClassVariables
    ProfitAndLossAccountPresenter objProfitAndLossAccountPresenter;
    private Boolean IsConsol;
    private String HierarchyCode;
    private int MainId;
    public int Menu_Item_Id;
    public string _CompanyName = UserManager.getUserParam().CompanyName;
    #endregion


    #region ControlsValue

    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }

    public int Mode
    {
        get { return Common.GetMode(); }
    }
    public int Category
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["Category"]);


        }
    }

    public string CompanyName
    {
        set { lbl_Company_Name.Text = value; }
    }

    public DateTime StartDate
    {
        get { return WucStartEndDate1.Start_Date; }
        set { WucStartEndDate1.Start_Date = value; }
    }

    public DateTime EndDate
    {
        get { return WucStartEndDate1.End_Date; }
        set { WucStartEndDate1.End_Date = value; }
    }

    public Boolean Is_Consol
    {
        get { return IsConsol; }
        set { IsConsol = value; }
    }

    public String Hierarchy_Code
    {
        get { return HierarchyCode; }
        set { HierarchyCode = value; }
    }

    public int Main_Id
    {
        get { return MainId; }
        set { MainId = value; }
    }
    public DataSet PnL_DS
    {
        get { return StateManager.GetState<DataSet>("PnL_DS"); }
        set { StateManager.SaveState("PnL_DS", value); }

    }
    #endregion

    #region ControlsBind
    public DataTable BindExpensesGrid
    {
        set
        {
            dg_Expenses.DataSource = value;
            dg_Expenses.DataBind();
        }
    }

    public DataTable BindIncomeGrid
    {
        set
        {
            dg_Income.DataSource = value;
            dg_Income.DataBind();
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        return true;
    }

    public int keyID
    {
        get { return Convert.ToInt32(Request.QueryString["Id"]); }
    }
    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    #endregion

      #region PageEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        Menu_Item_Id = Common.GetMenuItemId();
        lbl_Company_Name.Text = _CompanyName;
        Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);

        WucStartEndDate1.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            StartDate = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["StartDate"]));
            EndDate = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["EndDate"]));
        }

        //if (Session["StartDate"] == null)
        //{

        //    StartDate = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["StartDate"]));
        //}
        //else
        //{
        //    StartDate = StateManager.GetState<DateTime>("StartDate");
        //}

        //if (Session["EndDate"] == null)
        //{

        //    EndDate = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["EndDate"]));
        //}
        //else
        //{
        //    EndDate = StateManager.GetState<DateTime>("EndDate");
        //}

        
        objProfitAndLossAccountPresenter = new ProfitAndLossAccountPresenter(this, IsPostBack);
        Session["PnL_DS"] = PnL_DS;
        lbl_Assets_Total.Text = PnL_DS.Tables[3].Rows[0]["Total"].ToString();
        lbl_Liability_Total.Text = lbl_Assets_Total.Text;

        // PRINT PREVIEW
        Session.Add("FIN_DS", PnL_DS);
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Reports/Direct_Printing/Frm_FinancePrintingViewer.aspx?Menu_Item_Id=" + Menu_Item_Id + "&Start_Date=" + Util.EncryptString(StartDate.ToString()) + "&End_Date=" + Util.EncryptString(EndDate.ToString()));
        cmdprintPreview.Attributes.Add("onclick", "return Open_Show_Window('" + Path + "')");



    }
      #endregion

    #region GridEvents
    protected void DG_ItemDataBound_Expenses(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");
            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");
            }


            if (Convert.ToInt32(PnL_DS.Tables[1].Rows[e.Item.ItemIndex]["Ledger_Group_Id"]) != 0)
            {
                e.Item.Font.Bold = true;
            }
            else if (Convert.ToInt32(PnL_DS.Tables[1].Rows[e.Item.ItemIndex]["Ledger_Group_Id"]) == 0)
            {
                e.Item.Font.Italic = true;               
                e.Item.Cells[0].Text = "&nbsp;&nbsp;" + PnL_DS.Tables[1].Rows[e.Item.ItemIndex]["Ledger_Group_Name"].ToString();
            }

            if (Convert.ToInt32(PnL_DS.Tables[1].Rows[e.Item.ItemIndex]["Ledger_Group_Id"]) == -1)
            {
                e.Item.Font.Italic = false;
                e.Item.Font.Bold = true;
                e.Item.Cells[1].Font.Overline = true;
                e.Item.Cells[1].Font.Underline = true;
            }

        }
    }

    protected void DG_ItemDataBound_Income(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");
            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");
            }

            if (Convert.ToInt32(PnL_DS.Tables[2].Rows[e.Item.ItemIndex]["Ledger_Group_Id"]) != 0)
            {
                e.Item.Font.Bold = true;
            }
            else if (Convert.ToInt32(PnL_DS.Tables[2].Rows[e.Item.ItemIndex]["Ledger_Group_Id"]) == 0)
            {
                e.Item.Font.Italic = true;                
                e.Item.Cells[0].Text = "&nbsp;&nbsp;" + PnL_DS.Tables[2].Rows[e.Item.ItemIndex]["Ledger_Group_Name"].ToString();
            }

            if (Convert.ToInt32(PnL_DS.Tables[2].Rows[e.Item.ItemIndex]["Ledger_Group_Id"]) == -1)
            {
                e.Item.Font.Italic = false;
                e.Item.Font.Bold = true;
                e.Item.Cells[1].Font.Overline = true;
                e.Item.Cells[1].Font.Underline = true;
            }

        }
    }
    #endregion

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objProfitAndLossAccountPresenter.initValues();
    }
    //protected void cmdprintPreview_Click(object sender, EventArgs e)
    //{

    //    Session.Add("FIN_DS", PnL_DS);
    //    Response.Redirect("~/Reports/Direct_Printing/Frm_FinancePrintingViewer.aspx?Menu_Item_Id=" + Menu_Item_Id + "&Start_Date=" + Util.EncryptString(StartDate.ToString()) + "&End_Date=" + Util.EncryptString(EndDate.ToString()));
    //}
}
