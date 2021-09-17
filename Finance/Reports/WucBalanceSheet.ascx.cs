using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;

/// Name : Ankit champaneriya
/// Date : 23-10-08
/// Desc : BalanceSheet Page.
/// 

public partial class Reports_WucBalanceSheet : System.Web.UI.UserControl, IBalanceSheetView
{
    #region class variable

    private BalanceSheetPresenter _BSPresenter;
    public int Menu_Item_Id;
    public Boolean IsDetails;
    public Boolean IsConsol;
    public String HierarchyCode;
    public int MainId;
    public int Mode;
    
    #endregion

    #region ViewImplement

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string CompanyName
    {
        set { lbl_Company_Name.Text = value; }
    }

    public DateTime Start_Date
    {
        get { return WucStartEndDate.Start_Date; }
        set { WucStartEndDate.Start_Date = value; }
    }
    public DateTime End_Date
    {
        get { return WucStartEndDate.End_Date; }
        set { WucStartEndDate.End_Date = value; }
    }

    public string Main
    {
        get { return Convert.ToString(Request.QueryString["Main"]); }
    }

    public int Cat_Id
    {
        get { return Convert.ToInt32(Request.QueryString["Cat_Id"]); }
    }


    public DataTable BindLiabliltyGrid
    {
        set
        {
            //Session["BS_DS"]=value;
            dg_Libs.DataSource = value;
            dg_Libs.DataBind();
        }
    }

    public DataTable BindAssetsGrid
    {
        set
        {
            dg_Assets.DataSource = value;
            dg_Assets.DataBind();
        }
    }

    public DataSet BS_DS
    {
        get { return StateManager.GetState<DataSet>("BS_DS"); }
        set { StateManager.SaveState("BS_DS", value); }

    }

    public Boolean Is_Consol
    {
        get { return IsConsol; }
        set { IsConsol = value; }
    }

    public Boolean Is_Details
    {
        get { return IsDetails; }
        set { IsDetails = value; }
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

    public bool validateUI()
    {
        return true;
    }

    public string errorMessage { set { lbl_Error.Text = value; } }

    #endregion

    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {

        Menu_Item_Id = Common.GetMenuItemId();
        Mode = Common.GetMode();
        //StateManager.SaveState("URL", "~/Reports/Display/frm_Balance_Sheet.aspx?A=" + "A" + "&Menu_Item_Id=" + ClassLibrary.Util.EncryptInteger(Menu_Item_Id));

        Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);

        
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);

        Is_Details = Convert.ToBoolean(Convert.ToInt32(Request.QueryString["Is_Details"]));

