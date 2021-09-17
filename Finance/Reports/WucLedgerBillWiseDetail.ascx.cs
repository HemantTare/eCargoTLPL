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

public partial class Finance_Reports_WucLedgerBillWiseDetail : System.Web.UI.UserControl,ILedgerBillWiseDetailView
{
    #region ClassVariables
    DataSet ds = new DataSet();
    System.Type t;
    public string HierarchyCode;
    public Boolean IsConsol;
    public Boolean Is_Condensed;
    public int MainId;
   
    LedgerBillWiseDetailPresenter objLedgerBillWisePresenter;
    #endregion

    #region ControlsValue

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
    public Boolean IsCondensed
    {
        get { return Is_Condensed; }
        set { Is_Condensed = value; }
    }

    public int LedgerId
    {
        
        get { return Convert.ToInt32(Request.QueryString["LedgerId"]); }
    }


    public string LedgerName
    {
       
        get { return Convert.ToString(Request.QueryString["LedgerName"]); }
        set { lbl_LedgerName.Text = value; }
    }

    public DataSet SessionLedgerBillWiseGrid
    {
        get { return StateManager.GetState<DataSet>("LedgerBillWiseDetailGrid"); }
        set { StateManager.SaveState("LedgerBillWiseDetailGrid", value); }
    }

    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value) > 0)
        { return value.ToString() + " Dr"; }
        else { return value.ToString() +  " Cr"; }
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Math.Abs(Convert.ToDecimal(value)); }
    }

    #endregion

    #region ControlsBind
    public DataTable BindGrid
    {
        set
        {
            ds = value.DataSet;
            grid1.DataSource = value;
            grid1.DataBind();
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
        Is_Consol = Convert.ToBoolean(Request.QueryString["Is_Consol"]);
        HierarchyCode = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        IsCondensed = Convert.ToBoolean(Request.QueryString["IsCondensed"]);
       // Session["IsCondensed"] = true;
        Session["IsCondensed"] = IsCondensed;
     
        if (LedgerName == null)
        {
          
            lbl_ledger_Name.Visible = false;

        }
        else
        {
            lbl_LedgerName.Text = LedgerName;
        }

        WucStartEndDate1.OnDateChange += new EventHandler(FillOnDateChange);
        
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
        if (LedgerId == 0)
        {
            btn_Detailed.Visible = false;
            btn_Ageing.Visible = false;
        }       
       
        objLedgerBillWisePresenter = new LedgerBillWiseDetailPresenter(this, IsPostBack);
      
      

        if (!IsPostBack)
        {
            //objLedgerBillWisePresenter = new LedgerBillWiseDetailPresenter(this, IsPostBack);
           
             if ((Boolean)Session["IsCondensed"] == true)
             {
                 btn_Detailed.Text = "View Detailed";
             }
             else
             {
                 btn_Detailed.Text = "View Condensed";
             }
            
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
        this.grid1.DisplayLayout.EnableInternalRowsManagement = false;

        


    }

    protected void grid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        grid1.DataSource = ds;
        grid1.DataBind();
    }

    protected void grid1_InitializeLayout(object sender, LayoutEventArgs e)
    {
        this.grid1.DisplayLayout.LoadOnDemand = LoadOnDemand.Xml;
        this.grid1.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.OutlookGroupBy;
        this.grid1.DisplayLayout.StationaryMargins = StationaryMargins.Header;
        this.grid1.DisplayLayout.ColFootersVisibleDefault = ShowMarginInfo.Yes;
        this.grid1.DisplayLayout.HeaderStyleDefault.ForeColor = System.Drawing.Color.MediumBlue;
        this.grid1.DisplayLayout.TableLayout = TableLayout.Auto;
        this.grid1.Bands[0].HeaderStyle.Wrap = true;

        if (IsCondensed == false)
        {
           

            this.grid1.Bands[0].Columns[8].CellStyle.Font.Italic = true;
            this.grid1.DisplayLayout.Bands[0].Columns[8].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid1.Bands[0].Columns[9].CellStyle.Font.Italic = true;
            this.grid1.DisplayLayout.Bands[0].Columns[9].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid1.Bands[0].Columns[10].CellStyle.Font.Italic = true;
            this.grid1.DisplayLayout.Bands[0].Columns[10].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid1.Bands[0].Columns[11].CellStyle.Font.Italic = true;
            this.grid1.DisplayLayout.Bands[0].Columns[11].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid1.Bands[0].Columns[12].CellStyle.Font.Italic = true;
            this.grid1.DisplayLayout.Bands[0].Columns[12].CellStyle.ForeColor = System.Drawing.Color.Maroon;
        }
       // this.grid.DisplayLayout.Bands[0].Columns[8]. = System.Drawing.FontStyle.Italic;
        //this.grid.DisplayLayout.Pager.AllowPaging = true;
        //this.grid.DisplayLayout.Pager.PageSize = 10;

        //this.grid.DisplayLayout.Pager.CurrentPageIndex = 1;

        //this.grid.DisplayLayout.Bands[0].Columns[15].AllowRowFiltering = false;
        //HEADER
        foreach (ColumnHeader c in this.grid1.Bands[0].HeaderLayout)
        {
            c.RowLayoutColumnInfo.OriginY = 1;
        }
        // Adding total to numeric Field 
        if (ds.Tables[0].Columns.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                grid1.Columns[i].CellStyle.Wrap = true;
                try
                {
                    int intUserInput = Convert.ToInt32(ds.Tables[0].Rows[0][i]);

                    t = ds.Tables[0].Rows[0][i].GetType();
                    //MessageBox.Show(t.FullName);
                    if ((t.FullName == "System.Int32") || (t.FullName == "System.Int64") || (t.FullName == "System.Int16") || (t.FullName == "System.Double") || (t.FullName == "System.Decimal"))
                    {
                        this.grid1.DisplayLayout.Bands[0].Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Right;
                        this.grid1.DisplayLayout.Bands[0].Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
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
            this.grid1.Bands[0].HeaderLayout.Add(ch);
            this.grid1.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            //this.grid.DisplayLayout.Bands[0].Columns[8].Hidden = true;
        }
        else
        {
            ch.Caption = "Total Records: " + ds.Tables[0].Rows.Count.ToString();

            ch.RowLayoutColumnInfo.OriginY = 0;
            ch.RowLayoutColumnInfo.OriginX = 1;
            ch.RowLayoutColumnInfo.SpanX = 20;
            this.grid1.Bands[0].HeaderLayout.Add(ch);
            this.grid1.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            //this.grid.DisplayLayout.Bands[0].Columns[8].Hidden = true;
        }
    }
    protected void grid1_InitializeRow(object sender, RowEventArgs e)
    {
        //StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
        //EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);

        if (e.Row.Cells.Count > 0 && e.Row.Band.Index == 0)
        {

            int LedgerId = Convert.ToInt32(e.Row.Cells[0].Value);
            //string Tyre_No = Convert.ToString(e.Row.Cells[1].Value);
            string LedgerName = Convert.ToString(e.Row.Cells[5].Value);

            if (Convert.ToDateTime(e.Row.Cells[6].Value) == Convert.ToDateTime("1/1/1900"))
            {
                e.Row.Cells[6].Value = "";
            }

            if (Is_Condensed == true)
            {
                if (Convert.ToDateTime(e.Row.Cells[10].Value) == Convert.ToDateTime("1/1/1900"))
                {
                    e.Row.Cells[10].Value = "";
                }

                if (Convert.ToInt32(e.Row.Cells[8].Value) == 0 || Convert.ToInt32(e.Row.Cells[9].Value) == 0)
                {
                    e.Row.Cells[8].Value = "";
                    e.Row.Cells[9].Value = "";
                }
                else
                {
                    e.Row.Cells[8].Value = convertToDrCr(e.Row.Cells[8].Value);
                    e.Row.Cells[9].Value = convertToDrCr(e.Row.Cells[9].Value);
                }
               
            }
            else
            {
                if (Convert.ToDateTime(e.Row.Cells[15].Value) == Convert.ToDateTime("1/1/1900"))
                {
                    e.Row.Cells[15].Value = "";
                }

                if (Convert.ToInt32(e.Row.Cells[13].Value) == 0 || Convert.ToInt32(e.Row.Cells[14].Value) == 0)
                {
                    e.Row.Cells[13].Value = "";
                    e.Row.Cells[14].Value = "";
                }
                else
                {
                    e.Row.Cells[13].Value = convertToDrCr(e.Row.Cells[13].Value);
                    e.Row.Cells[14].Value = convertToDrCr(e.Row.Cells[14].Value);
                }
            }

            //if ((Convert.ToString(e.Row.Cells[5].Value)) != "")
           // {
                //e.Row.Cells.FromKey("Ledger Name").Value = "<a href=# style='text-decoration:none;cursor:hand' onClick=" + (char)34 + "window.open('" + Url + "&Id=" + Ledger_Id.ToString()  + "&StartDate=" + Convert.ToDateTime(startdate) + "&EndDate=" + Convert.ToDateTime(enddate) + "', 'test', 'toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=yes, width=850, height=1800,left=50,top=70')" + (char)34 + ">" + e.Row.Cells[5].Value.ToString() + "</a>";
               // e.Row.Cells.FromKey("ledger Name").Value = Response.Redirect("~/FrmLedgerBillWiseDetails.aspx?&Ledger_Id=" + Ledger_Id + "&LedgerGroupId=" + LedgerGroupId + "&LedgerName=" + LedgerName);
           // }
        }
    }


    protected void btn_ShowLedgerOutstanding_Click(object sender, EventArgs e)
    {
        btn_Ageing.Visible = false;    
        Response.Redirect("FrmLedgerOutstanding.aspx?&LedgerGroupId=" + LedgerGroupId +  "&IsConsolidated=" + Is_Consol + "&HierarchyCode=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
    }

    protected void btn_Ageing_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAgeing.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate );
    }
  
    protected void btn_Detailed_Click(object sender, EventArgs e)
    {
       
        if ((Boolean)Session["IsCondensed"] == true)
        {

            //btn_Detailed.Text = "View Condensed";
            //Session["IsCondensed"] = false;           
            Response.Redirect("FrmLedgerBillWiseDetail.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(0));
           
        }
        else
        {
            //btn_Detailed.Text = "";
            ////btn_Detailed.Text = "View Detailed";
            //Session["IsCondensed"] = true;
            Response.Redirect("FrmLedgerBillWiseDetail.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(1));
          
        }

        
       
    }


    public void FillOnDateChange(object sender, EventArgs e)
    {
        objLedgerBillWisePresenter.initValues();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        UltraWebGridExcelExporter1.Export(grid1);
    }
}
