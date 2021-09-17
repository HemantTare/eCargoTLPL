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
    class TempoPaymentModel : IModel
    {
        private ITempoPaymentView objITempoPaymentView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;

        public TempoPaymentModel(ITempoPaymentView TempoPaymentView)
        {
            objITempoPaymentView = TempoPaymentView;
        }

        public DataSet GetVendorLedgerDetails(int Vehicle_Id)
        {
            SqlParameter[] objSqlParam =
            { 
                objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0,Vehicle_Id)};

            objDAL.RunProc("EC_FA_TempoPayment_Get_VendorLedger_Details", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillDetails()
        {
            SqlParameter[] objSqlParam =
            { 
                objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,objITempoPaymentView.LedgerId),
            objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int, 0,objITempoPaymentView.keyID)};

            objDAL.RunProc("EC_FA_TempoPayment_Fill_Details", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillValues()
        {

            objDAL.RunProc("[EC_FA_Mst_Voucher_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,objITempoPaymentView.keyID)};


            objDAL.RunProc("[EC_FA_Opr_Payment_To_TempoOwner_ReadValues]", objSqlParam, ref objDS);


            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();
            int _menuItemId = Common.GetMenuItemId();
            int _menuItemCode = Common.GetMenuItemCode();

            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            objDAL.MakeInParams("@Voucher_ID",SqlDbType.Int,0,objITempoPaymentView.keyID),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@MainId", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@VoucherDate",SqlDbType.DateTime,0,objITempoPaymentView.VoucherDate),
                                            objDAL.MakeInParams("@RefNo",SqlDbType.VarChar,25,objITempoPaymentView.RefNo),
                                            objDAL.MakeInParams("@LedgerID",SqlDbType.Int,0,objITempoPaymentView.LedgerId), 
                                            objDAL.MakeInParams("@PaidTo",SqlDbType.VarChar,100,objITempoPaymentView.PaidToWhom),
                                            objDAL.MakeInParams("@Details",SqlDbType.VarChar,500,objITempoPaymentView.Details),  
                                            objDAL.MakeInParams("@Amount",SqlDbType.Decimal,0,objITempoPaymentView.Amount),  
                                            objDAL.MakeInParams("@IsCheque",SqlDbType.Bit,0,Convert.ToBoolean(objITempoPaymentView.Is_CashCheque)),  
                                            objDAL.MakeInParams("@ChequeNo",SqlDbType.VarChar,25,objITempoPaymentView.ChequeNo),  
                                            objDAL.MakeInParams("@ChequeBank",SqlDbType.VarChar,100,""),  
                                            objDAL.MakeInParams("@ChequeDate",SqlDbType.DateTime,0,objITempoPaymentView.ChequeDate),
                                            objDAL.MakeInParams("@UserId", SqlDbType.Int,0,_userID)                  
                                           };


            objDAL.RunProc("FA_Opr_TempoPayment_Voucher_Save", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            //objICashBankReceiptView.errorMessage = objMessage.message;
            string _Msg = "";
            if (objMessage.messageID != 0)
            {
                objITempoPaymentView.errorMessage = objMessage.message;
            }
            else if (objMessage.messageID == 0)
            {

                _Msg = "Saved SuccessFully";

                if (objITempoPaymentView.Flag == "SaveAndNew")
                {
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                                               ClassLibraryMVP.Util.EncryptString("Finance/Accounting Vouchers/FrmTempoPayment.aspx?Menu_Item_Id=" +
                                               ClassLibraryMVP.Util.EncryptInteger(_menuItemId) + "&Mode=" + Mode));
                }
                else if (objITempoPaymentView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
                }
                else
                {
                    Common.DisplayErrors(objMessage.messageID);
                }
            }

            return objMessage;
        }


    }
}
