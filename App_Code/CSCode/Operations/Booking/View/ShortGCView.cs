
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
 
/// <summary>
/// Summary description for ShortGCView
/// </summary>

namespace Raj.EC.OperationView
{
    public interface IShortGCView : ClassLibraryMVP.General.IView
    {
        int GC_Id { get;set;}
        int GC_No_Length { get;set;}
        int TotalTransitDays { get;set;}
        int TotalKiloMeter{ get;set;}

        int ToLocationId { get;set;}
        int FromLocationId { get;set;}
        int LoadingSuperVisorId { get;set;}
        int MarketingExecutiveId { get;set;}
        int FreightBasisId { get;set;}
        int VolumetricFreightUnitId { get;set;}

        int BookingTypeId { get;set;}
        int BookingSubTypeId { get;set;}

        int BookingModeId { get;set;}
        int ConsignmentTypeId { get;set;}
        int DeliveryTypeId { get;set;}
        int DeliveryAgainstId { get;set;}
        int VehicleTypeId { get;set;}
        int PaymentTypeId { get;set;}
        int PickupTypeId { get;set;}
        int GCRiskId { get;set;}
        int ServiceTaxPayableBy { get;set;}

        int ConsignorId { get;set;}        
        
        int ConsignorCountryId { get;set;}
        int ConsignorStateId { get;set;}
        int ConsignorCityId { get;set;}

        int ConsigneeId { get;set;}

        int ConsigneeCountryId { get;set;}
        int ConsigneeStateId { get;set;}
        int ConsigneeCityId { get;set;}

        int BookingBranchId { get;set;}
        int ArrivedFromBranchId { get;set;}
        int TotalArticles   { get;set;}

        int VAId { get;set;}
               
        int Attached_GC_Id { get;set;}
        int ReBook_GC_Id { get;set;}

        int DocumentSeriesAllocationId { get;set;}
        int DocumentId { get;}

        int Start_No { get;set;}
        int End_No { get;set;}        
        int Next_No { get;set;}
        
        int CompanyParameter_Standard_BasicFreightUnitId { get;set;}
        
        int UnitOfMeasurementId { get;set;}
        int DeliveryBaranchId { get;set;}

        int Contractual_ClientId { get;set;}
        
        int BillingPartyId { get;set;}
        int BillingBranchId { get;set;}
        
        int Contract_BranchId { get;set;}
        int ContractId { get;set;}

        int Contract_UnitOfFreightId { get;set;}        
        int Contract_FreightBasisId { get;set;}

        int Contract_FreightSubUnitId { get;set;}
        //int Contract_Freight_Unit_Id { get;set;}
        
        int Contract_CFTFactor { get;set;}
        int Is_ContractApplied { get;set;}
        
        int FirstCommodityId { get;set;}
        int FirstPackingTypeId { get;set;}
        int FirstItemId { get;set;}

        int Old_FirstCommodityId { get;set;}
        int Old_FirstPackingTypeId { get;set;}
        int Old_FirstItemId { get;set;}

        int Is_ServiceTaxApplicableForConsignor { get;set;}
        int Is_ServiceTaxApplicableForConsignee { get;set;}

        int Is_RegularConsignor { get;set;}
        int Is_RegularConsignee { get;set;}

        int RoadPermitTypeId { get;set;}

        int GC_Status_Id_At_Current_Branch { get;set;}
        int GC_Articles_At_Current_Branch  { get;set;}

        int Previous_Article_ID { get;set;}
	    int Previous_Status_ID  { get;set;}
	    int Previous_Document_ID { get;set;}

        int LengthChargeHeadId { get;set;}

        int ReBook_GCOctroiPaidByID { get;set;}
        
        int Default_Booking_Type { get;set;}
        int Default_Payment_Type { get;set;}
        int Default_Delivery_Type { get;set;}
        int Default_Road_Permit_Type { get;set;}
        int Default_Measurment_Unit { get;set;}
        int Default_Freight_Basis { get;set;}
        int Default_Risk_Type { get;set;}

