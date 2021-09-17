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
using System.Data.Odbc;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using Raj.EC;
using System.IO;
using System.Text.RegularExpressions;


public partial class Bank_Reco_ExcelImport : System.Web.UI.UserControl, IBankRecoExcelImportView
{
   // private BankRecoExcelImportPresenter _obj_BankRecoExcelImportPresenter;
    private DAL _objDAL = new DAL();

    private DataTable Dt_BankReco1=new DataTable();
    private DataTable Dt_BankReco2 = new DataTable();
    private DataTable Dt_BankReco3 = new DataTable();
    public int _rowNo = 1;
    public decimal Initial_AmountNotBank_dr, Initial_AmountNotBank_cr, Initial_BalAsBank_dr, Initial_BalAsBank_cr,BalanceAsPerCompBook;
    private DateTime maxVoucherDate;
    private String HierarchyCode;
    private int MainId;
    private string btn_text = "";
    private bool _uncleared = false;
    //private Boolean Is_VT = Convert.ToBoolean(Param.getUserParam().Is_VT);
    //private Boolean Is_VT = true;
    Raj.FA.ReportsModel.BankRecoModel _objBankRecoModel;
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
    public bool ShowUnclearedVouchers
    {
        set
        {
            _uncleared = value;
        }
        get
        {
            return _uncleared;
        }
    }
    public string SetButtonCaption
    {
        set
        {
            btn_text = value;
        }
    }

