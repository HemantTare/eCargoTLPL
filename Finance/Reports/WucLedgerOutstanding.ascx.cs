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
using Infragistics.WebUI.UltraWebGrid;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using System.Data.SqlClient;
using Raj.EC;
using Raj.EC.ControlsView;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;

public partial class Finance_Reports_WucLedgerOutstanding : System.Web.UI.UserControl,ILedgerOutstandingView
{

   #region ClassVariables
    DataSet ds = new DataSet();
    System.Type t;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    string Url;
    int LedgerId;
    LedgerOutstandingPresenter objLedgerOutstandingPresenter;

   #endregion

    #region ControlsValue

    public DateTime StartDate
    {
        get { return WucStartEndDate.Start_Date; }
        set { WucStartEndDate.Start_Date = value; }
    }

    public DateTime EndDate
    {
        get { return WucStartEndDate.End_Date; }
        set { WucStartEndDate.End_Date = value; }
    }
    public string Hierarchy_Code
    {
        get { return HierarchyCode; }
        set { HierarchyCode = value; }
    }

    public int Main_Id
    {
        get { return MainId; }
        set { MainId = value; }
    }

    public Boolean Is_Consol
    {
        get { return IsConsol; }
        set { IsConsol = value; }
    }

    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }
   

    public int Mode
    {
        get { return Common.GetMode(); }
    }

    public int LedgerGroupId
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["LedgerGroupId"]);
           
        }
    }

    public DataSet SessionLedgerOutstandingGrid
    {
        get { return StateManager.GetState<DataSet>("LedgerGrid"); }
        set { StateManager.SaveState("LedgerGrid", value); }
    }
    #endregion

    #region ControlsBind
    public DataTable BindGrid
    {
        set
        {
            ds = value.DataSet;
            grid.DataSource = value;
            grid.DataBind();
        }

           
    }


    #endregion


    #region IView Members

    public string errorMessage
    {
        set { ; }
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public bool validateUI()
    {
        bool _Is_Valid = true;
        return _Is_Valid;
    }

     #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);
        Hierarchy_Code = Convert.ToString(Request.QueryString["HierarchyCode"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
       // LedgerGroupId = Convert.ToInt32(Request.QueryString["LedgerGroupId"]);
       // Url =  Util.GetBaseURL() + "/Grid/FrmGridListing.aspx?";

        btn_Ageing.Visible = false;

        WucStartEndDate.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
            EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);    
        }

        //if (Session["StartDate"] == null)
        //{
        //    StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
        //}
        //else
        //{
        //    StartDate = StateManager.GetState<DateTime>("StartDate");
        //}

        //if (Session["EndDate"] == null)
        //{
        //    EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
        //}
        //else
        //{
        //    EndDate = StateManager.GetState<DateTime>("EndDate");
        //}

        objLedgerOutstandingPresenter = new LedgerOutstandingPresenter(this, IsPostBack);
        
        if (!IsPostBack)
        {
            decimal OnAccount = 0 ;

            objLedgerOutstandingPresenter.GetOnAccount(ref OnAccount);
            if (ds.Tables[0].Rows.Count == 0)
            {
                string AlertScript = "<script language='javascript'>" +
                 "alert('No Records Available!!! ');</script>";
                Page.ClientScript.RegisterStartupScript(typeof(string), "AlertScript", AlertScript);
                string popupScript = "<script language='javascript'> " + "window.close();" + "</script>";
                Page.ClientScript.RegisterStartupScript(typeof(string), "PopupScript", popupScript);
            }
        }


        //this.grid.DisplayLayout.Pager.AllowPaging = true;
        //this.grid.DisplayLayout.Pager.PageSize = 10;
        //this.grid.DisplayLayout.Pager.Alignment = PagerAlignment.Left;
        this.grid.DisplayLayout.EnableInternalRowsManagement = false;
        SetTotal(ds);

     


    }

    public void SetTotal(DataSet Ds)
    {
        DataSet objDS = StateManager.GetState<DataSet>("LedgerGrid");
        Decimal  Amount= 0;

          foreach (DataRow DR in objDS.Tables[0].Rows)
            {
                Amount = Amount + Convert.ToDecimal(DR["Pending_Amount"]);
            }
         
        if (Amount > 0)
        {

            lbl_PendingAmount.Text = String.Format(Math.Abs(Amount).ToString(), "0.00") + " " + "Cr";
        }
        else
        {
            lbl_PendingAmount.Text = String.Format(Math.Abs(Amount).ToString(), "0.00") + " " + "Dr";

        }
        

    }
    //override protected void OnInit(EventArgs e)
    //{
    //    InitializeComponent();
    //    base.OnInit(e);
    //}

    //protected void InitializeComponent()
    //{

    //    this.grid.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.grid_PageIndexChanged);
       
    //}
    protected void grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        grid.DataSource = ds;
        grid.DataBind();
    }
    //protected void grid_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
    //{
    //    this.grid.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

    //    ds = (DataSet)SessionLedgerOutstandingGrid;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        grid.DataSource = ds;
    //        grid.DataBind();
    //    }
    //    SetTotal(ds);
    //}

    protected void grid_InitializeLayout(object sender, LayoutEventArgs e)
    {
        this.grid.DisplayLayout.LoadOnDemand = LoadOnDemand.Xml;
        this.grid.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.OutlookGroupBy;
        this.grid.DisplayLayout.StationaryMargins = StationaryMargins.Header;
        this.grid.DisplayLayout.ColFootersVisibleDefault = ShowMarginInfo.Yes;
        this.grid.DisplayLayout.HeaderStyleDefault.ForeColor = System.Drawing.Color.MediumBlue;
        this.grid.DisplayLayout.TableLayout = TableLayout.Auto;
        this.grid.Bands[0].HeaderStyle.Wrap = true;
        //this.grid.DisplayLayout.Pager.AllowPaging = true;
        //this.grid.DisplayLayout.Pager.PageSize = 10;

        //this.grid.DisplayLayout.Pager.CurrentPageIndex = 1;

        //this.grid.DisplayLayout.Bands[0].Columns[15].AllowRowFiltering = false;
        //HEADER
        foreach (ColumnHeader c in this.grid.Bands[0].HeaderLayout)
        {
            c.RowLayoutColumnInfo.OriginY = 1;
        }
        // Adding total to numeric Field 
        if (ds.Tables[0].Columns.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                grid.Columns[i].CellStyle.Wrap = true;
                try
                {
                    int intUserInput = Convert.ToInt32(ds.Tables[0].Rows[0][i]);

                    t = ds.Tables[0].Rows[0][i].GetType();
                    //MessageBox.Show(t.FullName);
                    if ((t.FullName == "System.Int32") || (t.FullName == "System.Int64") || (t.FullName == "System.Int16") || (t.FullName == "System.Double") || (t.FullName == "System.Decimal"))
                    {
                        this.grid.DisplayLayout.Bands[0].Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Right;
                        this.grid.DisplayLayout.Bands[0].Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                       // this.grid.DisplayLayout.Bands[0].Columns[i].Footer.Style.HorizontalAlign = HorizontalAlign.Right;
                        //this.grid.DisplayLayout.Bands[0].Columns[i].Footer.Style.ForeColor = Color.MediumBlue;
                        //this.grid.DisplayLayout.Bands[0].Columns[i].Footer.Total = SummaryInfo.Sum;
                    }
                }
                catch
                {
                    //invalid user input
                }
            }
        }


        Infragistics.WebUI.UltraWebGrid.ColumnHeader ch = new Infragistics.WebUI.UltraWebGrid.ColumnHeader(true);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch.Caption = "Total Records: " + ds.Tables[0].Rows.Count.ToString();

            ch.RowLayoutColumnInfo.OriginY = 0;
            ch.RowLayoutColumnInfo.OriginX = 1;
            ch.RowLayoutColumnInfo.SpanX = 20;
            this.grid.Bands[0].HeaderLayout.Add(ch);
            this.grid.DisplayLayout.Bands[0].Columns[0].Hidden = true;          
            this.grid.DisplayLayout.Bands[0].Columns[8].Hidden = true;
        }
        else
        {
            ch.Caption = "Total Records: " + ds.Tables[0].Rows.Count.ToString();

            ch.RowLayoutColumnInfo.OriginY = 0;
            ch.RowLayoutColumnInfo.OriginX = 1;
            ch.RowLayoutColumnInfo.SpanX = 20;
            this.grid.Bands[0].HeaderLayout.Add(ch);
            this.grid.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            this.grid.DisplayLayout.Bands[0].Columns[8].Hidden = true;
        }
    }

    protected void grid_InitializeRow(object sender, RowEventArgs e)
    {
        //StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
        //EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
        
        if (e.Row.Cells.Count > 0 && e.Row.Band.Index == 0)
        {
           
            int LedgerId = Convert.ToInt32(e.Row.Cells[0].Value);
            //string Tyre_No = Convert.ToString(e.Row.Cells[1].Value);
            string LedgerName = Convert.ToString(e.Row.Cells[5].Value);

            if ((Convert.ToString(e.Row.Cells[5].Value)) != "")
            {
            //    //e.Row.Cells.FromKey("Ledger Name").Value = "<a href=# style='text-decoration:none;cursor:hand' onClick=" + (char)34 + "window.open('" + Url + "&Id=" + Ledger_Id.ToString()  + "&StartDate=" + Convert.ToDateTime(startdate) + "&EndDate=" + Convert.ToDateTime(enddate) + "', 'test', 'toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=yes, width=850, height=1800,left=50,top=70')" + (char)34 + ">" + e.Row.Cells[5].Value.ToString() + "</a>";
                // e.Row.Cells.FromKey("Ledger Name").Value=Response.Redirect("FrmLedgerBillWiseDetail.aspx?&LedgerId=" + LedgerId.ToString() + "&LedgerGroupId=" + LedgerGroupId.ToString() +"&LedgerName=" + LedgerName +"&Is_Consol=" +Is_Consol +"&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
                e.Row.Cells.FromKey("Ledger Name").TargetURL = "FrmLedgerBillWiseDetail.aspx?&LedgerId=" + LedgerId.ToString() + "&LedgerGroupId=" + LedgerGroupId.ToString() + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsCondensed=" +Convert.ToBoolean(1);
                   
                }
        }
    }

    protected void btn_ShowBillWiseDetails_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("FrmLedgerBillWiseDetail.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + 0 +"&Is_Consol=" +Is_Consol +"&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(1));
    }
    protected void btn_Ageing_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAgeing.aspx");
    }

    public void FillOnDateChange(object sender, EventArgs e)
    {
        decimal OnAccount = 0;

        objLedgerOutstandingPresenter.GetOnAccount(ref OnAccount);
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        UltraWebGridExcelExporter1.Export(grid);
    }
}
