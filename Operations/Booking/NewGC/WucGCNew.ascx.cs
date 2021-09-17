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
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using ClassLibraryMVP.Security;
using ClassLibraryMVP;
using Raj.EC;

public partial class Operations_Booking_WucGCNew : System.Web.UI.UserControl, INewGCView
{
    public NewGCPresenter objNewGCPresenter;
    public Common CommonObj = new Common();
    PageControls pc = new PageControls();
    string _flag;

    # region Set Controls
    public string GC_No_For_Print
    {
        set
        {
            if (MenuItemId == 200 && ClientCode.ToLower() == "nandwana" && keyID > 0)
            {
                char[] _Separator ={ '-' };
                string[] _IdArray = new string[2];

                _IdArray = value.Split(_Separator);

                ddl_GC_No_For_Print = _IdArray[0];
                txt_GC_No.Text = _IdArray[1].ToString();
                hdn_GC_No.Value = _IdArray[1].ToString();
            }
            else
            {
                ddl_GC_No_For_Print = value;
                txt_GC_No.Text = value;
                hdn_GC_No.Value = value;
            }
        }
        get { return hdn_GC_No.Value.Trim(); }
    }
    public string ddl_GC_No_For_Print
    {
        set
        {
            if (MenuItemId == 200 && ClientCode.ToLower() == "nandwana")
                ddl_GC_No.SelectedValue = value;
        }
        get
        {
            if (MenuItemId == 200 && ClientCode.ToLower() == "nandwana")
                return ddl_GC_No.SelectedValue.Trim() + "-" + GC_No_For_Print.Trim();
            else
                return GC_No_For_Print.Trim();
        }
    }
    public string CustomerRefNo
    {
        set { txt_CustomerRefNo.Text = value; }
        get { return txt_CustomerRefNo.Text.Trim(); }
    }
    public DateTime BookingDate
    {
        set { wuc_BookingDate.SelectedDate = value; }
        get { return wuc_BookingDate.SelectedDate; }
    }
    public string BookingTime
    {
        set { wuc_BookingTime.setTime(value); }
        get { return wuc_BookingTime.getTime(); }
    }
    public DateTime ChequeDate
    {
        set { wuc_ChequeDate.SelectedDate = value; }
        get { return wuc_ChequeDate.SelectedDate; }
    }
    public DateTime ArrivedDate
    {
        set { Wuc_Arrived_Date.SelectedDate = value; }
        get { return Wuc_Arrived_Date.SelectedDate; }
    }
    public DateTime ExpectedDeliveryDate
    {
        set
        {
            lbl_ExpDel_Date_Value.Text = value.ToString("dd-MM-yyyy");
            hdn_dly_date.Value = value.ToString("dd-MM-yyyy");
        }
        get { return Convert.ToDateTime(hdn_dly_date.Value); }
    }
    public int AgencyId
    {
        set { hdn_AgencyId.Value = Util.Int2String(value); }
        get { return hdn_AgencyId.Value == string.Empty ? 0 : Util.String2Int(hdn_AgencyId.Value); }
    }
    public string AgencyName
    {
        set { txt_Agency.Text = value; }
    }
    public int AgencyLedgerId
    {
        set { hdn_LedgerId.Value = Util.Int2String(value); }
        get { return hdn_LedgerId.Value == string.Empty ? 0 : Util.String2Int(hdn_LedgerId.Value); }
    }
    public string AgencyLedger
    {
        set
        {
            txt_Ledger.Text = value;
            hdn_LedgerName.Value = value;
        }
    }
    public string Agency_GC_No
    {
        set { txt_Agency_GCNo.Text = value; }
        get { return txt_Agency_GCNo.Text; }
    }
    public int CRMPickupRequestId
    {
        set { hdnPickupReqId.Value = Util.Int2String(value); }
        get { return Util.String2Int(ddl_pick_request.SelectedValue); }
    }
    private int SetCRMPickupRequestId
    {
        set { ddl_pick_request.SelectedValue = Util.Int2String(value); }
    }
    public int BookingBranchId
    {
        set { hdn_BookingBranchId.Value = Util.Int2String(value); }
        get { return hdn_BookingBranchId.Value == string.Empty ? 0 : Util.String2Int(hdn_BookingBranchId.Value); }
    }
    public string BookingBranch
    {
        set { txt_Booking_Branch.Text = value; }
    }
    public int ArrivedFromBranchId
    {
        set { hdn_ArrivedFromBranchId.Value = Util.Int2String(value); }
        get { return hdn_ArrivedFromBranchId.Value == string.Empty ? 0 : Util.String2Int(hdn_ArrivedFromBranchId.Value); }
    }
    public string ArrivedFromBranch
    {
        set { txt_ArrivedFrom_Branch.Text = value; }
    }
    public bool Is_POD
    {
        set { chk_PodRequire.Checked = value; }
        get { return chk_PodRequire.Checked; }
    }
    public bool Is_SignedByConsignor
    {
        set { chk_SignedByConsignor.Checked = value; }
        get { return chk_SignedByConsignor.Checked; }
    }
    public bool Is_Insured
    {
        set
        {
            chk_IsInsured.Checked = value;
            Is_InsuranceDetails_Filled = value;
        }
        get { return chk_IsInsured.Checked; }
    }
    public bool Is_OctroiApplicable
    {
        set
        {
            chk_Is_Oct_Appl.Checked = value;
            hdn_Is_Oct_Appl.Value = value == true ? "1" : "0";
        }
        get { return chk_Is_Oct_Appl.Checked; }
    }
    public bool Is_Attached
    {
        set
        {
            chk_IsAttached.Checked = value;
            hdn_IsAttached.Value = value == true ? "1" : "0";
        }
        get { return chk_IsAttached.Checked; }
    }
    public int Attached_GC_Id
    {
        set { hdn_AttachedGCId.Value = Util.Int2String(value); }
        get { return hdn_AttachedGCId.Value == string.Empty ? 0 : Util.String2Int(hdn_AttachedGCId.Value); }
    }
    public string Attached_GC_No
    {
        set { txt_Attached_GC_No.Text = value; }
        get { return txt_Attached_GC_No.Text; }
    }
    public int FromLocationId
    {
        set { hdn_FromLocationId.Value = Util.Int2String(value); }
        get { return hdn_FromLocationId.Value == string.Empty ? 0 : Util.String2Int(hdn_FromLocationId.Value); }
    }
    public string FromLocation
    {
        set { txt_From_Location.Text = value; }
    }
    public int ToLocationId
    {
        set { hdn_ToLocationId.Value = Util.Int2String(value); }
        get { return hdn_ToLocationId.Value == string.Empty ? 0 : Util.String2Int(hdn_ToLocationId.Value); }
    }
    public string ToLocation
    {
        set { txt_To_Location.Text = value; }
    }
    public int DeliveryBranchId
    {
        set { hdn_DeliveryBranchId.Value = Util.Int2String(value); }
        get { return hdn_DeliveryBranchId.Value == string.Empty ? 0 : Util.String2Int(hdn_DeliveryBranchId.Value); }
    }
    public string DeliveryBranchName
    {
        set
        {
            lbl_Dly_Branch_Value.Text = value;
            hdn_DeliveryBranchName.Value = value;
        }
    }
    public int PickupTypeId
    {
        set { ddl_Pickup_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Pickup_Type.SelectedValue); }
    }
    public int DeliveryWayTypeId
    {
        set
        {
            if (value > 0)
                ddl_DelWay_Type.SelectedValue = Util.Int2String(value);
        }
        get { return Util.String2Int(ddl_DelWay_Type.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_DelWay_Type.SelectedValue); }
    }
    public int BookingTypeId
    {
        set
        {
            ddl_Booking_Type.SelectedValue = Util.Int2String(value);
            hdn_Booking_Type.Value = value.ToString();
            if (ClientCode == "nandwana" && value == 5)
                Is_ContainerDetails_Filled = 1;
        }
        get { return Util.String2Int(hdn_Booking_Type.Value); }
        //get { return Util.String2Int(ddl_Booking_Type.SelectedValue); }
    }
    public int BookingSubTypeId
    {
        set
        {
            if (value > 0)
                ddl_booking_Sub_Type.SelectedValue = Util.Int2String(value);
        }
        get { return Util.String2Int(ddl_booking_Sub_Type.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_booking_Sub_Type.SelectedValue); }
    }
    public int BookingModeId
    {
        set { }
        get { return 1; } // surface..
    }
    public int DeliveryTypeId
    {
        set 
        {
            ddl_Dly_Type.SelectedValue = Util.Int2String(value);
            hdn_Dly_Type.Value = value.ToString();
        }
        get { return Util.String2Int(hdn_Dly_Type.Value); }
        //get { return Util.String2Int(ddl_Dly_Type.SelectedValue); }
    }
    public int ConsignmentTypeId
    {
        set { ddl_Consignment_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Consignment_Type.SelectedValue); }
    }
    public int DeliveryAgainstId
    {
        set
        {
            if (value > 0)
                ddl_Delivery_Against.SelectedValue = Util.Int2String(value);
        }
        get { return Util.String2Int(ddl_Delivery_Against.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_Delivery_Against.SelectedValue); }
    }
    public int VehicleTypeId
    {
        set
        {
            if (value > 0)
                ddl_VehicleType.SelectedValue = Util.Int2String(value);
        }
        get { return Util.String2Int(ddl_VehicleType.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_VehicleType.SelectedValue); }
    }
    public int RoadPermitTypeId
    {
        set
        {
            if (value > 0)
            {
                ddl_Road_Permit.SelectedValue = Util.Int2String(value);
                hdn_RoadPermitTypeId.Value = Util.Int2String(value);
            }
        }
        get { return Util.String2Int(ddl_Road_Permit.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_Road_Permit.SelectedValue); }
    }
    public int PaymentTypeId
    {
        set
        {
            ddl_PaymentType.SelectedValue = Util.Int2String(value);
            hdn_OldPaymentType.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_OldPaymentType.Value); }
    }

    public int ReasonFreightPendingId
    {
        set
        {
            ddl_Reason_Freight_Pending.SelectedValue = Util.Int2String(value);
        }
        get { return Util.String2Int(ddl_Reason_Freight_Pending.SelectedValue); }
    }

    public string PaidFreightPendingPersonName
    {
        set { txt_Paid_Freight_Pending_Person_Name.Text = value; }
        get { return txt_Paid_Freight_Pending_Person_Name.Text.Trim(); }
    }

    public string PaidFreightPendingPersonMobile
    {
        set { txt_Paid_Freight_Pending_Person_Mobile.Text = value; }
        get { return txt_Paid_Freight_Pending_Person_Mobile.Text.Trim(); }
    }

    public int GCRiskId
    {
        set { ddl_GCRisk.SelectedValue = Util.Int2String(value); }
        
        get 
        {
            return Util.String2Int(ddl_GCRisk.SelectedValue); 
            //                    sundry ? Carrier : Owner 
            //return BookingTypeId == 1 ? 2 : 1; 
        }
    }
    public bool Is_InsuranceDetails_Filled
    {
        set { hdn_Is_InsuranceDetails_Filled.Value = value == true ? "1" : "0"; }
        get { return hdn_Is_InsuranceDetails_Filled.Value == "1" ? true : false; }
    }
    public int Is_ContainerDetails_Filled
    {
        set { hdn_Is_ContainerDetails_Filled.Value = value.ToString(); }
        get { return hdn_Is_ContainerDetails_Filled.Value == string.Empty ? 0 : Util.String2Int(hdn_Is_ContainerDetails_Filled.Value); }
    }
    public int ServiceTaxPayableBy
    {
        set
        {
            ddl_ServiceTaxPayableBy.SelectedValue = value.ToString();
            hdn_ServiceTaxPayableBy.Value = value.ToString();
        }
        get { return Util.String2Int(hdn_ServiceTaxPayableBy.Value); }
    }
    public int LengthChargeHeadId
    {
        set { ddl_LengthChargeHead.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_LengthChargeHead.SelectedValue); }
    }
    public int UnitOfMeasurementId
    {
        set { ddl_UnitOfMeasurment.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_UnitOfMeasurment.SelectedValue); }
    }
    public int FreightBasisId
    {
        set
        {
            ddl_FreightBasis.SelectedValue = Util.Int2String(value);
            hdn_FreightBasisId.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_FreightBasisId.Value); }
    }

    public int defaultFreightBasisId
    {
        set { hdn_defaultFreightBasisId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_defaultFreightBasisId.Value); }
    }
    public int defaultConsignmentType
    {
        set { hdn_defaultConsignmentType.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_defaultConsignmentType.Value); }
    }
    public int defaultGCRiskType
    {
        set { hdn_Default_GCRisk.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Default_GCRisk.Value); }
    }
    public int VolumetricFreightUnitId
    {
        set
        {
            if (value > 0)
                ddl_VolumetricFreightUnit.SelectedValue = Util.Int2String(value);
        }
        get { return FreightBasisId == 4 ? Util.String2Int(ddl_VolumetricFreightUnit.SelectedValue) : 0; }
    }
    public int LoadingSuperVisorId
    {
        get { return Util.String2Int(ddl_LoadingSuperVisor.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_LoadingSuperVisor.SelectedValue); }
    }
    public int MarketingExecutiveId
    {
        get { return Util.String2Int(ddl_MarketingExecutive.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_MarketingExecutive.SelectedValue); }
    }

    public bool Is_MultipleBilling
    {
        set { chk_IsMultipleBilling.Checked = value; }
        get { return chk_IsMultipleBilling.Checked; }
    }
    public int BillingPartyId
    {
        set { hdn_BillingPartyId.Value = Util.Int2String(value); }
        get { return hdn_BillingPartyId.Value == string.Empty ? 0 : Util.String2Int(hdn_BillingPartyId.Value); }
    }
    public string BillingParty
    {
        set
        {
            txt_BillingParty.Text = value.Trim();
            hdn_BillingParty.Value = value.Trim();
        }
    }
    public string BillingHierarchy
    {
        set
        {
            ddl_BillingHierarchy.SelectedValue = value;
            hdn_BillingHierarchy.Value = value.Trim();
        }
        get
        {
            return hdn_BillingHierarchy.Value;
        }
    }
    public int BillingLocationId
    {
        set { hdn_BillingLocationId.Value = Util.Int2String(value); }
        get { return hdn_BillingLocationId.Value == string.Empty ? 0 : Util.String2Int(hdn_BillingLocationId.Value); }
    }
    public string BillingLocation
    {
        set
        {
            txt_BillingLocation.Text = value.Trim();
            hdn_BillingLocation.Value = value.Trim();
        }
    }
    public string BillingRemark
    {
        set
        {
            txt_BillingRemark.Text = value.Trim();
            hdn_BillingRemark.Value = value.Trim();
        }
        get { return hdn_BillingRemark.Value.Trim(); }
    }

    public int WholeselerId
    {
        set { hdn_WholeselerId.Value = Util.Int2String(value); }
        get { return hdn_WholeselerId.Value == string.Empty ? 0 : Util.String2Int(hdn_WholeselerId.Value); }
    }

    public string Wholeseler
    {
        set
        {
            txt_Wholeseler.Text = value.Trim();
            hdn_Wholeseler.Value = value.Trim();
        }
    }

    public int ConsignorId
    {
        set { hdn_ConsignorId.Value = Util.Int2String(value); }
        get { return hdn_ConsignorId.Value == string.Empty ? 0 : Util.String2Int(hdn_ConsignorId.Value); }
    }
    public string ConsignorName
    {
        set { txt_ConsignorName.Text = value; }
    }
    public string ConsignorAddressValue
    {
        set
        {
            lbl_ConsignorDetailsValue.Text = value;
            txt_ConsignorDetailsValue.Text = value;
            hdn_ConsignorDetailsValue.Value = value;
        }
    }
    public string ConsignorPhoneNumbers
    {
        set { lbl_ConsignorPhoneNumbers.Text = value; }
    }
    public string ConsigneePhoneNumbers
    {
        set { lbl_ConsigneePhoneNumbers.Text = value; }
    }
    public string ConsignorMobileNumbers
    {
        set { lbl_ConsignorMobileNumbers.Text = value; }
    }
    public string ConsigneeMobileNumbers
    {
        set { lbl_ConsigneeMobileNumbers.Text = value; }
    }
    public bool Is_ServiceTaxApplicableForConsignor
    {
        set { hdn_IsServiceTaxApplicableForConsignor.Value = value == true ? "1" : "0"; }
        get { return hdn_IsServiceTaxApplicableForConsignor.Value == "1" ? true : false; }
    }
    public bool Is_RegularConsignor
    {
        set { hdn_IsRegularConsignor.Value = value == true ? "1" : "0"; }
        get { return hdn_IsRegularConsignor.Value == "1" ? true : false; }
    }
    public int ConsignorStateId
    {
        set { hdn_ConsignorStateId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ConsignorStateId.Value); }
    }
    public int ConsigneeId
    {
        set { hdn_ConsigneeId.Value = Util.Int2String(value); }
        get { return hdn_ConsigneeId.Value == string.Empty ? 0 : Util.String2Int(hdn_ConsigneeId.Value); }
    }
    public string ConsigneeName
    {
        set { txt_ConsigneeName.Text = value; }
    }
    public string Consignee_CSTTINNo
    {
        set { hdn_Consignee_CSTTINNo.Value = value; }
    }
    public string ConsigneeAddressValue
    {
        set
        {
            lbl_ConsigneeDetailsValue.Text = value;
            txt_ConsigneeDetailsValue.Text = value;
            hdn_ConsigneeDetailsValue.Value = value;
        }
    }
    public bool Is_ServiceTaxApplicableForConsignee
    {
        set { hdn_IsServiceTaxApplicableForConsignee.Value = value == true ? "1" : "0"; }
        get { return hdn_IsServiceTaxApplicableForConsignee.Value == "1" ? true : false; }
    }
    public bool Is_RegularConsignee
    {
        set { hdn_IsRegularConsignee.Value = value == true ? "1" : "0"; }
        get { return hdn_IsRegularConsignee.Value == "1" ? true : false; }
    }
    public int ConsigneeStateId
    {
        set { hdn_ConsigneeStateId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ConsigneeStateId.Value); }
    }
    public string ConsigneeDDAddress1
    {
        set { hdn_ConsigneeDDAddressLine1.Value = value; }
        get { return hdn_ConsigneeDDAddressLine1.Value; }
    }
    public string ConsigneeDDAddress2
    {
        set { hdn_ConsigneeDDAddressLine2.Value = value; }
        get { return hdn_ConsigneeDDAddressLine2.Value; }
    }
    public string VehicleNo
    {
        set { txt_VehicleNo.Text = value; }
        get { return txt_VehicleNo.Text.Trim(); }
    }
    public string FeasibilityRouteSurveyNo
    {
        set { txt_FeasibilityAndRouteSurveyNo.Text = value; }
        get { return txt_FeasibilityAndRouteSurveyNo.Text.Trim(); }
    }
    public string STMNo
    {
        set { txt_STM_No.Text = value; }
        get { return txt_STM_No.Text.Trim(); }
    }
    public string RoadPermitSrNo
    {
        set { txt_Road_Permit_SrNo.Text = value; }
        get { return txt_Road_Permit_SrNo.Text.Trim(); }
    }
    public string PrivateMark
    {
        set
        {
            txt_Private_Mark.Text = value;
            hdn_Private_Mark.Value = value;
        }
        get { return hdn_Private_Mark.Value.Trim(); }
    }
    public string InstructionRemark
    {
        set { txt_GC_Remarks.Text = value; }
        get { return txt_GC_Remarks.Text.Trim(); }
    }
    public string OtherChargesRemark
    {
        set { txt_OtherChargesRemark.Text = value; }
        get { return txt_OtherChargesRemark.Text.Trim(); }
    }
    public string Enclosures
    {
        set { txt_Enclosure.Text = value; }
        get { return txt_Enclosure.Text.Trim(); }
    }
    public bool Is_Cheque
    {
        get { return ChequeAmount > 0 ? true : false; }
    }
    public string ChequeNo
    {
        set { txt_ChequeNo.Text = value; }
        get { return txt_ChequeNo.Text.Trim() == string.Empty ? "0" : txt_ChequeNo.Text.Trim(); }
    }
    public string BankName
    {
        set { txt_BankName.Text = value; }
        get { return txt_BankName.Text.Trim(); }
    }
    public string eWayBillNo
    {
        set { txt_eWayBillNo.Text = value; }
        get { return txt_eWayBillNo.Text.Trim(); }
    }
    public bool Is_MultipleeWayBill
    {
        set { chk_IsMultipleeWayBill.Checked = value; }
        get { return chk_IsMultipleeWayBill.Checked; }
    }
    public decimal TotalLength
    {
        set { hdn_TotalLength.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_TotalLength.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalLength.Value); }
    }
    public decimal TotalWidth
    {
        set { hdn_TotalWidth.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_TotalWidth.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalWidth.Value); }
    }
    public decimal TotalHeight
    {
        set { hdn_TotalHeight.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_TotalHeight.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalHeight.Value); }
    }
    public decimal ItemValueForFOV
    {
        set { hdn_ItemValueForFOV.Value = Util.Decimal2String(value); }
        get { return hdn_ItemValueForFOV.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ItemValueForFOV.Value); }
    }
    public int TotalArticles
    {
        set { hdn_TotalArticles.Value = Util.Int2String(value); }
        get { return hdn_TotalArticles.Value == string.Empty ? 0 : Util.String2Int(hdn_TotalArticles.Value); }
    }
    public decimal TotalWeight
    {
        set { hdn_TotalWeight.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_TotalWeight.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalWeight.Value); }
    }
    public decimal ActualWeight
    {
        set { txt_ActualWeight.Text = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(txt_ActualWeight.Text); }
    }
    public decimal ChargeWeight
    {
        set
        {
            txt_ChargeWeight.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_ChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_ChargeWeight.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ChargeWeight.Value); }
    }
    public decimal UnitOfMeasurmentHeight
    {
        set
        {
            txt_UnitOfMeasurmentHeight.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnitOfMeasurmentHeight.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_UnitOfMeasurmentHeight.Value == string.Empty ? 0 : Util.String2Decimal(hdn_UnitOfMeasurmentHeight.Value); }
    }
    public decimal UnitOfMeasurmentLength
    {
        set
        {
            txt_UnitOfMeasurmentLength.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnitOfMeasurmentLength.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_UnitOfMeasurmentLength.Value == string.Empty ? 0 : Util.String2Decimal(hdn_UnitOfMeasurmentLength.Value); }
    }
    public decimal UnitOfMeasurmentWidth
    {
        set
        {
            txt_UnitOfMeasurmentWidth.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnitOfMeasurmentWidth.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_UnitOfMeasurmentWidth.Value == string.Empty ? 0 : Util.String2Decimal(hdn_UnitOfMeasurmentWidth.Value); }
    }
    public decimal HeightInFeet
    {
        set
        {
            lbl_HeightInFeetValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_HeightInFeet.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_HeightInFeet.Value == string.Empty ? 0 : Util.String2Decimal(hdn_HeightInFeet.Value); }
    }
    public decimal LengthInFeet
    {
        set
        {
            lbl_LengthInFeetValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LengthInFeet.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LengthInFeet.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LengthInFeet.Value); }
    }
    public decimal WidthInFeet
    {
        set
        {
            lbl_WidthInFeetValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_WidthInFeet.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_WidthInFeet.Value == string.Empty ? 0 : Util.String2Decimal(hdn_WidthInFeet.Value); }
    }
    public decimal TotalCFT
    {
        set
        {
            lbl_TotalCFTValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_TotalCFT.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_TotalCFT.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalCFT.Value); }
    }

    public decimal TotalCBM
    {
        set
        {
            lbl_TotalCBMValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_TotalCBM.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_TotalCBM.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalCBM.Value); }
    }
    public decimal VolumetricToKgFactor
    {
        set
        {
            txt_VolumetricToKgFactor.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_VolumetricToKgFactor.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_VolumetricToKgFactor.Value == string.Empty ? 0 : Util.String2Decimal(hdn_VolumetricToKgFactor.Value); }
    }
    public decimal FreightRate
    {
        set
        {
            txt_FreightRate.Text = Util.Decimal2String(value);
            hdn_FreightRate.Value = Util.Decimal2String(value);
        }
        get { return hdn_FreightRate.Value == string.Empty ? 0 : Util.String2Decimal(hdn_FreightRate.Value); }
    }
    public decimal Freight
    {
        set
        {
            txt_Freight.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Freight.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_Freight.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Freight.Value); }
    }
    public decimal LocalCharge
    {
        set
        {
            txt_LocalCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LocalCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LocalCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LocalCharge.Value); }
    }
    public decimal LoadingCharge
    {
        set
        {
            txt_LoadingCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LoadingCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LoadingCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LoadingCharge.Value); }
    }
    public decimal StationaryCharge
    {
        set
        {
            txt_StationaryCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_StationaryCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_StationaryCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_StationaryCharge.Value); }
    }
    public decimal FOVRiskCharge
    {
        set
        {
            txt_FOVRiskCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_FOVRiskCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_FOVRiskCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_FOVRiskCharge.Value); }
    }
    public decimal ToPayCharge
    {
        set
        {
            txt_ToPayCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_ToPayCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_ToPayCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ToPayCharge.Value); }
    }
    public decimal DDCharge
    {
        set
        {
            txt_DDCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_DDCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_DDCharge.Value); }
    }
    public decimal DACCCharges
    {
        set
        {
            txt_DACCCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_DACCCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_DACCCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_DACCCharge.Value); }
    }
    public decimal OtherCharges
    {
        set
        {
            lbl_OtherChargesValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_OtherCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_OtherCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_OtherCharge.Value); }
    }
    public decimal NFormCharge
    {
        set
        {
            txt_NFormCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_NFormCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_NFormCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_NFormCharge.Value); }
    }
    public decimal ReBookGCCharges
    {
        set
        {
            txt_ReBookGCAmount.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_ReBookGCAmount.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_ReBookGCAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ReBookGCAmount.Value); }
    }
    public decimal LengthCharge
    {
        set
        {
            txt_LengthCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_LengthCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_LengthCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LengthCharge.Value); }
    }

    public decimal UnloadingCharge
    {
        set
        {
            txt_UnloadingCharge.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_UnloadingCharge.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_UnloadingCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_UnloadingCharge.Value); }
    }

    public decimal AOCPercent
    {
        set
        {
            hdn_AOCPercent.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_AOCPercent.Value == string.Empty ? 0 : Util.String2Decimal(hdn_AOCPercent.Value); }
    }

    public decimal AOC
    {
        set
        {
            txt_AOC.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_AOC.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_AOC.Value == string.Empty ? 0 : Util.String2Decimal(hdn_AOC.Value); }
    }

    public int RoundOff
    {
        set
        {
            lbl_RoundOff.Text = Util.Int2String(value);
            hdn_RoundOff.Value = Util.Int2String(value);
        }
        get { return hdn_RoundOff.Value == string.Empty ? 0 : Util.String2Int(hdn_RoundOff.Value); }
    }

    public decimal Discount
    {
        set
        {
            txt_Discount.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_Discount.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_Discount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Discount.Value); }
    }

    public int DiscountId
    {
        set { hdn_DiscountId.Value = value.ToString(); }
        get { return Util.String2Int(hdn_DiscountId.Value) <= 0 ? 0 : Util.String2Int(hdn_DiscountId.Value); }
    }


    public int ConsigneeDeliveryAreaID
    {
        set { hdn_ConsigneeDeliveryAreaID.Value = value.ToString(); }
        get { return Util.String2Int(hdn_ConsigneeDeliveryAreaID.Value) <= 0 ? 0 : Util.String2Int(hdn_ConsigneeDeliveryAreaID.Value); }
    }
    public string ConsigneeDeliveryAreaName
    {
        set { lbl_ConsigneeDeliveryAreaName.Text = value.ToString(); } 
    }

    public decimal ODACharges
    {
        set
        {
            txt_ODACharge.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_ODACharge.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_ODACharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ODACharge.Value); }
    }

    public decimal SubTotal
    {
        set
        {
            lbl_SubTotalValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_SubTotal.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_SubTotal.Value == string.Empty ? 0 : Util.String2Decimal(hdn_SubTotal.Value); }
    }
    public decimal TaxAbatePercent
    {
        set { }
        get { return hdn_AbatePercentage.Value == string.Empty ? 0 : Util.String2Decimal(hdn_AbatePercentage.Value); }
        //get { return Util.String2Decimal("0.75"); }
    }
    public decimal Abatment
    {
        set
        {
            lbl_AbatmentValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_Abatment.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_Abatment.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Abatment.Value); }
    }

    public decimal TaxableAmount
    {
        set
        {
            lbl_TaxableAmountValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_TaxableAmount.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_TaxableAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TaxableAmount.Value); }
    }
    public string ServiceTax_Label
    {
        set { lbl_ServiceTax.Text = value; }
    }
    public decimal ServiceTax
    {
        set
        {
            lbl_ServiceTaxValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_ServiceTax.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_ServiceTax.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ServiceTax.Value); }
    }
    public decimal ActualServiceTax
    {
        set { hdn_Actual_ServiceTax.Value = Util.Decimal2String(Math.Round(value, 0)); }
        get { return hdn_Actual_ServiceTax.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Actual_ServiceTax.Value); }
    }
    public decimal ReBookGC_OctroiAmount
    {
        set
        {
            lbl_ReBookOctroiAmountValue.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_ReBookGC_OctroiAmount.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_ReBookGC_OctroiAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ReBookGC_OctroiAmount.Value); }
    }

    public decimal TotalGCAmount
    {
        set
        {
            lbl_TotalGCAmountValue.Text = Util.Decimal2String(Math.Round(value, 0));
            hdn_TotalGCAmount.Value = Util.Decimal2String(Math.Round(value, 0));
        }
        get { return hdn_TotalGCAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalGCAmount.Value); }
    }
    public decimal Advance
    {
        set
        {
            txt_Advance.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_Advance.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_Advance.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Advance.Value); }
    }
    public decimal CashAmount
    {
        set
        {
            txt_CashAmount.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_CashAmount.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_CashAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_CashAmount.Value); }
    }
    public decimal ChequeAmount
    {
        set
        {
            txt_ChequeAmount.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_ChequeAmount.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_ChequeAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ChequeAmount.Value); }
    }
    public decimal TotalInvoiceAmount
    {
        set { hdn_TotalInvoiceAmount.Value = Util.Decimal2String(value); }
        get { return hdn_TotalInvoiceAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalInvoiceAmount.Value); }
    }
    public bool Is_Service_Tax_Payable_For_BillingParty
    {
        set { hdn_IsServiceTaxApplForBillParty.Value = value == true ? "1" : "0"; }
        get { return hdn_IsServiceTaxApplForBillParty.Value == "1" ? true : false; }
    }
    public bool Is_Service_Tax_Applicable_For_Commodity
    {
        set { hdn_Is_Service_Tax_Applicable_For_Commodity.Value = value == true ? "1" : "0"; }
        get { return hdn_Is_Service_Tax_Applicable_For_Commodity.Value == "1" ? true : false; }
    }
    public int Contractual_ClientId
    {
        set { hdn_ContractualClientId.Value = value.ToString(); }
        get { return Util.String2Int(hdn_ContractualClientId.Value) <= 0 ? 0 : Util.String2Int(hdn_ContractualClientId.Value); }
    }
    public string Contractual_Client
    {
        set
        {
            txt_ContractClient.Text = value.Trim();
            hdn_ContractClient.Value = value.Trim();
        }
        get { return hdn_ContractClient.Value.Trim(); }
    }
    public int Contract_BranchId
    {
        set
        {
            ddl_ContractBranch.SelectedValue = value.ToString();
            hdn_ContractBranchId.Value = value.ToString();
        }
        get { return Util.String2Int(hdn_ContractBranchId.Value) <= 0 ? 0 : Util.String2Int(hdn_ContractBranchId.Value); }
    }
    public int ContractId
    {
        set
        {
            ddl_Contract.SelectedValue = value.ToString();
            hdn_ContractId.Value = value.ToString();
        }
        get { return Util.String2Int(hdn_ContractId.Value) <= 0 ? 0 : Util.String2Int(hdn_ContractId.Value); }
    }
    public bool IsCC_PaidAllowed
    {
        set { hdn_Is_Paid_Allowed.Value = value.ToString().ToLower(); }
        get { return hdn_Is_Paid_Allowed.Value == "true" ? true : false; }
    }
    public bool IsCC_ToPayAllowed
    {
        set { hdn_Is_To_Pay_Allowed.Value = value.ToString().ToLower(); }
        get { return hdn_Is_To_Pay_Allowed.Value == "true" ? true : false; }
    }
    public bool IsCC_FOCAllowed
    {
        set { hdn_Is_FOC_Allowed.Value = value.ToString().ToLower(); }
        get { return hdn_Is_FOC_Allowed.Value == "true" ? true : false; }
    }
    public bool IsCC_TBBAllowed
    {
        set { hdn_Is_To_Be_Billed_Allowed.Value = value.ToString().ToLower(); }
        get { return hdn_Is_To_Be_Billed_Allowed.Value == "true" ? true : false; }
    }
    public int Is_ContractApplied
    {
        set { hdn_IsContractApplied.Value = value.ToString(); }
        get { return Util.String2Int(hdn_IsContractApplied.Value); }
    }
    public int RateContractId
    {
        set { hdn_RateContractId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_RateContractId.Value); }
    }

    #endregion

    #region Default GC Parameter Setting
    public string Flag
    {
        get { return _flag; }
    }
    public int GC_No_Length
    {
        set { hdn_GC_No_Length.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_GC_No_Length.Value); }
    }
    public bool Is_ToPay_Charge_Require
    {
        set { chk_Is_ToPay_Charge_Require.Checked = value; }
        get { return chk_Is_ToPay_Charge_Require.Checked; }
    }
    public bool Is_Multiple_Location_Billing_Allowed
    {
        set { chk_Is_Multiple_Location_Billing_Allowed.Checked = value; }
        get { return chk_Is_Multiple_Location_Billing_Allowed.Checked; }
    }
    public bool Is_Multiple_Party_Billing_Allowed
    {
        set { hdn_Is_Multiple_Party_Billing_Allowed.Value = value == true ? "1" : "0"; }
        get { return hdn_Is_Multiple_Party_Billing_Allowed.Value == "1" ? true : false; }
    }
    public bool Is_ToPayBookingApplicable
    {
        set { chk_IsToPayBkgApplicable.Checked = value; }
        get { return chk_IsToPayBkgApplicable.Checked; }
    }
    public bool Is_FOV_Calculated_As_Per_Standard
    {
        set { chk_Is_FOV_Calculated_As_Per_Standard.Checked = value; }
        get { return chk_Is_FOV_Calculated_As_Per_Standard.Checked; }
    }
    public bool Is_Auto_Booking_MR_For_Paid_Booking
    {
        set { chk_Is_Auto_Booking_MR_For_Paid_Booking.Checked = value; }
        get { return chk_Is_Auto_Booking_MR_For_Paid_Booking.Checked; }
    }
    public bool Is_DefaultPOD_Checked
    {
        set
        {
            hdn_defaultPod.Value = value == true ? "1" : "0";
            Is_POD = value;
        }
        get { return hdn_defaultPod.Value == "1" ? true : false; }
    }
    public bool Is_POD_Disabled
    {
        set { chk_PodRequire.Enabled = !value; }
    }
    public bool Is_Consignor_Consignee_Details_Shown
    {
        set { chk_Is_Consignor_Consignee_Details_Shown.Checked = value; }
        get { return chk_Is_Consignor_Consignee_Details_Shown.Checked; }
    }
    public int BillingParty_LedgerId
    {
        set { hdn_BillingParty_LedgerId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_BillingParty_LedgerId.Value); }
    }
    public decimal BillingParty_ClosingBalance
    {
        set { hdn_Billing_Party_ClosingBalance.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_ClosingBalance.Value); }
    }
    public decimal BillingParty_CreditLimit
    {
        set { hdn_Billing_Party_CreditLimit.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_CreditLimit.Value); }
    }
    public decimal BillingParty_MinimumBalance
    {
        set { hdn_Billing_Party_MinimumBalance.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_MinimumBalance.Value); }
    }
    public decimal BillingParty_Ledger_Closing_Balance
    {
        set { hdn_Billing_Party_Ledger_Closing_Balance.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_Ledger_Closing_Balance.Value); }
    }
    public int Valid_Cheque_Start_Days
    {
        set { hdn_Valid_Cheque_Start_Days.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Valid_Cheque_Start_Days.Value); }
    }
    public int Valid_Cheque_End_Days
    {
        set { hdn_Valid_Cheque_End_Days.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Valid_Cheque_End_Days.Value); }
    }
    public int Default_Cash_Ledger_Id
    {
        set { hdn_Default_Cash_Ledger_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Default_Cash_Ledger_Id.Value); }
    }
    public int Default_Bank_Ledger_Id
    {
        set { hdn_Default_Bank_Ledger_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Default_Bank_Ledger_Id.Value); }
    }
    public string Default_Cheque_Branch_Ledger_Name
    {
        set { hdn_Default_Cheque_Branch_Ledger_Name.Value = value; }
        get { return hdn_Default_Cheque_Branch_Ledger_Name.Value; }
    }
    public string Default_Cheque_Bank_Ledger_Name
    {
        set { hdn_Default_Cheque_Bank_Ledger_Name.Value = value; }
        get { return hdn_Default_Cheque_Bank_Ledger_Name.Value; }
    }
    public int DocumentSeriesAllocationId
    {
        set { hdn_DocumentSeriesAllocationID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_DocumentSeriesAllocationID.Value); }
    }
    public string No_For_Padd
    {
        set { hdn_No_For_Padd.Value = value; }
        get { return hdn_No_For_Padd.Value.Trim(); }
    }
    public int Next_No
    {
        set
        {
            hdn_Next_No.Value = Convert.ToInt32(value).ToString(No_For_Padd);
            GC_No_For_Print = Convert.ToInt32(value).ToString(No_For_Padd);
        }
        get { return Util.String2Int(hdn_Next_No.Value.Trim()); }
    }
    public int End_No
    {
        set { hdn_End_No.Value = Convert.ToInt32(value).ToString(No_For_Padd); }
        get { return Util.String2Int(hdn_End_No.Value.Trim()); }
    }
    public int Start_No
    {
        set { hdn_Start_No.Value = Convert.ToInt32(value).ToString(No_For_Padd); }
        get { return Util.String2Int(hdn_Start_No.Value.Trim()); }
    }
    public int VAId
    {
        set { hdn_VAId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_VAId.Value); }
    }
    public DateTime GCDate_ForRectify
    {
        set { hdn_GCDate_ForRectify.Value = value.ToString(); }
        get { return Convert.ToDateTime(hdn_GCDate_ForRectify.Value); }
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
    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }
    public DateTime ApplicationStartDate
    {
        set { wuc_ApplicationStartDate.SelectedDate = value; }
        get { return wuc_ApplicationStartDate.SelectedDate; }
    }
    public int ServiceTypeId
    {
        set { ddl_Service_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Service_Type.SelectedValue); }
    }
    public bool Is_ST_Abatment_Required
    {
        set { chk_Is_ST_Abatment_Required.Checked = Convert.ToBoolean(value); }
        get { return Convert.ToBoolean(chk_Is_ST_Abatment_Required.Checked); }
    }
    #endregion

    # region Set Company Parameter Details
    public bool Is_ODA
    {
        set { chk_IsODA.Checked = value; }
        get { return chk_IsODA.Checked; }
    }
    public decimal ODAChargesUpTo500Kg
    {
        set { hdn_ODAChargesUpTo500Kg.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_ODAChargesUpTo500Kg.Value); }
    }
    public decimal ODAChargesAbove500Kg
    {
        set { hdn_ODAChargesAbove500Kg.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_ODAChargesAbove500Kg.Value); }
    }
    public int CompanyParameter_Standard_BasicFreightUnitId
    {
        set { hdn_CompanyParameter_Standard_BasicFreightUnitId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_CompanyParameter_Standard_BasicFreightUnitId.Value); }
    }
    public decimal CompanyParameter_Standard_FreightRatePer
    {
        set { hdn_CompanyParameter_Standard_FreightRatePer.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_CompanyParameter_Standard_FreightRatePer.Value); }
    }
    public bool Is_GCNumberEditable
    {
        set { chk_Is_GCNumberEditable.Checked = value; }
        get { return chk_Is_GCNumberEditable.Checked; }
    }
    public bool Is_Contract_Required_For_TBB_GC
    {
        set { chk_Is_Contract_Required_For_TBB_GC.Checked = value; }
        get { return chk_Is_Contract_Required_For_TBB_GC.Checked; }
    }
    public bool Is_Invoice_Amount_Required
    {
        set { chk_Is_Invoice_Amount_Required.Checked = value; }
        get { return chk_Is_Invoice_Amount_Required.Checked; }
    }
    public bool Is_Item_Required
    {
        set { chk_Is_Item_Required.Checked = value; }
        get { return chk_Is_Item_Required.Checked; }
    }
    public bool Is_Validate_Credit_Limit
    {
        set { chk_Is_Validate_Credit_Limit.Checked = value; }
        get { return chk_Is_Validate_Credit_Limit.Checked; }
    }
    public string ClientCode
    {
        set { hdn_ClientCode.Value = value.ToLower(); }
        get { return hdn_ClientCode.Value.Trim().ToLower(); }
    }
    public string GCCaption
    {
        set { hdn_GCCaption.Value = value.ToUpper(); }
        get { return hdn_GCCaption.Value.ToUpper(); }
    }
    public string LoadingSuperVisor_RequiredFor_BookingType
    {
        set { hdn_LoadingSuperVisor_RequiredFor_BookingType.Value = value; }
        get { return hdn_LoadingSuperVisor_RequiredFor_BookingType.Value; }
    }
    public string Container_Details_RequiredFor_BookingType
    {
        set { hdn_Container_Details_RequiredFor_BookingType.Value = value; }
        get { return hdn_Container_Details_RequiredFor_BookingType.Value; }
    }
    # endregion
    # region Set Branch Rate Card Details

    public decimal RateCard_MinimumChargeWeight
    {
        set { hdn_RateCard_MinimumChargeWeight.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_MinimumChargeWeight.Value); }
    }
    public decimal RateCard_BiltiCharges
    {
        set { hdn_RateCard_BiltiCharges.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_BiltiCharges.Value); }
    }
    public decimal RateCard_MaxBiltyCharge
    {
        set { hdn_RateCard_MaxBiltiCharges.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_MaxBiltiCharges.Value); }
    }
    public decimal RateCard_FOV
    {
        set { hdn_RateCard_FOV.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_FOV.Value); }
    }
    public decimal RateCard_FOVPercentage
    {
        set { hdn_RateCard_FOVPercentage.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_FOVPercentage.Value); }
    }
    public decimal RateCard_FOVRate
    {
        set { hdn_RateCard_FOVRate.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_FOVRate.Value); }
    }
    public decimal RateCard_Fov_Charge_Discount_Percent
    {
        set { hdn_RateCard_Fov_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Fov_Charge_Discount_Percent.Value); }
    }
    public decimal RateCard_ToPayCharges
    {
        set { hdn_RateCard_ToPayCharges.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_ToPayCharges.Value); }
    }
    public decimal RateCard_DACCCharges
    {
        set { hdn_RateCard_DACCCharges.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_DACCCharges.Value); }
    }
    public decimal RateCard_LocalCharge
    {
        set { hdn_RateCard_LocalCharge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_LocalCharge.Value); }
    }
    public decimal RateCard_HamaliCharge
    {
        set { hdn_RateCard_HamaliCharge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_HamaliCharge.Value); }
    }
    public decimal RateCard_HamaliPerKg
    {
        set { hdn_RateCard_HamaliPerKg.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_HamaliPerKg.Value); }
    }
    public decimal RateCard_HamaliPerArticles
    {
        set { hdn_RateCard_HamaliPerArticles.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_HamaliPerArticles.Value); }
    }
    public decimal RateCard_Hamali_Charge_Discount_Percent
    {
        set { hdn_RateCard_Hamali_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Hamali_Charge_Discount_Percent.Value); }
    }
    public decimal RateCard_DDCharge_Rate
    {
        set { hdn_RateCard_DDCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_DDCharge_Rate.Value); }
    }
    public decimal RateCard_DDCharge
    {
        set { hdn_RateCard_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_DDCharge.Value); }
    }
    public decimal RateCard_DD_Charge_Discount_Percent
    {
        set { hdn_RateCard_DD_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_DD_Charge_Discount_Percent.Value); }
    }
    public decimal RateCard_CFTFactor
    {
        set { hdn_RateCard_CFTFactor.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_CFTFactor.Value); }
    }
    public decimal RateCard_Octroi_Form_Charge
    {
        set { hdn_RateCard_Octroi_Form_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Octroi_Form_Charge.Value); }
    }
    public decimal RateCard_Octroi_Service_Charge
    {
        set { hdn_RateCard_Octroi_Service_Charge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Octroi_Service_Charge.Value); }
    }
    public decimal RateCard_GI_Charges
    {
        set { hdn_RateCard_GI_Charges.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_GI_Charges.Value); }
    }
    public decimal RateCard_Demurrage_Days
    {
        set { hdn_RateCard_Demurrage_Days.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Demurrage_Days.Value); }
    }
    public decimal RateCard_Demurrage_Rate
    {
        set { hdn_RateCard_Demurrage_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Demurrage_Rate.Value); }
    }
    public decimal RateCard_Invoice_Rate
    {
        set { hdn_RateCard_Invoice_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Invoice_Rate.Value); }
    }
    public decimal RateCard_Invoice_Per_How_Many_Rs
    {
        set { hdn_RateCard_Invoice_Per_How_Many_Rs.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Invoice_Per_How_Many_Rs.Value); }
    }
    public decimal RateCard_Freight_Charge_Discount_Percent
    {
        set { hdn_RateCard_Freight_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_Freight_Charge_Discount_Percent.Value); }
    }
    public decimal RateCard_ToPay_Charge_Discount_Percent
    {
        set { hdn_RateCard_ToPay_Charge_Discount_Percent.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_RateCard_ToPay_Charge_Discount_Percent.Value); }
    }
    public decimal RateCard_AOC_Percent
    {
        set
        {
            hdn_AOCPercent.Value = Util.Decimal2String(Math.Round(value, 2));
            lbl_AOC.Text = "AOC (" + hdn_AOCPercent.Value + "%)";
        }
        get { return Util.String2Decimal(hdn_AOCPercent.Value); }
    }
    # endregion
    # region Standard Amount

    public decimal Standard_FreightRate
    {
        set { hdn_Standard_FreightRate.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Standard_FreightRate.Value); }
    }
    public decimal Standard_FreightAmount
    {
        set { hdn_Standard_FreightAmount.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_FreightAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_FreightAmount.Value); }
    }
    public decimal Standard_HamaliCharge
    {
        set { hdn_Standard_HamaliCharge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_HamaliCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_HamaliCharge.Value); }
    }
    public decimal Standard_DDChargeRate
    {
        set { hdn_Standard_DDCharge_Rate.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_DDCharge_Rate.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_DDCharge_Rate.Value); }
    }
    public decimal Standard_DDCharge
    {
        set { hdn_Standard_DDCharge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_DDCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_DDCharge.Value); }
    }
    public decimal Standard_FOV
    {
        set { hdn_Standard_FOV.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_FOV.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_FOV.Value); }
    }
    public decimal Standard_LengthCharge
    {
        set { hdn_Standard_LengthCharge.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_LengthCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_LengthCharge.Value); }
    }
    public decimal Standard_ServiceTaxPercent
    {
        set { hdn_Standard_ServiceTaxPercent.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_Standard_ServiceTaxPercent.Value); }
    }
    public decimal Standard_ServiceTaxAmount
    {
        set { hdn_Standard_ServiceTaxAmount.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return hdn_Standard_ServiceTaxAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Standard_ServiceTaxAmount.Value); }
    } 
    public bool Is_Consignee_Is_To_Pay_Allowed
    {
        //set { hdn_Consignee_Is_ToPay_Allowed.Value = value == true ? "1" : "0"; }
        //get { return hdn_Consignee_Is_ToPay_Allowed.Value == string.Empty ? false : true; }
        set { hdn_Consignee_Is_ToPay_Allowed.Value = value.ToString().ToLower(); }
        get { return hdn_Consignee_Is_ToPay_Allowed.Value == "true" ? true : false; }

    }

    # endregion
    # region Get GC No
    public void Get_Next_GC_No()
    {
        int local_DocumentSeriesAllocationId, local_Start_No, local_End_No, local_Next_No;

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

    public void Get_No_To_Padd()
    {
        string No_To_Padd = "";
        int i;
        No_For_Padd = "";

        for (i = 0; i < GC_No_Length; i++)
        {
            No_To_Padd = No_To_Padd + "0";
        }
        No_For_Padd = No_To_Padd;
    }
    public void Add_Button_Attributes()
    {
        if (Common.GetMenuItemId() != 194)
        {
            btn_Save_New.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_New, btn_Save_Exit, btn_Save_Print, btn_Save_Repeat, btn_Close));
            btn_Save_Print.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_New, btn_Save_Exit, btn_Save_Repeat, btn_Close));
            btn_Save_Repeat.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Repeat, btn_Save_Print, btn_Save_New, btn_Save_Exit, btn_Close));
        }
        btn_Save_Exit.Attributes.Add("onclick", CommonObj.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save_New, btn_Save_Print, btn_Save_Repeat, btn_Close));
    }

    private bool IsDuplicate_GCNo()
    {
        return objNewGCPresenter.IsDuplicate_GCNo();
    }
    #endregion
    # region Bind DDL

    public void SetMarketingExecutive(string text, string value)
    {
        ddl_MarketingExecutive.DataTextField = "Emp_Name";
        ddl_MarketingExecutive.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_MarketingExecutive);
    }
    public void SetLoadingSuperVisor(string text, string value)
    {
        ddl_LoadingSuperVisor.DataTextField = "Emp_Name";
        ddl_LoadingSuperVisor.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_LoadingSuperVisor);
    }

    public DataTable BindPickupType
    {
        set { Set_Common_DDL(ddl_Pickup_Type, "Pickup_Type", "Pickup_Type_ID", value, false); }
    }
    public DataTable BindDeliveryWayType
    {
        set { Set_Common_DDL(ddl_DelWay_Type, "Delivery_Way_Type", "Delivery_Way_Type_ID", value, false); }
    }
    public DataTable BindBookingType
    {
        set { Set_Common_DDL(ddl_Booking_Type, "Booking_Type", "Booking_Type_Id", value, false); }
    }
    public DataTable BindBookingSubType
    {
        set { Set_Common_DDL(ddl_booking_Sub_Type, "Booking_Sub_Type", "Booking_Sub_Type_Id", value, false); }
    }
    public DataTable BindDeliveryType
    {
        set { Set_Common_DDL(ddl_Dly_Type, "Delivery_Type", "Delivery_Type_Id", value, false); }
    }
    public DataTable BindConsignmentType
    {
        set { Set_Common_DDL(ddl_Consignment_Type, "Consignment_Type", "Consignment_Type_Id", value, false); }
    }
    public DataTable BindDeliveryAgainst
    {
        set { Set_Common_DDL(ddl_Delivery_Against, "Door_Delivery_Against", "Door_Delivery_Against_ID", value, false); }
    }
    public DataTable BindRoadPermitType
    {
        set { Set_Common_DDL(ddl_Road_Permit, "Road_Permit", "Road_Permit_Type_Id", value, false); }
    }
    public DataTable BindPaymentType
    {
        set { Set_Common_DDL(ddl_PaymentType, "Payment_Type", "Payment_Type_Id", value, false); }
    }
    public DataTable BindReasonFreightPending
    {
        set { Set_Common_DDL(ddl_Reason_Freight_Pending , "Reason", "Reason_Id", value, false); }
    }
    public DataTable BindGCRiskType
    {
        set { Set_Common_DDL(ddl_GCRisk, "Risk_Type", "Risk_Type_ID", value, false); }
    }
    public DataTable BindVehicleType
    {
        set { Set_Common_DDL(ddl_VehicleType, "Vehicle_Type", "Vehicle_Type_ID", value, false); }
    }
    public DataTable BindUnitOfMeasurement
    {
        set { Set_Common_DDL(ddl_UnitOfMeasurment, "Unit_Of_Measurement", "Unit_Of_Measurement_ID", value, false); }
    }
    public DataTable BindFreightBasis
    {
        set { Set_Common_DDL(ddl_FreightBasis, "Freight_Basis", "Freight_Basis_ID", value, false); }
    }
    public DataTable BindVolumetricFreightUnit
    {
        set { Set_Common_DDL(ddl_VolumetricFreightUnit, "Volumetric_Freight_Unit", "Volumetric_Freight_Unit_ID", value, false); }
    }
    public DataTable BindLengthChargeHead
    {
        set { Set_Common_DDL(ddl_LengthChargeHead, "Length_ChargeName", "LengthChargeHeadID", value, true); }
    }
    public DataTable BindGCInstructions
    {
        set { Set_Common_DDL(ddl_Instruction, "Instructions", "GC_Instruction_Id", value, true); }
    }
    public DataTable BindBillingHierarchy
    {
        set { Set_Common_DDL(ddl_BillingHierarchy, "Hierarchy_Name", "Hierarchy_Code", value, true); }
    }
    public DataTable BindContractBranches
    {
        set { Set_Common_DDL(ddl_ContractBranch, "Contract_Branch_Name", "Contract_Branch_Id", value, true); }
    }
    public DataTable BindContract
    {
        set { Set_Common_DDL(ddl_Contract, "Contract_Name", "Contract_ID", value, true); }
    }
    public DataTable BindServiceType
    {
        set { Set_Common_DDL(ddl_Service_Type, "Service_Type", "Service_Type_ID", value, false); }
    }
    public DataTable Bind_DDLGC_No
    {
        set { Set_Common_DDL(ddl_GC_No, "Branch_Code", "Branch_Code", value, false); }
    }
    private void Fill_CRM_Pickup_Request()
    {
        string query = "";
        DataSet ds_pickup_request = new DataSet();
        DataSet ds_pickup_request1 = new DataSet();

        query = query + "Select Pickup_ID,Pickup_No from EC_CRM_PickUp_Request where Pickup_Gc_ID = 0";
        query = query + " and Reason = ''";
        query = query + " and Branch_ID = " + BookingBranchId;
        query = query + " Order by Pickup_No";

        ds_pickup_request = CommonObj.EC_Common_Pass_Query(query);
        ds_pickup_request1 = CommonObj.Get_Values_Where("EC_CRM_PickUp_Request", "Pickup_ID,Pickup_No", "Pickup_GC_ID = " + keyID + " and VA_Id = " + VAId, "Pickup_no", false);

        ds_pickup_request.Merge(ds_pickup_request1, true);

        Set_Common_DDL(ddl_pick_request, "Pickup_No", "Pickup_ID", ds_pickup_request.Tables[0], true);
        if (keyID > 0)
            SetCRMPickupRequestId = Util.String2Int(hdnPickupReqId.Value);

        if (CRMPickupRequestId > 0)
            lnk_Pickup_Req.Visible = true;
        else
            lnk_Pickup_Req.Visible = false;
    }
    # endregion
    #region Session Variables
    public int Session_Mode
    {
        get { return StateManager.GetState<int>("SessionMode"); }
        set { StateManager.SaveState("SessionMode", value); }
    }
    public int Session_MenuItem
    {
        get { return StateManager.GetState<int>("SessionMenuItem"); }
        set { StateManager.SaveState("SessionMenuItem", value); }
    }
    public int Session_ContainerTypeId
    {
        get { return BookingTypeId != 5 ? 0 : StateManager.GetState<int>("ContainerTypeId"); }
        set { StateManager.SaveState("ContainerTypeId", value); }
    }
    public string Session_ContainerNoPart1
    {
        get { return BookingTypeId != 5 ? string.Empty : StateManager.GetState<string>("ContainerNoPart1"); }
        set { StateManager.SaveState("ContainerNoPart1", value); }
    }
    public string Session_ContainerNoPart2
    {
        get { return BookingTypeId != 5 ? string.Empty : StateManager.GetState<string>("ContainerNoPart2"); }
        set { StateManager.SaveState("ContainerNoPart2", value); }
    }
    public string Session_SealNo
    {
        get { return BookingTypeId != 5 ? string.Empty : StateManager.GetState<string>("SealNo"); }
        set { StateManager.SaveState("SealNo", value); }
    }
    public int Session_ReturnToYardId
    {
        get { return BookingTypeId != 5 ? 0 : StateManager.GetState<int>("ReturnToYardId"); }
        set { StateManager.SaveState("ReturnToYardId", value); }
    }
    public string Session_ReturnToYardName
    {
        get { return BookingTypeId != 5 ? string.Empty : StateManager.GetState<string>("ReturnToYardName"); }
        set { StateManager.SaveState("ReturnToYardName", value); }
    }
    public string Session_NFormNo
    {
        get { return BookingTypeId != 5 ? string.Empty : StateManager.GetState<string>("NFormNo"); }
        set { StateManager.SaveState("NFormNo", value); }
    }
    public decimal Session_PolicyAmount
    {
        get { return Is_Insured == false ? 0 : StateManager.GetState<Decimal>("PolicyAmount"); }
        set { StateManager.SaveState("PolicyAmount", value); }
    }
    public decimal Session_RiskAmount
    {
        get { return Is_Insured == false ? 0 : StateManager.GetState<Decimal>("RiskAmount"); }
        set { StateManager.SaveState("RiskAmount", value); }
    }
    public DateTime Session_PolicyExpDate
    {
        get { return Is_Insured == false ? DateTime.Now : StateManager.GetState<DateTime>("PolicyExpDate"); }
        set { StateManager.SaveState("PolicyExpDate", value); }
    }
    public string Session_InsuranceCompany
    {
        get { return Is_Insured == false ? string.Empty : StateManager.GetState<String>("InsuranceCompany"); }
        set { StateManager.SaveState("InsuranceCompany", value); }
    }
    public string Session_PolicyNo
    {
        get { return Is_Insured == false ? string.Empty : StateManager.GetState<String>("PolicyNo"); }
        set { StateManager.SaveState("PolicyNo", value); }
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
    public DataTable Session_SizeDdl
    {
        get { return StateManager.GetState<DataTable>("SizeDdl"); }
        set { StateManager.SaveState("SizeDdl", value); }
    }
    public DataTable Session_PackingTypeDdl
    {
        get { return StateManager.GetState<DataTable>("PackingTypeDdl"); }
        set { StateManager.SaveState("PackingTypeDdl", value); }
    }
    public DataTable Session_MultipleCommodityGrid
    {
        get { return StateManager.GetState<DataTable>("MultipleCommodityGrid"); }
        set
        {
            StateManager.SaveState("MultipleCommodityGrid", value);
            if (value.Rows.Count > 0)
            {
                hdn_FirstCommodityId.Value = value.Rows[0]["Commodity_ID"].ToString();
                hdn_FirstItemId.Value = value.Rows[0]["Item_ID"].ToString();
                hdn_FirstPackingTypeId.Value = value.Rows[0]["Packing_ID"].ToString();
            }
        }
    }
    public DataTable Session_InvoiceGrid
    {
        get { return StateManager.GetState<DataTable>("InvoiceGrid"); }
        set
        {
            StateManager.SaveState("InvoiceGrid", value);
            {
                if (value.Rows.Count > 0)
                {
                    DataRow dr = value.Rows[0];
                    txt_Invoice_No.Text = dr["Invoice_No"].ToString();
                    txt_Invoice_Value.Text = dr["Invoice_Amount"].ToString();
                }
            }
        }
    }
    public DataTable Session_GCOtherChargeHead
    {
        get { return StateManager.GetState<DataTable>("GCOtherChargeHead"); }
        set { StateManager.SaveState("GCOtherChargeHead", value); }
    }
    public DataTable Session_BillingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("BillingDetailsGrid"); }
        set { StateManager.SaveState("BillingDetailsGrid", value); }
    }
    public DataTable Session_ChequeDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("ChequeDetailsGrid"); }
        set { StateManager.SaveState("ChequeDetailsGrid", value); }
    }
    public DataTable Session_ContainerType
    {
        get { return StateManager.GetState<DataTable>("ContainerType"); }
        set { StateManager.SaveState("ContainerType", value); }
    }
    public String MultipleCommodityXml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_MultipleCommodityGrid.Copy());
            _objDs.Tables[0].TableName = "commodity";
            return _objDs.GetXml().ToLower();
        }
    }
    public String InvoiceXml
    {
        get
        {
            if (Session_InvoiceGrid.Rows.Count > 0)
            {
                Session_InvoiceGrid.Rows.RemoveAt(0);
            }

            DataRow dr = Session_InvoiceGrid.NewRow();
            dr["Invoice_No"] = txt_Invoice_No.Text;
            dr["Invoice_Amount"] = Util.String2Decimal(txt_Invoice_Value.Text);
            dr["chalan_no"] = txt_Invoice_No.Text;
            dr["BE_BL_No"] = "";
            Session_InvoiceGrid.Rows.Add(dr);

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
            _objDs.Tables.Add(Session_BillingDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "billingdetails";
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

            //objdr["cheque_bank_name"] = Default_Cheque_Bank_Ledger_Name;
            objdr["cheque_bank_name"] = BankName;
            objdr["cheque_branch_name"] = Default_Cheque_Branch_Ledger_Name;
            objdr["cheque_no"] = ChequeNo;
            objdr["bank_ledger_id"] = Default_Bank_Ledger_Id;
            objdr["cheque_amount"] = ChequeAmount;
            objdr["cheque_date"] = ChequeDate.ToString("dd MMMM yyyy");

            Session_ChequeDetailsGrid.Rows.Add(objdr);

            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_ChequeDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "mrchequedetails";
            return _objDs.GetXml().ToLower();
        }
    }
    # endregion
    #region IView Members

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
            lbl_Errors1.Text = value;
        }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public bool validateUI()
    {
        int Validate_LoadingSuperVisor_RequiredFor_BookingTypeId = 0;

        char[] _Separator ={ ',' };
        string[] _IdArray = LoadingSuperVisor_RequiredFor_BookingType.Split(_Separator);
        int i = 0;

        DateTime Valid_Cheque_Start_Date, Valid_Cheque_End_Date;

        Valid_Cheque_Start_Date = wuc_BookingDate.SelectedDate;

        Valid_Cheque_Start_Date = wuc_BookingDate.SelectedDate.AddDays(-Valid_Cheque_Start_Days);
        Valid_Cheque_End_Date = wuc_BookingDate.SelectedDate.AddDays(Valid_Cheque_End_Days);

        if (_IdArray.Length > 0)
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
        else
        {
            Validate_LoadingSuperVisor_RequiredFor_BookingTypeId = 0;
            _IdArray[0] = "all";
        }

        bool Is_Valid = false;
        TextBox txt_MarketingExcutive = (TextBox)ddl_MarketingExecutive.FindControl("txtBoxddl_MarketingExecutive");
        TextBox txt_LoadingSupervisor = (TextBox)ddl_LoadingSuperVisor.FindControl("txtBoxddl_LoadingSuperVisor");

        if (GC_No_For_Print == string.Empty)
        {
            errorMessage = "Please Enter " + GCCaption + " No.";
            txt_GC_No.Focus();
        }
        else if (keyID <= 0 && MenuItemId != 200 && (Util.String2Int(GC_No_For_Print) < Start_No || Util.String2Int(GC_No_For_Print) > End_No))
        {
            errorMessage = GCCaption + " No.should be between (" + Start_No.ToString() + ") and (" + End_No.ToString() + ")";
            txt_GC_No.Focus();
        }
        else if (MenuItemId == 200 && Util.String2Int(GC_No_For_Print) <= 0)
        {
            errorMessage = "Please Enter " + GCCaption + " No.";
            txt_GC_No.Focus();
        }
        //else if (GC_No_For_Print.Length < GC_No_Length)
        //{
        //    errorMessage = GCCaption + " Number should have " + GC_No_Length.ToString() + " Digits Only.";
        //    txt_GC_No.Focus();
        //}
        else if (BookingDate > UserManager.getUserParam().TodaysDate)
        {
            errorMessage = "Booking Date Should Be Less Than Or Equal To Todays Date.";
        }
        else if (MenuItemId != 194 && BookingTypeId == 1 && BookingDate < UserManager.getUserParam().TodaysDate)
        {
            errorMessage = "Booking Date Should Be Equal To Todays Date.";
        }
        else if (MenuItemId == 194 && BookingDate > GCDate_ForRectify)
        {
            errorMessage = "Booking Date Should not Be greater Than (" + GCDate_ForRectify.ToString("dd MMM yyyy") + ") Date.";
        }
        else if (MenuItemId == 229 && Agency_GC_No.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Agency " + GCCaption + " No.";
            txt_Agency_GCNo.Focus();
        }
        else if (MenuItemId == 229 && AgencyId <= 0)
        {
            errorMessage = "Please Select Agency";
            txt_Agency.Focus();
        }
        else if (MenuItemId == 229 && AgencyId > 0 && AgencyLedgerId <= 0)
        {
            errorMessage = "Please Select Agency Ledger ";
            txt_Ledger.Focus();
        }
        else if (keyID <= 0 && IsDuplicate_GCNo())
        {
            errorMessage = "Duplicate " + GCCaption + " No.";
            txt_GC_No.Focus();
        }
        else if (ClientCode != "nandwana" && PrivateMark == string.Empty && pc.Control_Is_Mandatory(txt_Private_Mark) == true)
        {
            errorMessage = "Please Enter Private Mark.";
            txt_Private_Mark.Focus();
        }
        else if (ClientCode != "nandwana" && MenuItemId != 200 && Is_Attached && Attached_GC_Id <= 0)
        {
            errorMessage = "Please Enter " + GCCaption + " No. For Attached";
            txt_Attached_GC_No.Focus();
        }
        //else if (PaymentTypeId == 5 && ReBook_GC_Id > 0)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
        //}
        //else if (!Is_Allow_To_ReBook && ReBook_GC_Id > 0 && keyID <= 0)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_CantReBook").ToString();
        //}
        else if (MenuItemId != 200 && (BookingDate < UserManager.getUserParam().StartDate || BookingDate > UserManager.getUserParam().EndDate))
        {
            errorMessage = "Booking Date Should Be of Current Financial Year.";
        }
        else if (ClientCode != "nandwana" && MenuItemId == 200 && BookingDate >= ApplicationStartDate)
        {
            errorMessage = "Booking Date of Opening " + GCCaption + " Should Be Less Than Application Start Date - "
                             + " ( " + ApplicationStartDate.ToString("dd MMM yyyy") + " ) ";
        }
        else if (MenuItemId == 200 && ArrivedDate < BookingDate)
        {
            errorMessage = "Arrived Date Shoud Not Be Greater Than Booking Date.";
        }
        else if (ConsignmentTypeId <= 0 && pc.Control_Is_Mandatory(ddl_Consignment_Type) == true)
        {
            errorMessage = "Please Select Consignment Type.";
            ddl_Consignment_Type.Focus();
        }
        else if (BookingTypeId <= 0)
        {
            errorMessage = "Please Select Booking Type.";
            ddl_Booking_Type.Focus();
        }
        else if (ClientCode == "nandwana" && BookingTypeId == 5 && Is_ContainerDetails_Filled == 0)
        {
            errorMessage = "Please Mention Container Details.";
        }
        else if (ClientCode != "nandwana" && BookingSubTypeId <= 0 && pc.Control_Is_Mandatory(ddl_booking_Sub_Type) == true)
        {
            errorMessage = "Please Select Booking Sub Type.";
            ddl_booking_Sub_Type.Focus();
        }
        else if (DeliveryTypeId <= 0)
        {
            errorMessage = "Please Select Delivery Type.";
            ddl_Dly_Type.Focus();
        }
        else if (ClientCode != "nandwana" && DeliveryAgainstId <= 0)
        {
            errorMessage = "Please Select Delivery Against.";
            ddl_Delivery_Against.Focus();
        }
        else if (MenuItemId == 200 && BookingBranchId == 0)
        {
            errorMessage = "Please Select Booking Branch.";
            txt_Booking_Branch.Focus();
        }
        else if (FromLocationId <= 0)
        {
            errorMessage = "Please Select From Location.";
            txt_From_Location.Focus();
        }
        else if (ToLocationId <= 0)
        {
            errorMessage = "Please Select To Location.";
            txt_To_Location.Focus();
        }
        else if (ClientCode != "nandwana" && RoadPermitTypeId <= 0 && pc.Control_Is_Mandatory(ddl_Road_Permit) == true)
        {
            errorMessage = "Please Select Road Permit Type.";
            ddl_Road_Permit.Focus();
        }
        else if (ClientCode != "nandwana" && RoadPermitTypeId == 2 && RoadPermitSrNo.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Road_Permit_SrNo) == true)
        {
            errorMessage = "Please Enter Road Permit Sr. No.";
            txt_Road_Permit_SrNo.Focus();
        }
        else if (ClientCode != "nandwana" && VehicleTypeId <= 0)
        {
            errorMessage = "Please Select Vehicle Type.";
            ddl_VehicleType.Focus();
        }
        else if (PickupTypeId <= 0)
        {
            errorMessage = "Please Select Pickup Type.";
            ddl_Pickup_Type.Focus();
        }
        else if (ConsignorId <= 0)
        {
            errorMessage = "Please Select Consignor.";
            txt_ConsignorName.Focus();
        }
        else if (ConsigneeId <= 0)
        {
            errorMessage = "Please Select Consignee.";
            txt_ConsigneeName.Focus();
        }
        else if (PaymentTypeId <= 0)
        {
            errorMessage = "Please Select Payment Type.";
            ddl_PaymentType.Focus();
        }
        else if (PaymentTypeId == 1 && !Is_ToPayBookingApplicable)
        {
            errorMessage = "To Pay Booking is not allowed for selected To Location";
            ddl_PaymentType.Focus();
        }
        else if (PaymentTypeId == 3 && BillingPartyId <= 0 && Is_MultipleBilling == false)
        {
            errorMessage = "Please Select Billing Party.";
            txt_BillingParty.Focus();
        }
        else if (PaymentTypeId == 3 && BillingPartyId > 0 && Is_MultipleBilling == false && BillingHierarchy == "0")
        {
            errorMessage = "Please Select Billing Hierarchy";
            ddl_BillingHierarchy.Focus();
        }
        else if (ClientCode != "nandwana" && PaymentTypeId == 3 && BillingPartyId > 0 && Is_MultipleBilling == false && BillingHierarchy != "0" && BillingHierarchy != "HO" && BillingLocationId <= 0)
        {
            if (BillingHierarchy == "BO")
            {
                errorMessage = "Please Select Billing Branch";
                txt_BillingLocation.Focus();
            }
            else if (BillingHierarchy == "AO")
            {
                errorMessage = "Please Select Billing Area";
                txt_BillingLocation.Focus();
            }
            else if (BillingHierarchy == "RO")
            {
                errorMessage = "Please Select Billing Region";
                txt_BillingLocation.Focus();
            }
        }
        else if (ClientCode == "nandwana" && PaymentTypeId == 3 && BillingPartyId > 0 && BillingLocationId <= 0)
        {
            if (BillingHierarchy == "BO")
            {
                errorMessage = "Please Select Billing Branch";
                txt_BillingLocation.Focus();
            }
            else if (BillingHierarchy == "AO")
            {
                errorMessage = "Please Select Billing Area";
                txt_BillingLocation.Focus();
            }
            else if (BillingHierarchy == "RO")
            {
                errorMessage = "Please Select Billing Region";
                txt_BillingLocation.Focus();
            }
        }
        else if (GCRiskId <= 0)
        {
            errorMessage = "Please Select " + GCCaption + " Risk.";
            ddl_GCRisk.Focus();
        }
        //else if (GCRiskId == 2 && Is_Insured == false) // for career risk
        //{
        //    errorMessage = "Please Mention Insurance details.";
        //}
        //else if (GCRiskId == 2 && Is_Insured == true && !Is_InsuranceDetails_Filled)
        //{
        //    errorMessage = "Please Mention Valid Insurance details.";
        //}
        else if (TotalArticles <= 0 || Session_MultipleCommodityGrid.Rows.Count <= 0)
        {
            errorMessage = "Please Mention Commodity Details.";
        }
        else if (PaymentTypeId == 3 && BillingParty_CreditLimit > 0 && (BillingParty_Ledger_Closing_Balance + BillingParty_CreditLimit) <= 0)
        {
            errorMessage = "Aapka Credit Limmit Khatam Huva. Om Turanth Me Payment Bhejo.";
        }
        else if (PaymentTypeId == 3 && BillingParty_MinimumBalance > 0 && BillingParty_Ledger_Closing_Balance < BillingParty_MinimumBalance)
        {
            errorMessage = "Aapka Balanace Khatam Huva. Om Turanth Me Payment Bhejo.";
        }
        else if (TotalWeight <= 0)
        {
            errorMessage = "Total Actual Weight Should be Greater than Zero.";
        }
        else if (FreightBasisId == 4 && VolumetricFreightUnitId == 1 && TotalCFT <= 0)
        {
            errorMessage = "Please Enter Valid Length, Width and Height.";
        }
        else if (FreightBasisId == 4 && VolumetricFreightUnitId == 2 && TotalCBM <= 0)
        {
            errorMessage = "Please Enter Valid Length, Width and Height.";
        }
        else if ((PaymentTypeId != 5 && !Is_Attached) && Freight <= 0)
        {
            errorMessage = "Freight Amount Should Be Greater Than Zero.";
            txt_Freight.Focus();
        }
        else if ((PaymentTypeId != 5 && !Is_Attached) && TotalGCAmount <= 0)
        {
            errorMessage = "Total " + GCCaption + " Amount Should Be Greater Than Zero.";
        }
        else if (ChequeAmount > 0 && Util.String2Int(ChequeNo) <= 0)
        {
            errorMessage = "Please Enter Cheque No.";
            txt_ChequeNo.Focus();
        }
        else if (ChequeAmount > 0 && (ChequeNo == string.Empty || ChequeNo.Length < 5))
        {
            errorMessage = "Cheque No Should have Atleast 5 Digits."; ;
            txt_ChequeNo.Focus();
        }
        else if (ChequeAmount > 0 && BankName.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Bank Name.";
            txt_BankName.Focus();
        }
        else if (ChequeAmount > 0 && ChequeDate < Valid_Cheque_Start_Date)
        {
            errorMessage = "Cheque Date Should Not be Less Than "
                            + Valid_Cheque_Start_Days + " Days Old From Booking Date"
                            + " ( " + Valid_Cheque_Start_Date.ToString("dd MMM yyyy") + " )";
        }
        else if (ChequeAmount > 0 && ChequeDate > Valid_Cheque_End_Date)
        {
            errorMessage = "Check Date Should Not Greater Than "
                            + Valid_Cheque_End_Days + " Days From Booking Date"
                            + " ( " + Valid_Cheque_End_Date.ToString("dd MMM yyyy") + " )";
        }
        else if ((PaymentTypeId == 2) && (CashAmount + ChequeAmount != TotalGCAmount)) // paid
        {
            errorMessage = "Cash + Cheque Amount Should Be Equal To " + GCCaption + " Amount.";
            txt_CashAmount.Focus();
        }
        else if (PaymentTypeId == 1 && (CashAmount + ChequeAmount != Advance))
        {
            errorMessage = "Cash + Cheque Amount Should Be Equal To Advance Amount.";
            txt_CashAmount.Focus();
        }

        else if ((PaymentTypeId == 2) && (ChequeAmount > 0 && ChequeAmount < 500)) // paid
        {
            errorMessage = "Cheque Can Not Be Accepted For Freight Less Than Rs. 500";
            txt_CashAmount.Focus();
        }
        else if (PaymentTypeId == 1 && Advance > SubTotal)
        {
            errorMessage = "Advance Amount Should Not Be Greater Than Sub Total.";
            txt_Advance.Focus();
        }
        else if (LoadingSuperVisorId <= 0 && _IdArray[0] == "all")
        {
            errorMessage = "Please Select Loading Supervisor.";
            txt_LoadingSupervisor.Focus();
        }
        else if (LoadingSuperVisorId <= 0 && _IdArray[0] != "all" && BookingTypeId == Validate_LoadingSuperVisor_RequiredFor_BookingTypeId)
        {
            errorMessage = "Please Select Loading Supervisor.";
            txt_LoadingSupervisor.Focus();
        }
        //else if (MarketingExecutiveId <= 0 && pc.Control_Is_Mandatory(td_ddl_MarketingExecutive) == true)
        //{
        //    errorMessage = "Please Select Marketing Executive.";
        //    txt_MarketingExcutive.Focus();
        //}
        else if (MenuItemId != 200 && ContractId > 0 && Is_ContractApplied == 0 && !Is_Attached)
        {
            errorMessage = "Contractual Rates are not Applicable.";
            ddl_Contract.Focus();
        }
        else if (MenuItemId != 200 && ContractId <= 0 && Is_Contract_Required_For_TBB_GC && PaymentTypeId == 3)
        {
            errorMessage = "Contract Applied In Case Of TBB " + GCCaption;
            ddl_Contract.Focus();
        }
        else if (PaymentTypeId == 3 && Is_MultipleBilling && Session_TotalRatio == 0)
        {
            errorMessage = "Please Enter Billing Details.";
        }
        else if (PaymentTypeId == 3 && Is_MultipleBilling && (Session_TotalRatio < 100 || Session_TotalRatio > 100))
        {
            errorMessage = "Invalid Billing Details.";
        }
        else if (ClientCode != "nandwana" && Contractual_ClientId > 0 && PaymentTypeId == 1 && !IsCC_ToPayAllowed)
        {
            errorMessage = "To Pay Booking Is Not Allow For Selected Contractual Client";
            txt_ContractClient.Focus();
        }
        else if (ClientCode != "nandwana" && Contractual_ClientId > 0 && (PaymentTypeId == 2 || PaymentTypeId == 4) && !IsCC_PaidAllowed)
        {
            errorMessage = "Paid Booking Is Not Allow For Selected Contractual Client";
            txt_ContractClient.Focus();
        }
        else if (ClientCode != "nandwana" && Contractual_ClientId > 0 && PaymentTypeId == 3 && !IsCC_TBBAllowed)
        {
            errorMessage = "To Be Billed Booking Is Not Allow For Selected Contractual Client";
            txt_ContractClient.Focus();
        }
        else if (ClientCode != "nandwana" && Contractual_ClientId > 0 && PaymentTypeId == 5 && !IsCC_FOCAllowed)
        {
            errorMessage = "FOC Booking Is Not Allow For Selected Contractual Client";
            txt_ContractClient.Focus();
        }
        //else if (keyID <= 0)
        //{
        //    if (PaymentTypeId == 1 && Is_Consignee_Is_To_Pay_Allowed == false) 
        //    {
        //        errorMessage = "To Pay Is Not Allow For Selected Consignee";
        //        ddl_PaymentType.Focus();
        //    }
        //}
        else if (PaymentTypeId == 1 && Is_Consignee_Is_To_Pay_Allowed == false)
        {
            errorMessage = "To Pay Is Not Allow For Selected Consignee";
            ddl_PaymentType.Focus();
        }
        else if (txt_eWayBillNo.Text.Trim().Length > 0 && txt_eWayBillNo.Text.Trim().Length < 12)
        {
            errorMessage = "Invalid e-Way Bill No.";
            txt_eWayBillNo.Focus();
        }
        else if (txt_eWayBillNo.Text == "000000000000" || txt_eWayBillNo.Text == "111111111111" || txt_eWayBillNo.Text == "222222222222" || txt_eWayBillNo.Text == "333333333333" || txt_eWayBillNo.Text == "444444444444" || txt_eWayBillNo.Text == "555555555555" || txt_eWayBillNo.Text == "666666666666" || txt_eWayBillNo.Text == "777777777777" || txt_eWayBillNo.Text == "888888888888" || txt_eWayBillNo.Text == "999999999999")
        {
            errorMessage = ("Invalid e-Way Bill No.");
            txt_eWayBillNo.Focus();
        }
        else if ((Is_ServiceTaxApplicableForConsignor == true || Is_ServiceTaxApplicableForConsignee == true) && (TotalInvoiceAmount >= 100000 && ConsignorStateId == 1 && ConsigneeStateId == 1 && txt_eWayBillNo.Text.Trim().Length < 12 && Is_Service_Tax_Applicable_For_Commodity == true))
        {
            errorMessage = "Enter Proper e-Way Bill No. As Per Maharashtra Intra-State Rule";
            txt_eWayBillNo.Focus();
        }
        else if ((Is_ServiceTaxApplicableForConsignor == true || Is_ServiceTaxApplicableForConsignee == true) && (TotalInvoiceAmount >= 50000 && ConsignorStateId == 2 && ConsigneeStateId == 2 && txt_eWayBillNo.Text.Trim().Length < 12 && Is_Service_Tax_Applicable_For_Commodity == true))
        {
            errorMessage = "Enter Proper e-Way Bill No. As Per Gujrat Intra-State Rule";
            txt_eWayBillNo.Focus();
        }

        //else if ((Is_ServiceTaxApplicableForConsignor == true || Is_ServiceTaxApplicableForConsignee == true) && (TotalInvoiceAmount >= 50000 && ConsignorStateId != ConsigneeStateId && txt_eWayBillNo.Text.Trim().Length < 12 && Is_Service_Tax_Applicable_For_Commodity == true))
        else if ((Is_ServiceTaxApplicableForConsignor == true || Is_ServiceTaxApplicableForConsignee == true) && (TotalInvoiceAmount >= 50000 && ConsignorStateId != ConsigneeStateId && txt_eWayBillNo.Text.Trim().Length < 12 && Is_Service_Tax_Applicable_For_Commodity == true))
        {
            errorMessage = "Enter Proper e-Way Bill No. As Per Inter-State Rule";
            txt_eWayBillNo.Focus();
        }
        //else if ((Is_ServiceTaxApplicableForConsignor == true || Is_ServiceTaxApplicableForConsignee == true) && (TotalInvoiceAmount >= 50000 && ConsignorStateId == 2 && ConsigneeStateId == 2 && txt_eWayBillNo.Text.Trim().Length < 12 && Is_Service_Tax_Applicable_For_Commodity == true))
        //{
        //    errorMessage = "e-Way Bill No. is Mandatory for Intra State Transaction in Gujrat.";
        //    txt_eWayBillNo.Focus();
        //}
        else if (hdn_LoginUserHierarchy.Value == "BO" && PaymentTypeId == 5 )
        {
            errorMessage = "You Are Not Authorised To Save FOC LR";
            ddl_PaymentType.Focus();
        }

        else if (PaymentTypeId == 4 && ReasonFreightPendingId <= 0)
        {
            errorMessage = "Select Proper Reason";
            ddl_Reason_Freight_Pending.Focus();
        }

        else if (PaymentTypeId == 4 && PaidFreightPendingPersonName.Length <= 0)
        {
            errorMessage = "Enter Person Name";
            txt_Paid_Freight_Pending_Person_Name.Focus();
        }

        else if (PaymentTypeId == 4 && PaidFreightPendingPersonMobile.Length < 10)
        {
            errorMessage = "Enter Person Mobile No.";
            txt_Paid_Freight_Pending_Person_Mobile.Focus();
        }
        else
        {
            Is_Valid = true;
        }

        return Is_Valid;
    }

    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }

    private void Set_Hidden_Value()
    {
        ExpectedDeliveryDate = Convert.ToDateTime(hdn_dly_date.Value);
        DeliveryBranchName = hdn_DeliveryBranchName.Value;
        ConsignorAddressValue = hdn_ConsignorDetailsValue.Value;
        ConsigneeAddressValue = hdn_ConsigneeDetailsValue.Value;
        Is_OctroiApplicable = hdn_Is_Oct_Appl.Value == "1" ? true : false;
        ServiceTaxPayableBy = hdn_ServiceTaxPayableBy.Value == string.Empty ? 3 : Util.String2Int(hdn_ServiceTaxPayableBy.Value);
        FreightRate = hdn_FreightRate.Value == string.Empty ? 0 : Util.String2Decimal(hdn_FreightRate.Value);
        Freight = hdn_Freight.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Freight.Value);
        OtherCharges = hdn_OtherCharge.Value == string.Empty ? 0 : Util.String2Decimal(hdn_OtherCharge.Value);
        SubTotal = hdn_SubTotal.Value == string.Empty ? 0 : Util.String2Decimal(hdn_SubTotal.Value);
        Abatment = hdn_Abatment.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Abatment.Value);
        TaxableAmount = hdn_TaxableAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TaxableAmount.Value);
        ServiceTax = hdn_ServiceTax.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ServiceTax.Value);
        ReBookGC_OctroiAmount = hdn_ReBookGC_OctroiAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ReBookGC_OctroiAmount.Value);
        TotalGCAmount = hdn_TotalGCAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalGCAmount.Value);

        HeightInFeet = hdn_HeightInFeet.Value == string.Empty ? 0 : Util.String2Decimal(hdn_HeightInFeet.Value);
        LengthInFeet = hdn_LengthInFeet.Value == string.Empty ? 0 : Util.String2Decimal(hdn_LengthInFeet.Value);
        WidthInFeet = hdn_WidthInFeet.Value == string.Empty ? 0 : Util.String2Decimal(hdn_WidthInFeet.Value);
        TotalCFT = hdn_TotalCFT.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalCFT.Value);
        TotalCBM = hdn_TotalCBM.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalCBM.Value);

        if (ContractId > 0)
        {
            objNewGCPresenter.Fill_Contract_And_Branches(1);
            Contract_BranchId = Util.String2Int(hdn_ContractBranchId.Value);
            objNewGCPresenter.Fill_Contract_And_Branches(2);
            ContractId = Util.String2Int(hdn_ContractId.Value);
        }
        else
        {
            objNewGCPresenter.Fill_Contract_And_Branches(1);
            objNewGCPresenter.Fill_Contract_And_Branches(2);
        }

        PaymentTypeId = Util.String2Int(hdn_OldPaymentType.Value);
        Contractual_Client = hdn_ContractClient.Value;
        AgencyLedger = hdn_LedgerName.Value;
        BillingParty = hdn_BillingParty.Value;
        BillingHierarchy = hdn_BillingHierarchy.Value == string.Empty ? "0" : hdn_BillingHierarchy.Value;
        BillingLocation = hdn_BillingLocation.Value;
        BillingRemark = hdn_BillingRemark.Value;
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        Set_Default_Value_On_Page_Load();

        if (!IsPostBack)
        {
            Add_Button_Attributes();

            pc.AddAttributes(this.Controls);
            pc.Txtbox_Add_Attributes(this.Controls);

            Set_DefaultSessionValue();
            if (keyID > 0)
            {
                hdnissaveandnewandprint.Value = null;
                Session["SaveAndNewAndPrint"] = null;
            }
        }
        objNewGCPresenter = new NewGCPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            Get_No_To_Padd();
            Fill_CRM_Pickup_Request();

            if (ClientCode.ToLower() == "nandwana")
                Fill_BranchCode();

            if (keyID <= 0)
            {
                if (MenuItemId != 200)  // 200 means Opening GC
                {
                    Get_Next_GC_No();
                }
                else
                {
                    DocumentSeriesAllocationId = 0;
                    Start_No = 0;
                    End_No = 0;
                    Next_No = 0;
                    ddl_GC_No.SelectedIndex = 0;
                }
                Get_From_Location_Details();
            }
            ExpectedDeliveryDate = BookingDate;
            txt_GC_No.MaxLength = GC_No_Length;
            txt_Agency_GCNo.MaxLength = GC_No_Length;
            txt_Attached_GC_No.MaxLength = GC_No_Length;

            if ((MenuItemId != 200 && !Is_GCNumberEditable || keyID > 0) || (MenuItemId == 200 && keyID > 0))
            {
                txt_GC_No.Attributes.Add("disabled", "true");
                ddl_GC_No.Attributes.Add("disabled", "true");
            }

            lbl_ConsignorDetailsValue.Style.Add("display", "none");
            lbl_ConsigneeDetailsValue.Style.Add("display", "none");
            lbl_BookingTime.Style.Add("display", "none");

            if (keyID > 0)
            {
                btn_ConsrId_Click(sender, e);
                btn_ConseeId_Click(sender, e);
            }
            txt_To_Location.Focus();
        }
        if (MenuItemId == 229)  // 229 means Agency GC
        {
            FreightBasisId = 3;
        }

        if (!StateManager.IsValidSession("MultipleCommodityGrid") || !StateManager.IsValidSession("InvoiceGrid"))
        {
            Common.DisplayErrors(1002);
        }

        txt_Discount.Attributes.Add("disabled", "true");
        txt_StationaryCharge.Attributes.Add("disabled", "true");
        txt_FOVRiskCharge.Attributes.Add("disabled", "true");
        txt_AOC.Attributes.Add("disabled", "true");

        Set_Hidden_Value();


        if (Session["DocumentIdForSaveAndNewAndPrint"] != null)
        {
            hdndocumentID.Value = Session["DocumentIdForSaveAndNewAndPrint"].ToString();
        }

        if (Session["SaveAndNewAndPrint"] != null)
        {
            if (Session["SaveAndNewAndPrint"].ToString() == "1")
            {
                string baseurl = ClassLibraryMVP.Util.GetBaseURL();
                hdnprinturl.Value = baseurl + "/Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=30&Document_ID=" + hdndocumentID.Value;

                hdnissaveandnewandprint.Value = "1";
            }
        }
        if (MenuItemId == 30)
        {
            txt_Freight.ReadOnly = true;
        }
        else 
        {
            txt_Freight.ReadOnly = false;
        }

        if (keyID > 0 && MenuItemId != 194)
        {
            btn_Save_New.Enabled = false;
            btn_Save_Exit.Enabled = false;
        }

        lnk_FromServiceLocation.Visible = false;
        lnk_AddToServiceLocation.Visible = false;
    }
    private void Set_Default_Value_On_Page_Load()
    {
        hdn_MenuItemId.Value = MenuItemId.ToString();
        ClientCode = CompanyManager.getCompanyParam().ClientCode.ToLower();
        hdn_DivisionId.Value = Util.Int2String(UserManager.getUserParam().DivisionId);
        hdn_Mode.Value = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        GCCaption = CompanyManager.getCompanyParam().GcCaption.ToUpper();

        string current_time = DateTime.Now.ToShortTimeString();
        wuc_BookingTime.setFormat("24");
        wuc_BookingTime.setTime(current_time);
        Session_Mode = Util.String2Int(hdn_Mode.Value);
        Session_MenuItem = MenuItemId;
        hdn_GCId.Value = keyID.ToString();
        hdn_Rectify_GCId.Value = Util.EncryptInteger(keyID);

        //Commented By Hemant On 10 Feb 2021
        //hdn_year.Value = DateTime.Now.Year.ToString();
        //hdn_month.Value = DateTime.Now.Month.ToString();
        //hdn_date.Value = DateTime.Now.Day.ToString();

        hdn_year.Value = UserManager.getUserParam().TodaysDate.Year.ToString();
        hdn_month.Value = UserManager.getUserParam().TodaysDate.Month.ToString();
        hdn_date.Value = UserManager.getUserParam().TodaysDate.Day.ToString();
        
        hdn_LoginUserHierarchy.Value  = UserManager.getUserParam().HierarchyCode;
    }
    private void Fill_BranchCode()
    {
        Bind_DDLGC_No = CommonObj.Get_Values_Where("ec_master_branch", "Branch_Code", "", "Branch_Code", true).Tables[0];
    }

    private void Set_DefaultSessionValue()
    {
        Session_TotalRatio = 0;
        Session_InsuranceCompany = "";
        Session_PolicyAmount = 0;
        Session_RiskAmount = 0;
        Session_PolicyNo = "";
        Session_PolicyExpDate = DateTime.Now.Date;

        Session_ContainerTypeId = 0;
        Session_ContainerNoPart1 = "";
        Session_ContainerNoPart2 = "";
        Session_SealNo = "";
        Session_ReturnToYardName = "";
        Session_ReturnToYardId = 0;
        Session_NFormNo = "";

        if (keyID <= 0)
        {
            BookingBranchId = UserManager.getUserParam().MainId;
            ArrivedFromBranchId = UserManager.getUserParam().MainId;
            BookingBranch = UserManager.getUserParam().MainName;
            ArrivedFromBranch = UserManager.getUserParam().MainName;
        }
    }
    public void Get_From_Location_Details()
    {
        DataSet ds = objNewGCPresenter.Get_From_Location_Details();

        FromLocationId = 0;
        FromLocation = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            FromLocationId = Util.String2Int(ds.Tables[0].Rows[0]["Service_Location_ID"].ToString());
            FromLocation = ds.Tables[0].Rows[0]["Service_Location_Name"].ToString();
        }
    }
    protected void btn_Save_New_Click(object sender, EventArgs e)
    {
        //_flag = "SaveAndNew";
        //objNewGCPresenter.save();
       
        OnConfirm();
    }

    public void OnConfirm()
    {
        _flag = "SaveAndNew";
        Get_Next_GC_No();
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            Session["SaveAndNewAndPrint"] = "1";
        }
        else
        {
            Session["SaveAndNewAndPrint"] = "0";
        }
        objNewGCPresenter.save();
    }

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        if (keyID <= 0)
        {
            Get_Next_GC_No();
        }
        objNewGCPresenter.save();
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        Session["SaveAndNewAndPrint"] = "0";
        _flag = "SaveAndPrint";
        if (keyID <= 0)
        {
            Get_Next_GC_No();
        }
        objNewGCPresenter.save();
    }
    protected void btn_Save_Repeat_Click(object sender, EventArgs e)
    {
        Session["SaveAndNewAndPrint"] = "0";
        _flag = "SaveAndRepet";
        if (keyID <= 0)
        {
            Get_Next_GC_No();
        }

        if (validateUI())
        {
            objNewGCPresenter.Save_And_Repet_Save();
            Get_Next_GC_No();
            txt_To_Location.Text = hdn_To_Location.Value;
        }
    }
    protected void btn_ConsrId_Click(object sender, EventArgs e)
    {
        if (ConsignorId > 0)
            hdn_EncreptedConsignorId.Value = Util.EncryptInteger(ConsignorId);
        else
            hdn_EncreptedConsignorId.Value = "";
    }
    protected void btn_ConseeId_Click(object sender, EventArgs e)
    {
        if (ConsigneeId > 0)
            hdn_EncreptedConsigneeId.Value = Util.EncryptInteger(ConsigneeId);
        else
            hdn_EncreptedConsigneeId.Value = "";
    }
    protected void btn_GetAttachedGCDetails_Click(object sender, EventArgs e)
    {
        objNewGCPresenter.read_Values(true);
        btn_ConsrId_Click(sender, e);
        btn_ConseeId_Click(sender, e);
    }
    protected void ddl_pick_request_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_pick_request.SelectedValue) > 0)
        {
            lnk_Pickup_Req.Visible = true;
            hdnEncrypted_pickuprequestid.Value = Util.EncryptInteger(Util.String2Int(ddl_pick_request.SelectedValue));
        }
        else
        {
            lnk_Pickup_Req.Visible = false;
        }
    }
}