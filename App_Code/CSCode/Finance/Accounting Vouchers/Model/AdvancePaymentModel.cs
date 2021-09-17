using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView ;
using Raj.EC;
using ClassLibraryMVP;

namespace Raj.EC.FinanceModel
{
    class AdvancePaymentModel : IModel
    {
        private IAdvancePaymentView objIAdvancePaymentView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        
        //private int _userID = 53;
        //private int _yearCode = 8;
        //private string _hierarchyCode = "HO";
        //private int _mainId = 1;
        //private int _Division_Id = 1;


        private int _userID = (int)UserManager.getUserParam().UserId;// Param.getUserParam().UserID;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;// Param.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;// Param.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;//.getUserParam().MainId;
        private int _Division_Id = (int)Convert.ToInt32(UserManager.getUserParam().DivisionId);

        public AdvancePaymentModel(IAdvancePaymentView AdvancePaymentView)
        {
            objIAdvancePaymentView = AdvancePaymentView;
        }

        public DataSet ReadValues()
        {

            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();


            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@ERROR_CODE", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                               objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
                                               objDAL.MakeInParams("@MainId", SqlDbType.Int, 0,_mainId),
                                               objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,_yearCode),
                                               objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_Division_Id), 
                                               //objDAL.MakeInParams("@Key_Id", SqlDbType.Int, 0, objIAdvancePaymentView.keyID),
                                               objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime,0,objIAdvancePaymentView.PaymentDate),
                                               objDAL.MakeInParams("@Ref_No", SqlDbType.VarChar,25,objIAdvancePaymentView.RefNo),
                                               objDAL.MakeInParams("@CashBankLedger_Id", SqlDbType.VarChar,100,objIAdvancePaymentView.CashBankLedgerId),
                                               objDAL.MakeInParams("@Cheque_No", SqlDbType.Int,0,Util.String2Int(objIAdvancePaymentView.ChequeNo)),
                                               objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIAdvancePaymentView.PartyLedgerId),
                                               objDAL.MakeInParams("@Party_Ref_No", SqlDbType.VarChar,25,objIAdvancePaymentView.RefNoPartyLedger),
                                               objDAL.MakeInParams("@TotalDebit", SqlDbType.Decimal,0,objIAdvancePaymentView.Amount),
                                               objDAL.MakeInParams("@TotalCredit", SqlDbType.Decimal,0,objIAdvancePaymentView.Amount),
                                               objDAL.MakeInParams("@TDS_Amount", SqlDbType.Decimal,0,Util.String2Decimal(objIAdvancePaymentView.GetITDSPaymentDeductionView.NetTDSDeducted.Trim())),
                                               objDAL.MakeInParams("@TDS_Ledger_Id", SqlDbType.Int,0, objIAdvancePaymentView.TDSLedgerId),
                                               objDAL.MakeInParams("@TDS_Ref_No", SqlDbType.VarChar,25,objIAdvancePaymentView.RefNoTDSLedger),
                                               objDAL.MakeInParams("@Narration", SqlDbType.VarChar,1000,objIAdvancePaymentView.Narration)
                                          };


                objDAL.RunProc("[FA_TDS_Advance_Payment_Save]", objSqlParam);
            

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIAdvancePaymentView.errorMessage = objMessage.message;

            if (objMessage.messageID == 0)
            {
                string _Msg;
               _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
               
            }

            return objMessage;
        }


        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 10, _hierarchyCode),
                                               objDAL.MakeInParams("@MainId", SqlDbType.Int, 0, _mainId),
                                                objDAL.MakeInParams("@Division_Id",SqlDbType.Int,1,_Division_Id)
                                         };

            objDAL.RunProc("[EC_FA_Common_AdvancePayment_FillValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public bool ValidateRefNo(string ladgerRefNo,int ledger_Id)
        {
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
                                               objDAL.MakeInParams("@Main_Id", SqlDbType.Int ,1, _mainId  ),
                                               objDAL.MakeInParams("@Division_Id", SqlDbType.Int ,1,_Division_Id ),
                                               objDAL.MakeInParams("@LedgerRefNo", SqlDbType.VarChar,20,ladgerRefNo),
                                               objDAL.MakeInParams("@LedgerId", SqlDbType.Int,0,ledger_Id),
                                          };

            objDAL.RunProc("[FA_TDS_Advance_Payment_ValidateRefNo]", objSqlParam, ref objDS);

            if (Util.String2Int(objDS.Tables[0].Rows[0]["RowCount"].ToString()) > 0)
            {
                return false;
            }
            else { return true;}

        }

         
        }
    }
 
