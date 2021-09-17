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
using System.Text;

public partial class Finance_Reports_WucTrialBalanceLedger : System.Web.UI.UserControl, ITrialBalanceLedgerView
{
    #region ClassVariable
    DataSet ds = null;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    public int Menu_Item_Id;
    public string _CompanyName = UserManager.getUserParam().CompanyName;
    TrialBalanceLedgerPresenter objTrialBalanceLedgerPresenter;
    #endregion

    #region ControlsValue

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

    public DataSet TB_DS
    {
        get { return StateManager.GetState<DataSet>("TB_DS"); }
        set
        {
            StateManager.SaveState("TB_DS", value);
            BindTrialBalanceGrid = value;
        }
    }

    public int DivisionId
    {
        get { return Convert.ToInt32(Request.QueryString["Division_Id"]); }
    }

    #endregion

    #region ControlsBind
    public DataSet BindTrialBalanceGrid
    {
        set
        {
            //Session["TB_DS"] = value;            
            dgTrialBalance.DataSource = value;
            dgTrialBalance.DataBind();

        }
    }
    #endregion

    #region IView Members

    public string errorMessage
    {
        set { lbl_Error.Text = value; }
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

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        Menu_Item_Id = Common.GetMenuItemId();

        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);
        int MenuItemId = Common.GetMenuItemId();

        if (!IsPostBack)
        {
            lbl_Company_Name.Text = _CompanyName;

            if (!IsPostBack)
            {
                StartDate = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["StartDate"]));
                EndDate = Convert.ToDateTime(Util.DecryptToString(Request.QueryString["EndDate"]));
                WucGridSearch1.SetComSepColumnName = "Particulars=Name,Opening Bal Debit=OpeningDr,Opening Bal Credit=OpeningCr,Current Bal Debit=CurrentDr,Current Bal Credit=CurrentCr,Closing Bal Debit=ClosingDr,Closing Bal Credit=ClosingCr";
            }
        }

        objTrialBalanceLedgerPresenter = new TrialBalanceLedgerPresenter(this, IsPostBack);
        WucGridSearch1.SetDataGrid = dgTrialBalance;

        if (Session["TB_DS"] != null)
        {
            ds.Tables.Add(TB_DS.Tables[0].Copy());
            WucGridSearch1.SetDataSet = ds;
        }

        WucGridSearch1.btnChangedPeriod = new EventHandler(FillOnDateChange);
        HttpContext.Current.Session["TBC_DS"] = TB_DS;
        HttpContext.Current.Session["St_Dt"] = StartDate.ToString("dd-MMM-yyyy");
        HttpContext.Current.Session["End_Dt"] = EndDate.ToString("dd-MMM-yyyy");
        Session.Add("FIN_DS", TB_DS);
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Reports/Direct_Printing/Frm_FinancePrintingViewer.aspx?Menu_Item_Id=" + Menu_Item_Id + "&Start_Date=" + Util.EncryptString(StartDate.ToString()) + "&End_Date=" + Util.EncryptString(EndDate.ToString()));
        Btn_Preview.Attributes.Add("onclick", "return Open_Show_Window('" + Path + "')");
    }
    //--Function For Footer Total Start

    public decimal totopDR, totopCR, totcrntDR, totcrntCR, totclDR, totclCR;

    //OPENING DR
    public decimal FootTotal_OP_DR()
    {
        if (TB_DS.Tables[1].Rows.Count > 0)
        {
            return totopDR = Convert.ToDecimal(TB_DS.Tables[1].Rows[0]["Tot_OP_DR"].ToString());
        }
        else
        {
            return 0;
        }
    }
    //OPENING CR
    public decimal FootTotal_OP_CR()
    {
        if (TB_DS.Tables[1].Rows.Count > 0)
        {
            return totopCR = Convert.ToDecimal(TB_DS.Tables[1].Rows[0]["Tot_OP_CR"].ToString());
        }
        else
        {
            return 0;
        }
    }
    //CURRENT DR
    public decimal FootTotal_CRNT_DR()
    {

        if (TB_DS.Tables[1].Rows.Count > 0)
        {
            return totcrntDR = Convert.ToDecimal(TB_DS.Tables[1].Rows[0]["Tot_CRNT_DR"].ToString());
        }
        else
        {
            return 0;
        }
    }
    //CURRNT CR
    public decimal FootTotal_CRNT_CR()
    {

        if (TB_DS.Tables[1].Rows.Count > 0)
        {
            return totcrntCR = Convert.ToDecimal(TB_DS.Tables[1].Rows[0]["Tot_CRNT_CR"].ToString());
        }
        else
        {
            return 0;
        }
    }
    //CLOSING DR
    public decimal FootTotal_CL_DR()
    {

        if (TB_DS.Tables[1].Rows.Count > 0)
        {
            return totclDR = Convert.ToDecimal(TB_DS.Tables[1].Rows[0]["Tot_CL_DR"].ToString());
        }
        else
        {
            return 0;
        }
    }
    //CLOSING CR
    public decimal FootTotal_CL_CR()
    {

        if (TB_DS.Tables[1].Rows.Count > 0)
        {
            return totclCR = Convert.ToDecimal(TB_DS.Tables[1].Rows[0]["Tot_CL_CR"].ToString());
        }
        else
        {
            return 0;
        }
    }
    //--Function Footer Total End

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objTrialBalanceLedgerPresenter.initValues();
    }

    #endregion

    #region GridEvents
    protected void dgTrialBalance_RowDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER'");
            if (e.Item.ItemType == ListItemType.Item)
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");
            else if (e.Item.ItemType == ListItemType.AlternatingItem)
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");

            if (e.Item.Cells[0].Text == "0")
            {
                e.Item.Font.Italic = true;
                e.Item.Cells[1].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Item.Font.Bold = false;
                e.Item.Font.Italic = false;
            }
        }
    }

    protected void dgTrialBalance_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgTrialBalance.CurrentPageIndex = e.NewPageIndex;
        BindTrialBalanceGrid = TB_DS; //StateManager.GetState<DataSet>("TB_DS");
    }

    #endregion

