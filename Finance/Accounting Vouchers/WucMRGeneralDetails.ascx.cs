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

public partial class Finance_Accounting_Vouchers_WucMRGeneralDetails : System.Web.UI.UserControl,IMRGeneralDetailsView
{
    MRGeneralDetailsPresenter objMRGeneralDetailsPresenter;
    Common objCommon = new Common();
    public EventHandler OnGetDetails;
    public EventHandler OnMRDateChange;
    private int _Document_Allocation_ID = 0;
    private int _Start_No = 0;
    private int _End_No = 0;
    private int _Next_No = 0;
    private string _Padded_Next_No = "";

    #region Control Values

    public string MRNo 
    {
        get {return txt_MRNo.Text;}
        set { txt_MRNo.Text = value;}
    }

    public DateTime MRDate
    {
        get { return Wuc_MRDate.SelectedDate; }
        set { Wuc_MRDate.SelectedDate = value; }
    }
    
    public string GCNo 
    {
        get { return txt_GCNo.Text.Trim() == string.Empty ? "0" : txt_GCNo.Text.Trim(); }
        set { txt_GCNo.Text = value; }
    }
    private string MRNo_Label
    {
        set { lbl_MRNo.Text = value; }
    }

    public string MRDate_Label
    {
        set { lbl_MRDate.Text = value; }
    }
    public string BookingBranch 
    {
        get { return txt_BookingBranch.Text.Trim(); }
        set { txt_BookingBranch.Text = value; }
    }
    
    public string DeliveryBranch 
    {
        get { return txt_DeliveryBranch.Text.Trim(); }
        set { txt_DeliveryBranch.Text = value; }
    }
    
    public string Consignor 
    {
        get { return txt_Consignor.Text.Trim(); }
        set { txt_Consignor.Text = value; }
    }
    
    public string Consignee 
    {
        get { return txt_Consignee.Text.Trim(); }
        set { txt_Consignee.Text = value; }
    }
     
    public string BookingType 
    {
        get { return txt_BookingType.Text.Trim(); }
        set { txt_BookingType.Text = value; }
    }
    
    public string PaymentType 
    {
        get { return txt_PaymentType.Text.Trim(); }
        set { txt_PaymentType.Text = value; }
    }  
    public string ServiceTax 
    {
        get { return txt_ServiceTax.Text.Trim(); }
        set { txt_ServiceTax.Text = value; }
    }
    
    public string ServiceTaxBy 
    {
        get { return txt_serviceTaxBy.Text.Trim(); }
        set { txt_serviceTaxBy.Text = value; }
    }
        
    public string BookingDate 
    {
        get { return txt_BookingDate.Text.Trim(); }
        set { txt_BookingDate.Text = value; }
    }

    public int GC_ID 
    {
        get { return hdn_GC_ID.Value == "0" ? 0 : Util.String2Int(hdn_GC_ID.Value); }
        set { hdn_GC_ID.Value = value.ToString();}
    }   
   
    public string Start_End_No 
    {
        get {return lbl_Start_End_No.Text ;}
        set {lbl_Start_End_No.Text = value ;} 
    }

    public int MR_Type_ID 
    {
        get {return Util.String2Int(hdn_MR_Type_ID.Value) ;}
        set {hdn_MR_Type_ID.Value = value.ToString() ;}
    }
    
