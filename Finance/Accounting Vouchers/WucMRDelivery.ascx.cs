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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC; 

public partial class Finance_Accounting_Vouchers_WucMRDelivery : System.Web.UI.UserControl,IMRDeliveryView
{

    MRDeliveryPresenter objMRDeliveryPresenter;
    MRCashChequePresenter objMRCashChequePresenter;

    private DataSet _ds = new DataSet();
    DateTime Delivery_Date = new DateTime();
    Common objcommon = new Common();
    string Mode = "0";
    string _flag = "";


    #region Control Values

    public IMRCashChequeDetailsView MRCashChequeDetailsView 
    {
        get {return (IMRCashChequeDetailsView)WucMRCashChequeDetails1 ;}
    }
     
    public IMRGeneralDetailsView MRGeneralDetailsView 
    {
        get { return (IMRGeneralDetailsView)WucMRGeneralDetails1; }
    }

    public IMRDeliveryDetailsView MRDeliveryDetailsView
    {
        get { return (IMRDeliveryDetailsView)WucMRDeliveryDetails1; }
    }
 
    public decimal RebookedCharges
    {
        get{return txt_Rebooked_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Rebooked_Charges.Text);}
        set { txt_Rebooked_Charges.Text = value.ToString(); }
    }       

    public decimal Advance_Amount
    {
        get 
        {
            return Util.String2Decimal(txt_Advance_Amount.Text);
        }
        set
        {
            txt_Advance_Amount.Text = value.ToString();
        }
    }

    public decimal Service_Tax_On_Advance
    {
        get
        {
            return hdn_Service_Tax_On_Advance.Value == "" ? 0 : Util.String2Decimal(hdn_Service_Tax_On_Advance.Value);
        }
        set
        {
            hdn_Service_Tax_On_Advance.Value = value.ToString();
        }
    }

    public decimal TotalReceivables 
    {
        get {
            return hdn_Delivery_Total_Receivables.Value == "" ? 0 : Util.String2Decimal(hdn_Delivery_Total_Receivables.Value);
            }
        set { 
                txt_TotalReceivables.Text = value.ToString();
                hdn_Delivery_Total_Receivables.Value = value.ToString();
            }
    }
   
    public string GCTotal 
    {
        get { return txt_GCTotal.Text; }
        set { txt_GCTotal.Text = value; ;}
    }      

    public decimal SubTotal 
    {
        get {return hdn_Delivery_Sub_Total.Value == "" ? 0 : Util.String2Decimal(hdn_Delivery_Sub_Total.Value);}
        set{
            txt_Sub_Total.Text = value.ToString();
            hdn_Delivery_Sub_Total.Value = value.ToString();
        }
    }

    public decimal GCSubTotal
    {
        get { return hdn_SubTotal.Value == "" ? 0 : Util.String2Decimal(hdn_SubTotal.Value); }
        set { hdn_SubTotal.Value = value.ToString(); }
    }

    public decimal GC_Total_Amount
    {
        get { return hdn_Total_GC_Amount.Value == "" ? 0 : Util.String2Decimal(hdn_Total_GC_Amount.Value); }
        set { hdn_Total_GC_Amount.Value = value.ToString(); }
    }

    public decimal TotalAmount
    {
        get { return txt_Total_Amount.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Total_Amount.Text); }
        set { txt_Total_Amount.Text = value.ToString(); }
    }

    public decimal TaxAbate 
    {
        get{return hdn_Tax_Abate.Value == "" ? 0 : Util.String2Decimal(hdn_Tax_Abate.Value);}
        set { 
              txt_Tax_Abate.Text = value.ToString();
              hdn_Tax_Abate.Value = value.ToString();
            }
    }
    
    public decimal AmountTaxable 
    {
        get {return hdn_Amount_Taxable.Value == "" ? 0 : Util.String2Decimal(hdn_Amount_Taxable.Value);}
        set { 
                txt_Amount_Taxable.Text = value.ToString();
                hdn_Amount_Taxable.Value = value.ToString();    
            }
    }
    
    public decimal ServiceTax 
    {
        get {return hdn_Service_Tax_Amount.Value == "" ? 0 : Util.String2Decimal(hdn_Service_Tax_Amount.Value); }
        set { 
                txt_Service_Tax.Text = value.ToString();
                hdn_Service_Tax_Amount.Value = value.ToString();
            }
    }
    
    public decimal OctrAmount 
    {
        get {return hdn_OctAmount.Value == "" ? 0 : Util.String2Decimal(hdn_OctAmount.Value); }
        set { 
                txt_Octr_Amount.Text = value.ToString();
                hdn_OctAmount.Value = value.ToString();
            }
    }
    
    public string ServiceTaxBy 
    {
        get { return txt_service_tax_By.Text; }
        set { txt_service_tax_By.Text = value; ;}
    }
    
    public int DemurageDays 
    {
        get {return txt_Demurage_Days.Text.Trim() == "" ? 0 : Util.String2Int(txt_Demurage_Days.Text); }
        set { txt_Demurage_Days.Text = value.ToString() ;}
    }
    
    public string OctrRecNo 
    {
        get { return txt_Octr_rec_no.Text.Trim() == "" ? "0" : txt_Octr_rec_no.Text; }
        set { txt_Octr_rec_no.Text = value; ;}
    }
    
    public string OctrFormType 
    {
        get {return hdn_Octr_Form_Type_ID.Value == "" ? "0" : hdn_Octr_Form_Type_ID.Value;}
        set { txt_Octr_Form_Type.Text = value; ;}
    }
    
    public DateTime OctrRecDate 
    {
        get
        {
            if (txt_Octr_Rec_Date.Text == "")
            {
                return DateTime.Now;
            }
            else
            {
                return Convert.ToDateTime(txt_Octr_Rec_Date.Text);
            }
        }
        set { txt_Octr_Rec_Date.Text = value.ToString("dd MMM yyyy");}
    }
    
    public string OctrPaidBy 
    {
        get {return hdn_Octr_Pay_Type_ID.Value == "" ? "0" : hdn_Octr_Pay_Type_ID.Value;  }
        set { txt_Octr_Paid_By.Text = value; }
    }
    
    public string AddChrgRemark 
    {
        get { return txt_Add_Charge_remark.Text.Trim(); }
        set { txt_Add_Charge_remark.Text = value;}
    }
    
    public string DiscountRemark 
    {
        get { return txt_discount_remark.Text.Trim(); }
        set { txt_discount_remark.Text = value;}
    }
    
    public decimal OctrFormCharges 
    {
        get{return txt_Octr_Form_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Octr_Form_Charges.Text);}
        set { txt_Octr_Form_Charges.Text = value.ToString();}
    }
    
    public decimal OctrServiceCharges 
    {
        get {return txt_Octr_Service_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Octr_Service_Charges.Text);}
            
        set { txt_Octr_Service_Charges.Text = value.ToString("0.00") ;}
    }
    
    public decimal GICharges 
    {
        get {return txt_GI_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_GI_Charges.Text);}            
        set { txt_GI_Charges.Text = value.ToString() ;}
    }
    
    public decimal DetentionCharges 
    {
        get {return txt_Detention_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Detention_Charges.Text);}            
        set { txt_Detention_Charges.Text = value.ToString() ;}
    }
    
    public decimal HamaliCharges 
    {
        get {return txt_Hamali_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Hamali_Charges.Text);}           
        set { txt_Hamali_Charges.Text = value.ToString("0.00") ;}
    }
    
    public decimal LocalCharges 
    {
        get {return txt_Local_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Local_Charges.Text);}           
        set { txt_Local_Charges.Text = value.ToString() ;}
    }
    
    public decimal DemurageCharges 
    {
        get {return txt_Demurage_Charges.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Demurage_Charges.Text);}            
        set { txt_Demurage_Charges.Text = value.ToString() ;}
    }

    public decimal DeliveryCommission
    {
        get { return txt_DeliveryCommission.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_DeliveryCommission.Text); }
        set { txt_DeliveryCommission.Text = value.ToString(); }
    }
    
    public decimal AdditionalCharges 
    {
        get {return hdn_Additional_Charges.Value == "" ? 0 : Util.String2Decimal(hdn_Additional_Charges.Value); }            
        set { 
                txt_Addition_Charges.Text = value.ToString();
                hdn_Additional_Charges.Value = value.ToString();
            }
    }
    
    public decimal DiscountAmount 
    {
        get {return hdn_Discount_Amount.Value == "" ? 0 : Util.String2Decimal(hdn_Discount_Amount.Value); }            
        set { 
                txt_Discount_Amount.Text = value.ToString();
                hdn_Discount_Amount.Value = value.ToString();
            }
    }

    public int Payment_Type_ID 
    {
        get {return Util.String2Int(hdn_Payment_Type_ID.Value) ;}
        set { hdn_Payment_Type_ID.Value = value.ToString();}
    }
    
    public int Booking_Type_ID
    {
        get { return Util.String2Int(hdn_Booking_Type_ID.Value); }
        set { hdn_Booking_Type_ID.Value = value.ToString(); }
    }
    public int Debit_To_Ledger_ID
    {
        get { return Util.String2Int(ddl_DebitTo.SelectedValue); }
    }

    public int Debit_To_Branch_ID
    {
        get { return Util.String2Int(ddl_BillingBranch.SelectedValue); }
    }
    public int Credit_Memo_ForID
    {
        get{ return Util.String2Int(hdn_CreditMemoFor_Id.Value);}
        set 
        {
            rbtn_CreditMemoFor.SelectedValue = value.ToString();
            hdn_CreditMemoFor_Id.Value = value.ToString();
        }
    }
    public int ReceivedBy
    {
        get { return Util.String2Int(Rbl_Receivedby.SelectedValue); }
        set
        {
            Rbl_Receivedby.SelectedValue = value.ToString();
        }
    }
    public bool Is_Credit_For_Consignee
    {
        get 
        {
            return Util.String2Int(rbtn_CMFConsignee.SelectedValue) == 0 ? false : true;
        }
        set
        {
            rbtn_CMFConsignee.Items[0].Selected = !value;
            rbtn_CMFConsignee.Items[1].Selected = value;
        }
    }  
    public DateTime DeliveryDate
    {
        get
        {
            if (hdn_DeliveryDate.Value == "")
            {
                return Convert.ToDateTime(DateTime.Now.ToShortDateString());
            }
            else
            {
                return Convert.ToDateTime(hdn_DeliveryDate.Value);
            }
        }
        set { hdn_DeliveryDate.Value = value.ToString(); }
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
        
    public decimal Charged_Wt
    {
        get { return hdn_Charges_Wt.Value == "" ? 0 : Util.String2Decimal(hdn_Charges_Wt.Value); }            
        set { hdn_Charges_Wt.Value = value.ToString(); }
    }

    public decimal Std_Octroi_Form_Charges 
    {
        get {
              return hdn_Std_Octroi_Form_Charges.Value == "" ? 0 : Util.String2Decimal(hdn_Std_Octroi_Form_Charges.Value);
            }
            
        set { hdn_Std_Octroi_Form_Charges.Value = value.ToString(); }
    }
    
    public decimal Std_Octroi_Service_Charges
    {
        get {
              return hdn_Std_Octroi_Service_Charges.Value == "" ? 0 : Util.String2Decimal(hdn_Std_Octroi_Service_Charges.Value);
            }
            
        set { hdn_Std_Octroi_Service_Charges.Value = value.ToString(); }
    }
    
    public decimal Std_GI_Charges
    {
        get {return hdn_Std_GI_Charges.Value == "" ? 0 : Util.String2Decimal(hdn_Std_GI_Charges.Value);  }            
        set { hdn_Std_GI_Charges.Value = value.ToString(); }
    }
           
    public decimal Std_Hamali_Charges
    {
        get {return hdn_Std_Hamali_Charges.Value == "" ? 0 : Util.String2Decimal(hdn_Std_Hamali_Charges.Value);}            
        set { hdn_Std_Hamali_Charges.Value = value.ToString(); }
    }    
      
    public decimal Std_Demurage_Charges
    {
        get {return hdn_Std_Demurage_Charges.Value == "" ? 0 : Util.String2Decimal(hdn_Std_Demurage_Charges.Value);}            
        set { hdn_Std_Demurage_Charges.Value = value.ToString(); }
    }

    public decimal Service_Tax_Percent
    {
        get {return hdn_Service_Tax_Percent.Value == "" ? 0 : Util.String2Decimal(hdn_Service_Tax_Percent.Value);}
        set { hdn_Service_Tax_Percent.Value = value.ToString(); }
    }

    public int Service_Pay_By_ID
    {
        get {return hdn_Service_Pay_By_ID.Value == "" ? 0 : Util.String2Int(hdn_Service_Pay_By_ID.Value);}
        set { hdn_Service_Pay_By_ID.Value = value.ToString(); }
    }

    public int Document_ID
    {
        get{return Util.String2Int(hdn_DocumentID.Value);}
        set{hdn_DocumentID.Value = value.ToString(); }
    }

    public int MenuItemId
    {
        get { return Util.String2Int(hdn_MenuItemID.Value); }
        set { hdn_MenuItemID.Value = value.ToString(); }
    }

    public string Flag
    {
        get { return _flag; }
    }

    //Start added on 23-12-13
    public int RoundOff
    {
        set
        {
            lbl_RoundOff.Text = Util.Int2String(value);
            hdn_RoundOff.Value = Util.Int2String(value);
        }
        get { return hdn_RoundOff.Value == string.Empty ? 0 : Util.String2Int(hdn_RoundOff.Value); }
    }
    //End added on 23-12-13

    //Added By Vajiha 1/07/09
    public DataTable SessionDeliveredAgainst
    {
        get { return StateManager.GetState<DataTable>("DeliveredAgainst"); }
        set { StateManager.SaveState("DeliveredAgainst", value); }
    }

    public DataTable SessionDeliveredTo
    {
        get { return StateManager.GetState<DataTable>("DeliveredTo"); }
        set { StateManager.SaveState("DeliveredTo", value); }
    }

    public DataTable BindMemoFor
    {
        set
        {
            rbtn_CreditMemoFor.DataTextField = "Credit_Memo_For";
            rbtn_CreditMemoFor.DataValueField = "Credit_Memo_For_ID";
            rbtn_CreditMemoFor.DataSource = value;
            rbtn_CreditMemoFor.DataBind();
        }
    }   

    #endregion     

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
           lbl_Errors.Text = value;
        }
    }

    public bool validateUI()
    {

        TextBox Txt_DebitLedger, Txt_DebitBranch;
        string Validation = "";
        //int MenuItemId = Common.GetMenuItemId();
        MenuItemId = Common.GetMenuItemId();

        errorMessage = "";

        if (Document_ID == 3)
            Validation = "MR";
        else
            Validation = "Credit Memo";

        Txt_DebitLedger = (TextBox)ddl_DebitTo.FindControl("txtBoxddl_DebitTo");
        Txt_DebitBranch = (TextBox)ddl_BillingBranch.FindControl("txtBoxddl_BillingBranch");


        if (!WucMRGeneralDetails1.validateGeneralDetails(lbl_Errors))
        {
            return false;
        }
        else if (WucMRGeneralDetails1.GC_ID<=0 && keyID<=0)
        {
            errorMessage = "Please Enter " + CompanyManager.getCompanyParam().GcCaption + " No.";
            return false;
        }
        else if (Datemanager.IsValidProcessDate("MR_DEL", MRGeneralDetailsView.MRDate) == false)
        {
            errorMessage = "Please Select Valid Date";
            return false;
        }
        else if (MRGeneralDetailsView.MRDate < DeliveryDate && hdn_DeliveryDate.Value !="")
        {
            lbl_Errors.Text = Validation + " Date can't be less than Delivery Date (" + DeliveryDate.ToString("dd MMM yyyy") + ")";
            return false;
        }        
        else if (AdditionalCharges > 0 && AddChrgRemark == "")
        {
            errorMessage = "Please Enter Additional Charge Remark";
            txt_Add_Charge_remark.Focus();
            return false;
        }
        else if (DiscountAmount > 0 && DiscountRemark == "")
        {
            errorMessage = "Please Enter Discount Remark";
            txt_discount_remark.Focus();
            return false;
        }
        else if (SubTotal < 0)
        {
            errorMessage = "Sub Total should be greater than Zero";
            return false;
        }
        else if (TaxAbate < 0)
        {
            errorMessage = "Tax Abate should be greater than Zero";
            return false;
        }
        else if (AmountTaxable < 0)
        {
            errorMessage = "Amount Taxable should be greater than Zero";
            return false;
        }
        else if (ServiceTax < 0)
        {
            errorMessage = "Service Tax should be greater than Zero";
            return false;
        }
        else if (OctrAmount < 0)
        {
            errorMessage = "Octroi Amount should be greater than Zero";
            return false;
        }
        else if (TotalReceivables <= 0)
        {
            errorMessage = "Total Receivable should be greater than Zero";
            return false;
        }
        else if (ReceivedBy == 1 && Document_ID == 3 && !WucMRCashChequeDetails1.validateWUCChequeDetails(lbl_Errors))
        {
            return false;
        }
        else if (ReceivedBy ==1 && Document_ID == 3 && WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = "Please Enter Valid Cheque Amount";
            return false;
        }


	else if (MenuItemId == 108 && ReceivedBy == 1 && ((WucMRCashChequeDetails1.CashAmount + WucMRCashChequeDetails1.ChequeAmount) != TotalReceivables))
        {
            errorMessage = "Sum Of Total Receivables Equal to Cash Amount and Cheque Amount";
            return false;
        }

        else if (ReceivedBy == 2 && Debit_To_Ledger_ID <= 0)
        {
            errorMessage = "Please Select Debit To Ledger";
            Txt_DebitLedger.Focus();
            return false;
        }
        else if (ReceivedBy == 2 && Debit_To_Branch_ID <= 0)
        {
            errorMessage = "Please Select Debit To Branch";
            Txt_DebitBranch.Focus();
            return false;
        }
        else if (WucMRDeliveryDetails1.validateUI() == false && CompanyManager.getCompanyParam().ClientCode.ToLower()== "excel" && MenuItemId == 108 )
        {
            MultiPage1.SelectedIndex = 1;
            TabStrip1.SelectedTab = TabStrip1.Tabs[1];
            //UpdatePanel20.Update();
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion
    #region Other Methods
    public void ClearVariables()
    {
        MRCashChequeDetailsView.Session_ChequeDetailsGrid = null;
        MRCashChequeDetailsView.Session_ddl_DepositIn = null;
        
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            rbtn_CMFConsignee.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        WucMRDeliveryDetails1.SetScriptManager = ScriptManager1;

        Document_ID = Util.String2Int(StateManager.GetState<string>("QueryString").ToString());
        //int MenuItemId = Common.GetMenuItemId();
        MenuItemId = Common.GetMenuItemId();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.FinanceModel.MRDeliveryModel));

        if (Document_ID == 3)
        objMRCashChequePresenter = new MRCashChequePresenter(WucMRCashChequeDetails1, IsPostBack);

        objMRDeliveryPresenter = new MRDeliveryPresenter(this, IsPostBack);

        btn_Save.Attributes.Add("onclick", objcommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Print, btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objcommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save, btn_Close));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel" && MenuItemId == 108)
        {
            TabStrip1.Tabs[1].Visible = true;
        }
        else
        {
            TabStrip1.Tabs[1].Visible = false;
        }
        if (Document_ID == 8)
            //Page.Title = "Credit Memo";
            Page.Title = "Marfatiya Delivery Memo";
        else
            Page.Title = "Money Receipt (Delivery)";
        if (Document_ID == 8)
        {
            ddl_BillingBranch.Enabled = false; 
        }

	if (MenuItemId == 108)
        {
        	if (ReceivedBy == 1)
        	{
            		Set_DebitTo_LedgerID("", "0");
            		Set_DebitTo_BranchID("", "0");
       		}
        	else
        	{
            		WucMRCashChequeDetails1.CashAmount = 0;
            		WucMRCashChequeDetails1.ChequeAmount = 0;
            		WucMRCashChequeDetails1.CashLedgerID = 0;
        	}             
	}
  
        if (!IsPostBack)
        {
            
            lbl_Octr_Service_Charges.Text = "Octroi Service Charge :";
            Change_Text_For_Credit_Memo();
            hdn_KeyID.Value = keyID.ToString();           
            string Branch_Name = UserManager.getUserParam().MainName;
            int Branch_Id = UserManager.getUserParam().MainId;               
            Set_DebitTo_BranchID(Branch_Name.ToString(),Branch_Id.ToString());
           
            if (keyID <= 0)
            {
                //btn_Save.Enabled = false;
                //btn_Save_Print.Enabled = false;
                Credit_Memo_ForID = 3;
            }
            else
            {
                rbtn_CreditMemoFor.Enabled = false;
            }
        }

        WucMRGeneralDetails1.OnGetDetails += new EventHandler(SetValues);
        //WucMRGeneralDetails1.OnMRDateChange += new EventHandler(SetValues_OnMrDateChange);
        WucMRCashChequeDetails1.Scmcheque = ScriptManager1;

        MRGeneralDetailsView.MR_Type_ID = 2;
       
        String Script = "<script type='text/javascript'>Hide_Octr_Controls();</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);

         if (Document_ID == 8)
         {
            String Script1 = "<script type='text/javascript'>Disable_Control_On_Octroi();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script1", Script1, false);
         }
         SetStandardCaption();
         //UpdatePanel23.Update();
    }

    private void SetStandardCaption()
    {
        lbl_Total_Amount.Text = "Total " + CompanyManager.getCompanyParam().GcCaption + " Amount";
        lbl_GC_Total.Text = CompanyManager.getCompanyParam().GcCaption + " Amount (To Be Recd)";
    }


    public void SetValues(object sender, EventArgs e)
    {
        if (MRGeneralDetailsView.GCNo == string.Empty || MRGeneralDetailsView.GC_ID == 0)
        {
            //btn_Save.Enabled = false;
            //btn_Save_Print.Enabled = false;
            txt_TotalReceivables.Text = "";
            MRCashChequeDetailsView.CashAmount = 0;
            MRCashChequeDetailsView.ChequeAmount = 0;
            MRGeneralDetailsView.Total_Receivables = 0;
            MRGeneralDetailsView.GC_ID = 0;
            if (Document_ID == 3)
            {
                MRCashChequeDetailsView.Session_ChequeDetailsGrid.Clear();
                MRCashChequeDetailsView.Bind_ChequeDetailsGrid = MRCashChequeDetailsView.Session_ChequeDetailsGrid;
            }
            MRCashChequeDetailsView.Total_ChequeAmount = 0;
            Clear_Fields();
        }
        else
        {
            //btn_Save.Enabled = true;
            //btn_Save_Print.Enabled = true;

            Set_Values_For_Delivery();

            if (MRGeneralDetailsView.Is_MR_FirstTime == false)
                  Set_Values_MultipleDelivery();

              if (Document_ID == 8 && (MRGeneralDetailsView.Is_CreditMemoOctroi_FirstTime == false || OctrPaidBy != "3"))
              {
                  rbtn_CreditMemoFor.Items[1].Enabled = false;
                  rbtn_CreditMemoFor.Items[2].Enabled = false;
                  Credit_Memo_ForID = 1;
              }
        }

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }

    private void Change_Text_For_Credit_Memo()
    {
        if (Document_ID == 8)
        {
            Rbl_Receivedby.Items[0].Enabled = false;
            Rbl_Receivedby.Items[1].Selected = true;
            //TabStrip1.Tabs[0].Text = "Credit Memo";
            TabStrip1.Tabs[0].Text = "Marfatiya Delivery Memo";
            tr_ReceivedBy.Style.Add("display", "none");
            tr_Credit_Memo_for.Style.Add("display", "inline");
            TD_CrMemoForText.Style.Add("display", "inline");
            TD_CrMemoForValue.Style.Add("display", "inline");
            tr_TotalAmtCreditFor.Style.Add("display", "none");
            tr_Credit_Memo_for.Style.Add("display", "none");
        }
        else
        {
            TabStrip1.Tabs[0].Text = "Money Receipt(Delivery)";
            tr_ReceivedBy.Style.Add("display", "inline");
            tr_Credit_Memo_for.Style.Add("display", "none");
            TD_CrMemoForText.Style.Add("display", "none");
            TD_CrMemoForValue.Style.Add("display", "none");
        }
    }

    public void Set_Values_For_Delivery()
    {
        TimeSpan DateDiff = new TimeSpan();
        DateTime AUS_Date = new DateTime();

        _ds = objMRDeliveryPresenter.Get_Delivery_Details();

        if (_ds.Tables[0].Rows.Count > 0)
        {
            DataRow DR = _ds.Tables[0].Rows[0];
            DataRow DR1 = _ds.Tables[1].Rows[0];

            if (DR["AUS_Date"].ToString() == "" || DR["Del_Date"].ToString() == "")
            {
                DemurageCharges = 0;
            }
            else
            {
                AUS_Date = Convert.ToDateTime(DR["AUS_Date"]);
                Delivery_Date = Convert.ToDateTime(DR["Del_Date"]);
                DeliveryDate = Convert.ToDateTime(DR["Del_Date"]);

                DateDiff = Delivery_Date.Subtract(AUS_Date);

                if ((DateDiff.Days - Util.String2Int(DR["Demurrage_Days"].ToString())) <= 0)
                {
                    DemurageDays = 0;
                    DemurageCharges = 0;
                    Std_Demurage_Charges = 0;
                }
                else
                {
                    DemurageDays = DateDiff.Days - Util.String2Int(DR["Demurrage_Days"].ToString());

                    DemurageCharges = ((DateDiff.Days - Util.String2Int(DR["Demurrage_Days"].ToString()))
                                       * Util.String2Decimal(DR["Demurrage_Rate_Kg_Per_Day"].ToString())
                                       * Util.String2Decimal(DR1["Charged_Weight"].ToString()));

 		    DemurageCharges = Math.Round(DemurageCharges);
                    Std_Demurage_Charges = DemurageCharges;
                }
            }
                                 
            Set_Values_Delivery_Add_Edit(_ds);

            if (chk_Is_Octr.Checked == true)
            {
                OctrFormCharges = Util.String2Decimal(DR["Octroi_Form_Charges"].ToString());
                Std_Octroi_Form_Charges = Util.String2Decimal(DR["Octroi_Form_Charges"].ToString());

                lbl_Octr_Service_Charges.Text = "Octroi Service Charges (" +
                                                    DR["Octroi_Service_Charges"].ToString()
                                                    + "% ) :";

                OctrServiceCharges = (Util.String2Decimal(DR["Octroi_Service_Charges"].ToString()) * OctrAmount) / 100;
                Std_Octroi_Service_Charges = (Util.String2Decimal(DR["Octroi_Service_Charges"].ToString()) * OctrAmount) / 100;
		
		OctrServiceCharges = Math.Round(OctrServiceCharges);
                Std_Octroi_Service_Charges = Math.Round(Std_Octroi_Service_Charges);

            }
            GICharges = Util.String2Decimal(DR["GI_Charges"].ToString());
            Std_GI_Charges = Util.String2Decimal(DR["GI_Charges"].ToString());

            if ((Util.String2Decimal(DR["Hamali_Per_Kg"].ToString()) * Charged_Wt)
                 < Util.String2Decimal(DR["Min_Hamali"].ToString()))
            {
                HamaliCharges = Util.String2Decimal(DR["Min_Hamali"].ToString());
                Std_Hamali_Charges = Util.String2Decimal(DR["Min_Hamali"].ToString());
            }
            else
            {
                HamaliCharges = Util.String2Decimal(DR["Hamali_Per_Kg"].ToString()) * Charged_Wt;
                Std_Hamali_Charges = Util.String2Decimal(DR["Hamali_Per_Kg"].ToString()) * Charged_Wt;
            }

            hdn_Other_Charges.Value = DR["Other_Charges"].ToString();
            Service_Tax_Percent = Util.String2Decimal(DR["Service_Tax_Percent"].ToString());

            hdn_RoundOff.Value = DR["Round_Off"].ToString();
            RoundOff = Util.String2Int(DR["Round_Off"].ToString());


            String Script = "<script type='text/javascript'>Hide_Octr_Controls(); </script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);

            String Script1 = "<script type='text/javascript'>Calculate_GrandTotal(); </script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script1", Script1, false);
        }
    }

    public void Set_Values_Delivery_Add_Edit(DataSet ds)
    {

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow DR = ds.Tables[0].Rows[0];
            DataRow DR1 = ds.Tables[1].Rows[0];

            Payment_Type_ID = Util.String2Int(DR1["Payment_Type_Id"].ToString());
            Booking_Type_ID = Util.String2Int(DR1["Booking_Type_Id"].ToString());
            OctrAmount = Util.String2Decimal(DR1["Oct_Amount"].ToString());
            GCSubTotal = Util.String2Decimal(DR1["Sub_Total"].ToString());
            GC_Total_Amount = Util.String2Decimal(DR1["Total_GC_Amount"].ToString());
            Charged_Wt = Util.String2Decimal(DR1["Charged_Weight"].ToString());
            Advance_Amount = Util.String2Decimal(DR1["Advance_Amount"].ToString());
            Service_Tax_On_Advance = Util.String2Decimal(DR1["Service_Tax_On_Advance"].ToString());
            TotalAmount = GC_Total_Amount;

            OctrRecNo = DR1["Oct_Receipt_No"].ToString();
            OctrRecDate = Convert.ToDateTime(DR1["Oct_Bill_Date"]);
            OctrFormType = DR1["Octroi_Form_Type"].ToString();
            OctrPaidBy = DR1["Octroi_Paid_By"].ToString();

            chk_Is_Octr.Checked = Convert.ToBoolean(DR["Is_Octroi"]);
            chk_Is_Octr_To_Be_Added_In_MR.Checked = Util.String2Bool(DR1["To_Be_Added_In_MR"].ToString());
            chk_Is_Oct_Recovered_From_Consignee.Checked = Util.String2Bool(DR1["Is_Oct_Recovered_From_Consignee"].ToString());

            chk_Is_Service_Tax_App_Commodity.Checked = Convert.ToBoolean(DR1["Is_Service_Tax_Applicable"]);
            chk_Is_Service_Tax_by_Consignee.Checked = Convert.ToBoolean(DR1["Is_Consignee_Service_Tax_Applicable"]);

            if (chk_Is_Service_Tax_by_Consignee.Checked == true)
            {
                ServiceTaxBy = "Consignee";
                Service_Pay_By_ID = 2;//3;
            }
            else
            {
                ServiceTaxBy = "Transporter";
                Service_Pay_By_ID = 3;//1;
            }

            if (Payment_Type_ID == 1)
            {
                GCTotal = Convert.ToString(GCSubTotal - Advance_Amount);

                if (Service_Pay_By_ID == 3) // Transporter
                {
                    GCTotal = Convert.ToString(GCSubTotal - Advance_Amount + Service_Tax_On_Advance);
                }
            }
            else
            {
                GCTotal = GC_Total_Amount.ToString();
            }           
            if (keyID > 0)
            {
                //OctrAmount = Util.String2Decimal(DR["Octroi_Amount"].ToString());

                if (Payment_Type_ID == 1)
                {
                    GCTotal = Convert.ToString(Util.String2Decimal(DR["GC_Sub_Total"].ToString()) - Advance_Amount);

                    if (Service_Pay_By_ID == 3) // Transporter
                    {
                        GCTotal = Convert.ToString(GCSubTotal - Advance_Amount + Service_Tax_On_Advance);
                    }
                }
                else
                {
                    GCTotal = DR["GC_Sub_Total"].ToString();
                }

                GCSubTotal = Util.String2Decimal(DR["GC_Sub_Total"].ToString());
            }
            

            if (chk_Is_Octr.Checked == true)
            {
                txt_Octr_Form_Charges.Enabled = true;
                txt_Octr_Service_Charges.Enabled = true;
            }
            else
            {
                OctrFormCharges = 0;
                Std_Octroi_Form_Charges = 0;
                lbl_Octr_Service_Charges.Text = "Octroi Service Charges :";
                OctrServiceCharges = 0;
                Std_Octroi_Service_Charges = 0;
                txt_Octr_Form_Charges.Enabled = false;
                txt_Octr_Service_Charges.Enabled = false;
            }

            if (DemurageDays == 0)
            {
                if (keyID <= 0)
                {
                    DemurageCharges = 0;
                }
                txt_Demurage_Charges.Enabled = false;
            }
            else
            {
                txt_Demurage_Charges.Enabled = true;
            }

            hdn_Other_Charges.Value = DR["Other_Charges"].ToString();
            hdn_Octr_Pay_Type_ID.Value = DR1["Octroi_Paid_By_ID"].ToString();
            hdn_Octr_Form_Type_ID.Value = DR1["Octroi_Form_Type_ID"].ToString();

            hdn_Oct_Form_Chg_Discount.Value = DR["Dly_Oct_Form_Chg_Discount_Percent"].ToString();
            hdn_Oct_Service_Chg_Discount.Value = DR["Dly_Oct_Service_Chg_Discount_Percent"].ToString();
            hdn_GI_Chg_Discount.Value = DR["Dly_GI_Chg_Discount_Percent"].ToString();
            hdn_Hamali_Chg_Discount.Value = DR["Dly_Hamali_Chg_Discount_Percent"].ToString();
            hdn_Demurage_Chg_Discount.Value = DR["Dly_Demurrage_Chg_Discount_Percent"].ToString();
            hdn_RoundOff.Value = DR["Round_OFf"].ToString();   
                     
        }
        
        if (MRGeneralDetailsView.Is_MR_FirstTime == false)//For multiple delivery
        {
            txt_Octr_Form_Charges.Enabled = false;
            txt_Octr_Service_Charges.Enabled = false;
        }
    }

    public void Calculate_Grand_Total()
    {
        decimal GC_Amt = 0;
        GC_Amt = GCSubTotal;
        decimal Grand_Total = 0;

        hdn_Additional_Charges.Value = AdditionalCharges.ToString();
        hdn_Discount_Amount.Value = DiscountAmount.ToString();

        if (Payment_Type_ID != 1)
        {
            GC_Amt = 0;
        }
        //Start added on 10-12-13
        if (Payment_Type_ID == 1)
        {
            GC_Amt = Util.String2Decimal(txt_Total_Amount.Text);
        }
        //End added on 10-12-13

        SubTotal = GC_Amt + OctrServiceCharges + OctrFormCharges + DemurageCharges + Util.String2Decimal(hdn_Other_Charges.Value)
                   + DetentionCharges + GICharges + HamaliCharges + LocalCharges + AdditionalCharges + DeliveryCommission;

        if (Payment_Type_ID == 1) // To Pay
        {
            SubTotal = SubTotal - Advance_Amount;
        }

        if(Document_ID == 8 && rbtn_CreditMemoFor.SelectedValue == "1")
        {
            SubTotal = GC_Amt + DemurageCharges + Util.String2Decimal(hdn_Other_Charges.Value) + DetentionCharges + GICharges + HamaliCharges + LocalCharges + AdditionalCharges + DeliveryCommission;
        }
        else if (Document_ID == 8 && rbtn_CreditMemoFor.SelectedValue == "2")
        {
            SubTotal = OctrServiceCharges + OctrFormCharges;
        }

        SubTotal = Math.Round(SubTotal,2);

        if (DiscountAmount > SubTotal)
        {
            DiscountAmount = 0;
        }
        else
        {
            SubTotal = SubTotal - DiscountAmount;
        }

        SubTotal = Math.Round(SubTotal,2);

        //Start added on 10-12-13
        if (Payment_Type_ID == 1)
        {
            SubTotal = SubTotal - Util.String2Decimal(txt_Total_Amount.Text);
        }        
        //End added on 10-12-13

        TaxAbate = SubTotal * Convert.ToDecimal(0.75);
        if (Booking_Type_ID == 1 && SubTotal < 750) TaxAbate = 0;
        if (Booking_Type_ID == 2 && SubTotal < 1500) TaxAbate = 0;
        if (chk_Is_Service_Tax_by_Consignee.Checked == true ) TaxAbate = 0;
        if (chk_Is_Service_Tax_App_Commodity.Checked == false) TaxAbate = 0;
        TaxAbate = Math.Round(TaxAbate);       

        AmountTaxable = SubTotal - TaxAbate;
        if (Booking_Type_ID == 1 && SubTotal < 750) AmountTaxable = 0;
        if (Booking_Type_ID == 2 && SubTotal < 1500) AmountTaxable = 0;
        if (chk_Is_Service_Tax_by_Consignee.Checked == true) AmountTaxable = 0;
        if (chk_Is_Service_Tax_App_Commodity.Checked == false) AmountTaxable = 0;
        AmountTaxable = Math.Round(AmountTaxable);      

        ServiceTax = (Util.String2Decimal(hdn_Service_Tax_Percent.Value)/100) * AmountTaxable;
        if (Booking_Type_ID == 1 && SubTotal < 750) ServiceTax = 0;
        if (Booking_Type_ID == 2 && SubTotal < 1500) ServiceTax = 0;
        if (chk_Is_Service_Tax_by_Consignee.Checked == true) ServiceTax = 0;
        if (chk_Is_Service_Tax_App_Commodity.Checked == false) ServiceTax = 0;
        ServiceTax = Math.Round(ServiceTax);

        //Start added on 23-12-13
        if (Payment_Type_ID == 1)
        {
            SubTotal = SubTotal + Util.String2Decimal(txt_Total_Amount.Text);
        }
        //End added on 23-12-13
        if (chk_Is_Octr_To_Be_Added_In_MR.Checked == false) OctrAmount = 0;

        if(chk_Is_Oct_Recovered_From_Consignee.Checked == false) OctrAmount = 0;

        if(OctrPaidBy != "3") OctrAmount = 0;

        if (Document_ID == 8 && rbtn_CreditMemoFor.SelectedValue == "1") OctrAmount = 0;  // added by shiv

        if(chk_Is_Octr.Checked == false) OctrAmount = 0;

        if (chk_Is_Service_Tax_by_Consignee.Checked == true && chk_Is_Oct_Recovered_From_Consignee.Checked == true) //service tax by client
          Grand_Total = SubTotal + OctrAmount;
        else
          Grand_Total = SubTotal + ServiceTax + OctrAmount ;

        if (Service_Pay_By_ID == 3) Grand_Total = Grand_Total + Service_Tax_On_Advance;

        Grand_Total = Math.Round(Grand_Total,2);

        TotalReceivables = Grand_Total + RoundOff;

       

        String Script = "<script type='text/javascript'>Hide_Octr_Controls(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);      
    
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        Calculate_Grand_Total();
        objMRDeliveryPresenter.Save();
    }

    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        Calculate_Grand_Total();
        objMRDeliveryPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        string CloseScript = "<script language='javascript'>{self.close()}</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseScript()", CloseScript, false);
    }

    public void Clear_Fields()
    {
        Payment_Type_ID = 0;
        Booking_Type_ID = 0;
        OctrAmount = 0;
        GCSubTotal = 0;
        GC_Total_Amount = 0;
        Charged_Wt = 0;
        TotalAmount = GC_Total_Amount;
        GCTotal = "0";
        OctrRecNo = "";
        OctrRecDate = Convert.ToDateTime("1900-01-01");
        Delivery_Date = Convert.ToDateTime("1900-01-01");
        OctrFormType = "";
        OctrPaidBy = "";
        //chk_Is_Octr.Checked = false;
        chk_Is_Octr_To_Be_Added_In_MR.Checked = false;
        chk_Is_Oct_Recovered_From_Consignee.Checked = false;
        chk_Is_Service_Tax_App_Commodity.Checked = false;
        chk_Is_Service_Tax_by_Consignee.Checked = false;
        ServiceTaxBy = "";
        Service_Pay_By_ID = 0;
        hdn_Other_Charges.Value = "0";
        hdn_Octr_Pay_Type_ID.Value = "0";
        hdn_Octr_Form_Type_ID.Value = "0";
        OctrFormCharges = 0;
        Std_Octroi_Form_Charges = 0;
        lbl_Octr_Service_Charges.Text = "Octroi Service Charges :";
        OctrServiceCharges = 0;
        Std_Octroi_Service_Charges = 0;
        GICharges = 0;
        Std_GI_Charges = 0;
        HamaliCharges = 0;
        Std_Hamali_Charges = 0;
        DemurageCharges = 0;
        Std_Demurage_Charges = 0;
        DemurageDays = 0;
        hdn_Other_Charges.Value = "0";
        Service_Tax_Percent = 0;
        LocalCharges = 0;
        AddChrgRemark = "";
        DiscountRemark = "";
        AdditionalCharges = 0;
        DiscountAmount = 0;
        SubTotal = 0;
        TaxAbate = 0;
        AmountTaxable = 0;
        Service_Tax_Percent = 0;
        ServiceTax = 0;
        ServiceTaxBy = "";
        TotalReceivables = 0;
        hdn_Oct_Form_Chg_Discount.Value = "0";
        hdn_Oct_Service_Chg_Discount.Value = "0";
        hdn_GI_Chg_Discount.Value = "0";
        hdn_Hamali_Chg_Discount.Value = "0";
        hdn_Demurage_Chg_Discount.Value = "0";
        RoundOff = 0;
        hdn_RoundOff.Value = "0"; 

        String Script = "<script type='text/javascript'>Hide_Octr_Controls(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);

        String Script1 = "<script type='text/javascript'>Calculate_GrandTotal(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script1", Script1, false);
    }


    public void Set_Values_MultipleDelivery()
    {

        GCSubTotal = 0;
        GCTotal = "0";
        Advance_Amount = 0;
        Service_Tax_On_Advance = 0;

        if (Document_ID == 3)
        {
            OctrAmount = 0;
            GC_Total_Amount = 0;

            OctrFormCharges = 0;
            Std_Octroi_Form_Charges = 0;
            OctrServiceCharges = 0;
            Std_Octroi_Service_Charges = 0;
            SubTotal = 0;
            TaxAbate = 0;
            AmountTaxable = 0;
            Service_Tax_Percent = 0;
            ServiceTax = 0;
            TotalReceivables = 0;
            hdn_Oct_Form_Chg_Discount.Value = "0";
            hdn_Oct_Service_Chg_Discount.Value = "0";
            lbl_Octr_Service_Charges.Text = "Octroi Service Charge :";             
        }

        hdn_Other_Charges.Value = "0";
        GICharges = 0;
        Std_GI_Charges = 0;
        HamaliCharges = 0;
        Std_Hamali_Charges = 0;
        DemurageCharges = 0;
        Std_Demurage_Charges = 0;
        DemurageDays = 0;
        hdn_Other_Charges.Value = "0";
        LocalCharges = 0;
        AdditionalCharges = 0;
        DiscountAmount = 0;
        hdn_GI_Chg_Discount.Value = "0";
        hdn_Hamali_Chg_Discount.Value = "0";
        hdn_Demurage_Chg_Discount.Value = "0";

        hdn_RoundOff.Value = "0";
        RoundOff = 0;
    }


    //public void SetValues_OnMrDateChange(object sender, EventArgs e)
    //{
    //    if (WucMRGeneralDetails1.MR_Type_ID == 2) //For Both MR Delivery And Credit Memo
    //    {
    //        Service_Tax_Percent = objcommon.Get_Service_Tax_Percent(WucMRGeneralDetails1.MRDate);

    //        String Script2 = "<script type='text/javascript'>Calculate_GrandTotal(); </script>";
    //        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script2", Script2, false);
    //    }
    //}   
}
