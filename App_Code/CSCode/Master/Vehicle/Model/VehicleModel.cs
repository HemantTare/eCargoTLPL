using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using Raj.EF.MasterView;
//using Raj.EF.TransactionsView;
/// <summary>
/// Summary description for VehicleModel
/// </summary>
/// 

namespace Raj.EF.MasterModel
{
    public class VehicleModel : IModel
    {
        private IVehicleView objIVehicleView;
        private DAL objDAL = new DAL();
      //  private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public VehicleModel(IVehicleView VehicleView)
        {
            objIVehicleView = VehicleView;
        }

        public DataSet ReadValues()
        {
            DataSet DS = null;
            return DS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Number_Part1", SqlDbType.VarChar, 5,objIVehicleView.VehicleInformationView.NumberPart1),
            objDAL.MakeInParams("@Number_Part2", SqlDbType.NVarChar, 5,objIVehicleView.VehicleInformationView.NumberPart2),
            objDAL.MakeInParams("@Number_Part3", SqlDbType.NVarChar, 5,objIVehicleView.VehicleInformationView.NumberPart3),
            objDAL.MakeInParams("@Number_Part4", SqlDbType.VarChar, 5,objIVehicleView.VehicleInformationView.NumberPart4),
            objDAL.MakeInParams("@Market_Owener", SqlDbType.VarChar, 50,objIVehicleView.VehicleInformationView.OwnerName),
            objDAL.MakeInParams("@Market_Address_1", SqlDbType.VarChar, 100,objIVehicleView.VehicleInformationView.AddressView.AddressLine1),
            objDAL.MakeInParams("@MarketAddress_2", SqlDbType.VarChar, 100,objIVehicleView.VehicleInformationView.AddressView.AddressLine2),
            objDAL.MakeInParams("@Market_Email_ID", SqlDbType.VarChar, 100,objIVehicleView.VehicleInformationView.AddressView.EmailId),
            objDAL.MakeInParams("@General_Comments", SqlDbType.VarChar, 500,objIVehicleView.VehicleInformationView.Notes),
            objDAL.MakeInParams("@Paint_Code", SqlDbType.VarChar, 10,objIVehicleView.EngineBodySpecificationView.PaintCode),              //10
            objDAL.MakeInParams("@Paint_Color", SqlDbType.VarChar, 25,objIVehicleView.EngineBodySpecificationView.PaintColor),
            objDAL.MakeInParams("@Loan_Comments", SqlDbType.VarChar, 500,objIVehicleView.VehicleLoanDetailsView.Comments),
            objDAL.MakeInParams("@Vehicle_No", SqlDbType.NVarChar, 20,objIVehicleView.VehicleInformationView.VehicleNo),
            objDAL.MakeInParams("@GPS_Connectivity_ID", SqlDbType.NVarChar, 25,objIVehicleView.VehicleInformationView.GPSConnectivityId),
            objDAL.MakeInParams("@Market_Pin_Code", SqlDbType.NVarChar, 15,objIVehicleView.VehicleInformationView.AddressView.PinCode),
            objDAL.MakeInParams("@Market_Std_Code", SqlDbType.NVarChar, 15,objIVehicleView.VehicleInformationView.AddressView.StdCode),
            objDAL.MakeInParams("@Market_Phone_1", SqlDbType.NVarChar, 20,objIVehicleView.VehicleInformationView.AddressView.Phone1),
            objDAL.MakeInParams("@Market_Phone_2", SqlDbType.NVarChar, 20,objIVehicleView.VehicleInformationView.AddressView.Phone2),
            objDAL.MakeInParams("@Market_Mobile_No", SqlDbType.NVarChar, 25,objIVehicleView.VehicleInformationView.AddressView.MobileNo),
            objDAL.MakeInParams("@Market_Fax", SqlDbType.NVarChar, 20,objIVehicleView.VehicleInformationView.AddressView.FaxNo),            //20
            objDAL.MakeInParams("@Chasis_No", SqlDbType.NVarChar, 50,objIVehicleView.EngineBodySpecificationView.ChasisNo),
            objDAL.MakeInParams("@Trolly_Chasis_No", SqlDbType.NVarChar, 50,objIVehicleView.EngineBodySpecificationView.TrollyChasisNo),
            objDAL.MakeInParams("@Engine_No", SqlDbType.NVarChar, 25,objIVehicleView.EngineBodySpecificationView.EngineNo),
            objDAL.MakeInParams("@Power_bhp", SqlDbType.NVarChar, 5,objIVehicleView.EngineBodySpecificationView.Power),
            objDAL.MakeInParams("@Ignition_Key_Code", SqlDbType.NVarChar, 20,objIVehicleView.EngineBodySpecificationView.IgnitionKeyCode),
            objDAL.MakeInParams("@Door_Key_Code", SqlDbType.NVarChar, 20,objIVehicleView.EngineBodySpecificationView.DoorKeyCode),
            objDAL.MakeInParams("@Loan_Acc_No", SqlDbType.NVarChar, 20,objIVehicleView.VehicleLoanDetailsView.LoanAcctNo),
            objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,objIVehicleView.keyID),
            objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.VehicleCategoryId),         //30
            objDAL.MakeInParams("@Vehicle_Type_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.VehicleTypeId),
            objDAL.MakeInParams("@Vehicle_Body_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.VehicleBodyId),
            objDAL.MakeInParams("@Carrier_Category_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.CarrierCategoryId),
            objDAL.MakeInParams("@Manufacturer_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.ManufacturerId),
            objDAL.MakeInParams("@Vehicle_Model_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.VehicleModelId),
            objDAL.MakeInParams("@Year_Of_Manufacture", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.YearOfManufacture),
            objDAL.MakeInParams("@Driver1_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.DriverId1),
            objDAL.MakeInParams("@Driver2_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.DriverId2),
            objDAL.MakeInParams("@Cleaner_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.CleanerId),
            objDAL.MakeInParams("@Vendor_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.BrokerId),                            //40
            //objDAL.MakeInParams("@TDS_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.TdsId),
            objDAL.MakeInParams("@Market_City_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.AddressView.CityId),
            objDAL.MakeInParams("@Fuel_Type_ID", SqlDbType.Int, 0,objIVehicleView.EngineBodySpecificationView.FuelTypeID),
            objDAL.MakeInParams("@Vehicle_Capacity", SqlDbType.Int, 0,objIVehicleView.EngineBodySpecificationView.VehicleCapacity),
            objDAL.MakeInParams("@Gross_Wt", SqlDbType.Int, 0,objIVehicleView.EngineBodySpecificationView.GrossVehicleWt),
            objDAL.MakeInParams("@Unladen_Wt", SqlDbType.Int, 0,objIVehicleView.EngineBodySpecificationView.UnladenWt),
            objDAL.MakeInParams("@Loan_Bank_ID", SqlDbType.Int, 0,objIVehicleView.VehicleLoanDetailsView.BankID),
            objDAL.MakeInParams("@Terms_Months", SqlDbType.Int, 0,objIVehicleView.VehicleLoanDetailsView.TermsInMonths),
            objDAL.MakeInParams("@Interest_Type_ID", SqlDbType.Int, 0,objIVehicleView.VehicleLoanDetailsView.InterestTypeID),
            objDAL.MakeInParams("@EMI_Payment_Mode_ID", SqlDbType.Int, 0,objIVehicleView.VehicleLoanDetailsView.PaymentModeID),             //50
            objDAL.MakeInParams("@Front_Wheel_Size_ID", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.FrontWheelSizeID),
            objDAL.MakeInParams("@Front_Tire_Size_ID", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.FrontTyreSizeID),
            objDAL.MakeInParams("@Front_PSI", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.FrontPSI),
            objDAL.MakeInParams("@Rear_Wheel_Size_ID", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.RearWheelSizeID),
            objDAL.MakeInParams("@Rear_Tire_Size_ID", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.RearTyreSizeID),
            objDAL.MakeInParams("@Rear_PSI", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.RearPSI),
            objDAL.MakeInParams("@No_Of_Stepheany", SqlDbType.Int, 0,objIVehicleView.VehicleChasisTyresView.NoOfStephaney),
            objDAL.MakeInParams("@Permit_Type_ID", SqlDbType.Int, 0,objIVehicleView.RegistrationPermitView.PermitTypeId),
            objDAL.MakeInParams("@Permit_No", SqlDbType.NVarChar, 25,objIVehicleView.RegistrationPermitView.PermitNo),
            objDAL.MakeInParams("@Permit_Document_No", SqlDbType.NVarChar, 25,objIVehicleView.RegistrationPermitView.PermitDocumentNo),
            objDAL.MakeInParams("@Permit_Valid_From", SqlDbType.DateTime, 0,objIVehicleView.RegistrationPermitView.PermitValidFrom),        //60
            objDAL.MakeInParams("@Permit_Valid_Upto", SqlDbType.DateTime, 0,objIVehicleView.RegistrationPermitView.PermitValidUpTo),
            objDAL.MakeInParams("@Truck_Hire_Type_Id", SqlDbType.Int, 0,objIVehicleView.VehicleHireDetailsView.HireTypeID),
            objDAL.MakeInParams("@Region_ID", SqlDbType.Int, 0,objIVehicleView.VehicleHireDetailsView.RegionID),
            objDAL.MakeInParams("@Area_ID", SqlDbType.Int, 0,objIVehicleView.VehicleHireDetailsView.AreaID),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0,objIVehicleView.VehicleHireDetailsView.BranchID),
            objDAL.MakeInParams("@Opening_Odometer", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.OpenOdometer),
            objDAL.MakeInParams("@Current_Odometer", SqlDbType.Int, 0,objIVehicleView.VehicleInformationView.CurrentOdometer),
            objDAL.MakeInParams("@Attachment_Form_ID", SqlDbType.Int, 0,objIVehicleView.AttachmentsView.AttachmentFormId),
            //objDAL.MakeInParams("@TDS_Rate_Percent", SqlDbType.Decimal, 0,objIVehicleView.VehicleInformationView.TdsRatePercent),
            //objDAL.MakeInParams("@TDS_Exemption_Limit", SqlDbType.Decimal, 0,objIVehicleView.VehicleInformationView.TdsExemptionLimit),     //70
            objDAL.MakeInParams("@Fuel_Tank_Capacity", SqlDbType.Decimal, 0,objIVehicleView.EngineBodySpecificationView.FuelTankCapacity),
            objDAL.MakeInParams("@Wheel_Base", SqlDbType.Decimal, 0,objIVehicleView.EngineBodySpecificationView.WheelBase),
            objDAL.MakeInParams("@Length", SqlDbType.Decimal, 0,objIVehicleView.EngineBodySpecificationView.Length),
            objDAL.MakeInParams("@Height", SqlDbType.Decimal, 0,objIVehicleView.EngineBodySpecificationView.Height),
            objDAL.MakeInParams("@Width", SqlDbType.Decimal, 0,objIVehicleView.EngineBodySpecificationView.Width),
            objDAL.MakeInParams("@Loan_Amount", SqlDbType.Decimal, 0,objIVehicleView.VehicleLoanDetailsView.LoanAmount),
            objDAL.MakeInParams("@Interest_Rate", SqlDbType.Decimal, 0,objIVehicleView.VehicleLoanDetailsView.RateOfInterest),
            objDAL.MakeInParams("@EMI_Amount", SqlDbType.Decimal, 0,objIVehicleView.VehicleLoanDetailsView.EMIAmount),
            objDAL.MakeInParams("@Truck_Hire_Amount", SqlDbType.Decimal, 0,objIVehicleView.VehicleHireDetailsView.HireAmount),
            objDAL.MakeInParams("@Is_TDS_Applicable", SqlDbType.Bit, 0,objIVehicleView.VehicleInformationView.TDSAppView.IsTDSApp),             //80
            objDAL.MakeInParams("@Is_TDS_To_Owner", SqlDbType.Bit, 0,objIVehicleView.VehicleInformationView.TDSCertificateForOwner),
            objDAL.MakeInParams("@Multiple_Trip_Day", SqlDbType.Bit, 0,objIVehicleView.VehicleHireDetailsView.MultipleTripADay),
            objDAL.MakeInParams("@First_Payment_Due", SqlDbType.DateTime, 0,objIVehicleView.VehicleLoanDetailsView.FirstPaymentDue),
            objDAL.MakeInParams("@Last_Payment_Due", SqlDbType.DateTime, 0,objIVehicleView.VehicleLoanDetailsView.LastPaymentDue),
            objDAL.MakeInParams("@On_Road_Date", SqlDbType.DateTime, 0,objIVehicleView.VehicleInformationView.OnRoadDate),
            objDAL.MakeInParams("@Registration_Date", SqlDbType.DateTime, 0,objIVehicleView.RegistrationFitnessView.RegistrationDate),
            objDAL.MakeInParams("@Registration_Certificate_No", SqlDbType.NVarChar, 50,objIVehicleView.RegistrationFitnessView.RegistraionCertificateNo),
            objDAL.MakeInParams("@Registration_State_ID", SqlDbType.Int, 0,objIVehicleView.RegistrationFitnessView.RegistrationStateID),
            objDAL.MakeInParams("@Registration_RTO_City_ID", SqlDbType.Int, 0,objIVehicleView.RegistrationFitnessView.RegistraionRtoId),
            objDAL.MakeInParams("@Registration_Fee", SqlDbType.Decimal, 0,objIVehicleView.RegistrationFitnessView.RegistrationFee),           //90
            objDAL.MakeInParams("@Fitness_Certificate_No", SqlDbType.NVarChar, 50,objIVehicleView.RegistrationFitnessView.FitnesCertificateNo),
            objDAL.MakeInParams("@Fitness_RTO_City_ID", SqlDbType.Int, 0,objIVehicleView.RegistrationFitnessView.FitnessRtoId),
            objDAL.MakeInParams("@Fitness_Issue_Date", SqlDbType.DateTime, 0,objIVehicleView.RegistrationFitnessView.IssueDate),
            objDAL.MakeInParams("@Fitness_Valid_Upto", SqlDbType.DateTime, 0,objIVehicleView.RegistrationFitnessView.ValidUpTO),
            objDAL.MakeInParams("@Fitness_Amount", SqlDbType.Decimal, 0,objIVehicleView.RegistrationFitnessView.Amount),
            objDAL.MakeInParams("@Vehicle_Specification_Details", SqlDbType.Xml, 0,objIVehicleView.EngineBodySpecificationView.VehicleSpecificationDetailsXML),
            objDAL.MakeInParams("@Vehicle_Loan_Details", SqlDbType.Xml, 0,objIVehicleView.VehicleLoanDetailsView.VehicleLoanDetailsXML),
            objDAL.MakeInParams("@Vehicle_Tyre_Configuration_Details", SqlDbType.Xml, 0,objIVehicleView.VehicleChasisTyresView.VehicleTyreConfigurationDetailsXML),
            objDAL.MakeInParams("@Vehicle_Permit_Tax_Details", SqlDbType.Xml, 0,objIVehicleView.RegistrationPermitView.VehiclePermitTaxDetailsXML),
            objDAL.MakeInParams("@Vehicle_Temporary_Permit_Details", SqlDbType.Xml, 0,objIVehicleView.RegistrationPermitView.VehicleTemporaryPermitDetailsXML), //100
            objDAL.MakeInParams("@Attachments_XML", SqlDbType.Xml, 0,objIVehicleView.AttachmentsView.AttachmentsXML),
            objDAL.MakeInParams("@Purchase_Broker_Name", SqlDbType.VarChar, 50,""),//doubt
            objDAL.MakeInParams("@Purchase_Comments", SqlDbType.VarChar, 500,""),//doubt
            objDAL.MakeInParams("@Purchase_Invoice_No", SqlDbType.NVarChar, 25,""),//doubt
            objDAL.MakeInParams("@PO_No", SqlDbType.NVarChar, 20,""),//doubt
            objDAL.MakeInParams("@Purchase_Type_ID", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Purchase_Vendor_ID", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Purchase_Odometer", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Current_Route_Id", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,0),//doubt                                                   //110
            objDAL.MakeInParams("@Ledger_Id_Driver_Acc", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Ledger_Id_VT", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Ledger_Id_VT_Driver_Acc", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Status_ID", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Start_Kms", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@End_Kms", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Old_Start_Kms", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Old_End_Kms", SqlDbType.Int, 0,0),//doubt
            objDAL.MakeInParams("@Assesable_Vehicle_Price", SqlDbType.Decimal, 0,0),//doubt
            objDAL.MakeInParams("@Total_Vehicle_Price", SqlDbType.Decimal, 0,0),//doubt                     //120
            objDAL.MakeInParams("@Brokerage_Amount", SqlDbType.Decimal, 0,0),//doubt
            objDAL.MakeInParams("@Is_Insurance_Transferred", SqlDbType.Bit, 0,0),//doubt
            objDAL.MakeInParams("@Is_RTO_Transferred", SqlDbType.Bit, 0,0),//doubt
            objDAL.MakeInParams("@Is_Photo_Taken", SqlDbType.Bit, 0,0),//doubt
            objDAL.MakeInParams("@Is_VTrans", SqlDbType.Bit, 0,1),//doubt 
            objDAL.MakeInParams("@Is_VXpress", SqlDbType.Bit, 0,1),//doubt
            objDAL.MakeInParams("@PO_Date", SqlDbType.DateTime, 0,DateTime.Now.Date),//doubt
            objDAL.MakeInParams("@Purchase_Date", SqlDbType.DateTime, 0,DateTime.Now.Date),//doubt
            objDAL.MakeInParams("@Mfg_Expires_On", SqlDbType.DateTime, 0,DateTime.Now.Date),//doubt
            objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int , 0,objIVehicleView.VehicleInsurancePremiumView.VehicleInsuranceID),             //130
           objDAL.MakeInParams("@Insurance_Company_ID", SqlDbType.Int , 0,objIVehicleView.VehicleInsurancePremiumView.InsuranceCompanyID), 
           objDAL.MakeInParams("@Insurance_Company_Branch_ID", SqlDbType.Int, 0, objIVehicleView.VehicleInsurancePremiumView.IssuingBranchID),
           objDAL.MakeInParams("@Policy_No",SqlDbType.NVarChar,25,objIVehicleView.VehicleInsurancePremiumView.PolicyNo),
           objDAL.MakeInParams("@Agent_ID", SqlDbType.Int, 0,objIVehicleView.VehicleInsurancePremiumView.AgentID), 
           objDAL.MakeInParams("@Commence_Date", SqlDbType.DateTime,0, objIVehicleView.VehicleInsurancePremiumView.CommenceDate),
           objDAL.MakeInParams("@Expiry_Date",SqlDbType.DateTime,0,objIVehicleView.VehicleInsurancePremiumView.ExpiryDate),
           objDAL.MakeInParams("@IDV", SqlDbType.Decimal, 0,objIVehicleView.VehicleInsurancePremiumView.IDV), 
           objDAL.MakeInParams("@First_Party_Premium", SqlDbType.Decimal, 0, objIVehicleView.VehicleInsurancePremiumView.FirstPartyPremium),
           objDAL.MakeInParams("@Third_Party_Premium", SqlDbType.Decimal, 0,objIVehicleView.VehicleInsurancePremiumView.ThirdPartyPremium), 
           objDAL.MakeInParams("@Loading_Percent_TPP", SqlDbType.Decimal,0, objIVehicleView.VehicleInsurancePremiumView.LoadingPercentTPP),             //140
           objDAL.MakeInParams("@Loading_Amount_TPP", SqlDbType.Decimal,0, objIVehicleView.VehicleInsurancePremiumView.LoadingAmountTPP),
           objDAL.MakeInParams("@Loading_Percent_FPP",SqlDbType.Decimal,0,objIVehicleView.VehicleInsurancePremiumView.LoadingPercentFPP),
           objDAL.MakeInParams("@Loading_Amount_FPP",SqlDbType.Decimal,0,objIVehicleView.VehicleInsurancePremiumView.LoadingAmountFPP),
           objDAL.MakeInParams("@NCB_Percent_FPP", SqlDbType.Decimal , 0,objIVehicleView.VehicleInsurancePremiumView.NCBPercentFPP), 
           objDAL.MakeInParams("@NCB_Amount", SqlDbType.Decimal,0, objIVehicleView.VehicleInsurancePremiumView.NCBAmount),
           objDAL.MakeInParams("@Net_Premium",SqlDbType.Decimal , 0,objIVehicleView.VehicleInsurancePremiumView.NetPremium),
           objDAL.MakeInParams("@Service_Tax_Percentage",SqlDbType.Decimal,0,objIVehicleView.VehicleInsurancePremiumView.ServiceTaxPercentage),
           objDAL.MakeInParams("@Service_Tax_Amount", SqlDbType.Decimal, 0,objIVehicleView.VehicleInsurancePremiumView.ServiceTaxAmount), 
           objDAL.MakeInParams("@Net_Payable", SqlDbType.Decimal,0, objIVehicleView.VehicleInsurancePremiumView.NetPayable),
           objDAL.MakeInParams("@Is_Cheque", SqlDbType.Bit,0, objIVehicleView.VehicleInsurancePremiumView.Is_Cheque),                                    //150
           objDAL.MakeInParams("@Cheque_No", SqlDbType.NVarChar,50,objIVehicleView.VehicleInsurancePremiumView.ChequeNo),
           objDAL.MakeInParams("@Cheque_Date", SqlDbType.DateTime,0, objIVehicleView.VehicleInsurancePremiumView.ChequeDate),
           objDAL.MakeInParams("@Bank_ID", SqlDbType.Int,0, objIVehicleView.VehicleInsurancePremiumView.Bank_ID),
           objDAL.MakeInParams("@Insurance_Premium_Details", SqlDbType.Xml,0, objIVehicleView.VehicleInsurancePremiumView.InsurancePremiumDetailsXML),
           objDAL.MakeInParams("@DeducteeTypeID", SqlDbType.Int,0,objIVehicleView.VehicleInformationView.TDSAppView.DeducteeTypeID),
           objDAL.MakeInParams("@IsLowerNoDeduction", SqlDbType.Bit,0,objIVehicleView.VehicleInformationView.TDSAppView.IsLower),
           objDAL.MakeInParams("@IsIgnoreExemption", SqlDbType.Bit,1,objIVehicleView.VehicleInformationView.TDSAppView.IsIgnore),
           objDAL.MakeInParams("@SectionNo", SqlDbType.NVarChar,15,objIVehicleView.VehicleInformationView.TDSAppView.sectionNo),
           objDAL.MakeInParams("@LowerRate", SqlDbType.Decimal,0,objIVehicleView.VehicleInformationView.TDSAppView.LowerRate),
           objDAL.MakeInParams("@DriverMobile1", SqlDbType.VarChar, 20,objIVehicleView.VehicleInformationView.DriverMobile1),
           objDAL.MakeInParams("@DriverMobile2", SqlDbType.VarChar, 20,objIVehicleView.VehicleInformationView.DriverMobile2)};

            //string VehicleSpecificationDetailsXML = objIVehicleView.EngineBodySpecificationView.VehicleSpecificationDetailsXML;
            //string VehicleLoanDetailsXML = objIVehicleView.VehicleLoanDetailsView.VehicleLoanDetailsXML;
            //string VehicleTyreConfigurationDetailsXML = objIVehicleView.VehicleChasisTyresView.VehicleTyreConfigurationDetailsXML;
            //string VehiclePermitDetailsXML = objIVehicleView.RegistrationPermitView.VehiclePermitTaxDetailsXML;
            //string VehicleTemporaryPermitDetailsXML = objIVehicleView.RegistrationPermitView.VehicleTemporaryPermitDetailsXML;
            //string ImagesXML = objIVehicleView.AttachmentsView.AttachmentsXML;

            objDAL.RunProc("rstil7.EF_Master_Vehicle_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIVehicleView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIVehicleView.CallFrom == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];

                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Master/Vehicle/FrmVehicle.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIVehicleView.CallFrom == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }

            return objMessage;
        }

    }
}