
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
using System.Drawing;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationModel;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;

using Raj.EC.ControlsView;
 

public partial class EC_Master_wucDirectDelivery : System.Web.UI.UserControl, IDirectDeliveryView
{
    private DirectDeliveryPresenter objDirectDeliveryPresenter;
    MRCashChequePresenter objMRCashChequePresenter;
    bool isValid = false;
    Raj.EC.Common objComm = new Raj.EC.Common();
    string _flag = "";
    string Mode = "0";

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]); }
    }
    public string Flag
    {
        get { return _flag; }
    }
    #endregion

    #region InitInterface

    public IVehicleSearchView VehicleSearchView
    {
        get { return (IVehicleSearchView)WucVehicleSearch1; }
    }
    public IMRCashChequeDetailsView MRCashChequeDetailsView
    {
        get { return (IMRCashChequeDetailsView)WucMRCashChequeDetails1; }
    }       
    
    #endregion

    #region ControlsBind
        
    //public DataSet BindGC
    //{
    //    set
    //    {
    //        ddl_GCNo.DataSource = value;
    //        ddl_GCNo.DataTextField = "GC_No_For_Print";
    //        ddl_GCNo.DataValueField = "GC_Id";

    //        ddl_GCNo.DataBind();
    //    }
    //}

    //Commented On 21 Aug 2018
    //public DataSet BindLHPO
    //{
    //    set
    //    {
    //        ddl_LHPO.DataSource = value;
    //        ddl_LHPO.DataTextField = "LHPO_No_For_Print";
    //        ddl_LHPO.DataValueField = "LHPO_ID";

    //        ddl_LHPO.DataBind();
    //    }
    //}

    //Added On 21 Aug 2018
    public DataSet BindLHPO
    {
        set
        {
            try
            {
                ddl_LHPO.DataSource = value;
                ddl_LHPO.DataTextField = "LHPO_No_For_Print";
                ddl_LHPO.DataValueField = "LHPO_ID";

                ddl_LHPO.DataBind();
            }
            catch (ArgumentException argu)
            {
                string e = argu.Message;
            }
        }
    }



    public DataTable   BindResionForLateDelivery
    {
        set
        {
            ddl_Reason_For_Late_Delivery.DataSource = value;
            ddl_Reason_For_Late_Delivery.DataTextField = "Reason";
            ddl_Reason_For_Late_Delivery.DataValueField = "Reason_Id";              
            ddl_Reason_For_Late_Delivery.DataBind();
            ddl_Reason_For_Late_Delivery.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable  BindDeliveryCondition      
    {
        set
        {
            ddl_Delivery_Condintion.DataSource = value;
            ddl_Delivery_Condintion.DataTextField = "Received_Condition";
            ddl_Delivery_Condintion.DataValueField = "Received_Condition_ID";
            ddl_Delivery_Condintion.DataBind();
        }
    }

    public int DeliveryToID
    {         
        set 
        {
            if (value <= 0)
            {
                value = 0;
            }
            WucDeliveryOtherDetails1.DeliveryToID = value; 
        }
        get { return WucDeliveryOtherDetails1.DeliveryToID; }
    }
    public int ConsigneeCopyID
    {
        set
        {
            if (value <= 0)
            {
                value = 0;
            } 
            WucDeliveryOtherDetails1.ConsigneeCopyID = value;
            if (value ==2)
            {
                WucDeliveryOtherDetails1.SetDeliveryAgainstVisibility = "";
            }
        }
        get { return WucDeliveryOtherDetails1.ConsigneeCopyID; }
    }
    public int DeliveryAgainstID
    {
        set
        {
            if (value <= 0)
            {
                value = 0;
            } 
            WucDeliveryOtherDetails1.DeliveryAgainstID = value;
        }
        get { return WucDeliveryOtherDetails1.DeliveryAgainstID; }
    }    
     
    public int GC_Id
    {
        //get { return Util.String2Int(ddl_GCNo.SelectedValue); }
        get
        {
            return Util.String2Int(hdn_GC_Id.Value);
        }
    }
    public int Vehicle_Id
    {
        set{hdn_Vehicle_Id.Value = Util.Int2String(value); }
        get{ return Util.String2Int(hdn_Vehicle_Id.Value); }
    }

    public int NoofMinuteDifferenceForLate
    {
        set { hdn_TimeDiffernceforLate.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_TimeDiffernceforLate.Value); }
    }

    public int Booking_Branch_Id
    {
        set { hdn_BookingBranchId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_BookingBranchId.Value); }
    }
    public int Booking_Articles
    {
        set {lbl_BookingArticlesValue.Text = Util.Int2String(value); }
        get {return Util.String2Int(lbl_BookingArticlesValue.Text); }
    }
    public int Loaded_Articles
    {
        set { lbl_LoadingArticlesValue.Text = Util.Int2String(value);}
        get  { return Util.String2Int(lbl_LoadingArticlesValue.Text); }
    }
    public int Delivered_Articles
    {
        set
        {
            txt_DeliveredArticles.Text = Util.Int2String(value);
            hdn_DeliveredArticles.Value = Util.Int2String(value);
        }
        get
        {
            //return Util.String2Int(txt_DeliveredArticles.Text);
            return Util.String2Int(hdn_DeliveredArticles.Value);
        }
    }
    public int Damage_Leakage_Articles
    {
        set{ txt_DamageLeakageArticle.Text = Util.Int2String(value);}
        get{ return Util.String2Int(txt_DamageLeakageArticle.Text); }
    }
    public int Short_Articles
    {
        set{ lbl_ShortArticlesValue.Text = Util.Int2String(value); }
        get{return Util.String2Int(lbl_ShortArticlesValue.Text); }
    }

    public int Delivery_Location_Id
    {
        set { hdn_DeliveryLocationId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_DeliveryLocationId.Value); }
    }
    public int Payment_Type_Id
    {
        set{ hdn_PaymentTypeId.Value = Util.Int2String(value); }
        get {return Util.String2Int(hdn_PaymentTypeId.Value);}
    }

    public int LHPO_Id
    {
        set
        {
            ddl_LHPO.SelectedValue = Util.Int2String(value);
            hdn_LHPO_Id.Value = Util.Int2String(value);
        }
        get
        {
            //return Util.String2Int(ddl_LHPO.SelectedValue);
            return Util.String2Int(hdn_LHPO_Id.Value);
        }
    }

    public int Memo_Id
    {
        set{hdn_Memo_Id.Value = Util.Int2String(value); }
        get {return Util.String2Int(hdn_Memo_Id.Value); }
    }

    public int Delivery_Condition
    {
        set{ddl_Delivery_Condintion.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Delivery_Condintion.SelectedValue); }
    }

    public int Reason_For_Late_Uploading
    {
        set { ddl_Reason_For_Late_Delivery.SelectedValue = Util.Int2String(value);  }
        get {return Util.String2Int(ddl_Reason_For_Late_Delivery.SelectedValue); }
    }

    public int Vehicle_Category_Id
    {
        set { hdn_Vehicle_Category_Id.Value = Util.Int2String(value);}
        get { return Util.String2Int(hdn_Vehicle_Category_Id.Value);}
    }

    public int Previous_Article_ID
    {
        set { hdn_Previous_Article_ID.Value = Util.Int2String(value);}
        get{ return Util.String2Int(hdn_Previous_Article_ID.Value); }
    }

    public int Previous_Status_ID
    {
        set { hdn_Previous_Status_ID.Value = Util.Int2String(value);}
        get{return Util.String2Int(hdn_Previous_Status_ID.Value); }
    }
    public int Previous_Document_ID
    {
        set {hdn_Previous_Document_ID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Previous_Document_ID.Value); }
    }

    public void SetGC(string text, string value)
    {
        ddl_GCNo.DataTextField = "GC_No_For_Print";
        ddl_GCNo.DataValueField = "GC_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_GCNo );
        hdn_GC_Id.Value = value;
        hdn_GC_No_For_Print.Value = text;
    }

    public void SetLHPO(string text, string value)
    {
        ddl_LHPO.DataTextField = "LHPO_No_For_Print";
        ddl_LHPO.DataValueField = "LHPO_ID";

        ddl_LHPO.Items.Insert(0, new ListItem(text, value));
        hdn_LHPO_Id.Value = value;
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

    public Decimal Total_GC_Amount
    {
        set{lbl_TotalGCAmountValue.Text = Util.Decimal2String(value); }
        get {return Util.String2Decimal(lbl_TotalGCAmountValue.Text); }
    }

    public Decimal  Booking_Articles_Weight
    {
        set{lbl_BookingArticlesWeightValue.Text = Util.Decimal2String(value); }
        get{return Util.String2Decimal(lbl_BookingArticlesWeightValue.Text); }
    }
    public Decimal Loaded_Articles_Weight
    {
        set {lbl_LoadingArticleWeightValue.Text = Util.Decimal2String(value); }
        get {return Util.String2Decimal(lbl_LoadingArticleWeightValue.Text); }
    }
    public Decimal Delivered_Articles_Weight
    {
        set {txt_DeliveredArticlesWeight.Text = Util.Decimal2String(value); }
        get{return Util.String2Decimal(txt_DeliveredArticlesWeight.Text); }
    }
    public Decimal Damage_Leakage_Articles_Value
    {
        set { txt_DamageLeakageValue.Text = Util.Decimal2String(value);}
        get { return Util.String2Decimal(txt_DamageLeakageValue.Text); }
    }

    public String  Booking_Branch
    {
        set {lbl_BookingBranchValue.Text = value;}
        get{return lbl_BookingBranchValue.Text;}
    }

    public String DDC_No_For_Print
    {
        set{lbl_DDCNoValue.Text = value;}
        get { return lbl_DDCNoValue.Text;}
    }
    public String Delivery_Location
    {
        set{ lbl_DeliveryLocationValue.Text = value; }
        get{return lbl_DeliveryLocationValue.Text;  }
    }
    public String Payment_Type 
    {
        set  {lbl_PaymentTypeValue.Text = value;}
        get  {return lbl_PaymentTypeValue.Text; }
    }
 
    public String Vehicle_Category
    {
        set{lbl_Vehicle_Category.Text = value; }
        get{return lbl_Vehicle_Category.Text; }
    }
    public String LHPO_Date
    {
        set{lbl_LHPODateValue.Text = value;}
        get{return lbl_LHPODateValue.Text;}
    }
    public String Memo_Date
    {
        set{lbl_MemoDateValue.Text = value;}
        get{return lbl_MemoDateValue.Text; }
    }
    public String Memo_No
    {
        set {lbl_MemoNoValue.Text = value; }
        get{ return lbl_MemoNoValue.Text; }
    }     
    public String ScheduledArivalDate
    {
        set{ lbl_ExpectedDeliveryDateValue.Text = value;}
        get{return lbl_ExpectedDeliveryDateValue.Text; }
    }
    public String BookingDate
    {
        set{lbl_GcBookingDateValue.Text = value;}
        get{return lbl_GcBookingDateValue.Text;}
    }
    public String ScheduledArivalTime
    {
        set{lbl_ExpectedDeliveryTimeValue.Text = value;}
        get{return lbl_ExpectedDeliveryTimeValue.Text; }
    }
    public String LHPO_From
    {
        set{lbl_LHPOFromValue.Text = value; }
        get{return lbl_LHPOFromValue.Text; }
    }
    public String LHPO_To
    {
        set{ lbl_LHPOToValue.Text = value; }
        get{return lbl_LHPOToValue.Text; }
    }
    public String Memo_From
    {
        set{ lbl_MemoFromValue.Text = value; }
        get {  return lbl_MemoFromValue.Text;}
    }
    public String Memo_To
    {
        set{lbl_MemoToValue.Text = value;}
        get{return lbl_MemoToValue.Text;}
    }
    public String Delivery_Time
    {
        set{wuc_ActualDeliveryTime.setTime(value); }
        get{return wuc_ActualDeliveryTime.getTime();}
    }
    public String Previous_Document_No_For_Print
    {
        set
        {
            hdn_Previous_Document_No_For_Print.Value = value;
        }
        get
        {
            return hdn_Previous_Document_No_For_Print.Value;
        }
    }    
    public String Delivery_Taken_By
    {
        set
        {
            txt_DeliveryTakenBy.Text = value;
        }
        get
        {
            return txt_DeliveryTakenBy.Text;
        }
    }
    public String Remarks
    {
        set
        {
            txt_Remarks.Text = value;
        }
        get
        {
            return txt_Remarks.Text;
        }
    }
    public Boolean Is_PODReceived
    {
        set
        {
            chk_IsPODReceived.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsPODReceived.Checked);
        }
    }


    public Boolean Is_OctroiApplicable
    {
        set
        {
            hdn_Is_OctroiApplicable.Value = Convert.ToString(value);
        }
        get
        {
            return Convert.ToBoolean(hdn_Is_OctroiApplicable.Value);
        }
    }


    public Boolean Is_OctroiUpdated
    {
        set
        {
            hdn_Is_OctroiUpdated.Value = Convert.ToString(value);
        }
        get
        {
            return Convert.ToBoolean(hdn_Is_OctroiUpdated.Value);
        }
    } 
     

    public DateTime DDC_Date
    {
        set
        {
            wuc_DirectDeliveryDate.SelectedDate = value;
        }
        get
        {
            return wuc_DirectDeliveryDate.SelectedDate;
        }
    }
    public DateTime ActualDeliveryDate
    {
        set
        {
            wuc_ActualDeliveryDate.SelectedDate = value;
        }
        get
        {
            return wuc_ActualDeliveryDate.SelectedDate;
        }
    }

    public DateTime Previous_Document_Date
    {
        set
        {
            hdn_Previous_Document_Date.Value = Convert.ToString(value);
        }
        get
        {
            return Convert.ToDateTime(hdn_Previous_Document_Date.Value);
        }
    }
    public Boolean IsFreightReceived
    {
        set
        {
            chk_IsFreightReceived.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsFreightReceived.Checked);
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
    public int Debit_To_Ledger_ID
    {
        get { return Util.String2Int(ddl_DebitTo.SelectedValue); }
    }

    public int Debit_To_Branch_ID
    {
        get { return Util.String2Int(ddl_BillingBranch.SelectedValue); }
    }

    public decimal TDS
    {
        set { txt_TDS.Text  = Util.Decimal2String(Math.Round(value, 2)); }
        get { return txt_TDS.Text == string.Empty ? 0 : Util.String2Decimal(txt_TDS.Text); }
    }

    #endregion
    #region Function


    public bool validateUI()
    {
        isValid = false;
        TextBox Txt_DebitLedger, Txt_DebitBranch;
        errorMessage = "";

        Txt_DebitLedger = (TextBox)ddl_DebitTo.FindControl("txtBoxddl_DebitTo");
        Txt_DebitBranch = (TextBox)ddl_BillingBranch.FindControl("txtBoxddl_BillingBranch");

        if (Datemanager.IsValidProcessDate("OPR_DD", DDC_Date) == false)
        {
            errorMessage = "Please Enter Valid Delivery Date";
        }
        else if (ActualDeliveryDate > DDC_Date)
        {
            errorMessage = "Actual Delivery Date can't be greater than Direct Delivery Date.";
        }
        else if (LHPO_Date != "" && ActualDeliveryDate < Convert.ToDateTime(LHPO_Date))
        {
            //if (ActualDeliveryDate < Convert.ToDateTime(LHPO_Date))
            //{
                errorMessage = "Actual Delivery Date can't be less than LHPO Date.";
            //}
        }
        else if (Vehicle_Id <= 0)
        {
            errorMessage = "Please Select Vehicle.";
        }
        else if (GC_Id <= 0)
        {
            errorMessage = "Please Select one " + CompanyManager.getCompanyParam().GcCaption;
        }

        else if (Is_OctroiApplicable == true && Is_OctroiUpdated == false)
        {
            errorMessage = "Octroi is not Updated, Please Update Octroi.";
        }
        else if (LHPO_Id <= 0)
        {
            errorMessage = "Please Select LHPO.";
        }
        else if (Delivered_Articles <= 0)
        {
            errorMessage = "Delivered Arrival Should Be Greater Than Zero.";
        }
        else if (Delivered_Articles > Loaded_Articles)
        {
            errorMessage = "Delivered Articles can't be greater than Loaded Articles.";
        }
        else if (CheckDamageArticles() == false)
        {
            isValid = false;
            return isValid;
        }
        else if (Damage_Leakage_Articles > Delivered_Articles)
        {
            errorMessage = "Damaged Articles can't be greater than Delivered Articles.";
        }
        else if (Delivered_Articles_Weight > Loaded_Articles_Weight)
        {
            errorMessage = "Delivered Articles Weight can't be greater than Loaded Articles Weight";
        }
        //else if (CheckActualDeliveryDateTimeForReason() == false)
        //{
        //    isValid = false;
        //    return isValid;
        //}
        else if (Delivery_Taken_By.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Delivery Taken By";
            txt_DeliveryTakenBy.Focus();
        }
        else if (IsFreightReceived == true && ReceivedBy == 1 && !WucMRCashChequeDetails1.validateWUCChequeDetails(lbl_Errors))
        {
            return false;
        }
        else if (IsFreightReceived == true && ReceivedBy == 1 && WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = "Please Enter Valid Cheque Amount";
            return false;
        }
        else if (IsFreightReceived == true && ReceivedBy == 1 && (WucMRCashChequeDetails1.CashAmount + WucMRCashChequeDetails1.ChequeAmount + TDS) != Total_GC_Amount)
        {
            errorMessage = "Sum Of Total Receivables Equal to Cash Amount and Cheque Amount and TDS Amount";
            return false;
        }
        else if (IsFreightReceived == true && ReceivedBy == 2 && Debit_To_Ledger_ID <= 0)
        {
            errorMessage = "Please Select Debit To Ledger";
            Txt_DebitLedger.Focus();
            return false;
        }
        else if (IsFreightReceived == true && ReceivedBy == 2 && Debit_To_Branch_ID <= 0)
        {
            errorMessage = "Please Select Debit To Branch";
            Txt_DebitBranch.Focus();
            return false;
        }
        else
        {
            isValid = true;
        }

        if (isValid)
        {
            if (Delivery_Condition == 1)
            {
                Damage_Leakage_Articles = 0;
                Damage_Leakage_Articles_Value = 0;                 
            }            
        }
        return isValid;
    }
    public void ClearVariables()
    {
        MRCashChequeDetailsView.Session_ChequeDetailsGrid = null;
        MRCashChequeDetailsView.Session_ddl_DepositIn = null;

    }

    private bool CheckDamageArticles()
    {
        if (Delivery_Condition == 2 || Delivery_Condition == 3)
        {
            if (Damage_Leakage_Articles <= 0)
            {
                errorMessage = "Damage/Leakage Articles Should be Greater than Zero";
                txt_DamageLeakageArticle.Focus();
                return false;
            }
            else if (Damage_Leakage_Articles_Value <= 0)
            {
                errorMessage = "Damage/Leakage Value Should be Greater than Zero";
                txt_DamageLeakageValue.Focus();
                return false;
            }
        }
        return true;
    }
    private bool CheckActualDeliveryDateTimeForReason()
    {
        bool _isMandotary = false;

        int TimeDifference = 0;
        int DayDifference = 0;
        int TotalActualMinute = 0;
        int TotalExpectedMinute = 0;
        int MinuteDifference = 0;

        TimeSpan ActualTimespan, Expectedtimespan;
        ActualTimespan = Convert.ToDateTime(wuc_ActualDeliveryTime.getTime()).TimeOfDay;
        Expectedtimespan = Convert.ToDateTime(lbl_ExpectedDeliveryTimeValue.Text).TimeOfDay;

        TotalActualMinute = Convert.ToInt32(ActualTimespan.TotalMinutes);
        TotalExpectedMinute = Convert.ToInt32(Expectedtimespan.TotalMinutes);

        MinuteDifference = TotalActualMinute - TotalExpectedMinute;
        DayDifference = wuc_ActualDeliveryDate.SelectedDate.DayOfYear - Convert.ToDateTime(lbl_ExpectedDeliveryDateValue.Text).DayOfYear;

        if (DayDifference > 0)
        {
            MinuteDifference = MinuteDifference + (DayDifference * 24) * 60;
        }      
        if (MinuteDifference >= NoofMinuteDifferenceForLate)
        {
            _isMandotary = true;
        }
        if (_isMandotary == true)
        {
            if (Reason_For_Late_Uploading <= 0)
            {
                errorMessage = "Please Select Reason for Late Delivery";
                ddl_Reason_For_Late_Delivery.Focus();
                return false;
            }
        }
        return true;
    }

    private void SetStandardCaption()
    {        
        lbl_GcNo.Text = "Enter " + CompanyManager.getCompanyParam().GcCaption + "  No:";
        lbl_TotalGCAmount.Text = "Total " + CompanyManager.getCompanyParam().GcCaption + " Amount:";
        lbl_LHPONo.Text= CompanyManager.getCompanyParam().LHPOCaption + "  No:";
        lbl_LHPODate.Text= CompanyManager.getCompanyParam().LHPOCaption + " Date:";
        lblLHPOFrom.Text= CompanyManager.getCompanyParam().LHPOCaption + " From:";
        lbl_LHPOTo.Text = CompanyManager.getCompanyParam().LHPOCaption + " To:";        
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            wuc_ActualDeliveryDate.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Save_Exit,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit,btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        On_Load();

        objMRCashChequePresenter = new MRCashChequePresenter(WucMRCashChequeDetails1, IsPostBack);
        objDirectDeliveryPresenter = new DirectDeliveryPresenter(this, IsPostBack);
        ddl_BillingBranch.Enabled = false;


        if (ReceivedBy == 1)
        {
            Set_DebitTo_LedgerID("", "0");
            Set_DebitTo_BranchID("", "0");

             
            //lbl_DebitTo.Visible = false;
            //lbl_BillingBranch.Visible = false;

            //ddl_DebitTo.Visible = false;
            //ddl_BillingBranch.Visible = false;
        }
        else
        {
            WucMRCashChequeDetails1.CashAmount = 0;
            WucMRCashChequeDetails1.ChequeAmount = 0;

            //Commented By Hemant On 08 Apr 2021
            //WucMRCashChequeDetails1.CashLedgerID = 0;
        }
        string Branch_Name = UserManager.getUserParam().MainName;
        int Branch_Id = UserManager.getUserParam().MainId;
        Set_DebitTo_BranchID(Branch_Name.ToString(), Branch_Id.ToString()); 

        if (!IsPostBack)
        {

            //Added On 21 Aug 2018
            string Crypt = "", _GCNo = "";
            int _VehicleId, _GCId;

            Crypt = System.Web.HttpContext.Current.Request.QueryString["VehicleId"];
            _VehicleId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = System.Web.HttpContext.Current.Request.QueryString["GCId"];
            _GCId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = System.Web.HttpContext.Current.Request.QueryString["GCNo"];

            if (Crypt != null)
            {
                _GCNo = ClassLibraryMVP.Util.DecryptToString(Crypt);
            }

            if (_VehicleId > 0 && _GCId > 0)
            {
                WucVehicleSearch1.VehicleID = _VehicleId;

                GetDetails(this, e);

                Raj.EC.Common.SetValueToDDLSearch(_GCNo, Util.Int2String(_GCId), ddl_GCNo);
                hdn_GC_Id.Value = Util.Int2String(_GCId);
                hdn_GC_No_For_Print.Value = _GCNo;

                objDirectDeliveryPresenter.Get_GCDetails();

                objDirectDeliveryPresenter.Get_LHPO();

                hdn_LHPO_Id.Value = ddl_LHPO.SelectedValue;
                objDirectDeliveryPresenter.Get_LHPO_Details();
            }

            if (keyID <= 0)
            {
                Get_Next_DDC_Number();

                hdn_gc_caption.Value = CompanyManager.getCompanyParam().GcCaption;
                hdn_lhpo_caption.Value = CompanyManager.getCompanyParam().LHPOCaption;
            }
            else
            {
                wuc_DirectDeliveryDate.Enabled = false;
                ddl_GCNo.Enabled = false;
                WucVehicleSearch1.SetEnabled = false;
                ddl_LHPO.Enabled = false;
                WucDeliveryOtherDetails1.SetDeliveryAgainstCaption = "Against :";
                On_Delivery_Condition_Change();
            }
        }

        SetStandardCaption();       
    }
    public void On_Load()
    {
        
        String scripts;
        scripts = "<script type='text/javascript' language='javascript'>" +
                    "  HideReceivedByControl();" +
                    "  On_Delivery_Condition_Change(); " +
                    " On_Delivery_Article_Change(); " +
                    " On_chkIsFreightReceived(); " +
                    " Chk_CashLedger(); " +
                    " chkPaymentType(); " +                 
                    "</script>";

        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
        

        WucVehicleSearch1.SetAutoPostBack = true;
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(GetDetails);
        WucMRCashChequeDetails1.Scmcheque = SM_DirectDelivery; ;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.DirectDeliveryModel));

        ddl_GCNo.DataTextField = "GC_No_For_Print";
        ddl_GCNo.DataValueField = "GC_Id";

        string current_time = DateTime.Now.ToShortTimeString();

        if (CompanyManager.getCompanyParam().IsPartLoadingRequired == false)
        {
            disable_Textbox(txt_DeliveredArticles, txt_DeliveredArticlesWeight);
        }
        if (!IsPostBack)
        {
            wuc_ActualDeliveryTime.setFormat("24");
            wuc_ActualDeliveryTime.setTime(current_time);

            wuc_DirectDeliveryDate.SelectedDate = DateTime.Now;
            wuc_ActualDeliveryDate.SelectedDate = DateTime.Now;

            lbl_ExpectedDeliveryDateValue.Text = DateTime.Now.ToString("dd MMM yyyy");
        }    
        ddl_GCNo.OtherColumns = Util.Int2String(WucVehicleSearch1.VehicleID) + "," + wuc_DirectDeliveryDate.SelectedDate;

        if (ReceivedBy == 1)
        {  
            //lbl_DebitTo.Visible = false;
            //lbl_BillingBranch.Visible = false;

            //ddl_DebitTo.Visible = false;
            //ddl_BillingBranch.Visible = false;
        }
        else
        { 
        }   
    }

    private void disable_Textbox(TextBox txtbox1, TextBox txtbox2)
    {
        txtbox1.BackColor = Color.Transparent;
        txtbox1.BorderColor = Color.Transparent;
        txtbox1.ReadOnly = true;

        txtbox2.BackColor = Color.Transparent;
        txtbox2.BorderColor = Color.Transparent;
        txtbox2.ReadOnly = true;
    }

    public void GetDetails(object sender, EventArgs e)
    {
        ddl_GCNo.OtherColumns = Util.Int2String(WucVehicleSearch1.VehicleID) + "," + wuc_DirectDeliveryDate.SelectedDate;
        GetVehicleDetails(sender, e);
        //GetGC(sender, e);
        //GetGCDetails(sender, e);
    }
    public void GetGCDetails()
    {
        //if (ddl_LHPO.Items.Count > 0)
        //{
            objDirectDeliveryPresenter.Get_GCDetails();
        //}
    }
    public void GetVehicleDetails(object sender, EventArgs e)
    {
         Clear_Data();
        hdn_Vehicle_Id.Value = Util.Int2String(WucVehicleSearch1.VehicleID);

        //if (ddl_LHPO.Items.Count > 0)
        //{
            objDirectDeliveryPresenter.Get_VehicleDetails (sender, e);
        //}        
    }

    private void Get_Next_DDC_Number()
    {
        lbl_DDCNoValue.Text  = objComm.Get_Next_Number();
    }

    protected void wuc_DirectDeliveryDate_SelectionChanged(object sender, EventArgs e)
    {
        Clear_Data();
    
    }
    public void Clear_Data()
    {
        Booking_Branch = "";
        Delivery_Location = "";
        Payment_Type = "";
        Payment_Type_Id = 0;
        Total_GC_Amount = 0;
        Booking_Articles = 0;
        Booking_Articles_Weight = 0;
       //GC_Id = 0;
        SetGC("0", "0");
       // WucVehicleSearch1.VehicleID = 0;

        BookingDate = "";
        ddl_LHPO.Items.Clear();

        SetLHPO("0", "0");
        LHPO_Id = 0;
        LHPO_Date = "";
        LHPO_From = "";
        LHPO_To = "";

        Memo_Id = 0;
        Memo_No = "";
        Memo_From = "";
        Memo_To = "";
        Memo_Date = "";
        
        Loaded_Articles = 0;
        Loaded_Articles_Weight = 0;
        Delivered_Articles = 0;
        Delivered_Articles_Weight = 0;

        Damage_Leakage_Articles = 0;
        Damage_Leakage_Articles_Value = 0;
        Short_Articles = 0;

        ScheduledArivalDate = "";
        ScheduledArivalTime = "";
        Delivery_Taken_By = "";

        Is_PODReceived = false;
        IsFreightReceived = false;
        Remarks = "";
    }
    protected void ddl_LHPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdn_LHPO_Id.Value = ddl_LHPO.SelectedValue;
        objDirectDeliveryPresenter.Get_LHPO_Details ();
    }
    //protected void ddl_Delivery_Condintion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    On_Delivery_Condition_Change();
    //}

    public void On_Delivery_Condition_Change()
    {
        if (Util.String2Int(ddl_Delivery_Condintion.SelectedValue) != 1)
        {

            txt_DamageLeakageArticle.Enabled = true;
            txt_DamageLeakageValue.Enabled = true;
        }
        else
        {
            txt_DamageLeakageArticle.Text = "0";
            txt_DamageLeakageValue.Text = "0";

            txt_DamageLeakageArticle.Enabled = false;
            txt_DamageLeakageValue.Enabled = false;
        }
    }
    protected void ddl_GCNo_TxtChange(object sender, EventArgs e)
    {
        //Clear_Data();
        hdn_GC_Id.Value = ddl_GCNo.SelectedValue.Trim() == string.Empty ? "0" : ddl_GCNo.SelectedValue.Trim();

        if (ddl_GCNo.SelectedValue.Trim() == string.Empty || GC_Id == 0)
        {
            
            MRCashChequeDetailsView.CashAmount = 0;
            MRCashChequeDetailsView.ChequeAmount = 0;
            Total_GC_Amount = 0;
            
            
            MRCashChequeDetailsView.Session_ChequeDetailsGrid.Clear();
            MRCashChequeDetailsView.Bind_ChequeDetailsGrid = MRCashChequeDetailsView.Session_ChequeDetailsGrid;

            MRCashChequeDetailsView.Total_ChequeAmount = 0;
            Clear_Data();
        }
        else
        {
        }

        objDirectDeliveryPresenter.Get_GCDetails();
        objDirectDeliveryPresenter.Get_LHPO();

        hdn_LHPO_Id.Value = ddl_LHPO.SelectedValue;
        objDirectDeliveryPresenter.Get_LHPO_Details();


        if (Is_OctroiApplicable == true && Is_OctroiUpdated == false)
        {
            String popupScript = "<script language='javascript'>alert('Octroi is not Updated, Please Update Octroi.');</script>";
            ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

            //errorMessage = "Octroi is not Updated, Please Update Octroi.";
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        objDirectDeliveryPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objDirectDeliveryPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