        int Default_Consignment_Type { get;set;}
        int Default_Pickup_Type { get;set;}
        int Default_Commodity_Weight { get;set;}

        int Default_Cash_Ledger_Id{ get;set;}
        int Default_Bank_Ledger_Id { get;set;}      

        int Valid_Cheque_End_Days { get;set;}      
        int Valid_Cheque_Start_Days { get;set;}
        int Remark_Max_Length { get;set;}      

        //----------------------------------------------------------------------------------------        

        String Default_Cheque_Branch_Ledger_Name { get;set;}
        String Default_Cheque_Bank_Ledger_Name { get;set;}
        String Default_Cash_Ledger_Name { get;set;}
        
        String LoadingSuperVisor_RequiredFor_BookingType { get;set;}
        String Container_Details_RequiredFor_BookingType { get;set;}

        String ServiceTax_Label { set;}
        String Previous_Document_No_For_Print { get;set;}
        
        String GC_No_For_Print { get;set;}
        String GC_No { get;set;}
        String Attached_GC_No_For_Print { get;set;}

        String PrivateMark { get;set;}

        String BankName { get;set;}
        String ChequeNo { get;set;}

        String VehicleNo { get;set;}
        String STMNo { get;set;}
        String FeasibilityRouteSurveyNo { get;set;}
        String CustomerRefNo { get;set;}
        String OtherChargesRemark { get;set;}
        String Enclosures { get;set;}
        String InstructionRemark { get;set;}

        String ConsignorCountryName { get;set;}
        String ConsignorStateName { get;set;}
        String ConsigneeCountryName { get;set;}        
        String ConsigneeStateName { get;set;}

        String EncreptedConsignorId { get;set;}
        String EncreptedConsigneeId { get;set;}
        
        String ConsigneeDDAddressLine1 { get;set;}
        String ConsigneeDDAddressLine2 { get;set;}
        
        String Session_ConsigneeAddressLine1 { get;set;}
        String Session_ConsigneeAddressLine2 { get;set;}
        String Session_ConsigneeName { get;set;}

        String DeliveryBranchName { get;set;}
     //   String hdn_DeliveryBaranchName { get;set;}

        String Series { get;set;}
        String BillingRemark { get;set;}

        String RoadPermitSrNo { get;set;}

        String ClientCode { get;set;}
        String RegularClientCaption { get;set;}
        //----------------------------------------------------------------------------------------

        DataTable  Bind_dg_Commodity { set;}
        DataTable  Bind_dg_Invoice { set;}

        //----------------------------------------------------------------------------------------

        void SetBookingBranch(string text, string value);
        void SetArrivedFromBranch(string text, string value);              

        void SetFromLocation(string text, string value);
        void SetToLocation(string text, string value);
        void SetConsingor(string text, string value);
        void SetConsingee(string text, string value);        
        void SetBillingBranch(string text, string value);
        void SetBillingParty(string text, string value);
        void SetContractualClient(string text, string value);

        //void SetContractBranch(string text, string value);
        
        void SetLoadingSuperVisor(string text, string value);
        void SetMarketingExecutive(string text, string value);

        //----------------------------------------------------------------------------------------

        Boolean Is_POD_Checked { get;set;}
        Boolean Is_POD_Disabled { get;set;}

        Boolean Is_Opening_GC { get;set;}
        Boolean Is_ODA { get;set;}
        Boolean Is_OctroiApplicable { get;set;}
        Boolean Is_ToPayBookingApplicable { get;set;}
        Boolean Is_ReBookGC_ToPay { get;set;}

        Boolean Is_POD { get;set;}
        Boolean Is_DACC { get;set;}
        Boolean Is_Cheque { get;}

        Boolean Is_SignedByConsignor { get;set;}

        Boolean Is_Service_Tax_Applicable_For_Commodity { get;set;}
        Boolean Is_Attached { get;set;}
        Boolean Is_ReBooked { get;set;}
        Boolean Is_MultipleBilling { get;set;}

        Boolean Is_ReBookGC_Octroi_Updated { get;set;}
        Boolean Is_ReBookGC_Octroi_Applicable { get;set;}

