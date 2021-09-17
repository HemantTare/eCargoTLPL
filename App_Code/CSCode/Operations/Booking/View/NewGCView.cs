using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.OperationView;
using ClassLibraryMVP.General;
/// <summary>
/// Summary description for NewGCView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface INewGCView : IView
    {
        int GC_No_Length { get;set;}
        int FromLocationId { get;set;}
        int ToLocationId { get;set;}
        int DeliveryBranchId { get;set;}
        int PickupTypeId { get;set;}
        int DeliveryWayTypeId { get;set;}
        int BookingTypeId { get;set;}
        int BookingSubTypeId { get;set;}
        int BookingModeId { get;set;}
        int DeliveryTypeId { get;set;}
        int ConsignmentTypeId { get;set;}
        int defaultConsignmentType { get;set;}

        int DeliveryAgainstId { get;set;}
        int RoadPermitTypeId { get;set;}
        int PaymentTypeId { get;set;}
        int ReasonFreightPendingId { get;set;}

        string PaidFreightPendingPersonName { get;set;}
        string PaidFreightPendingPersonMobile { get;set;}

        int GCRiskId { get;set;}
        int defaultGCRiskType { get;set;}
        int ServiceTypeId { get;set;}

        int ServiceTaxPayableBy { get;set;}
        int LengthChargeHeadId { get;set;}
        int UnitOfMeasurementId { get;set;}
        int FreightBasisId { get;set;}
        int defaultFreightBasisId { get;set;}
        int VolumetricFreightUnitId { get;set;}
        int LoadingSuperVisorId { get;}
        int MarketingExecutiveId { get;}
        int VehicleTypeId { get;set;}
        int ConsignorId { get;set;}
        int ConsigneeId { get;set;}
        int AgencyId { get;set;}
        int AgencyLedgerId { get;set;}
        int CRMPickupRequestId { get;set;}
        int BookingBranchId { get;set;}
        int ArrivedFromBranchId { get;set;}
        decimal ItemValueForFOV { get;set;}
        int TotalArticles { get;set;}
        int BillingPartyId { get;set;}
        int BillingLocationId { get;set;}
        int CompanyParameter_Standard_BasicFreightUnitId { get;set;}
        int BillingParty_LedgerId { get;set;}
        int VAId { get;set;}
        int DocumentSeriesAllocationId { get;set;}
        int DocumentId { get;}
        int Start_No { get;set;}
        int End_No { get;set;}
        int Next_No { get;set;}

        int Attached_GC_Id { get;set;}
        //int ReBook_GC_Id { get;set;}

        decimal BillingParty_CreditLimit { get;set;}
        decimal BillingParty_ClosingBalance { get;set;}
        decimal BillingParty_MinimumBalance { get;set;}
        decimal BillingParty_Ledger_Closing_Balance { get;set;}
        decimal CompanyParameter_Standard_FreightRatePer { get;set;}

        int Contractual_ClientId { get;set;}
        string Contractual_Client { get;set;}
        int Contract_BranchId { get;set;}
        int ContractId { get;set;}
        int Is_ContractApplied { get;set;}

        int RateContractId { get;set;}

        int Default_Cash_Ledger_Id { get;set;}
        int Default_Bank_Ledger_Id { get;set;}
        int Valid_Cheque_Start_Days { get;set;}
        int Valid_Cheque_End_Days { get;set;}

        //int Remark_Max_Length { get;set;}
        //int ReBook_GCOctroiPaidByID { get;set;}

        ////----------------------------------------------------------------------------------------        

        string Default_Cheque_Branch_Ledger_Name { get;set;}
        string Default_Cheque_Bank_Ledger_Name { get;set;}
        string LoadingSuperVisor_RequiredFor_BookingType { get;set;}
        string Container_Details_RequiredFor_BookingType { get;set;}
        string Attached_GC_No { get;set;}

        string CustomerRefNo { get;set;}
        string Flag { get;}
        string VehicleNo { get;set;}
        string ServiceTax_Label { set;}
        string GC_No_For_Print { get;set;}
        string ddl_GC_No_For_Print { get;set;}
        string STMNo { get;set;}
        string PrivateMark { get;set;}
        string FeasibilityRouteSurveyNo { get;set;}
        string RoadPermitSrNo { get;set;}
        string OtherChargesRemark { get;set;}
        string Enclosures { get;set;}
        string InstructionRemark { get;set;}
        string ChequeNo { get;set;}
        string BankName { get;set;}
        string eWayBillNo { get;set;}
        bool Is_MultipleeWayBill { get;set;}
        string FromLocation { set;}
        string ToLocation { set;}
        string ConsignorName { set;}
        string ConsigneeName { set;}
        string Consignee_CSTTINNo { set;}
        string BookingBranch { set;}
        string ArrivedFromBranch { set;}
        string AgencyName { set;}
        string AgencyLedger { set;}
        string Agency_GC_No { get;set;}
        string ConsignorAddressValue { set;}
        string ConsigneeAddressValue { set;}

        string ConsignorPhoneNumbers { set;}
        string ConsigneePhoneNumbers { set;}

        string ConsignorMobileNumbers { set;}
        string ConsigneeMobileNumbers { set;}

        string ConsigneeDDAddress1 { get;set;}
        string ConsigneeDDAddress2 { get;set;}
        string DeliveryBranchName { set;}
        string BookingTime { get;set;}
        string ClientCode { get;set;}
        string BillingHierarchy { get;set;}
        string BillingRemark { get;set;}
        string BillingParty { set;}
        string BillingLocation { set;}

        int WholeselerId { get;set;}
        string Wholeseler { set;}

        //string Series { get;set;}
        //Boolean Is_ReBookGC_ToPay { get;set;}
        bool Is_Attached { get;set;}
        //Boolean Is_ReBookGC_Octroi_Updated { get;set;}
        //Boolean Is_ReBookGC_Octroi_Applicable { get;set;}

        bool Is_Cheque { get;}
        bool Is_ODA { get;set;}
        bool Is_GCNumberEditable { get;set;}
        bool Is_Contract_Required_For_TBB_GC { get;set;}
        bool Is_Invoice_Amount_Required { get;set;}
        bool Is_Item_Required { get;set;}
        bool Is_Validate_Credit_Limit { get;set;}
        bool Is_POD { get;set;}
        bool Is_MultipleBilling { get;set;}
        bool Is_SignedByConsignor { get;set;}
        bool Is_OctroiApplicable { get;set;}
        bool Is_ToPayBookingApplicable { get;set;}
        bool Is_Insured { get;set;}
        bool Is_ServiceTaxApplicableForConsignor { get;set;}
        bool Is_ServiceTaxApplicableForConsignee { get;set;}
        bool Is_RegularConsignor { get;set;}
        bool Is_RegularConsignee { get;set;}
        int ConsignorStateId { get;set;}
        int ConsigneeStateId { get;set;}
        bool Is_DefaultPOD_Checked { get;set;}
        bool Is_POD_Disabled { set;}
        bool Is_FOV_Calculated_As_Per_Standard { get;set;}
        bool Is_Auto_Booking_MR_For_Paid_Booking { get;set;}
        bool Is_ToPay_Charge_Require { get;set;}
        bool Is_Consignor_Consignee_Details_Shown { get;set;}
        bool Is_Multiple_Location_Billing_Allowed { get;set;}
        bool Is_Multiple_Party_Billing_Allowed { get;set;}
        bool Is_Service_Tax_Payable_For_BillingParty { get;set;}
        bool Is_Service_Tax_Applicable_For_Commodity { get;set;}
        bool Is_ST_Abatment_Required { get;set;}

        bool IsCC_PaidAllowed { get;set;}
        bool IsCC_ToPayAllowed { get;set;}
        bool IsCC_FOCAllowed { get;set;}
        bool IsCC_TBBAllowed { get;set;}
        bool Is_Consignee_Is_To_Pay_Allowed { get;set;}

        //bool Is_Validate_Freight_On_Article { get;set;}
        ////----------------------------------------------------------------------------------------
        
        decimal UnitOfMeasurmentLength { get;set;}
        decimal UnitOfMeasurmentHeight { get;set;}
        decimal UnitOfMeasurmentWidth { get;set;}
        decimal LengthInFeet { get;set;}
        decimal HeightInFeet { get;set;}
        decimal WidthInFeet { get;set;}
        decimal TotalCFT { get;set;}
        decimal TotalCBM { get;set;}
        decimal VolumetricToKgFactor { get;set;}

        decimal TotalWeight { get;set;}
        decimal ActualWeight { get;set;}
        decimal ChargeWeight { get;set;}
        decimal FreightRate { get;set;}

        decimal Freight { get;set;}
        decimal Discount { get;set;}
        int DiscountId { get;set;}
        int ConsigneeDeliveryAreaID { get;set;}
        string ConsigneeDeliveryAreaName { set;}
        decimal LocalCharge { get;set;}
        decimal LoadingCharge { get;set;}
        decimal StationaryCharge { get;set;}
        decimal FOVRiskCharge { get;set;}
        decimal ToPayCharge { get;set;}
        decimal DDCharge { get;set;}
        decimal DACCCharges { get;set;}
        decimal OtherCharges { get;set;}
        decimal NFormCharge { get;set;}
        decimal ReBookGCCharges { get;set;}
        decimal ODACharges { get;set;}
        decimal ODAChargesUpTo500Kg { get;set;}
        decimal ODAChargesAbove500Kg { get;set;}
        decimal LengthCharge { get;set;}
        decimal UnloadingCharge { get;set;}
        decimal AOCPercent { get;set;}
        decimal AOC { get;set;}
        int RoundOff { get;set;}
        decimal SubTotal { get;set;}
        decimal TaxAbatePercent { get;set;}
        decimal Abatment { get;set;}
        decimal TaxableAmount { get;set;}
        decimal ServiceTax { get;set;}
        decimal ActualServiceTax { get;set;}
        decimal ReBookGC_OctroiAmount { get;set;}
        decimal TotalGCAmount { get;set;}
        decimal Advance { get;set;}
        decimal CashAmount { get;set;}
        decimal ChequeAmount { get;set;}
        decimal TotalInvoiceAmount { get;set;}
       
        //Decimal Previous_SubTotal { get;set;}
        //Decimal Previous_GrandTotal { get;set;}
        //Decimal ReBookGC_SubTotal { get;set;}

        //// ------------------------ Standard Charges  ( 12 Charges )------------------------

        decimal RateCard_MinimumChargeWeight { get;set;}
        decimal RateCard_BiltiCharges { get;set;}
        decimal RateCard_MaxBiltyCharge { get;set;}
        decimal RateCard_FOV { get;set;}
        decimal RateCard_FOVPercentage { get;set;}
        decimal RateCard_FOVRate { get;set;}
        decimal RateCard_Fov_Charge_Discount_Percent { get;set;}
        decimal RateCard_ToPayCharges { get;set;}
        decimal RateCard_DACCCharges { get;set;}
        decimal RateCard_LocalCharge { get;set;}
        decimal RateCard_HamaliCharge { get;set;}
        decimal RateCard_HamaliPerKg { get;set;}
        decimal RateCard_HamaliPerArticles { get;set;}
        decimal RateCard_Hamali_Charge_Discount_Percent { get;set;}
        decimal RateCard_DDCharge_Rate { get;set;}
        decimal RateCard_DDCharge { get;set;}
        decimal RateCard_DD_Charge_Discount_Percent { get;set;}
        decimal RateCard_CFTFactor { get;set;}
        decimal RateCard_Octroi_Form_Charge { get;set;}
        decimal RateCard_Octroi_Service_Charge { get;set;}
        decimal RateCard_GI_Charges { get;set;}
        decimal RateCard_Demurrage_Days { get;set;}
        decimal RateCard_Demurrage_Rate { get;set;}
        decimal RateCard_Invoice_Rate { get;set;}
        decimal RateCard_Invoice_Per_How_Many_Rs { get;set;}
        decimal RateCard_Freight_Charge_Discount_Percent { get;set;}
        decimal RateCard_ToPay_Charge_Discount_Percent { get;set;}
        decimal RateCard_AOC_Percent { get;set;}

        //// ------------------------ Applicable Standard Charges ( 18 Charges ) ------------------------

        decimal Standard_FreightRate { get;set;}
        decimal Standard_FreightAmount { get;set;}
        decimal Standard_HamaliCharge { get;set;}
        decimal Standard_DDChargeRate { get;set;}
        decimal Standard_DDCharge { get;set;}
        decimal Standard_FOV { get;set;}
        decimal Standard_ServiceTaxPercent { get;set;}
        decimal Standard_ServiceTaxAmount { get;set;}
        decimal Standard_LengthCharge { get;set;}

        ////----------------------------------------------------------------------------------------

        //String In_Valid_Credit_Limit_Client_Name { get;set;}
        string MultipleCommodityXml { get;}
        string InvoiceXml { get;}
        string OtherChargesXml { get;}
        string BillingDetailsXml { get;}
        string ChequeDetailsXml { get;}

        DateTime ChequeDate { get;set;}
        DateTime BookingDate { get;set;}
        DateTime ArrivedDate { get;set;}
        DateTime ExpectedDeliveryDate { get;set;}
        DateTime ApplicationStartDate { get;set;}
        DateTime GCDate_ForRectify { get;set;} 

        // -------------------------

        int Session_ContainerTypeId { get;set;}
        int Session_ReturnToYardId { get;set;}
        decimal Session_PolicyAmount { get;set;}
        decimal Session_RiskAmount { get;set;}
        DateTime Session_PolicyExpDate { get;set;}
        string Session_InsuranceCompany { get;set;}
        string Session_PolicyNo { get;set;}
        string Session_ReturnToYardName { get;set;}
        string Session_ContainerNoPart1 { get;set;}
        string Session_ContainerNoPart2 { get;set;}
        string Session_SealNo { get;set;}
        string Session_NFormNo { get;set;}
     // ------------------------------------------

        void SetLoadingSuperVisor(string text, string value);
        void SetMarketingExecutive(string text, string value);

     // ------------------------------------------
        DataTable BindContractBranches { set;}
        DataTable BindContract { set;}
        DataTable BindPickupType { set;}
        DataTable BindDeliveryWayType { set;}
        DataTable BindBookingType { set;}
        DataTable BindBookingSubType { set;}
        DataTable BindDeliveryType { set;}
        DataTable BindConsignmentType { set;}
        DataTable BindDeliveryAgainst { set;}
        DataTable BindRoadPermitType { set;}
        DataTable BindPaymentType { set;}
        DataTable BindReasonFreightPending { set;}
        DataTable BindGCRiskType { set;}
        DataTable BindUnitOfMeasurement { set;}
        DataTable BindFreightBasis { set;}
        DataTable BindVolumetricFreightUnit { set;}
        DataTable BindLengthChargeHead { set;}
        DataTable BindGCInstructions { set;}
        DataTable BindBillingHierarchy { set;}
        DataTable BindVehicleType { set;}
        DataTable BindServiceType { set;}
        DataTable Session_ContainerType { get;set;}
        DataTable Session_GCOtherChargeHead { get;set;}
        DataTable Session_BillingDetailsGrid { get;set;}
        DataTable Session_CommodityDdl { get;set;}
        DataTable Session_SizeDdl { get;set;}
        DataTable Session_PackingTypeDdl { get;set;}
        DataTable Session_MultipleCommodityGrid { get;set;}
        DataTable Session_ChequeDetailsGrid { get;set;}
        DataTable Session_InvoiceGrid { get;set;}
    } 
}