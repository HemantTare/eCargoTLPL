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
using System.IO;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Raj.EC.Printers;
using System.Text;

public partial class FA_Common_Reports_wuc_Bank_Reco_Statement : System.Web.UI.UserControl, IBankRecoStatementView
{
    private BankRecoStatementPresenter _obj_BankRecoStatementPresenter;
    public int _rowNo = 1;
    private DateTime maxVoucherDate;
    private String HierarchyCode;
    private int MainId;



    public DataTable SetLables
    {
        set
        {

            DataRow Dr = value.Rows[0];

            lbl_AddAmmountNotReflectedInBank.Text = Convert.ToDecimal(Dr["Initial_AmountNotRefBank_cr"]).ToString("0.000") + " Cr";
            lbl_LessAmmountNotReflectedInBank.Text = Convert.ToDecimal(Dr["Initial_AmountNotRefBank_dr"]).ToString("0.000") + " Dr";

            if (Convert.ToDecimal(Dr["Initial_BalAsBank_dr"]) != 0)
            {
                lbl_BalanceAsPerBank.Text = Convert.ToDecimal(Dr["Initial_BalAsBank_dr"]).ToString("0.000") + " Dr";
            }
            else if (Convert.ToDecimal(Dr["Initial_BalAsBank_cr"]) != 0)
            {
                lbl_BalanceAsPerBank.Text = Convert.ToDecimal(Dr["Initial_BalAsBank_cr"]).ToString("0.000") + " Cr";
            }
            else
            {
                lbl_BalanceAsPerBank.Text = Convert.ToDecimal(Dr["Initial_BalAsBank_cr"]).ToString("0.000");
            }


            decimal _balAsComp = Convert.ToDecimal(Dr["Closing_Balance"]);

            if (_balAsComp >= 0)
            {
                lbl_BalanceAsPerCompany.Text = Math.Abs(_balAsComp).ToString("0.000") + " Cr";
            }
            else
            {
                lbl_BalanceAsPerCompany.Text = Math.Abs(_balAsComp).ToString("0.000") + " Dr";
            }

            maxVoucherDate = Convert.ToDateTime(Dr["Voucher_Date"]);
        }

    }



    public bool Is_Consol
    {
        get { return Convert.ToBoolean(Request.QueryString["Is_Consol"]); }
    }

    public bool Is_Uncleared
    {
        get { return  false; }
    }