        Boolean Is_GCNumberEditable { get;set;}
        Boolean Is_Insured { get;set;}
        Boolean Is_Contract_Required_For_TBB_GC { get;set;}
        Boolean Is_Invoice_Amount_Required { get;set;}
        Boolean Is_FOV_Calculated_As_Per_Standard { get;set;}
        Boolean Is_Auto_Booking_MR_For_Paid_Booking { get;set;}

        Boolean Is_ToPay_Charge_Require { get;set;}
        Boolean Is_Consignor_Consignee_Details_Shown { get;set;}
        Boolean Is_Validate_Freight_On_Article { get;set;}

        Boolean Is_Item_Required { get;set;}
        Boolean Is_Validate_Credit_Limit { get;set;}
        Boolean Session_Is_Validate_Credit_Limit { get;set;}
         
        //----------------------------------------------------------------------------------------

        DataTable BindBookingBranch { set;}
        DataTable BindArrivedFromBranch { set;}

        DataTable BindFromLocation { set;}
        DataTable BindToLocation { set;}
        DataTable BindConsignor { set;}
        DataTable BindConsignee { set;}
        DataTable BindCommodity { set;}
        DataTable BindItem { set;}
        DataTable BindPackingType { set;}
        DataTable BindDDLGC_NO { set;}
        DataTable BindUnitOfMeasurement { set;}
        DataTable BindFreightBasis { set;}
        DataTable BindVolumetricFreightUnit { set;}
        DataTable BindBookingType { set;}
        DataTable BindDeliveryType { set;}
        DataTable BindVehicleType { set;}         
        DataTable BindConsignmentType{ set;}
        DataTable BindPaymentType { set;}
        DataTable BindGCRiskType { set;}
        //DataTable BindMartkeingExecutive { set;}
        //DataTable BindSupervisor { set;}
        DataTable BindDeliveryAgainst { set;}
        DataTable BindPickupType { set;}

       // DataTable BindBookingType { set;}

        DataTable BindRoadPermitType { set;}
        DataTable BindGCInstructions { set;}
        DataTable BindBookingSubType { set;}        
        DataTable BindMarketingExecutive { set;}
        DataTable BindLengthChargeHead { set;}

        //----------------------------------------------------------------------------------------

        DataSet Session_DS_ContractDetails { get;set;}

        //----------------------------------------------------------------------------------------

        DataTable Session_CommodityDdl { get;set;}
        DataTable Session_ItemDdl { get;set;}
        DataTable Session_PackingTypeDdl { get;set;}

        DataTable Session_GCOtherChargeHead { get;set;}
        DataTable Session_BillingDetailsGrid { get;set;}
        DataTable Session_Main_BillingDetailsGrid { get;set;}

        DataTable Session_ChequeDetailsGrid { get;set;}

        DataTable Session_MultipleCommodityGrid { get;set;}
        DataTable Session_InvoiceGrid { get;set;}

        DataTable Session_ContainerType { get;set;}

        //----------------------------------------------------------------------------------------

        DataSet Session_RequireForms { get;set;}
        DataSet Session_ContractualClientDetails { get;set;}
         
        DataSet BindContractBranches { set;}
        DataSet BindContract { set;}

        //----------------------------------------------------------------------------------------
        int Billing_Party_Ledger_Id { get;set;}

        Decimal Billing_Party_Credit_Limit { get;set;}
        Decimal Billing_Party_Closing_Balance { get;set;}
        