    public DataTable Session_Dt_BankReco
    {
        set { StateManager.SaveState("Dt_BankReco", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankReco"); }
    }

    private DataTable Session_Dt_BankReco1
    {
        set { StateManager.SaveState("Dt_BankReco1", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankReco1"); }
    }

    private DataTable Session_Dt_BankReco2
    {
        set { StateManager.SaveState("Dt_BankReco2", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankReco2"); }
    }

    private DataTable Session_Dt_BankReco3
    {
        set { StateManager.SaveState("Dt_BankReco3", value); }
        get { return StateManager.GetState<DataTable>("Dt_BankReco3"); }
    }
    private DataTable Session_Dt_Upload
    {
        set { StateManager.SaveState("TableUpload", value); }
        get { return StateManager.GetState<DataTable>("TableUpload"); }
    }

    public DataSet get_Ds_BankReco
    {
        get 
        {

           DataTable Dt_MergeTable = new DataTable();
           Dt_MergeTable = Session_Dt_BankReco1;
           Dt_MergeTable.Merge(Session_Dt_BankReco2);

           //_UpdateDataTable();
           DataSet Ds_Temp=new DataSet();
           DataTable Dt_Temp = new DataTable();

           Dt_Temp.Columns.Add("Bank_Date",Type.GetType("System.String"));
           Dt_Temp.Columns.Add("Voucher_ID",Type.GetType("System.Int32"));
           Dt_Temp.Columns.Add("Cheque_No", Type.GetType("System.String"));

           DataView Dv = new DataView(Dt_MergeTable, "Is_Select = 1 OR Is_Select1 = 1", "Sr_No ASC", DataViewRowState.CurrentRows);

           DataTable Dt_Filtered = Dv.ToTable();
           int _count = Dt_Filtered.Rows.Count;
           for (int i = 0; i < _count; i++)
           {
               if (((bool)Dt_Filtered.Rows[i]["Is_Select"] != (bool)Dt_Filtered.Rows[i]["Is_Select1"]) || (Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date"]) != Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date1"])))
               { 
               DataRow Dr=  Dt_Temp.NewRow();
               Dr["Voucher_ID"] = Dt_Filtered.Rows[i]["Voucher_Id"].ToString();
               Dr["Cheque_No"] = Dt_Filtered.Rows[i]["Cheque_No"].ToString();

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
           
            //return Convert.ToDateTime(Session["End_Date"]);
            return WucMatchVaoucher.EndDate;
           
        }
        set { WucMatchVaoucher.EndDate = value; }
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
        get { return false; }
    }

    public DateTime Start_Date
    {
        get
        {
            
            //return Convert.ToDateTime(Session["Start_Date"]);
            return WucMatchVaoucher.StartDate; 

            
        }

        set { WucMatchVaoucher.StartDate = value; }
       
   }

    public  DataTable bind_dg_BankReco1
    { 
        set
        {
            //----To Bind Filter View---

          if (!IsPostBack)
          {Session_Dt_BankReco1 = value;}

          //Session_Dt_SearchBankReco1 = value;

           dg_BankReco1.DataSource=value;
           dg_BankReco1.DataBind();
          
        } 
    }

    public DataTable bind_dg_BankReco
    {
        set
        {
           

        }
    }



    public DataTable bind_dg_BankReco2
    {
        set
        {
            //----To Bind Filter View---

            if (!IsPostBack)
            { Session_Dt_BankReco2 = value; }

           // Session_Dt_SearchBankReco2 = value;

            dg_BankReco2.DataSource = value;
            dg_BankReco2.DataBind();

        }
    }

    public DataTable bind_dg_BankReco3
    {
        set
        {
            //----To Bind Filter View---

            if (!IsPostBack)
            {
                Session_Dt_BankReco3 = value;
            }
            dg_BankReco3.DataSource = value;
            dg_BankReco3.DataBind();

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
            //lbl_LedgerName.Text = "BALANCE BROUGHT FORWARD";

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
            //return 8;
        } 
    }

    public bool validateUI()
    {
        if (validateUI1() && validateUI2())
        {
            return true;
        }
        else { return false;}
    }

    public bool validateUI1()
    {
        ArrayList SrNoList = new ArrayList();

        DataView Dv = new DataView(Dt_BankReco1, "Is_Select = 1", "Sr_No ASC", DataViewRowState.CurrentRows);
            
        DataTable Dt_Filtered = Dv.ToTable();
        int _count = Dt_Filtered.Rows.Count;

        for (int i = 0; i < _count; i++)
        {
            if (Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date"]) < Convert.ToDateTime(Dt_Filtered.Rows[i]["Cheque_Date"]))
            {
                SrNoList.Add(Dt_Filtered.Rows[i]["Sr_No"].ToString());    
            }
        }

        //if (Dt_Filtered.Rows.Count == 0)
        //{ errorMessage = "Please Select Atleast One Voucher"; return false; }

        if (SrNoList.Count != 0)
        {
            System.Text.StringBuilder _errorMessage = new System.Text.StringBuilder("Bank Date Must Be Greater Than Cheque Date For Sr No:");
            for (int i = 0; i < SrNoList.Count; i++)
            { _errorMessage.Append("  " + SrNoList[i].ToString() + ","); }

            errorMessage = _errorMessage.Remove(_errorMessage.Length - 1, 1).ToString();

            return false;
        }        
        return true;
     }

    public bool validateUI2()
    {
        ArrayList SrNoList = new ArrayList();

        DataView Dv = new DataView(Dt_BankReco2, "Is_Select = 1", "Sr_No ASC", DataViewRowState.CurrentRows);

        DataTable Dt_Filtered = Dv.ToTable();
        int _count = Dt_Filtered.Rows.Count;

        for (int i = 0; i < _count; i++)
        {
            if (Convert.ToDateTime(Dt_Filtered.Rows[i]["Bank_Date"]) < Convert.ToDateTime(Dt_Filtered.Rows[i]["Cheque_Date"]))
            {
                SrNoList.Add(Dt_Filtered.Rows[i]["Sr_No"].ToString());
            }
        }

        //if (Dt_Filtered.Rows.Count == 0)
        //{ errorMessage = "Please Select Atleast One Voucher"; return false; }

        if (SrNoList.Count != 0)
        {
            System.Text.StringBuilder _errorMessage = new System.Text.StringBuilder("Bank Date Must Be Greater Than Cheque Date For Sr No:");
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

        DateTime t = new DateTime(2009, 01, 11);
        DataSet ds_Matched = new DataSet();
        DataSet ds_UnMatched = new DataSet();
        DataSet ds_UnBankMathed = new DataSet();
        Raj.EC.Common ObjCommon = new Common();
        btn_save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_save));
       // btn_Change_Period.Attributes.Add("onclick"," return ChangePeriod('../../Controls/frm_Date_Range.aspx')");
        btn_Upload.Attributes.Add("onclick", "return ChangePeriod('../../Finance/Utilities/FrmUploadExcelSheet.aspx')");




        if (!IsPostBack)
        {
            _nullSession();
            WucMatchVaoucher.SetComSepColumnName = "Particulars,Debit,Credit,Voucher_Type,Cheque_Date,Cheque_No,Bank_Date";
            WucMatchVaoucher.StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
            WucMatchVaoucher.EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
     


            WucUnMatchVaoucher.SetComSepColumnName = "Particulars,Debit,Credit,Voucher_Type,Cheque_Date,Cheque_No,Bank_Date";
            WucUnMatchVaoucher.StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
            WucUnMatchVaoucher.EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
     

            WucUnMatchBankVaoucher.SetComSepColumnName = "Particulars,Debit,Credit,Voucher_Type,Cheque_Date,Cheque_No,Bank_Date";
            WucUnMatchBankVaoucher.StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
            WucUnMatchBankVaoucher.EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
     

            WucMatchVaoucher.VisibleChangePeriod = false;
            WucUnMatchVaoucher.VisibleChangePeriod = false;
            WucUnMatchBankVaoucher.VisibleChangePeriod = false;

        }

        WucMatchVaoucher.SetDataGrid = dg_BankReco1;
        WucUnMatchVaoucher.SetDataGrid = dg_BankReco2;
        WucUnMatchBankVaoucher.SetDataGrid = dg_BankReco3;

       //_obj_BankRecoExcelImportPresenter = new BankRecoExcelImportPresenter(this, IsPostBack);
        _objBankRecoModel = new Raj.FA.ReportsModel.BankRecoModel(this);

        
        if (!IsPostBack)
        {           
            BindGrids();
        }
        DefaultSetting();

     
       

        //_Ledger_Voucher_ReportsPresenter = new Ledger_Voucher_ReportsPresenter(this, IsPostBack);

        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function'){");
        sbValid.Append("}");
        sbValid.Append("this.value = 'Wait...';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_save, ""));
        sbValid.Append(";");
        btn_save.Attributes.Add("OnClick", sbValid.ToString());


        if (Session["Dt_BankReco1"] != null)
        {
            ds_Matched.Tables.Add(Session_Dt_BankReco1.Copy());
            WucMatchVaoucher.SetDataSet = ds_Matched;
        }

        if (Session["Dt_BankReco2"] != null)
        {
            ds_UnMatched.Tables.Add(Session_Dt_BankReco2.Copy());
            WucUnMatchVaoucher.SetDataSet = ds_UnMatched;
        }

        if (Session["Dt_BankReco3"] != null)
        {
            ds_UnBankMathed.Tables.Add(Session_Dt_BankReco3.Copy());
            WucUnMatchBankVaoucher.SetDataSet = ds_UnBankMathed;
        }

    }
    
    protected void dg_BankReco1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
             CheckBox chk_Select = (CheckBox)e.Item.Cells[2].FindControl("chk_Select");
             chk_Select.Attributes.Add("onclick", "SetAmountNotCleared(this," + e.Item.Cells[8].Text + "," + e.Item.Cells[9].Text + ",'I')");
        }            
    }

    protected void dg_BankReco1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
         _UpdateDataTable1();
         dg_BankReco1.CurrentPageIndex = e.NewPageIndex;
        // BindSearchData1();
        // bind_dg_BankReco1 = Session_Dt_SearchBankReco1;
         bind_dg_BankReco =(DataTable)Session["Dt_BankReco1"]; 
        
    }



    protected void dg_BankReco2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chk_Select = (CheckBox)e.Item.Cells[2].FindControl("chk_Select");
            chk_Select.Attributes.Add("onclick", "SetAmountNotCleared(this," + e.Item.Cells[8].Text + "," + e.Item.Cells[9].Text + ",'I')");
        }
    }

    protected void dg_BankReco2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
         _UpdateDataTable2();
        dg_BankReco2.CurrentPageIndex = e.NewPageIndex;
       // BindSearchData2();
        bind_dg_BankReco2 = (DataTable)Session["Dt_BankReco2"];
    }




    protected void dg_BankReco3_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        _UpdateDataTable3();
        dg_BankReco3.CurrentPageIndex = e.NewPageIndex;

        bind_dg_BankReco3 = (DataTable)Session["Dt_BankReco3"];
       // BindSearchData3();
    }



