using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CreditMemoReceiptModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{

    public class CreditMemoReceiptModel:IModel 
    {
        private DataSet objDS;
        private ICreditMemoReceiptView objICreditMemoReceiptView;
        private DAL _objDAL = new DAL();

        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;

        public CreditMemoReceiptModel(ICreditMemoReceiptView creditMemoReceiptView)
        {
            objICreditMemoReceiptView = creditMemoReceiptView;        
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = {  
                                        _objDAL.MakeInParams("@Credit_Memo_Receipt_ID", SqlDbType.Int,0,objICreditMemoReceiptView.keyID)                                           
                                            
                                         };

            _objDAL.RunProc("EC_FA_Opr_CreditMemoReceipt_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = {  _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objICreditMemoReceiptView.PartyNameID),
                                            _objDAL.MakeInParams("@Id", SqlDbType.Int , 0,objICreditMemoReceiptView.keyID),
                                            _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0,_mainId)
                                          

                                         };

            _objDAL.RunProc("EC_FA_Opr_CreditMemoReceipt_FillGrid", objSqlParam, ref objDS);

            return objDS;
        }
        public Message Save()
        {
            Message objMsg = new Message();

         
                    SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                    _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                    _objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode),
                    _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_divisionId),
                     _objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,2,_hierarchyCode ),
                     _objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int ,0,Raj.EC.Common.GetMenuItemId()),                                            
                     _objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,_mainId ),
                    _objDAL.MakeInParams("@Credit_Memo_Receipt_ID",SqlDbType.Int,0,objICreditMemoReceiptView.keyID),
                    _objDAL.MakeInParams("@Credit_Memo_Receipt_No",SqlDbType.Int,0,objICreditMemoReceiptView.keyID),
                    _objDAL.MakeInParams("@Credit_Memo_Receipt_No_For_Print",SqlDbType.NVarChar,50,objICreditMemoReceiptView.ReceiptNo),
                    _objDAL.MakeInParams("@Credit_Memo_Receipt_Date",SqlDbType.DateTime,0,objICreditMemoReceiptView.ReceiptDate),
                    _objDAL.MakeInParams("@Credit_Memo_Branch_ID",SqlDbType.Int,0,_mainId),                    
                    _objDAL.MakeInParams("@Ledger_ID",SqlDbType.Int,0,objICreditMemoReceiptView.PartyNameID),
                    _objDAL.MakeInParams("@Total_Receipt_Amount",SqlDbType.Decimal,0,objICreditMemoReceiptView.TotalAmount),
                    _objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,objICreditMemoReceiptView.CashAmount),
                    _objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,objICreditMemoReceiptView.ChequeAmount),
                    _objDAL.MakeInParams("@Cheque_No",SqlDbType.NVarChar,20,objICreditMemoReceiptView.ChequeNo),
                    _objDAL.MakeInParams("@Cheque_Date",SqlDbType.DateTime,0,objICreditMemoReceiptView.ChequeDate),
                    _objDAL.MakeInParams("@Cheque_Bank",SqlDbType.NVarChar,200,objICreditMemoReceiptView.BankName),
                    _objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,100,objICreditMemoReceiptView.Remarks),  
                     _objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,objICreditMemoReceiptView.GetDetailsXML.ToLower()),
                    _objDAL.MakeInParams("@Voucher_ID",SqlDbType.Int,0,0),
                    _objDAL.MakeInParams("@Is_Approved",SqlDbType.Bit,0,1),
                    _objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                    
                    };


                    _objDAL.RunProc("EC_FA_Opr_CreditMemoReceipt_Save", objSqlParam);


                    objMsg.messageID = Convert.ToInt32(objSqlParam[0].Value);
                    objMsg.message = Convert.ToString(objSqlParam[1].Value);



             if (objMsg.messageID == 0)
            {
                string _Msg;             
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMsg;

        }
    }
}