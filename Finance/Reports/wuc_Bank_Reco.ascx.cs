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
using Raj.FA.ReportsPresenter;
using Raj.FA.ReportsView;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;



public partial class Master_Accounting_Masters_wuc__Bank_Reco : System.Web.UI.UserControl, IBankRecoView
{
    private BankRecoPresenter _obj_BankRecoPresenter;
    private DataTable Dt_BankReco=new DataTable();
    public int _rowNo = 1;
    public decimal Initial_AmountNotBank_dr, Initial_AmountNotBank_cr, Initial_BalAsBank_dr, Initial_BalAsBank_cr,BalanceAsPerCompBook;
    private DateTime maxVoucherDate, _Start_Date,_End_Date;
    private String HierarchyCode;
    private int MainId;

    public DataTable SetVariables
    {
        set
        {
            Session_Total = value;
            DataRow Dr = value.Rows[0];
            
            Initial_AmountNotBank_dr = Convert.ToDecimal(Dr["Initial_AmountNotRefBank_dr"]);
            Initial_AmountNotBank_cr = Convert.ToDecimal(Dr["Initial_AmountNotRefBank_cr"]);

            Initial_BalAsBank_dr = Convert.ToDecimal(Dr["Initial_BalAsBank_dr"]);
            Initial_BalAsBank_cr = Convert.ToDecimal(Dr["Initial_BalAsBank_cr"]);
            BalanceAsPerCompBook = Convert.ToDecimal(Dr["Closing_Balance"]);
            maxVoucherDate = Convert.ToDateTime(Dr["Voucher_Date"]);
        }
    }

