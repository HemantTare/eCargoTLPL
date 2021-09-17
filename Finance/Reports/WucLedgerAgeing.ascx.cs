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
using System.IO;




public partial class Finance_Reports_WucLedgerAgeing : System.Web.UI.UserControl, ILedgerAgeingView
{
    #region ClassVariables
    DataSet ds = new DataSet();
    System.Type t;
    public string HierarchyCode;
    public Boolean IsConsol;
    public Boolean Is_Condensed;
    public int MainId;
    private decimal _Total_Opening_Amount;
    private decimal _Total_Pending_Amount; 
    private decimal  Footer_Amount;


    LedgerAgeingPresenter objLedgerAgeingPresenter;

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
        set { lbl_Ledger_Name.Text = value; }
    }

    public DataSet SessionLedgerAgeingGrid
    {
        get { return StateManager.GetState<DataSet>("LedgerAgeingGrid"); }
        set { StateManager.SaveState("LedgerAgeingGrid", value); }
    }

    public bool IsBillDate
    {
        get { return Convert.ToBoolean(Request.QueryString["IsBillDate"]); }
    }
    #endregion

    #region ControlsBind

    public DataTable BindLedgerAgeing
    {
        set
        {
            dg_Ageing_Outstanding_Grid.DataSource = value;
            dg_Ageing_Outstanding_Grid.DataBind();
        }
    }
    #endregion

    #region OtherFunction
    public DataSet FillGrid()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("Table");//New Table Created Which Contains Static Column
        dt.Columns.Add("Bill_Date", System.Type.GetType("System.String"));
        dt.Columns.Add("Ref_No", System.Type.GetType("System.String"));
        dt.Columns.Add("Ref_Type", System.Type.GetType("System.String"));
        dt.Columns.Add("Voucher_Date", System.Type.GetType("System.String"));
        dt.Columns.Add("Voucher_Type", System.Type.GetType("System.String"));
        dt.Columns.Add("Date", System.Type.GetType("System.String"));
        dt.Columns.Add("Voucher_Name", System.Type.GetType("System.String"));
        dt.Columns.Add("Voucher_No", System.Type.GetType("System.String"));
        dt.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
        dt.Columns.Add("Opening_Amount", System.Type.GetType("System.Decimal"));
        dt.Columns.Add("Pending_Amount", System.Type.GetType("System.Decimal"));
        dt.Columns.Add("Due_Date", System.Type.GetType("System.String"));
        dt.Columns.Add("OverDueDays", System.Type.GetType("System.Int32"));
        //  dt.Columns.Add("Ref_Type_ID", System.Type.GetType("System.Int32"));
        dt.Columns.Add("Voucher_ID", System.Type.GetType("System.Int32"));
        ds.Tables.Add(dt);
        DataTable dt1 = new DataTable();//New Table Created Which Contains Static Column
        dt1.Columns.Add("Bill_Date", System.Type.GetType("System.String"));
        dt1.Columns.Add("Ref_No", System.Type.GetType("System.String"));
        dt1.Columns.Add("Ref_Type", System.Type.GetType("System.String"));
        dt1.Columns.Add("Voucher_Date", System.Type.GetType("System.String"));
        dt1.Columns.Add("Voucher_Type", System.Type.GetType("System.String"));
        dt1.Columns.Add("Date", System.Type.GetType("System.String"));
        dt1.Columns.Add("Voucher_Name", System.Type.GetType("System.String"));
        dt1.Columns.Add("Voucher_No", System.Type.GetType("System.String"));
        dt1.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
        dt1.Columns.Add("Opening_Amount", System.Type.GetType("System.Decimal"));
        dt1.Columns.Add("Pending_Amount", System.Type.GetType("System.Decimal"));
        dt1.Columns.Add("Due_Date", System.Type.GetType("System.String"));
        dt1.Columns.Add("OverDueDays", System.Type.GetType("System.Int32"));
        //dt1.Columns.Add("Ref_Type_ID", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("Voucher_ID", System.Type.GetType("System.Int32"));

        dg_Ageing_Outstanding_Grid.Width = Unit.Percentage(98);
        // Below Codding Add the Columns According to the Age ranges Which is Specified in The DataSet  Ds_Ageing

        DataSet DsAgeing = StateManager.GetState<DataSet>("AgeingGrid");
        if (DsAgeing != null)
        {
            for (int i = 0; i < DsAgeing.Tables["Table"].Rows.Count; i++)
            {
                //Add column to DataSet
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                ds.Tables["Table"].Columns.Add(DsAgeing.Tables["Table"].Rows[i]["From_Days"].ToString() + "-" + DsAgeing.Tables["Table"].Rows[i]["To_Days"].ToString());
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            }
            for (int i = 0; i < DsAgeing.Tables["Table"].Rows.Count; i++)
            {
                //Add new column
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                BoundColumn New_Column = new BoundColumn();
                New_Column.DataField = DsAgeing.Tables["Table"].Rows[i]["From_Days"].ToString() + "-" + DsAgeing.Tables["Table"].Rows[i]["To_Days"].ToString();
                New_Column.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                New_Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                New_Column.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                New_Column.FooterStyle.Wrap = true;
                New_Column.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------

                //Dynamically Increase the DataGrid Width
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                Unit GridWidth = dg_Ageing_Outstanding_Grid.Width;
                double Grid_width = GridWidth.Value;
                Grid_width = Grid_width + 10;
                dg_Ageing_Outstanding_Grid.Width = Unit.Percentage(Grid_width);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                if (i == 0)//for First Column Header text
                {
                    New_Column.HeaderText = " < " + DsAgeing.Tables["Table"].Rows[i]["To_Days"].ToString() + " Days";
                }
                else//for Middle Columns  Header text
                {
                    New_Column.HeaderText = DsAgeing.Tables["Table"].Rows[i]["From_Days"].ToString() + " To " + DsAgeing.Tables["Table"].Rows[i]["To_Days"].ToString() + " Days";
                }

                dg_Ageing_Outstanding_Grid.Columns.Add(New_Column);
            }
            if (DsAgeing.Tables["Table"].Rows.Count > 0)//for last Column
            {
                //Add column to DataSet
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                ds.Tables["Table"].Columns.Add(DsAgeing.Tables["Table"].Rows[DsAgeing.Tables["Table"].Rows.Count - 1]["To_Days"].ToString() + "-0");
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------

                //Add new column
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                BoundColumn New_Column = new BoundColumn();
                New_Column.DataField = DsAgeing.Tables["Table"].Rows[DsAgeing.Tables["Table"].Rows.Count - 1]["To_Days"].ToString() + "-0";
                New_Column.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                New_Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                New_Column.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                New_Column.FooterStyle.Wrap = true;
                New_Column.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                //Last Column Header text
                New_Column.HeaderText = "> " + DsAgeing.Tables["Table"].Rows[DsAgeing.Tables["Table"].Rows.Count - 1]["To_Days"].ToString() + " Days";
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------

                //Dynamically Increase the DataGrid Width
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                Unit GridWidth = dg_Ageing_Outstanding_Grid.Width;
                double Grid_width = GridWidth.Value;
                Grid_width = Grid_width + 10;
                dg_Ageing_Outstanding_Grid.Width = Unit.Percentage(Grid_width);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                dg_Ageing_Outstanding_Grid.Columns.Add(New_Column);
            }
        }
        dt1 = ds.Tables["Table"].Clone();
        dt1.TableName = "Table1";
        ds.Tables.Add(dt1);
        return ds;
    }

      public void DistributeValues()//This function Distributes the Value to each Dataset columns Where it needs
    {
        DataSet Ds = new DataSet();
        DataSet Ds_To_Bind = new DataSet();
        if ((Boolean)Session["IsCondensed"] == true)
        {
            DataTable dt = new DataTable();
            dt = SessionLedgerAgeingGrid.Tables[0].Copy();
            Ds.Tables.Add(dt);
        }
        else
        {
            DataTable dt = new DataTable();
            //dt = Session_Ledger_Ageing_OutStandings.Tables[1].Copy();
             dt = SessionLedgerAgeingGrid.Tables[0].Copy();
              Ds.Tables.Add(dt);
              Ds.Tables[0].TableName = "Table";
        }
        if (Ds.Tables["Table"].Columns.Count > 18)
        {

            for (int i = 0; i < Ds.Tables["Table"].Rows.Count; i++)
            {
                TimeSpan Days_Difference;
                DateTime Date;
                if (IsBillDate == true)
                {
                    Date = Convert.ToDateTime(Ds.Tables["Table"].Rows[i]["Bill_Date"].ToString());
                    Days_Difference = Date.Subtract(Convert.ToDateTime(System.DateTime.Now.ToShortDateString()));
                }
                else
                {
                    Date = Convert.ToDateTime(Ds.Tables["Table"].Rows[i]["Due_Date"].ToString());
                    Days_Difference = Date.Subtract(Convert.ToDateTime(System.DateTime.Now.ToShortDateString()));
                }

                decimal Amount = Convert.ToDecimal(Ds.Tables["Table"].Rows[i]["Pending_Amount"].ToString());
                for (int j = 20; j < Ds.Tables["Table"].Columns.Count; j++)
                {
                    int lower_Bound = Convert.ToInt32(Ds.Tables["Table"].Columns[j].ColumnName.Split('-').GetValue(0).ToString());
                    int Upper_Bound = Convert.ToInt32(Ds.Tables["Table"].Columns[j].ColumnName.Split('-').GetValue(1).ToString());
                    if (Days_Difference.Days <= 0)
                    {
                        int Days = Math.Abs(Days_Difference.Days);
                        if (Days >= lower_Bound && Days < Upper_Bound)
                        {
                            Ds.Tables["Table"].Rows[i][j] = Amount.ToString();
                        }
                        else
                        {
                            Ds.Tables["Table"].Rows[i][j] = "0.00";
                        }
                        if (Upper_Bound == 0)
                        {
                            if (Days >= lower_Bound)
                            {
                                Ds.Tables["Table"].Rows[i][j] = Amount.ToString();
                            }
                            else
                            {
                                Ds.Tables["Table"].Rows[i][j] = "0.00";
                            }
                        }
                    }
                    else
                    {
                        Ds.Tables["Table"].Rows[i][11] = Amount.ToString();
                        Ds.Tables["Table"].Rows[i][j] = "0.00";
                    }
                }
            }
        }
        if ((Boolean)Session["IsCondensed"] == true)
        {
            DataTable Dt = new DataTable();
            DataTable Dt1 = new DataTable();
            //Dt = Ds.Tables[0].Copy();
           // Dt.TableName = "Table";
           // Dt1 = Session_Ledger_Ageing_OutStandings.Tables[1].Copy();
            //Dt1.TableName = "Table1";
            Dt = Ds.Tables[0].Copy();
            Dt.TableName = "Table";
            Dt1 = SessionLedgerAgeingGrid.Tables[0].Copy();
            Dt1.TableName = "Table1";
            Ds_To_Bind.Tables.Add(Dt);
            Ds_To_Bind.Tables.Add(Dt1);


        }
        else
        {
            DataTable Dt = new DataTable();
            
            //Dt = Session_Ledger_Ageing_OutStandings.Tables[0].Copy();
            Dt = Ds.Tables[0].Copy();
            Dt.TableName = "Table";
            Ds_To_Bind.Tables.Add(Dt);
        }
       // Session_Ledger_Ageing_OutStandings = Ds_To_Bind;
        SessionLedgerAgeingGrid = Ds_To_Bind;
    }


    public string Add_DRCR(decimal Amount)//Add Dr -Cr to Amount
    {
        if (Amount < 0)
        {
            return Convert.ToString(Math.Abs(Amount)) + " Dr";
        }
        else
        {
            return Convert.ToString(Math.Abs(Amount)) + " Cr";
        }
    }
    public void FooterTotalAmount()//Calculate Total Amount for Each Dynamic Columns
        {
        DataSet ds = new DataSet();
        if ((Boolean)Session["IsCondensed"] == true)
        {
            DataTable dt = new DataTable();
            dt = SessionLedgerAgeingGrid.Tables[0].Copy();
            ds.Tables.Add(dt);
        }
        else
        {
            DataTable dt = new DataTable();
            //dt = Session_Ledger_Ageing_OutStandings.Tables[1].Copy();
            dt = SessionLedgerAgeingGrid.Tables[0].Copy();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "Table";
        }
        if (dg_Ageing_Outstanding_Grid.Columns.Count > 17)
        {
            for (int j = 20; j < dg_Ageing_Outstanding_Grid.Columns.Count; j++)
            {
                dg_Ageing_Outstanding_Grid.Columns[j].FooterStyle.Wrap = true;
                dg_Ageing_Outstanding_Grid.Columns[j].FooterStyle.Font.Bold = true;
                dg_Ageing_Outstanding_Grid.Columns[j].FooterStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#993300");
                for (int i = 0; i < ds.Tables["Table"].Rows.Count; i++)
                {
                    Footer_Amount = Footer_Amount + Convert.ToDecimal(ds.Tables["Table"].Rows[i][j].ToString());
                }
                if (Footer_Amount == 0)
                {
                    dg_Ageing_Outstanding_Grid.Columns[j].FooterText = "";
                }
                else
                {
                    dg_Ageing_Outstanding_Grid.Columns[j].FooterText = Add_DRCR(Footer_Amount);
                }
                Footer_Amount = 0;
            }        
            BindLedgerAgeing = ds.Tables[0];
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

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {

        Is_Consol = Convert.ToBoolean(Request.QueryString["Is_Consol"]);
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        IsCondensed = Convert.ToBoolean(Request.QueryString["IsCondensed"]);
        Session["IsCondensed"] = IsCondensed;

        WucStartEndDate.OnDateChange += new EventHandler(FillOnDateChange);

        if (!IsPostBack)
        {
            StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
            EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
        }

        // if (Session["StartDate"] == null)
        //{
        //    StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
        //}
        //else
        //{
        //    StartDate = StateManager.GetState<DateTime>("StartDate");
        //}
        //        if (Session["EndDate"] == null)
        //{
        //    EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
        //}
        //else
        //{
        //    EndDate = StateManager.GetState<DateTime>("EndDate");
        //}
      
        objLedgerAgeingPresenter =new LedgerAgeingPresenter(this,IsPostBack);


        if (!IsPostBack)
        {
           lbl_Ledger_Name.Text = LedgerName;


           if ((Boolean)Session["IsCondensed"] == true)
           {
               btn_Details.Text = "View Detailed";
           }
           else
           {
               btn_Details.Text = "View Condensed";
           }
            
           
        }
        //-----------------------------------------------------------------------------------------------------------------------------------
        //Please Do not Change Below Sequence
        //-----------------------------------------------------------------------------------------------------------------------------------

        DistributeValues();
        BindLedgerAgeing = SessionLedgerAgeingGrid.Tables["Table"];
        FooterTotalAmount();
        //-----------------------------------------------------------------------------------------------------------------------------------

    }

    #endregion

    #region GridEvents

    protected void dg_Ageing_Outstanding_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        DataSet ds = new DataSet();

        if ((Boolean)Session["IsCondensed"] == true)
        {
            DataTable dt = new DataTable();
            dt = SessionLedgerAgeingGrid.Tables[0].Copy();
            ds.Tables.Add(dt);

        }
        else
        {
           DataTable dt = new DataTable();
           dt = SessionLedgerAgeingGrid.Tables[0].Copy();
           ds.Tables.Add(dt);
           dg_Ageing_Outstanding_Grid.Columns[8].Visible = true;
           dg_Ageing_Outstanding_Grid.Columns[9].Visible = true;
           dg_Ageing_Outstanding_Grid.Columns[10].Visible = true;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            if (ds.Tables["Table"].Rows.Count > 0)
            {
                Display_Opening_Pending_Amounts(e, ds);              
            }
           
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total_Opening_Amount = (Label)e.Item.FindControl("lbl_Total_Opening_Amount");
            Label lbl_Total_Pending_Amount = (Label)e.Item.FindControl("lbl_Total_Pending_Amount");
            if (_Total_Opening_Amount < 0)
            {
                lbl_Total_Opening_Amount.Text = +Math.Abs(_Total_Opening_Amount) + " Dr";
            }
            else if (_Total_Opening_Amount > 0)
            {
                lbl_Total_Opening_Amount.Text = Math.Abs(_Total_Opening_Amount) + " Cr";
            }
            else
            {
                lbl_Total_Opening_Amount.Text = "0.000";
            }
            if (_Total_Pending_Amount < 0)
            {
                lbl_Total_Pending_Amount.Text = Math.Abs(_Total_Pending_Amount) + " Dr";
            }
            else if (_Total_Pending_Amount > 0)
            {
                lbl_Total_Pending_Amount.Text = Math.Abs(_Total_Pending_Amount) + " Cr";
            }
            else
            {
                lbl_Total_Pending_Amount.Text = "0.000";
            }
        }   
    }

    private void Display_Opening_Pending_Amounts(DataGridItemEventArgs e, DataSet ds)
    {
        if (Convert.ToDecimal(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Opening_Amount"]) < 0)
            e.Item.Cells[13].Text = Math.Abs(Util.String2Decimal(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Opening_Amount"].ToString())) + " Dr";
        else if (Convert.ToDecimal(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Opening_Amount"]) > 0)
            e.Item.Cells[13].Text = ds.Tables["Table"].Rows[e.Item.ItemIndex]["Opening_Amount"].ToString() + " Cr";
        else
            e.Item.Cells[13].Text = "";
        if (Convert.ToDecimal(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Pending_Amount"]) < 0)
            e.Item.Cells[14].Text = Math.Abs(Util.String2Decimal(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Pending_Amount"].ToString())) + " Dr";
        else if (Convert.ToDecimal(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Pending_Amount"]) > 0)
            e.Item.Cells[14].Text = ds.Tables["Table"].Rows[e.Item.ItemIndex]["Pending_Amount"].ToString() + " Cr";
        else
            e.Item.Cells[14].Text = "";
        if (ds.Tables["Table"].Rows[e.Item.ItemIndex]["Amount"].ToString().Equals("0.000"))
        {
            e.Item.Cells[12].Text = "";
        }
        if (Convert.ToDateTime(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Bill_Date"].ToString()).Equals(Convert.ToDateTime("1900-01-01")))
        {
            e.Item.Cells[6].Text = "";
        }
        if (Convert.ToDateTime(ds.Tables["Table"].Rows[e.Item.ItemIndex]["Due_Date"].ToString()).Equals(Convert.ToDateTime("1900-01-01")))
        {
            e.Item.Cells[15].Text = "";
        }

       // Dynamic columns Value
        if (ds.Tables["Table"].Columns.Count > 16)
        {
            for (int j = 20; j < ds.Tables["Table"].Columns.Count; j++)
            {

                if (Convert.ToDecimal(ds.Tables["Table"].Rows[e.Item.ItemIndex][j].ToString()) < 0)
                {
                     e.Item.Cells[j].Text = Math.Abs(Util.String2Decimal(ds.Tables["Table"].Rows[e.Item.ItemIndex][j].ToString())) + " Dr";
                }
                else if (Convert.ToDecimal(ds.Tables["Table"].Rows[e.Item.ItemIndex][j].ToString()) == 0)
                {
                     e.Item.Cells[j].Text = "";
                }
                else
                {
                     e.Item.Cells[j].Text = Math.Abs(Util.String2Decimal(ds.Tables["Table"].Rows[e.Item.ItemIndex][j].ToString())) + " Cr";

                }

            }

        }
        dg_Ageing_Outstanding_Grid.Columns[8].ItemStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#993300");
        dg_Ageing_Outstanding_Grid.Columns[9].ItemStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#993300");
        dg_Ageing_Outstanding_Grid.Columns[10].ItemStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#993300");
        dg_Ageing_Outstanding_Grid.Columns[11].ItemStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#993300");

        _Total_Opening_Amount = Convert.ToDecimal(ds.Tables["Table"].Compute("Sum(Opening_Amount)", "").ToString());
        _Total_Pending_Amount = Convert.ToDecimal(ds.Tables["Table"].Compute("Sum(Pending_Amount)", "").ToString());
    }

    protected void btn_Details_Click(object sender, EventArgs e)
    {
        dg_Ageing_Outstanding_Grid.CurrentPageIndex = 0;

        if ((Boolean)Session["IsCondensed"] == true)
        {
            //IsCondensed = false;
            ////Btn_Details.Text = "Condensed"
            //btn_Details.Text = "View Condensed";
            //-----------------------------------------------------------------------------------------------------------------------------------
            //Please Do not Change Below Sequence
            //-----------------------------------------------------------------------------------------------------------------------------------

            //DistributeValues();
            //BindLedgerAgeing = SessionLedgerAgeingGrid.Tables["Table"];
            //FooterTotalAmount();
           // -----------------------------------------------------------------------------------------------------------------------------------

            //btn_Detailed.Text = "View Condensed";
            //Session["IsCondensed"] = false;           
            Response.Redirect("FrmLedgerAgeing.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&IsBillDate=" + IsBillDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(0));

        }
        else
        {
            IsCondensed = false;
            //-----------------------------------------------------------------------------------------------------------------------------------
            //Please Do not Change Below Sequence
            //-----------------------------------------------------------------------------------------------------------------------------------

            //DistributeValues();
            //BindLedgerAgeing = SessionLedgerAgeingGrid.Tables["Table"];
            //FooterTotalAmount();
            //-----------------------------------------------------------------------------------------------------------------------------------

            //btn_Detailed.Text = "";
            ////btn_Detailed.Text = "View Detailed";
            //Session["IsCondensed"] = true;
            Response.Redirect("FrmLedgerAgeing.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&IsBillDate=" + IsBillDate + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(1));

        }
        //if (Details.Equals("false"))
        //{
        //    Details = "true";
        //    Btn_Details.Text = "Condensed";
        //    //-----------------------------------------------------------------------------------------------------------------------------------
        //    //Please Do not Change Below Sequence
        //    //-----------------------------------------------------------------------------------------------------------------------------------       
        //    Distribute_Values();
        //    Bind_Ledger_Ageing_OutStandings = Session_Ledger_Ageing_OutStandings.Tables["Table1"];
        //    Footer_Total_Amount();
        //    //-----------------------------------------------------------------------------------------------------------------------------------
        //}
        //else
        //{
        //    Details = "false";
        //    Btn_Details.Text = "Detailed";
        //    //-----------------------------------------------------------------------------------------------------------------------------------
        //    //Please Do not Change Below Sequence
        //    //-----------------------------------------------------------------------------------------------------------------------------------       
        //    Distribute_Values();
        //    Bind_Ledger_Ageing_OutStandings = Session_Ledger_Ageing_OutStandings.Tables["Table"];
        //    Footer_Total_Amount();
        //    //-----------------------------------------------------------------------------------------------------------------------------------
        //}

    }


   
    #endregion

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objLedgerAgeingPresenter.initValues();
    }


}
