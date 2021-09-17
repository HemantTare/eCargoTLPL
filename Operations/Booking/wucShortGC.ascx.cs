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
using System.Text;
using System.Text.RegularExpressions;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.Security;

public partial class Operations_Booking_wucShortGC : System.Web.UI.UserControl, IShortGCView
{
    public ShortGCPresenter objShortGCPresenter;
    bool Is_Valid = false;

    public Int32 lastVarValue = 0;

    public String Contractual_Charges_FilterExpression = "";
    PageControls pc = new PageControls();
    public Common CommonObj = new Common();

    DataSet ds = new DataSet();

    public DataSet ds_ContractDetails;
    DataTable objdata_Invoice;

    DataRow dr;

    DropDownList ddl_Commodity, ddl_Item, ddl_Packing_Type;
    TextBox txt_Articles,txt_Weight,txt_Width,txt_Length,txt_Height,txt_Commodity_SrNo,txt_Remark;
    TextBox txt_InvoiceNo, txt_Chalan_No, txt_InvoiceAmount, txt_BE_BLNo;
    Boolean Allow_To_Save,Is_Allow_To_ReBook,Is_Allow_To_Rectify,Is_Duplicate_GC_No;
    String _flag;

    #region IView Members

    public string errorMessage
    {
        set 
        { 
            lbl_Errors.Text = value;
            lbl_Errors1.Text = value;  
        }
    }

    public string MultipleCommodityGridErrorMessage
    {
        set
        {
            lbl_MultipleCommodityGridErrors.Text = value;
            lbl_MultipleCommodityGridErrorsNandwana.Text = value;
        }
    }

    public string Flag
    {
        get { return _flag; }
    }

    public string InvoiceGridErrorMessage
    {
        set{lbl_InvoiceGridErrors.Text = value;}
    }

    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    private int _meniItemID
    {
        get { return Common.GetMenuItemId(); }
    }

    public int DocumentId
    {
        get
        {
            return Util.String2Int(StateManager.GetState<string>("QueryString"));
            // Document_Id = 1 for IBA GC
            // Document_Id = 2 for Normal GC
        }
    } 

    #endregion

    #region InitInterface

    public DateTime Previous_Document_Date
    {
        set{ hdn_PreviousDocumentDate.Value = value.ToString("dd MMM yyyy"); }
        get
        {
            if (Is_ReBooked == true && ReBook_GC_Id > 0)
            {
                //hdn_PreviousDocumentDate.Value = gc
            }
            else
            {
                hdn_PreviousDocumentDate.Value = BookingDate.ToString("dd MMM yyyy");
            }

            return Convert.ToDateTime(hdn_PreviousDocumentDate.Value.Trim());
        }
    }

    public DateTime ExpectedDeliveryDate
    {
        set{lbl_ExpectedDeliveryDateVaule.Text = value.ToString("dd MMM yyyy");}
        get {return Convert.ToDateTime(lbl_ExpectedDeliveryDateVaule.Text.Trim()); }
    }

    public DateTime BookingDate
    {
        set
        {
            wuc_BookingDate.SelectedDate = value;
            hdn_BookingDate.Value = wuc_BookingDate.SelectedDate.ToString();
        }
        get{return wuc_BookingDate.SelectedDate;}
    }

    public DateTime ArrivedDate
    {
        set{wuc_ArrivedDate.SelectedDate = value;}
        get{return wuc_ArrivedDate.SelectedDate;}
    }
    
    public DateTime ApplicationStartDate
    {
        set
        {
            wuc_ApplicationStartDate.SelectedDate = value;
            hdn_ApplicationStartDate.Value = wuc_ApplicationStartDate.SelectedDate.ToString();
        }
        get{return wuc_ApplicationStartDate.SelectedDate;}
    }

    public String BookingTime
    {
        set{wuc_BookingTime.setTime(value); }
        get {return wuc_BookingTime.getTime();}
    }

    public DateTime ChequeDate
    {
        set
        {
            wuc_ChequeDate.SelectedDate = value;
            hdn_ChequeDate.Value = wuc_ChequeDate.SelectedDate.ToString();
        }
        get{return wuc_ChequeDate.SelectedDate;}
    }

    public DateTime PolicyExpDate
    {
        set
        {
            wuc_PolicyExpDate.SelectedDate = value;
            hdn_PolicyExpDate.Value = wuc_PolicyExpDate.SelectedDate.ToString();
        }
        get{return wuc_PolicyExpDate.SelectedDate;}
    }

    public DateTime  Session_PolicyExpDate
    {
        get { return StateManager.GetState<DateTime>("PolicyExpDate"); }
        set { StateManager.SaveState("PolicyExpDate", value); }
    }

    public String DeliveryBranchName
    {
        set
        {
            lbl_DeliveryBranchName.Text = value;
            hdn_DeliveryBaranchName.Value = value;
            Session_DeliveryBranchName = value;
        }
        get{return lbl_DeliveryBranchName.Text.Trim();}
    }

    public String GC_No
    {
        set
        {
            if (_meniItemID == 200 && Is_Opening_GC == true && ClientCode.ToLower()=="nandwana")
            {
                char[] _Separator ={ '-' };
                string[] _IdArray = new string[2];

                _IdArray = value.Split(_Separator);

                GC_No_For_Print = _IdArray[0];
                txt_GC_No_For_Print.Text = _IdArray[1].ToString();
                hdn_GC_No_For_Print.Value = _IdArray[1].ToString();
            }
            else
            {
                GC_No_For_Print = value;
                txt_GC_No_For_Print.Text = value;
                hdn_GC_No_For_Print.Value = value;
            }
        }
        get { return txt_GC_No_For_Print.Text.Trim();}
    }

    public String GC_No_For_Print
    {
        set 
        {
            if (_meniItemID == 200 && Is_Opening_GC == true && ClientCode.ToLower() == "nandwana")
            {
                ddl_GC_No.SelectedValue = value;
            }
        }
        get 
        {
            if (_meniItemID == 200 && Is_Opening_GC == true && ClientCode.ToLower() == "nandwana")
            {
                return ddl_GC_No.SelectedValue.Trim() + "-" + GC_No.Trim();
            }
            else
            {
                return GC_No.Trim();
            }
        }
    }

    public String PrivateMark
    {
        set{}
        get{return txt_GC_No_For_Print.Text.Trim(); }
    }
    public String ConsignorCountryName
    {
        set{hdn_ConsignorCountryName.Value = value; }
        get{ return hdn_ConsignorCountryName.Value.Trim(); }
    }

    public String ConsignorStateName
    {
        set{hdn_ConsignorStateName.Value = value;}
        get{return hdn_ConsignorStateName.Value.Trim();}
    }

    public String ConsigneeCountryName
    {
        set{hdn_ConsigneeCountryName.Value = value; }
        get{ return hdn_ConsigneeCountryName.Value.Trim(); }
    }

    public String ConsigneeStateName
    {
        set{hdn_ConsigneeStateName.Value = value; }
        get{return hdn_ConsigneeStateName.Value.Trim();}
    }

    public String EncreptedConsignorId
    {
        set{hdn_EncreptedConsignorId.Value = value; }
        get{ return hdn_EncreptedConsignorId.Value; }
    }

    public String Encrepted_Rectification_GC_Id
    {
        set{hdn_Encrepted_Rectification_GC_Id.Value = value; }
        get{return hdn_Encrepted_Rectification_GC_Id.Value; }
    }    

    public String ConsignorName
    {
        set{ddl_Consignor.SelectedText = value; }
        get {return ddl_Consignor.SelectedText; }
    }
    
    public String ConsignorDetails
    {
        set {lbl_ConsignorDetailsValue.Text = value;}
        get { return lbl_ConsignorDetailsValue.Text.Trim();}
    } 

    public String ConsignorAddressLine1
    {
        set{txt_ConsignorAddressLine1.Text = value; }
        get {return txt_ConsignorAddressLine1.Text.Trim();}
    }

    public String ConsignorAddressLine2
    {
        set{txt_ConsignorAddressLine2.Text = value; }
        get {return txt_ConsignorAddressLine2.Text.Trim();}
    }

    public String ConsignorCity
    {
        set{txt_ConsignorCity.Text = value; }
        get{return txt_ConsignorCity.Text.Trim(); }
    }

    public String ConsignorPinCode
    {
        set {txt_ConsignorPinCode.Text = value;}
        get { return txt_ConsignorPinCode.Text.Trim(); }
    }

    public String ConsignorTelNo
    {
        set{txt_ConsignorTelNo.Text = value;}
        get { return txt_ConsignorTelNo.Text.Trim();}
    }

    public String ConsignorMobileNo
    {
        set{txt_ConsignorMobileNo.Text = value;}
        get{return txt_ConsignorMobileNo.Text.Trim();}
    }

    public String ConsignorEmail
    {
        set{txt_ConsignorEmail.Text = value;}
        get{ return txt_ConsignorEmail.Text.Trim();}
    }