    private DataTable Session_Dt_BankReco
    {
        set { StateManager.SaveState("Dt_BankReco", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankReco"); }
    }

    private DataTable Session_Total
    {
        set { StateManager.SaveState("Dt_BankRecoTotal", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankRecoTotal"); }
    }


    public DataSet get_Ds_BankReco
    {
        get 
        {
           Dt_BankReco = Session_Dt_BankReco;
           _UpdateDataTable();
           DataSet Ds_Temp=new DataSet();
           DataTable Dt_Temp = new DataTable();

           Dt_Temp.Columns.Add("Bank_Date",Type.GetType("System.String"));
           Dt_Temp.Columns.Add("Voucher_ID",Type.GetType("System.Int32"));
           Dt_Temp.Columns.Add("Cheque_No", Type.GetType("System.String"));

           DataView Dv = new DataView(Dt_BankReco, "Is_Select = 1 OR Is_Select1 = 1", "Sr_No ASC", DataViewRowState.CurrentRows);

           DataTable Dt_Filtered = Dv.ToTable();
           int _count = Dt_Filtered.Rows.Count;
           for (int i = 0; i < _count; i++)
           {
               if (((bool)Dt_Filtered.Rows[i]["Is_Select"] != (bool)Dt_Filtered.Rows[i]["Is_Select1"]) || (Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date"]) != Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date1"])))
               { 
               DataRow Dr=  Dt_Temp.NewRow();
               Dr["Voucher_ID"] = Dt_Filtered.Rows[i]["Voucher_Id"].ToString();
               Dr["Cheque_No"] = Dt_Filtered.Rows[i]["Cheque_No"].ToString();
               //Dr["Details_Id"] = Dt_Filtered.Rows[i]["Details_Id"].ToString();

               if ((bool)Dt_Filtered.Rows[i]["Is_Select"] == true)
               {
                   Dr["Bank_Date"] = Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date"]).ToString("MM/dd/yyyy");
               }
               else 
               { 
                   Dr["Bank_Date"] = DBNull.Value; 
               }
               Dt_Temp.Rows.Add(Dr);

              }
           
           }

             

           Ds_Temp.Tables.Add(Dt_Temp.Copy());
           return Ds_Temp;
        }
    }

    public DateTime End_Date 
    {
        get
        {
            //return Convert.ToDateTime(Param.getUserParam().End_Date);
            return _End_Date;
            //Convert.ToDateTime(Request.QueryString["End_Date"]);
        
        }
        set { 
                lbl_EndDate.Text = value.ToString("dd-MMM-yyyy");
                _End_Date = value;
            }
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

    public bool Is_Consol
    {
        get { return Convert.ToBoolean(Request.QueryString["Is_Consol"]); }
    }

    public bool Is_Uncleared
    {
        get { return Request.QueryString["Is_Uncleared"] !=null ? Convert.ToBoolean(Request.QueryString["Is_Uncleared"]):true; }
    }


    public DateTime Start_Date
    {
        get
        {
            //return Convert.ToDateTime(Param.getUserParam().Start_Date);
            return _Start_Date;
        }
    set { 
            lbl_Start_Date.Text = value.ToString("dd-MMM-yyyy");
            _Start_Date = value;
        }
   }

  

    public  DataTable bind_dg_BankReco
    { 
        set
        {
            //----To Bind Filter View---

          if (!IsPostBack)
          {Session_Dt_BankReco = value;}

           Session_Dt_SearchBankReco = value;

           dg_BankReco.DataSource=value;
           dg_BankReco.DataBind();
          
        } 
    }
 
    private void DefaultSetting()
    {
        //lbl_Amt_Not_cleared_dr.Text = hdf_Updated_Amt_Not_cleared_dr.Value;
        //lbl_Amt_Not_cleared_cr.Text = hdf_Updated_Amt_Not_cleared_cr.Value;

        //lbl_Bal_As_Bank_dr.Text=hdf_Updated_BalAsBank_dr.Value;
        //lbl_Bal_As_Bank_cr.Text=hdf_Updated_BalAsBank_cr.Value;
        
        if (!IsPostBack)
        {
             lbl_LedgerName.Text = "Ledger: " + Request.QueryString["Name"].ToString();
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

    public  string errorMessage 
     { set  {lbl_Errors.Text = value; }}

    public int keyID
    { 
        get
        {
            return Convert.ToInt32(Request.QueryString["Id"]);
        } 
    }

    public bool validateUI()
    {
        ArrayList SrNoList = new ArrayList();

        DataView Dv = new DataView(Dt_BankReco, "Is_Select = 1", "Sr_No ASC", DataViewRowState.CurrentRows);
            
        DataTable Dt_Filtered = Dv.ToTable();
        int _count = Dt_Filtered.Rows.Count;

        for (int i = 0; i < _count; i++)
        {
            if (Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date"]) < Convert.ToDateTime(Dt_Filtered.Rows[i]["Voucher_Date"]))
            {
                SrNoList.Add(Dt_Filtered.Rows[i]["Sr_No"].ToString());    
            }
        }

        if (Dt_Filtered.Rows.Count == 0)
        { errorMessage = "Please Select Atleast One Voucher"; return false; }

        if (SrNoList.Count != 0)
        {
            System.Text.StringBuilder _errorMessage = new System.Text.StringBuilder("Bank Date Must Be Greater Than Voucher Date For Sr No:");
            for (int i = 0; i < SrNoList.Count; i++)
            { _errorMessage.Append("  " + SrNoList[i].ToString() + ","); }

            errorMessage = _errorMessage.Remove(_errorMessage.Length - 1, 1).ToString();

            return false;
        }        
        return true;
     }
  
    protected void Page_Load(object sender, EventArgs e)
    {

        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);

        if (Is_Uncleared)
        {
            btn_ShowReconcil.Text = "Show All Vouchers";
        }
        else
        {
            btn_ShowReconcil.Text = "Show Uncleared Vouchers";
        }

       // Response.Write(Hierarchy_Code + "," + Main_Id.ToString() + "," + keyID.ToString());
        if (!IsPostBack)
        {_nullSession();}

        if (Session["StartDate"] != null)
        {
            End_Date = StateManager.GetState<DateTime>("EndDate");
            Start_Date = StateManager.GetState<DateTime>("StartDate");
        }
        else
        {
            Start_Date = Convert.ToDateTime(Request.QueryString["StartDate"]);
            End_Date = Convert.ToDateTime(Request.QueryString["EndDate"]);
        }


        _obj_BankRecoPresenter = new BankRecoPresenter(this,IsPostBack);
        DefaultSetting();

        

        //if (Is_Uncleared==false)
        //{ SetAmountNotCleared_BalanceAsBank(); }


        //_Ledger_Voucher_ReportsPresenter = new Ledger_Voucher_ReportsPresenter(this, IsPostBack);
    }
    
    protected void dg_BankReco_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chk_Select = (CheckBox)e.Item.FindControl("chk_Select");
            //chk_Select.Attributes.Add("onclick", "SetAmountNotCleared(this," + e.Item.Cells[10].Text + "," + e.Item.Cells[11].Text + ",'I')");
            chk_Select.Attributes.Add("onclick", "CallRefreshClick()");

            Label lbl_Voucher_Date = (Label)e.Item.FindControl("lbl_Voucher_Date");
            Label lbl_Voucher_No = (Label)e.Item.FindControl("lbl_Voucher_No");

            if (lbl_Voucher_No.Text == "DUMMY")
            { lbl_Voucher_No.Text = ""; }

            if (lbl_Voucher_Date.Text == "01/01/1900")
            { lbl_Voucher_Date.Text = ""; }

        }
    }

    protected void dg_BankReco_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        _UpdateDataTable();
         dg_BankReco.CurrentPageIndex = e.NewPageIndex;
         bind_dg_BankReco = Session_Dt_SearchBankReco;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        _UpdateDataTable();
         //string s = get_Ds_BankReco.GetXml();
        if (validateUI())
        {
            _obj_BankRecoPresenter.Save();
            _nullSession();

            string CloseScript = "<script language='javascript'> " + "self.location.href=self.location.href" + "</script>";
            ScriptManager.RegisterStartupScript(upnl_Error, typeof(string), "CloseScript", CloseScript, false);
        }
    }

    private void _UpdateDataTable()
    {
        Dt_BankReco = Session_Dt_BankReco;
        DataGridItemCollection _items=dg_BankReco.Items;
        int _count=dg_BankReco.Items.Count;
        for (int i = 0; i <_count;i++)
        {
           CheckBox chk_Temp=(CheckBox)_items[i].Cells[2].FindControl("chk_Select");
           int Sr_No = Convert.ToInt32(chk_Temp.Text);
           if (chk_Temp.Checked)
           {
               ComponentArt.Web.UI.Calendar dtp_BankDate = (ComponentArt.Web.UI.Calendar)_items[i].Cells[8].FindControl("dtp_BankDate");

               //User_Controls_wuc_Date_Picker dtp_BankDate = (User_Controls_wuc_Date_Picker)_items[i].Cells[8].FindControl("dtp_BankDate");
               Dt_BankReco.Rows[Sr_No - 1]["Bank_Date"] = dtp_BankDate.SelectedDate;
               Dt_BankReco.Rows[Sr_No - 1]["Is_Select"] = true;
           }
           else { Dt_BankReco.Rows[Sr_No - 1]["Is_Select"] = false;}
        }

        Dt_BankReco.AcceptChanges();

        Session_Dt_BankReco = Dt_BankReco; 
    }

    private void _nullSession()
    {
        Session_Dt_BankReco = null;
        Session_Dt_SearchBankReco = null;
    }

    protected void btn_ShowReconcil_Click(object sender, EventArgs e)
    {
        _nullSession();

        StringBuilder _path = new StringBuilder("frm_Bank_Reco.aspx");

        _path.Append("?Id=" + Request.QueryString["Id"]);
        _path.Append("&Name=" + Request.QueryString["Name"]);
        _path.Append("&Hierarchy_Code=" + Request.QueryString["Hierarchy_Code"]);
        _path.Append("&Main_Id=" + Request.QueryString["Main_Id"]);
        _path.Append("&Is_Consol=" + Is_Consol.ToString());
        _path.Append("&StartDate=" + Start_Date.ToString());
        _path.Append("&EndDate=" + End_Date.ToString());


        if (!Is_Uncleared)
        {
            _path.Append("&Is_Uncleared=" + true.ToString());
            btn_ShowReconcil.Text =  "Show Uncleared Vouchers";
        }
        else 
        {
            _path.Append("&Is_Uncleared=" + false.ToString());
            btn_ShowReconcil.Text =  "Show All Vouchers";
        }

        Response.Redirect(_path.ToString());
    }

    protected void btn_Export_To_Excel_Click(object sender, EventArgs e)
    {

        StringBuilder _path = new StringBuilder("frm_Bank_Reco_Report.aspx");

        _path.Append("?Id=" + Request.QueryString["Id"]);
        _path.Append("&Name=" + Request.QueryString["Name"]);
        _path.Append("&Hierarchy_Code=" + Request.QueryString["Hierarchy_Code"]);
        _path.Append("&Main_Id=" + Request.QueryString["Main_Id"]);
        _path.Append("&Is_Consol=" + Is_Consol.ToString());
        _path.Append("&Is_Uncleared=" + Is_Uncleared.ToString());
      

        Response.Redirect(_path.ToString());

//        dg_BankReco.AllowPaging = false;
//        dg_BankReco.Columns[2].Visible = false;
//        //dg_BankReco.DataSource = Session_Dt_BankReco;
//        //dg_BankReco.DataBind();
        
//        string filename = "BANK RECONCILATION";
//        StringWriter strWtr = new System.IO.StringWriter();
//        Html32TextWriter htmlWtr = new Html32TextWriter(strWtr);
        
//        //ClearControls(dg_Delivery_Service_Tax_Payable)
//        //this.dg_Ageing_Outstanding_Grid.RenderControl(htmlWtr);
//foreach(Control ct in dg_BankReco.Controls)
//{
//    LinkButton lb = (LinkButton)ct;
//    if (lb != null)
//    {
//        lb.Visible = false;
//    }
//}
//        this.Panel1.RenderControl(htmlWtr);
//        Response.Clear();

//        //Use following line if you want to force user to download file instead of displaying it.   
//        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
//        //Response.AddHeader("Content-Disposition", "attachment; filename=""" + filename + """");
//        Response.ContentEncoding = System.Text.Encoding.UTF7;
//        Response.Charset = "";
//        Response.ContentType = "application/vnd.ms-excel"; //Use application/msword for Word
//        Response.Write(strWtr.ToString());
//        Response.End();
 
    }
    private string SelectedSearchColumn
    {
        get { return ddl_Search.SelectedValue.Trim();}
    }

    private string SearchText
    {
        get { return txt_Search.Text.Trim(); }
    }

    private DataTable Session_Dt_SearchBankReco
    {
        set { StateManager.SaveState("Dt_SearchBankReco", value); }
        get { return StateManager.GetState<DataTable>("Dt_SearchBankReco"); }
    }


    protected void btn_Search_Click(object sender, EventArgs e)
    {
        dg_BankReco.CurrentPageIndex = 0;
        if (SelectedSearchColumn == "0" ||SearchText==string.Empty)
        {
            bind_dg_BankReco = Session_Dt_BankReco;
        }
        else 
        {
            try
            {
                string filterStr;
                if (SelectedSearchColumn == "Bank_Date" || SelectedSearchColumn == "Voucher_Date1")
                {
                    DateTime dt = Convert.ToDateTime(SearchText);
                    filterStr = SelectedSearchColumn + " = #" + dt.ToString("MM/dd/yyyy") + "#";
                }
                else if (SelectedSearchColumn == "Credit" || SelectedSearchColumn == "Debit")
                {
                    filterStr = SelectedSearchColumn + "=" + SearchText + "";
                }
                else
                {
                    filterStr = SelectedSearchColumn + " Like '" + SearchText + "%'";
                }
                DataView objDv = new DataView(Session_Dt_BankReco.Copy(), filterStr, "", DataViewRowState.CurrentRows);
                bind_dg_BankReco = objDv.ToTable();
            }
            catch
            {
                bind_dg_BankReco = Session_Dt_BankReco.Clone();
                errorMessage = "No Data Found";
            }
        }
    }
    protected void Btn_Go_To_Click(object sender, EventArgs e)
    {
        int ds_goto;
        double ds_total;
        int Total_Pages;
        DataTable dt = new DataTable();
        dt=(Session_Dt_BankReco);
        ds_total = dt.Rows.Count;
        ds_goto =Convert.ToInt32(Txt_Go_To.Text);
        dg_BankReco.CurrentPageIndex= ds_goto - 1;
        dg_BankReco.DataSource = Session_Dt_SearchBankReco;
        Total_Pages = Convert.ToInt32(Math.Ceiling((ds_total) / (dg_BankReco.PageSize)));
        if (ds_goto > Total_Pages)
        {
            lbl_Errors.Text = "Requested Page does not Exist";
            dg_BankReco.CurrentPageIndex = 0;
        }
        dg_BankReco.DataBind();
     }
    
    
    protected void chk_BankVoucherDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_BankVoucherDate.Checked == true)
        {
            DataTable BankReco = new DataTable();
            BankReco = Session_Dt_BankReco;

            foreach (DataRow dr in BankReco.Rows)
            {
                if (Convert.ToBoolean(dr["Is_Select"]) == false)
                {
                    dr["Bank_Date"] = Convert.ToDateTime(dr["Voucher_Date"]);
                    dr["Is_Select"] = true;
                }
            }

            Session_Dt_BankReco = BankReco;
            bind_dg_BankReco = Session_Dt_BankReco;
        }

        if (chk_BankVoucherDate.Checked == false)
        {
            DataTable BankReco = new DataTable();
            BankReco = Session_Dt_BankReco;

            foreach (DataRow dr in BankReco.Rows)
            {
                if (Convert.ToBoolean(dr["Is_Select1"]) == false)
                {
                    dr["Bank_Date"] = DateTime.Now;
                    dr["Is_Select"] = false;
                }
            }

            Session_Dt_BankReco = BankReco;
            bind_dg_BankReco = Session_Dt_BankReco;
        }

        SetAmountNotCleared_BalanceAsBank();
    }