        WucStartEndDate.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            Start_Date = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["StartDate"]));
            End_Date = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["EndDate"]));

        }

        _BSPresenter = new BalanceSheetPresenter(this, IsPostBack);
        Session["BS_DS"] = BS_DS;
        lbl_Assets_Total.Text = BS_DS.Tables[2].Rows[0]["Assets"].ToString();  //updated by Ankit champaneriya
        lbl_Liability_Total.Text = BS_DS.Tables[2].Rows[0]["Liab"].ToString();

        btn_Detail.Visible = false;

        // PRINT PREVIEW
        Session.Add("FIN_DS", BS_DS);
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Reports/Direct_Printing/Frm_FinancePrintingViewer.aspx?Menu_Item_Id=" + Menu_Item_Id + "&Start_Date=" + Util.EncryptString(Start_Date.ToString()) + "&End_Date=" + Util.EncryptString(End_Date.ToString()));
        cmdPreview.Attributes.Add("onclick", "return Open_Show_Window('" + Path + "')");

    }

    #endregion

    #region event click
    protected void btn_Detail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FA_Common/Reports/frm_Balance_Sheet_Details.aspx?Is_Consol=" + Is_Consol.ToString() + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id.ToString() + "&Start_Date=" + Util.EncryptString(Start_Date.ToString()) + "&End_Date=" + Util.EncryptString(End_Date.ToString()) + "&Is_Details=1");
        //Response.Redirect("~/FA_Common/Reports/frm_Balance_Sheet_Details.aspx?Is_Consol=" + Is_Consol.ToString() + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id.ToString() + "&Start_Date=" + ClassLibraryMVP.crypt.EncryptString(Start_Date.ToString()) + "&End_Date=" + ClassLibraryMVP.crypt.EncryptString(End_Date.ToString()) + "&Is_Details=1");
       
    }

    protected void DG_ItemDataBound_Lib(object sender, DataGridItemEventArgs e)
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

            if (Is_Details)
            {
                if (Convert.ToInt32(BS_DS.Tables[2].Rows[e.Item.ItemIndex]["Category"]) == 0)
                {
                    e.Item.Font.Bold = true;
                }
                else if (Convert.ToInt32(BS_DS.Tables[2].Rows[e.Item.ItemIndex]["Category"]) == 3)
                {
                    e.Item.Font.Bold = true;
                    //e.Item.Cells[0].Text = BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Liabilities"].ToString();
                }
                else if (Convert.ToInt32(BS_DS.Tables[2].Rows[e.Item.ItemIndex]["Category"]) == 6)
                {
                    e.Item.Font.Italic = true;
                    e.Item.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[2].Rows[e.Item.ItemIndex]["Liabilities"].ToString();
                }
                else
                {
                    e.Item.Font.Italic = true;
                    e.Item.ForeColor = System.Drawing.Color.DarkSlateGray;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[2].Rows[e.Item.ItemIndex]["Liabilities"].ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Category"]) == 0)
                {
                    e.Item.Font.Bold = true;
                }
                else if (Convert.ToInt32(BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Category"]) == 3)
                {
                    e.Item.Font.Bold = true;
                    //e.Item.Cells[0].Text = BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Liabilities"].ToString();
                }
                else if (Convert.ToInt32(BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Category"]) == 6)
                {
                    e.Item.Font.Italic = true;
                    e.Item.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Liabilities"].ToString();
                }
                else
                {
                    e.Item.Font.Italic = true;
                    e.Item.ForeColor = System.Drawing.Color.DarkSlateGray;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[0].Rows[e.Item.ItemIndex]["Liabilities"].ToString();
                }
            }


        }
    }

    protected void DG_ItemDataBound_Asset(object sender, DataGridItemEventArgs e)
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
            if (Is_Details)
            {
                if (Convert.ToInt32(BS_DS.Tables[3].Rows[e.Item.ItemIndex]["Category"]) == 0)
                {
                    e.Item.Font.Bold = true;
                }
                else if (Convert.ToInt32(BS_DS.Tables[3].Rows[e.Item.ItemIndex]["Category"]) == 3)
                {
                    e.Item.Font.Bold = true;
                    //e.Item.Cells[0].Text = BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Assets"].ToString(); 
                }
                else if (Convert.ToInt32(BS_DS.Tables[3].Rows[e.Item.ItemIndex]["Category"]) == 6)
                {
                    e.Item.Font.Italic = true;
                    e.Item.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[3].Rows[e.Item.ItemIndex]["Assets"].ToString();
                }
                else
                {
                    e.Item.Font.Italic = true;
                    e.Item.ForeColor = System.Drawing.Color.DarkSlateGray;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[3].Rows[e.Item.ItemIndex]["Assets"].ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Category"]) == 0)
                {
                    e.Item.Font.Bold = true;
                }
                else if (Convert.ToInt32(BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Category"]) == 3)
                {
                    e.Item.Font.Bold = true;
                    //e.Item.Cells[0].Text = BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Assets"].ToString(); 
                }
                else if (Convert.ToInt32(BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Category"]) == 6)
                {
                    e.Item.Font.Italic = true;
                    e.Item.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Assets"].ToString();
                }
                else
                {
                    e.Item.Font.Italic = true;
                    e.Item.ForeColor = System.Drawing.Color.DarkSlateGray;
                    e.Item.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + BS_DS.Tables[1].Rows[e.Item.ItemIndex]["Assets"].ToString();
                }
            }
        }
    }

    public void FillOnDateChange(object sender, EventArgs e)
    {
        _BSPresenter.initValues();
    }

    #endregion
}