    public String ConsignorCSTNo
    {
        set{txt_ConsignorCSTNo.Text = value; }
        get{return txt_ConsignorCSTNo.Text.Trim(); }
    }
    public String EncreptedConsigneeId
    {
        set{hdn_EncreptedConsigneeId.Value = value;}
        get{return hdn_EncreptedConsigneeId.Value;}
    }
    public String ConsigneeName
    {
        set{ddl_Consignee.SelectedText = value;}
        get{return ddl_Consignee.SelectedText;}
    }
    public String ConsigneeDetails
    {
        set
        {lbl_ConsigneeDetailsValue.Text = value; }
        get{return lbl_ConsigneeDetailsValue.Text.Trim();}
    }
    public String ConsigneeAddressLine1
    {
        set{txt_ConsigneeAddressLine1.Text = value;}
        get{return txt_ConsigneeAddressLine1.Text.Trim();}
    }
    public String ConsigneeAddressLine2
    {
        set{txt_ConsigneeAddressLine2.Text = value;}
        get{return txt_ConsigneeAddressLine2.Text.Trim();}
    }
    public String ConsigneeDDAddressLine1
    {
        set
        {
            txt_ConsigneeDDAddressLine1.Text = value;
            Session_ConsigneeAddressLine1 = value;
        }
        get{return Session_ConsigneeAddressLine1.Trim(); }
    }
    public String ConsigneeDDAddressLine2
    {
        set
        {
            txt_ConsigneeDDAddressLine2.Text = value;
            Session_ConsigneeAddressLine2 = value;
        }
        get{return Session_ConsigneeAddressLine2.Trim(); }
    }
    public String ConsigneeCity
    {
        set{txt_ConsigneeCity.Text = value;}
        get{return txt_ConsigneeCity.Text.Trim();}
    }
    public String ConsigneePinCode
    {
        set{txt_ConsigneePinCode.Text = value; }
        get{return txt_ConsigneePinCode.Text.Trim(); }
    }
    public String ConsigneeTelNo
    {
        set{txt_ConsigneeTelNo.Text = value;}
        get{return txt_ConsigneeTelNo.Text.Trim();}
    }
    public String ConsigneeMobileNo
    {
        set{txt_ConsigneeMobileNo.Text = value;}
        get{return txt_ConsigneeMobileNo.Text.Trim();}
    }
    public String ConsigneeEmail
    {
        set{txt_ConsigneeEmail.Text = value;}
        get{return txt_ConsigneeEmail.Text.Trim();}
    }
    public String ConsigneeCSTNo
    {
        set{txt_ConsigneeCSTNo.Text = value;}
        get{return txt_ConsigneeCSTNo.Text.Trim(); }
    }
    public Boolean Is_Attached
    {
        set{hdn_IsAttached.Value = "0";}
        get{return false;}
    }
    public Boolean Is_Insured
    {
        set{chk_IsInsured.Checked = Convert.ToBoolean(value);}
        get{return Convert.ToBoolean(chk_IsInsured.Checked); }
    }    
    public Boolean Is_ReBooked
    {
        set{}
        get{return false;}
    }
    public Boolean Is_MultipleBilling
    {
        set{}
        get{return false;}
    }
    public Boolean Is_POD
    {
        set{chk_PodRequire.Checked = Convert.ToBoolean(value); }
        get{return Convert.ToBoolean(chk_PodRequire.Checked);}
    }
    public Boolean Is_SignedByConsignor
    {
        set{}
        get{return false;}
    }
    public int GC_Id
    {
        set{hdn_GCId.Value = Util.Int2String(value);}
        get{ return ValueOfHdn_Int(hdn_GCId);}
    }
    public int Rectification_GC_Id
    {
        set{hdn_Rectification_GC_Id.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_Rectification_GC_Id); }
    }
    public int Previous_Article_ID
    {
        set{hdn_PreviousArticleID.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_PreviousArticleID); }
    }
    public int Previous_Status_ID
    {
        set{hdn_PreviousStatusID.Value = Util.Int2String(value);}
        get{ return ValueOfHdn_Int(hdn_PreviousStatusID); }
    }
    public int Previous_Document_ID
    {
        set {hdn_PreviousDocumentID.Value = Util.Int2String(value); }
        get {return ValueOfHdn_Int(hdn_PreviousDocumentID);}
    }
    public int Is_RegularConsignor
    {
        set{hdn_IsRegularConsignor.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_IsRegularConsignor); }
    }
    public int Is_RegularConsignee
    {
        set{hdn_IsRegularConsignee.Value = Util.Int2String(value);}
        get{ return ValueOfHdn_Int(hdn_IsRegularConsignee);}
    }
    public Boolean Is_DACC
    {
        set{}
        get{ return false;}
    }
    public Boolean Is_ODA
    {
        set{hdn_IsODA.Value = Convert.ToString(value); }
        get{return Convert.ToBoolean(hdn_IsODA.Value); }
    }
    public Boolean Is_POD_Checked
    {
        set{hdn_Is_POD_Checked.Value = Convert.ToString(value);}
        get{ return Convert.ToBoolean(hdn_Is_POD_Checked.Value);}
    }
    public Boolean Is_POD_Disabled
    {
        set{hdn_Is_POD_Disabled.Value = Convert.ToString(value);}
        get{return Convert.ToBoolean(hdn_Is_POD_Disabled.Value);}
    }
    public Boolean Is_Opening_GC
    {
        set{hdn_Is_Opening_GC.Value = Convert.ToString(value);}
        get{return Util.String2Bool(hdn_Is_Opening_GC.Value);}
    }
    public Boolean Is_GCNumberEditable
    {
        set{hdn_Is_GCNumberEditable.Value = Convert.ToString(value);}
        get{return Convert.ToBoolean(hdn_Is_GCNumberEditable.Value);}
    }
    public Boolean Is_Invoice_Amount_Required
    {
        set{hdn_Is_Invoice_Amount_Required.Value = Convert.ToString(value); }
        get{return Convert.ToBoolean(hdn_Is_Invoice_Amount_Required.Value); }
    }        
    public Boolean Is_FOV_Calculated_As_Per_Standard
    {
        set{hdn_Is_FOV_Calculated_As_Per_Standard.Value = Convert.ToString(value);}
        get {return Convert.ToBoolean(hdn_Is_FOV_Calculated_As_Per_Standard.Value);}
    }
    public Boolean Is_Auto_Booking_MR_For_Paid_Booking
    {
        set{hdn_Is_Auto_Booking_MR_For_Paid_Booking.Value = Convert.ToString(value);}
        get{return Convert.ToBoolean(hdn_Is_Auto_Booking_MR_For_Paid_Booking.Value); }
    }
    public Boolean Is_ToPay_Charge_Require
    {
        set{hdn_Is_ToPay_Charge_Require.Value = Convert.ToString(value);}
        get{return Convert.ToBoolean(hdn_Is_ToPay_Charge_Require.Value);}
    }
    public Boolean Is_Consignor_Consignee_Details_Shown
    {
        set{hdn_Is_Consignor_Consignee_Details_Shown.Value = Convert.ToString(value);}
        get{ return Convert.ToBoolean(hdn_Is_Consignor_Consignee_Details_Shown.Value); }
    }
    public Boolean Is_Validate_Freight_On_Article
    {
        set{hdn_Is_Validate_Freight_On_Article.Value = Convert.ToString(value);}
        get{return Convert.ToBoolean(hdn_Is_Validate_Freight_On_Article.Value);}
    }    
    public Boolean Is_Item_Required
    {
        set{hdn_Is_Item_Required.Value = Convert.ToString(value); }
        get{ return Convert.ToBoolean(hdn_Is_Item_Required.Value);}
    }
    public Boolean Is_Validate_Credit_Limit
    {
        set{hdn_Is_Validate_Credit_Limit.Value = Convert.ToString(value); }
        get{return Convert.ToBoolean(hdn_Is_Validate_Credit_Limit.Value); }
    } 
    public Boolean Is_Contract_Required_For_TBB_GC
    {
        set{hdn_Is_Contract_Required_For_TBB_GC.Value = Convert.ToString(value);}
        get{return Convert.ToBoolean(hdn_Is_Contract_Required_For_TBB_GC.Value);}
    }     
    public Boolean Is_ToPayBookingApplicable
    {
        set{hdn_IsToPayBookingApplicable.Value = Convert.ToString(value); }
        get{return Convert.ToBoolean(hdn_IsToPayBookingApplicable.Value);}
    }
    public Boolean Is_Service_Tax_Applicable_For_Commodity
    {
        set{hdn_Is_Service_Tax_Applicable_For_Commodity.Value = Convert.ToString(value); }
        get {return Convert.ToBoolean(hdn_Is_Service_Tax_Applicable_For_Commodity.Value.Trim()); }
    }
    public Boolean Is_ReBookGC_ToPay
    {
        set{hdn_IsReBookGC_ToPay.Value = Convert.ToString(value);}
        get {return Convert.ToBoolean(hdn_IsReBookGC_ToPay.Value.Trim());}
    }
    public Boolean Is_ReBookGC_Octroi_Updated
    {
        set {}
        get{return false;}
    }
    public Boolean Is_ReBookGC_Octroi_Applicable
    {
        set { }
        get{return false;}
    }

    public Boolean Is_OctroiApplicable
    {
        set{chk_IsOctroiApplicable.Checked = Convert.ToBoolean(value);}
        get{return Convert.ToBoolean(chk_IsOctroiApplicable.Checked); }
    }
    public Boolean Is_Cheque
    {       
        get{ return ChequeAmount > 0 ? true : false; }
    }

    public int Next_No
    {
        set
        {
            hdn_Next_No.Value = Convert.ToInt32(value).ToString(No_For_Padd);

            txt_GC_No_For_Print.Text = Convert.ToInt32(value).ToString(No_For_Padd);
            hdn_GC_No_For_Print.Value = Convert.ToInt32(value).ToString(No_For_Padd);           
        }
        get{ return Util.String2Int(hdn_Next_No.Value.Trim()); }
    }

    public int End_No
    {
        set {hdn_End_No.Value = Convert.ToInt32(value).ToString(No_For_Padd);}
        get{return Util.String2Int(hdn_End_No.Value.Trim());}
    }

    public int Start_No
    {
        set{hdn_Start_No.Value = Convert.ToInt32(value).ToString(No_For_Padd);}
        get{return Util.String2Int(hdn_Start_No.Value.Trim());}
    }

    public int VAId
    {
        set{hdn_VAId.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_VAId);}
    }

    public int Attached_GC_Id
    {
        set{hdn_AttachedGCId.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_AttachedGCId);}
    }
    public int ReBook_GC_Id
    {
        set{hdn_ReBookGCId.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_ReBookGCId); }
    }
    public int GC_Status_Id_At_Current_Branch
    {
        set {hdn_GC_Status_Id_At_Current_Branch.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_GC_Status_Id_At_Current_Branch);}
    }
    public int GC_Articles_At_Current_Branch
    {
        set{ hdn_GC_Articles_At_Current_Branch.Value = Util.Int2String(value);}
        get{ return ValueOfHdn_Int(hdn_GC_Articles_At_Current_Branch);}
    }
    public int Actual_GC_Articles
    {
        set{hdn_Actual_GC_Articles.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_Actual_GC_Articles); }
    }    
    public String Attached_GC_No_For_Print
    {
        set{}
        get{return "0";}
    }
    public int Default_Cash_Ledger_Id
    {
        set{ hdn_Default_Cash_Ledger_Id.Value = Util.Int2String(value); }
        get{ return ValueOfHdn_Int(hdn_Default_Cash_Ledger_Id); }
    }

    public int Valid_Cheque_Start_Days
    {
        set{hdn_Valid_Cheque_Start_Days.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_Valid_Cheque_Start_Days);}
    }
    
    public int Valid_Cheque_End_Days
    {
        set{ hdn_Valid_Cheque_End_Days.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_Valid_Cheque_End_Days); }
    }
    public int Remark_Max_Length
    {
        set{hdn_Remark_Max_Length.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_Remark_Max_Length); }
    }     
    public int Default_Bank_Ledger_Id
    {
        set{hdn_Default_Bank_Ledger_Id.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_Default_Bank_Ledger_Id); }
    }
    public int GC_No_Length
    {
        set{ hdn_GC_No_Length.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_GC_No_Length);  }
    }    
    public int FirstCommodityId
    {
        set{hdn_FirstCommodityId.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_FirstCommodityId);  }
    }

    public int FirstPackingTypeId
    {
        set{hdn_FirstPackingTypeId.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_FirstPackingTypeId);}
    }
    public int FirstItemId
    {
        set{hdn_FirstItemId.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_FirstItemId); }
    }
    public int Old_FirstCommodityId
    {
        set{hdn_OldFirstCommodityId.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_OldFirstCommodityId); }
    }
    public int Old_FirstPackingTypeId
    {
        set{hdn_OldFirstPackingTypeId.Value = Util.Int2String(value);}
        get{return ValueOfHdn_Int(hdn_OldFirstPackingTypeId);}
    }
    public int Old_FirstItemId
    {
        set
        {
            hdn_OldFirstItemId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_OldFirstItemId);
        }
    }

    public int TotalKiloMeter
    {
        set
        {
            hdn_TotalKiloMeter.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_TotalKiloMeter);
        }
    }

    public int TotalTransitDays
    {
        set
        {
            hdn_TotalTransitDays.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_TotalTransitDays);
        }
    }

    public int Is_ContractApplied
    {
        set
        {
            hdn_IsContractApplied.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_IsContractApplied);
        }
    }

    public int Contract_UnitOfFreightId
    {
        set
        {
            hdn_Contract_UnitOfFreightId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Contract_UnitOfFreightId);
        }
    }

    public int Contract_FreightSubUnitId
    {
        set
        {
            hdn_Contract_FreightSubUnitId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Contract_FreightSubUnitId);
        }
    }

    public int Contract_FreightBasisId
    {
        set
        {
            hdn_Contract_FreightBasisId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Contract_FreightBasisId);
        }
    }

    public int Contract_FreightUnitItemId
    {
        set
        {
            hdn_Contract_FreightUnitItemId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Contract_FreightSubUnitItemId);
        }
    }

    public int Contract_CFTFactor
    {
        set
        {
            hdn_Contract_CFTFactor.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Contract_CFTFactor);
        }
    }   
    public int Contract_FreightSubUnitItemId
    {
        set
        {
            hdn_Contract_FreightSubUnitItemId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Contract_FreightSubUnitItemId);
        }
    }

    public int DocumentSeriesAllocationId
    {
        set
        {
            hdn_DocumentSeriesAllocationID.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_DocumentSeriesAllocationID);
        }
    }

    public int DocumentNextCounterNo
    {
        set
        {
            hdn_DocumentNextCounterNo.Value = Util.Int2String(value);
            lbl_DocumentNextCounterNo.Text = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_DocumentNextCounterNo);
        }
    }

    public int CompanyParameter_Standard_BasicFreightUnitId
    {
        set
        {
            hdn_CompanyParameter_Standard_BasicFreightUnitId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_CompanyParameter_Standard_BasicFreightUnitId);
        }
    }

    public int BookingBranchId
    {
        set
        {
            hdn_BookingBranchId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_BookingBranchId);
        }
    }

    public int ToLocationId
    {
        set
        {
            hdn_ToLocationId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ToLocationId);
        }
    }

    public int FromLocationId
    {
        set
        {
            hdn_FromLocationId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_FromLocationId);
        }
    }

    public int Default_Booking_Type
    {
        set
        {
            hdn_Default_Booking_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Booking_Type);
        }
    }

    public int Default_Payment_Type
    {
        set
        {
            hdn_Default_Payment_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Payment_Type);
        }
    }

    public int Default_Delivery_Type
    {
        set
        {
            hdn_Default_Delivery_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Delivery_Type);
        }
    }

    public int Default_Road_Permit_Type
    {
        set
        {
            hdn_Default_Road_Permit_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Road_Permit_Type);
        }
    }

    public int Default_Consignment_Type
    {
        set
        {
            hdn_Default_Consignment_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Consignment_Type);
        }
    }

    public int Default_Pickup_Type
    {
        set
        {
            hdn_Default_Pickup_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Pickup_Type);
        }
    }
    
    public int Default_Commodity_Weight
    {
        set
        {
            hdn_Default_Commodity_Weight.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Commodity_Weight);
        }
    }

    public int Default_Measurment_Unit
    {
        set
        {
            hdn_Default_Measurment_Unit.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Measurment_Unit);
        }
    }

    public int Default_Freight_Basis
    {
        set
        {
            hdn_Default_Freight_Basis.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Freight_Basis);
        }
    }

    public int Default_Risk_Type
    {
        set
        {
            hdn_Default_Risk_Type.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Default_Risk_Type);
        }
    }  
    public int ArrivedFromBranchId
    {
        set
        {
            hdn_ArrivedFromBranchId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ArrivedFromBranchId);
        }
    }

    public int Contractual_ClientId
    {
        set
        {
            hdn_ContractualClientId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ContractualClientId);
        }
    }

    public int BillingPartyId
    {
        set
        {
            hdn_BillingPartyId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_BillingPartyId);
        }
    }

    public int BillingBranchId
    {
        set
        {
            hdn_BillingBranchId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_BillingBranchId);
        }
    }

    public int Contract_BranchId
    {
        set
        {
            ddl_ContractBranch.SelectedValue = Util.Int2String(value);
            hdn_ContractBranchId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ContractBranchId);
        }
    }

    public void SetFromLocation(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_FromLocation);
    }

    public void SetToLocation(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_ToLocation);
    }

    public void SetBookingBranch(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_BookingBranch  );
    }

    public void SetArrivedFromBranch(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_ArrivedFromBranch );
    }

    public void SetBillingBranch(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_BillingBranch);
    }  
    public void SetBillingParty(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_BillingParty);
    }

    public void SetContractualClient(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_ContractualClient);
    }

    public void SetConsingor(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_Consignor);
    }

    public void SetConsingee(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_Consignee);
    }

    public void SetMarketingExecutive(string text, string value)
    {
       Common.SetValueToDDLSearch(text, value, ddl_MarketingExecutive);
    }

    public void SetLoadingSuperVisor(string text, string value)
    {
       Common.SetValueToDDLSearch(text, value, ddl_LoadingSuperVisor);
    }

    public int TotalArticles
    {
        set
        {
            lbl_TotalArticles.Text = Util.Int2String(value);
            hdn_TotalArticles.Value = Util.Int2String(value);

            lbl_TotalArticlesNandwana.Text = Util.Int2String(value);
            hdn_TotalArticlesNandwana.Value = Util.Int2String(value);
        }
        get{return ValueOfLable_Int(lbl_TotalArticles);}
    }

    public Decimal CompanyParameter_Standard_FreightRatePer
    {
        set{hdn_CompanyParameter_Standard_FreightRatePer.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get{return ValueOfHdn_Decimal(hdn_CompanyParameter_Standard_FreightRatePer);}
    }

    public Decimal TotalWidth
    {
        set
        {
            lbl_TotalWidth.Text = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
            hdn_TotalWidth.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
            lbl_TotalWidthNandwana.Text = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
            hdn_TotalWidthNandwana.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get{return ValueOfHdn_Decimal(hdn_TotalWidth);}
    }

    public Decimal TotalWeight
    {
        set
        {
            lbl_TotalWeight.Text = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
            hdn_TotalWeight.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
            lbl_TotalWeightNandwana.Text = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
            hdn_TotalWeightNandwana.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalWeight);
        }
    }
    // ------------------------ Discounted Percent ------------------------

    public Decimal Additional_Freight
    {
        set
        {
            hdn_Additional_Freight.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Additional_Freight);
        }
    }

    public Decimal Freight_Charge_Discount_Percent
    {
        set
        {
            hdn_Freight_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Freight_Charge_Discount_Percent);
        }
    }

    public Decimal Hamali_Charge_Discount_Percent
    {
        set
        {
            hdn_Hamali_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Hamali_Charge_Discount_Percent);
        }
    }

    public Decimal Fov_Charge_Discount_Percent
    {
        set
        {
            hdn_Fov_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Fov_Charge_Discount_Percent);
        }
    }

    public Decimal ToPay_Charge_Discount_Percent
    {
        set
        {
            hdn_ToPay_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ToPay_Charge_Discount_Percent);
        }
    }

    public Decimal DD_Charge_Discount_Percent
    {
        set
        {
            hdn_DD_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_DD_Charge_Discount_Percent);
        }
    }

    // ------------------------ Contractual Charges ------------------------

    public Decimal Standard_ServiceTaxPercent
    {
        set
        {
            hdn_Standard_ServiceTaxPercent.Value = Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_ServiceTaxPercent);
        }
    }

    public Decimal Standard_BiltiCharges
    {
        set
        {
            hdn_Standard_BiltiCharges.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_BiltiCharges);
        }
    }

    public Decimal Standard_DDCharge_Rate
    {
        set
        {
            hdn_Standard_DDCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_DDCharge_Rate);
        }
    }

    public Decimal Standard_DDCharge
    {
        set
        {
            hdn_Standard_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_DDCharge);
        }
    }

    public Decimal Standard_DACCCharges
    {
        set
        {
            hdn_Standard_DACCCharges.Value = Util.Decimal2String(Math.Round(value, 2)); //Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_DACCCharges);
        }
    }

    public Decimal Standard_FOV
    {
        set
        {
            hdn_Standard_FOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_FOV);
        }
    }

    public Decimal Standard_FOVPercentage
    {
        set
        {
            hdn_Standard_FOVPercentage.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_FOVPercentage);
        }
    }
    
    public Decimal Standard_FOVRate
    {
        set
        {
            hdn_Standard_FOVRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_FOVRate);
        }
    }

    public Decimal Standard_Invoice_Rate
    {
        set
        {
            hdn_Standard_Invoice_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_Invoice_Rate);
        }
    }
    
    public Decimal Standard_Invoice_Per_How_Many_Rs
    {
        set
        {
            hdn_Standard_Invoice_Per_How_Many_Rs.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_Invoice_Per_How_Many_Rs);
        }
    }
 
    public Decimal Standard_LengthCharge
    {
        set
        {
            hdn_Standard_LengthCharge.Value  = Util.Decimal2String(Math.Round(value, 2));            
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_LengthCharge);
        }
    }

    public Decimal Standard_FreightAmount
    {
        set
        {
            hdn_Standard_FreightAmount.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_FreightAmount);
        }
    }

    public Decimal Standard_CFTFactor
    {
        set
        {
            hdn_Standard_CFTFactor.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_CFTFactor);
        }
    }

    public Decimal Standard_FreightRate
    {
        set
        {
            hdn_Standard_FreightRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            if (ContractId <= 0)
            {
                hdn_Applicable_Standard_FreightRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            }
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_FreightRate);
        }
    }

    public Decimal Special_FreightRate
    {
        set
        {
            hdn_Special_FreightRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Special_FreightRate);
        }
    }

    public Decimal Standard_HamaliCharge
    {
        set
        {
            hdn_Standard_HamaliCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_HamaliCharge);
        }
    }

    public Decimal Standard_LocalCharge
    {
        set
        {
            hdn_Standard_LocalCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_LocalCharge);
        }
    }

    public Decimal Standard_LocalCharge_Rate
    {
        set
        {
            hdn_Standard_LocalCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_LocalCharge_Rate);
        }
    }

    public Decimal Standard_ServiceTaxAmount
    {
        set
        {
            hdn_Standard_ServiceTaxAmount.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_ServiceTaxAmount);
        }
    }

    public Decimal Standard_MinimumFOV
    {
        set
        {
            hdn_Standard_MinimumFOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_MinimumFOV);
        }
    }

    public Decimal Standard_MinimumChargeWeight
    {
        set
        {
            hdn_Standard_MinimumChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_MinimumChargeWeight);
        }
    }

    public Decimal Standard_HamaliPerKg
    {
        set
        {
            hdn_Standard_HamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_HamaliPerKg);
        }
    }

    public Decimal Standard_ToPayCharges
    {
        set
        {
            hdn_Standard_ToPayCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_ToPayCharges);
        }
    }



    public Decimal Standard_NForm_Charge
    {
        set
        {
            hdn_Standard_NForm_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_NForm_Charge);
        }
    }


    //----------------- Delivery Related Standard Charges Start ------------------------

    public Decimal Standard_Octroi_Form_Charge
    {
        set
        {
            hdn_Standard_Octroi_Form_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_Octroi_Form_Charge);
        }
    }

    public Decimal Standard_Octroi_Service_Charge
    {
        set
        {
            hdn_Standard_Octroi_Service_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_Octroi_Service_Charge);
        }
    }

    public Decimal Standard_GI_Charges
    {
        set
        {
            hdn_Standard_GI_Charges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_GI_Charges);
        }
    }

    public Decimal Standard_Demurrage_Days
    {
        set
        {
            hdn_Standard_Demurrage_Days.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_Demurrage_Days);
        }
    }

    public Decimal Standard_Demurrage_Rate
    {
        set
        {
            hdn_Standard_Demurrage_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_Demurrage_Rate);
        }
    }
    // ------------------------ Contractual Charges ------------------------

    public Decimal Contractual_BiltiCharges
    {
        set
        {
            hdn_Contractual_BiltiCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_BiltiCharges);
        }
    }

    public Decimal Contractual_DDCharge_Rate
    {
        set
        {
            hdn_Contractual_DDCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_DDCharge_Rate);
        }
    }

    public Decimal Contractual_DDCharge
    {
        set
        {
            hdn_Contractual_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_DDCharge);
        }
    }

    public Decimal Contractual_DACCCharges
    {
        set
        {
            hdn_Contractual_DACCCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_DACCCharges);
        }
    }

    public Decimal Contractual_FOV
    {
        set
        {
            hdn_Contractual_FOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_FOV);
        }
    }
    
    public Decimal Contractual_FOVRate
    {
        set
        {
            hdn_Contractual_FOVRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_FOVRate);
        }
    }

    public Decimal Contractual_Invoice_Rate
    {
        set
        {
            hdn_Contractual_Invoice_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_Invoice_Rate);
        }
    }
    
    public Decimal Contractual_Invoice_Per_How_Many_Rs
    {
        set
        {
            hdn_Contractual_Invoice_Per_How_Many_Rs.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_Invoice_Per_How_Many_Rs);
        }
    }
    
    public Decimal Contractual_FOVPercentage
    {
        set
        {
            hdn_Contractual_FOVPercentage.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_FOVPercentage);
        }
    }

    public Decimal Contractual_LengthCharge
    {
        set
        {
            hdn_Contractual_LengthCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_LengthCharge);
        }
    }

    public Decimal Contractual_CFTFactor
    {
        set
        {
            hdn_Contractual_CFTFactor.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_CFTFactor);
        }
    }

    public Decimal Contractual_FreightAmount
    {
        set
        {
            hdn_Contractual_FreightAmount.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_FreightAmount);
        }
    }

    public Decimal Contractual_FreightRate
    {
        set
        {
            hdn_Contractual_FreightRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_FreightRate);
        }
    }

    //public Decimal Special_Freight_Rate
    //{
    //    set
    //    {
    //        hdn_Special_Freight_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
    //    }
    //    get
    //    {
    //        return ValueOfHdn_Decimal(hdn_Special_Freight_Rate);
    //    }
    //}

    public Decimal Contractual_HamaliCharge
    {
        set
        {
            hdn_Contractual_HamaliCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_HamaliCharge);
        }
    }

    public Decimal Contractual_LocalCharge
    {
        set
        {
            hdn_Contractual_LocalCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_LocalCharge);
        }
    }

    public Decimal Contractual_LocalCharge_Rate
    {
        set
        {
            hdn_Contractual_LocalCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_LocalCharge_Rate);
        }
    }

    public Decimal Contractual_ServiceTaxAmount
    {
        set
        {
            hdn_Contractual_ServiceTaxAmount.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_ServiceTaxAmount);
        }
    }

    public Decimal Contractual_ToPayCharges
    {
        set
        {
            hdn_Contractual_ToPayCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_ToPayCharges);
        }
    }
    public Decimal Contractual_NForm_Charge
    {
        set
        {
            hdn_Contractual_NForm_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_NForm_Charge);
        }
    }
    public Decimal Contractual_ServiceTaxPercent
    {
        set
        {
            hdn_Contractual_ServiceTaxPercent.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_ServiceTaxPercent);
        }
    }
    public Decimal Contractual_MinimumFOV
    {
        set
        {
            hdn_Contractual_MinimumFOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_MinimumFOV);
        }
    }
    public Decimal Contractual_MinimumChargeWeight
    {
        set
        {
            hdn_Contractual_MinimumChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_MinimumChargeWeight);
        }
    }

    public Decimal Contractual_MinimumHamaliPerKg
    {
        set
        {
            hdn_Contractual_MinimumHamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_HamaliPerKg);
        }
    }

    public Decimal Contractual_MinFOV
    {
        set
        {
            hdn_Contractual_MinimumFOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_MinimumFOV);
        }
    }

    public Decimal Contractual_HamaliPerKg
    {
        set
        {
            hdn_Contractual_HamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_HamaliPerKg);
        }
    }
    
    public Decimal Contractual_HamaliPerArticles
    {
        set
        {
            hdn_Contractual_HamaliPerArticles.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_HamaliPerArticles);
        }
    }

    //----------------- Delivery Related Contractual Standard Charges Start ------------------------

    public Decimal Contractual_Octroi_Form_Charge
    {
        set
        {
            hdn_Contractual_Octroi_Form_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_Octroi_Form_Charge);
        }
    }

    public Decimal Contractual_Octroi_Service_Charge
    {
        set
        {
            hdn_Contractual_Octroi_Service_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_Octroi_Service_Charge);
        }
    }

    public Decimal Contractual_GI_Charges
    {
        set
        {
            hdn_Contractual_GI_Charges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_GI_Charges);
        }
    }

    public Decimal Contractual_Demurrage_Days
    {
        set
        {
            hdn_Contractual_Demurrage_Days.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_Demurrage_Days);
        }
    }

    public Decimal Contractual_Demurrage_Rate
    {
        set
        {
            hdn_Contractual_Demurrage_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Contractual_Demurrage_Rate);
        }
    }

    //----------------- Delivery Related Charges End ------------------------

    //public Decimal Contractual_CFT_Factor
    //{
    //    set
    //    {
    //        hdn_Contractual_CFT_Factor.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
    //    }
    //    get
    //    {
    //        return ValueOfHdn_Decimal(hdn_Contractual_CFT_Factor);
    //    }
    //}

    //public Decimal Contractual_DACCCharges
    //{
    //    set
    //    {
    //        hdn_Contractual_DACCCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
    //    }
    //    get
    //    {
    //        return ValueOfHdn_Decimal(hdn_Contractual_DACCCharges);
    //    }
    //}

    // ------------------------ Applicable Standard Charges ------------------------

    public Decimal Applicable_Standard_BiltiCharges
    {
        set
        {
            hdn_Applicable_Standard_BiltiCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_BiltiCharges);
        }
    }

    public Decimal Applicable_Standard_DDCharge_Rate
    {
        set
        {
            hdn_Applicable_Standard_DDCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_DDCharge_Rate);
        }
    }

    public Decimal Applicable_Standard_DDCharge
    {
        set
        {
            hdn_Applicable_Standard_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_DDCharge);
        }
    }

    public Decimal Applicable_Standard_DACCCharges
    {
        set
        {
            hdn_Applicable_Standard_DACCCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_DACCCharges);
        }
    }

    public Decimal Applicable_Standard_FOV
    {
        set
        {
            hdn_Applicable_Standard_FOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_FOV);
        }
    }
    
    public Decimal Applicable_Standard_FOVRate
    {
        set
        {
            hdn_Applicable_Standard_FOVRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_FOVRate);
        }
    }

    public Decimal Applicable_Standard_Invoice_Rate
    {
        set
        {
            hdn_Applicable_Standard_Invoice_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_Invoice_Rate);
        }
    }
    
    public Decimal Applicable_Standard_Invoice_Per_How_Many_Rs
    {
        set
        {
            hdn_Applicable_Standard_Invoice_Per_How_Many_Rs.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_Invoice_Per_How_Many_Rs);
        }
    }
    
    public Decimal Applicable_Standard_FOVPercentage
    {
        set
        {
            hdn_Applicable_Standard_FOVPercentage.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_FOVPercentage);
        }
    }

    public Decimal Applicable_Standard_LengthCharge
    {
        set
        {
            hdn_Applicable_Standard_LengthCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_LengthCharge);
        }
    }

    public Decimal Applicable_Standard_FreightAmount
    {
        set
        {
            hdn_Applicable_Standard_FreightAmount.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_FreightAmount);
        }
    }

    public Decimal Applicable_Standard_FreightRate
    {
        set
        {
            hdn_Applicable_Standard_FreightRate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_FreightRate);
        }
    }

    //public Decimal Special_Freight_Rate
    //{
    //    set
    //    {
    //        hdn_Special_Freight_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
    //    }
    //    get
    //    {
    //        return ValueOfHdn_Decimal(hdn_Special_Freight_Rate);
    //    }
    //}

    public Decimal Applicable_Standard_HamaliCharge
    {
        set
        {
            hdn_Applicable_Standard_HamaliCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_HamaliCharge);
        }
    }

    public Decimal Applicable_Standard_LocalCharge
    {
        set
        {
            hdn_Applicable_Standard_LocalCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_LocalCharge);
        }
    }

    public Decimal Applicable_Standard_LocalCharge_Rate
    {
        set
        {
            hdn_Applicable_Standard_LocalCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_LocalCharge_Rate);
        }
    }

    public Decimal Applicable_Standard_ServiceTaxAmount
    {
        set
        {
            hdn_Applicable_Standard_ServiceTaxAmount.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_ServiceTaxAmount);
        }
    }

    public Decimal Applicable_Standard_ToPayCharges
    {
        set
        {
            hdn_Applicable_Standard_ToPayCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_ToPayCharges);
        }
    }

    public Decimal Applicable_Standard_NForm_Charge
    {
        set
        {
            hdn_Applicable_Standard_NForm_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_NForm_Charge);
        }
    }
    public Decimal Applicable_Standard_ServiceTaxPercent
    {
        set
        {
            hdn_Applicable_Standard_ServiceTaxPercent.Value = Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_ServiceTaxPercent);
        }
    }
    public Decimal Applicable_Standard_MinimumFOV
    {
        set
        {
            hdn_Applicable_Standard_MinimumFOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_MinimumFOV);
        }
    }
    public Decimal Applicable_Standard_MinimumChargeWeight
    {
        set
        {
            hdn_Applicable_Standard_MinimumChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_MinimumChargeWeight);
        }
    }
    public Decimal Applicable_Standard_MinimumHamaliPerKg
    {
        set
        {
            hdn_Applicable_Standard_MinimumHamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_MinimumHamaliPerKg);
        }
    }
    //public Decimal Applicable_Standard_MinimumFOV
    //{
    //    set
    //    {
    //        hdn_Applicable_Standard_MinimumFOV.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
    //    }
    //    get
    //    {
    //        return ValueOfHdn_Decimal(hdn_Applicable_Standard_MinimumFOV);
    //    }
    //}

    public Decimal Applicable_Standard_HamaliPerKg
    {
        set
        {
            hdn_Applicable_Standard_HamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_HamaliPerKg);
        }
    }
    public Decimal Applicable_Standard_HamaliPerArticles
    {
        set
        {
            hdn_Applicable_Standard_HamaliPerArticles.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_HamaliPerArticles);
        }
    }
    public Decimal Applicable_Standard_CFTFactor
    {
        set
        {
            hdn_Applicable_Standard_CFTFactor.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_CFTFactor);
        }
    }

    //----------------- Delivery Related Applicable Standard Charges Start ------------------------

    public Decimal Applicable_Standard_Octroi_Form_Charge
    {
        set
        {
            hdn_Applicable_Standard_Octroi_Form_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_Octroi_Form_Charge);
        }
    }

    public Decimal Applicable_Standard_Octroi_Service_Charge
    {
        set
        {
            hdn_Applicable_Standard_Octroi_Service_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_Octroi_Service_Charge);
        }
    }

    public Decimal Applicable_Standard_GI_Charges
    {
        set
        {
            hdn_Applicable_Standard_GI_Charges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_GI_Charges);
        }
    }

    public Decimal Applicable_Standard_Demurrage_Days
    {
        set
        {
            hdn_Applicable_Standard_Demurrage_Days.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_Demurrage_Days);
        }
    }

    public Decimal Applicable_Standard_Demurrage_Rate
    {
        set
        {
            hdn_Applicable_Standard_Demurrage_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Applicable_Standard_Demurrage_Rate);
        }
    }

    //----------------- Delivery Related Charges End ------------------------

    //public Decimal Applicable_Standard_DACCCharges
    //{
    //    set
    //    {
    //        hdn_Applicable_Standard_DACCCharges.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
    //    }
    //    get
    //    {
    //        return ValueOfHdn_Decimal(hdn_Applicable_Standard_DACCCharges);
    //    }
    //}

    public Decimal TotalLength
    {
        set
        {
            lbl_TotalLength.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_TotalLength.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            lbl_TotalLengthNandwana.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_TotalLengthNandwana.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalLength);
        }
    }

    public Decimal TotalHeight
    {
        set
        {
            lbl_TotalHeight.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_TotalHeight.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            lbl_TotalHeightNandwana.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_TotalHeightNandwana.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalHeight);
        }
    }

    public Decimal ChargeWeight
    {
        set
        {
            txt_ChargeWeight.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_ChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ChargeWeight);
        }
    }

    public Decimal ActualWeight
    {
        set {}
        get{return ValueOfLable_Decimal(lbl_TotalWeight);}
    }

    public Decimal TotalCFT
    {
        set
        {
            txt_TotalCFT.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_TotalCFT.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalCFT);
        }
    }

    public Decimal TotalCBM
    {
        set
        {
            txt_TotalCBM.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_TotalCBM.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalCBM);
        }
    }

    public Decimal TaxAbatePercent
    {
        set{}
        get{return Util.String2Decimal("0.75");}
    }

    public Decimal FreightRate
    {
        set
        {
            txt_FreightRate.Text = Util.Decimal2String(value);
            hdn_FreightRate.Value = Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_FreightRate);
        }
    }

    public Decimal Freight
    {
        set
        {
            txt_Freight.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Freight.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfTextBox(txt_Freight);
        }
    }

    public Decimal FOVRiskCharge
    {
        set
        {
            txt_FOVRiskCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_FOVRiskCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_FOVRiskCharge);
        }
    }

    public Decimal SubTotal
    {
        set
        {
            lbl_SubTotalValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_SubTotal.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_SubTotal);
        }
    }

    public Decimal ReBookGC_SubTotal
    {
        set{}
        get{return 0;}
    }

    public Decimal Abatment
    {
        set
        {
            lbl_AbatmentValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_Abatment.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Abatment);
        }
    }

    public Decimal TaxableAmount
    {
        set
        {
            lbl_TaxableAmountValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_TaxableAmount.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TaxableAmount);
        }
    }

    public Decimal ServiceTax
    {
        set
        {
            lbl_ServiceTaxValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_ServiceTax.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ServiceTax);
        }
    }

    public Decimal ReBookGC_OctroiAmount
    {
        set{}
        get{return 0;}
    }
    public Decimal TotalGCAmount
    {
        set
        {
            lbl_TotalGCAmountValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_TotalGCAmount.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalGCAmount);
        }
    }

    public Decimal Previous_SubTotal
    {
        set
        {            
            hdn_Previous_SubTotal.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Previous_SubTotal);
        }
    }

    public Decimal Previous_GrandTotal
    {
        set
        {            
            hdn_Previous_GrandTotal.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Previous_GrandTotal);
        }
    }

    public Decimal ReBookGC_Amount
    {
        set
        {
        }
        get
        {
            return 0;// ValueOfHdn_Decimal(hdn_ReBookGCAmount);
        }
    }

    public Decimal LengthCharge
    {
        set
        {
            txt_LengthCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LengthCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_LengthCharge);
        }
    }

    public Decimal UnloadingCharge
    {
        set
        {
            txt_UnloadingCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnloadingCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_UnloadingCharge);
        }
    }

    public Decimal Advance
    {
        set
        {
            txt_Advance.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Advance.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Advance);
        }
    }

    public Decimal CashAmount
    {
        set
        {
            txt_CashAmount.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_CashAmount.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_CashAmount);
        }
    }

    public Decimal ChequeAmount
    {
        set
        {
            txt_ChequeAmount.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_ChequeAmount.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ChequeAmount);
        }
    }

    public int Billing_Party_Ledger_Id
    {
        set
        {
            hdn_Billing_Party_Ledger_Id.Value = Util.Int2String (value);
        }
        get
        {
            return ValueOfHdn_Int (hdn_Billing_Party_Ledger_Id);
        }
    }
    
    public Decimal Billing_Party_Closing_Balance
    {
        set
        {
            hdn_Billing_Party_Closing_Balance.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Billing_Party_Closing_Balance);
        }
    }

    public Decimal Billing_Party_Credit_Limit
    {
        set
        {
            hdn_Billing_Party_Credit_Limit.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Billing_Party_Credit_Limit);
        }
    }
    
    public String No_For_Padd
    {
        set
        {
            hdn_No_For_Padd.Value = value;
        }
        get
        {
            return hdn_No_For_Padd.Value.Trim();
        }
    }
    
    public String LoadingSuperVisor_RequiredFor_BookingType
    {
        set
        {
            hdn_LoadingSuperVisor_RequiredFor_BookingType.Value = value;
        }
        get
        {
            return hdn_LoadingSuperVisor_RequiredFor_BookingType.Value.Trim();
        }
    }




    public String Container_Details_RequiredFor_BookingType
    {
        set
        {
            hdn_Container_Details_RequiredFor_BookingType.Value = value;
        }
        get
        {
            return hdn_Container_Details_RequiredFor_BookingType.Value.Trim();
        }
    }
    
    
    public String In_Valid_Credit_Limit_Client_Name
    {
        set
        {
            hdn_In_Valid_Credit_Limit_Client_Name.Value = value;
        }
        get
        {
            return hdn_In_Valid_Credit_Limit_Client_Name.Value;
        }
    }

    public String Previous_Document_No_For_Print
    {
        set
        {
            hdn_PreviousDocumentNoForPrint.Value = value;
        }
        get
        {
            return hdn_PreviousDocumentNoForPrint.Value.Trim();
        }
    }

    public String ChequeNo
    {
        set
        {
            txt_ChequeNo.Text = value;
            hdn_ChequeNo.Value = value;
        }
        get
        {
            return txt_ChequeNo.Text.Trim() == string.Empty ? "0" : txt_ChequeNo.Text.Trim();
        }
    }

    public String Series
    {
        set
        {
            lbl_Series.Text = value;
        }
        get
        {
            return lbl_Series.Text.Trim();
        }
    }

    public String Default_Cheque_Branch_Ledger_Name
    {
        set
        {
            hdn_Default_Cheque_Branch_Ledger_Name.Value   = value;
        }
        get
        {
            return hdn_Default_Cheque_Branch_Ledger_Name.Value.Trim();
        }
    }

    public String Default_Cheque_Bank_Ledger_Name
    {
        set
        {
            hdn_Default_Cheque_Bank_Ledger_Name.Value  = value;
        }
        get
        {
            return hdn_Default_Cheque_Bank_Ledger_Name.Value.Trim();
        }
    }

    public String Default_Cash_Ledger_Name 
    {
        set
        {
            hdn_Default_Cash_Ledger_Name.Value  = value;
        }
        get
        {
            return hdn_Default_Cash_Ledger_Name.Value.Trim();
        }
    }
    
    public String ServiceTax_Label
    {
        set
        {
            lbl_ServiceTax.Text = value;// "Service Tax " + Standard_ServiceTaxPercent.ToString() + "%";
        }
    }

    public String BankName
    {
        set
        {
            txt_BankName.Text = value;
            hdn_BankName.Value = value;
        }
        get
        {
            return hdn_BankName.Value.Trim();
        }
    }

    public String InstructionRemark
    {
        set
        {
            txt_InstructionRemark.Text = value;
        }
        get
        {
            return txt_InstructionRemark.Text.Trim();
        }
    }

    public String OtherChargesRemark
    {
        set
        {
            txt_OtherChargesRemark.Text = value;
        }
        get
        {
            return txt_OtherChargesRemark.Text.Trim();
        }
    }

    public String Enclosures
    {
        set
        {
            txt_Enclosure.Text = value;
        }
        get
        {
            return txt_Enclosure.Text.Trim();
        }
    }

    public String VehicleNo
    {
        set
        {
        }
        get
        {
            return "";
        }
    }

    public String STMNo
    {
        set
        {
        }
        get
        {
            return "";
        }
    }

    public String FeasibilityRouteSurveyNo
    {
        set
        {
        }
        get
        {
            return "";
        }
    }

    public String InsuranceCompany
    {
        set
        {
            txt_InsuranceCompany.Text = value;
        }
        get
        {
            return txt_InsuranceCompany.Text.Trim();
        }
    }

    public Int32 Session_ContainerTypeId
    {
        get { return StateManager.GetState<Int32>("ContainerTypeId"); }
        set { StateManager.SaveState("ContainerTypeId", value); }
    }

    public String Session_ContainerNoPart1
    {
        get { return StateManager.GetState<String>("ContainerNoPart1"); }
        set { StateManager.SaveState("ContainerNoPart1", value); }
    }

    public String Session_ContainerNoPart2
    {
        get { return StateManager.GetState<String>("ContainerNoPart2"); }
        set { StateManager.SaveState("ContainerNoPart2", value); }
    }

    public String Session_SealNo
    {
        get { return StateManager.GetState<String>("SealNo"); }
        set { StateManager.SaveState("SealNo", value); }
    }

    public Int32 Session_ReturnToYardId
    {
        get { return StateManager.GetState<Int32>("ReturnToYardId"); }
        set { StateManager.SaveState("ReturnToYardId", value); }
    }

    public String Session_ReturnToYardName
    {
        get { return StateManager.GetState<String>("ReturnToYardName"); }
        set { StateManager.SaveState("ReturnToYardName", value); }
    }

    public String Session_NFormNo
    {
        get { return StateManager.GetState<String>("NFormNo"); }
        set { StateManager.SaveState("NFormNo", value); }
    }    

    public String Session_InsuranceCompany
    {
        get { return StateManager.GetState<String>("InsuranceCompany"); }
        set { StateManager.SaveState("InsuranceCompany", value); }
    }

    public String PolicyNo
    {
        set
        {
            txt_PolicyNo.Text = value;
        }
        get
        {
            return txt_PolicyNo.Text.Trim();
        }
    }

    public String Session_PolicyNo
    {
        get { return StateManager.GetState<String>("PolicyNo"); }
        set { StateManager.SaveState("PolicyNo", value); }
    }

    public String BillingRemark
    {
        set
        {
            txt_BillingRemark.Text = value;
        }
        get
        {
            return txt_BillingRemark.Text.Trim();
        }
    }
    
    public String RoadPermitSrNo 
    {
        set
        {
        }
        get
        {
            return "";
        }
    }
    
    public String ClientCode
    {
        set
        {
            hdn_ClientCode.Value = value.ToLower();
        }
        get
        {
            return hdn_ClientCode.Value.Trim().ToLower();
        }
    }

    public String RegularClientCaption
    {
        set{hdn_RegularClientCaption.Value = value;}
        get{return hdn_RegularClientCaption.Value;}
    }

    public String CustomerRefNo
    {
        set
        {
        }
        get
        {
            return "";
        }
    }

    public String Session_ConsigneeAddressLine1
    {
        get { return StateManager.GetState<String>("ConsigneeAddressLine1"); }
        set { StateManager.SaveState("ConsigneeAddressLine1", value); }
    }

    public String Session_ConsigneeAddressLine2
    {
        get { return StateManager.GetState<String>("ConsigneeAddressLine2"); }
        set { StateManager.SaveState("ConsigneeAddressLine2", value); }
    }

    public String Session_ConsigneeActualAddressLine1
    {
        get { return StateManager.GetState<String>("ConsigneeActualAddressLine1"); }
        set { StateManager.SaveState("ConsigneeActualAddressLine1", value); }
    }

    public String Session_ConsigneeActualAddressLine2
    {
        get { return StateManager.GetState<String>("ConsigneeActualAddressLine2"); }
        set { StateManager.SaveState("ConsigneeActualAddressLine2", value); }
    }

    public String Session_ConsigneeName
    {
        get { return StateManager.GetState<String>("ConsigneeName"); }
        set { StateManager.SaveState("ConsigneeName", value); }
    }

    public String MultipleCommodityXml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_MultipleCommodityGrid.Copy());
            _objDs.Tables[0].TableName = "multiple_commodity";
            return _objDs.GetXml().ToLower();
        }
    }

    public String InvoiceXml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_InvoiceGrid.Copy());
            _objDs.Tables[0].TableName = "invoice";
            return _objDs.GetXml().ToLower();
        }
    }

    public String OtherChargesXml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_GCOtherChargeHead.Copy());
            _objDs.Tables[0].TableName = "othercharges";
            return _objDs.GetXml().ToLower();
        }
    }

    public String BillingDetailsXml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_Main_BillingDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "billing_details";
            return _objDs.GetXml().ToLower();
        }
    }
    
    public String ChequeDetailsXml
    {
        get
        {
            Session_ChequeDetailsGrid.Clear();
            DataRow objdr;
            objdr = Session_ChequeDetailsGrid.NewRow();
                        
            objdr["cheque_bank_name"]  = Default_Cheque_Bank_Ledger_Name;
            objdr["cheque_branch_name"] = Default_Cheque_Branch_Ledger_Name;
            objdr["cheque_no"]= ChequeNo;
            objdr["bank_ledger_id"] = Default_Bank_Ledger_Id ;
            objdr["cheque_amount"]=  ChequeAmount ;
            objdr["cheque_date"]  = ChequeDate.ToString("dd MMMM yyyy");

            Session_ChequeDetailsGrid.Rows.Add(objdr);

            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_ChequeDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "mrchequedetails";
            return _objDs.GetXml().ToLower();
        }
    }

    public Int32 Session_ConsigneeId
    {
        get { return StateManager.GetState<Int32>("ConsigneeId"); }
        set { StateManager.SaveState("ConsigneeId", value); }
    }

    public int LoadingSuperVisorId
    {
        set
        {
            hdn_LoadingSuperVisorId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_LoadingSuperVisorId);
        }
    }

    public int MarketingExecutiveId
    {
        set
        {
            hdn_MarketingExecutiveId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_MarketingExecutiveId);
        }
    }

    public int FreightBasisId
    {
        set
        {
            ddl_FreightBasis.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_FreightBasis.SelectedValue);
        }
    }

    public int VolumetricFreightUnitId
    {
        set
        {
            if (value > 0)
            {
                ddl_VolumetricFreightUnit.SelectedValue = Util.Int2String(value);
            }
        }
        get
        {
            int VolumetricFreightUnit;

            if (FreightBasisId == 4)
            {
                VolumetricFreightUnit = Convert.ToInt32(ddl_VolumetricFreightUnit.SelectedValue);
            }
            else
            {
                VolumetricFreightUnit = 0;
            }

            return VolumetricFreightUnit;
        }
    }

    public int BookingTypeId
    {
        set
        {
            ddl_BookingType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_BookingType.SelectedValue);
        }
    }

    public int BookingSubTypeId
    {
        set
        {
            if (value > 0)
            {
            }
        }
        get
        {
            int temp_BookingSubTypeId;
            temp_BookingSubTypeId = 0;
            return temp_BookingSubTypeId;
        }
    }

    public int BookingModeId
    {
        set{ }
        get
        {
            return 1; // surface..
        }
    }

    public int ConsignmentTypeId
    {
        set
        {
            ddl_ConsignmentType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_ConsignmentType.SelectedValue);
        }
    }

    public int DeliveryTypeId
    {
        set
        {
            ddl_DeliveryType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_DeliveryType.SelectedValue);
        }
    }

    public int DeliveryAgainstId
    {
        set
        {
        }
        get
        {
            return 0;
        }
    }

    public int VehicleTypeId
    {
        set
        {
            //ddl_VehicleType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return 0;// Convert.ToInt32(ddl_VehicleType.SelectedValue);
        }
    }

    public int PaymentTypeId
    {
        set
        {
            ddl_PaymentType.SelectedValue = Util.Int2String(value);
            hdn_OldPaymentType.Value = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_PaymentType.SelectedValue);
        }
    }

    public int UnitOfMeasurementId
    {
        set
        {
            ddl_UnitOfMeasurment.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_UnitOfMeasurment.SelectedValue);
        }
    }

    public int ContractId
    {
        set
        {
            ddl_Contract.SelectedValue = Util.Int2String(value);
            hdn_ContractId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ContractId);
        }
    }

    public int LengthChargeHeadId
    {
        set
        {
            ddl_LengthChargeHead.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_LengthChargeHead.SelectedValue);// ValueOfHdn_Int(hdn_LengthChargeHeadId);
        }
    }

    public int PickupTypeId
    {
        set
        {
            ddl_PickupType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_PickupType.SelectedValue);
        }
    }

    public int GCRiskId
    {
        set
        {
            ddl_GCRisk.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_GCRisk.SelectedValue);
        }
    }

    public int ServiceTaxPayableBy
    {
        set
        {
            ddl_ServiceTaxPayableBy.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_ServiceTaxPayableBy.SelectedValue);
        }
    }

    public int ReBook_GCOctroiPaidByID
    {
        set
        {
        }
        get
        {
            return 0;// ValueOfHdn_Int(hdn_ReBook_GCOctroiPaidByID);
        }
    }

    public Decimal ODAChargesUpTo500Kg
    {
        set
        {
            hdn_ODAChargesUpTo500Kg.Value = Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ODAChargesUpTo500Kg);
        }
    }

    public Decimal ODAChargesAbove500Kg
    {
        set
        {
            hdn_ODAChargesAbove500Kg.Value = Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ODAChargesAbove500Kg);
        }
    }

    public Decimal TotalInvoiceAmount
    {
        set
        {
            lbl_TotalInvoiceAmount.Text = Util.Decimal2String(value);
            hdn_TotalInvoiceAmount.Value = Util.Decimal2String(value);
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_TotalInvoiceAmount);
        }
    }

    public int ConsignorId
    {
        set
        {
            hdn_ConsignorId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsignorId);
        }
    }

    public int ConsigneeId
    {
        set
        {
            hdn_ConsigneeId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsigneeId);
        }
    }

    public int ConsignorCountryId
    {
        set
        {
            hdn_ConsignorCountryId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsignorCountryId);
        }
    }

    public int ConsignorCityId
    {
        set
        {
            hdn_ConsignorCityId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsignorCityId);
        }
    }

    public int ConsigneeCityId
    {
        set
        {
            hdn_ConsigneeCityId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsigneeCityId);
        }
    }

    public int ConsignorStateId
    {
        set
        {
            hdn_ConsignorStateId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsigneeStateId);
        }
    }

    public int ConsigneeCountryId
    {
        set
        {
            hdn_ConsigneeCountryId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsigneeCountryId);
        }
    }

    public int ConsigneeStateId
    {
        set
        {
            hdn_ConsigneeStateId.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_ConsigneeStateId);
        }
    }

    public int DeliveryBaranchId
    {
        set
        {
            hdn_DeliveryBaranchId.Value = Util.Int2String(value);
            Session_DeliveryBranchId = value;
        }
        get
        {
            return ValueOfHdn_Int(hdn_DeliveryBaranchId);
        }
    }

    public int TransitDays
    {
        set
        {
            hdn_TransitDays.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_TransitDays);
        }
    }

    public int Is_ServiceTaxApplicableForConsignor
    {
        set
        {
            hdn_IsServiceTaxApplicableForConsignor.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_IsServiceTaxApplicableForConsignor);
        }
    }

    public int Is_ServiceTaxApplicableForConsignee
    {
        set
        {
            hdn_IsServiceTaxApplicableForConsignee.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_IsServiceTaxApplicableForConsignee);
        }
    }

    public int RoadPermitTypeId
    {
        set
        {
            //ddl_RoadPermitType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return 0;// Util.String2Int(ddl_RoadPermitType.SelectedValue);
        }
    }

    public Decimal LocalCharge
    {
        set
        {            
            txt_LocalCharge.Text = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;
            hdn_LocalCharge.Value = Util.Decimal2String(Math.Round(value, 2)); // Util.Decimal2String(value);;            
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_LocalCharge);
        }
    }
    public Decimal VolumetricToKgFactor
    {
        set
        {
            txt_VolumetricToKgFactor.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_VolumetricToKgFactor.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_VolumetricToKgFactor);
        }
    }

    public Decimal PolicyAmount
    {
        set
        {
            txt_PolicyAmount.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfTextBox(txt_PolicyAmount);
        }
    }

    public Decimal Session_PolicyAmount
    {
        get { return StateManager.GetState<Decimal>("PolicyAmount"); }
        set { StateManager.SaveState("PolicyAmount", value); }
    }

    public Decimal RiskAmount
    {
        set
        {
            txt_RiskAmount.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfTextBox(txt_RiskAmount);
        }
    }

    public Decimal Session_RiskAmount
    {
        get { return StateManager.GetState<Decimal>("RiskAmount"); }
        set { StateManager.SaveState("RiskAmount", value); }
    }

    public Decimal LoadingCharge
    {
        set
        {
            txt_LoadingCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LoadingCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_LoadingCharge);
        }
    }

    public Decimal StationaryCharge
    {
        set
        {
            txt_StationaryCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_StationaryCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_StationaryCharge);
        }
    }
    public Decimal MaxStationaryCharge
    {
        set
        {
            hdn_MaxStationaryCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_StationaryCharge);
        }
    }
    public Decimal ToPayCharge
    {
        set
        {
            txt_ToPayCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_ToPayCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_ToPayCharge);
        }
    }

    public Decimal DDCharge
    {
        set
        {
            txt_DDCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_DDCharge);
        }
    }
    public Decimal NFormCharge
    {
        set
        {
            txt_NFormCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_NFormCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_NFormCharge);
        }
    }     

    public Decimal DACCCharges
    {
        set
        {
            txt_DACCCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_DACCCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_DACCCharge);
        }
    }

    public Decimal OtherCharges
    {
        set
        {
            lbl_OtherChargesValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_OtherCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_OtherCharge);
        }
    }

    public Decimal hdn_StandardMinimumChargeWeight
    {
        set
        {
            hdn_Standard_MinimumChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_MinimumChargeWeight);
        }
    }

    public Decimal hdn_StandardHamaliPerKg
    {
        set
        {
            hdn_Standard_HamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_HamaliPerKg);
        }
    }

    public Decimal Standard_HamaliPerArticles
    {
        set
        {
            hdn_Standard_HamaliPerArticles.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_HamaliPerArticles);
        }
    }

    //public Decimal hdn_HamaliPerKg
    //{
    //    set 
    //    { 
    //        hdn_HamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); 
    //    }
    //    get 
    //    { 
    //        return ValueOfHdn_Decimal(hdn_HamaliPerKg); 
    //    }
    //}

    //public Decimal hdn_CFTFactor
    //{
    //    set 
    //    { 
    //        hdn_CFT_Factor.Value = Util.Decimal2String(Math.Round(value, 2)); 
    //    }
    //    get 
    //    { 
    //        //return Util.String2Decimal(hdn_CFT_Factor.Value ); 
    //        return ValueOfHdn_Decimal(hdn_CFT_Factor);
    //    }
    //}

    //public Decimal hdn_FOVPercentage
    //{
    //    set 
    //    { 
    //        hdn_FOVPercentage.Value = Util.Decimal2String(Math.Round(value, 2)); 
    //    }
    //    get
    //    {            
    //        return ValueOfHdn_Decimal(hdn_FOVPercentage);
    //    }
    //}

    public Decimal hdn_StandardMinimumFOV
    {
        set
        {
            hdn_Standard_MinimumFOV.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_Standard_MinimumFOV);
        }
    }

    public Decimal UnitOfMeasurmentHeight
    {
        set
        {
            txt_UnitOfMeasurmentHeight.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnitOfMeasurmentHeight.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_UnitOfMeasurmentHeight);
        }
    }

    public Decimal UnitOfMeasurmentLength
    {
        set
        {
            txt_UnitOfMeasurmentLength.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnitOfMeasurmentLength.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_UnitOfMeasurmentLength);
        }
    }

    public Decimal UnitOfMeasurmentWidth
    {
        set
        {
            txt_UnitOfMeasurmentWidth.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnitOfMeasurmentWidth.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_UnitOfMeasurmentWidth);
        }
    }

    public Decimal HeightInFeet
    {
        set
        {
            txt_HeightInFeet.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_HeightInFeet.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_HeightInFeet);
        }
    }

    public Decimal LengthInFeet
    {
        set
        {
            txt_LengthInFeet.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LengthInFeet.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_LengthInFeet);
        }
    }

    public Decimal WidthInFeet
    {
        set
        {
            txt_WidthInFeet.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_WidthInFeet.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get
        {
            return ValueOfHdn_Decimal(hdn_WidthInFeet);
        }
    }

    #endregion

    #region ControlsBind

    public DataTable Bind_dg_Commodity
    {
        set
        {
            Session_MultipleCommodityGrid = value;

            dg_Commodity.DataSource = value;
            dg_Commodity.DataBind();

            dg_CommodityNandwana.DataSource = value;
            dg_CommodityNandwana.DataBind();
        }
    }

    public DataTable Bind_dg_Invoice
    {
        set
        {
            Session_InvoiceGrid = value;
            dg_Invoice.DataSource = value;
            dg_Invoice.DataBind();
       }
    }

    public DataTable BindDeliveryAgainst
    {
        set
        {
        }
    }

    public DataTable BindPickupType
    {
        set
        {
            ddl_PickupType.DataSource = value;
            ddl_PickupType.DataTextField = "Pickup_Type";
            ddl_PickupType.DataValueField = "Pickup_Type_ID";
            ddl_PickupType.DataBind();
        }
    }

    public DataTable BindDDLGC_NO
    {
        set
        {
            ddl_GC_No.DataSource = value;
            ddl_GC_No.DataTextField = "Branch_Code";
            ddl_GC_No.DataValueField = "Branch_Code";
            ddl_GC_No.DataBind();
        }
    }

    public DataTable BindFromLocation
    {
        set
        {
            ddl_FromLocation.DataTextField = "Location_Name";
            ddl_FromLocation.DataValueField = "Location_ID";
        }
    }

    public DataTable BindConsignor
    {
        set
        {
            ddl_Consignor.DataTextField = "Client_Name";
            ddl_Consignor.DataValueField = "Client_ID";
        }
    }

    public DataTable BindConsignee
    {
        set
        {
            ddl_Consignee.DataTextField = "Client_Name";
            ddl_Consignee.DataValueField = "Client_ID";
        }
    }

    public DataTable BindLoadingSuperVisor
    {
        set
        {
            ddl_LoadingSuperVisor.DataTextField = "Emp_Name";
            ddl_LoadingSuperVisor.DataValueField = "Emp_ID";
        }
    }

    public DataTable BindMarketingExecutive
    {
        set
        {
            ddl_MarketingExecutive.DataTextField = "Emp_Name";
            ddl_MarketingExecutive.DataValueField = "Emp_ID";
        }
    }

    public DataTable BindToLocation
    {
        set
        {
            ddl_ToLocation.DataTextField = "Location_Name";
            ddl_ToLocation.DataValueField = "Location_ID";
        }
    }

    public DataTable BindBookingBranch
    {
        set
        {
            ddl_BookingBranch.DataTextField = "Branch_Name";
            ddl_BookingBranch.DataValueField = "Branch_ID";
        }
    }

    public DataTable BindArrivedFromBranch
    {
        set
        {
            ddl_ArrivedFromBranch.DataTextField = "Branch_Name";
            ddl_ArrivedFromBranch.DataValueField = "Branch_ID";
        }
    }
    
    public DataTable BindCommodity
    {
        set
        {
            ddl_Commodity.DataSource = value;
            ddl_Commodity.DataTextField = "Commodity_Name";
            ddl_Commodity.DataValueField = "Commodity_Id";
            ddl_Commodity.DataBind();
            ddl_Commodity.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindItem
    {
        set
        {
            ddl_Item.DataSource = value;
            ddl_Item.DataTextField = "Item_Name";
            ddl_Item.DataValueField = "Item_Id";
            ddl_Item.DataBind();
            ddl_Item.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindPackingType
    {
        set
        {
            ddl_Packing_Type.DataSource = value;
            ddl_Packing_Type.DataTextField = "Packing_Type";
            ddl_Packing_Type.DataValueField = "Packing_Id";
            ddl_Packing_Type.DataBind();
            ddl_Packing_Type.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataSet BindContractBranches
    {
        set
        {
            ddl_ContractBranch.DataSource = null;
            ddl_ContractBranch.DataBind();

            ddl_ContractBranch.DataSource = value;
            ddl_ContractBranch.DataTextField = "Contract_Branch_Name";
            ddl_ContractBranch.DataValueField = "Contract_Branch_Id";
            ddl_ContractBranch.DataBind();
            ddl_ContractBranch.Items.Insert(0, new ListItem("Select One", "0"));

            Contract_BranchId = 0;
        }
    }

    public DataSet BindContract
    {
        set
        {
            ddl_Contract.Items.Clear();
            ddl_Contract.DataSource = value;
            ddl_Contract.DataTextField = "Contract_Name";
            ddl_Contract.DataValueField = "Contract_ID";
            ddl_Contract.DataBind();
            ddl_Contract.Items.Insert(0, new ListItem("Select One", "0"));

            ContractId = 0;
        }
    }

    public DataTable BindLengthChargeHead
    {
        set
        {
            ddl_LengthChargeHead.Items.Clear();
            ddl_LengthChargeHead.DataSource = value;
            ddl_LengthChargeHead.DataTextField = "Length_ChargeName";
            ddl_LengthChargeHead.DataValueField = "LengthChargeHeadID";
            ddl_LengthChargeHead.DataBind();
            ddl_LengthChargeHead.Items.Insert(0, new ListItem("Select One", "0"));

            LengthChargeHeadId  = 0;
        }
    }

    public DataTable BindUnitOfMeasurement
    {
        set
        {
            ddl_UnitOfMeasurment.DataSource = value;
            ddl_UnitOfMeasurment.DataTextField = "Unit_Of_Measurement";
            ddl_UnitOfMeasurment.DataValueField = "Unit_Of_Measurement_ID";
            ddl_UnitOfMeasurment.DataBind();
        }
    }

    public DataTable BindFreightBasis
    {
        set
        {
            ddl_FreightBasis.DataSource = value;
            ddl_FreightBasis.DataTextField = "Freight_Basis";
            ddl_FreightBasis.DataValueField = "Freight_Basis_ID";
            ddl_FreightBasis.DataBind();
        }
    }

    public DataTable BindVolumetricFreightUnit
    {
        set
        {
            ddl_VolumetricFreightUnit.DataSource = value;
            ddl_VolumetricFreightUnit.DataTextField = "Volumetric_Freight_Unit";
            ddl_VolumetricFreightUnit.DataValueField = "Volumetric_Freight_Unit_ID";
            ddl_VolumetricFreightUnit.DataBind();
        }
    }

    public DataTable BindBookingType
    {
        set
        {
            ddl_BookingType.DataSource = value;
            ddl_BookingType.DataTextField = "Booking_Type";
            ddl_BookingType.DataValueField = "Booking_Type_Id";
            ddl_BookingType.DataBind();
        }
    }

    public DataTable BindDeliveryType
    {
        set
        {
            ddl_DeliveryType.DataSource = value;
            ddl_DeliveryType.DataTextField = "Delivery_Type";
            ddl_DeliveryType.DataValueField = "Delivery_Type_Id";
            ddl_DeliveryType.DataBind();
        }
    }

    public DataTable BindVehicleType
    {
        set
        {
        }
    }

    public DataTable BindConsignmentType
    {
        set
        {
            ddl_ConsignmentType.DataSource = value;
            ddl_ConsignmentType.DataTextField = "Consignment_Type";
            ddl_ConsignmentType.DataValueField = "Consignment_Type_Id";
            ddl_ConsignmentType.DataBind();
        }
    }

    public DataTable BindPaymentType
    {
        set
        {
            ddl_PaymentType.DataSource = value;
            ddl_PaymentType.DataTextField = "Payment_Type";
            ddl_PaymentType.DataValueField = "Payment_Type_Id";
            ddl_PaymentType.DataBind();
        }
    }

    public DataTable BindGCRiskType
    {
        set
        {
            ddl_GCRisk.DataSource = value;
            ddl_GCRisk.DataTextField = "Risk_Type";
            ddl_GCRisk.DataValueField = "Risk_Type_ID";
            ddl_GCRisk.DataBind();
        }
    }

    public DataTable BindRoadPermitType
    {
        set
        {
        }
    }

    public DataTable BindGCInstructions
    {
        set
        {
            ddl_Instruction.DataSource = value;
            ddl_Instruction.DataTextField = "Instructions";
            ddl_Instruction.DataValueField = "GC_Instruction_Id";
            ddl_Instruction.DataBind();
            ddl_Instruction.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindBookingSubType
    {
        set
        {
        }
    }
    #endregion
    
    public DataTable Session_GCOtherChargeHead
    {
        get { return StateManager.GetState<DataTable>("GCOtherChargeHead"); }
        set { StateManager.SaveState("GCOtherChargeHead", value); }
    }
    public DataTable Session_ContainerType
    {
        get { return StateManager.GetState<DataTable>("ContainerType"); }
        set { StateManager.SaveState("ContainerType", value); }
    }
    public Boolean Session_Is_Validate_Credit_Limit
    {
        get { return StateManager.GetState<Boolean >("Is_Validate_Credit_Limit"); }
        set { StateManager.SaveState("Is_Validate_Credit_Limit", value); }
    }
    public DataTable Session_BillingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("BillingDetailsGrid"); }
        set { StateManager.SaveState("BillingDetailsGrid", value); }
    }
    public DataTable Session_Main_BillingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("Main_BillingDetailsGrid"); }
        set { StateManager.SaveState("Main_BillingDetailsGrid", value); }
    }
    public DataTable Session_ChequeDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("ChequeDetailsGrid"); }
        set { StateManager.SaveState("ChequeDetailsGrid", value); }
    }
    public Decimal Session_TotalRatio
    {
        get { return StateManager.GetState<Decimal>("Session_TotalRatio"); }
        set { StateManager.SaveState("Session_TotalRatio", value); }
    }
    public DataTable Session_CommodityDdl
    {
        get { return StateManager.GetState<DataTable>("CommodityDdl"); }
        set { StateManager.SaveState("CommodityDdl", value); }
    }
    public DataSet Session_RequireForms
    {
        get { return StateManager.GetState<DataSet>("RequireForms"); }
        set { StateManager.SaveState("RequireForms", value); }
    }
    public DataSet Session_ContractualClientDetails
    {
        get { return StateManager.GetState<DataSet>("ContractualClientDetails"); }
        set { StateManager.SaveState("ContractualClientDetails", value); }
    }
    public DataSet Session_DS_ContractDetails
    {
        get { return StateManager.GetState<DataSet>("ContractDetails"); }
        set { StateManager.SaveState("ContractDetails", value); }
    }
    public String Session_DeliveryBranchName
    {
        get { return StateManager.GetState<String>("DeliveryBranchName"); }
        set { StateManager.SaveState("DeliveryBranchName", value); }
    }
    public Int32 Session_DeliveryBranchId
    {
        get { return StateManager.GetState<Int32>("DeliveryBranchId"); }
        set { StateManager.SaveState("DeliveryBranchId", value); }
    }
    public DataTable Session_MultipleCommodityGrid
    {
        get { return StateManager.GetState<DataTable>("MultipleCommodityGrid"); }
        set { StateManager.SaveState("MultipleCommodityGrid", value); }
    }
    public DataTable Session_InvoiceGrid
    {
        get { return StateManager.GetState<DataTable>("InvoiceGrid"); }
        set { StateManager.SaveState("InvoiceGrid", value); }
    }
    public DataTable Session_OtherChargesGrid
    {
        get { return StateManager.GetState<DataTable>("OtherChargesGrid"); }
        set { StateManager.SaveState("OtherChargesGrid", value); }
    }
    public DataTable Session_ItemDdl
    {
        get { return StateManager.GetState<DataTable>("ItemDdl"); }
        set { StateManager.SaveState("ItemDdl", value); }
    }
    public DataTable Session_PackingTypeDdl
    {
        get { return StateManager.GetState<DataTable>("PackingTypeDdl"); }
        set { StateManager.SaveState("PackingTypeDdl", value); }
    }

    #region Function

    public bool validate_GC_No()
    {
        string _Msg ="";
        Is_Valid = false;

        errorMessage = "";

        Is_Duplicate();

        if (txt_GC_No_For_Print.Text.Trim() == String.Empty )
        {
            errorMessage = GetLocalResourceObject("Msg_GCNo").ToString();
            _Msg = GetLocalResourceObject("Msg_GCNo").ToString();
        }
        else if (_meniItemID == 200 && Util.String2Int(GC_No) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_GCNo").ToString();
            txt_GC_No_For_Print.Focus();
        }
        else if (txt_GC_No_For_Print.Text.Trim().Length < GC_No_Length )
        {
            errorMessage = GetLocalResourceObject("Msg_InValidGCNoLength").ToString() + " " +
                           GC_No_Length.ToString() + " Digits Only.";

            _Msg = GetLocalResourceObject("Msg_InValidGCNoLength").ToString() + " " +
                           GC_No_Length.ToString() + " Digits Only.";
        }
        else if (_meniItemID != 200 && keyID <= 0 && 
            (Util.String2Int(txt_GC_No_For_Print.Text.Trim()) < Util.String2Int(hdn_Start_No.Value.Trim()) || 
            Util.String2Int(txt_GC_No_For_Print.Text.Trim()) > Util.String2Int(hdn_End_No.Value.Trim())))
        {
            errorMessage = GetLocalResourceObject("Msg_InValidGCNo").ToString() + hdn_Start_No.Value + " & " + hdn_End_No.Value + ".";
            _Msg = GetLocalResourceObject("Msg_InValidGCNo").ToString() + hdn_Start_No.Value + " & " + hdn_End_No.Value + ".";
        }      
        else if (_meniItemID == 200 && Is_Duplicate_GC_No)
        {
            errorMessage = GetLocalResourceObject("Msg_DuplicateGCNo").ToString();
            txt_GC_No_For_Print.Focus();
        }
        else if (Is_Duplicate_GC_No  && keyID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_DuplicateGCNo").ToString();
            _Msg = GetLocalResourceObject("Msg_DuplicateGCNo").ToString();
        }      
        else
        {
            Is_Valid = true;
        }
        return Is_Valid;
    }

    public bool validateUI()
    {
        int Validate_LoadingSuperVisor_RequiredFor_BookingTypeId = 0;

        char[] _Separator ={ ',' };
        string[] _IdArray ;

        _IdArray = LoadingSuperVisor_RequiredFor_BookingType.Split(_Separator);

        int i =0;

        DateTime Valid_Cheque_Start_Date;
        DateTime Valid_Cheque_End_Date;
        
        Valid_Cheque_Start_Date = wuc_BookingDate.SelectedDate;

        Valid_Cheque_Start_Date = wuc_BookingDate.SelectedDate.AddDays ( - Valid_Cheque_Start_Days  ); 

        Valid_Cheque_End_Date = wuc_BookingDate.SelectedDate.AddDays( Valid_Cheque_End_Days );
                
        if (_IdArray.Length > 0)
        {
            if (_IdArray[0].ToLower() == "all")
            {
                Validate_LoadingSuperVisor_RequiredFor_BookingTypeId = 0;
            }
            else
            {
                for (i = 0; i < _IdArray.Length - 1; i++)
                {
                    if (Util.String2Int(_IdArray[i]) == BookingTypeId)
                    {
                        Validate_LoadingSuperVisor_RequiredFor_BookingTypeId = BookingTypeId;
                        break;
                    }
                }
            }
        }
        else
        {
            Validate_LoadingSuperVisor_RequiredFor_BookingTypeId = 0;
            _IdArray[0] = "all";
        }

        Is_Valid = false;
        errorMessage = "";

        Is_Duplicate();

        if (ReBook_GC_Id > 0 && keyID <= 0 )
        {
            Allow_To_ReBook();

            if (Is_Allow_To_ReBook && Is_ReBookGC_Octroi_Applicable != Is_ReBookGC_Octroi_Updated)
            {
                errorMessage = GetLocalResourceObject("Msg_UpdateOctroi").ToString();
            }
            else if (!Is_Allow_To_ReBook)
            {
                errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
            }
            else
            {
                Is_Valid = true;
            }
        }
        else
        {
            Is_Valid = true;
        }

        if (Is_Valid == true)
        {
            Is_Valid = false;

            if (txt_GC_No_For_Print.Text.Trim() == String.Empty )
            {
                errorMessage = GetLocalResourceObject("Msg_GCNo").ToString();
                txt_GC_No_For_Print.Focus();
            }
            else if (_meniItemID == 200 && Util.String2Int(GC_No) <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_GCNo").ToString();
                txt_GC_No_For_Print.Focus();
            } 
            else if (txt_GC_No_For_Print.Text.Trim().Length < GC_No_Length )
            {
                errorMessage = GetLocalResourceObject("Msg_InValidGCNoLength").ToString() + " " + GC_No_Length.ToString() + " Digits Only.";
                txt_GC_No_For_Print.Focus();
            }
            else if (_meniItemID != 200 && keyID <= 0 && 
                (Util.String2Int(txt_GC_No_For_Print.Text.Trim()) < Util.String2Int(hdn_Start_No.Value.Trim()) || 
                Util.String2Int(txt_GC_No_For_Print.Text.Trim()) > Util.String2Int(hdn_End_No.Value.Trim())))
            {
                errorMessage = GetLocalResourceObject("Msg_InValidGCNo").ToString() + hdn_Start_No.Value + " & " + hdn_End_No.Value + ".";
                txt_GC_No_For_Print.Focus();
            }
            else if (_meniItemID == 200 && Is_Duplicate_GC_No)
            {
                errorMessage = GetLocalResourceObject("Msg_DuplicateGCNo").ToString();
                txt_GC_No_For_Print.Focus();
            }
            else if (Is_Duplicate_GC_No  && keyID <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_DuplicateGCNo").ToString();
                txt_GC_No_For_Print.Focus();
            }          
            else if (_meniItemID != 200 && Allow_To_Attached() == false && Is_Attached == true)
            {
                errorMessage = GetLocalResourceObject("Msg_CantAttached").ToString();
            }
            else if (PaymentTypeId == 5 && ReBook_GC_Id > 0)
            {
                errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
            }
            else if (!Is_Allow_To_ReBook && ReBook_GC_Id > 0 && keyID <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
            }
            else if (BookingDate > DateTime.Now)
            {
                errorMessage = GetLocalResourceObject("Msg_InValidBookingDate").ToString();
                wuc_BookingDate.Focus();
            }
            else if (_meniItemID != 200 && (BookingDate < UserManager.getUserParam().StartDate || BookingDate > UserManager.getUserParam().EndDate))
            {
                errorMessage = GetLocalResourceObject("Msg_InValidFinancialBookingDate").ToString();
            }
            else if (ClientCode.ToLower() != "nandwana" && _meniItemID == 200 && BookingDate >= ApplicationStartDate)//|| BookingDate > UserManager.getUserParam().EndDate)
            {
                errorMessage = GetLocalResourceObject("Msg_InValidBookingDate_For_OpeningGc").ToString()
                                 + " ( " + ApplicationStartDate.ToString("dd MMM yyyy") + " ) ";
            }
            else if (_meniItemID == 200 && ArrivedDate < BookingDate)//|| BookingDate > UserManager.getUserParam().EndDate)
            {
                errorMessage = GetLocalResourceObject("Msg_InValidArrivedDate_For_OpeningGc").ToString();
            }
            else if (ddl_ConsignmentType.Items.Count <= 0 && pc.Control_Is_Mandatory(ddl_ConsignmentType) == true)
            {
                errorMessage = GetLocalResourceObject("Msg_ConsignmentType").ToString();
                ddl_ConsignmentType.Focus();
            }
            else if (ddl_BookingType.Items.Count <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_BookingType").ToString();
                ddl_BookingType.Focus();
            }          
            else if (ddl_DeliveryType.Items.Count <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_DeliveryType").ToString();
                ddl_DeliveryType.Focus();
            }           
            else if (_meniItemID == 200 && BookingBranchId == 0)
            {
                errorMessage = GetLocalResourceObject("Msg_BookingBranch").ToString();
                ddl_BookingBranch.Focus();
            }
            else if (FromLocationId <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_FromLocation").ToString();
                ddl_FromLocation.Focus();
            }
            else if (ToLocationId <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_ToLocation").ToString();
                ddl_ToLocation.Focus();
            }          
            else if (ddl_PickupType.Items.Count <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_PickupType").ToString();
                ddl_PickupType.Focus();
            }
            else if (ConsignorId <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_Consignor").ToString();
                ddl_Consignor.Focus();
            }
            else if (ConsigneeId <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_Consignee").ToString();
                ddl_Consignee.Focus();
            }
            else if (ddl_PaymentType.Items.Count <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_PaymentType").ToString();
                ddl_PaymentType.Focus();
            }
            else if (PaymentTypeId == 1 && !Is_ToPayBookingApplicable)
            {
                errorMessage = GetLocalResourceObject("Msg_ToPayBookingNotApplicable").ToString();
                ddl_PaymentType.Focus();
            }
            else if (PaymentTypeId == 3 && BillingPartyId <= 0 && Is_MultipleBilling == false)
            {
                errorMessage = GetLocalResourceObject("Msg_BillingParty").ToString();
                ddl_BillingParty.Focus();
            }
            else if (ddl_GCRisk.Items.Count <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_GCRisk").ToString();
                ddl_GCRisk.Focus();
            }
            else if (GCRiskId == 2 && chk_IsInsured.Checked == false) // for career risk
            {
                errorMessage = GetLocalResourceObject("Msg_InsurenceDetails").ToString();
            }
            else if (GCRiskId == 2 && chk_IsInsured.Checked == true && !Is_Valid_Insurence_Details()) // for owner risk
            {
                errorMessage = GetLocalResourceObject("Msg_InvalidInsurenceDetails").ToString();
            }
            else if (TotalArticles <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_CommodityDetails").ToString();
            }
            else if (TotalWeight <= 0) //else if (FreightBasisId == 1 && TotalWeight <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_TotalWeight").ToString();
            }           
            else if (FreightBasisId == 4 && VolumetricFreightUnitId == 1 && TotalCFT <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_LengthBreadthHeight").ToString();
            }
            else if (FreightBasisId == 4 && VolumetricFreightUnitId == 2 && TotalCBM <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_LengthBreadthHeight").ToString();
            }
            else if ((PaymentTypeId != 5 && !Is_Attached) && Freight <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_FreightAmount").ToString();
                txt_Freight.Focus();
            }
            else if ((PaymentTypeId != 5 && !Is_Attached) && TotalGCAmount <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_TotalGCAmount").ToString();
            }
            else if (ChequeAmount > 0 && (Util.String2Int(ChequeNo) <= 0))
            {
                errorMessage = GetLocalResourceObject("Msg_EnterChequeNo").ToString();
                txt_ChequeNo.Focus();
            }
            else if (ChequeAmount > 0 && (ChequeNo == string.Empty || ChequeNo.Length < 5))
            {
                errorMessage = GetLocalResourceObject("Msg_ChequeNo").ToString();
                txt_ChequeNo.Focus();
            }
            else if (ChequeAmount > 0 && (BankName.Trim() == string.Empty || BankName == "0"))
            {
                errorMessage = GetLocalResourceObject("Msg_BankName").ToString();
                txt_BankName.Focus();
            }
            else if (ChequeAmount > 0 && ChequeDate < Valid_Cheque_Start_Date)
            {
                errorMessage = GetLocalResourceObject("Msg_ValidChequeStartDate").ToString()
                                + Valid_Cheque_Start_Days + " Days Old From Booking Date" 
                                + " ( " + Valid_Cheque_Start_Date.ToString("dd MMM yyyy") + " ) ";
            }
            else if (ChequeAmount > 0 && ChequeDate > Valid_Cheque_End_Date  )
            {
                errorMessage = GetLocalResourceObject("Msg_ValidChequeEndDate").ToString()
                                + Valid_Cheque_End_Days + " Days From Booking Date" 
                                + " ( " + Valid_Cheque_End_Date.ToString("dd MMM yyyy") + " ) ";
            }
            else if ((PaymentTypeId == 2) && (CashAmount + ChequeAmount != TotalGCAmount)) // paid
            {
                errorMessage = GetLocalResourceObject("Msg_CashChequeGCAmount").ToString();
                txt_CashAmount.Focus();
            }
            else if (PaymentTypeId == 1 && (CashAmount + ChequeAmount != Advance))
            {
                errorMessage = GetLocalResourceObject("Msg_ChequeCashAdvanceAmount").ToString();
                txt_CashAmount.Focus();
            }

            else if (PaymentTypeId == 1 && Advance > SubTotal )
            {
                errorMessage = GetLocalResourceObject("Msg_ValidateAdvanceAmount").ToString();
                txt_Advance.Focus();
            }
            else if (Common.GetMenuItemId() != 200 && ContractId > 0 && Is_ContractApplied == 0 && !Is_Attached)
            {
                errorMessage = GetLocalResourceObject("Msg_ContractualRates").ToString();
            }
            else if (Common.GetMenuItemId() != 200 && ContractId <= 0 && Is_Contract_Required_For_TBB_GC && PaymentTypeId == 3)
            {
                errorMessage = GetLocalResourceObject("Msg_ContractFOrTBB").ToString();
            }
            else if (PaymentTypeId == 3 && Is_MultipleBilling && Session_TotalRatio == 0)
            {
                errorMessage = GetLocalResourceObject("Msg_BillingDetails").ToString();
            }
            else if (PaymentTypeId == 3 && Is_MultipleBilling &&
               (Session_TotalRatio < 100 || Session_TotalRatio > 100))
            {
                errorMessage = GetLocalResourceObject("Msg_InvalidBillingDetails").ToString();
            }
            else
            {
                Is_Valid = true;
            }
        }

        if (PaymentTypeId != 3)
        {
            Session_BillingDetailsGrid.Clear();
        }

        bool Is_Paid_Allowed = false;
        bool Is_To_Pay_Allowed = false;
        bool Is_To_Be_Billed_CR_Allowed = false;
        bool Is_FOC_Allowed = false;
        bool Is_To_Be_Billed_Docket_Allowed = false;

        if (Is_Valid == true)
        {
            if (StateManager.IsValidSession("ContractualClientDetails") && Contractual_ClientId > 0)
            {
                if (Session_ContractualClientDetails.Tables[0].Rows.Count > 0)
                {
                    Is_Valid = false;
                    DataRow objDR = Session_ContractualClientDetails.Tables[0].Rows[0];

                    Is_Paid_Allowed = Convert.ToBoolean(objDR["Is_Paid_Allowed"].ToString());
                    Is_To_Pay_Allowed = Convert.ToBoolean(objDR["Is_To_Pay_Allowed"].ToString());
                    Is_To_Be_Billed_CR_Allowed = Convert.ToBoolean(objDR["Is_To_Be_Billed_CR_Allowed"].ToString());
                    Is_FOC_Allowed = Convert.ToBoolean(objDR["Is_FOC_Allowed"].ToString());
                    Is_To_Be_Billed_Docket_Allowed = Convert.ToBoolean(objDR["Is_To_Be_Billed_Docket_Allowed"].ToString());

                    if (PaymentTypeId == 1 && !Is_To_Pay_Allowed)
                    {
                        errorMessage = GetLocalResourceObject("Msg_IsToPayAllowed").ToString();
                        Is_Valid = false;
                    }
                    else if ((PaymentTypeId == 2 || PaymentTypeId == 4) && !Is_Paid_Allowed)
                    {
                        errorMessage = GetLocalResourceObject("Msg_IsPaidAllowed").ToString();
                        Is_Valid = false;
                    }
                    else if (PaymentTypeId == 3 && !Is_To_Be_Billed_CR_Allowed)
                    {
                        errorMessage = GetLocalResourceObject("Msg_IsToBeBilledCRAllowed").ToString();
                        Is_Valid = false;
                    }
                    else if (PaymentTypeId == 5 && !Is_FOC_Allowed)
                    {
                        errorMessage = GetLocalResourceObject("Msg_IsFOCAllowed").ToString();
                        Is_Valid = false;
                    }
                    else
                    {
                        errorMessage = "";
                        Is_Valid = true;
                    }                   
                }
            }
        }

        if (Is_Valid == true && Is_Validate_Credit_Limit == true  )
        {
            if (PaymentTypeId == 3 )//&& BillingPartyId <= 0 && Is_MultipleBilling == false)
            {
                Is_Valid = false;
                In_Valid_Credit_Limit_Client_Name = "";
                Is_Valid = Validate_Credit_Limit();
                
                if (Is_Valid == false )
                {
                    errorMessage = " Credit Limit Exceed For The Selected Billing Parties ( " + In_Valid_Credit_Limit_Client_Name + " )";
                }
                else
                {
                    Is_Valid = true;
                }
            }
        }

        if (!Is_Valid)
        {
            btn_Save_New.Enabled = true;
            btn_Save_Exit.Enabled = true;
            btn_Save_Print.Enabled = true;
            btn_Save_Repeat.Enabled = true; 
        }
        return Is_Valid;
    }
    public Boolean Validate_Credit_Limit()
    {
        bool Is_Valid_Credit_Limit = false;
        Is_Valid_Credit_Limit = objShortGCPresenter.Validate_Credit_Limit();
        return Is_Valid_Credit_Limit;
    }

    public void Is_Duplicate()
    {
        Is_Duplicate_GC_No = false;
        Is_Duplicate_GC_No = objShortGCPresenter.Is_Duplicate();
    }

    public Boolean Allow_To_Attached()
    {
        bool Is_Allow_To_Attached = false;
        Is_Allow_To_Attached = objShortGCPresenter.Allow_To_Attached();
        return Is_Allow_To_Attached;
    }

    public void Allow_To_ReBook()
    {
        Is_Allow_To_ReBook = false;
        Is_Allow_To_ReBook = objShortGCPresenter.Allow_To_ReBook();
    }
    public void Allow_To_Rectify()
    {
        Is_Allow_To_Rectify = false;
        Is_Allow_To_Rectify = objShortGCPresenter.Allow_To_Rectify();
    }    

    public Boolean  Is_Valid_Insurence_Details()
    {
        bool isValid = false;
        errorMessage = "";

        if ( Session_InsuranceCompany.Trim() == String.Empty)
        {
            //errorMessage = "Please Enter Insurence Company.";
        }
        else if (Session_PolicyNo.Trim() == String.Empty)
        {
            //errorMessage = "Please Enter Insurence Policy No.";
        }       
        else if (Session_PolicyExpDate  <= DateTime.Now)
        {
            //errorMessage = "Policy Expiry Date Should Be Greater Than Current Date.";
        }
        else
        {
            isValid = true;
        }

        isValid = true ;
        return isValid;         
    }

    private void Calculate_Billing_Total_Ratio()
    {
        Session_TotalRatio = 0;

        if (StateManager.IsValidSession("BillingDetailsGrid"))
        {
            if (Session_BillingDetailsGrid.Rows.Count > 0)
            {
                Session_TotalRatio = Util.String2Decimal(Session_BillingDetailsGrid.Compute("sum(bill_Ratio)", "").ToString());
            }
        }
        else
        {
            Session_TotalRatio = 0;
        }
    }
    #endregion
    
    #region dg_Invoice Commands

    private string sortCriteria_Invoice_R
    {
        get{ return Convert.ToString(ViewState["sortCriteria_Invoice_R"]); }
        set{ ViewState["sortCriteria_Invoice_R"] = value; }
    }

    // ' Holds the direction to be sorted.
    private string sortDir_Invoice_R
    {
        get{return Convert.ToString(ViewState["sortDir_Invoice"]); }
        set{ViewState["sortDir_Invoice"] = value;}
    }  

    protected void dg_Invoice_EditCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Invoice.EditItemIndex = e.Item.ItemIndex;
        dg_Invoice.ShowFooter = false;
        BindInvoiceGrid();

        InvoiceGridErrorMessage = "";
        SM_ShortGC.SetFocus(txt_InvoiceNo);
    }

    protected void dg_Invoice_CancelCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Invoice.EditItemIndex = -1;
        dg_Invoice.ShowFooter = true;
        BindInvoiceGrid();
        InvoiceGridErrorMessage = "";
        SM_ShortGC.SetFocus(txt_InvoiceNo);
    }

    private void BindInvoiceGrid()
    {
        Set_Invoice_SrNo();
        dg_Invoice.DataSource = Session_InvoiceGrid;
        dg_Invoice.DataBind();

        Calculate_InvoiceTotal();
        On_PaymentTypeChange();
        Calculate_FOV();
        Calculate_GrandTotal();
    }

    public void Set_Invoice_SrNo()
    {
        int i = 0;

        if (StateManager.IsValidSession("InvoiceGrid"))
        {
            for (i = 0; i <= Session_InvoiceGrid.Rows.Count - 1; i++)
            {
                Session_InvoiceGrid.Rows[i]["Sr_No"] = i + 1;
                Session_InvoiceGrid.AcceptChanges();
            }
        }
    }

    public void dg_Invoice_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (StateManager.IsValidSession("InvoiceGrid"))
        {
            dr = Session_InvoiceGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_InvoiceGrid.AcceptChanges();
        }

        dg_Invoice.ShowFooter = true;
        BindInvoiceGrid();
        SM_ShortGC.SetFocus(txt_InvoiceNo);
    }

    protected void dg_Invoice_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Invoice_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                BindInvoiceGrid();
                dg_Invoice.EditItemIndex = -1;
                dg_Invoice.ShowFooter = true;
                SM_ShortGC.SetFocus(txt_InvoiceNo);
            }
        }
    }

    private void Insert_Update_Invoice_Dataset(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objdata_Invoice = Session_InvoiceGrid;

        txt_InvoiceNo = (TextBox)(e.Item.FindControl("txt_Invoice_No"));
        txt_Chalan_No = (TextBox)(e.Item.FindControl("txt_Chalan_No"));
        txt_InvoiceAmount = (TextBox)(e.Item.FindControl("txt_Invoice_Amount"));
        txt_BE_BLNo = (TextBox)(e.Item.FindControl("txt_BE_BL_No"));

        if (Allow_To_Add_Update_Invoice())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_InvoiceGrid.NewRow();
                dr["GC_Invoice_ID"] = "0";
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_InvoiceGrid.Rows[e.Item.ItemIndex];
            }

            MultipleCommodityGridErrorMessage = "";

            dr["Sr_No"] = e.Item.ItemIndex;
            dr["Invoice_No"] = txt_InvoiceNo.Text.Trim();
            dr["Chalan_No"] = txt_Chalan_No.Text.Trim(); 
            dr["Invoice_Amount"] = ValueOfTextBox(txt_InvoiceAmount);
            dr["BE_BL_No"] = txt_BE_BLNo.Text;

            if (e.CommandName == "Add")
            {
                objdata_Invoice.Rows.Add(dr);
            }
            Session_InvoiceGrid.AcceptChanges();
        }
        Session_InvoiceGrid = objdata_Invoice;
    }

    protected void dg_Invoice_UpdateCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        Insert_Update_Invoice_Dataset(sender, e);
        if (Allow_To_Save == true)
        {
            dg_Invoice.EditItemIndex = -1;
            dg_Invoice.ShowFooter = true;
            BindInvoiceGrid();
            SM_ShortGC.SetFocus(txt_InvoiceNo);
        }
        SM_ShortGC.SetFocus(txt_InvoiceNo);
    }

    public Boolean Allow_To_Add_Update_Invoice()
    {
        Allow_To_Save = false;
        InvoiceGridErrorMessage = "";

        if (txt_InvoiceNo.Text.Trim() == string.Empty && txt_Chalan_No.Text == string.Empty)
        {
            InvoiceGridErrorMessage = GetLocalResourceObject("Msg_Invoice").ToString();
            SM_ShortGC.SetFocus(txt_InvoiceNo);
        }
        else if(Convert.ToDecimal(txt_InvoiceAmount.Text.Trim() == string.Empty ? "0" : txt_InvoiceAmount.Text.Trim()) <= 0 
                    && Is_Invoice_Amount_Required)
        {
            InvoiceGridErrorMessage = GetLocalResourceObject("Msg_InvoiceAmount").ToString();
            SM_ShortGC.SetFocus(txt_InvoiceAmount);
        }
        else
        {
            Allow_To_Save = true;
        }
        return Allow_To_Save;
    }

    protected void dg_Invoice_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            txt_InvoiceNo = (TextBox)(e.Item.FindControl("txt_Invoice_No"));
            txt_Chalan_No = (TextBox)(e.Item.FindControl("txt_Chalan_No"));
            txt_InvoiceAmount = (TextBox)(e.Item.FindControl("txt_Invoice_Amount"));
            txt_BE_BLNo = (TextBox)(e.Item.FindControl("txt_BE_BL_No"));
        }

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            dr = Session_InvoiceGrid.Rows[e.Item.ItemIndex];
            txt_InvoiceNo.Text = dr["Invoice_No"].ToString();
            txt_Chalan_No.Text = dr["Chalan_No"].ToString();
            txt_InvoiceAmount.Text = dr["Invoice_Amount"].ToString();
            txt_BE_BLNo.Text = dr["BE_BL_No"].ToString();
        }
    }

    #endregion


    #region dg_Commodity Commands

    private string sortCriteria_R
    {
        get
        {
            return Convert.ToString(ViewState["sortCriteria_R"]);
        }
        set
        {
            ViewState["sortCriteria_R"] = value;
        }
    }

    // ' Holds the direction to be sorted.
    private string sortDir_Commodity_R
    {
        get
        {
            return Convert.ToString(ViewState["sortDir_Commodity"]);
        }
        set
        {
            ViewState["sortDir_Commodity"] = value;
        }
    }

    protected void dg_Commodity_SortCommand(object sender, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
    {
    }     

    protected void dg_Commodity_EditCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;

        dg_CommodityNandwana.EditItemIndex = e.Item.ItemIndex;
        dg_CommodityNandwana.ShowFooter = false;

        BindCommodityGrid();
        MultipleCommodityGridErrorMessage = "";              

        if (ClientCode.ToLower() == "nandwana")
            SM_ShortGC.SetFocus(ddl_Commodity);
        else
            SM_ShortGC.SetFocus(txt_Articles);
    }

    protected void dg_Commodity_CancelCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;

        dg_CommodityNandwana.EditItemIndex = -1;
        dg_CommodityNandwana.ShowFooter = true;

        BindCommodityGrid();
        MultipleCommodityGridErrorMessage = "";

        if (ClientCode.ToLower() == "nandwana")
            SM_ShortGC.SetFocus(ddl_Commodity);
        else
            SM_ShortGC.SetFocus(txt_Articles);
    }

    private void BindCommodityGrid()
    {
        decimal Temp_ChargeWeight;

        Temp_ChargeWeight = ChargeWeight;

        Set_Commodity_SrNo();
        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();

        dg_CommodityNandwana.DataSource = Session_MultipleCommodityGrid;
        dg_CommodityNandwana.DataBind();
      
        if (IsPostBack )
        {
            objShortGCPresenter.Get_Service_Tax_Applicable_For_Commodity();
            Calculate_MultipleCommodityTotal();
        }

        ActualWeight = TotalWeight;
        Calculate_ChargeWeight();

        if (Temp_ChargeWeight != ChargeWeight && Contract_UnitOfFreightId == 5 && ContractId > 0) // 5 = contract for weight
        {
            Get_FilterExpression();
        }

        if (Old_FirstCommodityId != FirstCommodityId && Contract_FreightSubUnitId == 2 && ContractId > 0) // for Commodity
        {
            Old_FirstCommodityId = FirstCommodityId;
            Get_FilterExpression();
        }
        else if (Old_FirstItemId != FirstItemId && Contract_FreightSubUnitId == 3 && ContractId > 0) // for Item
        {
            Old_FirstItemId = FirstItemId;
            Get_FilterExpression();
        }
        else if (Old_FirstPackingTypeId != FirstPackingTypeId && Contract_FreightSubUnitId == 4 && ContractId > 0) // for Article Type
        {
            Old_FirstPackingTypeId = FirstPackingTypeId;
            Get_FilterExpression();
        }

        if (IsPostBack)
        {
            On_PaymentTypeChange();
            Calculate_Freight();
            Calculate_LocalCharge();
            Calculate_LoadingCharge();
            Calculate_FOV();
            Calculate_DDODA_Charge();
            Calculate_GrandTotal();
        }
        upd_tbl_Charges.Update();
    }

    private void BindCommodityGrid_Attached()
    {
        decimal Temp_ChargeWeight;

        Temp_ChargeWeight = ChargeWeight;

        Set_Commodity_SrNo();
        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();

        dg_CommodityNandwana.DataSource = Session_MultipleCommodityGrid;
        dg_CommodityNandwana.DataBind();
             
        if (IsPostBack)
        {
            objShortGCPresenter.Get_Service_Tax_Applicable_For_Commodity();
        }

        ActualWeight = TotalWeight;

        Calculate_ChargeWeight();

        if (Temp_ChargeWeight != ChargeWeight && Contract_UnitOfFreightId == 5 && ContractId > 0) // 5 = contract for weight
        {
            Get_FilterExpression();
        }

        if (Old_FirstCommodityId != FirstCommodityId && Contract_FreightSubUnitId == 2 && ContractId > 0) // for Commodity
        {
            Old_FirstCommodityId = FirstCommodityId;
            Get_FilterExpression();
        }
        else if (Old_FirstItemId != FirstItemId && Contract_FreightSubUnitId == 3 && ContractId > 0) // for Item
        {
            Old_FirstItemId = FirstItemId;
            Get_FilterExpression();
        }
        else if (Old_FirstPackingTypeId != FirstPackingTypeId && Contract_FreightSubUnitId == 4 && ContractId > 0) // for Article Type
        {
            Old_FirstPackingTypeId = FirstPackingTypeId;
            Get_FilterExpression();
        } 

        upd_tbl_Charges.Update();
    }

    public void Set_Commodity_SrNo()
    {
        int i = 0;

        if (StateManager.IsValidSession("MultipleCommodityGrid"))
        {
            for (i = 0; i <= Session_MultipleCommodityGrid.Rows.Count - 1; i++)
            {
                Session_MultipleCommodityGrid.Rows[i]["Sr_No"] = i + 1;
                Session_MultipleCommodityGrid.AcceptChanges();
            }
        }
    }

    public void dg_Commodity_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (StateManager.IsValidSession("MultipleCommodityGrid"))
        {
            dr = Session_MultipleCommodityGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_MultipleCommodityGrid.AcceptChanges();
        }

        dg_Commodity.ShowFooter = true;
        dg_CommodityNandwana.ShowFooter = true;

        BindCommodityGrid();

        if (ClientCode.ToLower() == "nandwana")
            SM_ShortGC.SetFocus(ddl_Commodity);
        else
            SM_ShortGC.SetFocus(txt_Articles);
    }     

    protected void dg_Commodity_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Commodity_Dataset(source, e);

            if (Allow_To_Save == true)
            {
                Session_MultipleCommodityGrid.AcceptChanges();

                BindCommodityGrid();
                dg_Commodity.EditItemIndex = -1;
                dg_Commodity.ShowFooter = true;

                dg_CommodityNandwana.EditItemIndex = -1;
                dg_CommodityNandwana.ShowFooter = true;


                if (ClientCode.ToLower() == "nandwana")
                    SM_ShortGC.SetFocus(ddl_Commodity);
                else
                    SM_ShortGC.SetFocus(txt_Articles);

            }
            else
            {
                Session_MultipleCommodityGrid.RejectChanges();
            }
        }
    }
     
    private void Insert_Update_Commodity_Dataset(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_Commodity = (DropDownList)(e.Item.FindControl("ddl_Commodity"));
        ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
        ddl_Packing_Type = (DropDownList)(e.Item.FindControl("ddl_Packing_Type"));

        txt_Articles = (TextBox)(e.Item.FindControl("txt_Articles"));
        txt_Weight = (TextBox)(e.Item.FindControl("txt_Weight"));
        txt_Width = (TextBox)(e.Item.FindControl("txt_Width"));
        txt_Height = (TextBox)(e.Item.FindControl("txt_Height"));
        txt_Length = (TextBox)(e.Item.FindControl("txt_Length"));
        txt_Remark = (TextBox)(e.Item.FindControl("txt_Remark"));
        txt_Commodity_SrNo = (TextBox)(e.Item.FindControl("txt_Commodity_SrNo"));

        if (Allow_To_Add_Update_Commodity(e.Item.ItemIndex + 1))
        {
            if (e.CommandName == "Add")
            {
                dr = Session_MultipleCommodityGrid.NewRow();
                dr["GC_Commodity_ID"] = "0";
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_MultipleCommodityGrid.Rows[e.Item.ItemIndex];
            }

            MultipleCommodityGridErrorMessage = "";

            dr["Sr_No"] = e.Item.ItemIndex;//'"Sr_No";

            if (ddl_Commodity.Items.Count <= 0)
            {
                dr["Commodity_Id"] = "0";
                dr["Commodity_Name"] = "";
            }
            else
            {
                dr["Commodity_Id"] = ddl_Commodity.SelectedValue;
                dr["Commodity_Name"] = ddl_Commodity.SelectedItem.Text.Trim();
            }

            if (ddl_Item.Items.Count <= 0)
            {
                dr["Item_Id"] = "0";
                dr["Item_Name"] = "";
            }
            else
            {
                dr["Item_Id"] = ddl_Item.SelectedValue;
                dr["Item_Name"] = ddl_Item.SelectedItem.Text.Trim();
            }

            if (ddl_Packing_Type.Items.Count <= 0)
            {
                dr["Packing_Id"] = "0";
                dr["Packing_Type"] = "";
            }
            else
            {
                dr["Packing_Id"] = ddl_Packing_Type.SelectedValue;
                dr["Packing_Type"] = ddl_Packing_Type.SelectedItem.Text.Trim();
            }

            dr["Articles"] = txt_Articles.Text.Trim() == string.Empty ? "0" : txt_Articles.Text.Trim();
            dr["Weight"] = txt_Weight.Text.Trim() == string.Empty ? "0" : txt_Weight.Text.Trim();
            dr["Width"] = txt_Width.Text.Trim() == string.Empty ? "0" : txt_Width.Text.Trim();
            dr["Length"] = txt_Length.Text.Trim() == string.Empty ? "0" : txt_Length.Text.Trim();
            dr["Height"] = txt_Height.Text.Trim() == string.Empty ? "0" : txt_Height.Text.Trim();
            dr["Remark"] = txt_Remark.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_MultipleCommodityGrid.Rows.Add(dr);
            }
        }
    }

    protected void dg_Commodity_UpdateCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        Insert_Update_Commodity_Dataset(sender, e);

        if (Allow_To_Save == true)
        {
            Session_MultipleCommodityGrid.AcceptChanges();
            dg_Commodity.EditItemIndex = -1;
            dg_Commodity.ShowFooter = true;

            dg_CommodityNandwana.EditItemIndex = -1;
            dg_CommodityNandwana.ShowFooter = true;
                        
            BindCommodityGrid();

            if (ClientCode.ToLower() == "nandwana")
                SM_ShortGC.SetFocus(ddl_Commodity);
            else
                SM_ShortGC.SetFocus(txt_Articles);
        }
        else
        {
            Session_MultipleCommodityGrid.RejectChanges();
        }

        if (ClientCode.ToLower() == "nandwana")
            SM_ShortGC.SetFocus(ddl_Commodity);
        else
            SM_ShortGC.SetFocus(txt_Articles);
    }
     
    public Boolean Allow_To_Add_Update_Commodity(int Sr_No)
    {
        Allow_To_Save = false;
        MultipleCommodityGridErrorMessage = "";

        if (Util.String2Int( ddl_Commodity.SelectedValue)  <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_Commodity").ToString();            
        }
        else if (Util.String2Int( ddl_Item.SelectedValue) <= 0 && Is_Item_Required )
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityItem").ToString();
        }
        else if (Util.String2Int(ddl_Packing_Type.SelectedValue) <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityPackingType").ToString();
        }
        else if (Util.String2Decimal(txt_Articles.Text.Trim() == string.Empty ? "0" : txt_Articles.Text.Trim()) <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityArticles").ToString();
        }
        else if (FreightBasisId == 1 && Util.String2Decimal(txt_Weight.Text.Trim() == string.Empty ? "0" : txt_Weight.Text.Trim()) <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityWeight").ToString();
        }
        else if (FreightBasisId == 4 && Util.String2Decimal(txt_Width.Text.Trim() == string.Empty ? "0" : txt_Width.Text.Trim()) <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityWidth").ToString();
        }
        else if (FreightBasisId == 4 && Util.String2Decimal(txt_Length.Text.Trim() == string.Empty ? "0" : txt_Length.Text.Trim()) <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityLength").ToString();
        }
        else if (FreightBasisId == 4 && Util.String2Decimal(txt_Height.Text.Trim() == string.Empty ? "0" : txt_Height.Text.Trim()) <= 0)
        {
            MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityHeight").ToString();
        }
        else
        {
            Allow_To_Save = true;
        }

        DataView dv;

        if (Allow_To_Save)
        {
            Int32 Commodity_Id, Item_Id, Packing_Id;

            if (ddl_Commodity.Items.Count <= 0)
            {
                Commodity_Id = 0;                
            }
            else
            {
                Commodity_Id = Util.String2Int( ddl_Commodity.SelectedValue);                
            }

            if (ddl_Item.Items.Count <= 0)
            {
                Item_Id = 0;                
            }
            else
            {
                Item_Id =  Util.String2Int( ddl_Item.SelectedValue);                
            }

            if (ddl_Packing_Type.Items.Count <= 0)
            {
                Packing_Id  = 0;                
            }
            else
            {
                Packing_Id =  Util.String2Int( ddl_Packing_Type.SelectedValue);                
            }
 
            string FQ = " Commodity_Id = " + Commodity_Id.ToString() + " And Item_Id = " + Item_Id.ToString() + 
                        " And Packing_Id = " + Packing_Id.ToString() + " And Sr_No <> " + Sr_No;

            dv = CommonObj.Get_View_Table(Session_MultipleCommodityGrid, FQ);

            if (dv.Count > 0)
            {
                MultipleCommodityGridErrorMessage = GetLocalResourceObject("Msg_CommodityDuplicateCommodityDetails").ToString();
                Allow_To_Save = false;
            }
        }
        return Allow_To_Save;
    }

    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            ddl_Commodity = (DropDownList)(e.Item.FindControl("ddl_Commodity"));
            ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
            ddl_Packing_Type = (DropDownList)(e.Item.FindControl("ddl_Packing_Type"));
            txt_Articles = (TextBox)(e.Item.FindControl("txt_Articles"));
            txt_Weight = (TextBox)(e.Item.FindControl("txt_Weight"));
            txt_Width = (TextBox)(e.Item.FindControl("txt_Width"));
            txt_Height = (TextBox)(e.Item.FindControl("txt_Height"));
            txt_Length = (TextBox)(e.Item.FindControl("txt_Length"));
            txt_Remark = (TextBox)(e.Item.FindControl("txt_Remark"));
            txt_Commodity_SrNo = (TextBox)(e.Item.FindControl("txt_Commodity_SrNo"));

            BindCommodity = Session_CommodityDdl;

            if (Util.String2Int(hdn_CommodityId.Value) > 0)
            {
                ddl_Commodity.SelectedValue = hdn_CommodityId.Value;
            }

            if (ddl_Commodity.Items.Count > 0)
            {
                objShortGCPresenter.Fill_Item(Util.String2Int(ddl_Commodity.SelectedValue));
                if (Util.String2Int(hdn_ItemId.Value) > 0)
                {
                    ddl_Item.SelectedValue = hdn_ItemId.Value;
                }
            }
            else
            {
                objShortGCPresenter.Fill_Item(0);
            }
            BindItem = Session_ItemDdl;
            BindPackingType = Session_PackingTypeDdl;
            txt_Weight.Text = Default_Commodity_Weight.ToString();
        }

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            dr = Session_MultipleCommodityGrid.Rows[e.Item.ItemIndex];

            ddl_Commodity.SelectedValue = dr["Commodity_ID"].ToString();

            if (ddl_Commodity.Items.Count > 0)
            {
                objShortGCPresenter.Fill_Item(Util.String2Int(ddl_Commodity.SelectedValue));
                BindItem = Session_ItemDdl;
            }
            else
            {
                objShortGCPresenter.Fill_Item(0);
                BindItem = Session_ItemDdl;
            }

            ddl_Item.SelectedValue = dr["Item_ID"].ToString();
            ddl_Packing_Type.SelectedValue = dr["Packing_ID"].ToString();
            txt_Articles.Text = dr["Articles"].ToString();
            txt_Weight.Text = dr["Weight"].ToString();
            txt_Width.Text = dr["Width"].ToString();
            txt_Length.Text = dr["Length"].ToString();
            txt_Height.Text = dr["Height"].ToString();
            txt_Remark.Text = dr["Remark"].ToString();
        }
    }
    #endregion

    #region OtherMethod

    public void Set_Controls_As_GC_Company_Parameter()
    {
        PaymentTypeId = Default_Payment_Type;
        BookingTypeId = Default_Booking_Type;
        UnitOfMeasurementId = Default_Measurment_Unit;
        PickupTypeId = Default_Pickup_Type; 
        ConsignmentTypeId = Default_Consignment_Type;        
        DeliveryTypeId = Default_Delivery_Type;
        FreightBasisId = Default_Freight_Basis;
        GCRiskId = Default_Risk_Type;
        Is_POD = Is_POD_Checked;
        
        if (ClientCode.ToLower() == "nandwana")
            txt_OtherChargesRemark.TextMode = TextBoxMode.SingleLine;
        else
            txt_OtherChargesRemark.TextMode = TextBoxMode.MultiLine;

        System.Text.StringBuilder OtherChargesRemark_Arrributs = new System.Text.StringBuilder();

        String Remark_Arrributs;

        Remark_Arrributs = " return Check_Max_Length_For_Multiline(" + txt_OtherChargesRemark.ClientID +
                           " , " + Util.String2Int(hdn_Remark_Max_Length.Value.Trim()) + " );";

        OtherChargesRemark_Arrributs.Append(Remark_Arrributs);
        OtherChargesRemark_Arrributs.Append(Page.ClientScript.GetPostBackEventReference(txt_OtherChargesRemark, ""));
        OtherChargesRemark_Arrributs.Append(";");
        txt_OtherChargesRemark.Attributes.Add("onkeyPress", OtherChargesRemark_Arrributs.ToString());
    }     

    private void Get_FilterExpression()
    {
        DataSet ds_Temp_ContractCharges = new DataSet();
        DataSet ds_Temp_ContractOtherCharges = new DataSet();

        DataTable dt = new DataTable();
        DataView dv = new DataView();

        String FilterExpression_For_UnitOfFreight = Get_FilterExpression_For_UnitOfFreight();
        String FilterExpression_For_FreightBasis = Get_FilterExpression_For_FreightBasis();
        String FilterExpression_For_SubUnit = Get_FilterExpression_For_SubUnit();

        Contractual_Charges_FilterExpression = " 1 = 1 " + FilterExpression_For_UnitOfFreight + FilterExpression_For_FreightBasis + FilterExpression_For_SubUnit;
        Is_ContractApplied = 0;

        if (StateManager.IsValidSession("ContractDetails"))
        {
            dv = CommonObj.Get_View_Table(Session_DS_ContractDetails.Tables[0], Contractual_Charges_FilterExpression);
            dt = dv.ToTable();
            ds_Temp_ContractCharges.Tables.Add(dt);

            if (ds_Temp_ContractCharges.Tables[0].Rows.Count > 0 && ContractId > 0)
            {
                DataRow objDR = ds_Temp_ContractCharges.Tables[0].Rows[0];

                Contractual_LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                Contractual_LocalCharge_Rate = Util.String2Decimal(objDR["Local_Charges"].ToString());

                Contractual_HamaliCharge = 0; // Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());
                Contractual_BiltiCharges = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                Contractual_DDCharge = Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                                
                if (Is_ToPay_Charge_Require == true)
                    Contractual_ToPayCharges = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                else
                    Contractual_ToPayCharges = 0;
                
                Contractual_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                Contractual_MinimumChargeWeight = 0;// Standard_MinimumChargeWeight;

                Contractual_CFTFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                Contractual_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());
                Contractual_MinimumHamaliPerKg = Standard_HamaliPerKg;
                Contractual_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                Contractual_FOV = Standard_FOV;

                Contractual_ServiceTaxPercent = Standard_ServiceTaxPercent;
                Contractual_BiltiCharges = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                Contractual_DDCharge = Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                Contractual_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                Contractual_HamaliCharge = Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());

                Contractual_MinimumFOV = 0;


                if (Is_ToPay_Charge_Require == true)
                    Contractual_ToPayCharges = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                else
                    Contractual_ToPayCharges = 0;
                                
                Contractual_FreightRate = Util.String2Decimal(objDR["Freight_Rate"].ToString());
                
                Contractual_Octroi_Form_Charge = Util.String2Decimal(objDR["Octroi_Form_Charges"].ToString());
                Contractual_Octroi_Service_Charge = Util.String2Decimal(objDR["Octroi_Service_Charges"].ToString());
                Contractual_GI_Charges = Util.String2Decimal(objDR["GI_Charges"].ToString());
                Contractual_Demurrage_Days = Util.String2Decimal(objDR["Demurrage_Days"].ToString());
                Contractual_Demurrage_Rate = Util.String2Decimal(objDR["Demurrage_Percent"].ToString());
                
                Set_Applicable_Standard_Charges(true);

                Is_ContractApplied = 1;
            }
            else
            {
                if (keyID > 0)
                {
                    objShortGCPresenter.Get_StdandardFreightRate();
                    objShortGCPresenter.Get_BranchRateParameter();
                }
                Set_Applicable_Standard_Charges(false);
                Is_ContractApplied = 0;
            }
        }
        else
        {
            if (keyID > 0)
            {
                objShortGCPresenter.Get_StdandardFreightRate();
                objShortGCPresenter.Get_BranchRateParameter();
            }
            Set_Applicable_Standard_Charges(false);
            Is_ContractApplied = 0;
        }
    }

    private void Set_Applicable_Standard_Charges(Boolean Apply_Contractual_Charges)
    {
        if (Apply_Contractual_Charges)
        {
            Applicable_Standard_LocalCharge = Contractual_LocalCharge;
            Applicable_Standard_LocalCharge_Rate = Contractual_LocalCharge_Rate;
            Applicable_Standard_HamaliCharge = Contractual_HamaliCharge;
            Applicable_Standard_BiltiCharges = Contractual_BiltiCharges;
            Applicable_Standard_DDCharge_Rate = Contractual_DDCharge_Rate;
            Applicable_Standard_DDCharge = Contractual_DDCharge;
            Applicable_Standard_ToPayCharges = Contractual_ToPayCharges;
            Applicable_Standard_Invoice_Rate = Contractual_Invoice_Rate;
            Applicable_Standard_Invoice_Per_How_Many_Rs = Contractual_Invoice_Per_How_Many_Rs;
            Applicable_Standard_FOVRate = Contractual_FOVRate;
            Applicable_Standard_FOVPercentage = Contractual_FOVPercentage;
            Applicable_Standard_MinimumChargeWeight = Contractual_MinimumChargeWeight;
            Applicable_Standard_CFTFactor = Contractual_CFTFactor;
            Applicable_Standard_HamaliPerKg = Contractual_HamaliPerKg;
            Applicable_Standard_MinimumHamaliPerKg = Contractual_MinimumHamaliPerKg;
            Applicable_Standard_FOVPercentage = Contractual_FOVPercentage;
            Applicable_Standard_FOV = 0;// Contractual_MinimumFOV;
            Applicable_Standard_MinimumFOV = Contractual_MinimumFOV;
            Applicable_Standard_ServiceTaxPercent = Contractual_ServiceTaxPercent;
            Applicable_Standard_BiltiCharges = Contractual_BiltiCharges;
            Applicable_Standard_DDCharge = Contractual_DDCharge;
            Applicable_Standard_DDCharge_Rate = Contractual_DDCharge_Rate;
            Applicable_Standard_FOVPercentage = Contractual_FOVPercentage;
            Applicable_Standard_ToPayCharges = Contractual_ToPayCharges;
            Applicable_Standard_FreightRate = Contractual_FreightRate;
            Applicable_Standard_Demurrage_Days = Contractual_Demurrage_Days;
            Applicable_Standard_Demurrage_Rate = Contractual_Demurrage_Rate;
            Applicable_Standard_GI_Charges = Contractual_GI_Charges;
            Applicable_Standard_Octroi_Form_Charge = Contractual_Octroi_Form_Charge;
            Applicable_Standard_Octroi_Service_Charge = Contractual_Octroi_Service_Charge;

            if (Contract_UnitOfFreightId == 1) // for vehicle
            {
                FreightBasisId = 3; // fixed
            }
            else if (Contract_UnitOfFreightId == 2)// for weight
            {
                FreightBasisId = 1; // Weight
            }
            else if (Contract_UnitOfFreightId == 3)// for CFT
            {
                FreightBasisId = 4; // Volumetric for cft
            }
            else if (Contract_UnitOfFreightId == 4)// for Article
            {
                FreightBasisId = 2; // Article
            }
        }
        else
        {
            Applicable_Standard_LocalCharge = Standard_LocalCharge;
            Applicable_Standard_LocalCharge_Rate = Standard_LocalCharge_Rate;
            Applicable_Standard_HamaliCharge = Standard_HamaliCharge;
            Applicable_Standard_BiltiCharges = Standard_BiltiCharges;
            Applicable_Standard_DDCharge = Standard_DDCharge;
            Applicable_Standard_DDCharge_Rate = Standard_DDCharge_Rate;
            Applicable_Standard_ToPayCharges = Standard_ToPayCharges;
            Applicable_Standard_Invoice_Rate = Standard_Invoice_Rate;
            Applicable_Standard_Invoice_Per_How_Many_Rs = Standard_Invoice_Per_How_Many_Rs;
            Applicable_Standard_FOVRate = Standard_FOVRate;
            Applicable_Standard_FOVPercentage = Standard_FOVPercentage;
            Applicable_Standard_MinimumChargeWeight = Standard_MinimumChargeWeight;
            Applicable_Standard_CFTFactor = Standard_CFTFactor;
            Applicable_Standard_HamaliPerKg = Standard_HamaliPerKg;
            Applicable_Standard_MinimumHamaliPerKg = Standard_HamaliPerKg;
            Applicable_Standard_FOVPercentage = Standard_FOVPercentage;
            Applicable_Standard_MinimumFOV = Standard_MinimumFOV;
            Applicable_Standard_ServiceTaxPercent = Standard_ServiceTaxPercent;
            Applicable_Standard_BiltiCharges = Standard_BiltiCharges;
            Applicable_Standard_DDCharge = Standard_DDCharge;
            Applicable_Standard_DDCharge_Rate = Standard_DDCharge_Rate;
            Applicable_Standard_FOVPercentage = Standard_FOVPercentage;
            Applicable_Standard_ToPayCharges = Standard_ToPayCharges;
            Applicable_Standard_FreightRate = Standard_FreightRate;
            Applicable_Standard_Demurrage_Days = Standard_Demurrage_Days;
            Applicable_Standard_Demurrage_Rate = Standard_Demurrage_Rate;
            Applicable_Standard_GI_Charges = Standard_GI_Charges;
            Applicable_Standard_Octroi_Form_Charge = Standard_Octroi_Form_Charge;
            Applicable_Standard_Octroi_Service_Charge = Standard_Octroi_Service_Charge;
        }

        FreightRate = Applicable_Standard_FreightRate;
        LocalCharge = Applicable_Standard_LocalCharge;
        LoadingCharge = Applicable_Standard_HamaliCharge;
        StationaryCharge = Applicable_Standard_BiltiCharges;
        FOVRiskCharge = Applicable_Standard_FOV;
        ToPayCharge = Applicable_Standard_ToPayCharges;
        DDCharge = Applicable_Standard_DDCharge;
    }

    private void Apply_Rate(Boolean Is_Apply_Contractual_Rate, DataSet ds_Temp_Contract_Charges)
    {
        DataRow objDR;

        if (Is_Apply_Contractual_Rate) // For Contractual Rate
        {
            if (ds_Temp_Contract_Charges.Tables[0].Rows.Count > 0)
            {
                objDR = ds_Temp_Contract_Charges.Tables[0].Rows[0];
                FreightRate = Util.String2Decimal(objDR["Freight_Rate"].ToString());
            }
        }
        else // For Standard Rate
        {
            FreightRate = Standard_FreightRate;
        }
    }

    private String Get_FilterExpression_For_UnitOfFreight()
    {
        String FilterExpression_For_UnitOfFreight = "";

        if (Contract_UnitOfFreightId == 1) // for Vehicle Type
        {
            FilterExpression_For_UnitOfFreight = " and Freight_Unit_ID = 1 and Freight_Unit_Item_ID = " + VehicleTypeId;
        }
        else if (Contract_UnitOfFreightId == 2) // For Wt
        {
            FilterExpression_For_UnitOfFreight = " and Freight_Unit_ID = 2 and From_Range <= " + ChargeWeight.ToString() +
                                                 " and To_Range >= " + ChargeWeight.ToString();

        }
        else if (Contract_UnitOfFreightId == 3) // For CFT
        {
            FilterExpression_For_UnitOfFreight = " and Freight_Unit_ID = 3 and From_Range <= " + TotalCFT.ToString() +
                                                 " and To_Range >= " + TotalCFT.ToString();
        }
        else if (Contract_UnitOfFreightId == 4) // For Articles
        {
            FilterExpression_For_UnitOfFreight = " and Freight_Unit_ID = 4 and From_Range <= " + TotalArticles.ToString() +
                                                 " and To_Range >= " + TotalArticles.ToString();
        }
        else if (Contract_UnitOfFreightId == 5) // For for Kilo Meter
        {
            FilterExpression_For_UnitOfFreight = " and Freight_Unit_ID = 5 and From_Range <= " + ChargeWeight.ToString() +
                                                 " and To_Range >= " + ChargeWeight.ToString();
        }
        return FilterExpression_For_UnitOfFreight;
    }

    private String Get_FilterExpression_For_FreightBasis()
    {
        String FilterExpression_For_FreightBasis = "";

        if (Contract_FreightBasisId == 1) // for Location
        {
            FilterExpression_For_FreightBasis = " and Freight_Basis_Id = 1 and From_Id = " + FromLocationId.ToString() +
                                                " and To_Id = " + ToLocationId.ToString();
        }
        else if (Contract_FreightBasisId == 2) // for Kilo Meter
        {
            FilterExpression_For_FreightBasis = " and Freight_Basis_Id = 2  and From_Id <= " + TotalKiloMeter.ToString() +
                                                " and To_Id >= " + TotalKiloMeter.ToString();
        }
        else if (Contract_FreightBasisId == 3) // for Transit Days
        {
            FilterExpression_For_FreightBasis = "  and Freight_Basis_Id = 3 and From_Id <= " + TotalTransitDays.ToString() +
                                                "  and To_Id >= " + TotalTransitDays.ToString();
        }
        return FilterExpression_For_FreightBasis;
    }

    private String Get_FilterExpression_For_SubUnit()
    {
        String FilterExpression_For_SubUnit = "";

        if (Contract_FreightSubUnitId == 1) // for Not Applicable
        {
            FilterExpression_For_SubUnit = " and Freight_Sub_Unit_Item_ID = 0 ";
        }
        else if (Contract_FreightSubUnitId == 2) // for Commodity
        {
            FilterExpression_For_SubUnit = " and Freight_Sub_Unit_Item_ID = " + FirstCommodityId.ToString();
        }
        else if (Contract_FreightSubUnitId == 3) // for Item
        {
            FilterExpression_For_SubUnit = " and Freight_Sub_Unit_Item_ID = " + FirstItemId.ToString();
        }
        else if (Contract_FreightSubUnitId == 4) // for Article Type
        {
            FilterExpression_For_SubUnit = " and Freight_Sub_Unit_Item_ID = " + FirstPackingTypeId.ToString();
        }
        else if (Contract_FreightSubUnitId == 5) // for Vehicle Type
        {
            FilterExpression_For_SubUnit = " and Freight_Sub_Unit_Item_ID = " + VehicleTypeId.ToString();
        }
        return FilterExpression_For_SubUnit;
    } 

    public void Get_No_To_Padd()
    {
        String No_To_Padd = "";
        int i;
        No_For_Padd = "";

        for (i = 0; i < GC_No_Length; i++)
        {
            No_To_Padd = No_To_Padd + "0";
        }

        No_For_Padd = No_To_Padd;
    }

    public void Get_Next_GC_No()
    {
        int local_DocumentSeriesAllocationId;
        int local_Start_No, local_End_No, local_Next_No;

        local_DocumentSeriesAllocationId = 0;
        local_Start_No = 0;
        local_Next_No = 0;
        local_End_No = 0;

        CommonObj.Get_Document_Allocation_Details(ref local_DocumentSeriesAllocationId, ref local_Start_No,
                                                  ref local_End_No, ref local_Next_No, VAId, 0, DocumentId);
        
        DocumentSeriesAllocationId = local_DocumentSeriesAllocationId;
        Start_No = local_Start_No;
        End_No = local_End_No;
        Next_No = local_Next_No;

        if (PrivateMark == string.Empty)
        {
            PrivateMark = local_Next_No.ToString(No_For_Padd); ;
        }

        if (DocumentSeriesAllocationId == 0)
        {
            Common.DisplayErrors(1013);// "Please Allocate Document Series."
        }
    }

    public void Get_Next_MR_No()
    {
        int local_DocumentSeriesAllocationId;
        int local_Start_No, local_End_No, local_Next_No;

        local_DocumentSeriesAllocationId = 0;
        local_Start_No = 0;
        local_Next_No = 0;
        local_End_No = 0;

        CommonObj.Get_Document_Allocation_Details(ref local_DocumentSeriesAllocationId, ref local_Start_No,
                                                  ref local_End_No, ref local_Next_No, 0 , UserManager.getUserParam().MainId , DocumentId);

        if (local_DocumentSeriesAllocationId == 0)
        {
            Common.DisplayErrors(1017);// "Please Allocate Document Series For MR."
        }
    }

    public void Get_Next_Counter_No()
    {
        int local_Document_Next_Counter_No = 0;
        CommonObj.Get_Document_Next_Counter_No(ref local_Document_Next_Counter_No, BookingBranchId, 2); // 2 for GC
        DocumentNextCounterNo = local_Document_Next_Counter_No;
    }

    public void Add_Button_Attributes()
    {
        if (_meniItemID != 194)
        {
            btn_Save_New.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_New, btn_Save_Exit, btn_Save_Print, btn_Save_Repeat));
            btn_Save_Exit.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save_New, btn_Save_Print, btn_Save_Repeat));
            btn_Save_Print.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_New, btn_Save_Exit, btn_Save_Repeat));
            btn_Save_Repeat.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Repeat, btn_Save_Print, btn_Save_New, btn_Save_Exit));
        }
        else
        {
            btn_Save_Exit.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit));
        }
    }

    public void On_Load()
    {
        string scripts = "";
        scripts = "<script type='text/javascript' language='javascript'>On_Load();</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(string), "ss", scripts, false);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        tr_ConsigneeTINNo.Visible = false;
        tr_ConsignorTINNo.Visible = false;

        hdn_Mode.Value = Util.DecryptToString(Request.QueryString["Mode"]);
        hdn_MenuItemId.Value = _meniItemID.ToString();

        if (hdn_MenuItemId.Value == "200" || _meniItemID == 200)
        {
            Is_Opening_GC = true;
        }
        else
        {
            Is_Opening_GC = false;
        }      

        if (_meniItemID == 194)
        {
            Encrepted_Rectification_GC_Id = Request.QueryString["Id"].ToString().Trim();
            Rectification_GC_Id = Util.DecryptToInt(Request.QueryString["Id"]);
        }
        else
        {
            Encrepted_Rectification_GC_Id = "0";
            Rectification_GC_Id = 0;
        }
        if (!IsPostBack)
        {
            Add_Button_Attributes();

            // _meniItemID = 30  for Normal GC
            // _meniItemID = 184 for ReBook GC
            // _meniItemID = 188 for IBA GC
            // _meniItemID = 194 for Rectification GC

            if (keyID <= 0 && _meniItemID == 184)
            {
                if (Request.QueryString["ReBook_GC_No_For_Print"].ToString().Trim() != "")
                {
                    Attached_GC_No_For_Print = Request.QueryString["ReBook_GC_No_For_Print"].ToString().Trim();
                    ReBook_GC_Id = Util.String2Int(Request.QueryString["ReBook_GC_ID"].ToString().Trim());
                    Is_ReBooked = true;
                    Is_Attached = false;
                }
            }
            else if (keyID <= 0 && _meniItemID == 194)
            {
                if (Request.QueryString["Rectification_GC_No_For_Print"].ToString().Trim() != "")
                {
                    Attached_GC_No_For_Print = Request.QueryString["Rectification_GC_No_For_Print"].ToString().Trim();
                    ReBook_GC_Id = Util.String2Int(Request.QueryString["Rectification_GC_ID"].ToString().Trim());
                    Is_ReBooked = true;
                    Is_Attached = false;
                }
            }
        }

        if (ReBook_GC_Id > 0 || Rectification_GC_Id>0) // only for rebook gc and Rectification
        {
            dg_Commodity.Columns[11].Visible = false;
            dg_Commodity.Columns[12].Visible = false;
            dg_CommodityNandwana.Columns[11].Visible = false;
            dg_CommodityNandwana.Columns[12].Visible = false;
        }

        ddl_BookingBranch.DataTextField = "Branch_Name";
        ddl_BookingBranch.DataValueField = "Branch_ID";

        ddl_ArrivedFromBranch.DataTextField = "Branch_Name";
        ddl_ArrivedFromBranch.DataValueField = "Branch_ID";

        ddl_FromLocation.DataTextField = "From_Location_Name";
        ddl_FromLocation.DataValueField = "From_Location_ID";

        ddl_ToLocation.DataTextField = "To_Location_Name";
        ddl_ToLocation.DataValueField = "To_Location_ID";

        ddl_Consignee.DataTextField = "Client_Name";
        ddl_Consignee.DataValueField = "Client_ID";

        ddl_Consignor.DataTextField = "Client_Name";
        ddl_Consignor.DataValueField = "Client_ID";

        ddl_MarketingExecutive.DataTextField = "Emp_Name";
        ddl_MarketingExecutive.DataValueField = "Emp_ID";

        ddl_LoadingSuperVisor.DataTextField = "Emp_Name";
        ddl_LoadingSuperVisor.DataValueField = "Emp_ID";

        ddl_BillingBranch.DataTextField = "Billing_Branch_Name";
        ddl_BillingBranch.DataValueField = "Billing_Branch_Id";

        ddl_BillingParty.DataTextField = "Billing_Client_Name";
        ddl_BillingParty.DataValueField = "Billing_Client_ID";
        
        ddl_ContractualClient.DataTextField = "Contractual_Client_Name";
        ddl_ContractualClient.DataValueField = "Contractual_Client_Id";

        string current_time = DateTime.Now.ToShortTimeString();
        wuc_BookingTime.setFormat("24");
        wuc_BookingTime.setTime(current_time);

        if (wuc_PolicyExpDate.SelectedDate < DateTime.Now)
        {
            PolicyExpDate = DateTime.Now;
        }

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                ConsignorId = 0;
                EncreptedConsignorId = Util.EncryptInteger(ConsignorId);

                ConsigneeId = 0;
                EncreptedConsigneeId = Util.EncryptInteger(ConsigneeId);
            }

            hdn_IsContractApplied.Value = "0";
            Session_DS_ContractDetails = null;
            Is_ODA = false;

            ddl_ContractBranch.Items.Insert(0, new ListItem("Select One", "0"));
            ddl_Contract.Items.Insert(0, new ListItem("Select One", "0"));

            ChequeDate = DateTime.Now;
            Session_InsuranceCompany = "";
            Session_PolicyAmount = 0;
            Session_RiskAmount = 0;
            Session_PolicyNo = "";
            Session_PolicyExpDate = DateTime.Now;
        }

        SetStandardCaption();
        SetLinks_Rights();
    }

    private void SetStandardCaption()
    {
        lbl_GCNo.Text = CompanyManager.getCompanyParam().GcCaption + "  No:";
        lbl_TotalGCAmount.Text = "Total " + CompanyManager.getCompanyParam().GcCaption + " Amount :";
        lbl_GCRisk.Text = CompanyManager.getCompanyParam().GcCaption + " Risk ";
    }

    public void Clear_GC_Details(object sender, EventArgs e)
    {
        DataSet objDS_ContractDetails = new DataSet();

            CustomerRefNo = "";

            if (keyID > 0)
            {
                Is_Attached = false;
                Attached_GC_Id = 0;
                Attached_GC_No_For_Print = "";
            }
            else
            {
                Is_Attached = false;
            }

            if (Is_Attached)
            {
                Attached_GC_Id = 0;
                Attached_GC_No_For_Print = "";
            }
            else
                BookingDate = DateTime.Now;


            CustomerRefNo = "";          
            SetFromLocation("", "0");
            SetToLocation("", "0");
            SetConsingee("", "0");
            SetConsingor("", "0");
            FromLocationId = 0;
            ToLocationId = 0;
            DeliveryBaranchId = 0;
            DeliveryBranchName = "";
            VehicleNo = "";
            STMNo = "";
            FeasibilityRouteSurveyNo = "";
            ConsignorId = 0;
            EncreptedConsignorId = Util.EncryptInteger(ConsignorId);
            ConsignorAddressLine1 = "";
            ConsignorAddressLine2 = "";
            ConsignorCity = "";
            ConsignorPinCode = "";
            ConsignorTelNo = "";
            ConsignorMobileNo = "";
            ConsignorEmail = "";
            ConsignorCSTNo = "";
            ConsignorCityId = 0;
            ConsignorStateId = 0;
            ConsignorCountryId = 0;
            ConsignorStateName = "";
            ConsignorCountryName = "";
            Is_RegularConsignor = 0;
            ConsigneeId = 0;
            EncreptedConsigneeId = Util.EncryptInteger(ConsigneeId);
            Session_ConsigneeName = "";
            ConsigneeAddressLine1 = "";
            ConsigneeAddressLine2 = "";
            Session_ConsigneeAddressLine1 = "";
            Session_ConsigneeAddressLine2 = "";
            ConsigneeCity = "";
            ConsigneePinCode = "";
            ConsigneeTelNo = "";
            ConsigneeMobileNo = "";
            ConsigneeEmail = "";
            ConsigneeCSTNo = "";
            ConsigneeCityId = 0;
            ConsigneeStateId = 0;
            ConsigneeCountryId = 0;
            ConsigneeStateName = "";
            ConsigneeCountryName = "";         
            Is_RegularConsignee = 0;
            SetContractualClient("", "0");
            Contractual_ClientId = 0;
            ddl_ContractualClient_TxtChange(sender, e);
            ddl_ContractBranch_SelectedIndexChanged(sender, e);
            Contract_BranchId = 0;
            ContractId = 0;
            Is_ContractApplied = 0;
            objDS_ContractDetails = null;
            Session_DS_ContractDetails = objDS_ContractDetails;
            InsuranceCompany = "";
            PolicyNo = "";
            PolicyAmount = 0;
            RiskAmount = 0;
            Session_InsuranceCompany = "";
            Session_PolicyAmount = 0;
            Session_RiskAmount = 0;
            Session_PolicyNo = "";
            Session_PolicyExpDate = DateTime.Now;
            //----------------------------------------------------------------------------------------

            TotalArticles = 0;
            TotalLength = 0;
            TotalWidth = 0;
            TotalWeight = 0;
            TotalHeight = 0;

            //----------------------------------------------------------------------------------------
            UnitOfMeasurmentLength = 0;
            UnitOfMeasurmentWidth = 0;
            UnitOfMeasurmentHeight = 0;
            HeightInFeet = 0;
            WidthInFeet = 0;
            LengthInFeet = 0;
            TotalCFT = 0;
            TotalCBM = 0;
            VolumetricToKgFactor = 0;
            ActualWeight = 0;
            Calculate_ChargeWeight();
            FreightRate = 0;
            TotalInvoiceAmount = 0;
            //----------------------------------------------------------------------------------------

            ServiceTaxPayableBy = 3; // transporter Util.String2Int(objDR["Tax_Payable_By"].ToString());

            Is_ServiceTaxApplicableForConsignor = 0;
            Is_ServiceTaxApplicableForConsignee = 0;
            Is_ODA = false;
            Is_OctroiApplicable = false;
            Is_ToPayBookingApplicable = true;
            Is_MultipleBilling = false;

            SetBillingBranch("", "0");
            SetBillingParty("", "0");
            BillingBranchId = 0;
            BillingPartyId = 0;
            BillingRemark = "";
            Freight = 0;
            LocalCharge = 0;
            LoadingCharge = 0;
            StationaryCharge = 0;
            FOVRiskCharge = 0;
            ToPayCharge = 0;
            DDCharge = 0;
            Applicable_Standard_DDCharge_Rate = 0;
            Standard_DDCharge_Rate = 0;
            OtherCharges = 0;
            SubTotal = 0;
            Abatment = 0;
            TaxableAmount = 0;
            ServiceTax = 0;
            ReBookGC_OctroiAmount = 0;
            TotalGCAmount = 0;
            Advance = 0;
            CashAmount = 0;
            ChequeAmount = 0;
            Previous_SubTotal = 0;
            Previous_GrandTotal = 0;
            ChequeNo = "";
            BankName = "";
            SetLoadingSuperVisor("", "0");
            SetLoadingSuperVisor("", "0");
            LoadingSuperVisorId = 0;
            MarketingExecutiveId = 0;

            // -----------------------

            Standard_FreightRate = 0;
            Standard_ServiceTaxPercent = Util.String2Decimal("12.36");
            Standard_BiltiCharges = 0;
            Standard_DDCharge = 0;
            Standard_FOV = 0;
            Standard_FOVPercentage = 0;
            Standard_FreightAmount = 0;
            Standard_HamaliCharge = 0;
            Standard_LocalCharge = 0;
            Standard_LocalCharge_Rate = 0;
            Standard_ServiceTaxAmount = 0;
            Applicable_Standard_ServiceTaxAmount = 0;
            Standard_ToPayCharges = 0;
            Standard_CFTFactor = 0;
            Applicable_Standard_BiltiCharges = Standard_BiltiCharges;
            Applicable_Standard_FOV = FOVRiskCharge;
            Applicable_Standard_FOVPercentage = Standard_FOVPercentage;
            Applicable_Standard_FreightRate = FreightRate;
            Applicable_Standard_HamaliCharge = Standard_HamaliCharge;
            Applicable_Standard_HamaliPerKg = Standard_HamaliPerKg;
            Applicable_Standard_LocalCharge = Standard_LocalCharge;
            Applicable_Standard_LocalCharge_Rate = Standard_LocalCharge_Rate;

            if (ContractId > 0)
            {
                Applicable_Standard_MinimumFOV = 0;
                Applicable_Standard_MinimumChargeWeight = 0;
                Applicable_Standard_MinimumFOV = 0;
            }
            else
            {
                Applicable_Standard_MinimumFOV = Standard_MinimumFOV;
                Applicable_Standard_MinimumChargeWeight = Standard_MinimumChargeWeight;
                Applicable_Standard_MinimumFOV = Standard_MinimumFOV;
            }
            Applicable_Standard_ServiceTaxAmount = Standard_ServiceTaxAmount;
            Applicable_Standard_ServiceTaxPercent = Standard_ServiceTaxPercent;
            Applicable_Standard_ToPayCharges = Standard_ToPayCharges;
            Applicable_Standard_DDCharge = Standard_DDCharge;
            Applicable_Standard_DDCharge_Rate = Standard_DDCharge_Rate;
            Applicable_Standard_CFTFactor = Standard_CFTFactor;
            OtherChargesRemark = "";
            InstructionRemark = "";
            Enclosures = "";
            Is_POD = false;

        Session_MultipleCommodityGrid.Rows.Clear();
        Session_MultipleCommodityGrid.AcceptChanges();

        Session_InvoiceGrid.Rows.Clear();
        Session_InvoiceGrid.AcceptChanges();

        Bind_dg_Invoice = Session_InvoiceGrid;

        Session_OtherChargesGrid.Rows.Clear();
        Session_OtherChargesGrid.AcceptChanges();

        Session_Main_BillingDetailsGrid.Rows.Clear();
        Session_Main_BillingDetailsGrid.AcceptChanges();
        Session_TotalRatio = 0;

        Session_ContainerTypeId = 0;
        Session_ContainerNoPart1 = "";
        Session_ContainerNoPart2 = "";
        Session_SealNo = "";
        Session_ReturnToYardName = "";
        Session_ReturnToYardId = 0;
        Session_NFormNo = "";
    }

    private void Clear_Other_Charges()
    {
        int i = 0;

        if (StateManager.IsValidSession("GCOtherChargeHead"))
        {
            for (i = 0; i <= Session_GCOtherChargeHead.Rows.Count - 1; i++)
            {
                Session_GCOtherChargeHead.Rows[i]["Amount"] = 0;
                Session_GCOtherChargeHead.AcceptChanges();
            }
        }
    }

    private void Read_ContractCharges()
    {
        Contract_UnitOfFreightId = 0;
        Contract_FreightBasisId = 0;
        Contract_FreightSubUnitId = 0;

        ds_ContractDetails = objShortGCPresenter.Get_ContractDetails("");

        Session_DS_ContractDetails = ds_ContractDetails;

        if (ds_ContractDetails.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds_ContractDetails.Tables[0].Rows[0];

            Contract_UnitOfFreightId = Util.String2Int(objDR["Freight_Unit_Id"].ToString());
            Contract_FreightBasisId = Util.String2Int(objDR["Freight_Basis_Id"].ToString());
            Contract_FreightSubUnitId = Util.String2Int(objDR["Freight_Sub_Unit_ID"].ToString());
       }
    }

    private void Calculate_MultipleCommodityTotal()
    {
        TotalArticles = 0;
        TotalWeight = 0;
        TotalWidth = 0;
        TotalLength = 0;
        TotalHeight = 0;

        UnitOfMeasurmentLength = 0;
        UnitOfMeasurmentWidth = 0;
        UnitOfMeasurmentHeight = 0;
        //----------------------------------------------------------------------------------------
        HeightInFeet = 0;
        WidthInFeet = 0;
        LengthInFeet = 0;
        //----------------------------------------------------------------------------------------

        FirstCommodityId = 0;
        FirstItemId = 0;
        FirstPackingTypeId = 0;

        if (StateManager.IsValidSession("MultipleCommodityGrid"))
        {
            if (Session_MultipleCommodityGrid.Rows.Count > 0)
            {
                TotalArticles = Util.String2Int(Session_MultipleCommodityGrid.Compute("sum(Articles)", "").ToString());
                TotalHeight = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Height)", "").ToString());
                TotalLength = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Length)", "").ToString());
                TotalWeight = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Weight)", "").ToString());
                TotalWidth = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Width)", "").ToString());

                FirstCommodityId = Util.String2Int(Session_MultipleCommodityGrid.Rows[0]["Commodity_id"].ToString());
                FirstItemId = Util.String2Int(Session_MultipleCommodityGrid.Rows[0]["Item_Id"].ToString());
                FirstPackingTypeId = Util.String2Int(Session_MultipleCommodityGrid.Rows[0]["Packing_Id"].ToString());
            }
            else
            {
                TotalArticles = 0;
                TotalWeight = 0;
                TotalWidth = 0;
                TotalLength = 0;
                TotalHeight = 0;

                FirstCommodityId = 0;
                FirstItemId = 0;
                FirstPackingTypeId = 0;
            }
        }

        UnitOfMeasurmentLength = TotalLength;
        UnitOfMeasurmentHeight = TotalHeight;
        UnitOfMeasurmentWidth = TotalWidth;

        Convert_InTo_Feet();

        Calculate_ChargeWeight();

        if (ContractId > 0)
        {
            Get_FilterExpression();
        }

        Calculate_Freight();
        Calculate_LocalCharge();

        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();

        Calculate_GrandTotal();

        upd_tbl_Charges.Update();
    }

    private void Calculate_MultipleCommodityTotalEdit()
    {
        TotalArticles = 0;
        TotalHeight = 0;
        TotalLength = 0;
        TotalWeight = 0;
        TotalWidth = 0;

        FirstCommodityId = 0;
        FirstItemId = 0;
        FirstPackingTypeId = 0;

        if (StateManager.IsValidSession("MultipleCommodityGrid"))
        {
            if (Session_MultipleCommodityGrid.Rows.Count > 0)
            {
                TotalArticles = Util.String2Int(Session_MultipleCommodityGrid.Compute("sum(Articles)", "").ToString());
                TotalHeight = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Height)", "").ToString());
                TotalLength = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Length)", "").ToString());
                TotalWeight = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Weight)", "").ToString());
                TotalWidth = Util.String2Decimal(Session_MultipleCommodityGrid.Compute("sum(Width)", "").ToString());

                FirstCommodityId = Util.String2Int(Session_MultipleCommodityGrid.Rows[0]["Commodity_id"].ToString());
                FirstItemId = Util.String2Int(Session_MultipleCommodityGrid.Rows[0]["Item_Id"].ToString());
                FirstPackingTypeId = Util.String2Int(Session_MultipleCommodityGrid.Rows[0]["Packing_Id"].ToString());
            }
            else
            {
                TotalArticles = 0;
                TotalHeight = 0;
                TotalLength = 0;
                TotalWeight = 0;
                TotalWidth = 0;

                FirstCommodityId = 0;
                FirstItemId = 0;
                FirstPackingTypeId = 0;
            }
        }
    }

    private void Calculate_InvoiceTotal()
    {
        Decimal TotalInvoice = 0;

        if (StateManager.IsValidSession("InvoiceGrid"))
        {
            if (Session_InvoiceGrid.Rows.Count > 0)
            {
                TotalInvoice = Util.String2Decimal(Session_InvoiceGrid.Compute("sum(Invoice_Amount)", "").ToString());
            }
            else
            {
                TotalInvoice = 0;
            }
        }
        TotalInvoiceAmount = TotalInvoice;
    }

    public void Calculate_FOV()
    {
        Decimal FOV, System_Invoice, Invoice_Difference;
        int Mod;
       
        FOV = 0;

        if ((GCRiskId == 2 || GCRiskId == 3) || (GCRiskId == 1 && Is_Insured == false))    // for carrier risk (2) and none (3)
        {
            if (Is_FOV_Calculated_As_Per_Standard)
            {
                FOV = TotalInvoiceAmount * Applicable_Standard_FOVPercentage / 100;
                Standard_FOV = TotalInvoiceAmount * Standard_FOVPercentage / 100;
            }
            else
            {
                System_Invoice = ChargeWeight * Applicable_Standard_Invoice_Rate  ;

                Invoice_Difference = TotalInvoiceAmount - System_Invoice;
                Invoice_Difference = Math.Round(Invoice_Difference, 0);

                Mod = Convert.ToInt32(Invoice_Difference) % 1000;

                if (TotalInvoiceAmount > System_Invoice)
                {
                    if (Mod > 0)
                    {
                        int Difference = 1000 - Mod;
                        Invoice_Difference = Invoice_Difference + Difference;                        
                    }
                    if (Applicable_Standard_Invoice_Per_How_Many_Rs <= 0 )
                    {
                        FOV = 0;
                        Standard_FOV = 0;
                    }
                    else
                    {
                        FOV = Invoice_Difference * Applicable_Standard_FOVRate / Applicable_Standard_Invoice_Per_How_Many_Rs;
                        Standard_FOV = Invoice_Difference * Standard_FOVRate / Standard_Invoice_Per_How_Many_Rs;
                    }
                }
                else
                {
                    FOV = 0;
                    Standard_FOV = 0;
                }
            }

            if (FOV < Applicable_Standard_MinimumFOV)// Util.String2Decimal(hdn_Minimum_FOV.Value))
            {
                FOV = Applicable_Standard_MinimumFOV;// Util.String2Decimal(hdn_Minimum_FOV.Value);
            }
        }
        else
        {
            FOV = 0;
        }

        FOVRiskCharge = FOV;

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            FOVRiskCharge = 0;
            Standard_FOV = 0;
        }
    }

    public void Calculate_LoadingCharge()
    {
        Decimal Temp_LoadingCharge =0;//= Applicable_Standard_HamaliPerKg * ChargeWeight /CompanyParameter_Standard_FreightRatePer;
        
        if ( FreightBasisId == 2 ) // for Articles
        {
            Temp_LoadingCharge = Applicable_Standard_HamaliPerArticles  * TotalArticles ;
        }
        else
        {
            Temp_LoadingCharge = Applicable_Standard_HamaliPerKg * ChargeWeight /
                                 CompanyParameter_Standard_FreightRatePer;
        }
        
        if (Temp_LoadingCharge < Applicable_Standard_HamaliCharge)// Standard_Hamali_Charge  )
            Temp_LoadingCharge = Applicable_Standard_HamaliCharge;

        LoadingCharge = Temp_LoadingCharge;

        if (PaymentTypeId == 5 || Is_Attached == true )
        {
            LoadingCharge = 0;
            Applicable_Standard_HamaliCharge = 0;
        }
    }

    public void Calculate_Standard_FOV()
    {
        Decimal System_Invoice, Invoice_Difference;
        int Mod;
        
        if ((GCRiskId == 2 || GCRiskId == 3) || (GCRiskId == 1 && Is_Insured == false))    // for carrier risk (2) and none (3)
        {
            if (Is_FOV_Calculated_As_Per_Standard)
            {
                Applicable_Standard_FOV = TotalInvoiceAmount * Standard_FOVPercentage / 100;
            }
            else
            {
                System_Invoice = 0;
                System_Invoice = ChargeWeight * Standard_Invoice_Rate;
                
                Invoice_Difference = TotalInvoiceAmount - System_Invoice;

                Invoice_Difference = Math.Round(Invoice_Difference, 0);

                Mod = Convert.ToInt32(Invoice_Difference) % 1000;

                if (TotalInvoiceAmount > System_Invoice)
                {
                    if (Mod > 0)
                    {
                        int Difference = 1000 - Mod;
                        Invoice_Difference = Invoice_Difference + Difference;
                    }

                    if (Standard_Invoice_Per_How_Many_Rs <= 0)
                    {
                        Applicable_Standard_FOV = 0;
                    }
                    else
                    {
                        Applicable_Standard_FOV = Invoice_Difference * Standard_FOVRate / Standard_Invoice_Per_How_Many_Rs;
                    }
                }
                else
                {
                    Applicable_Standard_FOV = 0;                    
                }
            }

        }
        else
        {
            Applicable_Standard_FOV = 0;
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            Applicable_Standard_FOV = 0;
        }
    }

    public void Calculate_Standard_LoadingCharge()
    {
        if (FreightBasisId == 2)
        {
            Applicable_Standard_HamaliCharge = Applicable_Standard_HamaliPerArticles * TotalArticles;
        }
        else
        {
            Applicable_Standard_HamaliCharge = Applicable_Standard_HamaliPerKg * ChargeWeight /
                                        CompanyParameter_Standard_FreightRatePer;
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            Applicable_Standard_HamaliCharge = 0;
        }
    }

    public void Calculate_Standard_Charges()
    {
        Calculate_Standard_Freight();
        Calculate_Standard_LocalCharge();
        Calculate_Standard_LoadingCharge();
        Calculate_Standard_FOV();
        Calculate_Standard_DDODACharge();
    }

    public void Calculate_Standard_Freight()
    {
        if (FreightBasisId == 4 && VolumetricFreightUnitId == 1)
        {
            Applicable_Standard_FreightAmount = TotalCFT * Applicable_Standard_FreightRate;
            Standard_FreightAmount = TotalCFT * Standard_FreightRate;
        }
        else if (FreightBasisId == 4 && VolumetricFreightUnitId == 2)
        {
            Applicable_Standard_FreightAmount = TotalCBM * Applicable_Standard_FreightRate;

            Standard_FreightAmount = TotalCBM * Standard_FreightRate;
        }
        else if (FreightBasisId == 4 && VolumetricFreightUnitId == 3)
        {
            Applicable_Standard_FreightAmount = ChargeWeight / CompanyParameter_Standard_FreightRatePer
                                                 * Applicable_Standard_FreightRate;

            Standard_FreightAmount = ChargeWeight / CompanyParameter_Standard_FreightRatePer
                                                 * Standard_FreightRate;
        }
        else if (FreightBasisId == 3) // for Fixed
        {
            Applicable_Standard_FreightAmount = Applicable_Standard_FreightRate;
            Standard_FreightAmount = Standard_FreightRate;
        }
        else if (FreightBasisId == 2) // for Article
        {
            Applicable_Standard_FreightAmount = TotalArticles * Applicable_Standard_FreightRate;
            Standard_FreightAmount = TotalArticles * Standard_FreightRate;
        }
        else
        {
            Applicable_Standard_FreightAmount = (ChargeWeight / CompanyParameter_Standard_FreightRatePer) *
                                                Applicable_Standard_FreightRate;

            Standard_FreightAmount = (ChargeWeight / CompanyParameter_Standard_FreightRatePer) *
                                                Standard_FreightRate;
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            Applicable_Standard_FreightAmount = 0;
            Standard_FreightAmount = 0;
        }
    }

    public void Calculate_Standard_DDODACharge()
    {
        Standard_DDCharge = 0;

        if (Is_ODA == true && DeliveryTypeId == 2) // oda and door
        {
            if (ChargeWeight <= 500)
            {
                Standard_DDCharge = ODAChargesUpTo500Kg;
            }
            else if (ChargeWeight > 500)
            {
                Standard_DDCharge = ODAChargesAbove500Kg;
            }
        }
        else
        {
            if (DeliveryTypeId == 2)
            {
                Standard_DDCharge = ChargeWeight / CompanyParameter_Standard_FreightRatePer * Applicable_Standard_DDCharge_Rate;
            }
            else
            {
                Standard_DDCharge = 0;
            }
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            Standard_DDCharge = 0;
            Applicable_Standard_DDCharge = 0;
        }
    }
    public void Calculate_DDODA_Charge()
    {
        DDCharge = 0;

        if (Is_ODA == true && DeliveryTypeId == 2) // oda and door
        {
            if (ChargeWeight <= 500)
            {
                DDCharge = ODAChargesUpTo500Kg;
            }
            else if (ChargeWeight > 500)
            {
                DDCharge = ODAChargesAbove500Kg;
            }
        }
        else
        {
            if (DeliveryTypeId == 2)
            {
                DDCharge = ChargeWeight / CompanyParameter_Standard_FreightRatePer * Applicable_Standard_DDCharge_Rate;
            }
            else
            {
                DDCharge = 0;
            }
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            DDCharge = 0;
            Applicable_Standard_DDCharge = 0;
        }
    }

    public void Calculate_LocalCharge()
    {
        Decimal Temp_LocalCharge = 0;

        Temp_LocalCharge = ChargeWeight / CompanyParameter_Standard_FreightRatePer * Applicable_Standard_LocalCharge;

        if (LocalCharge < Temp_LocalCharge && Is_ContractApplied == 1)
        {
            LocalCharge = Temp_LocalCharge;
        }

        if (Is_ContractApplied == 0)
        {
            Temp_LocalCharge = ChargeWeight / CompanyParameter_Standard_FreightRatePer * Applicable_Standard_LocalCharge_Rate;
            LocalCharge = Temp_LocalCharge;
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            LocalCharge = 0;
            Applicable_Standard_LocalCharge = 0;
        }
    }
    public void Calculate_Standard_LocalCharge()
    {
        Standard_LocalCharge = ChargeWeight / CompanyParameter_Standard_FreightRatePer * Standard_LocalCharge;

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            Standard_LocalCharge = 0;
            Applicable_Standard_LocalCharge = 0;
        }
    }
    public void Convert_InTo_Feet()
    {
        String Convirsion_Factor = "0";

        HeightInFeet = UnitOfMeasurmentHeight * Util.String2Decimal(Convirsion_Factor);
        WidthInFeet = UnitOfMeasurmentWidth * Util.String2Decimal(Convirsion_Factor);
        LengthInFeet = UnitOfMeasurmentLength * Util.String2Decimal(Convirsion_Factor);

        if (ddl_UnitOfMeasurment.Items.Count > 0)
        {
            if (UnitOfMeasurementId == 1)
            {
                Convirsion_Factor = "0.083";
            }
            else if (UnitOfMeasurementId == 2)
            {
                Convirsion_Factor = "1";
            }
            else if (UnitOfMeasurementId == 3)
            {
                Convirsion_Factor = "3.29";
            }
            else if (UnitOfMeasurementId == 4)
            {
                Convirsion_Factor = "0.032";
            }

            HeightInFeet = UnitOfMeasurmentHeight * Util.String2Decimal(Convirsion_Factor);
            WidthInFeet = UnitOfMeasurmentWidth * Util.String2Decimal(Convirsion_Factor);
            LengthInFeet = UnitOfMeasurmentLength * Util.String2Decimal(Convirsion_Factor);
        }
        Calculate_CFTCBM();
    }
       
    public void Get_ContractDetails(String Call_From)
    {
        if (ContractId > 0 && Contract_BranchId > 0)
        {
            ds_ContractDetails = objShortGCPresenter.Get_ContractDetails(Call_From);
            Session_DS_ContractDetails = ds_ContractDetails;
            Get_FilterExpression();
        }
        else
        {
            SetBillingBranch("", "0");
            SetBillingParty("", "0");

            BillingBranchId = 0;
            BillingPartyId = 0;
            Is_ContractApplied = 0;

            if (keyID > 0)
            {
                objShortGCPresenter.Get_StdandardFreightRate();
                objShortGCPresenter.Get_BranchRateParameter();
            }
            Set_Applicable_Standard_Charges(false);
            Is_POD = false;
        }

        Calculate_Freight();
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();
        upd_tbl_Charges.Update();
    }

    public void Get_ServiceTaxDetails()
    {
        objShortGCPresenter.Get_Service_Tax_Details();
        On_PaymentTypeChange();
    }

    public void On_FreightBasisChanged()
    {
        if (FreightBasisId == 4)
        {
            tr_Volumetric.Visible = true;
            tr_VolumetrickgFactor.Visible = true;
            Calculate_CFTCBM();
        }
        else
        {
            tr_Volumetric.Visible = false;
            tr_VolumetrickgFactor.Visible = false;
            TotalCBM = 0;
            TotalCFT = 0;
        }
        On_PaymentTypeChange();
        Calculate_Freight();
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();
        upd_tbl_Charges.Update();
    }

    public void Calculate_CFTCBM()
    {
        TotalCFT = 0;
        TotalCBM = 0;

        if (FreightBasisId == 4)
        {
            TotalCFT = LengthInFeet * WidthInFeet * HeightInFeet;
            TotalCBM = TotalCFT / Util.String2Decimal("34.328125");

            TotalCFT = Math.Round(TotalCFT, 2);
            TotalCBM = Math.Round(TotalCBM, 2);
        }
        else
        {
            TotalCFT = 0;
            TotalCBM = 0;
        }
    }

    private void SetLinks_Rights()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        Boolean can_add_Location;

        Boolean can_add_Consignor;
        Boolean can_edit_Consignor;
        Boolean can_view_Consignor;

        Boolean can_add_Consignee;
        Boolean can_edit_Consignee;
        Boolean can_view_Consignee;

        Boolean can_add_Commodity;
        Boolean can_add_Item;
    
        fRights = uObj.getForm_Rights(34);  // service location 
        can_add_Location = fRights.canAdd();

        fRights = uObj.getForm_Rights(36);    //  Regular Client
        can_add_Consignor  = fRights.canAdd();
        can_edit_Consignor = fRights.canEdit();
        can_view_Consignor = fRights.canRead();

        //fRights = uObj.getForm_Rights(36);  // Regular Client
        can_add_Consignee  = fRights.canAdd();
        can_edit_Consignee = fRights.canEdit();
        can_view_Consignee = fRights.canRead();


        fRights = uObj.getForm_Rights(13);  // Commodity
        can_add_Commodity  = fRights.canAdd();

        fRights = uObj.getForm_Rights(16);  // Item
        can_add_Item  = fRights.canAdd();
       
        hdn_Can_Add_Location.Value = can_add_Location.ToString();
        hdn_Can_Add_Consignor.Value = can_add_Consignor.ToString();
        hdn_Can_Edit_Consignor.Value = can_edit_Consignor.ToString();
        hdn_Can_View_Consignor.Value = can_view_Consignor.ToString();
        hdn_Can_Add_Consignee.Value = can_add_Consignee.ToString();
        hdn_Can_Edit_Consignee.Value = can_edit_Consignee.ToString();
        hdn_Can_View_Consignee.Value = can_view_Consignee.ToString();
        hdn_Can_Add_Commodity.Value = can_add_Commodity.ToString();
        hdn_Can_Add_Item.Value = can_add_Item.ToString();
    }

    public Decimal ValueOfTextBox(TextBox T)
    {
        Decimal val = 0;
        val = Util.String2Decimal(T.Text.Trim() == string.Empty ? "0.00" : T.Text.Trim());
        return Math.Round(val, 2); ;
    }

    public Decimal ValueOfLable_Decimal(Label L)
    {
        Decimal val = 0;
        val = Util.String2Decimal(L.Text.Trim() == string.Empty ? "0.00" : L.Text.Trim());
        return Math.Round(val, 2); ;
    }

    public Int32 ValueOfLable_Int(Label L)
    {
        return Util.String2Int(L.Text.Trim() == string.Empty ? "0" : L.Text.Trim());
    }

    public Decimal ValueOfHdn_Decimal(HiddenField H)
    {
        Decimal val = 0;
        val = Util.String2Decimal(H.Value.Trim() == string.Empty ? "0.00" : H.Value.Trim());
        return Math.Round(val, 2); ;
    }

    public Int32 ValueOfHdn_Int(HiddenField H)
    {
        return Util.String2Int(H.Value.Trim() == string.Empty ? "0" : H.Value.Trim());
    }

    public void Calculate_Freight()
    {
        if (FreightBasisId == 4 && VolumetricFreightUnitId == 1)
        {
            Calculate_CFTCBM();
            Freight = TotalCFT * FreightRate;
        }
        else if (FreightBasisId == 4 && VolumetricFreightUnitId == 2)
        {
            Calculate_CFTCBM();

            Freight = TotalCBM * FreightRate;
            Applicable_Standard_FreightAmount = TotalCBM * Applicable_Standard_FreightRate;
            Standard_FreightAmount = Applicable_Standard_FreightAmount;
        }
        else if (FreightBasisId == 4 && VolumetricFreightUnitId == 3)
        {
            Calculate_ChargeWeight();

            Freight = ChargeWeight / CompanyParameter_Standard_FreightRatePer * FreightRate;
            Applicable_Standard_FreightAmount = ChargeWeight / CompanyParameter_Standard_FreightRatePer
                                                 * Applicable_Standard_FreightRate;

            Standard_FreightAmount = Applicable_Standard_FreightAmount;
        }
        else if (FreightBasisId == 3) // for Fixed
        {
            Freight = FreightRate;
            Applicable_Standard_FreightAmount = Applicable_Standard_FreightRate;// Standard_FreightRate;
            Standard_FreightAmount = Standard_FreightRate;
        }
        else if (FreightBasisId == 2)// for article
        {
            Calculate_ChargeWeight();
            Freight = TotalArticles * FreightRate;
            Standard_FreightAmount = TotalArticles * Applicable_Standard_FreightRate;
        }
        else
        {
            Calculate_ChargeWeight();
            Freight = (ChargeWeight / CompanyParameter_Standard_FreightRatePer) * FreightRate;
            Applicable_Standard_FreightAmount = (ChargeWeight / CompanyParameter_Standard_FreightRatePer) *
                                                Applicable_Standard_FreightRate;
            Standard_FreightAmount = Applicable_Standard_FreightAmount;
        }
        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            Freight = 0;
            Applicable_Standard_FreightAmount = 0;
            Standard_FreightAmount = Applicable_Standard_FreightAmount;
        }
    }

    public void Calculate_GrandTotal()
    {
        Decimal Payable_Amount;

        if (PaymentTypeId != 1 || Is_ToPay_Charge_Require == false) 
        {
            ToPayCharge = 0;
        }

        if (Is_DACC == false)
        {
            DACCCharges = 0;
        }

        if (LengthChargeHeadId == 0)
        {
            LengthCharge = 0;
        }

        if (_meniItemID != 184)
        {
            ReBookGC_Amount = 0;
            ReBookGC_OctroiAmount = 0;
        }

        SubTotal = Freight + LocalCharge + LoadingCharge +
                    StationaryCharge + FOVRiskCharge +
                    ToPayCharge + DDCharge + OtherCharges +
                    ReBookGC_Amount + DACCCharges +
                    LengthCharge + UnloadingCharge;

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            SubTotal = 0;
        }

        Decimal Tax_Abate = SubTotal * Util.String2Decimal("0.75");

        if (SubTotal < 750 && BookingTypeId == 1) Tax_Abate = 0;
        if (SubTotal < 1500 && BookingTypeId != 1) Tax_Abate = 0;

        if (ServiceTaxPayableBy != 3) Tax_Abate = 0;

        Abatment = Tax_Abate;

        Decimal Amt_Taxable = 0;

        Amt_Taxable = SubTotal - Tax_Abate;

        if (SubTotal < 750 && BookingTypeId == 1) Amt_Taxable = 0;
        if (SubTotal < 1500 && BookingTypeId != 1) Amt_Taxable = 0;

        TaxableAmount = Math.Round(Amt_Taxable);

        ServiceTax = (Standard_ServiceTaxPercent / 100) * Amt_Taxable;
        ServiceTax = Math.Round(ServiceTax);

        if (SubTotal < 750 && BookingTypeId == 1) ServiceTax = 0; //to pay
        if (SubTotal < 1500 && BookingTypeId != 1) ServiceTax = 0; //other

        Standard_ServiceTaxAmount = (Standard_ServiceTaxPercent / 100) * Amt_Taxable;
        Standard_ServiceTaxAmount = Math.Round(Standard_ServiceTaxAmount);

        Applicable_Standard_ServiceTaxAmount = (Applicable_Standard_ServiceTaxPercent / 100) * Amt_Taxable;
        Applicable_Standard_ServiceTaxAmount = Math.Round(Applicable_Standard_ServiceTaxAmount);

        if (SubTotal < 750 && BookingTypeId == 1)//to pay
        {
            Standard_ServiceTaxAmount = 0;
            Applicable_Standard_ServiceTaxAmount = 0;
        }

        if (SubTotal < 1500 && BookingTypeId != 1) //other
        {
            Standard_ServiceTaxAmount = 0;
            Applicable_Standard_ServiceTaxAmount = 0;
        }

        Decimal GrandTotal = 0;

        if (Is_Service_Tax_Applicable_For_Commodity == false)
        {
            ServiceTax = 0;
            Abatment = 0;
            TaxableAmount = 0;

            Standard_ServiceTaxAmount = 0;
            Applicable_Standard_ServiceTaxAmount = 0;
        }

        if (ServiceTaxPayableBy != 3) // service tax paid by client 
            GrandTotal = SubTotal;
        else
            GrandTotal = SubTotal + ServiceTax;

        if (ReBook_GCOctroiPaidByID == 3 && ReBook_GC_Id > 0 && _meniItemID == 184)
        {
            GrandTotal = GrandTotal + ReBookGC_OctroiAmount;
        }

        GrandTotal = Math.Round(GrandTotal);

        TotalGCAmount = GrandTotal;

        Payable_Amount = 0;

        if (PaymentTypeId == 1)//  //To pay
        {
            Advance = Advance;
            Payable_Amount = GrandTotal;
        }
        else
        {
            Advance = 0;
        }

        if (Advance > Payable_Amount)
        {
            Advance = 0;
        }

        if (PaymentTypeId == 2 || PaymentTypeId == 4) // paid
        {
            CashAmount = GrandTotal - ChequeAmount; //paid 
            Payable_Amount = GrandTotal;
        }
        else if (PaymentTypeId == 1) // to pay
        {
            CashAmount = Advance - ChequeAmount; //To pay
            Payable_Amount = Advance;
        }

        if (ChequeAmount > Payable_Amount || ChequeAmount == 0)
        {
            ChequeAmount = 0.00M;
            BankName = "";
            ChequeNo = "";
        }
    }

    public void Calculate_ChargeWeight()
    {
        Decimal CalculatedChargeWeight = 0;
        if (ActualWeight < TotalWeight)
        {
            ActualWeight = TotalWeight;
        }

        ChargeWeight = ActualWeight;

        if (ChargeWeight < ActualWeight)
        {
            CalculatedChargeWeight = ActualWeight;
        }

        if (FreightBasisId == 4 && VolumetricFreightUnitId == 3)
        {
            CalculatedChargeWeight = TotalCFT * VolumetricToKgFactor;
        }

        Decimal Mod;

        Decimal Difference;
        Mod = ActualWeight % 5;

        if (Mod > 0)
        {
            Difference = 5 - Mod;
            CalculatedChargeWeight = ActualWeight + Difference;
        }

        if (CalculatedChargeWeight < Applicable_Standard_MinimumChargeWeight)// hdn_Standard_Minimum_ChargeWeight  )
        {
            CalculatedChargeWeight = Applicable_Standard_MinimumChargeWeight;
        }
        if (ChargeWeight < CalculatedChargeWeight || ChargeWeight < Applicable_Standard_MinimumChargeWeight)// hdn_Standard_Minimum_ChargeWeight  )
        {
            ChargeWeight = CalculatedChargeWeight;
        }
    }

    public void On_PaymentTypeChange()
    {
        if (PaymentTypeId == 5 || Is_Attached == true)     // foc
        {
            FreightRate = 0;
            Freight = 0;
            LocalCharge = 0;
            LoadingCharge = 0;
            StationaryCharge = 0;
            ToPayCharge = 0;
            DDCharge = 0;
            OtherCharges = 0;
            FOVRiskCharge = 0;
            Advance = 0;
            CashAmount = 0;
            ChequeAmount = 0;
            BankName = "";
            ChequeNo = "";
            txt_FreightRate.Enabled = false;
            txt_Freight.Enabled = false;
            txt_LocalCharge.Enabled = false;
            txt_LoadingCharge.Enabled = false;
            txt_StationaryCharge.Enabled = false;
            txt_ToPayCharge.Enabled = false;
            txt_DDCharge.Enabled = false;
            txt_FOVRiskCharge.Enabled = false;
            txt_Advance.Enabled = false;
            txt_CashAmount.Enabled = false;
            txt_ChequeAmount.Enabled = false;
            txt_BankName.Enabled = false;
        }
        else
        {
            txt_FreightRate.Enabled = true;
            txt_Freight.Enabled = true;
            txt_LocalCharge.Enabled = true;
            txt_LoadingCharge.Enabled = true;
            txt_StationaryCharge.Enabled = true;
            txt_ToPayCharge.Enabled = true;
            txt_DDCharge.Enabled = true;
            txt_FOVRiskCharge.Enabled = true;
            txt_Advance.Enabled = true;
            txt_CashAmount.Enabled = true;
            txt_ChequeAmount.Enabled = true;
            txt_BankName.Enabled = true;
        }

        if (PaymentTypeId == 1)    // topay
        {
            Advance = 0;
            txt_Advance.Enabled = true;
            CashAmount = 0;
            ChequeAmount = 0;
            BankName = "";
            ChequeNo = "";
        }
        else
        {
            Advance = 0;
            txt_Advance.Enabled = false;
        }

        if (Is_ServiceTaxApplicableForConsignor == 1 && (PaymentTypeId == 2 || PaymentTypeId == 4))
        {
            ServiceTaxPayableBy = 1; // consignee
        }
        else if (Is_ServiceTaxApplicableForConsignee == 1 && PaymentTypeId == 1)
        {
            ServiceTaxPayableBy = 2; // consignor
        }
        else if (Is_ServiceTaxApplicableForConsignor == 1 && PaymentTypeId == 3)
        {
            ServiceTaxPayableBy = 1; // consignor
        }
        else if (Is_ServiceTaxApplicableForConsignor == 0 && PaymentTypeId == 3)
        {
            ServiceTaxPayableBy = 3; // transporter
        }
        else
        {
            ServiceTaxPayableBy = 3; // transporter 
        }

        ChequeDate = Convert.ToDateTime(hdn_ChequeDate.Value);          
        Calculate_GrandTotal();
    }

    public void Get_BillingPartyDetails()
    {
        objShortGCPresenter.Get_BillingPartyDetails();
    }   

    public DataSet Get_ConsignorConsigneeDetails(Int32 ConsignorConsigneeId, Boolean Is_RegularClient, Boolean Is_Consignor)
    {
        ds = objShortGCPresenter.Get_ConsignorConsigneeDetails(ConsignorConsigneeId, Is_RegularClient, Is_Consignor);
        return ds;
    }

    public void Get_From_Location_Details()
    {
        String From_Location_Name;
        String From_Location_Id;

        ds = objShortGCPresenter.Get_From_Location_Details();

        if (ds.Tables[0].Rows.Count > 0)
        {
            From_Location_Id = ds.Tables[0].Rows[0]["Service_Location_ID"].ToString();
            From_Location_Name = ds.Tables[0].Rows[0]["Service_Location_Name"].ToString();

            SetFromLocation(From_Location_Name , From_Location_Id  );
            FromLocationId = Util.String2Int(From_Location_Id) ;
        }
        else
        {            
            From_Location_Id = "0";
            From_Location_Name = "";

            SetFromLocation(From_Location_Name , From_Location_Id  );
            FromLocationId = Util.String2Int(From_Location_Id) ;
        }

    }

    private void Set_Default_Value(object sender, EventArgs e)
    {
        string current_time = DateTime.Now.ToShortTimeString();

        wuc_BookingTime.setFormat("24");
        wuc_BookingTime.setTime(current_time);
        BookingDate = DateTime.Now;
        ChequeDate = DateTime.Now;
        PolicyExpDate = DateTime.Now;
        ArrivedDate = DateTime.Now;

        SetBookingBranch(UserManager.getUserParam().MainName, UserManager.getUserParam().MainId.ToString());
        SetArrivedFromBranch(UserManager.getUserParam().MainName, UserManager.getUserParam().MainId.ToString());

        BookingBranchId = UserManager.getUserParam().MainId;
        ArrivedFromBranchId = UserManager.getUserParam().MainId;

        Get_From_Location_Details();
        ExpectedDeliveryDate = DateTime.Now;//.ToString("dd MMM yyyy");

        if (_meniItemID == 200)
        {
            ddl_FromLocation.OtherColumns = ddl_BookingBranch.SelectedValue  ;
            ddl_ToLocation.OtherColumns = "0";
        }
        else
        {
            ddl_FromLocation.OtherColumns = UserManager.getUserParam().MainId.ToString();
            ddl_ToLocation.OtherColumns = "0";
        }
        BookingBranchId = UserManager.getUserParam().MainId;
        ArrivedFromBranchId = UserManager.getUserParam().MainId;

        ActualWeight = 0;// Standard_MinimumChargeWeight;
        ChargeWeight = Standard_MinimumChargeWeight;

        ConsignmentTypeId = 1; // local 
        PaymentTypeId = 2; // paid
        PickupTypeId = 2;  // counter
        UnitOfMeasurementId = 2; // feet 

        BookingTypeId = 1; // Sundry
        DeliveryTypeId = 1; // Godown
        MarketingExecutiveId = 0;
        LoadingSuperVisorId = 0;

        Set_Controls_As_GC_Company_Parameter();

        objShortGCPresenter.Get_BookingSubType();
    }

    public void Get_StandardBasicFreight()
    {
        FreightRate = 0;
        Standard_FreightRate = 0;
        Special_FreightRate = 0;

        objShortGCPresenter.Get_StdandardFreightRate();

        ExpectedDeliveryDate = wuc_BookingDate.SelectedDate.AddDays(Convert.ToDouble(TotalTransitDays));//.ToString("dd MMM yyyy"); 
    }
    public void Set_Search_By_Code()
    {
        ddl_Consignor.OtherColumns = "";
        ddl_Consignee.OtherColumns = "";
    }
    private void Fill_Item(object sender, EventArgs e)
    {
        ddl_Commodity = (DropDownList)sender;

        DataGridItem dg_Commodity = (DataGridItem)ddl_Commodity.Parent.Parent;
        ddl_Commodity = (DropDownList)(dg_Commodity.FindControl("ddl_Commodity"));
        ddl_Item = (DropDownList)(dg_Commodity.FindControl("ddl_Item"));

        if (ddl_Commodity.Items.Count > 0)
        {
            objShortGCPresenter.Fill_Item(Util.String2Int(ddl_Commodity.SelectedValue));
            BindItem = Session_ItemDdl;
        }
        else
        {
            objShortGCPresenter.Fill_Item(0);
            BindItem = Session_ItemDdl;
        }
    }

    #endregion

    #region ControlsEvent

    protected void btn_Save_New_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";

        if (validateUI())
        {
            Calculate_Standard_Charges();

            if (ReBook_GC_Id > 0 && keyID <= 0)
            {
                Allow_To_ReBook();

                if (Is_Allow_To_ReBook && Is_ReBookGC_Octroi_Applicable != Is_ReBookGC_Octroi_Updated)
                {
                    errorMessage = GetLocalResourceObject("Msg_UpdateOctroi").ToString();
                }
                else if (!Is_Allow_To_ReBook)
                {
                    errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
                }
                else
                {
                    objShortGCPresenter.save();
                }
            }
            else if (_meniItemID == 194)
            {
                Allow_To_Rectify();
                if (Is_Allow_To_Rectify)
                {              
                    errorMessage = "";
                    objShortGCPresenter.save();
                }
            }
            else
            {
                objShortGCPresenter.save();
            }
            string _Msg;
            _Msg = "Saved SuccessFully";

            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                    ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                    ClassLibraryMVP.Util.EncryptString("Operations/Booking/FrmShortGC.aspx?Menu_Item_Id=" +
                    ClassLibraryMVP.Util.EncryptInteger(_meniItemID) + "&Mode=" + Mode));
        }
    }

    protected void btn_Save_Repeat_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndRepet";
        if (validateUI())
        {
            Calculate_Standard_Charges();

            if (ReBook_GC_Id > 0 && keyID <= 0)
            {
                Allow_To_ReBook();

                if (Is_Allow_To_ReBook && Is_ReBookGC_Octroi_Applicable != Is_ReBookGC_Octroi_Updated)
                {
                    errorMessage = GetLocalResourceObject("Msg_UpdateOctroi").ToString();
                }
                else if (!Is_Allow_To_ReBook)
                {
                    errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
                }
                else
                {
                    objShortGCPresenter.save();
                    Get_Next_GC_No();
                }
            }
            else if (_meniItemID == 194)
            {
                Allow_To_Rectify();
                if (Is_Allow_To_Rectify)
                {
                    errorMessage = "";
                    objShortGCPresenter.save();
                    Get_Next_GC_No();
                }
            }
            else
            {
                objShortGCPresenter.save();

                if (_meniItemID == 200 && Is_Opening_GC == true)
                {
                    ddl_GC_No.SelectedIndex = 0;
                    Next_No = 0;
                }
                else
                {
                    Get_Next_GC_No();
                }

                btn_Save_New.Enabled = true ;
                btn_Save_Exit.Enabled = true;
                btn_Save_Print.Enabled = true;
                btn_Save_Repeat.Enabled = true;
            }
        }
    }     

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        if (validateUI())
        {
            Calculate_Standard_Charges();

            if (ReBook_GC_Id > 0 && keyID <= 0)
            {
                Allow_To_ReBook();

                if (Is_Allow_To_ReBook && Is_ReBookGC_Octroi_Applicable != Is_ReBookGC_Octroi_Updated)
                {
                    errorMessage = GetLocalResourceObject("Msg_UpdateOctroi").ToString();
                }
                else if (!Is_Allow_To_ReBook)
                {
                    errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
                }
                else
                {
                    objShortGCPresenter.save();
                }
            }
            else if (_meniItemID == 194)
            {
                Allow_To_Rectify();
                if (Is_Allow_To_Rectify)
                {
                    errorMessage = "";
                    objShortGCPresenter.save();
                }
            }
            else
            {
                objShortGCPresenter.save();
            }
            string _Msg;
            _Msg = "Saved SuccessFully";
                        
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                    ClassLibraryMVP.Util.EncryptString(_Msg));
        }
    }

    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";

        if (validateUI())
        {
            Calculate_Standard_Charges();

            if (ReBook_GC_Id > 0 && keyID <= 0)
            {
                Allow_To_ReBook();

                if (Is_Allow_To_ReBook && Is_ReBookGC_Octroi_Applicable != Is_ReBookGC_Octroi_Updated)
                {
                    errorMessage = GetLocalResourceObject("Msg_UpdateOctroi").ToString();
                }
                else if (!Is_Allow_To_ReBook)
                {
                    errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
                }
                else
                {
                    objShortGCPresenter.save();
                }
            }
            else if (_meniItemID == 194)
            {
                Allow_To_Rectify();
                if (Is_Allow_To_Rectify)
                {
                    errorMessage = "";
                    objShortGCPresenter.save();
                }
            }
            else
            {
                objShortGCPresenter.save();
            }
            string _Msg;
            _Msg = "Saved SuccessFully";

            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            int Document_ID = GC_Id;

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                    ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                    ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" +
                    ClassLibraryMVP.Util.EncryptInteger(_meniItemID) + "&Mode=" + Mode + "&Document_ID=" +
                    ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
        }
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        _flag = "Print";
        Calculate_Standard_Charges();

        if (keyID > 0)
        {
            string _Msg;
            _Msg = "Print SuccessFully";

            string Mode = HttpContext.Current.Request.QueryString["Mode"];

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                    ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                    ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" +
                    ClassLibraryMVP.Util.EncryptInteger(_meniItemID) + "&Mode=" + Mode + "&Document_ID=" +
                    ClassLibraryMVP.Util.EncryptInteger(keyID)));
        }
        else
        {
            Response.Write("<script language='javascript'>{alert('Details Not Available'); }</script>");
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void btn_GetAttachedGCDetails_Click(object sender, EventArgs e)
    {
        Attached_GC_Id = 0;
        errorMessage = "";

        if (Attached_GC_No_For_Print != No_For_Padd)
        {
            objShortGCPresenter.Read_GC_Details(true);
            BindCommodityGrid_Attached();

            if (Attached_GC_Id <= 0)
            {
                errorMessage = "Invalid " + CompanyManager.getCompanyParam().GcCaption + "  No.";
                Clear_GC_Details(sender, e);
            }
             
            objShortGCPresenter.Get_Service_Tax_Applicable_For_Commodity();
            Calculate_MultipleCommodityTotalEdit();

            Calculate_InvoiceTotal();
            Convert_InTo_Feet();

            if (Attached_GC_Id > 0 && ContractId > 0)
            {
                Get_ContractDetails("From_GC_Read_Value");
                Get_BillingPartyDetails();
            }
            if (hdn_IsAttached.Value == "1")
            {
                Clear_Other_Charges();
            }

            if (Is_ContractApplied == 0 || ContractId <= 0)
            {
                objShortGCPresenter.Get_BranchRateParameter();
            }
            Get_Applicable_Service_Tax();
        }
        else
        {
            if (Attached_GC_Id <= 0)
            {
                errorMessage = GetLocalResourceObject("Msg_InValidAttachedGCNo").ToString();
                Clear_GC_Details(sender, e);
            }
            else
            {
                errorMessage = GetLocalResourceObject("Msg_OldGCNo").ToString();
            }
        }
    }

    protected void btn_ReadContractDetails_Click(object sender, EventArgs e)
    {
        Get_FilterExpression();
        Calculate_Freight();
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();
    }

    protected void btn_GetOtherCharges_Click(object sender, EventArgs e)
    {
        if (StateManager.IsValidSession("GCOtherChargeHead"))
        {
            OtherCharges = Util.String2Decimal(Session_GCOtherChargeHead.Compute("sum(Amount)", "").ToString());
        }
        else
        {
            OtherCharges = 0;
        }

        Calculate_GrandTotal();
    }

    protected void btn_SetOtherCharges_Click(object sender, EventArgs e)
    {
        int i;
        if (StateManager.IsValidSession("GCOtherChargeHead"))
        {
            for (i = 0; i <= Session_GCOtherChargeHead.Rows.Count - 1; i++)
            {
                Session_GCOtherChargeHead.Rows[i]["Amount"] = 0;
                Session_GCOtherChargeHead.AcceptChanges();
            }
            OtherCharges = Util.String2Decimal(Session_GCOtherChargeHead.Compute("sum(Amount)", "").ToString());
        }
        else
        {
            OtherCharges = 0;
        }
    }
    protected void btn_Get_GC_No_Click(object sender, EventArgs e)
    {
        Get_Next_GC_No ();
    }

    protected void btn_SetDoorDeliveryAdderess_Click(object sender, EventArgs e)
    {
        ConsigneeDDAddressLine1 = Session_ConsigneeAddressLine1;
        ConsigneeDDAddressLine2 = Session_ConsigneeAddressLine2;
    }

    protected void btn_SetConsignorConsigneeDetails_Click(object sender, EventArgs e)
    {
        if (hdn_Is_Consignor.Value == "1")
        {
            ConsignorId = Util.String2Int(hdn_ClientId.Value);
            ddl_Consignor_TxtChange(sender, e);
        }
        else
        {
            ConsigneeId = Util.String2Int(hdn_ClientId.Value);
            ddl_Consignee_TxtChange(sender, e);
        }
    }

    protected void btn_SetLocationDetails_Click(object sender, EventArgs e)
    {
        if (hdn_Is_FromLocation.Value == "1")
        {
            FromLocationId = Util.String2Int(hdn_LocationId.Value);
            SetFromLocation(hdn_LocationName.Value.ToUpper(), hdn_LocationId.Value);
            ddl_FromLocation_TxtChange(sender, e);
        }
        else
        {
            ToLocationId = Util.String2Int(hdn_LocationId.Value);
            SetToLocation(hdn_LocationName.Value.ToUpper(), hdn_LocationId.Value);
            ddl_ToLocation_TxtChange(sender, e);
        }
    }

    protected void btn_SetCommodityDetails_Click(object sender, EventArgs e)
    {        
        DataRow dr;
        dr = Session_CommodityDdl.NewRow();
        dr["Commodity_Id"] = hdn_CommodityId.Value;
        dr["Commodity_Name"] = hdn_CommodityName.Value;

        Session_CommodityDdl.Rows.Add(dr);
        dg_Commodity.ShowFooter = true;
        dg_CommodityNandwana.ShowFooter = true;
        Set_Commodity_SrNo();

        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();

        dg_CommodityNandwana.DataSource = Session_MultipleCommodityGrid;
        dg_CommodityNandwana.DataBind();
    }

    protected void btn_SetItemDetails_Click(object sender, EventArgs e)
    {
        dg_Commodity.ShowFooter = true;
        dg_CommodityNandwana.ShowFooter = true;

        Set_Commodity_SrNo();

        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();
        
        dg_CommodityNandwana.DataSource = Session_MultipleCommodityGrid;
        dg_CommodityNandwana.DataBind();
    }

    protected void btn_ValidateGCNo_Click(object sender, EventArgs e)
    {
        if (validate_GC_No())
        {
        }
    }

    protected void btn_Get_ServiceTaxDetails_Click(object sender, EventArgs e)
    {
        Get_ServiceTaxDetails();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        On_Load();

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            txt_GC_No_For_Print.MaxLength = GC_No_Length;
            ddl_ServiceTaxPayableBy.Enabled = false;
        }

        objShortGCPresenter = new  ShortGCPresenter (this, IsPostBack);
        Get_No_To_Padd();

        if (_meniItemID == 200 && ClientCode.ToLower() == "nandwana" && Is_Opening_GC == true)
        {
            ddl_GC_No.Visible = true;
            lbl_GC_No.Visible = true;
        }
        else
        {
            ddl_GC_No.Visible = false;
            lbl_GC_No.Visible = false;
        }

        if (_meniItemID == 200)
        {
            ddl_FromLocation.OtherColumns = ddl_BookingBranch.SelectedValue;
            ddl_ToLocation.OtherColumns = "0";
        }
        else
        {
            ddl_FromLocation.OtherColumns = UserManager.getUserParam().MainId.ToString();
            ddl_ToLocation.OtherColumns = "0";
        }

        if (!IsPostBack)
        {
            hdf_ResourecString.Value = CommonObj.GetResourceString("Operations/Booking/App_LocalResources/WucShortGC.ascx.resx");

            BindCommodityGrid();

            objShortGCPresenter.Get_BookingSubType();
            objShortGCPresenter.Get_Additional_Freight();

            if (keyID <= 0)
            {
                Is_ServiceTaxApplicableForConsignee = 0;
                Is_ServiceTaxApplicableForConsignor = 0;

                Previous_SubTotal = 0;
                Previous_GrandTotal = 0;
            }

            objShortGCPresenter.Get_Service_Tax_Applicable_For_Commodity();
            Calculate_MultipleCommodityTotalEdit();

            Calculate_InvoiceTotal();
            Convert_InTo_Feet();

            ddl_BookingType_SelectedIndexChanged(sender, e);
        }

        if (keyID > 0)
        {
            txt_GC_No_For_Print.Enabled = false;
            ddl_GC_No.Enabled = false;

            if (UserManager.getUserParam().HierarchyCode.ToUpper() == "HO")
            {
                ddl_FromLocation.Enabled = false;
                lnk_AddFromServiceLocation.Visible = false;
            }
            if (!IsPostBack)
            {
                objShortGCPresenter.Get_TransitDays();
                ExpectedDeliveryDate = wuc_BookingDate.SelectedDate.AddDays(Convert.ToDouble(TotalTransitDays));//.ToString("dd MMM yyyy"); 
            }
        }

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Session_TotalRatio = 0;
                Set_Default_Value(sender,e);

                Calculate_Freight();
                Calculate_LocalCharge();
                Calculate_LoadingCharge();
                Calculate_FOV();
                Calculate_DDODA_Charge();

                if (PaymentTypeId != 1 || Is_ToPay_Charge_Require == false)  // if not to pay
                {
                    ToPayCharge = 0;
                }
                Get_Applicable_Service_Tax();
                Calculate_GrandTotal();
            }
        }
      
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                if (Is_Opening_GC == false)
                {
                    Get_Next_GC_No ();                    
                    Get_Next_Counter_No();

                    if (Is_Auto_Booking_MR_For_Paid_Booking)
                    {
                        Get_Next_MR_No();
                    }
                }
                else
                {
                    DocumentSeriesAllocationId = 0;
                    Start_No = 0;
                    End_No = 0;
                    Next_No = 0;
                    DocumentNextCounterNo = 0;
                }
            }
            else
            {
                ddl_ConsignmentType.Focus();
            }
            ddl_ConsignmentType.Focus();
        }
        else
        {
            ddl_ConsignmentType.Focus();
        }

        if (!IsPostBack)
        {
            // _meniItemID = 30  for Normal GC
            // _meniItemID = 184 for ReBook GC
            // _meniItemID = 188 for IBA GC
            // _meniItemID = 194 for Rectification GC

            if (keyID <= 0 && _meniItemID == 184)
            {
                if (Request.QueryString["ReBook_GC_No_For_Print"].ToString().Trim() != "")
                {
                    Attached_GC_No_For_Print = Request.QueryString["ReBook_GC_No_For_Print"].ToString().Trim();
                    btn_GetAttachedGCDetails_Click(sender, e);
                    ReBook_GC_Id = Util.String2Int(Request.QueryString["ReBook_GC_ID"].ToString().Trim());
                    Is_ReBooked = true;
                    Is_Attached = false;

                    if (Is_Opening_GC == false)
                    {
                        Get_Next_GC_No ();                        
                        Get_Next_Counter_No();
                        
                        if (Is_Auto_Booking_MR_For_Paid_Booking)
                        {
                            Get_Next_MR_No();
                        }
                    }
                    else
                    {
                        DocumentSeriesAllocationId = 0;
                        Start_No = 0;
                        End_No = 0;
                        Next_No = 0;
                        DocumentNextCounterNo = 0;
                    }

                    SetFromLocation("", "0");
                    SetToLocation("", "0");
                    FromLocationId = 0;
                    ToLocationId = 0;
                    DeliveryBaranchId = 0;
                    DeliveryBranchName = ""; 

                    if (_meniItemID == 200)
                    {
                        ddl_FromLocation.OtherColumns = ddl_BookingBranch.SelectedValue;
                        ddl_ToLocation.OtherColumns = "0";
                    }
                    else
                    {
                        ddl_FromLocation.OtherColumns = UserManager.getUserParam().MainId.ToString();
                        ddl_ToLocation.OtherColumns = "0";
                    }

                    SetLoadingSuperVisor("", "0");
                    LoadingSuperVisorId = 0;
                    Previous_SubTotal = 0;
                    Previous_GrandTotal = 0;                    
                }
            }
            else if (keyID <= 0 && _meniItemID == 194) // rectification
            {

                if (Request.QueryString["Rectification_GC_No_For_Print"].ToString().Trim() != "")
                {
                    Attached_GC_No_For_Print = Request.QueryString["Rectification_GC_No_For_Print"].ToString().Trim();
                    btn_GetAttachedGCDetails_Click(sender, e);
                    ReBook_GC_Id = Util.String2Int(Request.QueryString["Rectification_GC_ID"].ToString().Trim());
                    Is_ReBooked = true;
                    Is_Attached = false;

                    if (Is_Opening_GC == false)
                    {
                        Get_Next_GC_No ();
                        Get_Next_Counter_No();

                        if (Is_Auto_Booking_MR_For_Paid_Booking)
                        {
                            Get_Next_MR_No();
                        }
                    }
                    else
                    {
                        DocumentSeriesAllocationId = 0;
                        Start_No = 0;
                        End_No = 0;
                        Next_No = 0;
                        DocumentNextCounterNo = 0;
                    }

                    if (_meniItemID == 200)
                    {
                        ddl_FromLocation.OtherColumns = ddl_BookingBranch.SelectedValue;
                        ddl_ToLocation.OtherColumns = "0";
                    }
                    else
                    {
                        ddl_FromLocation.OtherColumns = UserManager.getUserParam().MainId.ToString();
                        ddl_ToLocation.OtherColumns = "0";
                    }
                }
            }

            if (_meniItemID == 188)
            {
                Is_DACC = true;
                if (keyID <= 0)
                {
                    Is_DACC = true;
                    DACCCharges = Applicable_Standard_DACCCharges;
                }
            }
            else
            {
                Is_DACC = false;
                DACCCharges = 0;
                Applicable_Standard_DACCCharges = 0;
            }
        }

        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                hdn_GCId.Value = keyID.ToString();
                Calculate_Billing_Total_Ratio();

                if (ReBook_GC_Id > 0 || Rectification_GC_Id > 0) // only for rebook gc and Rectification
                {
                    dg_Commodity.Columns[11].Visible = false;
                    dg_Commodity.Columns[12].Visible = false;
                    dg_Commodity.ShowFooter = false;

                    dg_CommodityNandwana.Columns[11].Visible = false;
                    dg_CommodityNandwana.Columns[12].Visible = false;
                    dg_CommodityNandwana.ShowFooter = false;
                }
            }
        }
        if (_meniItemID != 200)
        {
            GC_No = hdn_GC_No_For_Print.Value;
        }
        Next_No = Util.String2Int(hdn_GC_No_For_Print.Value);
        
        if (_meniItemID == 200)
        {
            BookingBranchId =Util.String2Int(ddl_BookingBranch.SelectedValue);
            ddl_FromLocation.OtherColumns = BookingBranchId.ToString();// ddl_BookingBranch.SelectedValue;
            ddl_ToLocation.OtherColumns = "0";            
        }
        else
        {
            ddl_FromLocation.OtherColumns = UserManager.getUserParam().MainId.ToString();
            ddl_ToLocation.OtherColumns = "0";

            if (keyID<=0)
                BookingBranchId = UserManager.getUserParam().MainId;
        }

        //Page.MaintainScrollPositionOnPostBack = true;

        if (_meniItemID == 213 || _meniItemID == 200 || _meniItemID == 194)
        {
            dg_Commodity.Columns[7].Visible = false;
            dg_Commodity.Columns[8].Visible = false;
            dg_Commodity.Columns[9].Visible = false;
            dg_Commodity.Columns[10].Visible = false;

            dg_CommodityNandwana.Columns[7].Visible = false;
            dg_CommodityNandwana.Columns[8].Visible = false;
            dg_CommodityNandwana.Columns[9].Visible = false;
            dg_CommodityNandwana.Columns[10].Visible = false;
            
            lbl_TotalLength.Visible = false;
            lbl_TotalHeight.Visible = false;
            lbl_TotalWidth.Visible = false;

            lbl_TotalLengthNandwana.Visible = false;
            lbl_TotalHeightNandwana.Visible = false;
            lbl_TotalWidthNandwana.Visible = false;

            if (!IsPostBack )
            {
                ddl_FreightBasis.Items.RemoveAt(3);
            }
        }

        if (_meniItemID == 194)
        {
            btn_Save_New.Visible = false;
            btn_Save_Repeat.Visible = false;
            btn_Save_Print.Visible = false;
        }
    }

    protected void ddl_FreightBasis_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM_ShortGC.SetFocus(ddl_FreightBasis);
    }

    protected void ddl_FromLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_FromLocation.OtherColumns = UserManager.getUserParam().MainId.ToString();
        SM_ShortGC.SetFocus(ddl_FromLocation);
    }

    protected void ddl_Contract_SelectedIndexChanged(object sender, EventArgs e)
    {
        Is_ContractApplied = 0;
        ContractId = Util.String2Int(ddl_Contract.SelectedValue);

        Get_ContractDetails("");
        Get_BillingPartyDetails();
        Is_POD = ContractId > 0 ? true : false;
        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_Contract);
    }

    protected void ddl_UnitOfMeasurment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Convert_InTo_Feet();
        Calculate_CFTCBM();
        SM_ShortGC.SetFocus(ddl_UnitOfMeasurment);
    }

    protected void ddl_RoadPermitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Standard_FreightRate = Standard_FreightRate - Additional_Freight;

        if (Standard_FreightRate <= 0)
        {
            Standard_FreightRate = 0;
        }

        objShortGCPresenter.Get_Additional_Freight();

        Standard_FreightRate = Standard_FreightRate + Additional_Freight;
        FreightRate = Standard_FreightRate;
        Applicable_Standard_FreightRate = FreightRate;

        Calculate_Freight();
        Calculate_GrandTotal();

        if (RoadPermitTypeId == 3)
        {
                ddl_Instruction.SelectedValue = "6";
                InstructionRemark = ddl_Instruction.SelectedItem.Text;
        }
    }

    protected void ddl_BookingSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddl_VehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objShortGCPresenter.Get_TransitDays();

        ExpectedDeliveryDate = wuc_BookingDate.SelectedDate.AddDays(Convert.ToDouble(TotalTransitDays));//.ToString("dd MMM yyyy"); 

        if ((Contract_UnitOfFreightId == 1 || // for vehicle 
            Contract_FreightSubUnitId == 5 || // for vehicle 
            Contract_UnitOfFreightId == 5 ||  // for Km
            Contract_FreightBasisId == 2 ||   // for Kilo Meter
             Contract_FreightBasisId == 3     // for Transit Days
             ) && ContractId > 0)
        {
            Is_ContractApplied = 0;
            Get_ContractDetails("");
        }
        upd_tbl_Charges.Update();
    }

    protected void ddl_BookingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objShortGCPresenter.Get_BookingSubType();

        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_BookingType);
    }

    protected void ddl_LengthChargeHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_LengthChargeHead.SelectedValue) > 0)
        {
            LengthCharge = 0;
            Standard_LengthCharge = 0;
            Contractual_LengthCharge = 0;
            Applicable_Standard_LengthCharge = 0;
            objShortGCPresenter.Get_LengthCharge();
        }
        else
        {
            LengthCharge = 0;
            Standard_LengthCharge = 0;
            Contractual_LengthCharge = 0;
            Applicable_Standard_LengthCharge = 0;
        }

        if (PaymentTypeId == 5 || Is_Attached == true)
        {
            LengthCharge = 0;
        }

        Calculate_GrandTotal();
        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_LengthChargeHead);
    } 

    protected void ddl_VolumetricFreightUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Calculate_Freight();
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();
        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_VolumetricFreightUnit);
    }

    protected void ddl_ToLocation_TxtChange(object sender, EventArgs e)
    {
        hdn_ToLocationId.Value = ddl_ToLocation.SelectedValue;
        hdn_ChequeDate.Value = ChequeDate.ToString();

        ds = objShortGCPresenter.Get_ToLocationDetails();
        Is_OctroiApplicable = false;
        Is_ODA = false;

        TotalTransitDays = 0;
        TotalKiloMeter = 0;

        if (ds.Tables[0].Rows.Count > 0)
        {
            DeliveryBaranchId = Util.String2Int(ds.Tables[0].Rows[0]["del_branch_id"].ToString());
            DeliveryBranchName = ds.Tables[0].Rows[0]["del_branch_name"].ToString();
            
            if (Util.String2Bool(ds.Tables[0].Rows[0]["Is_Octroi"].ToString()))
            {
                Is_OctroiApplicable = true;
            }
            else
            {
                Is_OctroiApplicable = false;
            }

            if (Util.String2Bool(ds.Tables[0].Rows[0]["Is_ODA"].ToString()))
            {
                Is_ODA = true;
            }
            else
            {
                Is_ODA = false;
            }

            if (Util.String2Bool(ds.Tables[0].Rows[0]["Is_To_Pay_Booking"].ToString()))
            {
                Is_ToPayBookingApplicable = true;
            }
            else
            {
                Is_ToPayBookingApplicable = false;
            }

            Standard_DDCharge_Rate = Util.String2Decimal(ds.Tables[0].Rows[0]["Door_Delivery_Charges"].ToString());
            Applicable_Standard_DDCharge_Rate = Util.String2Decimal(ds.Tables[0].Rows[0]["Door_Delivery_Charges"].ToString());

            Get_StandardBasicFreight();

            ODAChargesUpTo500Kg = Util.String2Decimal(ds.Tables[0].Rows[0]["Oda_charges_upto_500_Kg"].ToString());
            ODAChargesAbove500Kg = Util.String2Decimal(ds.Tables[0].Rows[0]["Oda_charges_above_500_Kg"].ToString());

            objShortGCPresenter.Get_RequireForms();
        }
        else
        {
            ODAChargesUpTo500Kg = 0;
            ODAChargesAbove500Kg = 0;
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            TotalTransitDays = Util.String2Int(ds.Tables[1].Rows[0]["Transit_Days"].ToString());
            TotalKiloMeter = Util.String2Int(ds.Tables[1].Rows[0]["Distance_In_Km"].ToString());
        }
        else
        {
            TotalTransitDays = 0;
            TotalKiloMeter = 0;
        }
        Get_FilterExpression();

        On_PaymentTypeChange();
        Calculate_Freight();
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();

        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_ToLocation);
    }

    protected void chk_ConsigneeSearchByCode_CheckedChanged(object sender, EventArgs e)
    {
        Set_Search_By_Code();
    }

    protected void ddl_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Item(sender, e);
        SM_ShortGC.SetFocus(ddl_Commodity);
    }

    protected void txt_VolumetricToKgFactor_TextChanged(object sender, EventArgs e)
    {
        Calculate_Freight();
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();
        upd_tbl_Charges.Update();
    }

    protected void wuc_ChequeDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_ChequeDate.Value = wuc_ChequeDate.SelectedDate.ToString();
    }

    protected void wuc_BookingDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_BookingDate.Value = wuc_BookingDate.SelectedDate.ToString();
        ExpectedDeliveryDate = wuc_BookingDate.SelectedDate.AddDays(Convert.ToDouble(TotalTransitDays));//.ToString("dd MMM yyyy"); 
        Get_Applicable_Service_Tax();
    }
     
    protected void wuc_ApplicationStartDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_ApplicationStartDate.Value = wuc_ApplicationStartDate.SelectedDate.ToString();        
    }
 
    public void Get_Applicable_Service_Tax()
    {
        Decimal Applicable_Service_Tax_Percent;        
        Applicable_Service_Tax_Percent = CommonObj.Get_Service_Tax_Percent(BookingDate);

        Standard_ServiceTaxPercent = Applicable_Service_Tax_Percent;
        Applicable_Standard_ServiceTaxPercent = Applicable_Service_Tax_Percent;

        ServiceTax_Label = "Service Tax " + Standard_ServiceTaxPercent.ToString() + "%";
        Calculate_GrandTotal();
    }

    protected void wuc_PolicyExpDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_PolicyExpDate.Value = wuc_PolicyExpDate.SelectedDate.ToString();
    }

    protected void chk_ConsignorSearchByCode_CheckedChanged(object sender, EventArgs e)
    {
        Set_Search_By_Code();
    }

    protected void chk_IsMultipleBilling_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void ddl_Consignee_TxtChange(object sender, EventArgs e)
    {
        char[] _Separator ={ ',' };
        string[] _IdArray = new string[2];

        if (ddl_Consignee.SelectedValue.Trim() != string.Empty || Util.String2Int(hdn_ClientId.Value.Trim()) > 0)
        {
            _IdArray = ddl_Consignee.SelectedValue.Split(_Separator);

            if (Util.String2Int(hdn_ClientId.Value.Trim()) > 0)
            {
                hdn_ConsigneeId.Value = hdn_ClientId.Value.Trim();
                hdn_IsRegularConsignee.Value = "1";

                ConsigneeId = Util.String2Int(hdn_ClientId.Value.Trim());
                Is_RegularConsignee = 1;

                hdn_ClientId.Value = "0";
                hdn_Is_Consignor.Value = "0";
            }
            else
            {
                hdn_ConsigneeId.Value = _IdArray[0];
                hdn_IsRegularConsignee.Value = _IdArray[1];

                ConsigneeId = Util.String2Int(_IdArray[0]);
                Is_RegularConsignee = Util.String2Int(_IdArray[1]);
            }

            EncreptedConsigneeId = Util.EncryptInteger(ConsigneeId);

            ds = Get_ConsignorConsigneeDetails(Convert.ToInt32(hdn_ConsigneeId.Value), Convert.ToBoolean(Convert.ToInt32(hdn_IsRegularConsignee.Value)), false);

            if (ds.Tables[0].Rows.Count > 0)
            {
                SetConsingee(ds.Tables[0].Rows[0]["Client_Name"].ToString().ToUpper(), ds.Tables[0].Rows[0]["Client_Id"].ToString().ToUpper() + "," + Is_RegularConsignor.ToString());

                ConsigneeAddressLine1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                ConsigneeAddressLine2 = ds.Tables[0].Rows[0]["Address2"].ToString();

                ConsigneeDDAddressLine1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                ConsigneeDDAddressLine2 = ds.Tables[0].Rows[0]["Address2"].ToString();

                ConsigneeCity = ds.Tables[0].Rows[0]["City_Name"].ToString();
                ConsigneePinCode = ds.Tables[0].Rows[0]["Pin_Code"].ToString();
                ConsigneeTelNo = ds.Tables[0].Rows[0]["Phone1"].ToString();
                ConsigneeMobileNo = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                ConsigneeEmail = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                ConsigneeCSTNo = ds.Tables[0].Rows[0]["CST_TIN_No"].ToString();

                ConsigneeCityId = Util.String2Int(ds.Tables[0].Rows[0]["city_id"].ToString());
                ConsigneeStateId = Util.String2Int(ds.Tables[0].Rows[0]["State_ID"].ToString());
                ConsigneeCountryId = Util.String2Int(ds.Tables[0].Rows[0]["Country_ID"].ToString());

                ConsigneeStateName = ds.Tables[0].Rows[0]["State_Name"].ToString();
                ConsigneeCountryName = ds.Tables[0].Rows[0]["Country_Name"].ToString();

                if (Util.String2Bool(ds.Tables[0].Rows[0]["Is_Service_Tax_Applicable"].ToString()))// && PaymentTypeId == 1)
                {
                    Is_ServiceTaxApplicableForConsignee = 1;
                }
                else
                {
                    Is_ServiceTaxApplicableForConsignee = 0;
                }

                Session_ConsigneeAddressLine1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                Session_ConsigneeAddressLine2 = ds.Tables[0].Rows[0]["Address2"].ToString();
                Session_ConsigneeName = ds.Tables[0].Rows[0]["Client_Name"].ToString();
                Session_ConsigneeActualAddressLine1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                Session_ConsigneeActualAddressLine2 = ds.Tables[0].Rows[0]["Address2"].ToString();
                Session_ConsigneeId = Util.String2Int(hdn_ConsigneeId.Value);
                
                ConsigneeDetails = ConsigneeAddressLine1 + ", " + ConsigneeAddressLine2 + ", " +
                                   ConsigneeCity + ", " + ConsigneePinCode + ", " + ConsigneeStateName + ", " +
                                   " Ph : " + ConsigneeTelNo;
            }
        }
        else
        {
            hdn_ConsigneeId.Value = "0";
            hdn_IsRegularConsignee.Value = "0";
            EncreptedConsigneeId = Util.EncryptInteger(ConsigneeId);
            ConsigneeId = 0;
            Is_RegularConsignee = 0;

            ConsigneeAddressLine1 = "";
            ConsigneeAddressLine2 = "";
            ConsigneeCity = "";
            ConsigneePinCode = "";
            ConsigneeTelNo = "";
            ConsigneeMobileNo = "";
            ConsigneeEmail = "";
            ConsigneeCSTNo = "";
            Is_ServiceTaxApplicableForConsignee = 0;

            Session_ConsigneeAddressLine1 = "";
            Session_ConsigneeAddressLine2 = "";
            Session_ConsigneeName = "";
            Session_ConsigneeActualAddressLine1 = "";
            Session_ConsigneeActualAddressLine2 = "";
            Session_ConsigneeId = 0;
            
            ConsigneeDetails = "";

        }
        On_PaymentTypeChange();

        ChequeDate = Convert.ToDateTime(hdn_ChequeDate.Value);
        upd_tbl_Charges.Update();

        SM_ShortGC.SetFocus(ddl_Consignee);
    } 

    protected void ddl_ContractBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BillingParty.OtherColumns = ddl_ContractBranch.SelectedValue;
        hdn_ContractBranchId.Value = ddl_ContractBranch.SelectedValue;

        if (Contract_BranchId > 0)
        {
            objShortGCPresenter.Fill_Contract();
        }
        else
        {
            ddl_Contract.Items.Clear();
            ddl_Contract.Items.Insert(0, new ListItem("Select One", "0"));
            ContractId = 0;
            Is_POD = false;
        }

        Get_ContractDetails("");

        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_ContractBranch);
    }

    protected void ddl_BillingBranch_TxtChange(object sender, EventArgs e)
    {
        BillingBranchId = Util.String2Int(ddl_BillingBranch.SelectedValue);
        SM_ShortGC.SetFocus(ddl_BillingBranch);
    }
    protected void ddl_ContractualClient_TxtChange(object sender, EventArgs e)
    {
        if (ddl_ContractualClient.SelectedValue.Trim() != string.Empty)
        {
            Contractual_ClientId = Util.String2Int(ddl_ContractualClient.SelectedValue);
            Session_ContractualClientDetails = objShortGCPresenter.Get_Contractual_Client_Details();
            objShortGCPresenter.Fill_ContractBranches();
            objShortGCPresenter.Fill_Contract();
        }
        else
        {
            Contractual_ClientId = 0;

            ddl_ContractBranch.Items.Clear();
            ddl_ContractBranch.Items.Insert(0, new ListItem("Select One", "0"));
            Contract_BranchId = 0;
            ddl_Contract.Items.Clear();
            ddl_Contract.Items.Insert(0, new ListItem("Select One", "0"));
            ContractId = 0;
            Is_POD = false;
        }

        Get_ContractDetails("");
        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_ContractualClient);
    }

    protected void ddl_LoadingSuperVisor_TxtChange(object sender, EventArgs e)
    {
        hdn_LoadingSuperVisorId.Value = ddl_LoadingSuperVisor.SelectedValue;
        SM_ShortGC.SetFocus(ddl_LoadingSuperVisor);
    }

    protected void ddl_MarketingExecutive_TxtChange(object sender, EventArgs e)
    {
        hdn_MarketingExecutiveId.Value = ddl_MarketingExecutive.SelectedValue;
        SM_ShortGC.SetFocus(ddl_MarketingExecutive);
    }

    protected void ddl_Consignor_TxtChange(object sender, EventArgs e)
    {
        char[] _Separator ={ ',' };
        string[] _IdArray = new string[2];

        if (ddl_Consignor.SelectedValue.Trim() != string.Empty || Util.String2Int(hdn_ClientId.Value.Trim()) > 0)
        {
            _IdArray = ddl_Consignor.SelectedValue.Split(_Separator);

            if (Util.String2Int(hdn_ClientId.Value.Trim()) > 0)
            {
                hdn_ConsignorId.Value = hdn_ClientId.Value.Trim();
                hdn_IsRegularConsignor.Value = "1";

                ConsignorId = Util.String2Int(hdn_ClientId.Value.Trim());
                Is_RegularConsignor = 1;

                hdn_ClientId.Value = "0";
                hdn_Is_Consignor.Value = "0";
            }
            else
            {
                hdn_ConsignorId.Value = _IdArray[0];
                hdn_IsRegularConsignor.Value = _IdArray[1];

                ConsignorId = Util.String2Int(_IdArray[0]);
                Is_RegularConsignor = Util.String2Int(_IdArray[1]);
            }

            EncreptedConsignorId = Util.EncryptInteger(ConsignorId);

            ds = Get_ConsignorConsigneeDetails(Convert.ToInt32(hdn_ConsignorId.Value), Convert.ToBoolean(Convert.ToInt32(hdn_IsRegularConsignor.Value)), true);

            if (ds.Tables[0].Rows.Count > 0)
            {
                SetConsingor(ds.Tables[0].Rows[0]["Client_Name"].ToString().ToUpper(), ds.Tables[0].Rows[0]["Client_Id"].ToString().ToUpper() + "," + Is_RegularConsignor.ToString());
                ConsignorAddressLine1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                ConsignorAddressLine2 = ds.Tables[0].Rows[0]["Address2"].ToString();
                ConsignorCity = ds.Tables[0].Rows[0]["City_Name"].ToString();
                ConsignorPinCode = ds.Tables[0].Rows[0]["Pin_Code"].ToString();
                ConsignorTelNo = ds.Tables[0].Rows[0]["Phone1"].ToString();
                ConsignorMobileNo = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                ConsignorEmail = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                ConsignorCSTNo = ds.Tables[0].Rows[0]["CST_TIN_No"].ToString();
                ConsignorCityId = Util.String2Int(ds.Tables[0].Rows[0]["city_id"].ToString());
                ConsignorStateId = Util.String2Int(ds.Tables[0].Rows[0]["State_ID"].ToString());
                ConsignorCountryId = Util.String2Int(ds.Tables[0].Rows[0]["Country_ID"].ToString());
                ConsignorStateName = ds.Tables[0].Rows[0]["State_Name"].ToString();
                ConsignorCountryName = ds.Tables[0].Rows[0]["Country_Name"].ToString();

                if (Util.String2Bool(ds.Tables[0].Rows[0]["Is_Service_Tax_Applicable"].ToString()))// && PaymentTypeId == 2)
                {
                    Is_ServiceTaxApplicableForConsignor = 1;
                }
                else
                {
                    Is_ServiceTaxApplicableForConsignor = 0;
                }

                ConsignorDetails = ConsignorAddressLine1 + ", " + ConsignorAddressLine2 + ", " +
                                 ConsignorCity + ", " + ConsignorPinCode + ", " + ConsignorStateName + ", " +
                                 " Ph : " + ConsignorTelNo;
            }
        }
        else
        {
            hdn_ConsignorId.Value = "0";
            EncreptedConsignorId = Util.EncryptInteger(ConsignorId);
            hdn_IsRegularConsignor.Value = "0";
            ConsignorId = 0;
            Is_RegularConsignor = 0;
            ConsignorAddressLine1 = "";
            ConsignorAddressLine2 = "";
            ConsignorCity = "";
            ConsignorPinCode = "";
            ConsignorTelNo = "";
            ConsignorMobileNo = "";
            ConsignorEmail = "";
            ConsignorCSTNo = "";
            Is_ServiceTaxApplicableForConsignor = 0;
            ConsignorDetails = "";
        }
        On_PaymentTypeChange();
        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_Consignor);
    }

    protected void ddl_FromLocation_TxtChange(object sender, EventArgs e)
    {
        hdn_FromLocationId.Value = ddl_FromLocation.SelectedValue;
        objShortGCPresenter.Get_BranchRateParameter();
        Get_StandardBasicFreight();
        On_PaymentTypeChange();
        upd_tbl_Charges.Update();
        SM_ShortGC.SetFocus(ddl_FromLocation);
    }

    protected void ddl_ArrivedFromBranch_TxtChange(object sender, EventArgs e)
    {
        hdn_ArrivedFromBranchId.Value = ddl_ArrivedFromBranch.SelectedValue;
        SM_ShortGC.SetFocus(ddl_ArrivedFromBranch);
    }

    protected void ddl_BookingBranch_TxtChange(object sender, EventArgs e)
    {
        BookingBranchId = Util.String2Int(ddl_BookingBranch.SelectedValue);
        ddl_FromLocation.OtherColumns = ddl_BookingBranch.SelectedValue;
        ddl_ToLocation.OtherColumns = "0";

        SetFromLocation("", "0");
        SM_ShortGC.SetFocus(ddl_BookingBranch);
    }

    protected void ddl_BillingParty_TxtChange(object sender, EventArgs e)
    {
        BillingPartyId = Util.String2Int(ddl_BillingParty.SelectedValue);
        Get_BillingPartyDetails();
        Calculate_GrandTotal();
        SM_ShortGC.SetFocus(ddl_BillingParty);
    }
    #endregion
}