    public int Document_Allocation_ID
    {
        set { hdn_Document_Allocation_ID.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Document_Allocation_ID.Value); }
    }

    public int Start_No
    {
        set { hdn_Start_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Start_No.Value); }
    }

    public int End_No
    {
        set { hdn_End_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_End_No.Value); }
    }

    public int Next_No
    {
        set { hdn_Next_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Next_No.Value); }
    }

    public string Padded_Next_No
    {
        set { hdn_Padded_Next_No.Value = value; }
        get { return hdn_Padded_Next_No.Value; }
    }

    public string GCAmount
    {
        get { return txt_GCAmount.Text.Trim(); }
        set { txt_GCAmount.Text = value; }
    }
    public decimal GC_Total
    {
        get { return Util.String2Decimal(hdn_GC_Total.Value); }
        set { hdn_GC_Total.Value = value.ToString(); }
    }
    public decimal GC_SubTotal 
    {
        get {return Util.String2Decimal(hdn_GC_SubTotal.Value) ;}
        set { hdn_GC_SubTotal.Value = value.ToString();}
    }
    public decimal Total_Receivables
    {
        get{return Util.String2Decimal(hdn_Total_Receivables.Value); }
        set { hdn_Total_Receivables.Value = value.ToString(); }
    }

    public int GC_Booking_Type_ID
    {
        get { return Util.String2Int(hdn_Booking_Type_ID.Value); }
        set { hdn_Booking_Type_ID.Value = value.ToString(); }
    }

    public int GC_Payment_Type_ID
    {
        get { return Util.String2Int(hdn_Payment_Type_ID.Value); }
        set { hdn_Payment_Type_ID.Value = value.ToString(); }
    }

    public bool Is_MR_FirstTime 
    {
        get { return chk_Is_Mr_DlyFirstTime.Checked; }
        set { chk_Is_Mr_DlyFirstTime.Checked = value; }
    }
    public bool Is_CreditMemoOctroi_FirstTime
    {
        get { return chk_Is_CrMOctroi_FirstTime.Checked; }
        set { chk_Is_CrMOctroi_FirstTime.Checked = value; }
    }  
    public int Document_ID
    {
        get { return Util.String2Int(hdn_DocumentID.Value); }
        set { hdn_DocumentID.Value = value.ToString(); }
    }
       
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set{lbl_error.Text = value;}
    }

    public bool validateUI()
    {
        bool _isValid = true;     
        return _isValid;
    }
    //Start added on 23-12-13
    //public int RoundOff
    //{
    //    set
    //    {
    //        //lbl_RoundOff.Text = Util.Int2String(value);
    //        //hdn_RoundOff.Value = Util.Int2String(value);
    //    }
    //    //get { return hdn_RoundOff.Value == string.Empty ? 0 : Util.String2Int(hdn_RoundOff.Value); }
    //}
    //End added on 23-12-13
    private void On_PageLoad()
    {
        if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
        {
            txt_GCNo.MaxLength = 15;
        }
        else
        {
            txt_GCNo.MaxLength = Util.String2Int(objCommon.Get_Values_Where("EC_Master_Company_GC_Parameter", "GC_No_Length", "", "GC_No_Length", false).Tables[0].Rows[0]["GC_No_Length"].ToString());
            txt_GCNo.Attributes.Add("onkeypress", "return Only_Integers(" + txt_GCNo.ClientID + ",event)");
        }

        if(keyID <= 0 && CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel")
        {
            txt_MRNo.MaxLength = txt_GCNo.MaxLength;
        }
        else
        {
            txt_MRNo.ReadOnly = true;
        }

        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        txt_GCNo.Focus();

        if (Document_ID == 8)
        {
            //MRNo_Label = "Credit Memo No. :";
            //MRDate_Label = "Credit Memo Date :";

            MRNo_Label = "Memo No. :";
            MRDate_Label = "Memo Date :";
        }
        else
        {
            MRNo_Label = "MR No. :";
            MRDate_Label = "MR Date :";
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Document_ID = Util.String2Int(StateManager.GetState<string>("QueryString").ToString());

         
            Wuc_MRDate.AutoPostBackOnSelectionChanged = false;
         

        if (!IsPostBack)
        {
            On_PageLoad();

            if (keyID <= 0)
            {
                Get_MR_No();
            }
        }

        if (keyID > 0)
        {
            btn_GetDetails.Enabled = false;
            txt_GCNo.Enabled = false; 
            lbl_Start_End_No.Visible = false;
        }
        objMRGeneralDetailsPresenter = new MRGeneralDetailsPresenter(this, IsPostBack);
        SetStandardCaption();
    }

    private void SetStandardCaption()
    {
        lbl_GCNo.Text = CompanyManager.getCompanyParam().GcCaption + " No";
        lbl_GCAmount.Text = CompanyManager.getCompanyParam().GcCaption + " Sub Total";
    }

    protected void btn_GetDetails_Click(object sender, EventArgs e)
    {
        objMRGeneralDetailsPresenter.Get_GC_Details();                      
        if (OnGetDetails != null)
        {
            OnGetDetails(sender, e);
        }
    }

    public bool validateGeneralDetails(Label lbl_Errors)
    {
       
        if (GCNo == string.Empty)
        {
            lbl_Errors.Text = "Please Enter "+ CompanyManager.getCompanyParam().GcCaption +" No.";
            txt_GCNo.Focus();
            return false;
        }
        else if (keyID <= 0 && (Util.String2Int(MRNo) < Start_No || Util.String2Int(MRNo) > End_No))
        {
            lbl_Errors.Text = "MR No. Should be Between " + Start_No + " and " + End_No;
            return false;
        }
        else
        {
            return true;
        }    
    }

    public void Get_MR_No()
    {
        objCommon.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, Document_ID);

        Document_Allocation_ID = _Document_Allocation_ID;
        Start_No = _Start_No;
        End_No = _End_No;
        Next_No = _Next_No;

        if (_Next_No <= 0)
        {
            Common.DisplayErrors(1013);
        }

        _Padded_Next_No = _Next_No.ToString("0000000");
        Padded_Next_No = _Padded_Next_No;
        MRNo = Padded_Next_No;

        Start_End_No = "(" + Start_No + " - " + End_No + ")";
    }

    protected void MrDateChange(object sender, EventArgs e)
    {
        if (OnMRDateChange != null)
        {
            OnMRDateChange(sender, e);
        }  
    }
}
