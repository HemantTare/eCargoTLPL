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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for NewGCModel
/// </summary>
/// 
namespace Raj.EC.OperationModel
{
    public class NewGCModel : IModel
    {
        private INewGCView objINewGCView;
        private DataSet _ds;
        private DAL objDAL = new DAL();
        int MenuItemId = Common.GetMenuItemId();
        string message;
        public NewGCModel(INewGCView NewGCView)
        {
            objINewGCView = NewGCView;
        }

        public DataSet ReadValues()
        {
            return _ds;
        }

        public DataSet readValues(int KeyId)
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, KeyId) };

            objDAL.RunProc("EC_Opr_NewGC_ReadValues", objSqlParam, ref _ds);

            Common.SetPrimaryKeys(new string[] { "SizeID", "Item_ID", "Packing_ID" }, _ds.Tables[1]); // Commodity Grid
            Common.SetPrimaryKeys(new string[] { "Billing_Client_ID", "Billing_Branch_ID", "Billing_Hierarchy" }, _ds.Tables[3]); // Commodity Grid

            return _ds;
        }

        public DataSet Fill_Values()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId) };

            objDAL.RunProc("EC_Opr_NewGC_FillValues", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_From_Location_Details()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, objINewGCView.BookingBranchId) };

            objDAL.RunProc("Ec_Opr_GC_Get_From_Location_Details", objSqlParam, ref _ds);
            return _ds;
        }

        public bool IsDuplicate_GCNo()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, Util.String2Int(objINewGCView.GC_No_For_Print)),
                                          objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                          objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,objINewGCView.keyID),
                                          objDAL.MakeInParams("@MenuItem_Id",SqlDbType.Int ,0,MenuItemId)};

            objDAL.RunProc("EC_Opr_NewGC_Check_Duplicate", objSqlParam, ref _ds);

            return Util.String2Bool(_ds.Tables[0].Rows[0]["Is_Duplicate"].ToString());
        }

        public void getValidGCDate_ForRectify()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Document_Date", SqlDbType.DateTime, 0 ),
                                          objDAL.MakeInParams("@GC_ID", SqlDbType.Int, 0, objINewGCView.keyID) };
            objDAL.RunProc("EC_Opr_NewGC_getValidGCDate_ForRectify", objSqlParam);

            objINewGCView.GCDate_ForRectify = Convert.ToDateTime(objSqlParam[0].Value.ToString());
        }

        public void Get_VAId()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@VA_Id", SqlDbType.Int, 0 ),
                                        objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId )
                                        };
            objDAL.RunProc("Ec_Opr_GC_Get_VA_Id", objSqlParam, ref _ds);
            objINewGCView.VAId = Util.String2Int(objSqlParam[0].Value.ToString());
        }

        public DataSet Get_Company_GC_Parameter()
        {
            objDAL.RunProc("Get_Company_GC_Parameter", ref _ds);
            return _ds;
        }

        public DataTable Fill_Contract_And_Branches(int flag)
        {
            SqlParameter[] sqlPara = { 
                                objDAL.MakeInParams("@Flag", SqlDbType.Int, 0,flag),
                                objDAL.MakeInParams("@Contract_Client_Id", SqlDbType.Int, 0,objINewGCView.Contractual_ClientId),
                                objDAL.MakeInParams("@Contract_Branch_Id", SqlDbType.Int, 0,objINewGCView.Contract_BranchId),
                                objDAL.MakeInParams("@GC_Date", SqlDbType.DateTime, 0, objINewGCView.BookingDate)};

            objDAL.RunProc("EC_Opr_NewGC_Get_Contract_ClientBranch", sqlPara, ref _ds);

            return _ds.Tables[0];
        }

        public DataSet Get_Application_Start_Date()
        {
            objDAL.RunProc("Get_Application_Start_Date", ref _ds);
            return _ds;
        }
        public void Get_Service_Tax_Applicable_For_Commodity_And_BillingParty()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@Is_Service_Tax_Applicable_For_BillingParty", SqlDbType.Int,0),
                                         objDAL.MakeOutParams("@Is_Service_Tax_Applicable_For_Commodity", SqlDbType.Int,0),
                                         objDAL.MakeInParams("@Billing_Details_Xml",SqlDbType.Xml,0,objINewGCView.BillingDetailsXml),
                                         objDAL.MakeInParams("@Multiple_Commodity_Xml",SqlDbType.Xml,0,objINewGCView.MultipleCommodityXml)};

            objDAL.RunProc("EC_Opr_NewGC_Get_BillingParty_And_Commodity_ServiceTax", objSqlParam);

            objINewGCView.Is_Service_Tax_Payable_For_BillingParty = Convert.ToBoolean(Util.String2Int(objSqlParam[0].Value.ToString()));
            objINewGCView.Is_Service_Tax_Applicable_For_Commodity = Convert.ToBoolean(Util.String2Int(objSqlParam[1].Value.ToString()));
        }

        public DataSet Get_BranchRateParameter()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objINewGCView.BookingBranchId) };

            objDAL.RunProc("Ec_Opr_GC_Get_Branch_Rate_Parameter", objSqlParam, ref _ds);
            return _ds;
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
                                          objDAL.MakeOutParams("@Client_Code", SqlDbType.VarChar  , 100)};

            objDAL.RunProc("Get_Company_Parameter_Details", objSqlParam, ref _ds);

            objINewGCView.CompanyParameter_Standard_FreightRatePer = Util.String2Decimal(objSqlParam[0].Value.ToString());
            objINewGCView.CompanyParameter_Standard_BasicFreightUnitId = Util.String2Int(objSqlParam[1].Value.ToString());
            objINewGCView.Is_GCNumberEditable = Convert.ToBoolean(objSqlParam[2].Value.ToString());
            objINewGCView.Is_Contract_Required_For_TBB_GC = Convert.ToBoolean(objSqlParam[3].Value.ToString());
            objINewGCView.Is_Invoice_Amount_Required = Convert.ToBoolean(objSqlParam[4].Value.ToString());
            objINewGCView.Is_Item_Required = Convert.ToBoolean(objSqlParam[5].Value.ToString());
            objINewGCView.Is_Validate_Credit_Limit = Convert.ToBoolean(objSqlParam[6].Value.ToString());
            objINewGCView.ClientCode = objSqlParam[7].Value.ToString();
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Centralised_Booking_Branch_Id",SqlDbType.Int,0,objINewGCView.BookingBranchId),
            objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0, objINewGCView.DocumentSeriesAllocationId),
            objDAL.MakeInParams("@GC_Id",SqlDbType.Int,0, objINewGCView.keyID),
            objDAL.MakeInParams("@GC_No",SqlDbType.Int,0,Util.String2Int(objINewGCView.GC_No_For_Print)),
            objDAL.MakeInParams("@GC_No_For_Print",SqlDbType.VarChar,20,objINewGCView.ddl_GC_No_For_Print),
            objDAL.MakeInParams("@Is_Attached",SqlDbType.Bit,0,objINewGCView.Is_Attached),
            objDAL.MakeInParams("@Attached_GC_Id",SqlDbType.Int,0,objINewGCView.Attached_GC_Id ),
            objDAL.MakeInParams("@VA_Id",SqlDbType.Int,0,objINewGCView.VAId),
            objDAL.MakeInParams("@Pickup_Type_Id",SqlDbType.Int,0,objINewGCView.PickupTypeId),
            objDAL.MakeInParams("@GC_Date",SqlDbType.DateTime,0,objINewGCView.BookingDate),
            objDAL.MakeInParams("@GC_Time",SqlDbType.VarChar,8,objINewGCView.BookingTime),
            objDAL.MakeInParams("@Committed_Del_Date",SqlDbType.DateTime,0,objINewGCView.ExpectedDeliveryDate),
            objDAL.MakeInParams("@Consignment_Type_Id",SqlDbType.Int,0,objINewGCView.ConsignmentTypeId),
            objDAL.MakeInParams("@Booking_Mode_Id",SqlDbType.Int,0,objINewGCView.BookingModeId),
            objDAL.MakeInParams("@Booking_Type_Id",SqlDbType.Int,0,objINewGCView.BookingTypeId),
            objDAL.MakeInParams("@Booking_Sub_Type_Id",SqlDbType.Int,0,objINewGCView.BookingSubTypeId),
            objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,0,objINewGCView.PaymentTypeId),
            objDAL.MakeInParams("@Delivery_Type_Id",SqlDbType.Int,0,objINewGCView.DeliveryTypeId ),
            objDAL.MakeInParams("@Door_Delivery_Against_ID",SqlDbType.Int,0,objINewGCView.DeliveryAgainstId),
            objDAL.MakeInParams("@From_Branch_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@From_Location_ID",SqlDbType.Int,0, objINewGCView.FromLocationId),
            objDAL.MakeInParams("@To_Location_ID",SqlDbType.Int,0,objINewGCView.ToLocationId),
            objDAL.MakeInParams("@Delivery_Branch_Id",SqlDbType.Int,0,objINewGCView.DeliveryBranchId),
            objDAL.MakeInParams("@Vehicle_Type_Id",SqlDbType.Int,0,objINewGCView.VehicleTypeId),
            objDAL.MakeInParams("@Vehicle_No",SqlDbType.NVarChar,40,objINewGCView.VehicleNo),
            objDAL.MakeInParams("@STM_No",SqlDbType.NVarChar,50,objINewGCView.STMNo),
            objDAL.MakeInParams("@Feasibility_Route_Survey_No",SqlDbType.NVarChar,50,objINewGCView.FeasibilityRouteSurveyNo),
            objDAL.MakeInParams("@Consignee_Client_ID",SqlDbType.Int,0,objINewGCView.ConsigneeId),
            objDAL.MakeInParams("@Consignor_Client_ID",SqlDbType.Int,0,objINewGCView.ConsignorId),
            objDAL.MakeInParams("@Is_SignedByConsignor",SqlDbType.Bit,0, objINewGCView.Is_SignedByConsignor),
            objDAL.MakeInParams("@DD_Address_1",SqlDbType.VarChar,100,objINewGCView.ConsigneeDDAddress1),
            objDAL.MakeInParams("@DD_Address_2",SqlDbType.VarChar,100,objINewGCView.ConsigneeDDAddress2),
            objDAL.MakeInParams("@Road_Permit_Type_Id",SqlDbType.Int,0, objINewGCView.RoadPermitTypeId),
            objDAL.MakeInParams("@Delivery_Way_Type_ID",SqlDbType.Int,0,objINewGCView.DeliveryWayTypeId),
            objDAL.MakeInParams("@Acknowledge",SqlDbType.Bit,0, objINewGCView.Is_POD),
            objDAL.MakeInParams("@ItemValueForFOV",SqlDbType.Decimal,0,objINewGCView.ItemValueForFOV),
            objDAL.MakeInParams("@Total_Articles",SqlDbType.Int,0,objINewGCView.TotalArticles),
            objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,objINewGCView.TotalWeight),
            objDAL.MakeInParams("@Charged_Weight",SqlDbType.Decimal,0,objINewGCView.ChargeWeight),
            objDAL.MakeInParams("@Total_Invoice_Value",SqlDbType.Decimal,0,objINewGCView.TotalInvoiceAmount),
            objDAL.MakeInParams("@Freight_Rate",SqlDbType.Decimal,0,objINewGCView.FreightRate),
            objDAL.MakeInParams("@Freight_Amt",SqlDbType.Decimal,0,objINewGCView.Freight),
            objDAL.MakeInParams("@Discount",SqlDbType.Decimal,0,objINewGCView.Discount),
            objDAL.MakeInParams("@Local_Charges",SqlDbType.Decimal,0,objINewGCView.LocalCharge),
            objDAL.MakeInParams("@Bilti_Charges",SqlDbType.Decimal,0,objINewGCView.StationaryCharge),
            objDAL.MakeInParams("@Hamali_Per_Kg",SqlDbType.Decimal,0,objINewGCView.RateCard_HamaliPerKg),
            objDAL.MakeInParams("@Hamali_Charges",SqlDbType.Decimal,0,objINewGCView.LoadingCharge),
            objDAL.MakeInParams("@Hamali_Per_Articles",SqlDbType.Decimal,0,objINewGCView.RateCard_HamaliPerArticles),
            objDAL.MakeInParams("@DD_Charges",SqlDbType.Decimal,0,objINewGCView.DDCharge),
            objDAL.MakeInParams("@TP_Charges",SqlDbType.Decimal,0,objINewGCView.ToPayCharge),
            objDAL.MakeInParams("@Other_Charges",SqlDbType.Decimal,0, objINewGCView.OtherCharges),
            objDAL.MakeInParams("@Tax_Abate_Percent",SqlDbType.Decimal,0,objINewGCView.TaxAbatePercent),
            objDAL.MakeInParams("@Tax_Abate",SqlDbType.Decimal,0,objINewGCView.Abatment),
            objDAL.MakeInParams("@Amt_Taxable",SqlDbType.Decimal,0,objINewGCView.TaxableAmount),
            objDAL.MakeInParams("@FOVPercent",SqlDbType.Decimal,0, objINewGCView.RateCard_FOVPercentage),
            objDAL.MakeInParams("@FOV",SqlDbType.Decimal,0,objINewGCView.FOVRiskCharge),
            objDAL.MakeInParams("@ODA_Charges",SqlDbType.Decimal,0,objINewGCView.ODACharges),
            objDAL.MakeInParams("@Oda_Charges_UpTo_500_Kg",SqlDbType.Decimal,0,objINewGCView.ODAChargesUpTo500Kg),
            objDAL.MakeInParams("@Oda_Charges_Above_500_Kg",SqlDbType.Decimal,0,objINewGCView.ODAChargesAbove500Kg),
            objDAL.MakeInParams("@Length_Charge_Head_Id",SqlDbType.Int ,0, objINewGCView.LengthChargeHeadId),
            objDAL.MakeInParams("@Length_Charge",SqlDbType.Decimal ,0, objINewGCView.LengthCharge),
            objDAL.MakeInParams("@Unloading_Charge",SqlDbType.Decimal ,0,objINewGCView.UnloadingCharge),
            objDAL.MakeInParams("@NForm_Charge",SqlDbType.Decimal,0,objINewGCView.NFormCharge),
            objDAL.MakeInParams("@Std_NForm_Charge",SqlDbType.Decimal,0,objINewGCView.NFormCharge),
            objDAL.MakeInParams("@ReBook_Octroi_Amount",SqlDbType.Decimal ,0,objINewGCView.ReBookGC_OctroiAmount),
            objDAL.MakeInParams("@Sub_Total",SqlDbType.Decimal,0,objINewGCView.SubTotal),
            objDAL.MakeInParams("@Advance_Amount",SqlDbType.Decimal,0,objINewGCView.Advance),
            objDAL.MakeInParams("@Service_Tax_Percent",SqlDbType.Decimal,0,objINewGCView.Standard_ServiceTaxPercent),// 12.36),//objINewGCView.ServiceTaxpercent ),
            objDAL.MakeInParams("@Service_Tax_Amount",SqlDbType.Decimal,0,objINewGCView.ServiceTax),
            objDAL.MakeInParams("@Actual_Service_Tax_Amount",SqlDbType.Decimal,0,objINewGCView.ActualServiceTax),
            objDAL.MakeInParams("@Total_GC_Amount",SqlDbType.Decimal,0,objINewGCView.TotalGCAmount),
            objDAL.MakeInParams("@Std_Freight_Rate",SqlDbType.Decimal,0,objINewGCView.Standard_FreightRate ),
            objDAL.MakeInParams("@Std_Freight_Amt",SqlDbType.Decimal,0,objINewGCView.Standard_FreightAmount),
            objDAL.MakeInParams("@Std_Local_Charge_Rate",SqlDbType.Decimal,0,objINewGCView.RateCard_LocalCharge),
            objDAL.MakeInParams("@Std_Local_Charge",SqlDbType.Decimal,0,objINewGCView.RateCard_LocalCharge),
            objDAL.MakeInParams("@Std_Hamali_Charge",SqlDbType.Decimal,0,objINewGCView.Standard_HamaliCharge),
            objDAL.MakeInParams("@Std_DD_Charge_Rate",SqlDbType.Decimal,0, objINewGCView.Standard_DDChargeRate),
            objDAL.MakeInParams("@Std_DD_Charge",SqlDbType.Decimal,0, objINewGCView.Standard_DDCharge),
            objDAL.MakeInParams("@Std_Bilti_Charges",SqlDbType.Decimal,0,objINewGCView.RateCard_BiltiCharges),
            objDAL.MakeInParams("@Std_Service_Tax_Amount",SqlDbType.Decimal,0,objINewGCView.Standard_ServiceTaxAmount),// objINewGCView.Standard_ServiceTaxAmount 
            objDAL.MakeInParams("@Std_FOV",SqlDbType.Decimal,0, objINewGCView.Standard_FOV),
            objDAL.MakeInParams("@Std_TP_Charges",SqlDbType.Decimal,0,objINewGCView.RateCard_ToPayCharges),
            objDAL.MakeInParams("@Std_CFT_Factor",SqlDbType.Decimal,0,objINewGCView.RateCard_CFTFactor),
            objDAL.MakeInParams("@Std_Octroi_Form_Charges",SqlDbType.Decimal,0,objINewGCView.RateCard_Octroi_Form_Charge),
            objDAL.MakeInParams("@Std_Octroi_Service_Charges",SqlDbType.Decimal,0,objINewGCView.RateCard_Octroi_Service_Charge),
            objDAL.MakeInParams("@Std_Demurrage_Days",SqlDbType.Decimal,0,objINewGCView.RateCard_Demurrage_Days),
            objDAL.MakeInParams("@Std_Demurrage_Rate",SqlDbType.Decimal,0,objINewGCView.RateCard_Demurrage_Rate),
            objDAL.MakeInParams("@Std_GI_Charges",SqlDbType.Decimal,0,objINewGCView.RateCard_GI_Charges),
            objDAL.MakeInParams("@DACC_Charges",SqlDbType.Decimal,0,objINewGCView.DACCCharges),
            objDAL.MakeInParams("@Std_DACC_Charge",SqlDbType.Decimal,0,objINewGCView.RateCard_DACCCharges),
            objDAL.MakeInParams("@ReBook_Charges",SqlDbType.Decimal,0,0),//objINewGCView.ReBookGC_Amount
            objDAL.MakeInParams("@ReBook_GC_Sub_Total",SqlDbType.Decimal,0,0),//objINewGCView.ReBookGC_SubTotal
            objDAL.MakeInParams("@Is_Cheque",SqlDbType.Bit,0,objINewGCView.Is_Cheque),
            objDAL.MakeInParams("@Cheque_No",SqlDbType.Int,0,objINewGCView.ChequeNo),
            objDAL.MakeInParams("@Cheque_Date",SqlDbType.DateTime,0, objINewGCView.ChequeDate),
            objDAL.MakeInParams("@Bank_Name",SqlDbType.VarChar,100,objINewGCView.BankName),
            objDAL.MakeInParams("@eWayBillNo",SqlDbType.VarChar,25,objINewGCView.eWayBillNo),
            objDAL.MakeInParams("@Is_Multiple_eWayBill",SqlDbType.Bit,0,objINewGCView.Is_MultipleeWayBill),
            objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,objINewGCView.CashAmount),
            objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,objINewGCView.ChequeAmount),
            objDAL.MakeInParams("@Tax_Payable_By",SqlDbType.Int ,0,objINewGCView.ServiceTaxPayableBy),
            objDAL.MakeInParams("@Contract_Branch_ID",SqlDbType.Int,0,objINewGCView.Contract_BranchId),
            objDAL.MakeInParams("@Contract_ID",SqlDbType.Int,0,objINewGCView.ContractId),
            objDAL.MakeInParams("@Is_Multiple_Billing",SqlDbType.Bit,0,objINewGCView.Is_MultipleBilling),
            objDAL.MakeInParams("@Billing_Client_ID",SqlDbType.Int,0,objINewGCView.BillingPartyId),
            objDAL.MakeInParams("@Billing_Hierarchy",SqlDbType.VarChar,5,objINewGCView.BillingHierarchy),
            objDAL.MakeInParams("@Billing_Branch_Id",SqlDbType.Int,0,objINewGCView.BillingLocationId),
            objDAL.MakeInParams("@Billing_Remarks",SqlDbType.VarChar,50,objINewGCView.BillingRemark),
            objDAL.MakeInParams("@Billing_Party_Ledger_Id",SqlDbType.Int,0,objINewGCView.BillingParty_LedgerId),
            objDAL.MakeInParams("@Billing_Party_Credit_Limit",SqlDbType.Decimal,0,objINewGCView.BillingParty_CreditLimit),
            objDAL.MakeInParams("@Risk_Type_ID",SqlDbType.Int,0,objINewGCView.GCRiskId),
            objDAL.MakeInParams("@Insurance_Company",SqlDbType.VarChar,0,objINewGCView.Session_InsuranceCompany),
            objDAL.MakeInParams("@Policy_No",SqlDbType.NVarChar,50,objINewGCView.Session_PolicyNo),
            objDAL.MakeInParams("@Policy_Exp_Date",SqlDbType.DateTime,0,objINewGCView.Session_PolicyExpDate),
            objDAL.MakeInParams("@Policy_Amount",SqlDbType.Decimal,0,objINewGCView.Session_PolicyAmount),
            objDAL.MakeInParams("@Risk_Amount",SqlDbType.Decimal,0,objINewGCView.Session_RiskAmount),
            objDAL.MakeInParams("@Freight_Basis_ID",SqlDbType.Int,0,objINewGCView.FreightBasisId),
            objDAL.MakeInParams("@Volumetric_Freight_Unit_ID",SqlDbType.Int,0,objINewGCView.VolumetricFreightUnitId),
            objDAL.MakeInParams("@Unit_Of_Measurement_ID",SqlDbType.Int,0,objINewGCView.UnitOfMeasurementId),
            objDAL.MakeInParams("@Total_Length",SqlDbType.Decimal,0, objINewGCView.UnitOfMeasurmentLength),
            objDAL.MakeInParams("@Total_Width",SqlDbType.Decimal,0,objINewGCView.UnitOfMeasurmentWidth),
            objDAL.MakeInParams("@Total_Height",SqlDbType.Decimal,0,objINewGCView.UnitOfMeasurmentHeight),
            objDAL.MakeInParams("@Total_Length_In_Feet",SqlDbType.Decimal,0, objINewGCView.LengthInFeet),
            objDAL.MakeInParams("@Total_Width_In_Feet",SqlDbType.Decimal,0,objINewGCView.WidthInFeet),
            objDAL.MakeInParams("@Total_Height_In_Feet",SqlDbType.Decimal,0,objINewGCView.HeightInFeet),
            objDAL.MakeInParams("@CFT_Factor",SqlDbType.Decimal,0,objINewGCView.VolumetricToKgFactor),
            objDAL.MakeInParams("@Total_CFT",SqlDbType.Decimal,0,objINewGCView.TotalCFT),
            objDAL.MakeInParams("@Total_CBM",SqlDbType.Decimal,0,objINewGCView.TotalCBM),
            objDAL.MakeInParams("@Container_Type_Id",SqlDbType.Int,0,objINewGCView.Session_ContainerTypeId),
            objDAL.MakeInParams("@ContainerNo1",SqlDbType.VarChar,10,objINewGCView.Session_ContainerNoPart1),
            objDAL.MakeInParams("@ContainerNo2",SqlDbType.VarChar,10,objINewGCView.Session_ContainerNoPart2),
            objDAL.MakeInParams("@SealNo",SqlDbType.VarChar,10,objINewGCView.Session_SealNo),
            objDAL.MakeInParams("@Return_To_Yard_Id",SqlDbType.Int,0,objINewGCView.Session_ReturnToYardId),
            objDAL.MakeInParams("@NFormNo",SqlDbType.VarChar,20,objINewGCView.Session_NFormNo),
            objDAL.MakeInParams("@Customer_Ref_No",SqlDbType.VarChar,20,objINewGCView.CustomerRefNo),
            objDAL.MakeInParams("@GC_Remarks",SqlDbType.VarChar,250,objINewGCView.InstructionRemark),
            objDAL.MakeInParams("@GC_Remarks_Other_Charges",SqlDbType.VarChar,100, objINewGCView.OtherChargesRemark),
            objDAL.MakeInParams("@Enclosures",SqlDbType.VarChar,150,objINewGCView.Enclosures),
            objDAL.MakeInParams("@Is_ODA",SqlDbType.Bit,0,objINewGCView.Is_ODA),
            objDAL.MakeInParams("@Is_Octroi_Applicable",SqlDbType.Bit,0,objINewGCView.Is_OctroiApplicable),
            objDAL.MakeInParams("@Private_Mark",SqlDbType.VarChar,20,objINewGCView.PrivateMark),
            objDAL.MakeInParams("@Is_Consignor_Regular_Client",SqlDbType.Bit,0,objINewGCView.Is_RegularConsignor),
            objDAL.MakeInParams("@Is_Consignee_Regular_Client",SqlDbType.Bit,0,objINewGCView.Is_RegularConsignee),
            objDAL.MakeInParams("@Loading_Supervisor_ID",SqlDbType.Int,0,objINewGCView.LoadingSuperVisorId),
            objDAL.MakeInParams("@Marketing_Executive_ID",SqlDbType.Int,0,objINewGCView.MarketingExecutiveId),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Multiple_Commodity_Xml",SqlDbType.Xml,0,objINewGCView.MultipleCommodityXml),
            objDAL.MakeInParams("@Invoice_Xml",SqlDbType.Xml,0,objINewGCView.InvoiceXml),
            objDAL.MakeInParams("@Other_Charges_Xml",SqlDbType.Xml,0,objINewGCView.OtherChargesXml),
            objDAL.MakeInParams("@Billing_Details_Xml",SqlDbType.Xml,0,objINewGCView.BillingDetailsXml),
            objDAL.MakeInParams("@Cheque_Details_Xml",SqlDbType.Xml,0,objINewGCView.ChequeDetailsXml),
            objDAL.MakeInParams("@Road_Permit_SrNo",SqlDbType.VarChar,0,objINewGCView.RoadPermitSrNo),
            objDAL.MakeInParams("@Is_Insured",SqlDbType.Bit,0,objINewGCView.Is_Insured),
            objDAL.MakeInParams("@Default_Cash_Ledger_Id",SqlDbType.Int,0,objINewGCView.Default_Cash_Ledger_Id),
            objDAL.MakeInParams("@Booking_Branch_Id",SqlDbType.Int,0,objINewGCView.BookingBranchId),
            objDAL.MakeInParams("@Arrived_From_Branch_Id",SqlDbType.Int,0,objINewGCView.ArrivedFromBranchId),
            objDAL.MakeInParams("@Agency_GC_No",SqlDbType.NVarChar,40,objINewGCView.Agency_GC_No),
            objDAL.MakeInParams("@Agency_Ledger_Id",SqlDbType.Int,0,objINewGCView.AgencyLedgerId),
            objDAL.MakeInParams("@Agency_Branch_ID",SqlDbType.Int,0,objINewGCView.AgencyId),
            objDAL.MakeInParams("@CRM_Pickup_Request_ID",SqlDbType.Int,0,objINewGCView.CRMPickupRequestId),
            objDAL.MakeInParams("@Arrived_Date",SqlDbType.DateTime,0,objINewGCView.ArrivedDate),
            objDAL.MakeInParams("@Is_ReBooked",SqlDbType.Bit,0, 0),//objINewGCView.Is_ReBooked
            objDAL.MakeInParams("@ReBook_GC_Id",SqlDbType.Int,0,0),// objINewGCView.ReBook_GC_Id 
            objDAL.MakeInParams("@ReBook_Against_GC_Id",SqlDbType.Int,0, 0),//objINewGCView.ReBook_GC_Id 
            objDAL.MakeInParams("@New_ReBook_GC_Id",SqlDbType.Int,0, 0),
            objDAL.MakeInParams("@ReBook_GC_Octroi_Paid_By_ID",SqlDbType.Int ,0,0),//objINewGCView.ReBook_GCOctroiPaidByID
            objDAL.MakeInParams("@Menu_Item_Id",SqlDbType.Int,0, MenuItemId),
            objDAL.MakeInParams("@Service_Type_Id",SqlDbType.Int,0, objINewGCView.ServiceTypeId),
            objDAL.MakeInParams("@Is_ST_Abatment",SqlDbType.Bit,0, objINewGCView.Is_ST_Abatment_Required),
            objDAL.MakeInParams("@AOC_Percent",SqlDbType.Decimal ,0,objINewGCView.AOCPercent),
            objDAL.MakeInParams("@AOC",SqlDbType.Decimal ,0,objINewGCView.AOC),
            objDAL.MakeInParams("@Round_Off",SqlDbType.Int ,0,objINewGCView.RoundOff),
            objDAL.MakeInParams("@DiscountId",SqlDbType.Decimal,0,objINewGCView.DiscountId),
            objDAL.MakeInParams("@Wholeseler_ID",SqlDbType.Int,0,objINewGCView.WholeselerId),
            objDAL.MakeInParams("@ReasonFreightPendingId",SqlDbType.Int,0,objINewGCView.ReasonFreightPendingId),
            objDAL.MakeInParams("@PaidFreightPendingPersonName",SqlDbType.VarChar,50,objINewGCView.PaidFreightPendingPersonName),
            objDAL.MakeInParams("@PaidFreightPendingPersonMobile",SqlDbType.VarChar,20,objINewGCView.PaidFreightPendingPersonMobile),
            objDAL.MakeInParams("@RateContractId",SqlDbType.Int,0,objINewGCView.RateContractId)};

            if (MenuItemId == 194)
                objDAL.RunProc("EC_Opr_NewGC_Rectification_Save", objSqlParam);
            else
                objDAL.RunProc("EC_Opr_NewGC_Save", objSqlParam); 
            objMessage.messageID = Util.String2Int(objSqlParam[0].Value.ToString());
            objMessage.message = objSqlParam[1].Value.ToString();

            if (objMessage.messageID == 0)
            {
                string _Msg = "Saved SuccessFully";
                if (objINewGCView.keyID == -1)
                { Get_Consignor_Conginee_SMS(Convert.ToInt32(objSqlParam[2].Value)); }
                if (objINewGCView.Flag == "SaveAndNew")
                {
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];

                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    HttpContext.Current.Session["DocumentIdForSaveAndNewAndPrint"] = Document_ID;

                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                            ClassLibraryMVP.Util.EncryptString("Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=" +
                            ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));

                }
                else if (objINewGCView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objINewGCView.Flag == "SaveAndPrint")
                {
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);

                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                            ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                            ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" +
                            ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
                else if (objINewGCView.Flag == "SaveAndPrintandnew")
                {
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);

                    

                }
                else if (objINewGCView.Flag == "SaveAndRepet")
                { }

               
            }
            else
            {
                Common.DisplayErrors(objMessage.messageID);
            }

            return objMessage;
        }

        public void Get_Consignor_Conginee_SMS(int GC_ID)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();
            int MenuItemId = Common.GetMenuItemId();
            SqlParameter[] sqlPara = { objdal.MakeInParams("@GC_ID", SqlDbType.Int, 0, GC_ID)
             ,objdal.MakeInParams("@MenuItemID", SqlDbType.Int, 0, Common.GetMenuItemId())
             ,objdal.MakeInParams("@DocumentID", SqlDbType.Int, 0, GC_ID)};

            objdal.RunProc("Ec_Opr_Get_Consignor_Conginee_SMS_MSG", sqlPara, ref ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {

                    String sendToPhoneNumber = ds.Tables[0].Rows[0]["Consignor_Mobile_No"].ToString();
                    string msg = ds.Tables[0].Rows[0]["MsgConsignor"].ToString();

                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {

                        String userid = "2000126072";
                        String passwd = "Rajan@1234";


                        String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                        request = WebRequest.Create(url);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        reader.Close();
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    string excep = exp.ToString();
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    String sendToPhoneNumber = ds.Tables[1].Rows[0]["Consignee_Mobile_No"].ToString();
                    string msg = ds.Tables[1].Rows[0]["MsgConsignee"].ToString();


                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {
                        String userid = "2000126072";
                        String passwd = "Rajan@1234";

                        String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                        request = WebRequest.Create(url);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        reader.Close();
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    string excep = exp.ToString();
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    String sendToPhoneNumber = ds.Tables[2].Rows[0]["BillingParty_Mobile_No"].ToString();
                    string msg = ds.Tables[2].Rows[0]["MsgBillingParty"].ToString();


                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {
                        String userid = "2000126072";
                        String passwd = "Rajan@1234";

                        String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                        request = WebRequest.Create(url);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        reader.Close();
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    string excep = exp.ToString();
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    String sendToPhoneNumber = ds.Tables[3].Rows[0]["BillingPartyAccounts_Mobile_No"].ToString();
                    string msg = ds.Tables[3].Rows[0]["MsgBillingPartyAccounts"].ToString();


                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {
                        String userid = "2000126072";
                        String passwd = "Rajan@1234";

                        String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                        request = WebRequest.Create(url);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        reader.Close();
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    string excep = exp.ToString();
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    String sendToPhoneNumber = ds.Tables[4].Rows[0]["APMCBroker_Mobile_No"].ToString();
                    string msg = ds.Tables[4].Rows[0]["MsgAPMCBroker"].ToString();


                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {
                        String userid = "2000126072";
                        String passwd = "Rajan@1234";

                        String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                        request = WebRequest.Create(url);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        reader.Close();
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    string excep = exp.ToString();
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }


        }
        public bool ValidateMobileDetails(String sendToPhoneNumber, string msg)
        {
            bool Is_Valid = false;
            if (sendToPhoneNumber == "0" || msg == "")
            {
                Is_Valid = false;
            }
            else
            {
                Is_Valid = true;
            }

            return Is_Valid;
        }
       

    }
    public class NewGCSearch
    {
        [AjaxPro.AjaxMethod()]
        public DataTable Get_Search(string SearchFor, bool IsSearchByCode, string SearchType, int BranchId, string Defaults)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = {
                            objdal.MakeInParams("@SearchFor", SqlDbType.VarChar, 100, SearchFor),
                            objdal.MakeInParams("@Is_Search_By_Code", SqlDbType.Bit, 0, IsSearchByCode), //For Client Only
                            objdal.MakeInParams("@SearchType", SqlDbType.VarChar, 50, SearchType), //For Location Only
                            objdal.MakeInParams("@BranchId", SqlDbType.Int, 0, BranchId),        //For Location Only
                            objdal.MakeInParams("@Defaults", SqlDbType.VarChar, 50, Defaults)};

            objdal.RunProc("Ec_Opr_NewGC_Get_Search", sqlPara, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public DataTable CalculateApproxWeightAndRate(int packages, int fromBranchID, int toBranchID,
            int itemID, int sizeID, DateTime bookingDate, int DeliveryAreaID, int Dly_TypeID, int PaymentTypeID)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = {
                            objdal.MakeInParams("@Pakages", SqlDbType.Int, 0, packages),
                            objdal.MakeInParams("@FromBranchID", SqlDbType.Int, 0, fromBranchID),
                            objdal.MakeInParams("@ToLocationID", SqlDbType.Int, 0, toBranchID), 
                            objdal.MakeInParams("@ItemID", SqlDbType.Int, 0, itemID), 
                            objdal.MakeInParams("@SizeID", SqlDbType.Int, 0, sizeID),
                            objdal.MakeInParams("@bookingDate", SqlDbType.DateTime, 0, bookingDate),
                            objdal.MakeInParams("@DeliveryAreaID", SqlDbType.Int, 0, DeliveryAreaID),
                            objdal.MakeInParams("@DeliveryTypeId", SqlDbType.Int, 0, Dly_TypeID),
                            objdal.MakeInParams("@PaymentTypeID", SqlDbType.Int, 0, PaymentTypeID)
            };

            objdal.RunProc("dbo.EC_Opr_NewGC_Get_ApproxWeightAndAmt", sqlPara, ref ds);
            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public DataTable CalculateApproxWeightAndRate_RateContract(int RateContractID, int packages, int fromSerLocationID, int toSerLocationID,
            int sizeID, int itemID, int fromBranchID, DateTime bookingDate, int PaymentTypeID)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = {
                             objdal.MakeInParams("@RateContractID", SqlDbType.Int, 0, RateContractID),
                            objdal.MakeInParams("@Pakages", SqlDbType.Int, 0, packages),
                            objdal.MakeInParams("@fromSerLocationID", SqlDbType.Int, 0, fromSerLocationID),
                            objdal.MakeInParams("@toSerLocationID", SqlDbType.Int, 0, toSerLocationID), 
                            objdal.MakeInParams("@SizeID", SqlDbType.Int, 0, sizeID),
                            objdal.MakeInParams("@ItemID", SqlDbType.Int, 0, itemID),
                            objdal.MakeInParams("@FromBranchID", SqlDbType.Int, 0, fromBranchID),
                            objdal.MakeInParams("@bookingDate", SqlDbType.DateTime, 0, bookingDate),
                            objdal.MakeInParams("@PaymentTypeID", SqlDbType.Int, 0, PaymentTypeID)};

            objdal.RunProc("dbo.EC_Opr_NewGC_Get_ApproxWeightAndAmt_RateContract", sqlPara, ref ds);
            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public static DataTable Get_RateContract(DateTime BookingDate, int FromLocationID, int ToLocationID,
            int ConsignorID, int IsConsignorRegular, int ConsigneeID, int IsConsigneeRegular,
            int BillingClientID, int Payment_Type_ID)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = {
                            objdal.MakeInParams("@BookingDate", SqlDbType.DateTime, 0, BookingDate),
                            objdal.MakeInParams("@FromLocationID", SqlDbType.Int, 0, FromLocationID),
                            objdal.MakeInParams("@ToLocationID", SqlDbType.Int, 0, ToLocationID), 
                            objdal.MakeInParams("@ConsignorID", SqlDbType.Int, 0, ConsignorID), 
                            objdal.MakeInParams("@IsConsignorRegular", SqlDbType.Int, 0, IsConsignorRegular), 
                            objdal.MakeInParams("@ConsigneeID", SqlDbType.Int, 0, ConsigneeID),
                            objdal.MakeInParams("@IsConsigneeRegular", SqlDbType.Int, 0, IsConsigneeRegular),
                            objdal.MakeInParams("@BillingClientID", SqlDbType.Int, 0, BillingClientID),
                            objdal.MakeInParams("@Payment_Type_ID", SqlDbType.Int, 0, Payment_Type_ID)};

            objdal.RunProc("dbo.Get_GC_RateContract", sqlPara, ref ds);
            return ds.Tables[0];
        } 

        [AjaxPro.AjaxMethod()]
        public DataTable Get_StdFreightRate(int FromLocId, int ToLocId, int DivisionId, int RoadPermitTypeId, int VehicleTypeId)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = {
                            objdal.MakeInParams("@From_Location_id", SqlDbType.Int, 0, FromLocId),
                            objdal.MakeInParams("@To_Location_id", SqlDbType.Int, 0, ToLocId),
                            objdal.MakeInParams("@Division_Id", SqlDbType.Int, 0, DivisionId), 
                            objdal.MakeInParams("@Vehicle_Type_Id", SqlDbType.Int, 0, VehicleTypeId), 
                            objdal.MakeInParams("@RoadPermit_Type_Id", SqlDbType.Int, 0, RoadPermitTypeId)};

            objdal.RunProc("EC_Opr_NewGC_Get_FreightRate_And_TransitDays", sqlPara, ref ds);
            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public DataTable Get_AllDetails(int defaults, int SearchTypeId, string SearchType)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { 
                                objdal.MakeInParams("@defaults", SqlDbType.Int, 0, defaults),
                                objdal.MakeInParams("@SearchTypeId", SqlDbType.Int, 0, SearchTypeId),
                                objdal.MakeInParams("@SearchType", SqlDbType.VarChar, 50, SearchType)};

            objdal.RunProc("EC_Opr_NewGC_Get_All_Details", sqlPara, ref ds);

            return ds.Tables[0];
        }
        [AjaxPro.AjaxMethod()]
        public DataTable Get_Contract_ClientBranch(int flag, int ClientId, int BranchId, DateTime Date)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { 
                                objdal.MakeInParams("@Flag", SqlDbType.Int, 0,flag),
                                objdal.MakeInParams("@Contract_Client_Id", SqlDbType.Int, 0,ClientId),
                                objdal.MakeInParams("@Contract_Branch_Id", SqlDbType.Int, 0,BranchId),
                                objdal.MakeInParams("@GC_Date", SqlDbType.DateTime, 0, Date)};

            objdal.RunProc("EC_Opr_NewGC_Get_Contract_ClientBranch", sqlPara, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public DataTable Get_ContractDetails(int Contract_Id, int Vehicle_Type_Id, int From_Loc_Id, int To_Loc_Id, int Commodity_Id, int Item_Id, int Article_Id, int TransitDays, int DistanceInKm, int TotalArticle, int ChargeWt, int TotalCft)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { 
                                objdal.MakeInParams("@Contract_Id", SqlDbType.Int, 0,Contract_Id),
                                objdal.MakeInParams("@Vehicle_Type_Id", SqlDbType.Int, 0,Vehicle_Type_Id),
                                objdal.MakeInParams("@From_Loc_Id", SqlDbType.Int, 0,From_Loc_Id),
                                objdal.MakeInParams("@To_Loc_Id", SqlDbType.Int, 0,To_Loc_Id),
                                objdal.MakeInParams("@Commodity_Id", SqlDbType.Int, 0,Commodity_Id),
                                objdal.MakeInParams("@Item_Id", SqlDbType.Int, 0,Item_Id),
                                objdal.MakeInParams("@Article_Id", SqlDbType.Int, 0,Article_Id),
                                objdal.MakeInParams("@transit_days", SqlDbType.Int, 0,TransitDays),
                                objdal.MakeInParams("@distance", SqlDbType.Int, 0,DistanceInKm),
                                objdal.MakeInParams("@total_article", SqlDbType.Int, 0,TotalArticle),
                                objdal.MakeInParams("@charge_wt", SqlDbType.Int, 0,ChargeWt),
                                objdal.MakeInParams("@total_cft", SqlDbType.Int, 0,TotalCft)};

            objdal.RunProc("EC_Opr_NewGC_Get_Contract_Details", sqlPara, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public DataTable Get_DDCharges(int Articles, int Weight, int DeliveryAreaId)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { 
                                objdal.MakeInParams("@Articles", SqlDbType.Int, 0,Articles),
                                objdal.MakeInParams("@Weight", SqlDbType.Int, 0,Weight),
                                objdal.MakeInParams("@DeliveryAreaId", SqlDbType.Int, 0,DeliveryAreaId)};

            objdal.RunProc("EC_Opr_NewGC_Get_DDCharges", sqlPara, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod()]
        public DataTable Get_ServiceTaxOnPickerChange(DateTime ApplicableFrom)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { objdal.MakeInParams("@date", SqlDbType.DateTime, 0, ApplicableFrom) };
            objdal.RunProc("Ec_Opr_NewGC_Service_Tax_Percent", sqlPara, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAllEmployee(string SearchFor, string TableName, string KeyName, string KeyID, string othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 100, SearchFor),
                                        _objDAL.MakeInParams("@SearchType", SqlDbType.VarChar, 50, "Employee")};
            _objDAL.RunProc("EC_Mst_NewGC_Search_Type_All", sqlPara, ref _ds);

            return _ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetBranch(string SearchFor, string TableName, string KeyName, string KeyID, string othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 100, SearchFor),
                                        _objDAL.MakeInParams("@SearchType", SqlDbType.VarChar, 50, "Branch")};
            _objDAL.RunProc("EC_Mst_NewGC_Search_Type_All", sqlPara, ref _ds);

            return _ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable Check_Duplicate_GC_No(int GC_Id, int GC_No, int Menu_Item_Id)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = {
                _objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode),
                _objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, GC_No),
                _objDAL.MakeInParams("@GC_Id",SqlDbType.Int,0,GC_Id),
                _objDAL.MakeInParams("@MenuItem_Id", SqlDbType.Int, 0,Menu_Item_Id)};
            _objDAL.RunProc("EC_Opr_NewGC_Check_Duplicate", sqlPara, ref _ds);

            return _ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable Get_Next_GC_No(int VA_Id, int Main_Id, int Menu_Item_Id)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = {_objDAL.MakeInParams("@VA_Id",SqlDbType.Int,0,VA_Id),
                _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, 0),
                _objDAL.MakeInParams("@Menu_Item_Id", SqlDbType.Int, 0, Menu_Item_Id)};
            _objDAL.RunProc("Ec_Opr_NewGC_Get_Next_GC_No", sqlPara, ref _ds);

            return _ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable Check_Valid_Attach_GC_No(int Attach_GC_No, int Menu_Item_Id)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara ={ 
                  _objDAL.MakeInParams("@Attached_GC_No", SqlDbType.Int,0,Attach_GC_No),
                  _objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                  _objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                  _objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                  _objDAL.MakeInParams("@MenuItem_Id",SqlDbType.Int,0,Menu_Item_Id)};

            _objDAL.RunProc("EC_Opr_NewGC_Check_Valid_Attach_GC_No", sqlPara, ref _ds);

            return _ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable Is_STAbatmentRequired(int Service_Type_Id, DateTime Applicable_From)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] objSqlParam ={
            _objDAL.MakeInParams("@Service_Type_Id", SqlDbType.Int,0,Service_Type_Id),
            _objDAL.MakeInParams("@date", SqlDbType.DateTime,0,Applicable_From)};

            _objDAL.RunProc("EC_Opr_NewGC_Is_ST_Abatment_Required", objSqlParam, ref _ds);

            return _ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable Get_GC_Discount(DateTime bookingDate, int clientIDCnr, int clientIDCne, int clientIDCBilling,
            int branchIDBkg, int branchIDDly, int paymenTypeId,
            decimal qty, decimal freight, int ConsigneeDeliveryAreaID, int IsRegularConsignor, int IsRegularConsignee)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] objSqlParam ={
                _objDAL.MakeInParams("@BookingDate", SqlDbType.DateTime,0,bookingDate),
                _objDAL.MakeInParams("@ClientIDCnr", SqlDbType.Int,0,clientIDCnr),
                _objDAL.MakeInParams("@ClientIDCne", SqlDbType.Int,0,clientIDCne),
                _objDAL.MakeInParams("@ClientIDBilling", SqlDbType.Int,0,clientIDCBilling),
                _objDAL.MakeInParams("@BranchIDBkg", SqlDbType.Int,0,branchIDBkg),
                _objDAL.MakeInParams("@BranchIDDly", SqlDbType.Int,0,branchIDDly),
                _objDAL.MakeInParams("@PaymentTypeID", SqlDbType.Int,0,paymenTypeId),
                _objDAL.MakeInParams("@Qty", SqlDbType.Decimal,0,qty),
                _objDAL.MakeInParams("@Freight", SqlDbType.Decimal,0,freight),
                _objDAL.MakeInParams("@ConsigneeDeliveryAreaID", SqlDbType.Int,0,ConsigneeDeliveryAreaID),
                _objDAL.MakeInParams("@IsClientIDCnrRegular", SqlDbType.Bit,0,IsRegularConsignor),
                _objDAL.MakeInParams("@IsClientIDCneRegular", SqlDbType.Bit,0,IsRegularConsignee) 
            };

            _objDAL.RunProc("Get_GC_Discount", objSqlParam, ref _ds);

            return _ds.Tables[0];
        }

        //Added : Hemant 13 Jul 2020
        [AjaxPro.AjaxMethod]
        public static DataTable CheckDuplicateeWayBillNo(int GC_ID, string eWayBillNo)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] objSqlParam = {
                                    _objDAL.MakeInParams("@GC_ID", SqlDbType.Int,0,GC_ID),
                                    _objDAL.MakeInParams("@eWayBillNo",SqlDbType.VarChar,15,eWayBillNo),
                                    _objDAL.MakeOutParams("@Is_Duplicate", SqlDbType.Bit, 1)};

            _objDAL.RunProc("dbo.EC_Opr_Check_Duplicate_eWayBillNo", objSqlParam, ref _ds);

            return _ds.Tables[0];

        }
    }
}