
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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;

using ClassLibraryMVP; 

/// <summary>
/// Summary description for ShortGCModel
/// </summary>

namespace Raj.EC.OperationModel
{
    public class ShortGCModel : IModel
    {
        private IShortGCView objIShortGCView;
        private DataSet _ds;
        private DAL objDAL = new DAL();

        public ShortGCModel(IShortGCView ShortGCView)
        {
            objIShortGCView = ShortGCView;
        }
        
        public DataSet ReadValues()
        {
            return _ds;
        }

        public DataSet Read_GC_Details(Boolean Is_Attached_Gc)
        {
            if (Is_Attached_Gc && Common.GetMenuItemId() != 194)
            {
                Boolean Is_Attached;
                Boolean Is_Rebook;
                Boolean Is_Copy;

                if (Common.GetMenuItemId() == 184)
                {
                    Is_Attached = false;
                    Is_Rebook = true;
                    Is_Copy = false;
                }
                else
                {
                    Is_Attached = true;
                    Is_Rebook = false;
                    Is_Copy = true;
                }
                
                SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Attached_GC_No_For_Print", SqlDbType.VarChar  , 0, objIShortGCView.Attached_GC_No_For_Print  ),
                                              objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode   ),
                                              objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId) ,
                                              objDAL.MakeInParams("@Is_Attached",SqlDbType.Bit  ,0,Is_Attached  ) ,
                                              objDAL.MakeInParams("@Is_Rebook",SqlDbType.Bit  ,0,Is_Rebook  ) ,
                                              objDAL.MakeInParams("@Is_Copy",SqlDbType.Bit  ,0,Is_Copy) ,
                                              objDAL.MakeInParams("@Division_Id",SqlDbType.Int  ,0, UserManager.getUserParam().DivisionId  ),
                                              objDAL.MakeInParams("@Document_Id",SqlDbType.Int  ,0, objIShortGCView.DocumentId  ) };

