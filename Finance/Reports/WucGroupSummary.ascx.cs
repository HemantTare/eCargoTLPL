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

public partial class Finance_Reports_WucGroupSummary : System.Web.UI.UserControl,IGroupSummaryView
{
    #region ClassVariables
    GroupSummaryPresenter objGroupSummaryPresenter;
    //public DateTime startdate;
    //public DateTime enddate;
    private Boolean IsConsol;
    private String HierarchyCode;
    private int MainId;
    public int DivisionId;
    public int Menu_Item_Id;
   
    public string _CompanyName = UserManager.getUserParam().CompanyName;
     

    #endregion


    #region ControlsValue


    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }

    public int MenuItemCode
    {
        get { return Common.GetMenuItemCode(); }
    }

    public int Mode
    {
        get { return Common.GetMode(); }
    }

    public int Ledger_Id
    {

        get
        {
            //int MenuItemId = Common.GetMenuItemId();
            int Id=0;
                if (Request.QueryString["Id"] == null)
                {
                    if (MenuItemCode == 74)
                     {
                         Id = 20;
                     }
                     else if (MenuItemCode == 75)
                     {
                         Id = 19;
                     }
            
                   //int Id = Convert.ToInt32(Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);
                }
                else 
                {
                    Id = Util.DecryptToInt(Request.QueryString["Id"]);
                }

                return Id;
        }
    }
   



    public int Category
    {
        get
        {          
            return Convert.ToInt32(Request.QueryString["Category"]);
        }
    }

    public string Group
    {
        get 
        {
            if (Request.QueryString["Group"] != null)
            {
                return Request.QueryString["Group"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
             if (Request.QueryString["Group"] == null)
            {
                lblGroup.Visible = false;
                lbl_Group.Visible = false;
                lblUnder.Visible = false;
                lbl_Under.Visible = false;
            }
            lbl_Group.Text = value;

        }
    }

    public string Under
    {
        get 
        {
            if (Request.QueryString["Under"] != null)
            {
                return Request.QueryString["Under"].ToString();
            }
            else
            {
                return "Primary";
            }
        }
        set
        {
            lbl_Under.Text = value;
        }
    }

    public int IsForCostCentre
    {
        get
        {
            if (Request.QueryString["IsForCostCentre"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(Request.QueryString["IsForCostCentre"]);
            }
        }
    }


    public string CompanyName
    {
        set { lbl_Company_Name.Text = value; }
    }

    public DateTime StartDate
    {
        get { return WucGridSearch1.StartDate; }
        set { WucGridSearch1.StartDate = value; }
    }

    public DateTime EndDate
    {
        get { return WucGridSearch1.EndDate; }
        set { WucGridSearch1.EndDate = value; }
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

    public int Division_Id
    {
        get { return DivisionId; }
        set { DivisionId = value; }
    }


    private DataSet GS_DS
    {
        get { return StateManager.GetState<DataSet>("GS_DS"); }
        set { StateManager.SaveState("GS_DS", value); }
    }

    #endregion

    #region ControlsBind

    public DataSet BindGroupSummaryGrid
    {
        set
        {
            GS_DS = value;
            dgGroupSummary .DataSource = value;
            dgGroupSummary.DataBind();
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
        get { return Convert.ToInt32(Request.QueryString["Id"]);}
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
        DivisionId = Convert.ToInt32(Request.QueryString["DivisionId"]);
        DataSet ds = new DataSet();
        //Wuc_StartEndDate.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            StartDate = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["StartDate"]));
            EndDate = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["EndDate"]));
            WucGridSearch1.SetComSepColumnName = "Particulars=Name,Opening Bal Debit=OpeningDr,Opening Bal Credit=OpeningCr,Current Bal Debit=CurrentDr,Current Bal Credit=CurrentCr,Closing Bal Debit=ClosingDr,Closing Bal Credit=ClosingCr";
                      
        }
        
        Group = Group;
        Under = Under;

        if (Util.DecryptToInt(Request.QueryString["Category"])== 2)
        {
            Response.Redirect("FrmLedgerMonthly.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&Id=" + Ledger_Id + "&Name=" + Group + "&DivisionId=" + Division_Id);
        }
        else
        {
           objGroupSummaryPresenter = new GroupSummaryPresenter(this, IsPostBack);
        }

        if (Util.DecryptToInt(Request.QueryString["Category"]) == 3)
        {
            Response.Redirect("FrmProfitAndLossAccount.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&IsConsolidated=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&Id=" + Request.QueryString["Id"] + "&StartDate=" + Util.EncryptString(StartDate.ToString()) + "&EndDate=" + Util.EncryptString(EndDate.ToString()) + "&DivisionId=" + Division_Id);
        }
        WucGridSearch1.SetDataGrid = dgGroupSummary;
        
        

        if (Session["GS_DS"] != null)
        {
            
            ds.Tables.Add(GS_DS.Tables[0].Copy());
            WucGridSearch1.SetDataSet = ds;
        }

        WucGridSearch1.btnChangedPeriod = new EventHandler(FillOnDateChange);
        HttpContext.Current.Session["GS_DS1"] = GS_DS;
        HttpContext.Current.Session["St_Dt"] = StartDate.ToString("dd-MMM-yyyy");
        HttpContext.Current.Session["End_Dt"] = EndDate.ToString("dd-MMM-yyyy");
        //btn_period.Attributes.Add("OnClick", "javascript:window.open('../../CommonControls/FrmDateRange.aspx','win','height=500,width=600,resizable=Yes,scrollbars=Yes,menubar=No');");

        Session.Add("FIN_DS", GS_DS);
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Reports/Direct_Printing/Frm_FinancePrintingViewer.aspx?Menu_Item_Id=" + Menu_Item_Id + "&Start_Date=" + Util.EncryptString(StartDate.ToString()) + "&End_Date=" + Util.EncryptString(EndDate.ToString()) + "&GRPSUMM=" + Group );
        Btn_Preview.Attributes.Add("onclick", "return Open_Show_Window('" + Path + "')");

    }


    //--Function For Footer Total Start
    public decimal totopDR, totopCR, totcrntDR, totcrntCR, totclDR, totclCR;
    //OPENING DR
    public decimal FootTotal_OP_DR()
    {
        if (GS_DS.Tables[1].Rows.Count > 0)
            return totopDR = Convert.ToDecimal(GS_DS.Tables[1].Rows[0]["Tot_OP_DR"].ToString());

        return 0;
    }
    //OPENING CR
    public decimal FootTotal_OP_CR()
    {
        if (GS_DS.Tables[1].Rows.Count > 0)
            return totopCR = Convert.ToDecimal(GS_DS.Tables[1].Rows[0]["Tot_OP_CR"].ToString());

        return 0;
    }
    //CURRENT DR
    public decimal FootTotal_CRNT_DR()
    {
        if (GS_DS.Tables[1].Rows.Count > 0)
            return totcrntDR = Convert.ToDecimal(GS_DS.Tables[1].Rows[0]["Tot_CRNT_DR"].ToString());

        return 0;
    }
    //CURRNT CR
    public decimal FootTotal_CRNT_CR()
    {
        if (GS_DS.Tables[1].Rows.Count > 0)
            return totcrntCR = Convert.ToDecimal(GS_DS.Tables[1].Rows[0]["Tot_CRNT_CR"].ToString());

        return 0;
    }
    //CLOSING DR
    public decimal FootTotal_CL_DR()
    {
        if (GS_DS.Tables[1].Rows.Count > 0)
            return totclDR = Convert.ToDecimal(GS_DS.Tables[1].Rows[0]["Tot_CL_DR"].ToString());

        return 0;
    }
    //CLOSING CR
    public decimal FootTotal_CL_CR()
    {
        if (GS_DS.Tables[1].Rows.Count > 0)
            return totclCR = Convert.ToDecimal(GS_DS.Tables[1].Rows[0]["Tot_CL_CR"].ToString());

        return 0;
    }

    //--Function End

    public void FillOnDateChange(object sender,EventArgs e)
    {
        objGroupSummaryPresenter.initValues();
    }
    #endregion

    #region GridEvents
    protected void dgGroupSummary_RowDataBound(object sender, DataGridItemEventArgs e)
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
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");//GRIDALTERNATEROWCSS
            }
            
        }
    }
    protected void dgGroupSummary_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgGroupSummary.CurrentPageIndex = e.NewPageIndex;
        BindGroupSummaryGrid = GS_DS;
    }
    protected void dgGroupSummary_PreRender(object sender, EventArgs e)
    {
        DataGrid _DataGrid = (DataGrid)sender;
        DataGridItem _DataGridItem = new DataGridItem(0, 0, ListItemType.Header);
        TableCell _TableCell = new TableCell();



        //---- Create Header------

        _TableCell = new TableCell();
        _TableCell.Text = "PARTICULARS";
        _TableCell.HorizontalAlign = HorizontalAlign.Left;
        _TableCell.ColumnSpan = 1;
        _DataGridItem.Cells.Add(_TableCell);

        _TableCell = new TableCell();
        _TableCell.Text = "OPENING BALANCE";
        _TableCell.HorizontalAlign = HorizontalAlign.Center;
        _TableCell.ColumnSpan = 2;
        _DataGridItem.Cells.Add(_TableCell);

        _TableCell = new TableCell();
        _TableCell.Text = "CURRENT TRANSACTIONS";
        _TableCell.HorizontalAlign = HorizontalAlign.Center;
        _TableCell.ColumnSpan = 2;
        _DataGridItem.Cells.Add(_TableCell);

        _TableCell = new TableCell();
        _TableCell.Text = "CLOSING BALANCE";
        _TableCell.HorizontalAlign = HorizontalAlign.Center;
        _TableCell.ColumnSpan = 2;
        _DataGridItem.Cells.Add(_TableCell);

        _DataGrid.Controls[0].Controls.AddAt(1, _DataGridItem);
    }
    #endregion

}