        Decimal CompanyParameter_Standard_FreightRatePer { get;set;}
        Decimal TotalWidth { get;set;}
        Decimal TotalWeight { get;set;}
        Decimal TotalLength { get;set;}
        Decimal TotalHeight { get;set;}
        Decimal ChargeWeight { get;set;}
        Decimal ActualWeight { get;set;}
        Decimal ODAChargesUpTo500Kg { get;set;}
        Decimal ODAChargesAbove500Kg { get;set;}
        Decimal LocalCharge { get;set;}
        Decimal LoadingCharge { get;set;}
        Decimal StationaryCharge { get;set;}
        Decimal MaxStationaryCharge { get;set;}
        Decimal ToPayCharge { get;set;}
        Decimal DDCharge { get;set;}
        Decimal DACCCharges { get;set;}
        Decimal OtherCharges { get;set;}
        Decimal hdn_StandardMinimumChargeWeight { get;set;}
        Decimal hdn_StandardHamaliPerKg { get;set;}
        Decimal hdn_StandardMinimumFOV { get;set;}
        //Decimal hdn_HamaliPerKg { get;set;}  
        //Decimal hdn_CFTFactor { get;set;} 
       // Decimal hdn_FOVPercentage { get;set;}
        Decimal FreightRate { get;set;}
        Decimal TotalCFT { get;set;}
        Decimal TotalCBM { get;set;}
        Decimal Freight { get;set;}
        Decimal FOVRiskCharge { get;set;}
        Decimal SubTotal { get;set;}
        Decimal Abatment { get;set;}
        Decimal TaxableAmount { get;set;}
        Decimal ServiceTax { get;set;}

        Decimal ReBookGC_OctroiAmount { get;set;}
        Decimal TotalGCAmount { get;set;}
        Decimal Previous_SubTotal { get;set;}
        Decimal Previous_GrandTotal { get;set;}
        Decimal ReBookGC_Amount { get;set;}
        Decimal LengthCharge { get;set;}

        Decimal NFormCharge { get;set;}
        Decimal UnloadingCharge { get;set;}

        Decimal Advance { get;set;}
        Decimal CashAmount { get;set;}
        Decimal ChequeAmount { get;set;}
        Decimal TotalInvoiceAmount { get;set;}
        Decimal VolumetricToKgFactor { get;set;} // CFT Factor
        Decimal PolicyAmount { get;set;}
        Decimal RiskAmount { get;set;}        
        Decimal TaxAbatePercent { get;set;}

        Decimal ReBookGC_SubTotal { get;set;}

        // ------------------------ Standard Charges  ( 12 Charges )------------------------

        Decimal Standard_CFTFactor { get;set;}
        Decimal Standard_BiltiCharges { get;set;}
        Decimal Standard_DDCharge_Rate { get;set;}
        Decimal Standard_DDCharge { get;set;}        
        Decimal Standard_DACCCharges { get;set;}
        Decimal Standard_FOV { get;set;}
        Decimal Standard_FOVPercentage { get;set;}
        //Decimal hdn_CFTFactor { get;set;}      
        Decimal Standard_FreightAmount { get;set;}        
        Decimal Standard_FreightRate { get;set;}
        Decimal Special_FreightRate { get;set;}
        Decimal Standard_HamaliCharge { get;set;}
        Decimal Standard_LocalCharge_Rate { get;set;}
        Decimal Standard_LocalCharge { get;set;}
        Decimal Standard_ServiceTaxAmount { get;set;}
        Decimal Standard_ToPayCharges { get;set;}
        Decimal Standard_MinimumChargeWeight { get;set;}
        Decimal Standard_HamaliPerKg { get;set;}
        Decimal Standard_HamaliPerArticles { get;set;}

        Decimal Standard_MinimumFOV { get;set;}
        Decimal Standard_ServiceTaxPercent { get;set;}



        Decimal Standard_FOVRate { get;set;}

        Decimal Standard_Invoice_Rate { get;set;}
        Decimal Standard_Invoice_Per_How_Many_Rs { get;set;}


        Decimal Additional_Freight { get;set;}

        Decimal Standard_Octroi_Form_Charge { get;set;}
        Decimal Standard_Octroi_Service_Charge  { get;set;}
        Decimal Standard_GI_Charges { get;set;}
        Decimal Standard_Demurrage_Days { get;set;}         
        Decimal Standard_Demurrage_Rate { get;set;}
        Decimal Standard_LengthCharge { get;set;}

        Decimal Standard_NForm_Charge { get;set;}

