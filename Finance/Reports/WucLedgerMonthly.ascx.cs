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

public partial class Finance_Reports_WucLedgerMonthly : System.Web.UI.UserControl,ILedgerMonthlyView
{
    #region ClassVariable
    DataSet ds = null;
    public DateTime startdate;
    public DateTime enddate;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    public string _CompanyName = UserManager.getUserParam().CompanyName;
    LedgerMonthlyPresenter objLedgerMonthlyPresenter;
    #endregion

    #region ControlsValue

    public int Ledger_Id
    {
        get { return Convert.ToInt32(Request.QueryString["Id"]); }
    }

    public DateTime StartDate
    {
        get { return Convert.ToDateTime(UserManager.getUserParam().StartDate); }
    }

    public DateTime EndDate
    {
        get { return Convert.ToDateTime(UserManager.getUserParam().EndDate); }
    }

    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }

    public int Mode
    {
        get { return Common.GetMode(); }
    }


    public decimal Total_Debit
    {
        get
        {
            return Util.String2Decimal(lbl_Total_Debit.Text);
        }
        set
        {
            lbl_Total_Debit.Text = Util.Decimal2String(value);
            if (lbl_Total_Debit.Text == "0.000")
            {
                lbl_Total_Debit.Text = "";
            }
        }
    }
    public decimal Total_Credit
    {
        get
        {
            return Util.String2Decimal(lbl_Total_Credit.Text);
        }

        set
        {
            lbl_Total_Credit.Text = Util.Decimal2String(value);
            if (lbl_Total_Credit.Text == "0.000")
            {
                lbl_Total_Credit.Text = "";
            }
        }
    }

    public string CompanyName
    {
        set { lbl_Company_Name.Text = value; }
    }

    public String Closing_Balance
    {
        get { return lbl_Closing_Balance.Text; }
        set { lbl_Closing_Balance.Text = value; }
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

    public DataSet LM_DS
    {
        get { return StateManager.GetState<DataSet>("LM_DS"); }
        set { StateManager.SaveState("LM_DS", value); }
    }
    
    #endregion

    #region ControlsBind
    public DataSet BindLedgerMonthlyGrid
    {
        set
        {
            dgLedgerMonthly.DataSource = value;
            Session["DS"] = value;
            dgLedgerMonthly.DataBind();
            dgLedgerMonthly.Items[0].Visible = false;

        }
    }

    #endregion

    #region IView
    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }
    public int keyID
    {
        get { return Convert.ToInt32(Request.QueryString["Id"]); }
    }

    public bool validateUI()
    {
        return true;

    }

    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {

        lbl_Company_Name.Text = _CompanyName;
        Is_Consol = Convert.ToBoolean(Request.QueryString["Is_Consol"]);
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);


        Session["Temp_Date"] = null;
        Session["StartDate"] = null;
        Session["EndDate"] = null;
        objLedgerMonthlyPresenter = new LedgerMonthlyPresenter(this, IsPostBack);

        lbl_From_date.Text = Convert.ToDateTime(UserManager.getUserParam().StartDate).ToString("dd-MMM-yyyy");
        lbl_To_date.Text = Convert.ToDateTime(UserManager.getUserParam().EndDate).ToString("dd-MMM-yyyy");
        lbl_Ledger_Name.Text = Request.QueryString["Name"];

        HttpContext.Current.Session["LM_DS"] = ds;
        HttpContext.Current.Session["St_Dt"] = lbl_From_date.Text;
        HttpContext.Current.Session["End_Dt"] = lbl_To_date.Text;
        HttpContext.Current.Session["Ledger_Name"] = lbl_Ledger_Name.Text;
    }

    #endregion

    #region GridEvents
    protected void dgLedgerMonthly_ItemDataBound(object sender, DataGridItemEventArgs e)
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
                e.Item.Attributes.Add("onmouseout", "this.className='GRIDALTERNATEROWCSS';");//GRIDALTERNATEROWCSS
            }

        }

        //decimal Opening_Balance;
        if (e.Item.ItemIndex == -1)
        {
            ds = (DataSet)Session["DS"];

            e.Item.Cells[0].Text = "&nbsp;&nbsp;" + "Opening Balance";
            e.Item.ForeColor = System.Drawing.Color.Red;
           
            if (ds.Tables[0].Rows[0]["Closing_Balance"].ToString() == "0")
            {
                e.Item.Cells[3].Text = " " + "&nbsp;";
            }
            else
            {
                e.Item.Cells[3].Text = ds.Tables[0].Rows[0]["Closing_Balance"] + "&nbsp;&nbsp;";
            }
        }


    }
    #endregion

}