    private void SetAmountNotCleared_BalanceAsBank()
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        decimal result = 0;
        decimal Initial_BalAsBank_cr = 0;
        decimal Initial_BalAsBank_dr = 0;

        //string FilterString = "Is_Select=1 And Bank_Date >= #" + Start_Date.ToString("MM/dd/yyyy") + "# And Bank_Date <= #" + End_Date.ToString("MM/dd/yyyy") + "#";

        string FilterString = "Is_Select=1 And Bank_Date <= #" + End_Date.ToString("MM/dd/yyyy") + "#";

        TotalCredit = Convert.ToDecimal(Convert.IsDBNull(Session_Dt_BankReco.Compute("Sum(Credit)", FilterString)) == true ? 0 : Session_Dt_BankReco.Compute("Sum(Credit)", FilterString));
        TotalDebit = Convert.ToDecimal(Convert.IsDBNull(Session_Dt_BankReco.Compute("Sum(Debit)", FilterString)) == true ? 0 : Session_Dt_BankReco.Compute("Sum(Debit)", FilterString));

 
        lbl_Amt_Not_cleared_cr.Text = Convert.ToString((Math.Abs(Convert.ToDecimal(hdf_Updated_Amt_Not_cleared_cr.Value) - TotalCredit)).ToString("0.000"));
        lbl_Amt_Not_cleared_dr.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(hdf_Updated_Amt_Not_cleared_dr.Value) - TotalDebit).ToString("0.000"));

        Initial_BalAsBank_dr = Math.Abs(Convert.ToDecimal(hdf_Initial_BalAsBank_dr.Value)) + TotalDebit;
        Initial_BalAsBank_cr = Math.Abs(Convert.ToDecimal(hdf_Initial_BalAsBank_cr.Value)) + TotalCredit;

        result = Initial_BalAsBank_cr * 1 - Initial_BalAsBank_dr * 1;

        if(result<0)
        {
           lbl_Bal_As_Bank_dr.Text = Convert.ToString(Math.Abs(result).ToString("0.000"));
           lbl_Bal_As_Bank_cr.Text="0.000";
        }
        else
        {
           lbl_Bal_As_Bank_cr.Text = Convert.ToString(Math.Abs(result).ToString("0.000"));
           lbl_Bal_As_Bank_dr.Text="0.000";
        } 
    }

    protected void btn_Refresh_Click(object sender, EventArgs e)
    {
       _UpdateDataTable();
       SetAmountNotCleared_BalanceAsBank();
    }

     
}
