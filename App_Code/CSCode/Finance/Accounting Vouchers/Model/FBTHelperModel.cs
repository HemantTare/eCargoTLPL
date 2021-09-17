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
/// Summary description for FBTHelperModel
/// </summary>

namespace Raj.EC.FinanceModel
{
    public class FBTHelperModel : IModel
    {

        private DataSet objDS;
        private IFBTHelperView objFBTHelperView;
        private DAL _objDAL = new DAL();

        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;

        //private int _userID = 1;
        //private int _yearCode = 8;
        //private string _hierarchyCode = "HO";
        //private int _mainId = 1;
        //private int _divisionId = 1;

       

        public FBTHelperModel(IFBTHelperView fbtHelperView)
        {
            objFBTHelperView = fbtHelperView;
        }

        public DataSet FillFBTLedgerValues()
        {
            SqlParameter[] objSqlParam = {
                                            _objDAL.MakeInParams("@DivisionId", SqlDbType.Int,0,_divisionId)
                                         };
            _objDAL.RunProc("FA_FBTHelper_FillFBTLedgerValues",objSqlParam ,ref objDS);
            return objDS;
        }      

       

         public Message Save()
        {
            Message objMsg = new Message();                          
            return objMsg;
        }
        public DataSet FillCashBankValues()
        {
            SqlParameter[] objSqlParam = {  _objDAL.MakeInParams("@MainId", SqlDbType.Int, 0,_mainId),
                                            _objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar , 0,_hierarchyCode),
                                            _objDAL.MakeInParams("@DivisionId", SqlDbType.Int,0,_divisionId)
                                         };
                _objDAL.RunProc("FA_TDSHelper_FillCashBankValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = {   _objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                            _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar , 5,_hierarchyCode),
                                            _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,_mainId),
                                            _objDAL.MakeInParams("@Cash_Bank_Ledger_Id", SqlDbType.Int, 0,objFBTHelperView.CashBankAc),
                                            _objDAL.MakeInParams("@FBT_Ledger_Id", SqlDbType.Int, 0,objFBTHelperView.FBTLedger),
                                            _objDAL.MakeInParams("@From_Date", SqlDbType.DateTime, 0,objFBTHelperView.FromDateValue),
                                            _objDAL.MakeInParams("@To_Date", SqlDbType.DateTime, 0,objFBTHelperView.ToDateValue),
                                            
                                         };

            _objDAL.RunProc("FA_Get_FBT_Ledger_Balance", objSqlParam, ref objDS);            
            return objDS; 
        }

	}
}