        Decimal Freight_Charge_Discount_Percent { get;set;}
        Decimal Hamali_Charge_Discount_Percent { get;set;}
        Decimal Fov_Charge_Discount_Percent { get;set;}
        Decimal ToPay_Charge_Discount_Percent { get;set;}
        Decimal DD_Charge_Discount_Percent { get;set;}

        // ------------------------ Contractual Charges  ( 18 Charges )------------------------

        Decimal Contractual_BiltiCharges { get;set;}
        Decimal Contractual_DDCharge_Rate { get;set;}
        Decimal Contractual_DDCharge { get;set;}
        Decimal Contractual_DACCCharges { get;set;}
        Decimal Contractual_FOV { get;set;}
        Decimal Contractual_FOVPercentage { get;set;}
        Decimal Contractual_CFTFactor { get;set;}
        Decimal Contractual_FreightAmount { get;set;}
        Decimal Contractual_FreightRate { get;set;}
        //Decimal Special_Freight_Rate { get;set;}
        Decimal Contractual_HamaliCharge { get;set;}
        Decimal Contractual_LocalCharge_Rate { get;set;}
        Decimal Contractual_LocalCharge { get;set;}
        Decimal Contractual_ServiceTaxAmount { get;set;}
        Decimal Contractual_ToPayCharges { get;set;}
        Decimal Contractual_ServiceTaxPercent { get;set;}
        Decimal Contractual_MinimumFOV { get;set;}
        Decimal Contractual_MinimumChargeWeight { get;set;}
        Decimal Contractual_MinimumHamaliPerKg { get;set;}
        Decimal Contractual_MinFOV { get;set;}
        Decimal Contractual_HamaliPerKg { get;set;}
        Decimal Contractual_HamaliPerArticles { get;set;}
        //Decimal Contractual_CFT_Factor { get;set;}
        //Decimal Contractual_DACCCharges { get;set;}
        
        Decimal Contractual_Octroi_Form_Charge { get;set;}
        Decimal Contractual_Octroi_Service_Charge { get;set;}
        Decimal Contractual_GI_Charges { get;set;}
        Decimal Contractual_Demurrage_Days { get;set;}
        Decimal Contractual_Demurrage_Rate { get;set;}
        Decimal Contractual_LengthCharge { get;set;}


        Decimal Contractual_NForm_Charge { get;set;}

        Decimal Contractual_FOVRate { get;set;}

        Decimal Contractual_Invoice_Rate { get;set;}
        Decimal Contractual_Invoice_Per_How_Many_Rs { get;set;}

        // ------------------------ Applicable Standard Charges ( 18 Charges ) ------------------------

        Decimal Applicable_Standard_BiltiCharges { get;set;}
        Decimal Applicable_Standard_DDCharge_Rate { get;set;}
        Decimal Applicable_Standard_DDCharge { get;set;}
        Decimal Applicable_Standard_DACCCharges { get;set;}
        Decimal Applicable_Standard_FOV { get;set;}
        Decimal Applicable_Standard_FOVPercentage { get;set;}
        Decimal Applicable_Standard_FreightAmount { get;set;}
        Decimal Applicable_Standard_FreightRate { get;set;}
        //Decimal Special_Freight_Rate { get;set;}
        Decimal Applicable_Standard_HamaliCharge { get;set;}
        Decimal Applicable_Standard_LocalCharge_Rate { get;set;}
        Decimal Applicable_Standard_LocalCharge { get;set;}
        Decimal Applicable_Standard_ServiceTaxAmount { get;set;}
        Decimal Applicable_Standard_ToPayCharges { get;set;}
        Decimal Applicable_Standard_ServiceTaxPercent { get;set;}
        Decimal Applicable_Standard_MinimumFOV { get;set;}
        Decimal Applicable_Standard_MinimumChargeWeight { get;set;}
        Decimal Applicable_Standard_MinimumHamaliPerKg { get;set;}
        //Decimal Applicable_Standard_MinimumFOV { get;set;}
        Decimal Applicable_Standard_HamaliPerKg { get;set;}
        Decimal Applicable_Standard_HamaliPerArticles { get;set;}
        Decimal Applicable_Standard_CFTFactor { get;set;}
        //Decimal Applicable_Standard_DACCCharges { get;set;}
        Decimal Applicable_Standard_Octroi_Form_Charge { get;set;}
        Decimal Applicable_Standard_Octroi_Service_Charge { get;set;}
        Decimal Applicable_Standard_GI_Charges { get;set;}
        Decimal Applicable_Standard_Demurrage_Days { get;set;}
        Decimal Applicable_Standard_Demurrage_Rate { get;set;}
        Decimal Applicable_Standard_LengthCharge { get;set;}

