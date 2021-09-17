using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;


/// <summary>
/// Summary description for CompanyModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CompanyModel : IModel
    {
        private ICompanyView objICompanyView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        private int _userID = UserManager.getUserParam().UserId;
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;


        public CompanyModel(ICompanyView companyView)
        {
            objICompanyView = companyView;
        }

        public DataSet ReadValues()
        {

            return objDS;
        }
        public DataSet FillGrid()
        {
            objDAL.RunProc("[dbo].[EC_Master_Company_Fill_Values]", ref objDS);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Booking_Type_ID", "Division_ID", "Payment_Type_ID" }, objDS.Tables[3]);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Booking_Type_ID", "Division_ID" }, objDS.Tables[4]);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Booking_Type_ID", "Division_ID" }, objDS.Tables[5]);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Booking_Type_ID", "Division_ID" }, objDS.Tables[6]);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Booking_Type_ID", "Division_ID" }, objDS.Tables[7]);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Booking_Type_ID", "Division_ID" }, objDS.Tables[8]);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@CompanyId",SqlDbType.Int,0,objICompanyView.CompanyGeneralDetailsView.CompanyId),
                                               objDAL.MakeInParams("@CompanyName",SqlDbType.VarChar,100,objICompanyView.CompanyGeneralDetailsView.CompanyName),
                                               objDAL.MakeInParams("@MailingName",SqlDbType.VarChar,100,objICompanyView.CompanyGeneralDetailsView.MailingName),
                                               objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,objICompanyView.CompanyGeneralDetailsView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@AddressLine2", SqlDbType.VarChar,100,objICompanyView.CompanyGeneralDetailsView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@CityId", SqlDbType.Int,0,objICompanyView.CompanyGeneralDetailsView.AddressView.CityId),
                                               objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,objICompanyView.CompanyGeneralDetailsView.AddressView.PinCode),
                                               objDAL.MakeInParams("@StdCode", SqlDbType.NVarChar,15,objICompanyView.CompanyGeneralDetailsView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,objICompanyView.CompanyGeneralDetailsView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,20,objICompanyView.CompanyGeneralDetailsView.AddressView.Phone2),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objICompanyView.CompanyGeneralDetailsView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@EmailId", SqlDbType.VarChar,100,objICompanyView.CompanyGeneralDetailsView.AddressView.EmailId),
                                               objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
                                               objDAL.MakeInParams("@Tax_Assessment_No", SqlDbType.VarChar, 50,objICompanyView.CompanyTDSFBTDetailsView.TaxAssessmentNumber),
                                               objDAL.MakeInParams("@Income_Tax_Circle", SqlDbType.VarChar,50,objICompanyView.CompanyTDSFBTDetailsView.IncomeTaxCircle),
                                                objDAL.MakeInParams("@Deductor_Type", SqlDbType.VarChar, 50,objICompanyView.CompanyTDSFBTDetailsView.DeductorType),
                                                objDAL.MakeInParams("@Designation" ,SqlDbType.VarChar,50,objICompanyView.CompanyTDSFBTDetailsView.Designation),
                                                objDAL.MakeInParams("@Employee_Id ", SqlDbType.Int, 0,objICompanyView.CompanyTDSFBTDetailsView.PersonResponsible),
                                                objDAL.MakeInParams("@Allow_FBT_Category_Selection", SqlDbType.Bit, 1,objICompanyView.CompanyTDSFBTDetailsView.IsAllowSelectionFBTCategory),
                                                objDAL.MakeInParams("@Pan_No", SqlDbType.VarChar, 50,objICompanyView.CompanyTDSFBTDetailsView.PanNo),
                                                objDAL.MakeInParams("@Assessee_Type_Id", SqlDbType.Int, 0,objICompanyView.CompanyTDSFBTDetailsView.AssesseeType),
                                                objDAL.MakeInParams("@Is_Surcharge_Applicable", SqlDbType.Bit, 1, objICompanyView.CompanyTDSFBTDetailsView.IsSurchargeApplicable),
                                                objDAL.MakeInParams("@Assessee_Category_Id", SqlDbType.Int,1,objICompanyView.CompanyTDSFBTDetailsView.AssesseeCategory), 
                                                objDAL.MakeInParams("@IsActivateDivision",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsActivateDivision),
                                                objDAL.MakeInParams("@IsAccTansferRequired",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsAccTransferRequired),
                                                objDAL.MakeInParams("@IsColoaderBusiness",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsActivateCoLoaderBusiness),
                                                objDAL.MakeInParams("@StdBasicFreightUnit",SqlDbType.Int,0,objICompanyView.CompanyParametersView.StdBasicFreightUnit),
                                                objDAL.MakeInParams("@StdBasicFreightRate",SqlDbType.Decimal,0,objICompanyView.CompanyParametersView.StdFreightRateForSundry),
                                                objDAL.MakeInParams("@IsBookOwnTruckHire",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsBookOwnTruckHire),
                                                objDAL.MakeInParams("@IsMarketTruckLedgerAccTruckWise",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsMarketTruckLedgerAccTruckWise),
                                                objDAL.MakeInParams("@IsAttachedTruckLedgerAccTruckWise",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsAttachedTruckLedgerAccTruckWise),
                                                objDAL.MakeInParams("@IsManagedTruckLedgerAccTruckWise",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsManagedTruckLedgerAccTruckWise),
                                                objDAL.MakeInParams("@IsTreatBookingIncomeAsAdvance",SqlDbType.Bit,1,objICompanyView.CompanyBookingParametersView.IsTreatBookingIncomeAdvIncome),
                                                objDAL.MakeInParams("@IsToBeBilledAccGCWise",SqlDbType.Bit,1,objICompanyView.CompanyBookingParametersView.IsToBilledAccountingGCWise),
                                                objDAL.MakeInParams("@IsBookingMoneyReceiptRequired",SqlDbType.Bit,1,objICompanyView.CompanyBookingParametersView.IsBookingMoneyReceiptRequired),
                                                objDAL.MakeInParams("@BookingDivision",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.DivisionId),
                                                objDAL.MakeInParams("@BookingParametersXml",SqlDbType.Xml,0,objICompanyView.CompanyBookingParametersView.BookingParametersDetails),
                                                objDAL.MakeInParams("@TripHireParametersXml",SqlDbType.Xml,0,objICompanyView.CompanyTripHireParametersView.TripHireParametersDetailsXML),
                                                objDAL.MakeInParams("@ATHParametersXml",SqlDbType.Xml,0,objICompanyView.CompanyTripHireParametersView.ATHDetailsXML),
                                                objDAL.MakeInParams("@DeliveryParametersXml",SqlDbType.Xml,0,objICompanyView.CompanyDeliveryView.CompanyDeliveryDetails),
                                                objDAL.MakeInParams("@LocalCollectionParametersXml",SqlDbType.Xml,0,objICompanyView.LocalCollectionVoucherView.LocalCollectionVoucherXML),
                                                objDAL.MakeInParams("@DoorDeliveryParametersXml",SqlDbType.Xml,0,objICompanyView.LocalCollectionVoucherView.DoorDeliveryExpenseVoucherXML),
                                                objDAL.MakeInParams("@LHPONatureOfPayment",SqlDbType.Int,0,objICompanyView.CompanyTripHireParametersView.LHPONatureOfPaymentId),
                                                objDAL.MakeInParams("@IsMemoSeriesRequired",SqlDbType.Bit,1,objICompanyView.CompanyCaptionView.IsMemoSeriesRequired),
                                                objDAL.MakeInParams("@IsLHPOSeriesRequired",SqlDbType.Bit,1,objICompanyView.CompanyCaptionView.IsLHPOSeriesRequired),
                                                objDAL.MakeInParams("@GCCaption",SqlDbType.VarChar,50,objICompanyView.CompanyCaptionView.GCCaption),
                                                objDAL.MakeInParams("@LHPOCaption",SqlDbType.VarChar,50,objICompanyView.CompanyCaptionView.LHPOCaption),
                                                objDAL.MakeInParams("@IsAlsRequired",SqlDbType.Bit,1,objICompanyView.CompanyCaptionView.IsAlsRequired),
                                                objDAL.MakeInParams("@IsTasRequired",SqlDbType.Bit,1,objICompanyView.CompanyCaptionView.IsTasRequired),
                                                objDAL.MakeInParams("@MinDiff",SqlDbType.Int,0,objICompanyView.CompanyCaptionView.MinDiffTASandAUS),
                                                objDAL.MakeInParams("@HOLedgerId",SqlDbType.Int,0,objICompanyView.CompanyGeneralDetailsView.HOLedger),
                                                objDAL.MakeInParams("@PFALedgerId",SqlDbType.Int,0,objICompanyView.CompanyGeneralDetailsView.PFALedger),
                                                objDAL.MakeInParams("@MinDiffMemo",SqlDbType.Int,0,objICompanyView.CompanyParametersView.MinDiffMEMOandTAS),
                                                objDAL.MakeInParams("@IsPartLoadingRequired",SqlDbType.Bit,0,objICompanyView.CompanyParametersView.IsPartLoadingRequired),
                                                objDAL.MakeInParams("@IsGCNumberEditable",SqlDbType.Bit,0,objICompanyView.CompanyParametersView.IsGCNumberEditable),
                                                objDAL.MakeInParams("@ClientCode",SqlDbType.VarChar,50,objICompanyView.CompanyGeneralDetailsView.ClientCode),
                                                objDAL.MakeInParams("@IsContractRequiredForTBBGC",SqlDbType.Bit,1,objICompanyView.CompanyParametersView.IsContractRequiredForTBBGC),
                                                objDAL.MakeInParams("@ShortTermBillLedgerID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.ShortTermBillLedgerID),
                                                objDAL.MakeInParams("@IsTreatAdvanceForOwnTruckAsExpense",SqlDbType.Bit,1,objICompanyView.CompanyTripHireParametersView.IsTreatAdvanceForOwnTruckAsExpense),
                                                objDAL.MakeInParams("@TripExpenseLedgerId",SqlDbType.Int,0,objICompanyView.CompanyTripHireParametersView.TripExpenseLedgerId),
                                                objDAL.MakeInParams("@IsDebitTodelivery",SqlDbType.Bit,0,objICompanyView.CompanyBookingParametersView.IsDebitTodelivery),
                                                objDAL.MakeInParams("@PayforBookigBranchID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.PayforBookigBranchID),
                                                objDAL.MakeInParams("@PayforCrossingBranchID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.PayforCrossingBranchID),
                                                objDAL.MakeInParams("@PayforDeliveryBranchID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.PayforDeliveryBranchID),
                                                objDAL.MakeInParams("@DeliveryCommisionIncomeID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.DeliveryCommisionIncomeID),
                                                objDAL.MakeInParams("@DeliveryCommisionExpenseID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.DeliveryCommisionExpenseID),
                                                objDAL.MakeInParams("@LHPOOtherChargesExpenseID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.LHPOOtherChargesExpenseID),
                                                objDAL.MakeInParams("@LHPOOtherChargesPaybleID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.LHPOOtherChargesPaybleID),
                                                objDAL.MakeInParams("@LorryPayble_ATH_BTH_ID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.LorryPayble_ATH_BTH_ID),
                                                objDAL.MakeInParams("@UpcountryCostAC_ID",SqlDbType.Int,0,objICompanyView.CompanyBookingParametersView.UpcountryCostAC_ID),
                                                objDAL.MakeInParams("@HOCashLedgerId",SqlDbType.Int,0,objICompanyView.CompanyGeneralDetailsView.HOCashLedger),
                                                objDAL.MakeInParams("@HOBankLedgerId",SqlDbType.Int,0,objICompanyView.CompanyGeneralDetailsView.HOBankLedger),
                                                };
            objDAL.RunProc("Ec_Mst_CompanyDetails_Save", objSqlParam);
            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objICompanyView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;


        }
    }
}

