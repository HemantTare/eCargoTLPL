
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

/// <summary>
/// Summary description for GCPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class GCPresenter : Presenter
    {
        private IGCView objIGCView;
        private GCModel objGCModel;
        private DataSet objDS;

        public Common CommonObj = new Common();

        public GCPresenter(IGCView GCView, bool IsPostBack)
        {
            objIGCView = GCView;
            objGCModel = new GCModel(objIGCView);

            base.Init(objIGCView, objGCModel);

            if (!IsPostBack)
            {
                objIGCView.BookingDate = DateTime.Now.Date;
                objIGCView.ArrivedDate = DateTime.Now.Date;
                initValues();
            }
        }

        public void Get_Application_Start_Date()
        {
           objDS = objGCModel.Get_Application_Start_Date();

           if (objDS.Tables[0].Rows.Count > 0)
           {
               DataRow objDR = objDS.Tables[0].Rows[0];
               objIGCView.ApplicationStartDate =  Convert.ToDateTime(objDR["Start_Date"].ToString());
           }
        }

        public void Get_Agency_Ledger()
        {
            objDS = objGCModel.Get_Agency_Ledger();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                objIGCView.SetAgencyLedger(objDR["Agency_Ledger_Name"].ToString(), objDR["Agency_Ledger_Id"].ToString());
            }
            else
            {
                objIGCView.SetAgencyLedger("", "0");
            }
        }

        public void Get_Company_GC_Parameter()
        {
            objDS = objGCModel.Get_Company_GC_Parameter();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIGCView.Default_Booking_Type = Util.String2Int(objDR["Default_Booking_Type"].ToString());
                objIGCView.Default_Delivery_Type = Util.String2Int(objDR["Default_Delivery_Type"].ToString());
                objIGCView.Default_Freight_Basis = Util.String2Int(objDR["Default_Freight_Basis"].ToString());
                objIGCView.Default_Measurment_Unit = Util.String2Int(objDR["Default_Measurment_Unit"].ToString());
                objIGCView.Default_Payment_Type = Util.String2Int(objDR["Default_Payment_Type"].ToString());

                objIGCView.Default_Risk_Type = Util.String2Int(objDR["Default_Risk_Type"].ToString());
                objIGCView.Default_Road_Permit_Type = Util.String2Int(objDR["Default_Road_Permit_Type"].ToString());

                objIGCView.Default_Consignment_Type = Util.String2Int(objDR["Default_Consignment_Type"].ToString());
                objIGCView.Default_Pickup_Type = Util.String2Int(objDR["Default_Pickup_Type"].ToString());
                objIGCView.Default_Commodity_Weight = Util.String2Int(objDR["Default_Commodity_Weight"].ToString());
                 

                objIGCView.Is_POD_Checked = Util.String2Bool(objDR["Is_POD_Checked"].ToString());
                objIGCView.Is_POD_Disabled = Util.String2Bool(objDR["Is_POD_Disabled"].ToString());
                objIGCView.LoadingSuperVisor_RequiredFor_BookingType = objDR["Loading_SuperVisor_RequiredFor_Booking_Type"].ToString();

                objIGCView.Is_FOV_Calculated_As_Per_Standard = Util.String2Bool(objDR["Is_FOV_Calculated_As_Per_Standard"].ToString());
                objIGCView.Is_Auto_Booking_MR_For_Paid_Booking = Util.String2Bool(objDR["Is_Auto_Booking_MR_For_Paid_Booking"].ToString());

                objIGCView.GC_No_Length = Util.String2Int(objDR["GC_No_Length"].ToString());

                objIGCView.Valid_Cheque_Start_Days = Util.String2Int(objDR["Valid_Cheque_Start_Days"].ToString());
                objIGCView.Valid_Cheque_End_Days = Util.String2Int(objDR["Valid_Cheque_End_Days"].ToString());
                objIGCView.Container_Details_RequiredFor_BookingType = objDR["Container_Details_RequiredFor_Booking_Type"].ToString().ToLower();

                objIGCView.Is_ToPay_Charge_Require = Util.String2Bool(objDR["Is_ToPay_Charge_Require"].ToString());

                objIGCView.Is_Consignor_Consignee_Details_Shown = Util.String2Bool(objDR["Is_Consignor_Consignee_Details_Shown"].ToString());
                objIGCView.Is_Validate_Freight_On_Article = Util.String2Bool(objDR["Validate_Freight_On_Article"].ToString());

                objIGCView.Remark_Max_Length = Util.String2Int(objDR["Remark_Max_Length"].ToString());
                objIGCView.Is_Multiple_Location_Billing_Allowed = Util.String2Bool(objDR["Is_Multiple_Location_Billing_Allowed"].ToString());

                 
            }
        }

        public void Fill_Values()
        {
            objGCModel.Get_CompanyParameterDetails();

            objGCModel.Get_VAId();
         //   objGCModel.Get_Document_Allocation_Details();

            objDS = objGCModel.Fill_Values();

            objIGCView.Session_CommodityDdl = objDS.Tables[0] ;
            objIGCView.Session_ItemDdl  = objDS.Tables[1];
            objIGCView.Session_PackingTypeDdl  = objDS.Tables[2];

            objIGCView.BindUnitOfMeasurement = objDS.Tables[3];
            objIGCView.BindFreightBasis = objDS.Tables[4];

            objIGCView.BindVolumetricFreightUnit = objDS.Tables[5];

            objIGCView.BindBookingType  = objDS.Tables[6];
            objIGCView.BindDeliveryType  = objDS.Tables[7];
            objIGCView.BindVehicleType  = objDS.Tables[8];
            objIGCView.BindConsignmentType = objDS.Tables[9];

            objIGCView.BindPaymentType = objDS.Tables[10];
            objIGCView.BindGCRiskType = objDS.Tables[11];

            //objIGCView.BindLoadingSuperVisor  = objDS.Tables[12];
            //objIGCView.BindMarketingExecutive  = objDS.Tables[13];
            
            objIGCView.BindDeliveryAgainst  = objDS.Tables[14];
            objIGCView.BindPickupType  = objDS.Tables[15];

            objIGCView.BindRoadPermitType = objDS.Tables[16];
            objIGCView.BindGCInstructions  = objDS.Tables[17];

            objIGCView.BindBookingSubType  = objDS.Tables[18];
            objIGCView.Session_GCOtherChargeHead  = objDS.Tables[19];

            objIGCView.BindLengthChargeHead = objDS.Tables[20];

            objIGCView.Session_ContainerType = objDS.Tables[21];
            objIGCView.BindDeliveryWayType = objDS.Tables[23];
            objIGCView.BindServiceType = objDS.Tables[24];
       }
 
        private void initValues()
        {
            Get_Application_Start_Date();
            Fill_Values();
            Get_BookingSubType();
            Get_Company_GC_Parameter();

            Get_Additional_Freight();
            Get_Applicable_Service_Tax();

            Read_GC_Details(false);
            Get_Is_ST_Abatment_Required();
        }
        
        public void Get_RequireForms()
        {
            objIGCView.Session_RequireForms = objGCModel.Get_RequireForms();
        }
        
        public Boolean  Is_Duplicate()
        {
            Boolean Is_Duplicate_GC_No;
            Is_Duplicate_GC_No = false;            
            Is_Duplicate_GC_No = objGCModel.Is_Duplicate();
            return Is_Duplicate_GC_No;
        }


        public Boolean Validate_Credit_Limit()
        {
            Boolean Is_Valid_Credit_Limit;
            Is_Valid_Credit_Limit = false;
            Is_Valid_Credit_Limit = objGCModel.Validate_Credit_Limit();
            return Is_Valid_Credit_Limit;
        }

        public Boolean Allow_To_Attached()
        {
            Boolean Is_Allow_To_Attached;
            Is_Allow_To_Attached = false;
            Is_Allow_To_Attached = objGCModel.Allow_To_Attached();
            return Is_Allow_To_Attached;
        }

        public Boolean Allow_To_ReBook()
        {
            Boolean Is_Allow_To_ReBook;
            Is_Allow_To_ReBook = false;
            Is_Allow_To_ReBook = objGCModel.Allow_To_ReBook();
            return Is_Allow_To_ReBook;
        }

        public Boolean Allow_To_Rectify()
        {
            Boolean Is_Allow_To_Rectify;
            Is_Allow_To_Rectify = false;

            Is_Allow_To_Rectify = objGCModel.Allow_To_Rectify();

            return Is_Allow_To_Rectify;
        }

        public void Fill_Item(int CommodityId)
        {
            DataSet ds_Item = new DataSet();
            ds_Item = objGCModel.Fill_Item(CommodityId);
            objIGCView.Session_ItemDdl = ds_Item.Tables[0];
        }
        
        public void Get_BillingPartyDetails()
        {
           objDS = objGCModel.Get_BillingPartyDetails();

            if (objDS.Tables[0].Rows.Count>0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                if (Util.String2Int(objDR["Billing_Branch_Id"].ToString()) > 0)
                {
                    objIGCView.BillingHierarchy = "BO";
                    objIGCView.BillingBranchId = Util.String2Int(objDR["Billing_Branch_Id"].ToString());
                }
                else
                {
                    objIGCView.BillingHierarchy = "0";
                    objIGCView.BillingBranchId = 0;
                }
                objIGCView.Is_ServiceTaxApplicableForConsignor = Convert.ToBoolean(objDR["Is_Service_Tax_Applicable"].ToString()) == true ? 1 : 0;
                objIGCView.Billing_Party_Credit_Limit = Util.String2Decimal(objDR["Credit_Limit"].ToString());
                objIGCView.Billing_Party_Closing_Balance = Util.String2Decimal(objDR["Closing_Balance"].ToString());
                objIGCView.Billing_Party_Ledger_Id = Util.String2Int (objDR["Ledger_Id"].ToString());
            }
            else
            {
                objIGCView.BillingHierarchy = "0";
                objIGCView.BillingBranchId = 0;
                objIGCView.Is_ServiceTaxApplicableForConsignor = 0;
                objIGCView.Billing_Party_Credit_Limit = 0;
                objIGCView.Billing_Party_Closing_Balance = 0;
                objIGCView.Billing_Party_Ledger_Id = 0;
            }
        }
                 
        public void Fill_ContractBranches()
        {
            objIGCView.BindContractBranches = objGCModel.Fill_ContractBranches();
        }

        public DataSet  Get_Contractual_Client_Details()
        {
            return objGCModel.Get_Contractual_Client_Details();
        }

        public void Fill_Contract()
        {
            objIGCView.BindContract = objGCModel.Fill_Contract();
        }

        public DataSet  Get_ContractDetails(String Call_From)
        {
            DataRow objDR;
            objDS = objGCModel.Get_ContractDetails();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objDR = objDS.Tables[0].Rows[0];

                if (Call_From != "From_GC_Read_Value")
                {
                    objIGCView.BillingHierarchy = objDR["Billing_Hierarchy"].ToString();
                    objIGCView.BillingBranchId = Util.String2Int(objDR["Billing_Branch_ID"].ToString());
                    objIGCView.BillingPartyId = Util.String2Int(objDR["Billing_Client_ID"].ToString());
                    objIGCView.SetBillingParty(objDR["Billing_Client_Name"].ToString(), objDR["Billing_Client_ID"].ToString());
                    
                    objIGCView.ConsignmentTypeId = Util.String2Int(objDR["Consignment_Type_ID"].ToString());
                    objIGCView.GCRiskId = Util.String2Int(objDR["GC_risk_type_id"].ToString());
                }

                objIGCView.Contract_UnitOfFreightId = Util.String2Int(objDR["Freight_Unit_ID"].ToString());
                objIGCView.Contract_FreightBasisId = Util.String2Int(objDR["Freight_Basis_ID"].ToString());
                objIGCView.Contract_FreightSubUnitId = Util.String2Int(objDR["Freight_Sub_Unit_ID"].ToString());
            }
            return objDS;
        }

        public void Get_Applicable_Service_Tax()
        {
            Decimal Applicable_Service_Tax_Percent;   

            Applicable_Service_Tax_Percent = CommonObj.Get_Service_Tax_Percent(objIGCView.BookingDate);

            objIGCView.Standard_ServiceTaxPercent = Applicable_Service_Tax_Percent;
            objIGCView.Applicable_Standard_ServiceTaxPercent = Applicable_Service_Tax_Percent;

            objIGCView.ServiceTax_Label = "Service Tax " + Applicable_Service_Tax_Percent.ToString() + "%";
        }

        public void Get_Is_ST_Abatment_Required()
        {
            objIGCView.Is_ST_Abatment_Required = objGCModel.Get_Is_ST_Abatment_Required(objIGCView.ServiceTypeId, objIGCView.BookingDate);
        }

        public void Get_BranchRateParameter()
        {
            objDS = objGCModel.Get_BranchRateParameter();

            if (objIGCView.Is_Opening_GC == false)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    objIGCView.LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                    objIGCView.LoadingCharge = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                    objIGCView.StationaryCharge = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                    objIGCView.MaxStationaryCharge = Util.String2Decimal(objDR["Max_Bilty_Charges"].ToString());
                    objIGCView.DDCharge = Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());

                    if (objIGCView.Is_ToPay_Charge_Require == true)
                        objIGCView.ToPayCharge = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                    else
                        objIGCView.ToPayCharge = 0;

                    objIGCView.OtherCharges = Util.String2Decimal(objDR["Other_Charges"].ToString());
                    objIGCView.hdn_StandardMinimumChargeWeight = Util.String2Decimal(objDR["Min_Charge_Wt"].ToString());
                    objIGCView.Standard_CFTFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                    objIGCView.hdn_StandardHamaliPerKg = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                    objIGCView.hdn_StandardMinimumFOV = Util.String2Decimal(objDR["Min_FOV"].ToString());
                    objIGCView.Standard_ServiceTaxPercent = Util.String2Decimal(objDR["Service_Tax_Percent"].ToString());
                    objIGCView.Standard_BiltiCharges = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                    objIGCView.Standard_DDCharge = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIGCView.Standard_DDCharge_Rate = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIGCView.Standard_Invoice_Rate = Util.String2Decimal(objDR["Invoice_Rate"].ToString());
                    objIGCView.Standard_Invoice_Per_How_Many_Rs = Util.String2Decimal(objDR["Invoice_Per_How_Many_Rs"].ToString());
                    objIGCView.Standard_FOVRate = Util.String2Decimal(objDR["FOV_Rate"].ToString());
                    
                    objIGCView.Standard_FOV = Util.String2Decimal(objDR["Min_FOV"].ToString());
                    objIGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                    objIGCView.Standard_HamaliCharge = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                    objIGCView.Standard_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_kg"].ToString());
                    objIGCView.Standard_HamaliPerArticles = Util.String2Decimal(objDR["Hamali_Per_Article"].ToString());
                    objIGCView.Standard_LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                    objIGCView.Standard_LocalCharge_Rate = Util.String2Decimal(objDR["Local_Charges"].ToString());

                    if (objIGCView.Is_ToPay_Charge_Require == true)
                        objIGCView.Standard_ToPayCharges = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                    else
                        objIGCView.Standard_ToPayCharges = 0;
                                        
                    objIGCView.Standard_DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                    objIGCView.Standard_Octroi_Form_Charge = Util.String2Decimal(objDR["Octroi_Form_Charges"].ToString());
                    objIGCView.Standard_Octroi_Service_Charge = Util.String2Decimal(objDR["Octroi_Service_Charges"].ToString());
                    objIGCView.Standard_GI_Charges = Util.String2Decimal(objDR["GI_Charges"].ToString());
                    objIGCView.Standard_Demurrage_Days = Util.String2Decimal(objDR["Demurrage_Days"].ToString());
                    objIGCView.Standard_Demurrage_Rate = Util.String2Decimal(objDR["Demurrage_Rate_Kg_Per_Day"].ToString());
                    objIGCView.NFormCharge = Util.String2Decimal(objDR["NForm_Charge"].ToString());
                    objIGCView.Standard_NForm_Charge = Util.String2Decimal(objDR["NForm_Charge"].ToString());
                    objIGCView.Freight_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Freight_Chg_Discount_Percent"].ToString()); ;
                    objIGCView.Hamali_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Hamali_Chg_Discount_Percent"].ToString());
                    objIGCView.Fov_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Fov_Chg_Discount_Percent"].ToString());
                    objIGCView.ToPay_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_TP_Chg_Discount_Percent"].ToString());
                    objIGCView.DD_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_DD_Chg_Discount_Percent"].ToString());

                    objIGCView.ReBookGC_Amount = 0;
                    objIGCView.UnloadingCharge = 0;

                    objIGCView.Applicable_Standard_BiltiCharges = objIGCView.Standard_BiltiCharges;

                    //objIGCView.Applicable_Standard_Invoice_Rate = objIGCView.Standard_Invoice_Rate;
                    //objIGCView.Applicable_Standard_Invoice_Per_How_Many_Rs  = objIGCView.Standard_Invoice_Per_How_Many_Rs;
                    //objIGCView.Applicable_Standard_FOVRate = objIGCView.Standard_FOVRate;
                    
                    objIGCView.Applicable_Standard_FOV = objIGCView.Standard_FOV;
                    objIGCView.Applicable_Standard_FOVPercentage = objIGCView.Standard_FOVPercentage;
                    objIGCView.Applicable_Standard_FreightRate = objIGCView.Standard_FreightRate;
                    objIGCView.Applicable_Standard_HamaliCharge = objIGCView.Standard_HamaliCharge;
                    objIGCView.Applicable_Standard_HamaliPerKg = objIGCView.Standard_HamaliPerKg;

                    objIGCView.Applicable_Standard_HamaliPerArticles = objIGCView.Standard_HamaliPerArticles;

                    objIGCView.Applicable_Standard_LocalCharge = objIGCView.Standard_LocalCharge;
                    objIGCView.Applicable_Standard_LocalCharge_Rate = objIGCView.Standard_LocalCharge_Rate ;

                    objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                    objIGCView.Applicable_Standard_MinimumChargeWeight = objIGCView.Standard_MinimumChargeWeight;
                    objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                    objIGCView.Applicable_Standard_ServiceTaxAmount = objIGCView.Standard_ServiceTaxAmount;
                    objIGCView.Applicable_Standard_ServiceTaxPercent = objIGCView.Standard_ServiceTaxPercent;
                    objIGCView.Applicable_Standard_ToPayCharges = objIGCView.Standard_ToPayCharges;
                    objIGCView.Applicable_Standard_DDCharge = objIGCView.Standard_DDCharge;
                    objIGCView.Applicable_Standard_DDCharge_Rate = objIGCView.Standard_DDCharge_Rate;
                    objIGCView.Applicable_Standard_DACCCharges = objIGCView.Standard_DACCCharges;
                    objIGCView.Applicable_Standard_CFTFactor = objIGCView.Standard_CFTFactor;
                    objIGCView.Applicable_Standard_Octroi_Form_Charge = objIGCView.Standard_Octroi_Form_Charge;
                    objIGCView.Applicable_Standard_Octroi_Service_Charge = objIGCView.Standard_Octroi_Service_Charge;
                    objIGCView.Applicable_Standard_GI_Charges = objIGCView.Standard_GI_Charges;
                    objIGCView.Applicable_Standard_Demurrage_Days = objIGCView.Standard_Demurrage_Days;
                    objIGCView.Applicable_Standard_Demurrage_Rate = objIGCView.Standard_Demurrage_Rate;

                    objIGCView.Applicable_Standard_NForm_Charge = objIGCView.Standard_NForm_Charge;

                    objIGCView.Default_Bank_Ledger_Id = Util.String2Int(objDR["Default_Bank_Ledger_Id"].ToString());
                    objIGCView.Default_Cash_Ledger_Id = Util.String2Int(objDR["Default_Cash_Ledger_Id"].ToString());


                    objIGCView.Default_Cheque_Bank_Ledger_Name  = objDR["Default_Bank_Ledger_Name"].ToString();
                    objIGCView.Default_Cheque_Branch_Ledger_Name  = objDR["Default_Branch_Ledger_Name"].ToString();
                    objIGCView.Default_Cash_Ledger_Name = objDR["Default_Cash_Ledger_Name"].ToString();
                }
                else
                {
                    objIGCView.LocalCharge = 0;
                    objIGCView.LoadingCharge = 0;
                    objIGCView.StationaryCharge = 0;
                    objIGCView.MaxStationaryCharge = 0;
                    objIGCView.DDCharge = 0;
                    objIGCView.ToPayCharge = 0;
                    objIGCView.OtherCharges = 0;
                    objIGCView.UnloadingCharge = 0;

                    objIGCView.NFormCharge = 0;
                    objIGCView.Standard_NForm_Charge = 0;
                    objIGCView.hdn_StandardMinimumChargeWeight = 0;
                    objIGCView.Standard_CFTFactor = 0;
                    objIGCView.hdn_StandardHamaliPerKg = 0;
                    objIGCView.hdn_StandardMinimumFOV = 0;
                    objIGCView.Standard_ServiceTaxPercent = Util.String2Decimal("12.36");

                    objIGCView.Standard_BiltiCharges = 0;
                    objIGCView.Standard_DDCharge = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIGCView.Standard_DDCharge_Rate = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIGCView.Standard_FOV = 0;
                    objIGCView.Standard_FOVPercentage = 0;
                    objIGCView.Standard_HamaliCharge = 0;
                    objIGCView.Standard_HamaliPerKg = 0;
                    objIGCView.Standard_HamaliPerArticles = 0;
                    objIGCView.Standard_LocalCharge = 0;
                    objIGCView.Standard_LocalCharge_Rate = 0;
                    objIGCView.Standard_ToPayCharges = 0;
                    objIGCView.Freight_Charge_Discount_Percent = 0;
                    objIGCView.Hamali_Charge_Discount_Percent = 0;
                    objIGCView.Fov_Charge_Discount_Percent = 0;
                    objIGCView.ToPay_Charge_Discount_Percent = 0;
                    objIGCView.DD_Charge_Discount_Percent = 0;
                    objIGCView.Standard_Octroi_Form_Charge = 0;
                    objIGCView.Standard_Octroi_Service_Charge = 0;
                    objIGCView.Standard_GI_Charges = 0;
                    objIGCView.Standard_Demurrage_Days = 0;
                    objIGCView.Standard_Demurrage_Rate = 0;

                    objIGCView.Applicable_Standard_BiltiCharges = objIGCView.Standard_BiltiCharges;
                    objIGCView.Applicable_Standard_FOV = objIGCView.Standard_FOV;
                    objIGCView.Applicable_Standard_FOVPercentage = objIGCView.Standard_FOVPercentage;
                    objIGCView.Applicable_Standard_FreightRate = objIGCView.Standard_FreightRate;
                    objIGCView.Applicable_Standard_HamaliCharge = objIGCView.Standard_HamaliCharge;
                    objIGCView.Applicable_Standard_HamaliPerKg = objIGCView.Standard_HamaliPerKg;
                    objIGCView.Applicable_Standard_HamaliPerArticles = objIGCView.Standard_HamaliPerArticles;

                    objIGCView.Applicable_Standard_LocalCharge = objIGCView.Standard_LocalCharge;
                    objIGCView.Applicable_Standard_LocalCharge_Rate  = objIGCView.Standard_LocalCharge_Rate ;
                    
                    objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                    objIGCView.Applicable_Standard_MinimumChargeWeight = objIGCView.Standard_MinimumChargeWeight;
                    objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                    objIGCView.Applicable_Standard_ServiceTaxAmount = objIGCView.Standard_ServiceTaxAmount;
                    objIGCView.Applicable_Standard_ServiceTaxPercent = objIGCView.Standard_ServiceTaxPercent;
                    objIGCView.Applicable_Standard_ToPayCharges = objIGCView.Standard_ToPayCharges;
                    objIGCView.Applicable_Standard_DDCharge = objIGCView.Standard_DDCharge;
                    objIGCView.Applicable_Standard_DDCharge_Rate = objIGCView.Standard_DDCharge_Rate;
                    objIGCView.Applicable_Standard_CFTFactor = objIGCView.Standard_CFTFactor;
                    objIGCView.Applicable_Standard_Octroi_Form_Charge = objIGCView.Standard_Octroi_Form_Charge;
                    objIGCView.Applicable_Standard_Octroi_Service_Charge = objIGCView.Standard_Octroi_Service_Charge;
                    objIGCView.Applicable_Standard_GI_Charges = objIGCView.Standard_GI_Charges;
                    objIGCView.Applicable_Standard_Demurrage_Days = objIGCView.Standard_Demurrage_Days;
                    objIGCView.Applicable_Standard_Demurrage_Rate = objIGCView.Standard_Demurrage_Rate;

                    objIGCView.Applicable_Standard_NForm_Charge = objIGCView.Standard_NForm_Charge;

                    objIGCView.Default_Bank_Ledger_Id = 0;
                    objIGCView.Default_Cash_Ledger_Id = 0;

                    objIGCView.Default_Cheque_Bank_Ledger_Name  = "";
                    objIGCView.Default_Cheque_Branch_Ledger_Name  = "";
                    objIGCView.Default_Cash_Ledger_Name = "";

                    Common.DisplayErrors(1016); // Branch Rate Parameter Not Define.
                }
            }
            else
            {
                objIGCView.LocalCharge = 0;
                objIGCView.LoadingCharge = 0;
                objIGCView.StationaryCharge = 0;
                objIGCView.MaxStationaryCharge = 0;
                objIGCView.DDCharge = 0;
                objIGCView.ToPayCharge = 0;
                objIGCView.OtherCharges = 0;
                objIGCView.UnloadingCharge = 0;
                
                objIGCView.NFormCharge = 0;
                objIGCView.Standard_NForm_Charge = 0;
                objIGCView.hdn_StandardMinimumChargeWeight = 0;
                objIGCView.Standard_CFTFactor = 0;
                objIGCView.hdn_StandardHamaliPerKg = 0;
                objIGCView.hdn_StandardMinimumFOV = 0;

                objIGCView.Standard_ServiceTaxPercent = Util.String2Decimal("12.36");

                objIGCView.Standard_BiltiCharges = 0;
                objIGCView.Standard_DDCharge = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                objIGCView.Standard_DDCharge_Rate = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                objIGCView.Standard_FOV = 0;
                objIGCView.Standard_FOVPercentage = 0;
                objIGCView.Standard_HamaliCharge = 0;
                objIGCView.Standard_HamaliPerKg = 0;
                objIGCView.Standard_HamaliPerArticles = 0;
                objIGCView.Standard_LocalCharge = 0;
                objIGCView.Standard_ToPayCharges = 0;
                objIGCView.Freight_Charge_Discount_Percent = 0;
                objIGCView.Hamali_Charge_Discount_Percent = 0;
                objIGCView.Fov_Charge_Discount_Percent = 0;
                objIGCView.ToPay_Charge_Discount_Percent = 0;
                objIGCView.DD_Charge_Discount_Percent = 0;
                objIGCView.Standard_Octroi_Form_Charge = 0;
                objIGCView.Standard_Octroi_Service_Charge = 0;
                objIGCView.Standard_GI_Charges = 0;
                objIGCView.Standard_Demurrage_Days = 0;
                objIGCView.Standard_Demurrage_Rate = 0;

                objIGCView.Applicable_Standard_BiltiCharges = objIGCView.Standard_BiltiCharges;
                objIGCView.Applicable_Standard_FOV = objIGCView.Standard_FOV;
                objIGCView.Applicable_Standard_FOVPercentage = objIGCView.Standard_FOVPercentage;
                objIGCView.Applicable_Standard_FreightRate = objIGCView.Standard_FreightRate;
                objIGCView.Applicable_Standard_HamaliCharge = objIGCView.Standard_HamaliCharge;
                objIGCView.Applicable_Standard_HamaliPerKg = objIGCView.Standard_HamaliPerKg;
                objIGCView.Applicable_Standard_HamaliPerArticles = objIGCView.Standard_HamaliPerArticles;

                objIGCView.Applicable_Standard_LocalCharge = objIGCView.Standard_LocalCharge;
                objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                objIGCView.Applicable_Standard_MinimumChargeWeight = objIGCView.Standard_MinimumChargeWeight;
                objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                objIGCView.Applicable_Standard_ServiceTaxAmount = objIGCView.Standard_ServiceTaxAmount;
                objIGCView.Applicable_Standard_ServiceTaxPercent = objIGCView.Standard_ServiceTaxPercent;
                objIGCView.Applicable_Standard_ToPayCharges = objIGCView.Standard_ToPayCharges;
                objIGCView.Applicable_Standard_DDCharge = objIGCView.Standard_DDCharge;
                objIGCView.Applicable_Standard_DDCharge_Rate = objIGCView.Standard_DDCharge_Rate;
                objIGCView.Applicable_Standard_CFTFactor = objIGCView.Standard_CFTFactor;
                objIGCView.Applicable_Standard_Octroi_Form_Charge = objIGCView.Standard_Octroi_Form_Charge;
                objIGCView.Applicable_Standard_Octroi_Service_Charge = objIGCView.Standard_Octroi_Service_Charge;
                objIGCView.Applicable_Standard_GI_Charges = objIGCView.Standard_GI_Charges;
                objIGCView.Applicable_Standard_Demurrage_Days = objIGCView.Standard_Demurrage_Days;
                objIGCView.Applicable_Standard_Demurrage_Rate = objIGCView.Standard_Demurrage_Rate;

                objIGCView.Applicable_Standard_NForm_Charge = objIGCView.Standard_NForm_Charge;

                objIGCView.Default_Bank_Ledger_Id = 0;
                objIGCView.Default_Cash_Ledger_Id = 0;

                objIGCView.Default_Cheque_Bank_Ledger_Name  = "";
                objIGCView.Default_Cheque_Branch_Ledger_Name  = "";
                objIGCView.Default_Cash_Ledger_Name = "";
            }

            Get_Applicable_Service_Tax();
            objIGCView.ServiceTax_Label = "Service Tax " + objIGCView.Applicable_Standard_ServiceTaxPercent.ToString() + "%";
        }

        public void Read_GC_Details(Boolean Is_Attached_Gc)
        {         
            DataSet objDS_GC = new DataSet();
            DataSet objDS_ContractDetails= new DataSet();
            
            objDS_GC = objGCModel.Read_GC_Details(Is_Attached_Gc);

            if (objDS_GC.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS_GC.Tables[0].Rows[0];

                objIGCView.Previous_Article_ID = Util.String2Int(objDR["Previous_Article_ID_1"].ToString());
                objIGCView.Previous_Status_ID = Util.String2Int(objDR["Previous_Status_ID_1"].ToString());
                objIGCView.Previous_Document_ID = Util.String2Int(objDR["Previous_Document_ID_1"].ToString());
                objIGCView.Previous_Document_No_For_Print = objDR["Previous_Document_No_For_Print_1"].ToString();
                objIGCView.Previous_Document_Date = Convert.ToDateTime(objDR["Previous_Document_Date_1"].ToString());
                objIGCView.CustomerRefNo = objDR["Customer_Ref_No"].ToString();

                if (objIGCView.keyID > 0 && Is_Attached_Gc == false)
                {
                    objIGCView.Is_Attached = Util.String2Bool(objDR["Is_Attached"].ToString());
                    objIGCView.Attached_GC_Id = Util.String2Int(objDR["Attached_GC_Id"].ToString());
                    objIGCView.Attached_GC_No_For_Print = objDR["Attached_GC_No_For_Print"].ToString();
                    objIGCView.PrivateMark = objDR["Private_Mark"].ToString();

                    objIGCView.Is_ReBooked = Util.String2Bool(objDR["Is_ReBooked"].ToString());

                    if (objIGCView.Is_ReBooked == true)
                    {
                        objIGCView.ReBook_GC_Id = Util.String2Int(objDR["ReBook_Against_GC_Id"].ToString());
                        objIGCView.Attached_GC_No_For_Print = objDR["ReBook_GC_No_For_Print"].ToString();

                        objIGCView.PrivateMark = objDR["Private_Mark"].ToString();

                        objIGCView.ReBookGC_SubTotal = Util.String2Decimal(objDR["ReBook_Charges"].ToString());
                        objIGCView.ReBookGC_Amount = Util.String2Decimal(objDR["ReBook_Charges"].ToString());
                    }
                    else
                    {
                        objIGCView.ReBook_GC_Id = 0;
                        objIGCView.ReBookGC_SubTotal = 0;
                        objIGCView.ReBookGC_Amount = 0;
                    }
                }
                else
                {
                    if (Is_Attached_Gc == false)
                    {
                        objIGCView.Is_Attached = false;
                    }                                     
                }

                if (!Is_Attached_Gc)
                {
                    objIGCView.GC_No_For_Print = objDR["GC_No_For_Print"].ToString();
                    objIGCView.AgencyGC_No_For_Print = objDR["Agency_GC_No"].ToString();
                    objIGCView.PrivateMark = objDR["Private_Mark"].ToString();
                    objIGCView.BookingDate = Convert.ToDateTime(objDR["GC_Date"]);
                    objIGCView.BookingTime = objDR["GC_Time"].ToString();
                }

                if (Is_Attached_Gc) // && objIGCView.keyID <= 0)
                {
                    objIGCView.Attached_GC_Id = Util.String2Int(objDR["GC_Id"].ToString());
                    objIGCView.ReBook_GC_Id = Util.String2Int(objDR["GC_Id"].ToString());

                    objIGCView.Attached_GC_No_For_Print = objDR["GC_No_For_Print"].ToString();
                    objIGCView.PrivateMark = objDR["Private_Mark"].ToString();

                    if (Util.String2Int(objDR["Payment_Type_Id"].ToString()) == 1)
                    {
                        objIGCView.Is_ReBookGC_ToPay = true;
                        objIGCView.ReBookGC_SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                        objIGCView.ReBookGC_Amount = Util.String2Decimal(objDR["Sub_Total"].ToString());

                        objIGCView.ReBookGC_OctroiAmount = Util.String2Decimal(objDR["Oct_Amount"].ToString());
                    }
                    else
                    {
                        objIGCView.Is_ReBookGC_ToPay = false;
                        objIGCView.ReBookGC_SubTotal = 0;
                        objIGCView.ReBookGC_Amount = 0;
                    }
                    objIGCView.ReBookGC_OctroiAmount = Util.String2Decimal(objDR["Oct_Amount"].ToString());
                    objIGCView.ReBook_GCOctroiPaidByID = Util.String2Int(objDR["Octroi_Paid_By_ID"].ToString());

                    objIGCView.GC_Status_Id_At_Current_Branch = Util.String2Int(objDR["GC_Status_Id_At_Current_Branch"].ToString());
                    objIGCView.GC_Articles_At_Current_Branch = Util.String2Int(objDR["GC_Articles_At_Current_Branch"].ToString());
                }

                objIGCView.CustomerRefNo = objDR["Customer_Ref_No"].ToString();

                objIGCView.ConsignmentTypeId = Util.String2Int(objDR["Consignment_Type_Id"].ToString());
                objIGCView.BookingTypeId = Util.String2Int(objDR["Booking_Type_Id"].ToString());

                Get_BookingSubType();

                objIGCView.BookingSubTypeId = Util.String2Int(objDR["Booking_Sub_Type_Id"].ToString());
                objIGCView.DeliveryTypeId = Util.String2Int(objDR["Delivery_Type_Id"].ToString());
                objIGCView.DeliveryAgainstId = Util.String2Int(objDR["Door_Delivery_Against_ID"].ToString());

                objIGCView.SetFromLocation(objDR["From_Location_Name"].ToString(), objDR["From_Location_ID"].ToString());
                objIGCView.SetToLocation(objDR["To_Location_Name"].ToString(), objDR["To_Location_ID"].ToString());
                objIGCView.SetConsingee(objDR["Consignee_Name"].ToString(), objDR["Consignee_Client_ID"].ToString());
                objIGCView.SetConsingor(objDR["Consignor_Name"].ToString(), objDR["Consignor_Client_ID"].ToString());

                objIGCView.BookingBranchId = Util.String2Int(objDR["Booking_Branch_Id"].ToString());
                objIGCView.ArrivedFromBranchId = Util.String2Int(objDR["Arrived_From_Branch_Id"].ToString());
                objIGCView.ArrivedDate = Convert.ToDateTime(objDR["Arrived_Date"].ToString());

                objIGCView.SetBookingBranch(objDR["Booking_Branch_Name"].ToString(), objDR["Booking_Branch_Id"].ToString());
                objIGCView.SetArrivedFromBranch(objDR["Arrived_From_Branch_Name"].ToString(), objDR["Arrived_From_Branch_Id"].ToString());

                Get_BranchRateParameter(); 

                objIGCView.FromLocationId = Util.String2Int(objDR["From_Location_ID"].ToString());
                objIGCView.ToLocationId = Util.String2Int(objDR["To_Location_ID"].ToString());
                objIGCView.DeliveryBaranchId = Util.String2Int(objDR["Delivery_Branch_Id"].ToString());
                objIGCView.DeliveryBranchName = objDR["Delivery_Branch_Name"].ToString();
                objIGCView.VehicleTypeId = Util.String2Int(objDR["Vehicle_Type_Id"].ToString());
                objIGCView.VehicleNo = objDR["Vehicle_No"].ToString();
                objIGCView.PickupTypeId = Util.String2Int(objDR["Pickup_Type_Id"].ToString());
                objIGCView.STMNo = objDR["STM_No"].ToString();
                objIGCView.FeasibilityRouteSurveyNo = objDR["Feasibility_Route_Survey_No"].ToString();
                objIGCView.ServiceTypeId = Util.String2Int(objDR["Service_Type_Id"].ToString());
                objIGCView.Is_ST_Abatment_Required = Util.String2Bool(objDR["Is_ST_Abatment"].ToString());

                //----------------------------------------------------------------------------------------

                objIGCView.ConsignorId = Util.String2Int(objDR["Consignor_Client_ID"].ToString());
                objIGCView.EncreptedConsignorId = Util.EncryptInteger(objIGCView.ConsignorId);
                objIGCView.ConsignorAddressLine1 = objDR["Consignor_Add1"].ToString();
                objIGCView.ConsignorAddressLine2 = objDR["Consignor_Add2"].ToString();
                objIGCView.ConsignorCountryId = Util.String2Int(objDR["Consignor_Country_ID"].ToString());
                objIGCView.ConsignorCountryName = objDR["Consignor_Country"].ToString();
                objIGCView.ConsignorStateId = Util.String2Int(objDR["Consignor_State_ID"].ToString());
                objIGCView.ConsignorStateName = objDR["Consignor_State"].ToString();
                objIGCView.ConsignorCityId = Util.String2Int(objDR["Consignor_City_ID"].ToString());
                objIGCView.ConsignorCity = objDR["Consignor_City"].ToString();
                objIGCView.ConsignorPinCode = objDR["Consignor_Pin_Code"].ToString();
                objIGCView.ConsignorTelNo = objDR["Consignor_Tel_No"].ToString();
                objIGCView.ConsignorMobileNo = objDR["Consignor_Mobile_No"].ToString();
                objIGCView.ConsignorEmail = objDR["Consignor_EMail"].ToString();
                objIGCView.ConsignorCSTNo = objDR["Consignor_CST_TIN_No"].ToString();
                
                objIGCView.ConsignorDetails = objIGCView.ConsignorAddressLine1 + ", " + objIGCView.ConsignorAddressLine2 + ", " +
                                              objIGCView.ConsignorCity  + ", " + objIGCView.ConsignorPinCode + ", " + 
                                              objIGCView.ConsignorStateName + ", " + " Ph : " + objIGCView.ConsignorTelNo;

                objIGCView.Is_RegularConsignor = Convert.ToBoolean(objDR["Is_Consignor_Regular_Client"].ToString()) == true ? 1 : 0;

                //----------------------------------------------------------------------------------------

                objIGCView.ConsigneeId = Util.String2Int(objDR["Consignee_Client_ID"].ToString());
                objIGCView.EncreptedConsigneeId = Util.EncryptInteger(objIGCView.ConsigneeId);

                objIGCView.Session_ConsigneeName = objDR["Consignee_Name"].ToString();
                objIGCView.ConsigneeAddressLine1 = objDR["Consignee_Add1"].ToString();
                objIGCView.ConsigneeAddressLine2 = objDR["Consignee_Add2"].ToString();
                objIGCView.Session_ConsigneeAddressLine1 = objDR["DD_Address_1"].ToString();
                objIGCView.Session_ConsigneeAddressLine2 = objDR["DD_Address_2"].ToString();
                objIGCView.ConsigneeDDAddressLine1 = objDR["DD_Address_1"].ToString();
                objIGCView.ConsigneeDDAddressLine2 = objDR["DD_Address_2"].ToString();

                objIGCView.ConsigneeCountryId = Util.String2Int(objDR["Consignee_Country_ID"].ToString());
                objIGCView.ConsigneeCountryName = objDR["Consignee_Country"].ToString();
                objIGCView.ConsigneeStateId = Util.String2Int(objDR["Consignee_State_ID"].ToString());
                objIGCView.ConsigneeStateName = objDR["Consignee_State"].ToString();
                objIGCView.ConsigneeCityId = Util.String2Int(objDR["Consignee_City_ID"].ToString());
                objIGCView.ConsigneeCity = objDR["Consignee_City"].ToString();
                objIGCView.ConsigneePinCode = objDR["Consignee_Pin_Code"].ToString();
                objIGCView.ConsigneeTelNo = objDR["Consignee_Tel_No"].ToString();
                objIGCView.ConsigneeMobileNo = objDR["Consignee_Mobile_No"].ToString();
                objIGCView.ConsigneeEmail = objDR["Consignee_EMail"].ToString();
                objIGCView.ConsigneeCSTNo = objDR["Consignee_CST_TIN_No"].ToString();

                objIGCView.ConsigneeDetails = objIGCView.ConsigneeAddressLine1 + ", " + objIGCView.ConsigneeAddressLine2 + ", " +
                                              objIGCView.ConsigneeCity + ", " + objIGCView.ConsigneePinCode + ", " + 
                                              objIGCView.ConsigneeStateName + ", " + " Ph : " + objIGCView.ConsigneeTelNo;

                objIGCView.Is_RegularConsignee = Convert.ToBoolean(objDR["Is_Consignee_Regular_Client"].ToString()) == true ? 1 : 0;

                //--------------------------------- Contract Details ----------------------------------

                objIGCView.SetContractualClient(objDR["Contractual_Client_Name"].ToString(), objDR["Contractual_Client_Id"].ToString());

                objIGCView.Contractual_ClientId = Util.String2Int(objDR["Contractual_Client_Id"].ToString());

                Fill_ContractBranches();

                try
                {
                    objIGCView.Contract_BranchId = Util.String2Int(objDR["Contract_Branch_ID"].ToString());
                    Fill_Contract();
                    objIGCView.ContractId = Util.String2Int(objDR["Contract_ID"].ToString());
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    objIGCView.Contract_BranchId = 0;
                    objIGCView.ContractId = 0;
                }

                if (Util.String2Int(objDR["Contract_ID"].ToString()) > 0 && objIGCView.ContractId > 0)
                {
                    objIGCView.Is_ContractApplied = 1;
                }
                else
                {
                    objIGCView.Is_ContractApplied = 0;
                }

                objDS_ContractDetails = Get_ContractDetails("From_GC_Read_Value");
                objIGCView.Session_DS_ContractDetails = objDS_ContractDetails;

                //----------------------------------------------------------------------------------------

                objIGCView.PaymentTypeId = Util.String2Int(objDR["Payment_Type_Id"].ToString());
                objIGCView.GCRiskId = Util.String2Int(objDR["Risk_Type_ID"].ToString());
                objIGCView.InsuranceCompany = objDR["Insurance_Company"].ToString();
                objIGCView.PolicyNo = objDR["Policy_No"].ToString();
                objIGCView.PolicyAmount = Util.String2Decimal(objDR["Policy_Amount"].ToString());
                objIGCView.RiskAmount = Util.String2Decimal(objDR["Risk_Amount"].ToString());
                objIGCView.PolicyExpDate = Convert.ToDateTime(objDR["Policy_Exp_Date"].ToString());
                objIGCView.DeliveryWayTypeId = Util.String2Int(objDR["DeliveryWayTypeID"].ToString());
                objIGCView.Session_InsuranceCompany = objIGCView.InsuranceCompany;
                objIGCView.Session_PolicyNo = objIGCView.PolicyNo;
                objIGCView.Session_PolicyAmount = objIGCView.PolicyAmount;
                objIGCView.Session_RiskAmount = objIGCView.RiskAmount;
                objIGCView.Session_PolicyExpDate = objIGCView.PolicyExpDate;

                //----------------------------------------------------------------------------------------

                objIGCView.TotalArticles = Util.String2Int(objDR["Total_Articles"].ToString());
                objIGCView.TotalLength = Util.String2Decimal(objDR["Total_Length"].ToString());
                objIGCView.TotalWidth = Util.String2Decimal(objDR["Total_Width"].ToString());
                objIGCView.TotalWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIGCView.TotalHeight = Util.String2Decimal(objDR["Total_Height"].ToString());
                //----------------------------------------------------------------------------------------

                objIGCView.UnitOfMeasurementId = Util.String2Int(objDR["Unit_Of_Measurement_ID"].ToString());
                objIGCView.UnitOfMeasurmentLength = Util.String2Decimal(objDR["Total_Length"].ToString());
                objIGCView.UnitOfMeasurmentWidth = Util.String2Decimal(objDR["Total_Width"].ToString());
                objIGCView.UnitOfMeasurmentHeight = Util.String2Decimal(objDR["Total_Height"].ToString());

                //----------------------------------------------------------------------------------------

                objIGCView.HeightInFeet = Util.String2Decimal(objDR["Total_Height_In_Ft"].ToString());
                objIGCView.WidthInFeet = Util.String2Decimal(objDR["Total_Width_In_Ft"].ToString());
                objIGCView.LengthInFeet = Util.String2Decimal(objDR["Total_Length_In_Ft"].ToString());

                //----------------------------------------------------------------------------------------

                objIGCView.FreightBasisId = Util.String2Int(objDR["Freight_Basis_ID"].ToString());
                objIGCView.VolumetricFreightUnitId = Util.String2Int(objDR["Volumetric_Freight_Unit_ID"].ToString());

                objIGCView.TotalCFT = Util.String2Decimal(objDR["Total_CFT"].ToString());
                objIGCView.TotalCBM = Util.String2Decimal(objDR["Total_CBM"].ToString());

                // CFT Factor
                objIGCView.VolumetricToKgFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());

                //----------------------------------------------------------------------------------------

                objIGCView.ActualWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIGCView.ChargeWeight = Util.String2Decimal(objDR["Charged_Weight"].ToString());
                objIGCView.FreightRate = Util.String2Decimal(objDR["Freight_Rate"].ToString());
                objIGCView.TotalInvoiceAmount = Util.String2Decimal(objDR["Total_Invoice_Value"].ToString());
                objIGCView.LengthChargeHeadId = Util.String2Int(objDR["Length_Charge_Head_Id"].ToString());
                objIGCView.ServiceTaxPayableBy = Util.String2Int(objDR["Tax_Payable_By"].ToString());

                objIGCView.Is_ServiceTaxApplicableForConsignor = Convert.ToBoolean(objDR["Is_Consignor_Service_Tax_Applicable"].ToString()) == true ? 1 : 0;
                objIGCView.Is_ServiceTaxApplicableForConsignee = Convert.ToBoolean(objDR["Is_Consignee_Service_Tax_Applicable"].ToString()) == true ? 1 : 0;
                objIGCView.Is_ODA = Util.String2Bool(objDR["Is_ODA"].ToString());
                objIGCView.Is_Opening_GC = Util.String2Bool(objDR["Is_Opening_Gc"].ToString());
                objIGCView.Is_Agency_GC = Util.String2Bool(objDR["Is_Agency_Booking"].ToString());

                objIGCView.ODAChargesUpTo500Kg = Util.String2Decimal(objDR["Oda_charges_upto_500_Kg"].ToString());
                objIGCView.ODAChargesAbove500Kg = Util.String2Decimal(objDR["Oda_charges_above_500_Kg"].ToString());
                objIGCView.Is_OctroiApplicable = Util.String2Bool(objDR["Is_Octroi_Applicable"].ToString());
                objIGCView.Is_ToPayBookingApplicable = Util.String2Bool(objDR["Is_To_Pay_Booking"].ToString());

                objIGCView.Is_MultipleBilling = Util.String2Bool(objDR["Is_Multiple_Billing"].ToString());

                if (!objIGCView.Is_MultipleBilling)
                {
                    objIGCView.SetBillingParty(objDR["Billing_Client_Name"].ToString(), objDR["Billing_Client_ID"].ToString());
                }
                else
                {
                    objIGCView.SetBillingParty("", "0");
                }

                objIGCView.BillingPartyId = Util.String2Int(objDR["Billing_Client_ID"].ToString());
                if (objIGCView.BillingPartyId > 0)
                {
                    Get_BillingPartyDetails();
                }
                objIGCView.BillingHierarchy = objDR["Billing_Hierarchy"].ToString();
                objIGCView.BillingBranchId = Util.String2Int(objDR["Billing_Branch_ID"].ToString());
                objIGCView.BillingRemark = objDR["Billing_Remarks"].ToString();
                objIGCView.Freight = Util.String2Decimal(objDR["Freight_Amt"].ToString());
                objIGCView.LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                objIGCView.LoadingCharge = Util.String2Decimal(objDR["Hamali_Charges"].ToString());
                objIGCView.StationaryCharge = Util.String2Decimal(objDR["Bilti_Charges"].ToString());
                objIGCView.FOVRiskCharge = Util.String2Decimal(objDR["FOV"].ToString());
                objIGCView.ToPayCharge = Util.String2Decimal(objDR["TP_Charges"].ToString());

                objIGCView.DDCharge = Util.String2Decimal(objDR["DD_Charges"].ToString());
                objIGCView.LengthCharge = Util.String2Decimal(objDR["Length_Charge"].ToString());

                //objIGCView.Aoc = Util.String2Decimal(objDR["Length_Charge"].ToString());

                objIGCView.UnloadingCharge = Util.String2Decimal(objDR["Unloading_Charge"].ToString());

                objIGCView.NFormCharge = Util.String2Decimal(objDR["NForm_Charge"].ToString());
                                
                objIGCView.Standard_NForm_Charge = objIGCView.NFormCharge  ;

                objIGCView.Applicable_Standard_DDCharge_Rate = Util.String2Decimal(objDR["Std_DD_Charge_Rate"].ToString());
                objIGCView.Standard_DDCharge_Rate = Util.String2Decimal(objDR["Std_DD_Charge_Rate"].ToString());

                objIGCView.DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                objIGCView.Standard_DACCCharges = Util.String2Decimal(objDR["Std_DACC_Charge"].ToString());
                objIGCView.Applicable_Standard_DACCCharges = Util.String2Decimal(objDR["Std_DACC_Charge"].ToString());

                objIGCView.OtherCharges = Util.String2Decimal(objDR["Other_Charges"].ToString());
                objIGCView.SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                objIGCView.Abatment = Util.String2Decimal(objDR["Tax_Abate"].ToString());
                objIGCView.TaxableAmount = Util.String2Decimal(objDR["Amt_Taxable"].ToString());
                objIGCView.ServiceTax = Util.String2Decimal(objDR["Service_Tax_Amount"].ToString());

                if (!Is_Attached_Gc) // && objIGCView.keyID <= 0)
                {
                    objIGCView.ReBookGC_OctroiAmount = Util.String2Decimal(objDR["ReBook_Octroi_Amount"].ToString());
                    objIGCView.ReBook_GCOctroiPaidByID = Util.String2Int(objDR["ReBook_GC_Octroi_Paid_By_ID"].ToString());
                }

                objIGCView.TotalGCAmount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());

                objIGCView.Previous_SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                objIGCView.Previous_GrandTotal = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());

                objIGCView.Advance = Util.String2Decimal(objDR["Advance_Amount"].ToString());
                objIGCView.CashAmount = Util.String2Decimal(objDR["Cash_Amount"].ToString());
                objIGCView.ChequeAmount = Util.String2Decimal(objDR["Cheque_Amount"].ToString());
                objIGCView.ChequeDate = Convert.ToDateTime(objDR["Cheque_Date"].ToString());

                if (Util.String2Int(objDR["Cheque_No"].ToString()) == 0)
                {
                    objIGCView.ChequeNo = "";
                }
                else
                {
                    objIGCView.ChequeNo = Convert.ToInt32(objDR["Cheque_No"].ToString()).ToString("000000");
                }

                objIGCView.BankName = objDR["Bank_Name"].ToString();

                if (Util.String2Int(objDR["Loading_Supervisor_ID"].ToString()) > 0)
                {
                    objIGCView.SetLoadingSuperVisor(objDR["Loading_Supervisor_Name"].ToString(), objDR["Loading_Supervisor_ID"].ToString());
                }
                else
                {
                    objIGCView.SetLoadingSuperVisor("", "0");
                }

                if (Util.String2Int(objDR["Agency_ID"].ToString()) > 0)
                {
                    objIGCView.SetAgency(objDR["Agency_Name"].ToString(), objDR["Agency_ID"].ToString());
                }
                else
                {
                    objIGCView.SetAgency("", "0");
                }
                if (Util.String2Int(objDR["Agency_Ledger_ID"].ToString()) > 0)
                {
                    objIGCView.SetAgencyLedger(objDR["Agency_Ledger_Name"].ToString(), objDR["Agency_Ledger_ID"].ToString());
                }
                else
                {
                    objIGCView.SetAgencyLedger("", "0");
                }

                if (Util.String2Int(objDR["Marketing_Executive_ID"].ToString()) > 0)
                {
                    objIGCView.SetMarketingExecutive(objDR["Marketing_Executive_Name"].ToString(), objDR["Marketing_Executive_ID"].ToString());
                }
                else
                {
                    objIGCView.SetLoadingSuperVisor("", "0");
                }

                objIGCView.LoadingSuperVisorId = Util.String2Int(objDR["Loading_Supervisor_ID"].ToString());
                objIGCView.MarketingExecutiveId = Util.String2Int(objDR["Marketing_Executive_ID"].ToString());

                //----------------------------------------------------------------------------------------

                objIGCView.Standard_FreightRate = Util.String2Decimal(objDR["Std_Freight_Rate"].ToString());
                objIGCView.Standard_ServiceTaxPercent = Util.String2Decimal(objDR["Service_Tax_Percent"].ToString());
                objIGCView.Standard_BiltiCharges = Util.String2Decimal(objDR["Std_Bilti_Charges"].ToString());
                objIGCView.Standard_DDCharge = Util.String2Decimal(objDR["Std_DD_Charge"].ToString());
                objIGCView.Standard_FOV = Util.String2Decimal(objDR["Std_FOV"].ToString());
                objIGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["Std_FOVPercent"].ToString());
                objIGCView.Standard_FreightAmount = Util.String2Decimal(objDR["Std_Freight_Amt"].ToString());
                objIGCView.Standard_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());
                objIGCView.Standard_HamaliCharge = Util.String2Decimal(objDR["Std_Hamali_Charge"].ToString());
                objIGCView.Standard_HamaliPerArticles = Util.String2Decimal(objDR["Hamali_Per_Articles"].ToString());
                objIGCView.Standard_LocalCharge = Util.String2Decimal(objDR["Std_Local_Charge"].ToString());
                objIGCView.Standard_ServiceTaxAmount = Util.String2Decimal(objDR["Std_Service_Tax_Amount"].ToString());
                objIGCView.Standard_ToPayCharges = Util.String2Decimal(objDR["Std_TP_Charges"].ToString());
                objIGCView.Standard_CFTFactor = Util.String2Decimal(objDR["Std_CFT_Factor"].ToString());
                objIGCView.Standard_Octroi_Form_Charge = Util.String2Decimal(objDR["Std_Octroi_Form_Charges"].ToString());
                objIGCView.Standard_Octroi_Service_Charge = Util.String2Decimal(objDR["Std_Octroi_Service_Charges"].ToString());
                objIGCView.Standard_Demurrage_Days = Util.String2Decimal(objDR["Std_Demurrage_Days"].ToString());
                objIGCView.Standard_Demurrage_Rate = Util.String2Decimal(objDR["Std_Demurrage_Rate"].ToString());
                objIGCView.Standard_GI_Charges = Util.String2Decimal(objDR["Std_GI_Charges"].ToString());
                objIGCView.Standard_LengthCharge = Util.String2Decimal(objDR["Length_Charge"].ToString());

                //----------------------------------------------------------------------------------------

                objIGCView.Applicable_Standard_BiltiCharges = objIGCView.Standard_BiltiCharges;
                objIGCView.Applicable_Standard_FOV = objIGCView.Standard_FOV;// objIGCView.FOVRiskCharge;
                objIGCView.Applicable_Standard_FOVPercentage = objIGCView.Standard_FOVPercentage;
                objIGCView.Applicable_Standard_FreightRate = objIGCView.Standard_FreightRate;
                objIGCView.Applicable_Standard_HamaliCharge = objIGCView.Standard_HamaliCharge;
                objIGCView.Applicable_Standard_HamaliPerKg = objIGCView.Standard_HamaliPerKg;
                objIGCView.Applicable_Standard_HamaliPerArticles = objIGCView.Standard_HamaliPerArticles;
                objIGCView.Applicable_Standard_LocalCharge = objIGCView.Standard_LocalCharge;

                if (objIGCView.ContractId > 0)
                {
                    objIGCView.Applicable_Standard_MinimumFOV = 0;
                    objIGCView.Applicable_Standard_MinimumChargeWeight = 0;
                    objIGCView.Applicable_Standard_MinimumFOV = 0;
                }
                else
                {
                    objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                    objIGCView.Applicable_Standard_MinimumChargeWeight = objIGCView.Standard_MinimumChargeWeight;
                    objIGCView.Applicable_Standard_MinimumFOV = objIGCView.Standard_MinimumFOV;
                }

                objIGCView.Applicable_Standard_ServiceTaxAmount = objIGCView.Standard_ServiceTaxAmount;
                objIGCView.Applicable_Standard_ServiceTaxPercent = objIGCView.Standard_ServiceTaxPercent;

                objIGCView.ServiceTax_Label = "Service Tax " + objIGCView.Applicable_Standard_ServiceTaxPercent.ToString() + "%";

                objIGCView.Applicable_Standard_ToPayCharges = objIGCView.Standard_ToPayCharges;
                objIGCView.Applicable_Standard_DDCharge = objIGCView.Standard_DDCharge;
                objIGCView.Applicable_Standard_DDCharge_Rate = objIGCView.Standard_DDCharge_Rate;
                objIGCView.Applicable_Standard_CFTFactor = objIGCView.Standard_CFTFactor;
                objIGCView.Applicable_Standard_Octroi_Form_Charge = objIGCView.Standard_Octroi_Form_Charge;
                objIGCView.Applicable_Standard_Octroi_Service_Charge = objIGCView.Standard_Octroi_Service_Charge;
                objIGCView.Applicable_Standard_GI_Charges = objIGCView.Standard_GI_Charges;
                objIGCView.Applicable_Standard_Demurrage_Days = objIGCView.Standard_Demurrage_Days;
                objIGCView.Applicable_Standard_Demurrage_Rate = objIGCView.Standard_Demurrage_Rate;

                objIGCView.Applicable_Standard_NForm_Charge = objIGCView.Standard_NForm_Charge;
                //----------------------------------------------------------------------------------------

                objIGCView.OtherChargesRemark = objDR["GC_Remarks_Other_Charges"].ToString();
                objIGCView.InstructionRemark = objDR["GC_Remarks"].ToString();
                objIGCView.Enclosures = objDR["Enclosures"].ToString();

                objIGCView.Is_POD = Convert.ToBoolean(objDR["Acknowledge"].ToString());
                objIGCView.Is_SignedByConsignor = Convert.ToBoolean(objDR["Is_Signed_By_Consignor"].ToString());
                objIGCView.RoadPermitTypeId = Util.String2Int(objDR["Road_Permit_Type_ID"].ToString());
                objIGCView.RoadPermitSrNo = objDR["Road_Permit_SrNo"].ToString();
                objIGCView.Is_Insured = Convert.ToBoolean(objDR["Is_Insured"].ToString());
                objIGCView.Is_DACC = Convert.ToBoolean(objDR["Is_DACC"].ToString());
                objIGCView.Session_ContainerTypeId = Util.String2Int(objDR["Container_Type_Id"].ToString());
                objIGCView.Session_ContainerNoPart1 = objDR["ContainerNo1"].ToString();
                objIGCView.Session_ContainerNoPart2 = objDR["ContainerNo2"].ToString();
                objIGCView.Session_SealNo = objDR["SealNo"].ToString();
                objIGCView.Session_ReturnToYardId = Util.String2Int(objDR["Return_To_Yard_Id"].ToString());
                objIGCView.Session_ReturnToYardName = objDR["Return_To_Yard_Name"].ToString();
                objIGCView.Session_NFormNo = objDR["NFormNo"].ToString();

                Get_RequireForms();
            }
            else
            {
                objIGCView.Session_ConsigneeName = "";
                objIGCView.Session_ConsigneeAddressLine1 = "";
                objIGCView.Session_ConsigneeAddressLine2 = "";
                objIGCView.Session_InsuranceCompany = "";
                objIGCView.Session_PolicyNo = "";
                objIGCView.Session_PolicyAmount = 0;
                objIGCView.Session_RiskAmount = 0;
                objIGCView.Session_PolicyExpDate = DateTime.Now;
                objIGCView.Session_ContainerTypeId = 0;
                objIGCView.Session_ContainerNoPart1 = "";
                objIGCView.Session_ContainerNoPart2 = "";
                objIGCView.Session_SealNo = "";
                objIGCView.Session_ReturnToYardId = 0; ;
                objIGCView.Session_ReturnToYardName = "";
                objIGCView.Session_NFormNo = "";
                Get_BranchRateParameter();
            }

            objDS_GC.Tables[1].TableName = "multiple_commodity";
            objDS_GC.Tables[2].TableName = "invoice";

            objDS_GC.Tables[3].TableName = "other_chages";
            objDS_GC.Tables[4].TableName = "multiple_billing_details";

            objIGCView.Session_MultipleCommodityGrid = objDS_GC.Tables[1];
            objIGCView.Bind_dg_Invoice = objDS_GC.Tables[2];
            objIGCView.Session_GCOtherChargeHead  = objDS_GC.Tables[3];
            objIGCView.Session_BillingDetailsGrid = objDS_GC.Tables[4];
            
            DataSet objDs_Main_BillingDetailsGrid = new DataSet();
            objDs_Main_BillingDetailsGrid.Tables.Add(objDS_GC.Tables[4].Copy());
            objDs_Main_BillingDetailsGrid.Tables[0].TableName = "billing_details";

            objIGCView.Session_Main_BillingDetailsGrid = objDs_Main_BillingDetailsGrid.Tables[0];

            objIGCView.Session_ChequeDetailsGrid = objDS_GC.Tables[5]; 

            if (Is_Attached_Gc == true)
            {
                Get_Applicable_Service_Tax();
            }
        }
        
        public DataSet Get_ToLocationDetails()
        {
            objDS = objGCModel.Get_ToLocationDetails();
            return objDS;
        }

        public void Get_TransitDays()
        {
            objGCModel.Get_TransitDays();           
        }

        public void Get_Service_Tax_Applicable_For_Commodity()
        {
            objGCModel.Get_Service_Tax_Applicable_For_Commodity();
        }

        public void Get_Service_Tax_Details()
        {
            objGCModel.Get_ServiceTaxDetails();
        }

        public void Get_BookingSubType()
        {
            DataSet ds_BookingSubType = new DataSet();
            ds_BookingSubType = objGCModel.Get_BookingSubType();
            objIGCView.BindBookingSubType = ds_BookingSubType.Tables[0];
        }

        public void Get_LengthCharge()
        {
            objGCModel.Get_LengthCharge();            
        }

        public DataSet Get_ConsignorConsigneeDetails(Int32 ConsignorConsigneeId, Boolean Is_RegularClient, Boolean Is_Consignor)
        {
            objDS = objGCModel.Get_ConsignorConsigneeDetails(ConsignorConsigneeId, Is_RegularClient,Is_Consignor);
            return objDS;
        }

        public DataSet Get_From_Location_Details()
        {
            objDS = objGCModel.Get_From_Location_Details();
            return objDS;
        }

        public void Get_StdandardFreightRate()
        {
            objGCModel.Get_StdandardFreightRate();
        }

        public void Get_Additional_Freight()
        {
            objGCModel.Get_Additional_Freight();
        }
         
        public void save()
        {            
            base.DBSave();
            //objGCModel.Save();
        }         
    }
}