    protected void btn_save_Click(object sender, EventArgs e)
    {
        Message _objMessage = new Message();
         _UpdateDataTable1();
         _UpdateDataTable2();
         _UpdateDataTable3();
         //string s = get_Ds_BankReco.GetXml();
        if (validateUI())
        {
          
           _objMessage= _objBankRecoModel.Save();
          
            if (_objMessage.messageID == 0)
            {
                SaveUnmatchedBankStatement();
            }

            _nullSession();

            string CloseScript = "<script language='javascript'> " + "window.opener.location.reload();window.close();" + "</script>";
            ScriptManager.RegisterStartupScript(upnl_Error, typeof(string), "CloseScript", CloseScript, false);
        }
    }

    private void _UpdateDataTable1()
    {
        Dt_BankReco1 = Session_Dt_BankReco1;
        DataGridItemCollection _items=dg_BankReco1.Items;
        int _count=dg_BankReco1.Items.Count;
        for (int i = 0; i <_count;i++)
        {
           CheckBox chk_Temp=(CheckBox)_items[i].Cells[2].FindControl("chk_Select");
           int Sr_No = Convert.ToInt32(chk_Temp.Text);
           if (chk_Temp.Checked)
           {
               ComponentArt.Web.UI.Calendar dtp_BankDate = (ComponentArt.Web.UI.Calendar)_items[i].Cells[8].FindControl("dtp_BankDate");

               //User_Controls_wuc_Date_Picker dtp_BankDate = (User_Controls_wuc_Date_Picker)_items[i].Cells[8].FindControl("dtp_BankDate");
               Dt_BankReco1.Rows[Sr_No - 1]["Bank_Date"] = dtp_BankDate.SelectedDate;
               Dt_BankReco1.Rows[Sr_No - 1]["Is_Select"] = true;
           }
           else { Dt_BankReco1.Rows[Sr_No - 1]["Is_Select"] = false;}
        }

        Dt_BankReco1.AcceptChanges();

        Session_Dt_BankReco1 = Dt_BankReco1; 
    }

