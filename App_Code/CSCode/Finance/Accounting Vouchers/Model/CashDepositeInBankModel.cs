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
    class CashDepositeInBankModel : IModel
    {
        private ICashDepositeInBankView objICashDepositeInBankView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;


        public CashDepositeInBankModel(ICashDepositeInBankView CashDepositeInBankView)
        {
            objICashDepositeInBankView = CashDepositeInBankView;
        }

        public DataSet FillValues()
        {

            objDAL.RunProc("[EC_FA_Mst_Voucher_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,objICashDepositeInBankView.keyID),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,_yearCode)
                                         };

  
            objDAL.RunProc("[EC_FA_Mst_Voucher_ReadValues]", objSqlParam, ref objDS);
             

            Common.SetTableName(new string[] { "Voucher", "VoucherDetails", "VoucherCostCentre", "VoucherBillByBill", "MstCostCentre", "MstRefType" }, objDS);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS.Tables["VoucherDetails"]);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Cost_Centre_ID" }, objDS.Tables["VoucherCostCentre"]);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Ref_No" }, objDS.Tables["VoucherBillByBill"]);

            return objDS;
        }

        public DataSet Get_BranchRateParameter()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, _mainId)};

            objDAL.RunProc("Ec_Opr_GC_Get_Branch_Rate_Parameter", objSqlParam, ref objDS);
            return objDS;
        }    

        public Message Save()
        {
            Message objMessage = new Message();
            int _menuItemId = Common.GetMenuItemId();
            int _menuItemCode = Common.GetMenuItemCode();

            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@MainId", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@VoucherDate",SqlDbType.DateTime,0,objICashDepositeInBankView.VoucherDate),
                                            objDAL.MakeInParams("@RefNo",SqlDbType.VarChar,25,objICashDepositeInBankView.RefNo),
                                            objDAL.MakeInParams("@LedgerID",SqlDbType.Int,0,objICashDepositeInBankView.LedgerId), 
                                            objDAL.MakeInParams("@PaidTo",SqlDbType.VarChar,25,objICashDepositeInBankView.PaidToWhom),
                                            objDAL.MakeInParams("@Details",SqlDbType.VarChar,500,objICashDepositeInBankView.Details),  
                                            objDAL.MakeInParams("@Amount",SqlDbType.Decimal,0,objICashDepositeInBankView.Amount),  
                                            objDAL.MakeInParams("@UserId", SqlDbType.Int,0,_userID)                  
                                           };


            objDAL.RunProc("FA_Opr_CashDepositeInBank_Voucher_Save", objSqlParam);
            

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            //objICashExpensesView.errorMessage = objMessage.message;
            string _Msg="";
            if (objMessage.messageID !=0)
            {
                objICashDepositeInBankView.errorMessage = objMessage.message;
            }
            else if (objMessage.messageID == 0)
            {
                
                _Msg = "Saved SuccessFully";

                if (objICashDepositeInBankView.Flag == "SaveAndNew")
                {
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                                               ClassLibraryMVP.Util.EncryptString("Finance/Accounting Vouchers/FrmCashDepositeInBank.aspx?Menu_Item_Id=" +
                                               ClassLibraryMVP.Util.EncryptInteger(_menuItemId) + "&Mode=" + Mode));
                }
                else if (objICashDepositeInBankView.Flag == "SaveAndExit")
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
