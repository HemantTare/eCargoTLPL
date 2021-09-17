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

public partial class Master_Accounting_Masters_wuc_Bank_Reco_Report : System.Web.UI.UserControl
{
    private DataTable Dt_BankReco=new DataTable();
    public int _rowNo = 1;
    public decimal Initial_AmountNotBank_dr, Initial_AmountNotBank_cr, Initial_BalAsBank_dr, Initial_BalAsBank_cr,BalanceAsPerCompBook;
    private DateTime maxVoucherDate;
    private String HierarchyCode;
    private int MainId;
    private string btn_text = "";
    private bool _uncleared = false;
    public DataTable SetVariables
    {
        set 
        {
            DataRow Dr = value.Rows[0];
            
            Initial_AmountNotBank_dr = Convert.ToDecimal(Dr["Initial_AmountNotRefBank_dr"]);
            Initial_AmountNotBank_cr = Convert.ToDecimal(Dr["Initial_AmountNotRefBank_cr"]);

            Initial_BalAsBank_dr = Convert.ToDecimal(Dr["Initial_BalAsBank_dr"]);
            Initial_BalAsBank_cr = Convert.ToDecimal(Dr["Initial_BalAsBank_cr"]);
            BalanceAsPerCompBook = Convert.ToDecimal(Dr["Closing_Balance"]);
            maxVoucherDate = Convert.ToDateTime(Dr["Voucher_Date"]);
        }
    }

   
    public DateTime End_Date 
    {
        get
        {
            //return Convert.ToDateTime(Param.getUserParam().End_Date);
            return Convert.ToDateTime(lbl_EndDate.Text);
            //Convert.ToDateTime(Request.QueryString["End_Date"]);
        
        }
        set { lbl_EndDate.Text = value.ToString("dd-MMM-yyyy"); }
    }
    public DateTime Start_Date
    {
        get
        {
            //return Convert.ToDateTime(Param.getUserParam().Start_Date);
            return Convert.ToDateTime(lbl_Start_Date.Text);
        }

        set { lbl_Start_Date.Text = value.ToString("dd-MMM-yyyy"); }
       
   }

    public DataTable bind_dg_BankReco
    {
        set
        {
            Session_Dt_BankReco = value;
            dg_BankReco.DataSource = value;
            dg_BankReco.DataBind();

        }
    }
 
    private void DefaultSetting()
    {
        lbl_Amt_Not_cleared_dr.Text = hdf_Updated_Amt_Not_cleared_dr.Value;
        lbl_Amt_Not_cleared_cr.Text = hdf_Updated_Amt_Not_cleared_cr.Value;

        lbl_Bal_As_Bank_dr.Text=hdf_Updated_BalAsBank_dr.Value;
        lbl_Bal_As_Bank_cr.Text=hdf_Updated_BalAsBank_cr.Value;
        
        if (!IsPostBack)
        {
            lbl_LedgerName.Text = "Ledger: " +Request.QueryString["Name"];
            //lbl_LedgerName.Text = Request.QueryString["Name"];

            lbl_Amt_Not_cleared_dr.Text = Initial_AmountNotBank_dr.ToString();
            lbl_Amt_Not_cleared_cr.Text = Initial_AmountNotBank_cr.ToString();

            //decimal _amount=Convert.ToDecimal(Request.QueryString["Amount"]);
            //decimal _amount = +10;
            if (BalanceAsPerCompBook < 0)
            {
                lbl_Bal_Comp_dr.Text = Convert.ToDecimal(Math.Abs(BalanceAsPerCompBook)).ToString("0.000");
                lbl_Bal_Comp_cr.Text = "0.000";
            }
            else 
            {
                lbl_Bal_Comp_cr.Text = Convert.ToDecimal(Math.Abs(BalanceAsPerCompBook)).ToString("0.000");
                lbl_Bal_Comp_dr.Text = "0.000";
            }

            lbl_Bal_As_Bank_dr.Text = Initial_BalAsBank_dr.ToString();
            lbl_Bal_As_Bank_cr.Text = Initial_BalAsBank_cr.ToString();
            
            hdf_Initial_BalAsBank_dr.Value = Initial_BalAsBank_dr.ToString();
            hdf_Initial_BalAsBank_cr.Value = Initial_BalAsBank_cr.ToString();

            hdf_Updated_BalAsBank_dr.Value = Initial_BalAsBank_dr.ToString();
            hdf_Updated_BalAsBank_cr.Value = Initial_BalAsBank_cr.ToString();

            hdf_Updated_Amt_Not_cleared_dr.Value = Initial_AmountNotBank_dr.ToString();
            hdf_Updated_Amt_Not_cleared_cr.Value = Initial_AmountNotBank_cr.ToString();
        }
    }
    private DataTable Session_Dt_BankReco
    {
        set { StateManager.SaveState("Dt_BankReco", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankReco"); }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["Dt_BankReco"] != null)
        {           
            bind_dg_BankReco = (DataTable)Session["Dt_BankReco"];
            SetVariables = (DataTable)Session["Dt_BankRecoTotal"];
        }
        DefaultSetting();
        Start_Date  =Convert.ToDateTime(Request.QueryString["StartDate"]);
        End_Date =Convert.ToDateTime(Request.QueryString["EndDate"]);            
     
        Export_To_Excel();
    }
    
    protected void dg_BankReco_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Convert.ToBoolean(Session_Dt_BankReco.Rows[e.Item.ItemIndex]["Is_Select"])==true)
            {
                Label lbl_Bank_Date = (Label)e.Item.FindControl("lbl_Bank_Date");
                lbl_Bank_Date.Text = Convert.ToDateTime(Session_Dt_BankReco.Rows[e.Item.ItemIndex]["Bank_Date"]).ToString("dd/MM/yyyy");
            }
        }            
    }
    public void Export_To_Excel()
    {
        dg_BankReco.AllowPaging = false;
        
        string filename = "BANK RECONCILATION.xls";
	Response.Clear(); 
	Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename)); 
	Response.ContentType = "application/ms-excel"; 

	//Response.Charset = ""; 
	//this.EnableViewState = false; 
	System.IO.StringWriter oStringWriter = new 
	System.IO.StringWriter(); 
	System.Web.UI.HtmlTextWriter oHtmlTextWriter = new 
	System.Web.UI.HtmlTextWriter(oStringWriter); 
	//this.ClearControls(dg_BankReco); 
	//oHtmlTextWriter = 
	oHtmlTextWriter.ToString().Replace("<th ", "<th autofilter=''''all''''"); 
	dg_BankReco.RenderControl(oHtmlTextWriter); 
	string s = oStringWriter.ToString(); 
	//s = header + s + "</HTML>"; 

	//s = s.Replace("<th ", "<th x:autofilter='all' "); 

	Response.Write(s); 
	Response.Flush(); 
	Response.End(); 

    }
}
