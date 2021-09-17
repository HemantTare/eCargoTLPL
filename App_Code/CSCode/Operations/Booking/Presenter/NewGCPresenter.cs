using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP;
using Raj.EC;
/// <summary>
/// Summary description for NewGCPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class NewGCPresenter : Presenter
    {
        private INewGCView objINewGCView;
        private NewGCModel objNewGCModel;
        private DataSet objDS;
        Common objComm = new Common();

        public NewGCPresenter(INewGCView NewGCView, bool IsPostBack)
        {
            objINewGCView = NewGCView;
            objNewGCModel = new NewGCModel(objINewGCView);

            base.Init(objINewGCView, objNewGCModel);

            if (!IsPostBack)
            {
                objINewGCView.BookingDate = DateTime.Now.Date;
                objINewGCView.ChequeDate = DateTime.Now.Date;
                objINewGCView.ArrivedDate = DateTime.Now.Date;

                initValues();
            }
        }

        public void fillValues()
        {
            objNewGCModel.Get_CompanyParameterDetails();
            objNewGCModel.Get_VAId();
            objDS = objNewGCModel.Fill_Values();

            objINewGCView.BindPickupType = objDS.Tables[0];
            objINewGCView.BindDeliveryWayType = objDS.Tables[1];
            objINewGCView.BindBookingType = objDS.Tables[2];
            objINewGCView.BindBookingSubType = objDS.Tables[3];
            objINewGCView.BindDeliveryType = objDS.Tables[4];
            objINewGCView.BindConsignmentType = objDS.Tables[5];
            objINewGCView.BindDeliveryAgainst = objDS.Tables[6];
            objINewGCView.BindRoadPermitType = objDS.Tables[7];
            objINewGCView.BindPaymentType = objDS.Tables[8];
            objINewGCView.BindGCRiskType = objDS.Tables[9];
            objINewGCView.BindUnitOfMeasurement = objDS.Tables[10];
            objINewGCView.BindFreightBasis = objDS.Tables[11];
            objINewGCView.BindVolumetricFreightUnit = objDS.Tables[12];
            objINewGCView.BindLengthChargeHead = objDS.Tables[13];
            objINewGCView.BindGCInstructions = objDS.Tables[14];
            objINewGCView.BindBillingHierarchy = objDS.Tables[15];
            objINewGCView.BindVehicleType = objDS.Tables[16];
            objINewGCView.Session_ContainerType = objDS.Tables[17];
            objINewGCView.Session_GCOtherChargeHead = objDS.Tables[18];
            objINewGCView.Session_CommodityDdl = objDS.Tables[19];
            objINewGCView.Session_PackingTypeDdl = objDS.Tables[20];
            objINewGCView.BindServiceType = objDS.Tables[21];
            objINewGCView.Session_SizeDdl = objDS.Tables[22];
            objINewGCView.BindReasonFreightPending  = objDS.Tables[23];
        }

        private void initValues()
        {
            Get_Application_Start_Date();
            fillValues();
            Get_Company_GC_Parameter();
            read_Values(false);
            Get_Applicable_Service_Tax();

            if (Common.GetMenuItemId() == 194) // get max GC Date for Rectification
            {
                objNewGCModel.getValidGCDate_ForRectify();
            }
            //Get_BranchRateParameter();
        }
        public void Get_Company_GC_Parameter()
        {
            objDS = objNewGCModel.Get_Company_GC_Parameter();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objINewGCView.GC_No_Length = Util.String2Int(objDR["GC_No_Length"].ToString());

                objINewGCView.BookingTypeId = Util.String2Int(objDR["Default_Booking_Type"].ToString());
                objINewGCView.PaymentTypeId = Util.String2Int(objDR["Default_Payment_Type"].ToString());
                objINewGCView.DeliveryTypeId = Util.String2Int(objDR["Default_Delivery_Type"].ToString());
                objINewGCView.RoadPermitTypeId = Util.String2Int(objDR["Default_Road_Permit_Type"].ToString());
                objINewGCView.UnitOfMeasurementId = Util.String2Int(objDR["Default_Measurment_Unit"].ToString());
                objINewGCView.FreightBasisId = Util.String2Int(objDR["Default_Freight_Basis"].ToString());
                objINewGCView.defaultFreightBasisId = Util.String2Int(objDR["Default_Freight_Basis"].ToString());
                objINewGCView.GCRiskId = Util.String2Int(objDR["Default_Risk_Type"].ToString());
                objINewGCView.defaultGCRiskType = Util.String2Int(objDR["Default_Risk_Type"].ToString());
                objINewGCView.ConsignmentTypeId = Util.String2Int(objDR["Default_Consignment_Type"].ToString());
                objINewGCView.defaultConsignmentType = Util.String2Int(objDR["Default_Consignment_Type"].ToString());
                objINewGCView.PickupTypeId = Util.String2Int(objDR["Default_Pickup_Type"].ToString());
                objINewGCView.Is_DefaultPOD_Checked = Util.String2Bool(objDR["Is_POD_Checked"].ToString());
                objINewGCView.Is_POD_Disabled = Util.String2Bool(objDR["Is_POD_Disabled"].ToString());

                objINewGCView.Is_ToPay_Charge_Require = Util.String2Bool(objDR["Is_ToPay_Charge_Require"].ToString());
                objINewGCView.Is_FOV_Calculated_As_Per_Standard = Util.String2Bool(objDR["Is_FOV_Calculated_As_Per_Standard"].ToString());
                objINewGCView.Is_Auto_Booking_MR_For_Paid_Booking = Util.String2Bool(objDR["Is_Auto_Booking_MR_For_Paid_Booking"].ToString());
                objINewGCView.Is_Consignor_Consignee_Details_Shown = Util.String2Bool(objDR["Is_Consignor_Consignee_Details_Shown"].ToString());
                objINewGCView.LoadingSuperVisor_RequiredFor_BookingType = objDR["Loading_SuperVisor_RequiredFor_Booking_Type"].ToString();
                objINewGCView.Container_Details_RequiredFor_BookingType = objDR["Container_Details_RequiredFor_Booking_Type"].ToString().ToLower();
                objINewGCView.Is_Multiple_Location_Billing_Allowed = Util.String2Bool(objDR["Is_Multiple_Location_Billing_Allowed"].ToString());
                objINewGCView.Is_Multiple_Party_Billing_Allowed = Util.String2Bool(objDR["Is_Multiple_Party_Billing_Allowed"].ToString());

                objINewGCView.Valid_Cheque_Start_Days = Util.String2Int(objDR["Valid_Cheque_Start_Days"].ToString());
                objINewGCView.Valid_Cheque_End_Days = Util.String2Int(objDR["Valid_Cheque_End_Days"].ToString());
                //objINewGCView.Is_Validate_Freight_On_Article = Util.String2Bool(objDR["Validate_Freight_On_Article"].ToString());
                //objINewGCView.Remark_Max_Length = Util.String2Int(objDR["Remark_Max_Length"].ToString());
            }
        }
        public void Get_Applicable_Service_Tax()
        {
            decimal Standard_ServiceTaxPercent = objComm.Get_Service_Tax_Percent(objINewGCView.BookingDate);

            objINewGCView.Standard_ServiceTaxPercent = Standard_ServiceTaxPercent;
            objINewGCView.ServiceTax_Label = "GST (" + Standard_ServiceTaxPercent.ToString() + "%)";
        }
        public DataSet Get_From_Location_Details()
        {
            objDS = objNewGCModel.Get_From_Location_Details();
            return objDS;
        }
        public void Get_Application_Start_Date()
        {
            objDS = objNewGCModel.Get_Application_Start_Date();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objINewGCView.ApplicationStartDate = Convert.ToDateTime(objDS.Tables[0].Rows[0]["Start_Date"].ToString());
            }
        }
        public bool IsDuplicate_GCNo()
        {
            return objNewGCModel.IsDuplicate_GCNo();
        }
        public void Fill_Contract_And_Branches(int flag)
        {
            if (flag ==1)
                objINewGCView.BindContractBranches = objNewGCModel.Fill_Contract_And_Branches(flag);
            else
                objINewGCView.BindContract = objNewGCModel.Fill_Contract_And_Branches(flag);
        }
        public void Get_BranchRateParameter()
        {
           objDS = objNewGCModel.Get_BranchRateParameter();

           if (Common.GetMenuItemId() != 200)
           {
               if (objDS.Tables[0].Rows.Count > 0)
               {
                   DataRow objDR = objDS.Tables[0].Rows[0];

                   if (objINewGCView.keyID <= 0 && !objINewGCView.Is_Attached)
                   {
                       objINewGCView.LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                       objINewGCView.StationaryCharge = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                       //objINewGCView.DDCharge = Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                       objINewGCView.ChargeWeight = Util.String2Decimal(objDR["Min_Charge_Wt"].ToString());
                       objINewGCView.VolumetricToKgFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                       objINewGCView.ToPayCharge = objINewGCView.Is_ToPay_Charge_Require == false ? 0 : Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                       objINewGCView.RateCard_AOC_Percent = Util.String2Decimal(objDR["AOC_Percent"].ToString());
                       objINewGCView.FOVRiskCharge = Util.String2Decimal(objDR["Min_FOV"].ToString());
                   }

                   objINewGCView.RateCard_MinimumChargeWeight = Util.String2Decimal(objDR["Min_Charge_Wt"].ToString());
                   objINewGCView.RateCard_BiltiCharges = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                   objINewGCView.RateCard_MaxBiltyCharge = Util.String2Decimal(objDR["Max_Bilty_Charges"].ToString());
                   objINewGCView.RateCard_FOV = Util.String2Decimal(objDR["Min_FOV"].ToString());
                   objINewGCView.RateCard_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                   objINewGCView.RateCard_FOVRate = Util.String2Decimal(objDR["FOV_Rate"].ToString());
                   objINewGCView.RateCard_ToPayCharges = objINewGCView.Is_ToPay_Charge_Require == false ? 0 : Util.String2Decimal(objDR["To_Pay_Charges"].ToString());

                   objINewGCView.RateCard_DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                   objINewGCView.RateCard_LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                   objINewGCView.RateCard_HamaliCharge = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                   objINewGCView.RateCard_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_kg"].ToString());
                   objINewGCView.RateCard_HamaliPerArticles = Util.String2Decimal(objDR["Hamali_Per_Article"].ToString());
                   objINewGCView.RateCard_Hamali_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Hamali_Chg_Discount_Percent"].ToString());
                   objINewGCView.RateCard_DDCharge_Rate = 0; //` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                   objINewGCView.RateCard_DDCharge = 0; //` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                   objINewGCView.RateCard_DD_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_DD_Chg_Discount_Percent"].ToString());
                   objINewGCView.RateCard_CFTFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                   objINewGCView.RateCard_Octroi_Form_Charge = Util.String2Decimal(objDR["Octroi_Form_Charges"].ToString());
                   objINewGCView.RateCard_Octroi_Service_Charge = Util.String2Decimal(objDR["Octroi_Service_Charges"].ToString());
                   objINewGCView.RateCard_GI_Charges = Util.String2Decimal(objDR["GI_Charges"].ToString());
                   objINewGCView.RateCard_Demurrage_Days = Util.String2Decimal(objDR["Demurrage_Days"].ToString());
                   objINewGCView.RateCard_Demurrage_Rate = Util.String2Decimal(objDR["Demurrage_Rate_Kg_Per_Day"].ToString());
                   objINewGCView.RateCard_Invoice_Rate = Util.String2Decimal(objDR["Invoice_Rate"].ToString());
                   objINewGCView.RateCard_Invoice_Per_How_Many_Rs = Util.String2Decimal(objDR["Invoice_Per_How_Many_Rs"].ToString());
                   objINewGCView.RateCard_Freight_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Freight_Chg_Discount_Percent"].ToString());
                   objINewGCView.RateCard_Fov_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Fov_Chg_Discount_Percent"].ToString());
                   objINewGCView.RateCard_ToPay_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_TP_Chg_Discount_Percent"].ToString());

                   objINewGCView.Default_Bank_Ledger_Id = Util.String2Int(objDR["Default_Bank_Ledger_Id"].ToString());
                   objINewGCView.Default_Cash_Ledger_Id = Util.String2Int(objDR["Default_Cash_Ledger_Id"].ToString());
                   objINewGCView.Default_Cheque_Bank_Ledger_Name = objDR["Default_Bank_Ledger_Name"].ToString();
                   objINewGCView.Default_Cheque_Branch_Ledger_Name = objDR["Default_Branch_Ledger_Name"].ToString();
                   
               }
           }
           else
           {
               if (objINewGCView.keyID <= 0)
               {
                   objINewGCView.LocalCharge = 0;
                   objINewGCView.StationaryCharge = 0;
                   objINewGCView.DDCharge = 0;
                   objINewGCView.ChargeWeight = 0;
                   objINewGCView.VolumetricToKgFactor = 0;
                   objINewGCView.ToPayCharge = 0;
               }
               objINewGCView.RateCard_MinimumChargeWeight = 0;
               objINewGCView.RateCard_BiltiCharges = 0;
               objINewGCView.RateCard_MaxBiltyCharge = 0;
               objINewGCView.RateCard_FOV = 0;
               objINewGCView.RateCard_FOVPercentage = 0;
               objINewGCView.RateCard_FOVRate = 0;
               objINewGCView.RateCard_ToPayCharges = 0;

               objINewGCView.RateCard_DACCCharges = 0;
               objINewGCView.RateCard_LocalCharge = 0;
               objINewGCView.RateCard_HamaliCharge = 0;
               objINewGCView.RateCard_HamaliPerKg = 0;
               objINewGCView.RateCard_HamaliPerArticles = 0;
               objINewGCView.RateCard_Hamali_Charge_Discount_Percent = 0;
               objINewGCView.RateCard_DDCharge_Rate = 0;
               objINewGCView.RateCard_DDCharge = 0; 
               objINewGCView.RateCard_DD_Charge_Discount_Percent = 0;
               objINewGCView.RateCard_CFTFactor = 0;
               objINewGCView.RateCard_Octroi_Form_Charge = 0;
               objINewGCView.RateCard_Octroi_Service_Charge = 0;
               objINewGCView.RateCard_GI_Charges = 0;
               objINewGCView.RateCard_Demurrage_Days = 0;
               objINewGCView.RateCard_Demurrage_Rate = 0;
               objINewGCView.RateCard_Invoice_Rate = 0;
               objINewGCView.RateCard_Invoice_Per_How_Many_Rs = 0;
               objINewGCView.RateCard_Freight_Charge_Discount_Percent = 0;
               objINewGCView.RateCard_Fov_Charge_Discount_Percent = 0;
               objINewGCView.RateCard_ToPay_Charge_Discount_Percent = 0;
               objINewGCView.RateCard_AOC_Percent = 0;
               objINewGCView.Default_Bank_Ledger_Id = 0;
               objINewGCView.Default_Cash_Ledger_Id = 0;
               objINewGCView.Default_Cheque_Bank_Ledger_Name = "";
               objINewGCView.Default_Cheque_Branch_Ledger_Name = "";

           }
        }

        public void read_Values(bool Is_CopyGC)
        {
            if (Is_CopyGC)
                objDS = objNewGCModel.readValues(objINewGCView.Attached_GC_Id);
            else
                objDS = objNewGCModel.readValues(objINewGCView.keyID);

            if (objDS.Tables[0].Rows.Count > 0) // && objINewGCView.keyID > 0
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                if (!Is_CopyGC)
                {
                    objINewGCView.GC_No_For_Print = objDR["GC_No_For_Print"].ToString();
                    objINewGCView.Agency_GC_No = objDR["Agency_GC_No"].ToString();
                    objINewGCView.PrivateMark = objDR["Private_Mark"].ToString();
                    objINewGCView.BookingDate = Convert.ToDateTime(objDR["GC_Date"]);
                    objINewGCView.BookingTime = objDR["GC_Time"].ToString();
                    objINewGCView.Is_Attached = Util.String2Bool(objDR["Is_Attached"].ToString());
                }
                if (!Is_CopyGC && objINewGCView.Is_Attached)
                {
                    objINewGCView.Attached_GC_Id = Util.String2Int(objDR["Attached_GC_Id"].ToString());
                    objINewGCView.Attached_GC_No = objDR["Attached_GC_No"].ToString();
                }
                objINewGCView.CustomerRefNo = objDR["Customer_Ref_No"].ToString();
                objINewGCView.ConsignmentTypeId = Util.String2Int(objDR["Consignment_Type_Id"].ToString());
                objINewGCView.BookingTypeId = Util.String2Int(objDR["Booking_Type_Id"].ToString());
                objINewGCView.BookingSubTypeId = Util.String2Int(objDR["Booking_Sub_Type_Id"].ToString());
                objINewGCView.DeliveryTypeId = Util.String2Int(objDR["Delivery_Type_Id"].ToString());
                objINewGCView.DeliveryAgainstId = Util.String2Int(objDR["Door_Delivery_Against_ID"].ToString());
                objINewGCView.FromLocationId = Util.String2Int(objDR["From_Location_ID"].ToString());
                objINewGCView.FromLocation = objDR["From_Location_Name"].ToString();
                objINewGCView.ToLocationId = Util.String2Int(objDR["To_Location_ID"].ToString());
                objINewGCView.ToLocation = objDR["To_Location_Name"].ToString();

                objINewGCView.CRMPickupRequestId = Util.String2Int(objDR["CRM_Pickup_Request_ID"].ToString());
                objINewGCView.BookingBranchId = Util.String2Int(objDR["Booking_Branch_Id"].ToString());

                objINewGCView.ArrivedFromBranchId = Util.String2Int(objDR["Arrived_From_Branch_Id"].ToString());
                objINewGCView.ArrivedDate = Convert.ToDateTime(objDR["Arrived_Date"].ToString());
                objINewGCView.BookingBranch = objDR["Booking_Branch_Name"].ToString();
                objINewGCView.ArrivedFromBranch = objDR["Arrived_From_Branch_Name"].ToString();

                objINewGCView.DeliveryBranchId = Util.String2Int(objDR["Delivery_Branch_Id"].ToString());
                objINewGCView.DeliveryBranchName = objDR["Delivery_Branch_Name"].ToString();
                objINewGCView.VehicleTypeId = Util.String2Int(objDR["Vehicle_Type_Id"].ToString());
                objINewGCView.VehicleNo = objDR["Vehicle_No"].ToString();
                objINewGCView.PickupTypeId = Util.String2Int(objDR["Pickup_Type_Id"].ToString());
                objINewGCView.STMNo = objDR["STM_No"].ToString();
                objINewGCView.FeasibilityRouteSurveyNo = objDR["Feasibility_Route_Survey_No"].ToString();
                objINewGCView.ServiceTypeId = Util.String2Int(objDR["Service_Type_Id"].ToString());
                objINewGCView.Is_ST_Abatment_Required = Util.String2Bool(objDR["Is_ST_Abatment"].ToString());

                objINewGCView.ConsignorId = Util.String2Int(objDR["Consignor_Client_ID"].ToString());
                objINewGCView.ConsignorName = objDR["Consignor_Name"].ToString();
                objINewGCView.ConsignorAddressValue = objDR["Consignor_Add_Details"].ToString();
                objINewGCView.Is_RegularConsignor = Util.String2Bool(objDR["Is_Consignor_Regular_Client"].ToString());
                objINewGCView.Is_ServiceTaxApplicableForConsignor = Util.String2Bool(objDR["Is_Consignor_Service_Tax_Applicable"].ToString());
                objINewGCView.ConsignorStateId = Util.String2Int(objDR["ConsignorStateID"].ToString());

                objINewGCView.ConsignorPhoneNumbers = objDR["ConsignorPhoneNumbers"].ToString();
                objINewGCView.ConsigneePhoneNumbers = objDR["ConsigneePhoneNumbers"].ToString();

                objINewGCView.ConsignorMobileNumbers = objDR["ConsignorMobileNumbers"].ToString();
                objINewGCView.ConsigneeMobileNumbers = objDR["ConsigneeMobileNumbers"].ToString();

                objINewGCView.ConsigneeId = Util.String2Int(objDR["Consignee_Client_ID"].ToString());
                objINewGCView.ConsigneeName = objDR["Consignee_Name"].ToString();
                objINewGCView.Consignee_CSTTINNo = objDR["Consignee_CST_TIN_No"].ToString();
                objINewGCView.ConsigneeAddressValue = objDR["Consignee_Add_Details"].ToString();
                objINewGCView.Is_RegularConsignee = Util.String2Bool(objDR["Is_Consignee_Regular_Client"].ToString());
                objINewGCView.Is_ServiceTaxApplicableForConsignee = Util.String2Bool(objDR["Is_Consignee_Service_Tax_Applicable"].ToString());
                objINewGCView.ConsigneeDDAddress1 = objDR["DD_Address_1"].ToString();
                objINewGCView.ConsigneeDDAddress2 = objDR["DD_Address_2"].ToString();
                objINewGCView.ConsigneeStateId = Util.String2Int(objDR["ConsigneeStateID"].ToString());

                objINewGCView.PaymentTypeId = Util.String2Int(objDR["Payment_Type_Id"].ToString());

                objINewGCView.ReasonFreightPendingId = Util.String2Int(objDR["ReasonFreightPendingId"].ToString());
                objINewGCView.PaidFreightPendingPersonName = objDR["PaidFreightPendingPersonName"].ToString();
                objINewGCView.PaidFreightPendingPersonMobile = objDR["PaidFreightPendingPersonMobile"].ToString();

                objINewGCView.GCRiskId = Util.String2Int(objDR["Risk_Type_ID"].ToString());
                objINewGCView.Session_InsuranceCompany = objDR["Insurance_Company"].ToString();
                objINewGCView.Session_PolicyNo = objDR["Policy_No"].ToString();
                objINewGCView.Session_PolicyAmount = Util.String2Decimal(objDR["Policy_Amount"].ToString());
                objINewGCView.Session_RiskAmount = Util.String2Decimal(objDR["Risk_Amount"].ToString());
                objINewGCView.Session_PolicyExpDate = Convert.ToDateTime(objDR["Policy_Exp_Date"].ToString());
                objINewGCView.DeliveryWayTypeId = Util.String2Int(objDR["DeliveryWayTypeID"].ToString());

                objINewGCView.ItemValueForFOV = Util.String2Decimal(objDR["ItemValueForFOV"].ToString());
                objINewGCView.TotalArticles = Util.String2Int(objDR["Total_Articles"].ToString());
                objINewGCView.TotalWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());

                objINewGCView.UnitOfMeasurmentLength = Util.String2Decimal(objDR["Total_Length"].ToString());
                objINewGCView.UnitOfMeasurmentWidth = Util.String2Decimal(objDR["Total_Width"].ToString());
                objINewGCView.UnitOfMeasurmentHeight = Util.String2Decimal(objDR["Total_Height"].ToString());
                objINewGCView.HeightInFeet = Util.String2Decimal(objDR["Total_Height_In_Ft"].ToString());
                objINewGCView.WidthInFeet = Util.String2Decimal(objDR["Total_Width_In_Ft"].ToString());
                objINewGCView.LengthInFeet = Util.String2Decimal(objDR["Total_Length_In_Ft"].ToString());
                objINewGCView.UnitOfMeasurementId = Util.String2Int(objDR["Unit_Of_Measurement_ID"].ToString());
                objINewGCView.FreightBasisId = Util.String2Int(objDR["Freight_Basis_ID"].ToString());
                objINewGCView.VolumetricFreightUnitId = Util.String2Int(objDR["Volumetric_Freight_Unit_ID"].ToString());
                objINewGCView.VolumetricToKgFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                objINewGCView.TotalCFT = Util.String2Decimal(objDR["Total_CFT"].ToString());
                objINewGCView.TotalCBM = Util.String2Decimal(objDR["Total_CBM"].ToString());

                objINewGCView.ActualWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objINewGCView.ChargeWeight = Util.String2Decimal(objDR["Charged_Weight"].ToString());
                objINewGCView.FreightRate = Util.String2Decimal(objDR["Freight_Rate"].ToString());
                objINewGCView.TotalInvoiceAmount = Util.String2Decimal(objDR["Total_Invoice_Value"].ToString());
                objINewGCView.LengthChargeHeadId = Util.String2Int(objDR["Length_Charge_Head_Id"].ToString());
                objINewGCView.ServiceTaxPayableBy = Util.String2Int(objDR["Tax_Payable_By"].ToString());
                objINewGCView.Is_ODA = Util.String2Bool(objDR["Is_ODA"].ToString());

                objINewGCView.ODAChargesUpTo500Kg = Util.String2Decimal(objDR["Oda_charges_upto_500_Kg"].ToString());
                objINewGCView.ODAChargesAbove500Kg = Util.String2Decimal(objDR["Oda_charges_above_500_Kg"].ToString());
                objINewGCView.Is_OctroiApplicable = Util.String2Bool(objDR["Is_Octroi_Applicable"].ToString());
                objINewGCView.Is_ToPayBookingApplicable = Util.String2Bool(objDR["Is_To_Pay_Booking"].ToString());
               
                objINewGCView.Is_MultipleBilling = Util.String2Bool(objDR["Is_Multiple_Billing"].ToString());
                objINewGCView.BillingPartyId = Util.String2Int(objDR["Billing_Client_ID"].ToString());
                objINewGCView.BillingParty = objDR["Billing_Client_Name"].ToString();
                objINewGCView.BillingHierarchy = objDR["Billing_Hierarchy"].ToString();
                objINewGCView.BillingLocationId = Util.String2Int(objDR["Billing_Branch_ID"].ToString());
                objINewGCView.BillingLocation = objDR["billing_Branch_Name"].ToString();
                objINewGCView.BillingRemark = objDR["Billing_Remarks"].ToString();

                objINewGCView.WholeselerId = Util.String2Int(objDR["Wholeseler_ID"].ToString());
                objINewGCView.Wholeseler = objDR["Wholeseler"].ToString();

                objINewGCView.Freight = Util.String2Decimal(objDR["Freight_Amt"].ToString());
                objINewGCView.Discount = Util.String2Decimal(objDR["Discount"].ToString());
                objINewGCView.DiscountId = Util.String2Int(objDR["DiscountId"].ToString());
                objINewGCView.LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                objINewGCView.LoadingCharge = Util.String2Decimal(objDR["Hamali_Charges"].ToString());
                objINewGCView.StationaryCharge = Util.String2Decimal(objDR["Bilti_Charges"].ToString());
                objINewGCView.FOVRiskCharge = Util.String2Decimal(objDR["FOV"].ToString());
                objINewGCView.ToPayCharge = Util.String2Decimal(objDR["TP_Charges"].ToString());
                objINewGCView.DDCharge = Util.String2Decimal(objDR["DD_Charges"].ToString());
                objINewGCView.ODACharges = Util.String2Decimal(objDR["ODA_Charges"].ToString());
                objINewGCView.LengthCharge = Util.String2Decimal(objDR["Length_Charge"].ToString());
                objINewGCView.UnloadingCharge = Util.String2Decimal(objDR["Unloading_Charge"].ToString());
                objINewGCView.NFormCharge = Util.String2Decimal(objDR["NForm_Charge"].ToString());
                objINewGCView.Standard_DDChargeRate = Util.String2Decimal(objDR["Std_DD_Charge_Rate"].ToString());
                objINewGCView.DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                objINewGCView.OtherCharges = Util.String2Decimal(objDR["Other_Charges"].ToString());
                objINewGCView.AOCPercent = Util.String2Decimal(objDR["AOC_Percent"].ToString());
                objINewGCView.AOC = Util.String2Decimal(objDR["AOC"].ToString());
                objINewGCView.SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                objINewGCView.Abatment = Util.String2Decimal(objDR["Tax_Abate"].ToString());
                objINewGCView.TaxableAmount = Util.String2Decimal(objDR["Amt_Taxable"].ToString());
                objINewGCView.ServiceTax = Util.String2Decimal(objDR["Service_Tax_Amount"].ToString());
                objINewGCView.ActualServiceTax = Util.String2Decimal(objDR["Actual_Service_Tax_Amount"].ToString());
                objINewGCView.RoundOff = Util.String2Int(objDR["Round_Off"].ToString());
                objINewGCView.TotalGCAmount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                objINewGCView.Advance = Util.String2Decimal(objDR["Advance_Amount"].ToString());
                objINewGCView.CashAmount = Util.String2Decimal(objDR["Cash_Amount"].ToString());
                objINewGCView.ChequeAmount = Util.String2Decimal(objDR["Cheque_Amount"].ToString());
                objINewGCView.ChequeDate = Convert.ToDateTime(objDR["Cheque_Date"].ToString());
                objINewGCView.BankName = objDR["Bank_Name"].ToString();
                objINewGCView.eWayBillNo = objDR["eWayBillNo"].ToString();
                objINewGCView.Is_MultipleeWayBill = Util.String2Bool(objDR["Is_Multiple_eWayBill"].ToString());
                objINewGCView.ConsigneeDeliveryAreaID = Util.String2Int(objDR["ConsigneeDeliveryAreaID"].ToString());
                objINewGCView.ConsigneeDeliveryAreaName = objDR["ConsigneeDeliveryAreaName"].ToString();
                objINewGCView.Is_Consignee_Is_To_Pay_Allowed = Util.String2Bool(objDR["Consignee_Is_To_Pay_Allowed"].ToString());
                

                if (Util.String2Int(objDR["Cheque_No"].ToString()) == 0)
                    objINewGCView.ChequeNo = "";
                else
                    objINewGCView.ChequeNo = Convert.ToInt32(objDR["Cheque_No"].ToString()).ToString("000000");

                if (Util.String2Int(objDR["Loading_Supervisor_ID"].ToString()) > 0)
                    objINewGCView.SetLoadingSuperVisor(objDR["Loading_Supervisor_Name"].ToString(), objDR["Loading_Supervisor_ID"].ToString());
                else
                    objINewGCView.SetLoadingSuperVisor("", "0");

                if (Util.String2Int(objDR["Marketing_Executive_ID"].ToString()) > 0)
                    objINewGCView.SetMarketingExecutive(objDR["Marketing_Executive_Name"].ToString(), objDR["Marketing_Executive_ID"].ToString());
                else
                    objINewGCView.SetLoadingSuperVisor("", "0");

                //--------------------------------- Contract Details ----------------------------------
                if (Util.String2Int(objDR["Contract_ID"].ToString()) > 0)
                {
                    objINewGCView.Contractual_Client = objDR["Contractual_Client_Name"].ToString();
                    objINewGCView.Contractual_ClientId = Util.String2Int(objDR["Contractual_Client_Id"].ToString());

                    Fill_Contract_And_Branches(1);//1 flag is for Contract Branch

                    objINewGCView.Contract_BranchId = Util.String2Int(objDR["Contract_Branch_ID"].ToString());
                    Fill_Contract_And_Branches(2); //2 flag is for Contract
                    objINewGCView.ContractId = Util.String2Int(objDR["Contract_ID"].ToString());
                    objINewGCView.Is_ContractApplied = Util.String2Int(objDR["Is_ContractApplied"].ToString());

                    objINewGCView.IsCC_PaidAllowed = Util.String2Bool(objDR["Is_Paid_Allowed"].ToString());
                    objINewGCView.IsCC_ToPayAllowed = Util.String2Bool(objDR["Is_To_Pay_Allowed"].ToString());
                    objINewGCView.IsCC_FOCAllowed = Util.String2Bool(objDR["Is_FOC_Allowed"].ToString());
                    objINewGCView.IsCC_TBBAllowed = Util.String2Bool(objDR["Is_To_Be_Billed_Docket_Allowed"].ToString());
                }
                //--------------------------------- End Contract Details ----------------------------------

                objINewGCView.RateContractId = Util.String2Int(objDR["RateContractId"].ToString());

                objINewGCView.AgencyId = Util.String2Int(objDR["Agency_ID"].ToString());
                objINewGCView.AgencyName = objDR["Agency_Name"].ToString();
                objINewGCView.AgencyLedgerId = Util.String2Int(objDR["Agency_Ledger_ID"].ToString());
                objINewGCView.AgencyLedger = objDR["Agency_Ledger_Name"].ToString();

                objINewGCView.Standard_FreightRate = Util.String2Decimal(objDR["Std_Freight_Rate"].ToString());
                //objINewGCView.Standard_ServiceTaxPercent = Util.String2Decimal(objDR["Service_Tax_Percent"].ToString());
                objINewGCView.RateCard_BiltiCharges = Util.String2Decimal(objDR["Std_Bilti_Charges"].ToString());
                objINewGCView.Standard_DDCharge = Util.String2Decimal(objDR["Std_DD_Charge"].ToString());
                objINewGCView.Standard_FOV = Util.String2Decimal(objDR["Std_FOV"].ToString());
                objINewGCView.Standard_FreightAmount = Util.String2Decimal(objDR["Std_Freight_Amt"].ToString());
                objINewGCView.RateCard_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());
                objINewGCView.Standard_HamaliCharge = Util.String2Decimal(objDR["Std_Hamali_Charge"].ToString());
                objINewGCView.RateCard_HamaliPerArticles = Util.String2Decimal(objDR["Hamali_Per_Articles"].ToString());
                objINewGCView.RateCard_LocalCharge = Util.String2Decimal(objDR["Std_Local_Charge"].ToString());
                objINewGCView.Standard_ServiceTaxAmount = Util.String2Decimal(objDR["Std_Service_Tax_Amount"].ToString());
                objINewGCView.RateCard_Octroi_Form_Charge = Util.String2Decimal(objDR["Std_Octroi_Form_Charges"].ToString());
                objINewGCView.RateCard_Octroi_Service_Charge = Util.String2Decimal(objDR["Std_Octroi_Service_Charges"].ToString());
                objINewGCView.RateCard_Demurrage_Days = Util.String2Decimal(objDR["Std_Demurrage_Days"].ToString());
                objINewGCView.RateCard_Demurrage_Rate = Util.String2Decimal(objDR["Std_Demurrage_Rate"].ToString());
                objINewGCView.RateCard_GI_Charges = Util.String2Decimal(objDR["Std_GI_Charges"].ToString());
                objINewGCView.Standard_LengthCharge = Util.String2Decimal(objDR["Std_Length_Charge"].ToString());
                //objINewGCView.ServiceTax_Label = "Service Tax (" + objINewGCView.Standard_ServiceTaxPercent.ToString() + "%)";
                objINewGCView.OtherChargesRemark = objDR["GC_Remarks_Other_Charges"].ToString();
                objINewGCView.InstructionRemark = objDR["GC_Remarks"].ToString();
                objINewGCView.Enclosures = objDR["Enclosures"].ToString();
                objINewGCView.Is_POD = Convert.ToBoolean(objDR["Acknowledge"].ToString());
                objINewGCView.Is_SignedByConsignor = Convert.ToBoolean(objDR["Is_Signed_By_Consignor"].ToString());
                objINewGCView.RoadPermitTypeId = Util.String2Int(objDR["Road_Permit_Type_ID"].ToString());
                objINewGCView.RoadPermitSrNo = objDR["Road_Permit_SrNo"].ToString();
                objINewGCView.Is_Insured = Convert.ToBoolean(objDR["Is_Insured"].ToString());
                objINewGCView.Session_ContainerTypeId = Util.String2Int(objDR["Container_Type_Id"].ToString());
                objINewGCView.Session_ContainerNoPart1 = objDR["ContainerNo1"].ToString();
                objINewGCView.Session_ContainerNoPart2 = objDR["ContainerNo2"].ToString();
                objINewGCView.Session_SealNo = objDR["SealNo"].ToString();
                objINewGCView.Session_ReturnToYardId = Util.String2Int(objDR["Return_To_Yard_Id"].ToString());
                objINewGCView.Session_ReturnToYardName = objDR["Return_To_Yard_Name"].ToString();
                objINewGCView.Session_NFormNo = objDR["NFormNo"].ToString();
            }

            objINewGCView.Session_MultipleCommodityGrid = objDS.Tables[1];
            objINewGCView.Session_InvoiceGrid = objDS.Tables[2];
            objINewGCView.Session_BillingDetailsGrid = objDS.Tables[3];
            objINewGCView.Session_GCOtherChargeHead = objDS.Tables[4];
            objINewGCView.Session_ChequeDetailsGrid = objDS.Tables[5];

            if (objDS.Tables[1].Rows.Count > 0 || objDS.Tables[3].Rows.Count > 0)
                objNewGCModel.Get_Service_Tax_Applicable_For_Commodity_And_BillingParty();

            Get_BranchRateParameter();
        }

        public void save()
        {
            base.DBSave();
        }

        public void Save_And_Repet_Save()
        {
            objNewGCModel.Save();
        }
    }
}