    private void _UpdateDataTable2()
    {
        Dt_BankReco2 = Session_Dt_BankReco2;
        DataGridItemCollection _items = dg_BankReco2.Items;
        int _count = dg_BankReco2.Items.Count;
        for (int i = 0; i < _count; i++)
        {
            CheckBox chk_Temp = (CheckBox)_items[i].Cells[2].FindControl("chk_Select");
            int Sr_No = Convert.ToInt32(chk_Temp.Text);
            if (chk_Temp.Checked)
            {
                ComponentArt.Web.UI.Calendar dtp_BankDate = (ComponentArt.Web.UI.Calendar)_items[i].Cells[8].FindControl("dtp_BankDate");

                //User_Controls_wuc_Date_Picker dtp_BankDate = (User_Controls_wuc_Date_Picker)_items[i].Cells[8].FindControl("dtp_BankDate");
                Dt_BankReco2.Rows[Sr_No - 1]["Bank_Date"] = dtp_BankDate.SelectedDate;
                Dt_BankReco2.Rows[Sr_No - 1]["Is_Select"] = true;
            }
            else { Dt_BankReco2.Rows[Sr_No - 1]["Is_Select"] = false; }
        }

        Dt_BankReco2.AcceptChanges();

        Session_Dt_BankReco2 = Dt_BankReco2;
    }