                objDAL.RunProc("EC_Opr_Attached_GC_ReadValues", objSqlParam, ref _ds);
            }
            else
            {
                SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_ID", SqlDbType.Int, 0, objIShortGCView.keyID),
                                              objDAL.MakeInParams("@GC_No_For_Print", SqlDbType.VarChar  , 0, objIShortGCView.Attached_GC_No_For_Print  ),
                                              objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode   ),
                                              objDAL.MakeInParams("@Division_Id",SqlDbType.Int  ,0, UserManager.getUserParam().DivisionId  ),
                                              objDAL.MakeInParams("@Menu_Item_Id",SqlDbType.Int  ,0, Common.GetMenuItemId() )};

                objDAL.RunProc("EC_Opr_GC_ReadValues", objSqlParam, ref _ds);
            }
            return _ds;              
        }

        public DataSet Get_Attached_GC_Details (Int32 GC_Id)
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_ID", SqlDbType.Int, 0, GC_Id) };
            objDAL.RunProc("EC_Opr_GC_ReadValues", objSqlParam, ref _ds);           
            return _ds;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            Boolean Is_Short_GC;
            if (Common.GetMenuItemId() == 213)
                Is_Short_GC = true;
            else
                Is_Short_GC = false ;


            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),                
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Is_Centralised",SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Centralised_Booking_Branch_Id",SqlDbType.Int,0,objIShortGCView.BookingBranchId),
            objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0, objIShortGCView.DocumentSeriesAllocationId),
            objDAL.MakeInParams("@GC_Id",SqlDbType.Int,0, objIShortGCView.keyID),
            objDAL.MakeInParams("@GC_No",SqlDbType.Int,0,Util.String2Int(objIShortGCView.GC_No)),
            objDAL.MakeInParams("@GC_No_For_Print",SqlDbType.VarChar,20,objIShortGCView.GC_No_For_Print),
            objDAL.MakeInParams("@Private_Mark",SqlDbType.VarChar,20,objIShortGCView.PrivateMark),
            objDAL.MakeInParams("@Is_Attached",SqlDbType.Bit,0, objIShortGCView.Is_Attached),
            objDAL.MakeInParams("@Attached_GC_Id",SqlDbType.Int,0, objIShortGCView.Attached_GC_Id),
            objDAL.MakeInParams("@Is_ReBooked",SqlDbType.Bit  ,0, objIShortGCView.Is_ReBooked),
            objDAL.MakeInParams("@ReBook_GC_Id",SqlDbType.Int,0, objIShortGCView.ReBook_GC_Id),
            objDAL.MakeInParams("@ReBook_Against_GC_Id",SqlDbType.Int,0, objIShortGCView.ReBook_GC_Id ),
            objDAL.MakeInParams("@New_ReBook_GC_Id",SqlDbType.Int,0, 0 ),
            objDAL.MakeInParams("@VA_Id",SqlDbType.Int,0, objIShortGCView.VAId ),
            objDAL.MakeInParams("@Pickup_Type_Id",SqlDbType.Int,0, objIShortGCView.PickupTypeId),
            objDAL.MakeInParams("@GC_Date",SqlDbType.DateTime,0, objIShortGCView.BookingDate),
            objDAL.MakeInParams("@GC_Time",SqlDbType.VarChar,8,objIShortGCView.BookingTime),
            objDAL.MakeInParams("@Committed_Del_Date",SqlDbType.DateTime,0,  objIShortGCView.ExpectedDeliveryDate  ),
            objDAL.MakeInParams("@Consignment_Type_Id",SqlDbType.Int,0,objIShortGCView.ConsignmentTypeId),
            objDAL.MakeInParams("@Booking_Mode_Id",SqlDbType.Int,0,objIShortGCView.BookingModeId),
            objDAL.MakeInParams("@Booking_Type_Id",SqlDbType.Int,0, objIShortGCView.BookingTypeId),
            objDAL.MakeInParams("@Booking_Sub_Type_Id",SqlDbType.Int,0, objIShortGCView.BookingSubTypeId),
            objDAL.MakeInParams("@Road_Permit_Type_Id",SqlDbType.Int,0, objIShortGCView.RoadPermitTypeId),
            objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,0,objIShortGCView.PaymentTypeId ),
            objDAL.MakeInParams("@Delivery_Type_Id",SqlDbType.Int,0,objIShortGCView.DeliveryTypeId),
            objDAL.MakeInParams("@Door_Delivery_Against_ID",SqlDbType.Int,0,objIShortGCView.DeliveryAgainstId),
            objDAL.MakeInParams("@From_Branch_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@From_Location_ID",SqlDbType.Int,0, objIShortGCView.FromLocationId),
            objDAL.MakeInParams("@To_Location_ID",SqlDbType.Int,0,objIShortGCView.ToLocationId ),
            objDAL.MakeInParams("@Delivery_Branch_Id",SqlDbType.Int,0,objIShortGCView.DeliveryBaranchId  ),
            objDAL.MakeInParams("@Vehicle_Type_Id",SqlDbType.Int,0,objIShortGCView.VehicleTypeId  ),
            objDAL.MakeInParams("@Vehicle_No",SqlDbType.NVarChar,40,objIShortGCView.VehicleNo    ),
            objDAL.MakeInParams("@STM_No",SqlDbType.NVarChar,50,objIShortGCView.STMNo ),
            objDAL.MakeInParams("@Feasibility_Route_Survey_No",SqlDbType.NVarChar,50,objIShortGCView.FeasibilityRouteSurveyNo ),

            objDAL.MakeInParams("@Consignee_Client_ID",SqlDbType.Int,0,objIShortGCView.ConsigneeId  ),
            objDAL.MakeInParams("@Consignee_Name",SqlDbType.VarChar,100,objIShortGCView.ConsigneeName   ),
            objDAL.MakeInParams("@Consignee_Add1",SqlDbType.VarChar,250,objIShortGCView.ConsigneeAddressLine1  ),
            objDAL.MakeInParams("@Consignee_Add2",SqlDbType.VarChar,250,objIShortGCView.ConsigneeAddressLine2 ),
            objDAL.MakeInParams("@Consignee_City_ID",SqlDbType.Int,0, objIShortGCView.ConsigneeCityId  ) , //objIShortGCView.ConsigneeCity   ),
            objDAL.MakeInParams("@Consignee_City",SqlDbType.VarChar,50,objIShortGCView.ConsigneeCity  ),
            objDAL.MakeInParams("@Consignee_Pin_Code",SqlDbType.VarChar,20,objIShortGCView.ConsigneePinCode ),
            objDAL.MakeInParams("@Consignee_Country_Id",SqlDbType.Int,0,objIShortGCView.ConsigneeCountryId ),
            objDAL.MakeInParams("@Consignee_Country",SqlDbType.VarChar,50,objIShortGCView.ConsigneeCountryName  ),
            objDAL.MakeInParams("@Consignee_State_ID",SqlDbType.Int,0,objIShortGCView.ConsigneeStateId  ),
            objDAL.MakeInParams("@Consignee_State",SqlDbType.VarChar,50,objIShortGCView.ConsigneeStateName ),
            objDAL.MakeInParams("@Consignee_Tel_No",SqlDbType.VarChar,25,objIShortGCView.ConsigneeTelNo ),
            objDAL.MakeInParams("@Consignee_Mobile_No",SqlDbType.VarChar,25,objIShortGCView.ConsigneeMobileNo ),
            objDAL.MakeInParams("@Consignee_EMail",SqlDbType.VarChar,50,objIShortGCView.ConsigneeEmail ),
            objDAL.MakeInParams("@Consignee_CST_TIN_No",SqlDbType.VarChar,50,objIShortGCView.ConsigneeCSTNo ),
            objDAL.MakeInParams("@Is_Consignee_Service_Tax_Applicable",SqlDbType.Int ,0,objIShortGCView.Is_ServiceTaxApplicableForConsignee  ),            

            objDAL.MakeInParams("@Consignor_Client_ID",SqlDbType.Int,0,objIShortGCView.ConsignorId ),
            objDAL.MakeInParams("@Consignor_Name",SqlDbType.VarChar,100,objIShortGCView.ConsignorName ),
            objDAL.MakeInParams("@Consignor_Add1",SqlDbType.VarChar,250,objIShortGCView.ConsignorAddressLine1 ),
            objDAL.MakeInParams("@Consignor_Add2",SqlDbType.VarChar,250,objIShortGCView.ConsignorAddressLine2 ),
            objDAL.MakeInParams("@Consignor_City_ID",SqlDbType.Int,0,objIShortGCView.ConsignorCityId  ) ,//objIShortGCView.ConsignorCity ),
            objDAL.MakeInParams("@Consignor_City",SqlDbType.VarChar,50,objIShortGCView.ConsignorCity ),
            objDAL.MakeInParams("@Consignor_Pin_Code",SqlDbType.VarChar,20,objIShortGCView.ConsignorPinCode  ),
            objDAL.MakeInParams("@Consignor_Country_ID",SqlDbType.Int,0, objIShortGCView.ConsignorCountryId  ),
            objDAL.MakeInParams("@Consignor_Country",SqlDbType.VarChar,50,objIShortGCView.ConsignorCountryName ),
            objDAL.MakeInParams("@Consignor_State_ID",SqlDbType.Int,0,objIShortGCView.ConsignorStateId  ),
            objDAL.MakeInParams("@Consignor_State",SqlDbType.VarChar,50,objIShortGCView.ConsignorStateName ),
            objDAL.MakeInParams("@Consignor_Tel_No",SqlDbType.VarChar,25,objIShortGCView.ConsignorTelNo ),
            objDAL.MakeInParams("@Consignor_Mobile_No",SqlDbType.VarChar,25,objIShortGCView.ConsignorMobileNo ),
            objDAL.MakeInParams("@Consignor_EMail",SqlDbType.VarChar,50,objIShortGCView.ConsignorEmail ),
            objDAL.MakeInParams("@Consignor_CST_TIN_No",SqlDbType.VarChar,50,objIShortGCView.ConsignorCSTNo ),
            objDAL.MakeInParams("@Is_Consignor_Service_Tax_Applicable",SqlDbType.Int,0,objIShortGCView.Is_ServiceTaxApplicableForConsignor   ),

            objDAL.MakeInParams("@DD_Address_1",SqlDbType.VarChar,100,objIShortGCView.Session_ConsigneeAddressLine1    ),
            objDAL.MakeInParams("@DD_Address_2",SqlDbType.VarChar,100,objIShortGCView.Session_ConsigneeAddressLine2   ),
            objDAL.MakeInParams("@Acknowledge",SqlDbType.Bit,0, objIShortGCView.Is_POD),
            objDAL.MakeInParams("@Total_Articles",SqlDbType.Int,0,objIShortGCView.TotalArticles   ),
            objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,objIShortGCView.TotalWeight  ),
            objDAL.MakeInParams("@Charged_Weight",SqlDbType.Decimal,0,objIShortGCView.ChargeWeight ),
            objDAL.MakeInParams("@Total_Invoice_Value",SqlDbType.Decimal,0,objIShortGCView.TotalInvoiceAmount ),
            objDAL.MakeInParams("@Is_DACC",SqlDbType.Bit,0,objIShortGCView.Is_DACC  ),
            objDAL.MakeInParams("@Freight_Rate",SqlDbType.Decimal,0,objIShortGCView.FreightRate),
            objDAL.MakeInParams("@Freight_Amt",SqlDbType.Decimal,0,objIShortGCView.Freight ),
            objDAL.MakeInParams("@Local_Charges",SqlDbType.Decimal,0,objIShortGCView.LocalCharge  ),
            objDAL.MakeInParams("@Bilti_Charges",SqlDbType.Decimal,0,objIShortGCView.StationaryCharge  ),
            objDAL.MakeInParams("@Hamali_Per_Kg",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_HamaliPerKg   ),
            objDAL.MakeInParams("@Hamali_Charges",SqlDbType.Decimal,0,objIShortGCView.LoadingCharge  ),
            objDAL.MakeInParams("@DD_Charges",SqlDbType.Decimal,0,objIShortGCView.DDCharge  ),
            objDAL.MakeInParams("@TP_Charges",SqlDbType.Decimal,0,objIShortGCView.ToPayCharge  ),
            objDAL.MakeInParams("@Other_Charges",SqlDbType.Decimal,0, objIShortGCView.OtherCharges ),
            objDAL.MakeInParams("@Tax_Abate_Percent",SqlDbType.Decimal,0,objIShortGCView.TaxAbatePercent  ),
            objDAL.MakeInParams("@Tax_Abate",SqlDbType.Decimal,0,objIShortGCView.Abatment  ),
            objDAL.MakeInParams("@Amt_Taxable",SqlDbType.Decimal,0,objIShortGCView.TaxableAmount ),
            objDAL.MakeInParams("@FOVPercent",SqlDbType.Decimal,0, objIShortGCView.Applicable_Standard_FOVPercentage ),
            objDAL.MakeInParams("@FOV",SqlDbType.Decimal,0,objIShortGCView.FOVRiskCharge ),
            objDAL.MakeInParams("@ODA_Charges",SqlDbType.Decimal,0,objIShortGCView.DDCharge ),
            objDAL.MakeInParams("@Oda_Charges_UpTo_500_Kg",SqlDbType.Decimal,0,objIShortGCView.ODAChargesUpTo500Kg  ),
            objDAL.MakeInParams("@Oda_Charges_Above_500_Kg",SqlDbType.Decimal,0,objIShortGCView.ODAChargesAbove500Kg ),
            objDAL.MakeInParams("@ReBook_Charges",SqlDbType.Decimal,0,objIShortGCView.ReBookGC_Amount  ),
            objDAL.MakeInParams("@ReBook_GC_Sub_Total",SqlDbType.Decimal,0,objIShortGCView.ReBookGC_SubTotal    ), 
            objDAL.MakeInParams("@Sub_Total",SqlDbType.Decimal,0,objIShortGCView.SubTotal ),
            objDAL.MakeInParams("@Advance_Amount",SqlDbType.Decimal,0,objIShortGCView.Advance ),
            objDAL.MakeInParams("@Service_Tax_Percent",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_ServiceTaxPercent ),// 12.36),//objIShortGCView.ServiceTaxpercent ),
            objDAL.MakeInParams("@Service_Tax_Amount",SqlDbType.Decimal,0,objIShortGCView.ServiceTax ),
            objDAL.MakeInParams("@Total_GC_Amount",SqlDbType.Decimal,0,objIShortGCView.TotalGCAmount ),
            objDAL.MakeInParams("@Std_Freight_Rate",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_FreightRate ),
            objDAL.MakeInParams("@Std_Freight_Amt",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_FreightAmount ),
            objDAL.MakeInParams("@Std_Local_Charge_Rate",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_LocalCharge_Rate ),
            objDAL.MakeInParams("@Std_Local_Charge",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_LocalCharge ),
            objDAL.MakeInParams("@Std_Hamali_Charge",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_HamaliCharge ),
            objDAL.MakeInParams("@Std_DD_Charge",SqlDbType.Decimal,0, objIShortGCView.Applicable_Standard_DDCharge  ),
            objDAL.MakeInParams("@Std_DD_Charge_Rate",SqlDbType.Decimal,0, objIShortGCView.Applicable_Standard_DDCharge_Rate ),
            objDAL.MakeInParams("@Std_Bilti_Charges",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_BiltiCharges ),
            objDAL.MakeInParams("@Std_Service_Tax_Amount",SqlDbType.Decimal,0, objIShortGCView.Applicable_Standard_ServiceTaxAmount ),
            objDAL.MakeInParams("@Std_FOV",SqlDbType.Decimal,0, objIShortGCView.Applicable_Standard_FOV  ),
            objDAL.MakeInParams("@Std_TP_Charges",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_ToPayCharges ),
            objDAL.MakeInParams("@Std_CFT_Factor",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_CFTFactor   ),
                
            objDAL.MakeInParams("@Std_Octroi_Form_Charges",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_Octroi_Form_Charge  ),
            objDAL.MakeInParams("@Std_Octroi_Service_Charges",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_Octroi_Service_Charge  ),
            objDAL.MakeInParams("@Std_Demurrage_Days",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_Demurrage_Days  ),
            objDAL.MakeInParams("@Std_Demurrage_Rate",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_Demurrage_Rate  ),
            objDAL.MakeInParams("@Std_GI_Charges",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_GI_Charges  ),
                
            objDAL.MakeInParams("@Is_Cheque",SqlDbType.Bit,0,objIShortGCView.Is_Cheque  ),
            objDAL.MakeInParams("@Cheque_No",SqlDbType.Int,0,objIShortGCView.ChequeNo  ),
            objDAL.MakeInParams("@Cheque_Date",SqlDbType.DateTime,0, objIShortGCView.ChequeDate  ),
            objDAL.MakeInParams("@Bank_Name",SqlDbType.VarChar,100,objIShortGCView.BankName  ),
            objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,objIShortGCView.CashAmount ),
            objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,objIShortGCView.ChequeAmount ),
            objDAL.MakeInParams("@Tax_Payable_By",SqlDbType.Int ,0,objIShortGCView.ServiceTaxPayableBy ),
            objDAL.MakeInParams("@Contract_Branch_ID",SqlDbType.Int,0,objIShortGCView.Contract_BranchId  ),
            objDAL.MakeInParams("@Contract_ID",SqlDbType.Int,0,objIShortGCView.ContractId ),
            objDAL.MakeInParams("@Billing_Client_ID",SqlDbType.Int,0,objIShortGCView.BillingPartyId  ),
            objDAL.MakeInParams("@Billing_Party_Ledger_Id",SqlDbType.Int,0,objIShortGCView.Billing_Party_Ledger_Id  ),
            objDAL.MakeInParams("@Billing_Branch_Id",SqlDbType.Int,0,objIShortGCView.BillingBranchId  ),
                 
            objDAL.MakeInParams("@Billing_Remarks",SqlDbType.VarChar,50,objIShortGCView.BillingRemark  ),
            objDAL.MakeInParams("@Risk_Type_ID",SqlDbType.Int,0,objIShortGCView.GCRiskId  ),
            objDAL.MakeInParams("@Insurance_Company",SqlDbType.VarChar,100,objIShortGCView.Session_InsuranceCompany   ),// objIShortGCView.InsuranceCompany ),
            objDAL.MakeInParams("@Policy_No",SqlDbType.NVarChar,50,objIShortGCView.Session_PolicyNo ),
            objDAL.MakeInParams("@Policy_Exp_Date",SqlDbType.DateTime,0,objIShortGCView.Session_PolicyExpDate   ),
            objDAL.MakeInParams("@Policy_Amount",SqlDbType.Decimal,0,objIShortGCView.Session_PolicyAmount ),
            objDAL.MakeInParams("@Risk_Amount",SqlDbType.Decimal,0,objIShortGCView.Session_RiskAmount ),
            objDAL.MakeInParams("@Freight_Basis_ID",SqlDbType.Int,0,objIShortGCView.FreightBasisId  ),
            objDAL.MakeInParams("@Volumetric_Freight_Unit_ID",SqlDbType.Int,0,objIShortGCView.VolumetricFreightUnitId  ),
            objDAL.MakeInParams("@Unit_Of_Measurement_ID",SqlDbType.Int,0,objIShortGCView.UnitOfMeasurementId  ),
            objDAL.MakeInParams("@Total_Length",SqlDbType.Decimal,0, objIShortGCView.UnitOfMeasurmentLength    ),
            objDAL.MakeInParams("@Total_Width",SqlDbType.Decimal,0,objIShortGCView.UnitOfMeasurmentWidth   ),
            objDAL.MakeInParams("@Total_Height",SqlDbType.Decimal,0,objIShortGCView.UnitOfMeasurmentHeight  ),

            objDAL.MakeInParams("@Total_Length_In_Feet",SqlDbType.Decimal,0, objIShortGCView.LengthInFeet  ),
            objDAL.MakeInParams("@Total_Width_In_Feet",SqlDbType.Decimal,0,objIShortGCView.WidthInFeet  ),
            objDAL.MakeInParams("@Total_Height_In_Feet",SqlDbType.Decimal,0,objIShortGCView.HeightInFeet ),

            objDAL.MakeInParams("@CFT_Factor",SqlDbType.Decimal,0,objIShortGCView.VolumetricToKgFactor  ),
            objDAL.MakeInParams("@Total_CFT",SqlDbType.Decimal,0,objIShortGCView.TotalCFT  ),
            objDAL.MakeInParams("@Total_CBM",SqlDbType.Decimal,0,objIShortGCView.TotalCBM  ),
            objDAL.MakeInParams("@Customer_Ref_No",SqlDbType.VarChar,20,objIShortGCView.CustomerRefNo ),
            objDAL.MakeInParams("@GC_Remarks",SqlDbType.VarChar,250,objIShortGCView.InstructionRemark  ),
            objDAL.MakeInParams("@GC_Remarks_Other_Charges",SqlDbType.VarChar,100, objIShortGCView.OtherChargesRemark  ),
            objDAL.MakeInParams("@Enclosures",SqlDbType.VarChar,150,objIShortGCView.Enclosures  ),
            objDAL.MakeInParams("@Is_Billed",SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Is_Octroi_Applicable",SqlDbType.Bit,0,objIShortGCView.Is_OctroiApplicable    ),
            objDAL.MakeInParams("@Is_ODA",SqlDbType.Bit,0,objIShortGCView.Is_ODA  ),
            objDAL.MakeInParams("@Oct_Form_Type",SqlDbType.VarChar,1,0),
            objDAL.MakeInParams("@Oct_Type_Id",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Oct_Bill_No",SqlDbType.NVarChar,30,0),
            objDAL.MakeInParams("@Oct_Bill_Date",SqlDbType.DateTime,0,DateTime.Now ),
            objDAL.MakeInParams("@Oct_Receipt_No",SqlDbType.VarChar,15,0),
            objDAL.MakeInParams("@Oct_Amount",SqlDbType.Decimal,0,0),
            objDAL.MakeInParams("@Oct_Remark",SqlDbType.VarChar,200,0),
            objDAL.MakeInParams("@Is_Consignor_Regular_Client",SqlDbType.Int,0,objIShortGCView.Is_RegularConsignor   ),
            objDAL.MakeInParams("@Is_Consignee_Regular_Client",SqlDbType.Int,0,objIShortGCView.Is_RegularConsignee ),
            objDAL.MakeInParams("@Loading_Supervisor_ID",SqlDbType.Int,0,objIShortGCView.LoadingSuperVisorId ),
            objDAL.MakeInParams("@Marketing_Executive_ID",SqlDbType.Int,0,objIShortGCView.MarketingExecutiveId ),
            objDAL.MakeInParams("@Status_Id",SqlDbType.Int,0,0), // Booking
            objDAL.MakeInParams("@Is_Cancelled",SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Created_On",SqlDbType.DateTime,0,DateTime.Now ),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  ),
            objDAL.MakeInParams("@Updated_On",SqlDbType.DateTime,0,DateTime.Now ),
            objDAL.MakeInParams("@Updated_By",SqlDbType.Int,0,UserManager.getUserParam().UserId ),
            objDAL.MakeInParams("@Invoice_Xml",SqlDbType.Xml  ,0,objIShortGCView.InvoiceXml),
            objDAL.MakeInParams("@Multiple_Commodity_Xml",SqlDbType.Xml  ,0,objIShortGCView.MultipleCommodityXml),
            objDAL.MakeInParams("@Other_Charges_Xml",SqlDbType.Xml  ,0,objIShortGCView.OtherChargesXml),
            objDAL.MakeInParams("@Billing_Details_Xml",SqlDbType.Xml  ,0,objIShortGCView.BillingDetailsXml   ),
            objDAL.MakeInParams("@Is_Multiple_Billing",SqlDbType.Bit ,0,objIShortGCView.Is_MultipleBilling  ),
            objDAL.MakeInParams("@DACC_Charges",SqlDbType.Decimal  ,0,objIShortGCView.DACCCharges  ),
            objDAL.MakeInParams("@Std_DACC_Charge",SqlDbType.Decimal ,0,objIShortGCView.Applicable_Standard_DACCCharges),

            objDAL.MakeInParams("@Previous_Article_ID",SqlDbType.Int,0, objIShortGCView.Previous_Article_ID   ),
            objDAL.MakeInParams("@Previous_Status_ID",SqlDbType.Int,0,   objIShortGCView.Previous_Status_ID ),
            objDAL.MakeInParams("@Previous_Document_ID",SqlDbType.Int,0,   objIShortGCView.Previous_Document_ID ),
            objDAL.MakeInParams("@Previous_Document_No_For_Print",SqlDbType.VarChar ,0,  objIShortGCView.Previous_Document_No_For_Print  ),
            objDAL.MakeInParams("@Previous_Document_Date",SqlDbType.DateTime ,0,  objIShortGCView.Previous_Document_Date  ),
            objDAL.MakeInParams("@Is_SignedByConsignor",SqlDbType.Bit,0, objIShortGCView.Is_SignedByConsignor  ),
            objDAL.MakeInParams("@Length_Charge_Head_Id",SqlDbType.Int  ,0, objIShortGCView.LengthChargeHeadId    ),
            objDAL.MakeInParams("@Length_Charge",SqlDbType.Decimal ,0, objIShortGCView.LengthCharge    ),
            objDAL.MakeInParams("@Unloading_Charge",SqlDbType.Decimal ,0, objIShortGCView.UnloadingCharge    ),
            objDAL.MakeInParams("@ReBook_Octroi_Amount",SqlDbType.Decimal ,0, objIShortGCView.ReBookGC_OctroiAmount  ),
            objDAL.MakeInParams("@ReBook_GC_Octroi_Paid_By_ID",SqlDbType.Int  ,0, objIShortGCView.ReBook_GCOctroiPaidByID  ),
            objDAL.MakeInParams("@Document_Id",SqlDbType.Int  ,0, objIShortGCView.DocumentId  ),
            objDAL.MakeInParams("@Road_Permit_SrNo",SqlDbType.VarChar ,0, objIShortGCView.RoadPermitSrNo ),
            objDAL.MakeInParams("@Is_Insured",SqlDbType.Bit  ,0, objIShortGCView.Is_Insured   ),
            objDAL.MakeInParams("@Hamali_Per_Articles",SqlDbType.Decimal,0,objIShortGCView.Applicable_Standard_HamaliPerArticles  ),
            objDAL.MakeInParams("@Billing_Party_Credit_Limit",SqlDbType.Decimal,0,objIShortGCView.Billing_Party_Credit_Limit  ),
            objDAL.MakeInParams("@Is_Opening_GC",SqlDbType.Bit,0,objIShortGCView.Is_Opening_GC  ),                
            objDAL.MakeInParams("@Booking_Branch_Id",SqlDbType.Int  ,0,objIShortGCView.BookingBranchId  ),
            objDAL.MakeInParams("@Arrived_From_Branch_Id",SqlDbType.Int  ,0,objIShortGCView.ArrivedFromBranchId  ),
            objDAL.MakeInParams("@Arrived_Date",SqlDbType.DateTime  ,0,objIShortGCView.ArrivedDate  ),
            objDAL.MakeInParams("@Menu_Item_Id",SqlDbType.Int  ,0, Common.GetMenuItemId() ),
            objDAL.MakeInParams("@Is_Auto_Booking_MR_For_Paid_Booking",SqlDbType.Bit  ,0, objIShortGCView.Is_Auto_Booking_MR_For_Paid_Booking),
            objDAL.MakeInParams("@Default_Cash_Ledger_Id",SqlDbType.Int   ,0, objIShortGCView.Default_Cash_Ledger_Id  ),
            objDAL.MakeInParams("@Cheque_Details_Xml",SqlDbType.Xml  ,0,objIShortGCView.ChequeDetailsXml),
            objDAL.MakeInParams("@Is_Short_GC",SqlDbType.Bit  ,0, Is_Short_GC),

            objDAL.MakeInParams("@Container_Type_Id",SqlDbType.Int   ,0, objIShortGCView.Session_ContainerTypeId  ),
            objDAL.MakeInParams("@ContainerNo1",SqlDbType.VarChar  ,0, objIShortGCView.Session_ContainerNoPart1 ),
            objDAL.MakeInParams("@ContainerNo2",SqlDbType.VarChar    ,0, objIShortGCView.Session_ContainerNoPart2),
            objDAL.MakeInParams("@SealNo",SqlDbType.VarChar    ,0, objIShortGCView.Session_SealNo  ),
            objDAL.MakeInParams("@Return_To_Yard_Id",SqlDbType.Int,0, objIShortGCView.Session_ReturnToYardId  ),
            objDAL.MakeInParams("@NFormNo",SqlDbType.VarChar,0, objIShortGCView.Session_NFormNo  ),
            objDAL.MakeInParams("@NForm_Charge",SqlDbType.Decimal ,0, objIShortGCView.NFormCharge),
            objDAL.MakeInParams("@Std_NForm_Charge",SqlDbType.Decimal ,0, objIShortGCView.Applicable_Standard_NForm_Charge  )};
  
            if (Common.GetMenuItemId() == 194)
                objDAL.RunProc("EC_Opr_Rectification_GC_Save", objSqlParam);
            else
                objDAL.RunProc("EC_Opr_GC_Save", objSqlParam);
                         
            objMessage.messageID = Util.String2Int(objSqlParam[0].Value.ToString());
            objMessage.message = objSqlParam[1].Value.ToString();
             
            if (objMessage.messageID == 0)
            {
                objIShortGCView.GC_Id = Convert.ToInt32(objSqlParam[2].Value);
                string _Msg;
                _Msg = "Saved SuccessFully";
            }
            else
            {
                Common.DisplayErrors(objMessage.messageID);
            }
            
            return objMessage;
        }

        public DataSet Fill_Values()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId) };

            objDAL.RunProc("EC_Opr_GC_FillValues", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_ToLocationDetails()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@From_Location_Id", SqlDbType.Int, 0, objIShortGCView.FromLocationId  ) ,
                                          objDAL.MakeInParams("@Vehicle_Type_Id", SqlDbType.Int, 0, objIShortGCView.VehicleTypeId  )  ,
                                          objDAL.MakeInParams("@To_Location_Id", SqlDbType.Int, 0, objIShortGCView.ToLocationId) };

            objDAL.RunProc("Ec_Opr_GC_Get_ToLocation_Details", objSqlParam, ref _ds);
            return _ds;
        }

        //public void Get_Applicable_Service_Tax()
        //{
        //    SqlParameter[] objSqlParam ={ 
        //                                objDAL.MakeOutParams("@Applicable_Service_Tax_Percent", SqlDbType.Float  , 2 ),
        //                                objDAL.MakeInParams("@GC_Date", SqlDbType.DateTime , 0, objIShortGCView.BookingDate)};

        //    objDAL.RunProc("Ec_Opr_GC_Get_Applicable_Service_Tax", objSqlParam, ref _ds);

        //    objIShortGCView.Standard_ServiceTaxPercent  = Util.String2Decimal  (objSqlParam[0].Value.ToString()) ;
        //    objIShortGCView.Applicable_Standard_ServiceTaxPercent   = Util.String2Decimal(objSqlParam[0].Value.ToString());
        //}

        public DataSet Get_BranchRateParameter()
        {
            Int32 Branch_Id;

            if (objIShortGCView.BookingBranchId <= 0)
            {
                Branch_Id = UserManager.getUserParam().MainId;
            }
            else
            {
                Branch_Id = objIShortGCView.BookingBranchId;
            }

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, Branch_Id ) }; //UserManager.getUserParam().MainId  ) };

            objDAL.RunProc("Ec_Opr_GC_Get_Branch_Rate_Parameter", objSqlParam, ref _ds);
            return _ds;
        }

        public void  Get_DocumentAllocationDetails()
        {
            SqlParameter[] objSqlParam ={  
                                    objDAL.MakeOutParams("@Document_Allocation_Id", SqlDbType.Int, 0 ),
                                    objDAL.MakeOutParams("@Start_No", SqlDbType.Int, 0 ),
                                    objDAL.MakeOutParams("@End_No", SqlDbType.Int, 0 ),
                                    objDAL.MakeOutParams("@Next_No", SqlDbType.Int, 0 ),
                                    objDAL.MakeInParams("@VA_ID", SqlDbType.Int, 0, objIShortGCView.VAId ),                                  
                                    objDAL.MakeInParams("@Document_Id", SqlDbType.Int, 0, objIShortGCView.DocumentId ),
                                    };//UserManager.getUserParam().MainId  ) };
            
            objDAL.RunProc("Ec_Opr_GC_Get_Document_Allocation_Details", objSqlParam, ref _ds);

            objIShortGCView.DocumentSeriesAllocationId = Util.String2Int(objSqlParam[0].Value.ToString());
            
            objIShortGCView.Start_No = Util.String2Int(objSqlParam[1].Value.ToString());
            objIShortGCView.End_No  = Util.String2Int(objSqlParam[2].Value.ToString());

            if (objIShortGCView.keyID <= 0)
            {
                objIShortGCView.Next_No = Util.String2Int(objSqlParam[3].Value.ToString());
            }
            objIShortGCView.Series = " ( " + Convert.ToInt32(objSqlParam[1].Value.ToString()).ToString("0000000") + " - " + Convert.ToInt32(objSqlParam[2].Value.ToString()).ToString("0000000") + " ) ";

        //  hdn_GC_Start_No.Value = Format(start_GC_No, "0000000")
        //  hdn_GC_End_No.Value = Format(End_GC_No, "0000000")
            
        }

        public DataSet  Get_RequireForms()
        {
            SqlParameter[] objSqlParam ={  objDAL.MakeInParams("@Branch_Id", SqlDbType.Int,0, objIShortGCView.DeliveryBaranchId  )};
            
            objDAL.RunProc("Ec_Opr_GC_Get_Require_Forms", objSqlParam, ref _ds);
            return _ds;
        }

        public Boolean Is_Duplicate()
        {
            Boolean  Is_Duplicate;

            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Is_Duplicate", SqlDbType.Bit , 0 ),
                                          objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, Util.String2Int(objIShortGCView.GC_No)),
                                          objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode   ),
                                          objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,objIShortGCView.keyID  ),
                                          objDAL.MakeInParams("@Is_Opening_GC",SqlDbType.Bit ,0,objIShortGCView.Is_Opening_GC )};

            objDAL.RunProc("Ec_Opr_GC_Check_Duplicate", objSqlParam, ref _ds);

            Is_Duplicate = Convert.ToBoolean(objSqlParam[0].Value.ToString());

            return Is_Duplicate  ;
        }


        public Boolean Validate_Credit_Limit()
        {
            Boolean Is_Valid_Credit_Limit;

            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Is_Valid_Credit_Limit", SqlDbType.Bit , 0 ),
                                          objDAL.MakeOutParams("@Billing_Client_Name", SqlDbType.VarChar  , 100 ),
                                          objDAL.MakeInParams("@Billing_Client_ID",SqlDbType.Int,0,objIShortGCView.BillingPartyId  ),
                                          objDAL.MakeInParams("@Billing_Party_Ledger_Id",SqlDbType.Int,0,objIShortGCView.Billing_Party_Ledger_Id  ),                                           
                                          objDAL.MakeInParams("@Billing_Details_Xml",SqlDbType.Xml  ,0,objIShortGCView.BillingDetailsXml   ),
                                          objDAL.MakeInParams("@Is_Multiple_Billing",SqlDbType.Bit ,0,objIShortGCView.Is_MultipleBilling  ),
                                          objDAL.MakeInParams("@Billing_Party_Credit_Limit",SqlDbType.Decimal,0,objIShortGCView.Billing_Party_Credit_Limit  ),
                                          objDAL.MakeInParams("@Total_GC_Amount",SqlDbType.Decimal,0,objIShortGCView.TotalGCAmount )};

            objDAL.RunProc("Ec_Opr_GC_Validate_Credit_Limit", objSqlParam, ref _ds);

            Is_Valid_Credit_Limit = Convert.ToBoolean(objSqlParam[0].Value.ToString());
            objIShortGCView.In_Valid_Credit_Limit_Client_Name = objSqlParam[1].Value.ToString();

            return Is_Valid_Credit_Limit;
        }


        public Boolean Allow_To_Attached()
        {
            Boolean Is_Allow_To_Attached;

            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Is_Allow_To_Attached", SqlDbType.Bit , 0 ),
                                          objDAL.MakeInParams("@Attached_GC_Id", SqlDbType.Int, 0, objIShortGCView.Attached_GC_Id)};

            objDAL.RunProc("Ec_Opr_GC_Check_Allow_To_Attached", objSqlParam, ref _ds);

            Is_Allow_To_Attached = Convert.ToBoolean(objSqlParam[0].Value.ToString());

            return Is_Allow_To_Attached;
        }

        public Boolean Allow_To_ReBook()
        {
            Boolean Is_Allow_To_ReBook;

            SqlParameter[] objSqlParam ={ 
                                        objDAL.MakeOutParams("@Is_Allow_To_ReBook", SqlDbType.Bit , 0 ),
                                        objDAL.MakeOutParams("@ReBook_GC_Id", SqlDbType.Int  , objIShortGCView.ReBook_GC_Id  ),
                                        objDAL.MakeOutParams("@Is_Octroi_Updated",SqlDbType.Bit,0),
                                        objDAL.MakeOutParams("@Is_Octroi_Applicable",SqlDbType.Bit,0),
                                        objDAL.MakeInParams("@GC_No_For_Print", SqlDbType.VarChar , 0, objIShortGCView.Attached_GC_No_For_Print  ),
                                        objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                                        objDAL.MakeInParams("@year_code",SqlDbType.Int,0,UserManager.getUserParam().YearCode  ),
                                        objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId )};

            objDAL.RunProc("Ec_Opr_GC_Allow_To_ReBook", objSqlParam, ref _ds);

            Is_Allow_To_ReBook = Convert.ToBoolean(objSqlParam[0].Value.ToString());
            //objIShortGCView.ReBook_GC_Id = Convert.ToInt32(objSqlParam[1].Value.ToString());

            objIShortGCView.Is_ReBookGC_Octroi_Updated = Convert.ToBoolean(objSqlParam[2].Value.ToString());
            objIShortGCView.Is_ReBookGC_Octroi_Applicable = Convert.ToBoolean(objSqlParam[3].Value.ToString());

            return Is_Allow_To_ReBook;
        }

        public Boolean Allow_To_Rectify()
        {
            Boolean Is_Allow_To_Rectify;

            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                        objDAL.MakeOutParams("@Is_Allow_To_Rectify", SqlDbType.Bit , 0 ),
                                        objDAL.MakeOutParams("@Rectification_GC_Id", SqlDbType.Int  , 0 ),
                                        objDAL.MakeOutParams("@Is_Octroi_Updated",SqlDbType.Bit,0),
                                        objDAL.MakeOutParams("@Is_Octroi_Applicable",SqlDbType.Bit,0),
                                        objDAL.MakeInParams("@GC_No_For_Print", SqlDbType.VarChar , 20, objIShortGCView.GC_No),
                                        objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId) ,
                                        objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode) ,
                                        objDAL.MakeInParams("@year_code",SqlDbType.Int,0,UserManager.getUserParam().YearCode  ),
                                        objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId )};

            objDAL.RunProc("Ec_Opr_GC_Allow_To_Rectify", objSqlParam, ref _ds);

            objIShortGCView.errorMessage = objSqlParam[0].Value.ToString();
            Is_Allow_To_Rectify = Convert.ToBoolean(objSqlParam[1].Value.ToString());
            //objIShortGCView.Rectification_GC_Id = Convert.ToInt32(objSqlParam[2].Value.ToString());
            
            return Is_Allow_To_Rectify;
        }

        public DataSet Fill_Item(int CommodityId)
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Commodity_Id", SqlDbType.Int, 0, CommodityId) };

            objDAL.RunProc("Ec_Opr_GC_Fill_Item", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Fill_Contract()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Contractual_Client_Id", SqlDbType.Int, 0, objIShortGCView.Contractual_ClientId  ),
                                          objDAL.MakeInParams("@Contract_Branch_ID", SqlDbType.Int, 0, objIShortGCView.Contract_BranchId   ) ,
                                          objDAL.MakeInParams("@GC_Date", SqlDbType.DateTime , 0, objIShortGCView.BookingDate   ) };

            objDAL.RunProc("Ec_Opr_GC_Fill_Contract", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_BillingPartyDetails()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Billing_Party_Id", SqlDbType.Int, 0, objIShortGCView.BillingPartyId    )};

            objDAL.RunProc("Ec_Opr_GC_Get_Billing_Party_Details", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Fill_ContractBranches()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Contractual_Client_Id", SqlDbType.Int, 0, objIShortGCView.Contractual_ClientId  ),                                         
                                         objDAL.MakeInParams("@GC_Date", SqlDbType.DateTime , 0, objIShortGCView.BookingDate   ) };

            objDAL.RunProc("Ec_Opr_GC_Fill_Contract_Branch", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_Contractual_Client_Details()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Contractual_Client_Id", SqlDbType.Int, 0, objIShortGCView.Contractual_ClientId  )};

            objDAL.RunProc("Ec_Opr_GC_Get_Contractual_Client_Details", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_ContractDetails()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Contract_Id", SqlDbType.Int, 0, objIShortGCView.ContractId) };

            objDAL.RunProc("Ec_Opr_GC_Get_Contract_Details", objSqlParam, ref _ds);
            return _ds;
        }
        
        public void Get_VAId()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@VA_Id", SqlDbType.Int, 0 ),
                                        objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId )
                                        };//UserManager.getUserParam().MainId  ) };
            
            objDAL.RunProc("Ec_Opr_GC_Get_VA_Id", objSqlParam, ref _ds);

            objIShortGCView.VAId = Util.String2Int(objSqlParam[0].Value.ToString());            
        }           
            
        public void Get_StdandardFreightRate()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Transit_Days", SqlDbType.Int, 0 ),
                                        objDAL.MakeOutParams("@Distance_In_Km", SqlDbType.Int, 0 ),                                         
                                        objDAL.MakeOutParams("@Normal_Rate", SqlDbType.Float  , 2 ),
                                        objDAL.MakeOutParams("@Special_Rate", SqlDbType. Float , 2 ),
                                        objDAL.MakeOutParams("@FTL_Rate", SqlDbType.Float  , 2 ),
                                        objDAL.MakeInParams("@From_Location_id", SqlDbType.Int, 0, objIShortGCView.FromLocationId ),
                                        objDAL.MakeInParams("@To_Location_id", SqlDbType.Int, 0, objIShortGCView.ToLocationId  ),
                                        objDAL.MakeInParams("@Vehicle_Type_Id", SqlDbType.Int, 0, objIShortGCView.VehicleTypeId   ),
                                        objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId  ),//UserManager.getUserParam().DivisionId),
                                        objDAL.MakeInParams("@Standard_Basic_Freight_Unit_ID", SqlDbType.Int, 0, objIShortGCView.CompanyParameter_Standard_BasicFreightUnitId  )};

            objDAL.RunProc("EC_Opr_GC_Get_Standard_Freight", objSqlParam, ref _ds);

            objIShortGCView.TotalTransitDays = Util.String2Int(objSqlParam[0].Value.ToString());
            objIShortGCView.TotalKiloMeter = Util.String2Int(objSqlParam[1].Value.ToString());
            objIShortGCView.Standard_FreightRate  = Util.String2Decimal  (objSqlParam[2].Value.ToString()) + objIShortGCView.Additional_Freight ;
            objIShortGCView.FreightRate = Util.String2Decimal(objSqlParam[2].Value.ToString()) + objIShortGCView.Additional_Freight;
            objIShortGCView.Special_FreightRate = Util.String2Decimal(objSqlParam[3].Value.ToString()) + objIShortGCView.Additional_Freight; 
            
            // return _ds;
        }

        public void Get_Additional_Freight()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Additional_Freight", SqlDbType.Decimal  , 0 ), 
                                        objDAL.MakeInParams("@Road_Permit_Type_Id", SqlDbType.Int, 0, objIShortGCView.RoadPermitTypeId  )};

            objDAL.RunProc("EC_Opr_GC_Get_Additional_Freight", objSqlParam, ref _ds);

            objIShortGCView.Additional_Freight = Util.String2Decimal  (objSqlParam[0].Value.ToString());
        }

        public void Get_TransitDays()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Transit_Days", SqlDbType.Int, 0 ), 
                                        objDAL.MakeOutParams("@Distance_In_Km", SqlDbType.Int, 0 ), 
                                        objDAL.MakeInParams("@From_Location_id", SqlDbType.Int, 0, objIShortGCView.FromLocationId ),
                                        objDAL.MakeInParams("@To_Location_id", SqlDbType.Int, 0, objIShortGCView.ToLocationId  ),
                                        objDAL.MakeInParams("@Vehicle_Type_Id", SqlDbType.Int, 0, objIShortGCView.VehicleTypeId    ),
                                    objDAL.MakeInParams("@Standard_Basic_Freight_Unit_ID", SqlDbType.Int, 0, objIShortGCView.CompanyParameter_Standard_BasicFreightUnitId  )};

            objDAL.RunProc("EC_Opr_GC_Get_Transit_Days", objSqlParam, ref _ds);

            objIShortGCView.TotalTransitDays = Util.String2Int(objSqlParam[0].Value.ToString());
            objIShortGCView.TotalKiloMeter = Util.String2Int(objSqlParam[1].Value.ToString());
        }

        public void Get_Service_Tax_Applicable_For_Commodity()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Is_Service_Tax_Applicable_For_Commodity", SqlDbType.Int   , 0 ),                                         
                                         objDAL.MakeInParams("@Multiple_Commodity_Xml",SqlDbType.Xml  ,0,objIShortGCView.MultipleCommodityXml)};

            objDAL.RunProc("EC_Opr_GC_Get_Service_Tax_Applicable_For_Commodity", objSqlParam, ref _ds);

            objIShortGCView.Is_Service_Tax_Applicable_For_Commodity = Convert.ToBoolean(Util.String2Int(objSqlParam[0].Value.ToString()));
        }

        public void Get_ServiceTaxDetails()
        {
            Boolean Is_ServiceTaxApplicable;

            Is_ServiceTaxApplicable = false;

            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Is_Service_Tax_Applicable", SqlDbType.Int   , 0 ),
                                         objDAL.MakeInParams("@Billing_Details_Xml",SqlDbType.Xml  ,0,objIShortGCView.BillingDetailsXml   )};

            objDAL.RunProc("EC_Opr_GC_Get_Service_Tax_Details", objSqlParam, ref _ds);

            Is_ServiceTaxApplicable = Convert.ToBoolean(Util.String2Int(objSqlParam[0].Value.ToString()));

            if (Is_ServiceTaxApplicable == true)
            {
                objIShortGCView.Is_ServiceTaxApplicableForConsignor = 1;
                objIShortGCView.Is_ServiceTaxApplicableForConsignee = 0;
                //objIShortGCView.ServiceTaxPayableBy 
            }
            else
            {
                objIShortGCView.Is_ServiceTaxApplicableForConsignor = 0;
                objIShortGCView.Is_ServiceTaxApplicableForConsignee = 0;
            }
        }

        public DataSet Get_BookingSubType()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Booking_Type_id", SqlDbType.Int, 0, objIShortGCView.BookingTypeId  )};

            objDAL.RunProc("EC_Opr_GC_Get_Booking_Sub_Type", objSqlParam, ref _ds);
        
            return _ds;
        }

        public void Get_LengthCharge()
        {
            SqlParameter[] objSqlParam ={ 
                                        objDAL.MakeOutParams ("@Length_Charge", SqlDbType.Float   ,2 ),
                                        objDAL.MakeInParams("@Length_Charge_Head_Id", SqlDbType.Int, 0, objIShortGCView.LengthChargeHeadId  ) };

            objDAL.RunProc("EC_Opr_GC_Get_Length_Charge", objSqlParam, ref _ds);

            objIShortGCView.LengthCharge   = Util.String2Decimal(objSqlParam[0].Value.ToString());

            objIShortGCView.Standard_LengthCharge = Util.String2Decimal(objSqlParam[0].Value.ToString());
            objIShortGCView.Contractual_LengthCharge = Util.String2Decimal(objSqlParam[0].Value.ToString());
            objIShortGCView.Applicable_Standard_LengthCharge = Util.String2Decimal(objSqlParam[0].Value.ToString());            
        }
        
        public void Get_CompanyParameterDetails()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams ("@Standard_Freight_Rate_Per", SqlDbType.Float   ,2 ),
                                          objDAL.MakeOutParams("@Standard_Basic_Freight_Unit_ID", SqlDbType.Int, 0),
                                          objDAL.MakeOutParams("@Is_GC_Number_Editable", SqlDbType.Bit , 0),
                                          objDAL.MakeOutParams("@Is_Contract_Required_For_TBB_GC", SqlDbType.Bit , 0),
                                          objDAL.MakeOutParams("@Is_Invoice_Amount_Required", SqlDbType.Bit , 0),
                                          objDAL.MakeOutParams("@Is_Item_Required", SqlDbType.Bit , 0),
                                          objDAL.MakeOutParams("@Is_Validate_Credit_Limit", SqlDbType.Bit , 0),
                                          objDAL.MakeOutParams("@Client_Code", SqlDbType.VarChar  , 50),
                                          objDAL.MakeOutParams("@Regular_Client_Caption", SqlDbType.VarChar  , 50)};
                
            objDAL.RunProc("Get_Company_Parameter_Details", objSqlParam, ref _ds);
            
            objIShortGCView.CompanyParameter_Standard_FreightRatePer =   Util.String2Decimal   (objSqlParam[0].Value.ToString() ); 
            objIShortGCView.CompanyParameter_Standard_BasicFreightUnitId = Util.String2Int(objSqlParam[1].Value.ToString());
            objIShortGCView.Is_GCNumberEditable = Convert.ToBoolean(objSqlParam[2].Value.ToString());
            objIShortGCView.Is_Contract_Required_For_TBB_GC = Convert.ToBoolean(objSqlParam[3].Value.ToString());
            objIShortGCView.Is_Invoice_Amount_Required = Convert.ToBoolean(objSqlParam[4].Value.ToString());
            objIShortGCView.Is_Item_Required  = Convert.ToBoolean(objSqlParam[5].Value.ToString());
            objIShortGCView.Is_Validate_Credit_Limit = Convert.ToBoolean(objSqlParam[6].Value.ToString());
            objIShortGCView.ClientCode = objSqlParam[7].Value.ToString();
            objIShortGCView.RegularClientCaption = objSqlParam[8].Value.ToString();

            objIShortGCView.Session_Is_Validate_Credit_Limit = Convert.ToBoolean(objSqlParam[6].Value.ToString());

            //objIShortGCView.Is_FOV_Calculated_As_Per_Standard  = Convert.ToBoolean(objSqlParam[8].Value.ToString());
        }

        public DataSet Get_Application_Start_Date()
        {            
            objDAL.RunProc("Get_Application_Start_Date",  ref _ds);
            return _ds;
        }

        public DataSet Get_Company_GC_Parameter()
        {
            objDAL.RunProc("Get_Company_GC_Parameter", ref _ds);
            return _ds;
        }

        public DataSet Get_ConsignorConsigneeDetails(Int32 ConsignorConsigneeId, Boolean Is_RegularClient, Boolean Is_Consignor)
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Consignor_Consignee_Id", SqlDbType.Int, 0, ConsignorConsigneeId ),
                                        objDAL.MakeInParams("@Is_Regular_Client", SqlDbType.Bit, 0, Is_RegularClient )};

            objDAL.RunProc("Ec_Opr_GC_Get_Consignor_Consignee_Details", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_From_Location_Details()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, objIShortGCView.BookingBranchId )};

            objDAL.RunProc("Ec_Opr_GC_Get_From_Location_Details", objSqlParam, ref _ds);
            return _ds;
        } 

        public DataSet Fill_Location()
        {
            objDAL.RunProc("EC_Opr_GC_Fill_Location", ref _ds);
            return _ds;
        }       
    }
}




