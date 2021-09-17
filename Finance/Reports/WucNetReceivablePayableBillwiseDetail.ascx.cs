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


public partial class Finance_Reports_WucNetReceivablePayableBillwiseDetail : System.Web.UI.UserControl,INetReceivablePayableBillwiseView
{
    #region ClassVariables
    DataSet ds = new DataSet();
    System.Type t;
    public string HierarchyCode;
    public Boolean IsConsol;
    public Boolean Is_Condensed;
    public int MainId;

    NetReceivablePayableBillwisePresenter objNetReceivablePayableBillwisePresenter;
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

    public Boolean IsReceivable
    {
        get
        {
            return StateManager.GetState<Boolean>("IsReceivable");

        }
        set
        {
            StateManager.SaveState("IsReceivable", value);
        }
    }

    public DataSet SessionNetReceivablePayableBillWiseGrid
    {
        get { return StateManager.GetState<DataSet>("NetReceivablePayableBillWiseDetailGrid"); }
        set { StateManager.SaveState("NetReceivablePayableBillWiseDetailGrid", value); }
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
        Is_Consol = Convert.ToBoolean(Request.QueryString["Is_Consol"]);
        HierarchyCode = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        IsCondensed = Convert.ToBoolean(Request.QueryString["IsCondensed"]);
        Session["IsCondensed"] = IsCondensed;

        WucStartEndDate1.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
            EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
        }

        if (IsReceivable == true)
        {
            lbl_Heading.Text="NET RECEIVABLE BILLWISE DETAIL";
            btn_ShowBillWiseDetails.Text = "show Net Receivable Outstandings";
            
        }
        else
        {
            lbl_Heading.Text="NET PAYABLE BILLWISE DETAIL";
            btn_ShowBillWiseDetails.Text = "show Net Payable Outstandings";

        }

        if (LedgerName == null)
        {
          
            lbl_ledger_Name.Visible = false;

        }
        else
        {
            lbl_LedgerName.Text = LedgerName;
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
       
      
      

        if (!IsPostBack)
        {
            
             objNetReceivablePayableBillwisePresenter = new NetReceivablePayableBillwisePresenter(this,IsPostBack);
             
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


        
        this.grid.DisplayLayout.EnableInternalRowsManagement = false;
      
    }

    protected void grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        grid.DataSource = ds;
        grid.DataBind();
    }

    protected void grid_InitializeLayout(object sender, LayoutEventArgs e)
    {
        this.grid.DisplayLayout.LoadOnDemand = LoadOnDemand.Xml;
        this.grid.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.OutlookGroupBy;
        this.grid.DisplayLayout.StationaryMargins = StationaryMargins.Header;
        this.grid.DisplayLayout.ColFootersVisibleDefault = ShowMarginInfo.Yes;
        this.grid.DisplayLayout.HeaderStyleDefault.ForeColor = System.Drawing.Color.MediumBlue;
        this.grid.DisplayLayout.TableLayout = TableLayout.Auto;
        this.grid.Bands[0].HeaderStyle.Wrap = true;

        if (IsCondensed == false)
        {
           

            this.grid.Bands[0].Columns[8].CellStyle.Font.Italic = true;
            this.grid.DisplayLayout.Bands[0].Columns[8].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid.Bands[0].Columns[9].CellStyle.Font.Italic = true;
            this.grid.DisplayLayout.Bands[0].Columns[9].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid.Bands[0].Columns[10].CellStyle.Font.Italic = true;
            this.grid.DisplayLayout.Bands[0].Columns[10].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid.Bands[0].Columns[11].CellStyle.Font.Italic = true;
            this.grid.DisplayLayout.Bands[0].Columns[11].CellStyle.ForeColor = System.Drawing.Color.Maroon;
            this.grid.Bands[0].Columns[12].CellStyle.Font.Italic = true;
            this.grid.DisplayLayout.Bands[0].Columns[12].CellStyle.ForeColor = System.Drawing.Color.Maroon;
        }

       
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
        }
        else
        {
            ch.Caption = "Total Records: " + ds.Tables[0].Rows.Count.ToString();

            ch.RowLayoutColumnInfo.OriginY = 0;
            ch.RowLayoutColumnInfo.OriginX = 1;
            ch.RowLayoutColumnInfo.SpanX = 20;
            this.grid.Bands[0].HeaderLayout.Add(ch);
            this.grid.DisplayLayout.Bands[0].Columns[0].Hidden = true;
        }
    }
    protected void grid_InitializeRow(object sender, RowEventArgs e)
    {
        //StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
        //EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);

        if (e.Row.Cells.Count > 0 && e.Row.Band.Index == 0)
        {

            int LedgerId = Convert.ToInt32(e.Row.Cells[0].Value);
            string LedgerName = Convert.ToString(e.Row.Cells[5].Value);

            if (e.Row.Cells[8].Value.ToString() != "")
            {
                e.Row.Cells[8].Value = Convert.ToString(Math.Abs(Convert.ToDecimal(e.Row.Cells[8].Value))) + (Util.String2Decimal(e.Row.Cells[8].Value.ToString()) < 0 ? " Dr" : " Cr");
            }

            if (e.Row.Cells[9].Value.ToString() != "")
            {
                e.Row.Cells[9].Value = Convert.ToString(Math.Abs(Convert.ToDecimal(e.Row.Cells[9].Value))) + (Util.String2Decimal(e.Row.Cells[9].Value.ToString()) < 0 ? " Dr" : " Cr");
            }

            
        }       
        
    }


    protected void btn_ShowReceivableOutstanding_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmNetReceivablePayable.aspx?&LedgerGroupId=" + LedgerGroupId + "&IsConsolidated=" + Is_Consol + "&HierarchyCode=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&IsReceivable=" + IsReceivable + "&Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
    }

    protected void btn_Ageing_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAgeing.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&EndDate=" + EndDate );
    }
  
    protected void btn_Detailed_Click(object sender, EventArgs e)
    {
       
        if ((Boolean)Session["IsCondensed"] == true)
        {

            Response.Redirect("FrmNetReceivablePayableBillWiseDetail.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&IsReceivable=" + IsReceivable + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(0));
        
        }
        else
        {
            Response.Redirect("FrmNetReceivablePayableBillWiseDetail.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&IsReceivable=" + IsReceivable + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(1));
         
        }        
       
    }

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objNetReceivablePayableBillwisePresenter.initValues();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        UltraWebGridExcelExporter1.Export(grid);
    }
}