    private void _UpdateDataTable3()
    {
        Dt_BankReco3 = Session_Dt_BankReco3;
        DataGridItemCollection _items = dg_BankReco3.Items;
        int _count = dg_BankReco3.Items.Count;
        for (int i = 0; i < _count; i++)
        {
            CheckBox chk_Temp = (CheckBox)_items[i].FindControl("chk_Select");
            int Sr_No = Convert.ToInt32(chk_Temp.Text);
            if (chk_Temp.Checked)
            {
                //User_Controls_wuc_Date_Picker dtp_BankDate = (User_Controls_wuc_Date_Picker)_items[i].Cells[8].FindControl("dtp_BankDate");
                Dt_BankReco3.Rows[Sr_No - 1]["Is_Select"] = true;
            }
            else { Dt_BankReco3.Rows[Sr_No - 1]["Is_Select"] = false; }
        }

        Dt_BankReco3.AcceptChanges();

        Session_Dt_BankReco3 = Dt_BankReco3;
    }

    private void _nullSession()
    {
        Session_Dt_BankReco1 = null;
        Session_Dt_SearchBankReco1 = null;
        Session_Dt_BankReco2 = null;
        Session_Dt_SearchBankReco2 = null;
        Session_Dt_BankReco3 = null;

    }

    private DataTable Session_Dt_SearchBankReco1
    {
        set { StateManager.SaveState("Dt_SearchBankReco1", value); }
        get { return StateManager.GetState<DataTable>("Dt_SearchBankReco1"); }
    }

    private DataTable Session_Dt_SearchBankReco2
    {
        set { StateManager.SaveState("Dt_SearchBankReco2", value); }
        get { return StateManager.GetState<DataTable>("Dt_SearchBankReco2"); }
    } 

