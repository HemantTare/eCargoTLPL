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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;


/// <summary>
/// Summary description for LedgerOutstandingModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class LedgerOutstandingModel : IModel
    {
        private ILedgerOutstandingView objILedgerOutstandingView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public LedgerOutstandingModel(ILedgerOutstandingView ledgerOutstandingView)
        {
            objILedgerOutstandingView = ledgerOutstandingView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues1(ref Decimal OnAccount)
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objILedgerOutstandingView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 0,objILedgerOutstandingView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0, objILedgerOutstandingView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objILedgerOutstandingView.EndDate),
                                          objDAL.MakeInParams("@LedgerGroupId",SqlDbType.Int,0,objILedgerOutstandingView.LedgerGroupId),
                                          objDAL.MakeOutParams("@OnAccount",SqlDbType.Decimal,0)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Outstanding_Ledger]", objSqlParam, ref objDS);

            OnAccount = Convert.ToDecimal(objSqlParam[6].Value);
            return objDS;


        }
        public DataSet ReadValues()
        {
            return objDS;
        }

    }
}
