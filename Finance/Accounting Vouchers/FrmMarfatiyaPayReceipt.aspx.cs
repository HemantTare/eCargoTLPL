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
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView;
using Raj.EC;
using System.Data.SqlClient;


public partial class Finance_Accounting_Vouchers_FrmMarfatiyaPayReceipt : ClassLibraryMVP.UI.Page
{

    #region ClassVariables

    Raj.EC.Common objComm = new Raj.EC.Common();
    MRCashChequePresenter objMRCashChequePresenter;
    DataTable DT = new DataTable();
    DataSet objDS = new DataSet();
    private DAL objDAL = new DAL();
    string _flag = "";
    string Mode = "0";
    string _GC_No_XML;
    PageControls pc = new PageControls();
    Raj.EC.MarfatiyaPaymentReceipt objMarPayReceipt = new Raj.EC.MarfatiyaPaymentReceipt();
    public EventHandler OnGetDetails;  
    //DataTable SessionBindMarfatiyaGrid { set;}

 


    #endregion

    #region ControlsValues

    public IMRCashChequeDetailsView MRCashChequeDetailsView
    {
        get { return (IMRCashChequeDetailsView)WucMRCashChequeDetails1; }
    }

    public string ReceiptNo
    {
        set { lbl_ReceiptNo.Text = value; }
        get { return lbl_ReceiptNo.Text; }

    }
    public DateTime ReceiptDate
    {
        set { dtp_Receipt_Date.SelectedDate = value; }
        get { return dtp_Receipt_Date.SelectedDate; }
    }
    
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public String GetGCNoXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }

    public decimal TotalReceivables
    {
        get
        {
            return hdn_Received_Amount.Value == "" ? 0 : Util.String2Decimal(hdn_Received_Amount.Value);
        }
        set
        {
            lbl_Received_Amount.Text = value.ToString();
            hdn_Received_Amount.Value = value.ToString();
        }
    }
    public int Debit_To_Ledger_ID
    {
        get { return Util.String2Int(ddl_DebitTo.SelectedValue); }
    }

 #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string Branch_Name = UserManager.getUserParam().MainName;
        int Branch_Id = UserManager.getUserParam().MainId;
        Set_DebitTo_BranchID(Branch_Name.ToString(), Branch_Id.ToString());
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.FinanceModel.MRDeliveryModel));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));

        objMRCashChequePresenter = new MRCashChequePresenter(WucMRCashChequeDetails1, IsPostBack);
        WucMRCashChequeDetails1.Scmcheque = scm_MarfReceipt;
        DropDownList ddl;
        ddl = (DropDownList)(this.WucMRCashChequeDetails1.FindControl("ddl_CashLedger"));  
        //((DropDownList)((this.Parent).FindControl("WucMRCashChequeDetails1")).FindControl("ddl_CashLedger.selectedValue") = this.ddl_CashLedger.SelectedItem.Text);

        //WucMRCashChequeDetails1_ddl_CashLedger.selectedValue = "0";            //ddl_CashLedger 
        //WucMRCashChequeDetails1_ddl_CashLedger.Enabled = "false";
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            //Assign_Hidden_Values_For_Reset();
            Set_DebitTo_LedgerID("", "0");
            ReceiptDate = DateTime.Now; 
            if (keyID <= 0)
            {
                Next_Bill_Number(); 
            }
            else if (keyID >= 0)
            {

                String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
            }
                fillDataGrid();
            
        }
    }


    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_Bills.Value = "0";
        hdn_Bill_Amount.Value = "0"; 
        hdn_Received_Amount.Value = "0"; 
        hdnKeyID.Value = "0";

        lbl_Total_Bills.Text = "0";
        lbl_Bill_Amount.Text = "0"; 
        lbl_Received_Amount.Text = "0"; 
        lbl_Total_Bills.Text = "0";
 
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "2")
        {
            dtp_Receipt_Date.Enabled = false;
            ddl_DebitTo.Enabled = false;
            //calImg.Visible = false;
            TD_Calender.Visible = false;
            WucMRCashChequeDetails1.Visible = true;
        }
        else if (Mode == "4")
        {
            dtp_Receipt_Date.Enabled = false;
            ddl_DebitTo.Enabled = false;
            txt_Remarks.Enabled = false;
            dg_PayReceipt.Enabled = false; 
        }

    }

   

    private void fillDataGrid()
    {
        

        if (keyID <= 0)
        {
            objDS = objMarPayReceipt.Get_EC_Opr_Marfatiya_PayReceipt_ReadValues(Convert.ToInt32(ddl_DebitTo.SelectedValue), -1, Convert.ToDateTime(dtp_Receipt_Date.SelectedDate));
       
        }
        else if (keyID > 0)
        {
            objDS = objMarPayReceipt.Get_EC_Opr_Marfatiya_PayReceipt_ReadValues(0, keyID, Convert.ToDateTime(dtp_Receipt_Date.SelectedDate));
             
            BindData(objDS.Tables[1]);
        } 
         dg_PayReceipt.DataSource = objDS.Tables[0] ;
         dg_PayReceipt.DataBind();
    }

    private void BindData(DataTable dt)
    {
        //ReceiptNo = objComm.Get_Next_Number();
        lbl_Receipt_No.Text = dt.Rows[0]["Receipt_No_For_Print"].ToString();
        dtp_Receipt_Date.SelectedDate =  Convert.ToDateTime(dt.Rows[0]["Bill_Date"]);
        Set_DebitTo_LedgerID(dt.Rows[0]["Ledger_Name"].ToString(), dt.Rows[0]["Ledger_Id"].ToString());
        txt_Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        lbl_Bill_Amount.Text = dt.Rows[0]["Total_Received_Amount"].ToString();
        lbl_Received_Amount.Text = dt.Rows[0]["Total_Received_Amount"].ToString();
        hdn_Received_Amount.Value = dt.Rows[0]["Total_Received_Amount"].ToString(); 
        lbl_Total_Bills.Text = Convert.ToString(dt.Rows.Count);
        hdn_Total_Bills.Value = Convert.ToString(dt.Rows.Count);
        WucMRCashChequeDetails1.CashAmount = Convert.ToDecimal(dt.Rows[0]["Cash_Amount"]);
        WucMRCashChequeDetails1.ChequeAmount = Convert.ToDecimal(dt.Rows[0]["Total_Cheque_Amount"]);   
    }
    private void Next_Bill_Number()
    {
        ReceiptNo = objComm.Get_Next_Number();
    }

    public void Set_DebitTo_LedgerID(string text, string value)
    {
        ddl_DebitTo.DataTextField = "Ledger_Name";
        ddl_DebitTo.DataValueField = "Ledger_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_DebitTo);
    }

    public void Set_DebitTo_BranchID(string text, string value)
    {
        ddl_BillingBranch.DataTextField = "Branch_Name";
        ddl_BillingBranch.DataValueField = "Branch_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_BillingBranch);
    }
    protected void dg_PayReceipt_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
       
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        DataTable DT = (DataTable)(Session["MarfatiyaReceiptDetails"]);

        if (TotalReceivables <= 0)
        {
            errorMessage = "Total Receivable should be greater than Zero";
            return false;
        }
        else if (WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = "Please Enter Valid Cheque Amount";
            return false;
        }
        else if ((WucMRCashChequeDetails1.CashAmount + WucMRCashChequeDetails1.ChequeAmount) != TotalReceivables)
        {
            errorMessage = "Sum Of Total Receivables Equal to Cash Amount and Cheque Amount";
            return false;
        }
        else if (Debit_To_Ledger_ID <= 0)
        {
            errorMessage = "Please Select Debit To Ledger";
            ddl_DebitTo.Focus();
            return false;
        } 
        else
        {
          ATS = true;
        }

        return ATS;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        if (validateUI())
        {
            Save(_flag);
        }
        
    }

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        if (validateUI())
        {
            Save(_flag);
        }
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        if (validateUI())
        {
            Save(_flag);
        }
    }

    private Message Save(string _flag)
    {


        int Total_No_Of_GC = 0;
        CheckBox chk;
        int Bill_ID, GC_ID, Ledger_ID;
        string Bill_No_For_Print, Bill_Date;
        DateTime Marfatiya_Date, ReceiptDate;
        TextBox txt_GC_No_For_Print, txt_Bill_Amount, txt_Pending_Amount, txt_Received_Amount;
        int i;

        if (dg_PayReceipt.Items.Count > 0)
        {
            DT.Columns.AddRange(new DataColumn[5] { new DataColumn("Bill_ID"), new DataColumn("Bill_No_For_Print"), new DataColumn("Bill_Date"), new DataColumn("Bill_Amount"), new DataColumn("Received_Amount")});
            
            
            for (i = 0; i <= dg_PayReceipt.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_PayReceipt.Items[i].FindControl("Chk_Attach");
                
                if (chk.Checked == true)
                {
                    Bill_ID = Convert.ToInt32(dg_PayReceipt.Items[i].Cells[1].Text);
                    Bill_No_For_Print = dg_PayReceipt.Items[i].Cells[2].Text;
                    Bill_Date = dg_PayReceipt.Items[i].Cells[3].Text;
                    txt_Bill_Amount = (TextBox)dg_PayReceipt.Items[i].FindControl("txt_Bill_Amount");
                    txt_Received_Amount = (TextBox)dg_PayReceipt.Items[i].FindControl("txt_Received_Amount"); 

                    //GC_ID = Convert.ToInt32(dg_PayReceipt.Items[i].Cells[2].Text); 
                    //txt_GC_No_For_Print = (TextBox)dg_PayReceipt.Items[i].FindControl("txt_GC_No_For_Print");

                    DataRow newCustomersRow = DT.NewRow();
                    newCustomersRow["Bill_ID"] = Bill_ID; 
                    newCustomersRow["Bill_No_For_Print"] = Bill_No_For_Print;
                    newCustomersRow["Bill_Date"] = Bill_Date; 
                    newCustomersRow["Bill_Amount"] = Util.String2Decimal(txt_Bill_Amount.Text);
                    newCustomersRow["Received_Amount"] = Util.String2Decimal(txt_Received_Amount.Text); 
 
                    DT.Rows.Add(newCustomersRow); 
                }
            }

           
        }
 
        DT.TableName = "MarfatiyaReceiptDetails";
        DataTable DT1 = DT.Copy();

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string MarfatiyaReceiptDetailsXML = ds.GetXml().ToLower();

        Ledger_ID  =  Convert.ToInt32(ddl_DebitTo.SelectedValue);
        Marfatiya_Date = Convert.ToDateTime(dtp_Receipt_Date.SelectedDate);
        int Voucher_ID = 0;
        Decimal lbl_Bill_Amount = Util.String2Decimal(this.lbl_Bill_Amount.Text); 
        Decimal lbl_Received_Amount = Util.String2Decimal(this.lbl_Received_Amount.Text); 

        Message objMessage = new Message();
        SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode),    
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId),  
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Receipt_Date", SqlDbType.DateTime,0,Marfatiya_Date),
            objDAL.MakeInParams("@Marfatiya_ID", SqlDbType.Int, 0,Ledger_ID),  
            objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,MRCashChequeDetailsView.CashAmount),
            objDAL.MakeInParams("@Cash_Ledger_ID",SqlDbType.Int,0,MRCashChequeDetailsView.CashLedgerID),
            objDAL.MakeInParams("@Total_Cheque_Amount",SqlDbType.Decimal,0,MRCashChequeDetailsView.ChequeAmount),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,txt_Remarks.Text),
            objDAL.MakeInParams("@MarfatiyaReceiptDetailsXML", SqlDbType.Xml,0,MarfatiyaReceiptDetailsXML), 
            objDAL.MakeInParams("@mrchequedetailsXML",SqlDbType.Xml,0,MRCashChequeDetailsView.MRChequeDetailsXML),
            objDAL.MakeInParams("@Marfatiya_Receipt_ID", SqlDbType.Int,0, keyID),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)};

        objDAL.RunProc("dbo.FA_Opr_Marfatiya_Payment_Receipt_Save", objSqlParam);

        objMessage.messageID = Convert.ToInt32(objSqlParam[16].Value);
        objMessage.message = Convert.ToString(objSqlParam[17].Value);
         
        string _Msg;
        _Msg = "Saved SuccessFully";
        if (_flag == "SaveAndNew")
        {
            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Finance/Accounting Vouchers/FrmMarfatiyaBill.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
        }
        else if (_flag == "SaveAndExit")
        {
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else if (_flag == "SaveAndPrint")
        {
            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            int Document_ID = Convert.ToInt32(objSqlParam[11].Value);
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
        }
        else
        {
            lblErrors.Text = objMessage.message;

        }

        lblErrors.Text = objMessage.message;
        hdnKeyID.Value = Convert.ToString(objSqlParam[11].Value);
        return objMessage;
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    
    protected void dtp_Receipt_Date_SelectionChanged(object sender, EventArgs e)
    {
        //Assign_Hidden_Values_For_Reset();

        //_GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        //objPDSPresenter.fillgrid();
    }
    protected void ddl_MarfatiyaName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //WucVehicleSearch1.VehicleID = 0;
        //VendorName = "";
        //VendorID = 0;
        //DeliveryModeChange();
    }

    protected void ddl_DebitTo_TxtChange(object sender, EventArgs e)
    {
        fillDataGrid();
    }
}
