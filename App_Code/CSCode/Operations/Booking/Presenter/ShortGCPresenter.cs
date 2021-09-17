
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
/// Summary description for ShortGCPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class ShortGCPresenter : Presenter
    {
        private IShortGCView objIShortGCView;
        private ShortGCModel objShortGCModel;
        private DataSet objDS;

        public Common CommonObj = new Common();

        public ShortGCPresenter(IShortGCView ShortGCView, bool IsPostBack)
        {
            objIShortGCView = ShortGCView;
            objShortGCModel = new ShortGCModel(objIShortGCView);

            base.Init(objIShortGCView, objShortGCModel);

            if (!IsPostBack)
            {
                objIShortGCView.BookingDate = DateTime.Now.Date;
                initValues();
            }
        }

        public void Get_Application_Start_Date()
        {
            objDS = objShortGCModel.Get_Application_Start_Date();

           if (objDS.Tables[0].Rows.Count > 0)
           {
               DataRow objDR = objDS.Tables[0].Rows[0];
               objIShortGCView.ApplicationStartDate = Convert.ToDateTime(objDR["Start_Date"].ToString());
           }
        }

        public void Get_Company_GC_Parameter()
        {
            objDS = objShortGCModel.Get_Company_GC_Parameter();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIShortGCView.Default_Booking_Type = Util.String2Int(objDR["Default_Booking_Type"].ToString());
                objIShortGCView.Default_Delivery_Type = Util.String2Int(objDR["Default_Delivery_Type"].ToString());
                objIShortGCView.Default_Freight_Basis = Util.String2Int(objDR["Default_Freight_Basis"].ToString());
                objIShortGCView.Default_Measurment_Unit = Util.String2Int(objDR["Default_Measurment_Unit"].ToString());
                objIShortGCView.Default_Payment_Type = Util.String2Int(objDR["Default_Payment_Type"].ToString());

                objIShortGCView.Default_Risk_Type = Util.String2Int(objDR["Default_Risk_Type"].ToString());
                objIShortGCView.Default_Road_Permit_Type = Util.String2Int(objDR["Default_Road_Permit_Type"].ToString());

                objIShortGCView.Default_Consignment_Type = Util.String2Int(objDR["Default_Consignment_Type"].ToString());
                objIShortGCView.Default_Pickup_Type = Util.String2Int(objDR["Default_Pickup_Type"].ToString());
                objIShortGCView.Default_Commodity_Weight = Util.String2Int(objDR["Default_Commodity_Weight"].ToString());
                 

                objIShortGCView.Is_POD_Checked = Util.String2Bool(objDR["Is_POD_Checked"].ToString());
                objIShortGCView.Is_POD_Disabled = Util.String2Bool(objDR["Is_POD_Disabled"].ToString());
                objIShortGCView.LoadingSuperVisor_RequiredFor_BookingType = objDR["Loading_SuperVisor_RequiredFor_Booking_Type"].ToString();

                objIShortGCView.Is_FOV_Calculated_As_Per_Standard = Util.String2Bool(objDR["Is_FOV_Calculated_As_Per_Standard"].ToString());
                objIShortGCView.Is_Auto_Booking_MR_For_Paid_Booking = Util.String2Bool(objDR["Is_Auto_Booking_MR_For_Paid_Booking"].ToString());

                objIShortGCView.GC_No_Length = Util.String2Int(objDR["GC_No_Length"].ToString());

                objIShortGCView.Valid_Cheque_Start_Days = Util.String2Int(objDR["Valid_Cheque_Start_Days"].ToString());
                objIShortGCView.Valid_Cheque_End_Days = Util.String2Int(objDR["Valid_Cheque_End_Days"].ToString());

                objIShortGCView.Container_Details_RequiredFor_BookingType = objDR["Container_Details_RequiredFor_Booking_Type"].ToString();

                objIShortGCView.Is_ToPay_Charge_Require = Util.String2Bool(objDR["Is_ToPay_Charge_Require"].ToString());
                objIShortGCView.Is_Consignor_Consignee_Details_Shown = Util.String2Bool(objDR["Is_Consignor_Consignee_Details_Shown"].ToString());
                objIShortGCView.Is_Validate_Freight_On_Article = Util.String2Bool(objDR["Validate_Freight_On_Article"].ToString());

                objIShortGCView.Remark_Max_Length = Util.String2Int(objDR["Remark_Max_Length"].ToString());
            }
        }

        public void Fill_Values()
        {
            objShortGCModel.Get_CompanyParameterDetails();

            objShortGCModel.Get_VAId();
            //   objShortGCModel.Get_Document_Allocation_Details();

            objDS = objShortGCModel.Fill_Values();

            objIShortGCView.Session_CommodityDdl = objDS.Tables[0] ;
            objIShortGCView.Session_ItemDdl  = objDS.Tables[1];
            objIShortGCView.Session_PackingTypeDdl  = objDS.Tables[2];

            objIShortGCView.BindUnitOfMeasurement = objDS.Tables[3];
            objIShortGCView.BindFreightBasis = objDS.Tables[4];

            objIShortGCView.BindVolumetricFreightUnit = objDS.Tables[5];

            objIShortGCView.BindBookingType  = objDS.Tables[6];
            objIShortGCView.BindDeliveryType  = objDS.Tables[7];
            objIShortGCView.BindVehicleType  = objDS.Tables[8];
            objIShortGCView.BindConsignmentType = objDS.Tables[9];

            objIShortGCView.BindPaymentType = objDS.Tables[10];
            objIShortGCView.BindGCRiskType = objDS.Tables[11];

            //objIShortGCView.BindLoadingSuperVisor  = objDS.Tables[12];
            //objIShortGCView.BindMarketingExecutive  = objDS.Tables[13];
            
            objIShortGCView.BindDeliveryAgainst  = objDS.Tables[14];
            objIShortGCView.BindPickupType  = objDS.Tables[15];

            objIShortGCView.BindRoadPermitType = objDS.Tables[16];
            objIShortGCView.BindGCInstructions  = objDS.Tables[17];

            objIShortGCView.BindBookingSubType  = objDS.Tables[18];
            objIShortGCView.Session_GCOtherChargeHead  = objDS.Tables[19];

            objIShortGCView.BindLengthChargeHead = objDS.Tables[20];

            objIShortGCView.Session_ContainerType = objDS.Tables[21];
            objIShortGCView.BindDDLGC_NO = objDS.Tables[22];
       }
 
        private void initValues()
        {
            Get_Application_Start_Date();
            Fill_Values();
            Get_BookingSubType();

            //if (objIShortGCView.keyID <= 0)
            //{
                Get_Company_GC_Parameter();
            //}            

            //if ( UserManager.getUserParam().HierarchyCode == "BO")
            //    Get_BranchRateParameter();

            //Get_Additional_Freight();
            objIShortGCView.Additional_Freight = 0;
            Get_Applicable_Service_Tax();

            
            //if (objIShortGCView.keyID > 0)
            //{
            Read_GC_Details(false);
           // }

            //if (UserManager.getUserParam().HierarchyCode != "BO")
            //    Get_BranchRateParameter();
        }
        
        public void Get_RequireForms()
        {
            objIShortGCView.Session_RequireForms = objShortGCModel.Get_RequireForms();
        }
        
        public Boolean  Is_Duplicate()
        {
            Boolean Is_Duplicate_GC_No;
            Is_Duplicate_GC_No = false;
            Is_Duplicate_GC_No = objShortGCModel.Is_Duplicate();
            return Is_Duplicate_GC_No;
        }


        public Boolean Validate_Credit_Limit()
        {
            Boolean Is_Valid_Credit_Limit;
            Is_Valid_Credit_Limit = false;
            Is_Valid_Credit_Limit = objShortGCModel.Validate_Credit_Limit();
            return Is_Valid_Credit_Limit;
        }

        public Boolean Allow_To_Attached()
        {
            Boolean Is_Allow_To_Attached;
            Is_Allow_To_Attached = false;
            Is_Allow_To_Attached = objShortGCModel.Allow_To_Attached();
            return Is_Allow_To_Attached;
        }

        public Boolean Allow_To_ReBook()
        {
            Boolean Is_Allow_To_ReBook;
            Is_Allow_To_ReBook = false;
            Is_Allow_To_ReBook = objShortGCModel.Allow_To_ReBook();
            return Is_Allow_To_ReBook;
        }

        public Boolean Allow_To_Rectify()
        {
            Boolean Is_Allow_To_Rectify;
            Is_Allow_To_Rectify = false;

            Is_Allow_To_Rectify = objShortGCModel.Allow_To_Rectify();

            return Is_Allow_To_Rectify;
        }

        public void Fill_Item(int CommodityId)
        {
            DataSet ds_Item = new DataSet();
            ds_Item = objShortGCModel.Fill_Item(CommodityId);
            objIShortGCView.Session_ItemDdl = ds_Item.Tables[0];
        }
        
        public void Get_BillingPartyDetails()
        {
            objDS = objShortGCModel.Get_BillingPartyDetails();

            if ( objDS.Tables[0].Rows.Count>0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                if (Convert.ToBoolean(objDR["Is_Service_Tax_Applicable"].ToString()) == true)
                {
                    objIShortGCView.Is_ServiceTaxApplicableForConsignor = 1;
                }
                else
                {
                    objIShortGCView.Is_ServiceTaxApplicableForConsignor = 0;
                }
                if (Util.String2Int(objDR["Billing_Branch_Id"].ToString()) > 0)
                {
                    objIShortGCView.SetBillingBranch(objDR["Billing_Branch_Name"].ToString(), objDR["Billing_Branch_Id"].ToString());
                }
                else
                {
                    objIShortGCView.SetBillingBranch("","0");
                }
                objIShortGCView.Billing_Party_Credit_Limit = Util.String2Decimal(objDR["Credit_Limit"].ToString());
                objIShortGCView.Billing_Party_Closing_Balance = Util.String2Decimal(objDR["Closing_Balance"].ToString());
                objIShortGCView.Billing_Party_Ledger_Id = Util.String2Int (objDR["Ledger_Id"].ToString());
            }
            else
            {
                objIShortGCView.Is_ServiceTaxApplicableForConsignor = 0;
                objIShortGCView.Billing_Party_Credit_Limit = 0;
                objIShortGCView.Billing_Party_Closing_Balance = 0;
                objIShortGCView.Billing_Party_Ledger_Id = 0;
                objIShortGCView.SetBillingBranch("", "0");
            }
        }
                 
        public void Fill_ContractBranches()
        {
            objIShortGCView.BindContractBranches = objShortGCModel.Fill_ContractBranches();
        }

        public DataSet  Get_Contractual_Client_Details()
        {
            DataSet ds_Contractual_Client_Details = new DataSet();
            ds_Contractual_Client_Details = objShortGCModel.Get_Contractual_Client_Details();
            return ds_Contractual_Client_Details;
        }

        public void Fill_Contract()
        {
            objIShortGCView.BindContract = objShortGCModel.Fill_Contract();
        }

        public DataSet  Get_ContractDetails(String Call_From   )
        {
            DataRow objDR;
            objDS = objShortGCModel.Get_ContractDetails();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objDR = objDS.Tables[0].Rows[0];

                if (Call_From != "From_GC_Read_Value")
                {
                    objIShortGCView.BillingBranchId = Util.String2Int(objDR["Billing_Branch_ID"].ToString());
                    objIShortGCView.BillingPartyId = Util.String2Int(objDR["Billing_Client_ID"].ToString());

                    objIShortGCView.SetBillingBranch(objDR["Billing_Branch_Name"].ToString(), objDR["Billing_Branch_ID"].ToString());
                    objIShortGCView.SetBillingParty(objDR["Billing_Client_Name"].ToString(), objDR["Billing_Client_ID"].ToString());
                    
                    objIShortGCView.ConsignmentTypeId = Util.String2Int(objDR["Consignment_Type_ID"].ToString());
                    objIShortGCView.GCRiskId = Util.String2Int(objDR["GC_risk_type_id"].ToString());
                }

                objIShortGCView.Contract_UnitOfFreightId = Util.String2Int(objDR["Freight_Unit_ID"].ToString());
                objIShortGCView.Contract_FreightBasisId = Util.String2Int(objDR["Freight_Basis_ID"].ToString());
                objIShortGCView.Contract_FreightSubUnitId = Util.String2Int(objDR["Freight_Sub_Unit_ID"].ToString());
            }                        
            return objDS;
        }

        public void Get_Applicable_Service_Tax()
        {
            //objShortGCModel.Get_Applicable_Service_Tax();

            Decimal Applicable_Service_Tax_Percent;   

            Applicable_Service_Tax_Percent = CommonObj.Get_Service_Tax_Percent(objIShortGCView.BookingDate);

            objIShortGCView.Standard_ServiceTaxPercent = Applicable_Service_Tax_Percent;
            objIShortGCView.Applicable_Standard_ServiceTaxPercent = Applicable_Service_Tax_Percent;

            objIShortGCView.ServiceTax_Label = "Service Tax " + Applicable_Service_Tax_Percent.ToString() + "%";
        }

        public void Get_BranchRateParameter()
        {
            objDS = objShortGCModel.Get_BranchRateParameter();

            if (objIShortGCView.Is_Opening_GC == false)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    objIShortGCView.LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                    objIShortGCView.LoadingCharge = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                    objIShortGCView.StationaryCharge = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                    objIShortGCView.MaxStationaryCharge = Util.String2Decimal(objDR["Max_Bilty_Charges"].ToString());
                    objIShortGCView.DDCharge = Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());


                    if (objIShortGCView.Is_ToPay_Charge_Require == true)
                        objIShortGCView.ToPayCharge = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                    else
                        objIShortGCView.ToPayCharge = 0;

                    
                    objIShortGCView.OtherCharges = Util.String2Decimal(objDR["Other_Charges"].ToString());

                    // objIShortGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());

                    objIShortGCView.hdn_StandardMinimumChargeWeight = Util.String2Decimal(objDR["Min_Charge_Wt"].ToString());

                    //objIShortGCView._CFTFactor  = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                    objIShortGCView.Standard_CFTFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());

                    //objIShortGCView.hdn_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_kg"].ToString());
                    objIShortGCView.hdn_StandardHamaliPerKg = Util.String2Decimal(objDR["Min_Hamali"].ToString());

                    //objIShortGCView.hdn_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());

                    objIShortGCView.hdn_StandardMinimumFOV = Util.String2Decimal(objDR["Min_FOV"].ToString());

                    //objIShortGCView.Standard_Freight_Rate = Util.String2Decimal(objDR["Std_Freight_Rate"].ToString());
                    objIShortGCView.Standard_ServiceTaxPercent = Util.String2Decimal(objDR["Service_Tax_Percent"].ToString());

                    objIShortGCView.Standard_BiltiCharges = Util.String2Decimal(objDR["Bilty_Charges"].ToString());

                    objIShortGCView.Standard_DDCharge = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIShortGCView.Standard_DDCharge_Rate = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());

                    objIShortGCView.Standard_Invoice_Rate = Util.String2Decimal(objDR["Invoice_Rate"].ToString());
                    objIShortGCView.Standard_Invoice_Per_How_Many_Rs = Util.String2Decimal(objDR["Invoice_Per_How_Many_Rs"].ToString());
                    objIShortGCView.Standard_FOVRate = Util.String2Decimal(objDR["FOV_Rate"].ToString());
                    
                    objIShortGCView.Standard_FOV = Util.String2Decimal(objDR["Min_FOV"].ToString());
                    objIShortGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                    objIShortGCView.Standard_HamaliCharge = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                    objIShortGCView.Standard_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_kg"].ToString());

                    objIShortGCView.Standard_HamaliPerArticles = Util.String2Decimal(objDR["Hamali_Per_Article"].ToString());

                    objIShortGCView.Standard_LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                    objIShortGCView.Standard_LocalCharge_Rate = Util.String2Decimal(objDR["Local_Charges"].ToString());

                    if (objIShortGCView.Is_ToPay_Charge_Require == true)
                        objIShortGCView.Standard_ToPayCharges = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                    else
                        objIShortGCView.Standard_ToPayCharges = 0;
                                                           
                    objIShortGCView.Standard_DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                    objIShortGCView.Standard_Octroi_Form_Charge = Util.String2Decimal(objDR["Octroi_Form_Charges"].ToString());
                    objIShortGCView.Standard_Octroi_Service_Charge = Util.String2Decimal(objDR["Octroi_Service_Charges"].ToString());
                    objIShortGCView.Standard_GI_Charges = Util.String2Decimal(objDR["GI_Charges"].ToString());
                    objIShortGCView.Standard_Demurrage_Days = Util.String2Decimal(objDR["Demurrage_Days"].ToString());
                    objIShortGCView.Standard_Demurrage_Rate = Util.String2Decimal(objDR["Demurrage_Rate_Kg_Per_Day"].ToString());

                    objIShortGCView.NFormCharge = Util.String2Decimal(objDR["NForm_Charge"].ToString());

                    objIShortGCView.Standard_NForm_Charge = Util.String2Decimal(objDR["NForm_Charge"].ToString());

                    objIShortGCView.Freight_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Freight_Chg_Discount_Percent"].ToString()); ;
                    objIShortGCView.Hamali_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Hamali_Chg_Discount_Percent"].ToString());
                    objIShortGCView.Fov_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_Fov_Chg_Discount_Percent"].ToString());
                    objIShortGCView.ToPay_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_TP_Chg_Discount_Percent"].ToString());
                    objIShortGCView.DD_Charge_Discount_Percent = Util.String2Decimal(objDR["Bkg_DD_Chg_Discount_Percent"].ToString());

                    objIShortGCView.ReBookGC_Amount = 0;
                    objIShortGCView.UnloadingCharge = 0;
                   

                    objIShortGCView.Applicable_Standard_BiltiCharges = objIShortGCView.Standard_BiltiCharges;

                    objIShortGCView.Applicable_Standard_Invoice_Rate = objIShortGCView.Standard_Invoice_Rate;
                    objIShortGCView.Applicable_Standard_Invoice_Per_How_Many_Rs  = objIShortGCView.Standard_Invoice_Per_How_Many_Rs;
                    objIShortGCView.Applicable_Standard_FOVRate = objIShortGCView.Standard_FOVRate;
                    
                    objIShortGCView.Applicable_Standard_FOV = objIShortGCView.Standard_FOV;
                    objIShortGCView.Applicable_Standard_FOVPercentage = objIShortGCView.Standard_FOVPercentage;
                    objIShortGCView.Applicable_Standard_FreightRate = objIShortGCView.Standard_FreightRate;
                    objIShortGCView.Applicable_Standard_HamaliCharge = objIShortGCView.Standard_HamaliCharge;
                    objIShortGCView.Applicable_Standard_HamaliPerKg = objIShortGCView.Standard_HamaliPerKg;

                    objIShortGCView.Applicable_Standard_HamaliPerArticles = objIShortGCView.Standard_HamaliPerArticles;

                    objIShortGCView.Applicable_Standard_LocalCharge = objIShortGCView.Standard_LocalCharge;
                    objIShortGCView.Applicable_Standard_LocalCharge_Rate = objIShortGCView.Standard_LocalCharge_Rate ;

                    objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                    objIShortGCView.Applicable_Standard_MinimumChargeWeight = objIShortGCView.Standard_MinimumChargeWeight;
                    objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                    //objIShortGCView.Applicable_Standard_Minimum_Hamali_Per_Kg = objIShortGCView.st
                    objIShortGCView.Applicable_Standard_ServiceTaxAmount = objIShortGCView.Standard_ServiceTaxAmount;
                    objIShortGCView.Applicable_Standard_ServiceTaxPercent = objIShortGCView.Standard_ServiceTaxPercent;
                    objIShortGCView.Applicable_Standard_ToPayCharges = objIShortGCView.Standard_ToPayCharges;
                    objIShortGCView.Applicable_Standard_DDCharge = objIShortGCView.Standard_DDCharge;
                    objIShortGCView.Applicable_Standard_DDCharge_Rate = objIShortGCView.Standard_DDCharge_Rate;
                    objIShortGCView.Applicable_Standard_DACCCharges = objIShortGCView.Standard_DACCCharges;
                    objIShortGCView.Applicable_Standard_CFTFactor = objIShortGCView.Standard_CFTFactor;
                    objIShortGCView.Applicable_Standard_Octroi_Form_Charge = objIShortGCView.Standard_Octroi_Form_Charge;
                    objIShortGCView.Applicable_Standard_Octroi_Service_Charge = objIShortGCView.Standard_Octroi_Service_Charge;
                    objIShortGCView.Applicable_Standard_GI_Charges = objIShortGCView.Standard_GI_Charges;
                    objIShortGCView.Applicable_Standard_Demurrage_Days = objIShortGCView.Standard_Demurrage_Days;
                    objIShortGCView.Applicable_Standard_Demurrage_Rate = objIShortGCView.Standard_Demurrage_Rate;

                    objIShortGCView.Applicable_Standard_NForm_Charge = objIShortGCView.Standard_NForm_Charge;

                    objIShortGCView.Default_Bank_Ledger_Id = Util.String2Int(objDR["Default_Bank_Ledger_Id"].ToString());
                    objIShortGCView.Default_Cash_Ledger_Id = Util.String2Int(objDR["Default_Cash_Ledger_Id"].ToString());


                    objIShortGCView.Default_Cheque_Bank_Ledger_Name  = objDR["Default_Bank_Ledger_Name"].ToString();
                    objIShortGCView.Default_Cheque_Branch_Ledger_Name  = objDR["Default_Branch_Ledger_Name"].ToString();
                    objIShortGCView.Default_Cash_Ledger_Name = objDR["Default_Cash_Ledger_Name"].ToString();
                }
                else
                {
                    objIShortGCView.LocalCharge = 0;
                    objIShortGCView.LoadingCharge = 0;
                    objIShortGCView.StationaryCharge = 0;
                    objIShortGCView.MaxStationaryCharge = 0;
                    objIShortGCView.DDCharge = 0;
                    objIShortGCView.ToPayCharge = 0;
                    objIShortGCView.OtherCharges = 0;
                    objIShortGCView.UnloadingCharge = 0;
                    objIShortGCView.NFormCharge = 0;
                    // objIShortGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());

                    objIShortGCView.hdn_StandardMinimumChargeWeight = 0;

                    //objIShortGCView._CFTFactor  = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                    objIShortGCView.Standard_CFTFactor = 0;

                    //objIShortGCView.hdn_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_kg"].ToString());
                    objIShortGCView.hdn_StandardHamaliPerKg = 0;

                    //objIShortGCView.hdn_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());

                    objIShortGCView.hdn_StandardMinimumFOV = 0;

                    //objIShortGCView.Standard_Freight_Rate = Util.String2Decimal(objDR["Std_Freight_Rate"].ToString());

                    objIShortGCView.Standard_ServiceTaxPercent = Util.String2Decimal("12.36");

                    objIShortGCView.Standard_BiltiCharges = 0;
                    objIShortGCView.Standard_DDCharge = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIShortGCView.Standard_DDCharge_Rate = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIShortGCView.Standard_FOV = 0;
                    objIShortGCView.Standard_FOVPercentage = 0;
                    objIShortGCView.Standard_HamaliCharge = 0;
                    objIShortGCView.Standard_HamaliPerKg = 0;
                    objIShortGCView.Standard_HamaliPerArticles = 0;
                    objIShortGCView.Standard_LocalCharge = 0;
                    objIShortGCView.Standard_LocalCharge_Rate = 0;
                    objIShortGCView.Standard_ToPayCharges = 0;
                    objIShortGCView.Freight_Charge_Discount_Percent = 0;
                    objIShortGCView.Hamali_Charge_Discount_Percent = 0;
                    objIShortGCView.Fov_Charge_Discount_Percent = 0;
                    objIShortGCView.ToPay_Charge_Discount_Percent = 0;
                    objIShortGCView.DD_Charge_Discount_Percent = 0;
                    objIShortGCView.Standard_Octroi_Form_Charge = 0;
                    objIShortGCView.Standard_Octroi_Service_Charge = 0;
                    objIShortGCView.Standard_GI_Charges = 0;
                    objIShortGCView.Standard_Demurrage_Days = 0;
                    objIShortGCView.Standard_Demurrage_Rate = 0;
                    objIShortGCView.Standard_NForm_Charge = 0;

                    objIShortGCView.Applicable_Standard_BiltiCharges = objIShortGCView.Standard_BiltiCharges;
                    objIShortGCView.Applicable_Standard_FOV = objIShortGCView.Standard_FOV;
                    objIShortGCView.Applicable_Standard_FOVPercentage = objIShortGCView.Standard_FOVPercentage;
                    objIShortGCView.Applicable_Standard_FreightRate = objIShortGCView.Standard_FreightRate;
                    objIShortGCView.Applicable_Standard_HamaliCharge = objIShortGCView.Standard_HamaliCharge;
                    objIShortGCView.Applicable_Standard_HamaliPerKg = objIShortGCView.Standard_HamaliPerKg;
                    objIShortGCView.Applicable_Standard_HamaliPerArticles = objIShortGCView.Standard_HamaliPerArticles;

                    objIShortGCView.Applicable_Standard_LocalCharge = objIShortGCView.Standard_LocalCharge;
                    objIShortGCView.Applicable_Standard_LocalCharge_Rate  = objIShortGCView.Standard_LocalCharge_Rate ;
                    
                    objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                    objIShortGCView.Applicable_Standard_MinimumChargeWeight = objIShortGCView.Standard_MinimumChargeWeight;
                    objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                    //objIShortGCView.Applicable_Standard_Minimum_Hamali_Per_Kg = objIShortGCView.st
                    objIShortGCView.Applicable_Standard_ServiceTaxAmount = objIShortGCView.Standard_ServiceTaxAmount;
                    objIShortGCView.Applicable_Standard_ServiceTaxPercent = objIShortGCView.Standard_ServiceTaxPercent;
                    objIShortGCView.Applicable_Standard_ToPayCharges = objIShortGCView.Standard_ToPayCharges;
                    objIShortGCView.Applicable_Standard_DDCharge = objIShortGCView.Standard_DDCharge;
                    objIShortGCView.Applicable_Standard_DDCharge_Rate = objIShortGCView.Standard_DDCharge_Rate;
                    objIShortGCView.Applicable_Standard_CFTFactor = objIShortGCView.Standard_CFTFactor;
                    objIShortGCView.Applicable_Standard_Octroi_Form_Charge = objIShortGCView.Standard_Octroi_Form_Charge;
                    objIShortGCView.Applicable_Standard_Octroi_Service_Charge = objIShortGCView.Standard_Octroi_Service_Charge;
                    objIShortGCView.Applicable_Standard_GI_Charges = objIShortGCView.Standard_GI_Charges;
                    objIShortGCView.Applicable_Standard_Demurrage_Days = objIShortGCView.Standard_Demurrage_Days;
                    objIShortGCView.Applicable_Standard_Demurrage_Rate = objIShortGCView.Standard_Demurrage_Rate;

                    objIShortGCView.Applicable_Standard_NForm_Charge = objIShortGCView.Standard_NForm_Charge;

                    objIShortGCView.Default_Bank_Ledger_Id = 0;
                    objIShortGCView.Default_Cash_Ledger_Id = 0;

                    objIShortGCView.Default_Cheque_Bank_Ledger_Name  = "";
                    objIShortGCView.Default_Cheque_Branch_Ledger_Name  = "";
                    objIShortGCView.Default_Cash_Ledger_Name = "";

                    Common.DisplayErrors(1016); // Branch Rate Parameter Not Define.
                }
            }
            else
            {
                objIShortGCView.LocalCharge = 0;
                objIShortGCView.LoadingCharge = 0;
                objIShortGCView.StationaryCharge = 0;
                objIShortGCView.MaxStationaryCharge = 0;
                objIShortGCView.DDCharge = 0;
                objIShortGCView.ToPayCharge = 0;
                objIShortGCView.OtherCharges = 0;
                objIShortGCView.UnloadingCharge = 0;
                objIShortGCView.NFormCharge = 0;
                // objIShortGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());

                objIShortGCView.hdn_StandardMinimumChargeWeight = 0;

                //objIShortGCView._CFTFactor  = Util.String2Decimal(objDR["CFT_Factor"].ToString());
                objIShortGCView.Standard_CFTFactor = 0;

                //objIShortGCView.hdn_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_kg"].ToString());
                objIShortGCView.hdn_StandardHamaliPerKg = 0;

                //objIShortGCView.hdn_FOVPercentage = Util.String2Decimal(objDR["FOV_Percent"].ToString());

                objIShortGCView.hdn_StandardMinimumFOV = 0;

                //objIShortGCView.Standard_Freight_Rate = Util.String2Decimal(objDR["Std_Freight_Rate"].ToString());

                objIShortGCView.Standard_ServiceTaxPercent = Util.String2Decimal("12.36");

                objIShortGCView.Standard_BiltiCharges = 0;
                objIShortGCView.Standard_DDCharge = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                objIShortGCView.Standard_DDCharge_Rate = 0;//` Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                objIShortGCView.Standard_FOV = 0;
                objIShortGCView.Standard_FOVPercentage = 0;
                objIShortGCView.Standard_HamaliCharge = 0;
                objIShortGCView.Standard_HamaliPerKg = 0;
                objIShortGCView.Standard_HamaliPerArticles = 0;
                objIShortGCView.Standard_LocalCharge = 0;
                objIShortGCView.Standard_ToPayCharges = 0;
                objIShortGCView.Freight_Charge_Discount_Percent = 0;
                objIShortGCView.Hamali_Charge_Discount_Percent = 0;
                objIShortGCView.Fov_Charge_Discount_Percent = 0;
                objIShortGCView.ToPay_Charge_Discount_Percent = 0;
                objIShortGCView.DD_Charge_Discount_Percent = 0;
                objIShortGCView.Standard_Octroi_Form_Charge = 0;
                objIShortGCView.Standard_Octroi_Service_Charge = 0;
                objIShortGCView.Standard_GI_Charges = 0;
                objIShortGCView.Standard_Demurrage_Days = 0;
                objIShortGCView.Standard_Demurrage_Rate = 0;
                objIShortGCView.Standard_NForm_Charge = 0;

                objIShortGCView.Applicable_Standard_BiltiCharges = objIShortGCView.Standard_BiltiCharges;
                objIShortGCView.Applicable_Standard_FOV = objIShortGCView.Standard_FOV;
                objIShortGCView.Applicable_Standard_FOVPercentage = objIShortGCView.Standard_FOVPercentage;
                objIShortGCView.Applicable_Standard_FreightRate = objIShortGCView.Standard_FreightRate;
                objIShortGCView.Applicable_Standard_HamaliCharge = objIShortGCView.Standard_HamaliCharge;
                objIShortGCView.Applicable_Standard_HamaliPerKg = objIShortGCView.Standard_HamaliPerKg;
                objIShortGCView.Applicable_Standard_HamaliPerArticles = objIShortGCView.Standard_HamaliPerArticles;

                objIShortGCView.Applicable_Standard_LocalCharge = objIShortGCView.Standard_LocalCharge;
                objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                objIShortGCView.Applicable_Standard_MinimumChargeWeight = objIShortGCView.Standard_MinimumChargeWeight;
                objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                //objIShortGCView.Applicable_Standard_Minimum_Hamali_Per_Kg = objIShortGCView.st
                objIShortGCView.Applicable_Standard_ServiceTaxAmount = objIShortGCView.Standard_ServiceTaxAmount;
                objIShortGCView.Applicable_Standard_ServiceTaxPercent = objIShortGCView.Standard_ServiceTaxPercent;
                objIShortGCView.Applicable_Standard_ToPayCharges = objIShortGCView.Standard_ToPayCharges;
                objIShortGCView.Applicable_Standard_DDCharge = objIShortGCView.Standard_DDCharge;
                objIShortGCView.Applicable_Standard_DDCharge_Rate = objIShortGCView.Standard_DDCharge_Rate;
                objIShortGCView.Applicable_Standard_CFTFactor = objIShortGCView.Standard_CFTFactor;
                objIShortGCView.Applicable_Standard_Octroi_Form_Charge = objIShortGCView.Standard_Octroi_Form_Charge;
                objIShortGCView.Applicable_Standard_Octroi_Service_Charge = objIShortGCView.Standard_Octroi_Service_Charge;
                objIShortGCView.Applicable_Standard_GI_Charges = objIShortGCView.Standard_GI_Charges;
                objIShortGCView.Applicable_Standard_Demurrage_Days = objIShortGCView.Standard_Demurrage_Days;
                objIShortGCView.Applicable_Standard_Demurrage_Rate = objIShortGCView.Standard_Demurrage_Rate;

                objIShortGCView.Applicable_Standard_NForm_Charge = objIShortGCView.Standard_NForm_Charge;

                objIShortGCView.Default_Bank_Ledger_Id = 0;
                objIShortGCView.Default_Cash_Ledger_Id = 0;

                objIShortGCView.Default_Cheque_Bank_Ledger_Name  = "";
                objIShortGCView.Default_Cheque_Branch_Ledger_Name  = "";
                objIShortGCView.Default_Cash_Ledger_Name = "";
            }

            Get_Applicable_Service_Tax();
            objIShortGCView.ServiceTax_Label = "Service Tax " + objIShortGCView.Applicable_Standard_ServiceTaxPercent.ToString() + "%";
        }

        public void Read_GC_Details(Boolean Is_Attached_Gc)
        {         
            DataSet ds_Multiple_Commodity_Details = new DataSet ();
            DataSet ds_Invoice_Details = new DataSet ();

            DataSet objDS_GC = new DataSet();
            DataSet objDS_ContractDetails= new DataSet();

            objDS_GC = objShortGCModel.Read_GC_Details(Is_Attached_Gc);

            if (objDS_GC.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS_GC.Tables[0].Rows[0];

                objIShortGCView.Previous_Article_ID = Util.String2Int(objDR["Previous_Article_ID_1"].ToString());
                objIShortGCView.Previous_Status_ID = Util.String2Int(objDR["Previous_Status_ID_1"].ToString());
                objIShortGCView.Previous_Document_ID = Util.String2Int(objDR["Previous_Document_ID_1"].ToString());
                objIShortGCView.Previous_Document_No_For_Print = objDR["Previous_Document_No_For_Print_1"].ToString();
                objIShortGCView.Previous_Document_Date = Convert.ToDateTime(objDR["Previous_Document_Date_1"].ToString());

                objIShortGCView.CustomerRefNo = objDR["Customer_Ref_No"].ToString();

                if (objIShortGCView.keyID > 0 && Is_Attached_Gc == false)
                {
                    objIShortGCView.Is_Attached = Util.String2Bool(objDR["Is_Attached"].ToString());
                    objIShortGCView.Attached_GC_Id = Util.String2Int(objDR["Attached_GC_Id"].ToString());
                    objIShortGCView.Attached_GC_No_For_Print = objDR["Attached_GC_No_For_Print"].ToString();
                    objIShortGCView.PrivateMark = objDR["Private_Mark"].ToString();

                    objIShortGCView.Is_ReBooked = Util.String2Bool(objDR["Is_ReBooked"].ToString());

                    if (objIShortGCView.Is_ReBooked == true)
                    {
                        objIShortGCView.ReBook_GC_Id = Util.String2Int(objDR["ReBook_Against_GC_Id"].ToString());
                        objIShortGCView.Attached_GC_No_For_Print = objDR["ReBook_GC_No_For_Print"].ToString();

                        objIShortGCView.PrivateMark = objDR["Private_Mark"].ToString();

                        objIShortGCView.ReBookGC_SubTotal = Util.String2Decimal(objDR["ReBook_Charges"].ToString());
                        objIShortGCView.ReBookGC_Amount = Util.String2Decimal(objDR["ReBook_Charges"].ToString());
                    }
                    else
                    {
                        objIShortGCView.ReBook_GC_Id = 0;
                        objIShortGCView.ReBookGC_SubTotal = 0;
                        objIShortGCView.ReBookGC_Amount = 0;
                    }
                }
                else
                {
                    if (Is_Attached_Gc == false)
                    {
                        objIShortGCView.Is_Attached = false;
                    }
                    //else
                    //{
                    //    objIShortGCView.Is_Attached = true;
                    //}                      
                }

                if (!Is_Attached_Gc)
                {
                    objIShortGCView.GC_No = objDR["GC_No_For_Print"].ToString();
                    objIShortGCView.PrivateMark = objDR["Private_Mark"].ToString();
                    objIShortGCView.BookingDate = Convert.ToDateTime(objDR["GC_Date"]);
                    objIShortGCView.BookingTime = objDR["GC_Time"].ToString();
                }

                if (Is_Attached_Gc) // && objIShortGCView.keyID <= 0)
                {
                    //  objIShortGCView.Is_Attached = Is_Attached_Gc;

                    objIShortGCView.Attached_GC_Id = Util.String2Int(objDR["GC_Id"].ToString());
                    objIShortGCView.ReBook_GC_Id = Util.String2Int(objDR["GC_Id"].ToString());

                    objIShortGCView.Attached_GC_No_For_Print = objDR["GC_No_For_Print"].ToString();
                    objIShortGCView.PrivateMark = objDR["Private_Mark"].ToString();

                    if (Util.String2Int(objDR["Payment_Type_Id"].ToString()) == 1)
                    {
                        objIShortGCView.Is_ReBookGC_ToPay = true;
                        objIShortGCView.ReBookGC_SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                        objIShortGCView.ReBookGC_Amount = Util.String2Decimal(objDR["Sub_Total"].ToString());

                        objIShortGCView.ReBookGC_OctroiAmount = Util.String2Decimal(objDR["Oct_Amount"].ToString());
                    }
                    else
                    {
                        objIShortGCView.Is_ReBookGC_ToPay = false;
                        objIShortGCView.ReBookGC_SubTotal = 0;
                        objIShortGCView.ReBookGC_Amount = 0;
                    }
                    objIShortGCView.ReBookGC_OctroiAmount = Util.String2Decimal(objDR["Oct_Amount"].ToString());
                    objIShortGCView.ReBook_GCOctroiPaidByID = Util.String2Int(objDR["Octroi_Paid_By_ID"].ToString());

                    objIShortGCView.GC_Status_Id_At_Current_Branch = Util.String2Int(objDR["GC_Status_Id_At_Current_Branch"].ToString());
                    objIShortGCView.GC_Articles_At_Current_Branch = Util.String2Int(objDR["GC_Articles_At_Current_Branch"].ToString());
                }

                //if (objIShortGCView.Is_AttachedGC)
                //{
                //    objIShortGCView.Attached_GC_Id = Util.String2Int( objDR["Attached_GC_Id1"].ToString());
                //    objIShortGCView.Attached_GC_No_For_Print = objDR["Attached_GC_No_For_Print"].ToString();                       
                //}

                objIShortGCView.CustomerRefNo = objDR["Customer_Ref_No"].ToString();

                //objIShortGCView.Bookindate = objDR [""].ToString();                              

                objIShortGCView.ConsignmentTypeId = Util.String2Int(objDR["Consignment_Type_Id"].ToString());
                objIShortGCView.BookingTypeId = Util.String2Int(objDR["Booking_Type_Id"].ToString());

                Get_BookingSubType();

                objIShortGCView.DeliveryTypeId = Util.String2Int(objDR["Delivery_Type_Id"].ToString());
                objIShortGCView.DeliveryAgainstId = Util.String2Int(objDR["Door_Delivery_Against_ID"].ToString());

                objIShortGCView.SetFromLocation(objDR["From_Location_Name"].ToString(), objDR["From_Location_ID"].ToString());
                objIShortGCView.SetToLocation(objDR["To_Location_Name"].ToString(), objDR["To_Location_ID"].ToString());

                objIShortGCView.SetConsingee(objDR["Consignee_Name"].ToString(), objDR["Consignee_Client_ID"].ToString());
                objIShortGCView.SetConsingor(objDR["Consignor_Name"].ToString(), objDR["Consignor_Client_ID"].ToString());

                objIShortGCView.BookingBranchId = Util.String2Int(objDR["Booking_Branch_Id"].ToString());
                objIShortGCView.ArrivedFromBranchId = Util.String2Int(objDR["Arrived_From_Branch_Id"].ToString());
                objIShortGCView.ArrivedDate = Convert.ToDateTime(objDR["Arrived_Date"]);
                Get_BranchRateParameter();

                objIShortGCView.SetBookingBranch(objDR["Booking_Branch_Name"].ToString(), objDR["Booking_Branch_Id"].ToString());
                objIShortGCView.SetArrivedFromBranch(objDR["Arrived_From_Branch_Name"].ToString(), objDR["Arrived_From_Branch_Id"].ToString());

                objIShortGCView.FromLocationId = Util.String2Int(objDR["From_Location_ID"].ToString());
                objIShortGCView.ToLocationId = Util.String2Int(objDR["To_Location_ID"].ToString());
                objIShortGCView.DeliveryBaranchId = Util.String2Int(objDR["Delivery_Branch_Id"].ToString());
                objIShortGCView.DeliveryBranchName = objDR["Delivery_Branch_Name"].ToString();
                objIShortGCView.VehicleTypeId = Util.String2Int(objDR["Vehicle_Type_Id"].ToString());
                objIShortGCView.VehicleNo = objDR["Vehicle_No"].ToString();
                objIShortGCView.PickupTypeId = Util.String2Int(objDR["Pickup_Type_Id"].ToString());
                objIShortGCView.STMNo = objDR["STM_No"].ToString();
                objIShortGCView.FeasibilityRouteSurveyNo = objDR["Feasibility_Route_Survey_No"].ToString();

                //----------------------------------------------------------------------------------------

                // objIShortGCView.ConsignorName  = objDR ["Consignor_Name"].ToString();

                objIShortGCView.ConsignorId = Util.String2Int(objDR["Consignor_Client_ID"].ToString());
                objIShortGCView.EncreptedConsignorId = Util.EncryptInteger(objIShortGCView.ConsignorId);

                objIShortGCView.ConsignorAddressLine1 = objDR["Consignor_Add1"].ToString();
                objIShortGCView.ConsignorAddressLine2 = objDR["Consignor_Add2"].ToString();

                objIShortGCView.ConsignorCountryId = Util.String2Int(objDR["Consignor_Country_ID"].ToString());
                objIShortGCView.ConsignorCountryName = objDR["Consignor_Country"].ToString();

                objIShortGCView.ConsignorStateId = Util.String2Int(objDR["Consignor_State_ID"].ToString());
                objIShortGCView.ConsignorStateName = objDR["Consignor_State"].ToString();

                objIShortGCView.ConsignorCityId = Util.String2Int(objDR["Consignor_City_ID"].ToString());
                objIShortGCView.ConsignorCity = objDR["Consignor_City"].ToString();

                objIShortGCView.ConsignorPinCode = objDR["Consignor_Pin_Code"].ToString();
                objIShortGCView.ConsignorTelNo = objDR["Consignor_Tel_No"].ToString();
                objIShortGCView.ConsignorMobileNo = objDR["Consignor_Mobile_No"].ToString();
                objIShortGCView.ConsignorEmail = objDR["Consignor_EMail"].ToString();
                objIShortGCView.ConsignorCSTNo = objDR["Consignor_CST_TIN_No"].ToString();

                objIShortGCView.ConsignorDetails = objIShortGCView.ConsignorAddressLine1 + ", " + objIShortGCView.ConsignorAddressLine2 + ", " +
                                                   objIShortGCView.ConsignorCity + ", " + objIShortGCView.ConsignorPinCode + ", " +
                                                   objIShortGCView.ConsignorStateName + ", " + " Ph : " + objIShortGCView.ConsignorTelNo;

                //objIShortGCView.Is_RegularConsignor = Convert.ToBoolean(objDR["Is_Consignor_Regular_Client"].ToString());

                if (Convert.ToBoolean(objDR["Is_Consignor_Regular_Client"].ToString()))
                {
                    objIShortGCView.Is_RegularConsignor = 1;
                }
                else
                {
                    objIShortGCView.Is_RegularConsignor = 0;
                }

                //----------------------------------------------------------------------------------------

                //                objIShortGCView.ConsigneeName = objDR ["Consignee_Name"].ToString();

                objIShortGCView.ConsigneeId = Util.String2Int(objDR["Consignee_Client_ID"].ToString());
                objIShortGCView.EncreptedConsigneeId = Util.EncryptInteger(objIShortGCView.ConsigneeId);

                objIShortGCView.Session_ConsigneeName = objDR["Consignee_Name"].ToString();

                objIShortGCView.ConsigneeAddressLine1 = objDR["Consignee_Add1"].ToString();
                objIShortGCView.ConsigneeAddressLine2 = objDR["Consignee_Add2"].ToString();

                objIShortGCView.Session_ConsigneeAddressLine1 = objDR["DD_Address_1"].ToString();
                objIShortGCView.Session_ConsigneeAddressLine2 = objDR["DD_Address_2"].ToString();

                objIShortGCView.ConsigneeDDAddressLine1 = objDR["DD_Address_1"].ToString();
                objIShortGCView.ConsigneeDDAddressLine2 = objDR["DD_Address_2"].ToString();

                objIShortGCView.ConsigneeCountryId = Util.String2Int(objDR["Consignee_Country_ID"].ToString());
                objIShortGCView.ConsigneeCountryName = objDR["Consignee_Country"].ToString();

                objIShortGCView.ConsigneeStateId = Util.String2Int(objDR["Consignee_State_ID"].ToString());
                objIShortGCView.ConsigneeStateName = objDR["Consignee_State"].ToString();

                objIShortGCView.ConsigneeCityId = Util.String2Int(objDR["Consignee_City_ID"].ToString());
                objIShortGCView.ConsigneeCity = objDR["Consignee_City"].ToString();

                objIShortGCView.ConsigneePinCode = objDR["Consignee_Pin_Code"].ToString();
                objIShortGCView.ConsigneeTelNo = objDR["Consignee_Tel_No"].ToString();
                objIShortGCView.ConsigneeMobileNo = objDR["Consignee_Mobile_No"].ToString();
                objIShortGCView.ConsigneeEmail = objDR["Consignee_EMail"].ToString();
                objIShortGCView.ConsigneeCSTNo = objDR["Consignee_CST_TIN_No"].ToString();

                objIShortGCView.ConsigneeDetails = objIShortGCView.ConsigneeAddressLine1 + ", " + objIShortGCView.ConsigneeAddressLine2 + ", " +
                                                   objIShortGCView.ConsigneeCity + ", " + objIShortGCView.ConsigneePinCode + ", " +
                                                   objIShortGCView.ConsigneeStateName + ", " + " Ph : " + objIShortGCView.ConsigneeTelNo;

                //objIShortGCView.Is_RegularConsignee = Convert.ToBoolean(objDR["Is_Consignee_Regular_Client"].ToString());

                if (Convert.ToBoolean(objDR["Is_Consignee_Regular_Client"].ToString()))
                {
                    objIShortGCView.Is_RegularConsignee = 1;
                }
                else
                {
                    objIShortGCView.Is_RegularConsignee = 0;
                }
                //----------------------------------------------------------------------------------------

                //--------------------------------- Contract Details ----------------------------------

                objIShortGCView.SetContractualClient(objDR["Contractual_Client_Name"].ToString(), objDR["Contractual_Client_Id"].ToString());

                objIShortGCView.Contractual_ClientId = Util.String2Int(objDR["Contractual_Client_Id"].ToString());

                Fill_ContractBranches();

                try
                {
                    objIShortGCView.Contract_BranchId = Util.String2Int(objDR["Contract_Branch_ID"].ToString());
                    Fill_Contract();
                    objIShortGCView.ContractId = Util.String2Int(objDR["Contract_ID"].ToString());
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    //MultipleCommodityGridErrorMessage = "Duplicate Commodity , Item , Packing Type Name";
                    objIShortGCView.Contract_BranchId = 0;
                    objIShortGCView.ContractId = 0;
                }

                if (Util.String2Int(objDR["Contract_ID"].ToString()) > 0 && objIShortGCView.ContractId > 0)
                {
                    objIShortGCView.Is_ContractApplied = 1;
                }
                else
                {
                    objIShortGCView.Is_ContractApplied = 0;
                }

                objDS_ContractDetails = Get_ContractDetails("From_GC_Read_Value");

                objIShortGCView.Session_DS_ContractDetails = objDS_ContractDetails;

                //----------------------------------------------------------------------------------------

                //----------------------------------------------------------------------------------------

                objIShortGCView.PaymentTypeId = Util.String2Int(objDR["Payment_Type_Id"].ToString());
                objIShortGCView.GCRiskId = Util.String2Int(objDR["Risk_Type_ID"].ToString());
                objIShortGCView.InsuranceCompany = objDR["Insurance_Company"].ToString();
                objIShortGCView.PolicyNo = objDR["Policy_No"].ToString();
                objIShortGCView.PolicyAmount = Util.String2Decimal(objDR["Policy_Amount"].ToString());
                objIShortGCView.RiskAmount = Util.String2Decimal(objDR["Risk_Amount"].ToString());
                objIShortGCView.PolicyExpDate = Convert.ToDateTime(objDR["Policy_Exp_Date"].ToString());

                objIShortGCView.Session_InsuranceCompany = objIShortGCView.InsuranceCompany;
                objIShortGCView.Session_PolicyNo = objIShortGCView.PolicyNo;
                objIShortGCView.Session_PolicyAmount = objIShortGCView.PolicyAmount;
                objIShortGCView.Session_RiskAmount = objIShortGCView.RiskAmount;
                objIShortGCView.Session_PolicyExpDate = objIShortGCView.PolicyExpDate;

                //----------------------------------------------------------------------------------------

                objIShortGCView.TotalArticles = Util.String2Int(objDR["Total_Articles"].ToString());
                objIShortGCView.TotalLength = Util.String2Decimal(objDR["Total_Length"].ToString());
                objIShortGCView.TotalWidth = Util.String2Decimal(objDR["Total_Width"].ToString());
                objIShortGCView.TotalWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIShortGCView.TotalHeight = Util.String2Decimal(objDR["Total_Height"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.UnitOfMeasurementId = Util.String2Int(objDR["Unit_Of_Measurement_ID"].ToString());

                objIShortGCView.UnitOfMeasurmentLength = Util.String2Decimal(objDR["Total_Length"].ToString());
                objIShortGCView.UnitOfMeasurmentWidth = Util.String2Decimal(objDR["Total_Width"].ToString());
                objIShortGCView.UnitOfMeasurmentHeight = Util.String2Decimal(objDR["Total_Height"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.HeightInFeet = Util.String2Decimal(objDR["Total_Height_In_Ft"].ToString());
                objIShortGCView.WidthInFeet = Util.String2Decimal(objDR["Total_Width_In_Ft"].ToString());
                objIShortGCView.LengthInFeet = Util.String2Decimal(objDR["Total_Length_In_Ft"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.FreightBasisId = Util.String2Int(objDR["Freight_Basis_ID"].ToString());
                objIShortGCView.VolumetricFreightUnitId = Util.String2Int(objDR["Volumetric_Freight_Unit_ID"].ToString());

                objIShortGCView.TotalCFT = Util.String2Decimal(objDR["Total_CFT"].ToString());
                objIShortGCView.TotalCBM = Util.String2Decimal(objDR["Total_CBM"].ToString());

                // CFT Factor
                objIShortGCView.VolumetricToKgFactor = Util.String2Decimal(objDR["CFT_Factor"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.ActualWeight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIShortGCView.ChargeWeight = Util.String2Decimal(objDR["Charged_Weight"].ToString());
                objIShortGCView.FreightRate = Util.String2Decimal(objDR["Freight_Rate"].ToString());
                objIShortGCView.TotalInvoiceAmount = Util.String2Decimal(objDR["Total_Invoice_Value"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.LengthChargeHeadId = Util.String2Int(objDR["Length_Charge_Head_Id"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.ServiceTaxPayableBy = Util.String2Int(objDR["Tax_Payable_By"].ToString());

                objIShortGCView.Is_ServiceTaxApplicableForConsignor = 0;
                objIShortGCView.Is_ServiceTaxApplicableForConsignee = 0;

                if (Convert.ToBoolean(objDR["Is_Consignor_Service_Tax_Applicable"].ToString()))
                {
                    objIShortGCView.Is_ServiceTaxApplicableForConsignor = 1;
                }

                if (Convert.ToBoolean(objDR["Is_Consignee_Service_Tax_Applicable"].ToString()))
                {
                    objIShortGCView.Is_ServiceTaxApplicableForConsignee = 1;
                }

                if (Util.String2Bool(objDR["Is_ODA"].ToString()))
                {
                    objIShortGCView.Is_ODA = true;
                }
                else
                {
                    objIShortGCView.Is_ODA = false;
                }

                if (Util.String2Bool(objDR["Is_Opening_Gc"].ToString()))
                {
                    objIShortGCView.Is_Opening_GC = true;
                }
                else
                {
                    objIShortGCView.Is_Opening_GC = false;
                }

                objIShortGCView.ODAChargesUpTo500Kg = Util.String2Decimal(objDR["Oda_charges_upto_500_Kg"].ToString());
                objIShortGCView.ODAChargesAbove500Kg = Util.String2Decimal(objDR["Oda_charges_above_500_Kg"].ToString());

                if (Util.String2Bool(objDR["Is_Octroi_Applicable"].ToString()))
                {
                    objIShortGCView.Is_OctroiApplicable = true;
                }
                else
                {
                    objIShortGCView.Is_OctroiApplicable = false;
                }

                if (Util.String2Bool(objDR["Is_To_Pay_Booking"].ToString()))
                {
                    objIShortGCView.Is_ToPayBookingApplicable = true;
                }
                else
                {
                    objIShortGCView.Is_ToPayBookingApplicable = false;
                }

                objIShortGCView.Is_MultipleBilling = Util.String2Bool(objDR["Is_Multiple_Billing"].ToString());

                if (!objIShortGCView.Is_MultipleBilling)
                {
                    objIShortGCView.SetBillingBranch(objDR["Billing_Branch_Name"].ToString(), objDR["Billing_Branch_ID"].ToString());

                    objIShortGCView.SetBillingParty(objDR["Billing_Client_Name"].ToString(), objDR["Billing_Client_ID"].ToString());
                }
                else
                {
                    objIShortGCView.SetBillingBranch("", "0");
                    objIShortGCView.SetBillingParty("", "0");
                }

                objIShortGCView.BillingBranchId = Util.String2Int(objDR["Billing_Branch_ID"].ToString());
                objIShortGCView.BillingPartyId = Util.String2Int(objDR["Billing_Client_ID"].ToString());

                objIShortGCView.BillingRemark = objDR["Billing_Remarks"].ToString();
                objIShortGCView.Freight = Util.String2Decimal(objDR["Freight_Amt"].ToString());
                objIShortGCView.LocalCharge = Util.String2Decimal(objDR["Local_Charges"].ToString());
                objIShortGCView.LoadingCharge = Util.String2Decimal(objDR["Hamali_Charges"].ToString());
                objIShortGCView.StationaryCharge = Util.String2Decimal(objDR["Bilti_Charges"].ToString());
                objIShortGCView.FOVRiskCharge = Util.String2Decimal(objDR["FOV"].ToString());
                objIShortGCView.ToPayCharge = Util.String2Decimal(objDR["TP_Charges"].ToString());

                objIShortGCView.DDCharge = Util.String2Decimal(objDR["DD_Charges"].ToString());
                objIShortGCView.LengthCharge = Util.String2Decimal(objDR["Length_Charge"].ToString());
                objIShortGCView.UnloadingCharge = Util.String2Decimal(objDR["Unloading_Charge"].ToString());

                objIShortGCView.NFormCharge = Util.String2Decimal(objDR["NForm_Charge"].ToString());
                
                objIShortGCView.Applicable_Standard_DDCharge_Rate = Util.String2Decimal(objDR["Std_DD_Charge_Rate"].ToString());
                objIShortGCView.Standard_DDCharge_Rate = Util.String2Decimal(objDR["Std_DD_Charge_Rate"].ToString());

                objIShortGCView.DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                objIShortGCView.Standard_DACCCharges = Util.String2Decimal(objDR["Std_DACC_Charge"].ToString());
                objIShortGCView.Applicable_Standard_DACCCharges = Util.String2Decimal(objDR["Std_DACC_Charge"].ToString());

                objIShortGCView.OtherCharges = Util.String2Decimal(objDR["Other_Charges"].ToString());
                objIShortGCView.SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                objIShortGCView.Abatment = Util.String2Decimal(objDR["Tax_Abate"].ToString());
                objIShortGCView.TaxableAmount = Util.String2Decimal(objDR["Amt_Taxable"].ToString());
                objIShortGCView.ServiceTax = Util.String2Decimal(objDR["Service_Tax_Amount"].ToString());

                if (!Is_Attached_Gc) // && objIShortGCView.keyID <= 0)
                {
                    objIShortGCView.ReBookGC_OctroiAmount = Util.String2Decimal(objDR["ReBook_Octroi_Amount"].ToString());
                    objIShortGCView.ReBook_GCOctroiPaidByID = Util.String2Int(objDR["ReBook_GC_Octroi_Paid_By_ID"].ToString());
                }

                objIShortGCView.TotalGCAmount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());

                objIShortGCView.Previous_SubTotal = Util.String2Decimal(objDR["Sub_Total"].ToString());
                objIShortGCView.Previous_GrandTotal = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());

                objIShortGCView.Advance = Util.String2Decimal(objDR["Advance_Amount"].ToString());
                objIShortGCView.CashAmount = Util.String2Decimal(objDR["Cash_Amount"].ToString());
                objIShortGCView.ChequeAmount = Util.String2Decimal(objDR["Cheque_Amount"].ToString());
                objIShortGCView.ChequeDate = Convert.ToDateTime(objDR["Cheque_Date"].ToString());

                if (Util.String2Int(objDR["Cheque_No"].ToString()) == 0)
                {
                    objIShortGCView.ChequeNo = "";
                }
                else
                {
                    objIShortGCView.ChequeNo = Convert.ToInt32(objDR["Cheque_No"].ToString()).ToString("000000");
                }

                objIShortGCView.BankName = objDR["Bank_Name"].ToString();

                //objIShortGCView.LoadingSuperVisor  =  Util.String2Int ( objDR ["Loading_Supervisor_ID"].ToString());

                //objIShortGCView.MarketingExecutive  =  Util.String2Int ( objDR ["Marketing_Executive_ID"].ToString());

                if (Util.String2Int(objDR["Loading_Supervisor_ID"].ToString()) > 0)
                {
                    objIShortGCView.SetLoadingSuperVisor(objDR["Loading_Supervisor_Name"].ToString(), objDR["Loading_Supervisor_ID"].ToString());
                }
                else
                {
                    objIShortGCView.SetLoadingSuperVisor("", "0");
                }

                if (Util.String2Int(objDR["Marketing_Executive_ID"].ToString()) > 0)
                {
                    objIShortGCView.SetMarketingExecutive(objDR["Marketing_Executive_Name"].ToString(), objDR["Marketing_Executive_ID"].ToString());
                }
                else
                {
                    objIShortGCView.SetLoadingSuperVisor("", "0");
                }

                objIShortGCView.LoadingSuperVisorId = Util.String2Int(objDR["Loading_Supervisor_ID"].ToString());
                objIShortGCView.MarketingExecutiveId = Util.String2Int(objDR["Marketing_Executive_ID"].ToString());

                //----------------------------------------------------------------------------------------

                objIShortGCView.Standard_FreightRate = Util.String2Decimal(objDR["Std_Freight_Rate"].ToString());
                objIShortGCView.Standard_ServiceTaxPercent = Util.String2Decimal(objDR["Service_Tax_Percent"].ToString());
                objIShortGCView.Standard_BiltiCharges = Util.String2Decimal(objDR["Std_Bilti_Charges"].ToString());
                objIShortGCView.Standard_DDCharge = Util.String2Decimal(objDR["Std_DD_Charge"].ToString());
                objIShortGCView.Standard_FOV = Util.String2Decimal(objDR["Std_FOV"].ToString());
                objIShortGCView.Standard_FOVPercentage = Util.String2Decimal(objDR["Std_FOVPercent"].ToString());
                objIShortGCView.Standard_FreightAmount = Util.String2Decimal(objDR["Std_Freight_Amt"].ToString());

                objIShortGCView.Standard_HamaliPerKg = Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());
                objIShortGCView.Standard_HamaliCharge = Util.String2Decimal(objDR["Std_Hamali_Charge"].ToString());

                objIShortGCView.Standard_HamaliPerArticles = Util.String2Decimal(objDR["Hamali_Per_Articles"].ToString());

                objIShortGCView.Standard_LocalCharge = Util.String2Decimal(objDR["Std_Local_Charge"].ToString());
                objIShortGCView.Standard_ServiceTaxAmount = Util.String2Decimal(objDR["Std_Service_Tax_Amount"].ToString());
                objIShortGCView.Standard_ToPayCharges = Util.String2Decimal(objDR["Std_TP_Charges"].ToString());
                objIShortGCView.Standard_CFTFactor = Util.String2Decimal(objDR["Std_CFT_Factor"].ToString());
                objIShortGCView.Standard_Octroi_Form_Charge = Util.String2Decimal(objDR["Std_Octroi_Form_Charges"].ToString());
                objIShortGCView.Standard_Octroi_Service_Charge = Util.String2Decimal(objDR["Std_Octroi_Service_Charges"].ToString());
                objIShortGCView.Standard_Demurrage_Days = Util.String2Decimal(objDR["Std_Demurrage_Days"].ToString());
                objIShortGCView.Standard_Demurrage_Rate = Util.String2Decimal(objDR["Std_Demurrage_Rate"].ToString());
                objIShortGCView.Standard_GI_Charges = Util.String2Decimal(objDR["Std_GI_Charges"].ToString());
                objIShortGCView.Standard_LengthCharge = Util.String2Decimal(objDR["Length_Charge"].ToString());

                objIShortGCView.Standard_NForm_Charge = objIShortGCView.NFormCharge;

                //objIShortGCView.Applicable_Standard_BiltiCharges = objIShortGCView.Standard_BiltiCharges;
                //objIShortGCView.Applicable_Standard_CFTFactor = objIShortGCView.VolumetricToKgFactor ;

                //----------------------------------------------------------------------------------------

                objIShortGCView.Applicable_Standard_BiltiCharges = objIShortGCView.Standard_BiltiCharges;
                objIShortGCView.Applicable_Standard_FOV = objIShortGCView.Standard_FOV;// objIShortGCView.FOVRiskCharge;
                objIShortGCView.Applicable_Standard_FOVPercentage = objIShortGCView.Standard_FOVPercentage;
                objIShortGCView.Applicable_Standard_FreightRate = objIShortGCView.Standard_FreightRate;
                objIShortGCView.Applicable_Standard_HamaliCharge = objIShortGCView.Standard_HamaliCharge;
                objIShortGCView.Applicable_Standard_HamaliPerKg = objIShortGCView.Standard_HamaliPerKg;
                objIShortGCView.Applicable_Standard_HamaliPerArticles = objIShortGCView.Standard_HamaliPerArticles;
                objIShortGCView.Applicable_Standard_LocalCharge = objIShortGCView.Standard_LocalCharge;

                if (objIShortGCView.ContractId > 0)
                {
                    objIShortGCView.Applicable_Standard_MinimumFOV = 0;
                    objIShortGCView.Applicable_Standard_MinimumChargeWeight = 0;
                    objIShortGCView.Applicable_Standard_MinimumFOV = 0;
                }
                else
                {
                    objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                    objIShortGCView.Applicable_Standard_MinimumChargeWeight = objIShortGCView.Standard_MinimumChargeWeight;
                    objIShortGCView.Applicable_Standard_MinimumFOV = objIShortGCView.Standard_MinimumFOV;
                }

                //objIShortGCView.Applicable_Standard_Minimum_Hamali_Per_Kg = objIShortGCView.st
                objIShortGCView.Applicable_Standard_ServiceTaxAmount = objIShortGCView.Standard_ServiceTaxAmount;
                objIShortGCView.Applicable_Standard_ServiceTaxPercent = objIShortGCView.Standard_ServiceTaxPercent;

                objIShortGCView.ServiceTax_Label = "Service Tax " + objIShortGCView.Applicable_Standard_ServiceTaxPercent.ToString() + "%";

                objIShortGCView.Applicable_Standard_ToPayCharges = objIShortGCView.Standard_ToPayCharges;
                objIShortGCView.Applicable_Standard_DDCharge = objIShortGCView.Standard_DDCharge;
                objIShortGCView.Applicable_Standard_DDCharge_Rate = objIShortGCView.Standard_DDCharge_Rate;
                objIShortGCView.Applicable_Standard_CFTFactor = objIShortGCView.Standard_CFTFactor;
                objIShortGCView.Applicable_Standard_Octroi_Form_Charge = objIShortGCView.Standard_Octroi_Form_Charge;
                objIShortGCView.Applicable_Standard_Octroi_Service_Charge = objIShortGCView.Standard_Octroi_Service_Charge;
                objIShortGCView.Applicable_Standard_GI_Charges = objIShortGCView.Standard_GI_Charges;
                objIShortGCView.Applicable_Standard_Demurrage_Days = objIShortGCView.Standard_Demurrage_Days;
                objIShortGCView.Applicable_Standard_Demurrage_Rate = objIShortGCView.Standard_Demurrage_Rate;

                objIShortGCView.Applicable_Standard_NForm_Charge = objIShortGCView.Standard_NForm_Charge;
                //----------------------------------------------------------------------------------------

                objIShortGCView.OtherChargesRemark = objDR["GC_Remarks_Other_Charges"].ToString();
                objIShortGCView.InstructionRemark = objDR["GC_Remarks"].ToString();
                objIShortGCView.Enclosures = objDR["Enclosures"].ToString();

                objIShortGCView.Is_POD = Convert.ToBoolean(objDR["Acknowledge"].ToString());
                objIShortGCView.Is_SignedByConsignor = Convert.ToBoolean(objDR["Is_Signed_By_Consignor"].ToString());

                objIShortGCView.RoadPermitTypeId = Util.String2Int(objDR["Road_Permit_Type_ID"].ToString());
                objIShortGCView.RoadPermitSrNo = objDR["Road_Permit_SrNo"].ToString();
                objIShortGCView.Is_Insured = Convert.ToBoolean(objDR["Is_Insured"].ToString());


                objIShortGCView.Is_DACC = Convert.ToBoolean(objDR["Is_DACC"].ToString());


                objIShortGCView.Session_ContainerTypeId = Util.String2Int(objDR["Container_Type_Id"].ToString());

                objIShortGCView.Session_ContainerNoPart1 = objDR["ContainerNo1"].ToString();
                objIShortGCView.Session_ContainerNoPart2 = objDR["ContainerNo2"].ToString();
                objIShortGCView.Session_SealNo = objDR["SealNo"].ToString();
                objIShortGCView.Session_ReturnToYardId = Util.String2Int(objDR["Return_To_Yard_Id"].ToString());

                objIShortGCView.Session_ReturnToYardName = objDR["Return_To_Yard_Name"].ToString();
                objIShortGCView.Session_NFormNo = objDR["NFormNo"].ToString();

                Get_RequireForms();
            }
            else
            {
                objIShortGCView.Session_ConsigneeName = "";

                objIShortGCView.Session_ConsigneeAddressLine1 = "";
                objIShortGCView.Session_ConsigneeAddressLine2 = "";

                objIShortGCView.Session_InsuranceCompany = "";
                objIShortGCView.Session_PolicyNo = "";
                objIShortGCView.Session_PolicyAmount = 0;
                objIShortGCView.Session_RiskAmount = 0;
                objIShortGCView.Session_PolicyExpDate = DateTime.Now;

                objIShortGCView.Session_ContainerTypeId = 0;

                objIShortGCView.Session_ContainerNoPart1 = "";
                objIShortGCView.Session_ContainerNoPart2 = "";
                objIShortGCView.Session_SealNo = "";
                objIShortGCView.Session_ReturnToYardId = 0; ;

                objIShortGCView.Session_ReturnToYardName = "";
                objIShortGCView.Session_NFormNo = "";
                Get_BranchRateParameter();
            }

            objDS_GC.Tables[1].TableName = "multiple_commodity";
            objDS_GC.Tables[2].TableName = "invoice";

            objDS_GC.Tables[3].TableName = "other_chages";
            objDS_GC.Tables[4].TableName = "multiple_billing_details";

            //Common.SetPrimaryKeys(new string[] { "Commodity_Id,Item_Id,Packing_Id" }, objDS.Tables[1]);
            
            objIShortGCView.Session_MultipleCommodityGrid = objDS_GC.Tables[1];
            //objIShortGCView.Bind_dg_Commodity = objDS_GC.Tables[1];
            objIShortGCView.Bind_dg_Invoice = objDS_GC.Tables[2];

            objIShortGCView.Session_GCOtherChargeHead  = objDS_GC.Tables[3];
            objIShortGCView.Session_BillingDetailsGrid = objDS_GC.Tables[4];
            
            DataSet objDs_Main_BillingDetailsGrid = new DataSet();
            objDs_Main_BillingDetailsGrid.Tables.Add(objDS_GC.Tables[4].Copy());
            objDs_Main_BillingDetailsGrid.Tables[0].TableName = "billing_details";

            objIShortGCView.Session_Main_BillingDetailsGrid = objDs_Main_BillingDetailsGrid.Tables[0];

            objIShortGCView.Session_ChequeDetailsGrid = objDS_GC.Tables[5]; 

            if (Is_Attached_Gc == true)
            {
                Get_Applicable_Service_Tax();
            }

             

        }
        
        public DataSet Get_ToLocationDetails()
        {
            objDS = objShortGCModel.Get_ToLocationDetails();
            return objDS;
        }

        public void Get_TransitDays()
        {
            objShortGCModel.Get_TransitDays();           
        }

        public void Get_Service_Tax_Applicable_For_Commodity()
        {
            objShortGCModel.Get_Service_Tax_Applicable_For_Commodity();
        }

        public void Get_Service_Tax_Details()
        {
            objShortGCModel.Get_ServiceTaxDetails();
        }

        public void Get_BookingSubType()
        {
            DataSet ds_BookingSubType = new DataSet();
            ds_BookingSubType = objShortGCModel.Get_BookingSubType();
            objIShortGCView.BindBookingSubType = ds_BookingSubType.Tables[0];
        }

        public void Get_LengthCharge()
        {
            objShortGCModel.Get_LengthCharge();            
        }

        public DataSet Get_ConsignorConsigneeDetails(Int32 ConsignorConsigneeId, Boolean Is_RegularClient, Boolean Is_Consignor)
        {
            objDS = objShortGCModel.Get_ConsignorConsigneeDetails(ConsignorConsigneeId, Is_RegularClient, Is_Consignor);
            return objDS;
        }

        public DataSet Get_From_Location_Details()
        {
            objDS = objShortGCModel.Get_From_Location_Details();
            return objDS;
        }

        public void Get_StdandardFreightRate()
        {
            objShortGCModel.Get_StdandardFreightRate();
        }

        public void Get_Additional_Freight()
        {
            objShortGCModel.Get_Additional_Freight();
        }
         
        public void save()
        {            
           // base.DBSave();
            objShortGCModel.Save();
        }         
    }
}