    public DateTime End_Date
    {
        get
        {
            return WucStartEndDate1.End_Date;

        }
        set { WucStartEndDate1.End_Date = value; }
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


    public DateTime Start_Date
    {
        get
        {
            return WucStartEndDate1.Start_Date;
        }
        set
        {
            WucStartEndDate1.Start_Date = value;
        }

    }



    public DataTable bind_dg_AddBankRecoStatement
    {
        set
        {
            dg_AddBankRecoStatement.DataSource = value;
            dg_AddBankRecoStatement.DataBind();
        }
    }

    public DataTable bind_dg_LessBankRecoStatement
    {
        set
        {
            dg_LessBankRecoStatement.DataSource = value;
            dg_LessBankRecoStatement.DataBind();
        }
    }
    public DataTable Session_Dt_AddBankRecoStatement
    {
        set { StateManager.SaveState("AddBankRecoStatement", value); }
        get { return StateManager.GetState<DataTable>("AddBankRecoStatement"); }
    }
    public DataTable Session_Dt_LessBankRecoStatement
    {
        set { StateManager.SaveState("LessBankRecoStatement", value); }
        get { return StateManager.GetState<DataTable>("LessBankRecoStatement"); }
    }

    private void DefaultSetting()
    {
        if (!IsPostBack)
        {
            lbl_LedgerName.Text = "Ledger: " + Request.QueryString["Name"];
        }
    }

    public string errorMessage { set { ;} }
    public int keyID
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["Id"]);
        }
    }

    public bool validateUI()
    {
        return true;
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);

        Start_Date = Convert.ToDateTime(Request.QueryString["StartDate"]);
        End_Date = Convert.ToDateTime(Request.QueryString["EndDate"]);
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        if (!IsPostBack)
        {
            WucGridSearch1.SetComSepColumnName = "Date=Cheque_Date,Particulars,Voucher_Type,Credit";
            WucGridSearch2.SetComSepColumnName = "Date=Cheque_Date,Particulars,Voucher_Type,Debit";
            WucGridSearch1.VisibleChangePeriod = false;
            WucGridSearch2.VisibleChangePeriod = false;
        }
        WucGridSearch1.SetDataGrid = dg_AddBankRecoStatement;
        WucGridSearch2.SetDataGrid = dg_LessBankRecoStatement;
        _obj_BankRecoStatementPresenter = new BankRecoStatementPresenter(this, IsPostBack);

        DefaultSetting();
        if (Session["AddBankRecoStatement"] != null)
        {
            ds.Tables.Add(Session_Dt_AddBankRecoStatement.Copy());
            WucGridSearch1.SetDataSet = ds;
        }
        if (Session["LessBankRecoStatement"] != null)
        {
            ds1.Tables.Add(Session_Dt_LessBankRecoStatement.Copy());
            WucGridSearch2.SetDataSet = ds1;
        }
        WucGridSearch1.btnChangedPeriod = new EventHandler(_obj_BankRecoStatementPresenter.initControl);

    }


    //protected void dg_BankRecoStatement_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    _UpdateDataTable();
    //    dg_BankRecoStatement.CurrentPageIndex = e.NewPageIndex;
    //    bind_dg_BankRecoStatement = Dt_BankRecoStatement;
    //}


    protected void btn_save_Click(object sender, EventArgs e)
    {
        string CloseScript = "<script language='javascript'> " + "window.close();" + "</script>";
        // ScriptManager.RegisterStartupScript(upnl_Error, typeof(string), "CloseScript", CloseScript, false);
    }
    protected void dg_AddBankRecoStatement_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_AddBankRecoStatement.CurrentPageIndex = e.NewPageIndex;
        bind_dg_AddBankRecoStatement = Session_Dt_AddBankRecoStatement;
    }
    protected void btn_ExportToExcel_Click(object sender, EventArgs e)
    {

        DataTable Dt = new DataTable();

        if (Session["AddBankRecoStatement"] != null)
        {
            Dt = Session_Dt_AddBankRecoStatement.Copy();
            Dt.Columns.Remove("SrNo");
            Dt.Columns.Remove("Cheque_No");
            Dt.Columns.Remove("Is_Select");
            Dt.Columns.Remove("Is_Select1");
            Dt.Columns.Remove("Voucher_Id");
            Dt.Columns.Remove("Voucher_Date");
            Dt.Columns.Remove("Bank_Date1");
            Dt.Columns.Remove("Bank_Date");
        }

        //Session["BankStatementExportToExcel"] = Dt;
        Session["ExportToExcel"] = Dt;

        //Response.Redirect("~/Finance/Utilities/FrmBankStatementExportToExcel.aspx");
        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }

    protected void btn_ExportToExcel1_Click(object sender, EventArgs e)
    {
        DataTable Dt = new DataTable();

        if (Session["LessBankRecoStatement"] != null)
        {
            Dt = Session_Dt_LessBankRecoStatement.Copy();
            Dt.Columns.Remove("SrNo");
            Dt.Columns.Remove("Cheque_No");
            Dt.Columns.Remove("Is_Select");
            Dt.Columns.Remove("Is_Select1");
            Dt.Columns.Remove("Voucher_Id");
            Dt.Columns.Remove("Voucher_Date");
            Dt.Columns.Remove("Bank_Date1");
            Dt.Columns.Remove("Bank_Date");
        }

        //Session["BankStatementExportToExcel"] = Dt;
        Session["ExportToExcel"] = Dt;


        //Response.Redirect("~/Finance/Utilities/FrmBankStatementExportToExcel.aspx");
        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }

    protected void dg_LessBankRecoStatement_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_LessBankRecoStatement.CurrentPageIndex = e.NewPageIndex;
        bind_dg_LessBankRecoStatement = Session_Dt_LessBankRecoStatement;
    }


    private void BindCrystalReport()
    {
        DataSet DS = new DataSet();
        DataTable Dt = new DataTable("CR");
        DataTable Dt1 = new DataTable("DR");
        DataTable Dt2 = new DataTable("Other");

        
        Dt = Session_Dt_AddBankRecoStatement.Copy();
        Dt.Columns.Remove("SrNo");
        //Dt.Columns.Remove("Cheque_No");
        Dt.Columns.Remove("Is_Select");
        Dt.Columns.Remove("Is_Select1");
        Dt.Columns.Remove("Voucher_Id");
        Dt.Columns.Remove("Voucher_Date");
        Dt.Columns.Remove("Bank_Date1");
        //Dt.Columns.Remove("Bank_Date");

        Dt.TableName = "CR";

        Dt1 = Session_Dt_LessBankRecoStatement.Copy();
        Dt1.Columns.Remove("SrNo");
        //Dt1.Columns.Remove("Cheque_No");
        Dt1.Columns.Remove("Is_Select");
        Dt1.Columns.Remove("Is_Select1");
        Dt1.Columns.Remove("Voucher_Id");
        Dt1.Columns.Remove("Voucher_Date");
        Dt1.Columns.Remove("Bank_Date1");
        //Dt1.Columns.Remove("Bank_Date");

        Dt1.TableName = "DR";

        DataRow DR;

        Dt2.Columns.Add("BankName");
        Dt2.Columns.Add("Period");
        Dt2.Columns.Add("BalanceAsPerCompany");
        Dt2.Columns.Add("BalanceAsPerBank");
        
        DR = Dt2.NewRow();


        DR["BankName"] = lbl_LedgerName.Text;
        DR["Period"] = Start_Date.ToString("dd-MMM-yyyy") + " To " + End_Date.ToString("dd-MMM-yyyy");
        DR["BalanceAsPerCompany"] = lbl_BalanceAsPerCompany.Text;
        DR["BalanceAsPerBank"] = lbl_BalanceAsPerBank.Text;


        Dt2.Rows.Add(DR);
       
        Dt2.TableName = "Other";

        DS.Tables.Add(Dt);
        DS.Tables.Add(Dt1);
        DS.Tables.Add(Dt2);


        Session["FIN_DS"] = DS;

        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Reports/Direct_Printing/Frm_BankRecoStatement.aspx");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Open_Show_Window('" + Path + "')", true);

        //DS.WriteXmlSchema("C:/BankReco.Xsd");
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        BindCrystalReport();
    }
}