    public void BindGrids()
    {
        string _filePath;
        DataSet ds_Matched = new DataSet();
        DataSet ds_UnMatched = new DataSet();
        DataSet ds_UnBankMathed = new DataSet();

        if (Session["fileName"] != null)
        {
            _filePath = Request.PhysicalApplicationPath + "/Finance/Utilities/BankBookExcelFiles/" + Convert.ToString(Session["fileName"]).Trim();
            FileInfo serverFile = new FileInfo(_filePath);
            if (!serverFile.Exists)
            {
                errorMessage = "Please upload excel file";
                return;
            }

            Session["fileName"] = null;
        }
        else
        {
            errorMessage = "Please upload excel file";
            return;
        }


        string _ledgerName = Request.QueryString["Name"];

       

       // string _ledgerName = "BALANCE BROUGHT FORWARD";
        DataSet dsDistExcel = new DataSet();
        if (CompanyManager.getCompanyParam().ClientCode.ToLower() != "reach")
        {
            OdbcConnection excelCon = new OdbcConnection("Driver={Microsoft Excel Driver (*.xls)};DriverId=790;Dbq=" + _filePath + ";DefaultDir=" + Request.PhysicalApplicationPath + ";");
            OdbcCommand excelDistCmd = new OdbcCommand("Select * From [Sheet1$]", excelCon); 
            OdbcDataAdapter daDistExcel = new OdbcDataAdapter(excelDistCmd);
            daDistExcel.Fill(dsDistExcel, "Excel");
        }

        DataSet Ds;

        Ds = _objBankRecoModel.ReadValues();


        AddSrNo(Ds.Tables[0]);
      
        DataTable objDtFromDB = Ds.Tables[0];
        //DataTable objDtFromDB = Session_Dt_Upload;

        DataTable objDtFromExcel = objDtFromDB.Clone();

        DataRow DrExcel;
        DataRow objDr;
        if (CompanyManager.getCompanyParam().ClientCode.ToLower() != "reach")
        {
            Session_Dt_Upload = dsDistExcel.Tables[0];
        }
        //IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);

        for (int i = 0; i < Session_Dt_Upload.Rows.Count; i++)
        {
            DrExcel = objDtFromExcel.NewRow();
            objDr = Session_Dt_Upload.Rows[i];

            if (Convert.IsDBNull(objDr[0]) && Convert.IsDBNull(objDr[1]) && Convert.IsDBNull(objDr[2]) && Convert.IsDBNull(objDr[3]))
            { continue; }
            DrExcel["Cheque_Date"] = Convert.ToDateTime(objDr[0]).ToString("dd/MM/yyyy");
            DrExcel["Particulars"] = objDr[1].ToString().Trim();
            DrExcel["Bank_Date"] = Convert.ToDateTime(objDr[2]).ToString("dd/MM/yyyy");
            DrExcel["Cheque_No"] = objDr[5].ToString().Trim() == string.Empty ? "0" : objDr[5].ToString().Trim();
            DrExcel["Credit"] = objDr[3].ToString().Trim() == string.Empty ? 0 : Convert.ToDecimal(objDr[3]);
            DrExcel["Debit"] = objDr[4].ToString().Trim() == string.Empty ? 0 : Convert.ToDecimal(objDr[4]);

            DrExcel["Is_Select"] = false;
            DrExcel["Is_Select1"] = false;

            // DrExcel["Cheque_Date"] = DateTime.Now;
            DrExcel["Narration"] = "";
            DrExcel["Voucher_Id"] = "0";
            DrExcel["SrNo"] = "1";

            objDtFromExcel.Rows.Add(DrExcel);
        }


        //------------To Merge Data From Database Saved Bank Statement

       objDtFromExcel.Merge(GetOldBankStatement());
        

        DataTable objDt1 = new DataTable();
        DataTable objDt2 = new DataTable();
        DataTable objDt3 = new DataTable();

        objDt1 = objDtFromDB.Clone();
        objDt2 = objDtFromDB.Clone();
        objDt3 = objDtFromDB.Clone();

        foreach (DataRow Dr in objDtFromExcel.Rows)
        {

            //DateTime _voucherDate = Convert.ToDateTime(Dr["Voucher_Date"]);
            DateTime _bankDate = (Convert.IsDBNull(Dr["Bank_Date"]) || Dr["Bank_Date"].ToString().Trim()==string.Empty) == true ? Convert.ToDateTime("1/1/1900") : Convert.ToDateTime(Dr["Bank_Date"]);
            string _chequeNo = (Dr["Cheque_No"].ToString().Trim() == string.Empty || Dr["Cheque_No"].ToString().Trim() =="0") == true ? "0" : Dr["Cheque_No"].ToString();
            decimal _debit = (Convert.IsDBNull(Dr["Debit"]) || Dr["Debit"].ToString().Trim() == string.Empty) == true ? 0 : Convert.ToDecimal(Dr["Debit"]);
            decimal _credit = (Convert.IsDBNull(Dr["Credit"]) || Dr["Credit"].ToString().Trim() == string.Empty) == true ? 0 : Convert.ToDecimal(Dr["Credit"]);
            string filterStr;

            if (_debit > 0)
            {
                filterStr = "Voucher_Date <=#" + _bankDate.ToString("MM/dd/yyyy") + "#" + " And Cheque_No='" + _chequeNo.ToString() + "' And Debit=" + _debit.ToString() + " And Cheque_No<>0";
            }
            else { filterStr = "Voucher_Date <=#" + _bankDate.ToString("MM/dd/yyyy") + "#" + " And Cheque_No='" + _chequeNo.ToString() + "' And Credit=" + _credit.ToString() + " And Cheque_No<>0"; }

            DataView Dv = new DataView(objDtFromDB, filterStr, "", DataViewRowState.CurrentRows);

            if (Dv.Count != 0)
            {
               // Dv["Bank_Date"] = _bankDate.ToString("MM/dd/yyyy");

                DataRow Dr1 = objDt1.NewRow();

                DataTable DtTemp=Dv.ToTable().Copy();
              

                Dr1["Bank_Date"] = _bankDate;
                Dr1["Is_Select"] = true;

                Dr1["SrNo"] = DtTemp.Rows[0]["SrNo"];
                Dr1["Sr_No"] = DtTemp.Rows[0]["Sr_No"];
                Dr1["Voucher_Id"] = DtTemp.Rows[0]["Voucher_Id"];
                Dr1["Particulars"] = DtTemp.Rows[0]["Particulars"];
                Dr1["Narration"] = DtTemp.Rows[0]["Narration"];

                Dr1["Voucher_Type"] = DtTemp.Rows[0]["Voucher_Type"];
                Dr1["Cheque_Date"] = DtTemp.Rows[0]["Cheque_Date"];
                Dr1["Cheque_No"] = DtTemp.Rows[0]["Cheque_No"];
                Dr1["Debit"] = DtTemp.Rows[0]["Debit"];
                Dr1["Credit"] = DtTemp.Rows[0]["Credit"];

                Dr1["Is_Select1"] = DtTemp.Rows[0]["Is_Select1"];
                Dr1["Bank_Date1"] = DtTemp.Rows[0]["Bank_Date1"];
                Dr1["Voucher_Date"] = DtTemp.Rows[0]["Voucher_Date"];

                objDt1.Rows.Add(Dr1);
            }
        }

        objDt1.AcceptChanges();


        objDt2.Merge(objDtFromDB.Copy());


        foreach (DataRow Dr in objDt1.Rows)
        {
            objDt2.Rows[Convert.ToInt32(Dr["Sr_No"])-1].Delete();
        }

        objDt2.AcceptChanges();

        objDt3.Merge(objDtFromExcel.Copy());


        foreach (DataRow Dr in objDt1.Rows)
        {
            for (int i = 0; i < objDt3.Rows.Count;i++)
            {
                if (Dr["Cheque_No"].ToString().Trim() == objDt3.Rows[i]["Cheque_No"].ToString().Trim())
                {
                    objDt3.Rows[i].Delete();
                    objDt3.AcceptChanges();
                }
            }

            objDt3.AcceptChanges();
        }

        objDt3.AcceptChanges();
        objDt1.AcceptChanges();
        objDt2.AcceptChanges();

        AddSrNo(objDt1);
        AddSrNo(objDt2);
        AddSrNo(objDt3);

        bind_dg_BankReco1 = objDt1;
        bind_dg_BankReco2 = objDt2;
        bind_dg_BankReco3 = objDt3;


        decimal _totalDebit;
        decimal _totalCredit;

       if (objDt1.Rows.Count != 0)
       { 
        
        _totalDebit =  Convert.ToDecimal(objDt1.Compute("Sum(Debit)", ""));
        _totalCredit = Convert.ToDecimal(objDt1.Compute("Sum(Credit)", ""));

        DataRow Dr_FromDB = Ds.Tables[1].Rows[0];
        Dr_FromDB["Initial_AmountNotRefBank_dr"] = Math.Abs(Convert.ToDecimal(Dr_FromDB["Initial_AmountNotRefBank_dr"]) - _totalDebit);
        Dr_FromDB["Initial_AmountNotRefBank_cr"] =  Math.Abs(Convert.ToDecimal(Dr_FromDB["Initial_AmountNotRefBank_cr"]) - _totalCredit);


        decimal _tempBalAsBank_Dr = Convert.ToDecimal(Dr_FromDB["Initial_BalAsBank_dr"]) + _totalDebit;
        decimal _tempBalAsBank_Cr = Convert.ToDecimal(Dr_FromDB["Initial_BalAsBank_cr"]) + _totalCredit;

        decimal _tempAmount = _tempBalAsBank_Cr - _tempBalAsBank_Dr;

        if (_tempAmount < 0)
        {
            Dr_FromDB["Initial_BalAsBank_dr"] =Math.Abs(_tempAmount);
        }
        else 
        {
            Dr_FromDB["Initial_BalAsBank_cr"] = _tempAmount; 
        }

        Ds.Tables[1].AcceptChanges();

      }

        SetVariables = Ds.Tables[1];

        ds_Matched.Tables.Add(Session_Dt_BankReco1.Copy());
        WucMatchVaoucher.SetDataSet = ds_Matched;

        ds_UnMatched.Tables.Add(Session_Dt_BankReco2.Copy());
        WucUnMatchVaoucher.SetDataSet = ds_UnMatched;

        ds_UnBankMathed.Tables.Add(Session_Dt_BankReco3.Copy());
        WucUnMatchBankVaoucher.SetDataSet = ds_UnBankMathed;

    }