//    protected void btn_Export_To_Excel_Click(object sender, EventArgs e)
//    {
//        DataSet dsExport = new DataSet();
//        dsExport = TB_DS.Copy();
//        if (dsExport.Tables[0].Columns[1].ColumnName == "Id")
//        {
//            dsExport.Tables[0].Columns.Remove("Id");
//            dsExport.Tables[0].AcceptChanges();
//        }

//        if (dsExport.Tables[0].Columns[0].ColumnName == "Sr_No")
//        {
//            dsExport.Tables[0].Columns.Remove("Sr_No");
//            dsExport.Tables[0].AcceptChanges();
//        }

//        //ADD GRAND TOTAL
//        DataRow dr = dsExport.Tables[0].NewRow();
//        if (dsExport.Tables[1].Rows.Count != 0)
//        {            
//            dr["Name"] = "TOTAL : ";
//            dr["OpeningDr"] = Convert.ToDecimal(dsExport.Tables[1].Rows[0]["Tot_OP_DR"].ToString());
//            dr["OpeningCr"] = Convert.ToDecimal(dsExport.Tables[1].Rows[0]["Tot_OP_CR"].ToString());
//            dr["CurrentDr"] = Convert.ToDecimal(dsExport.Tables[1].Rows[0]["Tot_CRNT_DR"].ToString());
//            dr["CurrentCr"] = Convert.ToDecimal(dsExport.Tables[1].Rows[0]["Tot_CRNT_CR"].ToString());
//            dr["ClosingDr"] = Convert.ToDecimal(dsExport.Tables[1].Rows[0]["Tot_CL_DR"].ToString());
//            dr["ClosingCr"] = Convert.ToDecimal(dsExport.Tables[1].Rows[0]["Tot_CL_CR"].ToString());

//            dsExport.Tables[0].Rows.Add(dr);

//            //DataRow dr1 = dt.NewRow();
//            //dr1["Name"] = CompanyManager.getCompanyParam().CompanyName;
//            //dt.Rows.InsertAt(dr1, 0);  
//        }
//        //GridView GV = new GridView();
//        //GV.DataSource = dsExport;
//        //GV.DataBind();

//        //string filename = "Trial Balance Ledger";

//        //StringWriter stringWrite = new StringWriter();
//        //HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
//        //GV.RenderControl(htmlWrite);
//        //Response.Clear();
//        //Response.Charset = "";
//        //Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + " - " + lbl_Heading.Text + ".xls");

//        //Response.ContentEncoding = System.Text.Encoding.UTF7;
//        //Response.ContentType = "application/vnd.ms-excel";
//        //Response.Write(stringWrite.ToString());
//        //Response.End();

//        Session["ExportToExcel"] = dsExport.Tables[0];
//        //Response.Redirect("~/Finance/Utilities/frm_Common_ExportToExcelGrid.aspx?filename=Trial Balance (Ledger)");
//        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
//    }
}
