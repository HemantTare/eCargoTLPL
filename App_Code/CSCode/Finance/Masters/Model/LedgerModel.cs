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
using Raj.EC.FinanceView;

namespace Raj.EC.FinanceModel
{
    class LedgerModel : IModel
    {
        private ILedgerView objILedgerView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;

        //private int _userID =1;
        //private int _yearCode = 8;
        //private string _hierarchyCode = "HO";
        //private int _mainId = 1;
        
        
        public LedgerModel(ILedgerView LedgerView)
        {
            objILedgerView = LedgerView;
        }

        //public DataSet FillValues()
        //{
        //    objDAL.RunProc("[EC_FA_Mst_Ledger_FillValues]", ref objDS);
        //    return objDS;
        //}

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objILedgerView.keyID)
                                         };

            objDAL.RunProc("[EC_FA_Mst_Ledger_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                        objDAL.MakeInParams("@Ledger_Id",SqlDbType.Int,0,objILedgerView.keyID),
                                        objDAL.MakeInParams("@Ledger_Name",SqlDbType.VarChar,100,objILedgerView.getLedgerGeneralView.LedgerName),
                                        objDAL.MakeInParams("@Alias",SqlDbType.VarChar,100,objILedgerView.getLedgerGeneralView.Alias),
                                        objDAL.MakeInParams("@Ledger_Group_Id",SqlDbType.Int,0,Convert.ToInt32(objILedgerView.getLedgerGeneralView.LedgerUnderId)),
                                        //objDAL.MakeInParams("@Reserved_Ledger_Group_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.LedgerName),
                                        //objDAL.MakeInParams("@Primary_Ledger_Group_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.LedgerName),
                                        objDAL.MakeInParams("@Maintain_Bill_By_Bill",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsMaintainBillByBill),
                                        objDAL.MakeInParams("@Default_Credit_Period",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.DefaultCreditPeriod),
                                        objDAL.MakeInParams("@Credit_Limit",SqlDbType.Decimal,0,objILedgerView.getLedgerGeneralView.CreditLimit),
                                        //objDAL.MakeInParams("@Opening_Balance",SqlDbType.Decimal,0,objILedgerView.getLedgerGeneralView.),
                                        //objDAL.MakeInParams("@Closing_Balance",SqlDbType.Decimal,0,objILedgerView.getLedgerGeneralView.LedgerName),
                                        objDAL.MakeInParams("@Mailing_Name",SqlDbType.VarChar,100,objILedgerView.Name),
                                        objDAL.MakeInParams("@Add1",SqlDbType.VarChar,100,objILedgerView.getAddressView.AddressLine1),
                                        objDAL.MakeInParams("@Add2",SqlDbType.VarChar,100,objILedgerView.getAddressView.AddressLine2),
                                        objDAL.MakeInParams("@City_Id",SqlDbType.VarChar,50,objILedgerView.getAddressView.CityId),
                                        objDAL.MakeInParams("@Pin_Code",SqlDbType.NVarChar,30,objILedgerView.getAddressView.PinCode),
                                        objDAL.MakeInParams("@Contact_Person",SqlDbType.VarChar,100,objILedgerView.ContactPerson),
                                        objDAL.MakeInParams("@Phone",SqlDbType.NVarChar,40,objILedgerView.getAddressView.Phone1),
                                        objDAL.MakeInParams("@Fax",SqlDbType.NVarChar,40,objILedgerView.getAddressView.FaxNo),
                                        objDAL.MakeInParams("@Email",SqlDbType.VarChar,100,objILedgerView.getAddressView.EmailId),
                                        objDAL.MakeInParams("@Bank_Ac_No",SqlDbType.NVarChar,100,objILedgerView.getLedgerGeneralView.ACNo),
                                        objDAL.MakeInParams("@Income_Tax_No",SqlDbType.VarChar,100,objILedgerView.getLedgerGeneralView.Income_Tax_No),
                                        objDAL.MakeInParams("@TIN_Sales_Tax_No",SqlDbType.VarChar,100,objILedgerView.getLedgerGeneralView.TIN_Sales_Tax_No),
                                        objDAL.MakeInParams("@Service_Tax_No",SqlDbType.VarChar,50,objILedgerView.getLedgerGeneralView.ServiceTaxNo),
                                        objDAL.MakeInParams("@Service_Tax_Reg_Date",SqlDbType.DateTime,0,objILedgerView.getLedgerGeneralView.ServiceTaxRegDate),
                                        objDAL.MakeInParams("@Notes",SqlDbType.VarChar,250,objILedgerView.Note),
                                        objDAL.MakeInParams("@Type_Of_Duty_Tax",SqlDbType.VarChar,25,objILedgerView.getLedgerGeneralView.TypeOfDutyTax),
                                        objDAL.MakeInParams("@Nature_Of_Payment_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.NatureOfPaymentId),
                                        objDAL.MakeInParams("@Is_TDS_Applicable",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsTDSApplicable),
                                        objDAL.MakeInParams("@TDS_Deductee_Type_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.TDSDeducteeTypeId),
                                        objDAL.MakeInParams("@TDS_Deductee_Type_Name",SqlDbType.VarChar,100,objILedgerView.getLedgerGeneralView.TDSDeducteeTypeName),
                                        objDAL.MakeInParams("@Is_Lower_Deduction",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsLowerDeduction),
                                        objDAL.MakeInParams("@Section_Number",SqlDbType.NVarChar,30,objILedgerView.getLedgerGeneralView.SectionNumber),
                                        objDAL.MakeInParams("@TDS_Lower_Rate",SqlDbType.Decimal,0,objILedgerView.getLedgerGeneralView.TDSLowerRate),
                                        objDAL.MakeInParams("@Ignore_Exemption_Limit",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsIgnoreExemptionLimit),
                                        objDAL.MakeInParams("@Service_Tax_Category_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.ServiceTaxCategoryId),
                                        objDAL.MakeInParams("@Is_Service_Tax_Applicable",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsServiceTaxApplicable),
                                        objDAL.MakeInParams("@Is_Exempted",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsExempted),
                                        objDAL.MakeInParams("@Notification_Detail",SqlDbType.VarChar,255,objILedgerView.getLedgerGeneralView.NotificationDetail),
                                        objDAL.MakeInParams("@Is_FBT_Applicable",SqlDbType.Bit,0,objILedgerView.getLedgerGeneralView.IsFBTApplicable),
                                        objDAL.MakeInParams("@FBT_Category_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.FBTCategoryId),
                                        objDAL.MakeInParams("@User_Id",SqlDbType.Int,0,_userID),
                                        objDAL.MakeInParams("@Effective_Date_Of_Bank_Reco",SqlDbType.DateTime,0,objILedgerView.getLedgerGeneralView.DateOfBankReco),
                                        objDAL.MakeInParams("@Bank_Reco_Hierarchy_Code",SqlDbType.VarChar,10,objILedgerView.getLedgerGeneralView.SelectedHierarchy),
                                        objDAL.MakeInParams("@Bank_Reco_Main_Id",SqlDbType.Int,0,objILedgerView.getLedgerGeneralView.MainId),

                                        objDAL.MakeInParams("@Division_XML",SqlDbType.Xml,0,objILedgerView.getDivisionView.getDivisonXML)
                                    };


                                        objDAL.RunProc("EC_FA_Mst_Ledger_Save", objSqlParam);


                                        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
                                        objMessage.message = Convert.ToString(objSqlParam[1].Value);


       

           
            objILedgerView.errorMessage = objMessage.message;
            string _Msg = "";
            if (objMessage.messageID == 2627)
            {
                objILedgerView.errorMessage = "Duplicate Ledger Name";
            }
            else if (objMessage.messageID == 0)
            {
                _Msg = "Saved SuccessFully";
                objILedgerView.errorMessage = _Msg;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;
         
        }

        //public DataSet FillLocation()
        //{
        //    SqlParameter[] objSqlParam = { 
        //                                       objDAL.MakeInParams("@SelectedHierarchy", SqlDbType.VarChar,10,objILedgerView.SelectedHierarchy)
        //                                 };

        //    objDAL.RunProc("[EC_FA_Mst_Ledger_FillLocation]", objSqlParam, ref objDS);
        //    return objDS;
        //}
    }
}