    public void AddSrNo(DataTable Dt)
    {
        //Ds.Tables[0].Columns.Add("Is_Select", Type.GetType("System.Boolean"));
        //Ds.Tables[0].Columns.Add("Bank_Date", Type.GetType("System.DateTime"));

        if (Dt.Columns["Sr_No"] != null)
        {
            Dt.Columns.Remove("Sr_No");
        }

        Dt.Columns.Add("Sr_No", Type.GetType("System.Int64"));
        
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            //Dt_Temp.Rows[i]["Is_Select"] = false;
            //Dt_Temp.Rows[i]["Bank_Date"] = DateTime.Now;
            Dt.Rows[i]["Sr_No"] = i + 1;
        }
    }

    protected void btn_Export_To_Excel_Click1(object sender, EventArgs e)
    {

        DataTable Dt = new DataTable();


        if (Session["Dt_BankReco1"] != null)
        {
            Dt = Session_Dt_BankReco1.Copy();

            Dt.Columns.Remove("SrNo");
            Dt.Columns.Remove("Is_Select");
            Dt.Columns.Remove("Is_Select1");
            Dt.Columns.Remove("Voucher_Id");
            Dt.Columns.Remove("Bank_Date1");
            Dt.Columns.Remove("Voucher_Date");
        }
        Session["ExportToExcel"] = Dt;

        Response.Redirect("/Finance/Utilities/frm_ExportToExcelGrid.aspx");
    }

    protected void btn_Export_To_Excel_Click2(object sender, EventArgs e)
    {
        DataTable Dt = new DataTable();

        if (Session["Dt_BankReco2"] != null)
        {
            Dt = Session_Dt_BankReco2.Copy();
            Dt.Columns.Remove("SrNo");
            Dt.Columns.Remove("Is_Select");
            Dt.Columns.Remove("Is_Select1");
            Dt.Columns.Remove("Voucher_Id");
            Dt.Columns.Remove("Voucher_Date");
            Dt.Columns.Remove("Bank_Date1");
        }

        Session["ExportToExcel"] = Dt;
        Response.Redirect("~/Finance/Utilities/frm_ExportToExcelGrid.aspx");
    }

    protected void btn_Export_To_Excel_Click3(object sender, EventArgs e)
    {
        DataTable Dt = new DataTable();

        if (Session["Dt_BankReco3"] != null)
        {
            Dt = Session_Dt_BankReco3.Copy();
            Dt.Columns.Remove("SrNo");
            Dt.Columns.Remove("Is_Select");
            Dt.Columns.Remove("Is_Select1");
            Dt.Columns.Remove("Voucher_Id");
            Dt.Columns.Remove("Voucher_Date");
            Dt.Columns.Remove("Bank_Date1");
        }

        Session["ExportToExcel"] = Dt;

        Response.Redirect("~/Finance/Utilities/frm_ExportToExcelGrid.aspx");
    }


    private DataTable GetOldBankStatement()
    { 
       DataSet Ds = new DataSet();
        SqlParameter[] param =    { 
                                        _objDAL.MakeInParams("@Ledger_ID", SqlDbType.Int, 0,keyID)

                                  };

        _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_ExcelImport", param, ref Ds);
            return Ds.Tables[0];


    }


    private Message SaveUnmatchedBankStatement()
    {
        Message _objMessage = new Message();

        DataSet Ds = new DataSet();

        Ds.Tables.Add(Dt_BankReco3.Copy());

        string _unmatchedBankStatementXml = Ds.GetXml();
        Ds = null;

        SqlParameter[] param = {      
                                    _objDAL.MakeOutParams("@ERROR_CODE", SqlDbType.Int, 0),
                                    _objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                    _objDAL.MakeInParams("@Ledger_ID", SqlDbType.Int, 0,keyID),
                                    _objDAL.MakeInParams("@XML", SqlDbType.Xml, 0,_unmatchedBankStatementXml ),
                                    _objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0,1)
                                 };

        _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Bank_Statement_Save", param);

        _objMessage.messageID = Convert.ToInt32(param[0].Value);
        _objMessage.message = Convert.ToString(param[1].Value);

        return _objMessage;


    }
 
 
}