        Decimal Applicable_Standard_NForm_Charge { get;set;}

        Decimal Applicable_Standard_FOVRate { get;set;}
        Decimal Applicable_Standard_Invoice_Rate { get;set;}
        Decimal Applicable_Standard_Invoice_Per_How_Many_Rs { get;set;}


        // --------------------------
        
        Decimal UnitOfMeasurmentHeight { get;set;}
        Decimal UnitOfMeasurmentLength { get;set;}
        Decimal UnitOfMeasurmentWidth { get;set;}
        Decimal HeightInFeet { get;set;}
        Decimal LengthInFeet { get;set;}
        Decimal WidthInFeet { get;set;}

        //Decimal TotalInvoiceAmount { get;set;}

        //----------------------------------------------------------------------------------------

        String In_Valid_Credit_Limit_Client_Name { get;set;}

        String ConsignorName { get;set;}
        String ConsignorAddressLine1 { get;set;}
        String ConsignorAddressLine2 { get;set;}
        String ConsignorCity { get;set;}
        String ConsignorPinCode { get;set;}
        String ConsignorTelNo { get;set;}
        String ConsignorMobileNo { get;set;}
        String ConsignorEmail { get;set;}
        String ConsignorCSTNo { get;set;}

        String ConsignorDetails { get;set;}
        //  String ConsignorTINNo { get;set;}
        
        String ConsigneeName { get;set;}
        String ConsigneeAddressLine1 { get;set;}
        String ConsigneeAddressLine2 { get;set;}
        String ConsigneeCity { get;set;}
        String ConsigneePinCode { get;set;}
        String ConsigneeTelNo { get;set;}
        String ConsigneeMobileNo { get;set;}
        String ConsigneeEmail { get;set;}
        String ConsigneeCSTNo { get;set;}

        String ConsigneeDetails { get;set;}
        //  String ConsigneeTINNo { get;set;}

        String InsuranceCompany { get;set;}
        String PolicyNo { get;set;}
        String MultipleCommodityXml { get;}
        String InvoiceXml { get;}
        String OtherChargesXml { get;}
        String BillingDetailsXml { get;}

        String ChequeDetailsXml { get;}

        String Flag { get;}
        //----------------------------------------------------------------------------------------
                
        String  BookingTime { get;set;}
         
        //DateTime BookingDate { get;set;}
 
        DateTime PolicyExpDate { get;set;}
        DateTime ChequeDate { get;set;}
        DateTime ExpectedDeliveryDate { get;set;}
        DateTime BookingDate { get;set;}
        DateTime Previous_Document_Date { get;set;}

        DateTime ArrivedDate { get;set;}

        DateTime ApplicationStartDate { get;set;}

        // -------------------------

        Decimal Session_PolicyAmount{ get;set;}
        Decimal Session_RiskAmount { get;set;}
        DateTime Session_PolicyExpDate { get;set;}
        String Session_InsuranceCompany { get;set;}
        String Session_PolicyNo { get;set;}
        
        Int32 Session_ContainerTypeId { get;set;}
        Int32 Session_ReturnToYardId { get;set;}

        String Session_ReturnToYardName { get;set;}
        String Session_ContainerNoPart1 { get;set;}
        String Session_ContainerNoPart2 { get;set;}
        String Session_SealNo { get;set;}
        String Session_NFormNo { get;set;}    

        //int Session_GCRiskId { get;set;}
   
        // ---------------
    } 
}