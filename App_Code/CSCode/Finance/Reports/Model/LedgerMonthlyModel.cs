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
/// Summary description for LedgerMonthlyModel
/// </summary>
namespace Raj.EC.FinanceModel
{
	 public class LedgerMonthlyModel : IModel
    {
        private ILedgerMonthlyView objILedgerMonthlyView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();       
        private int _DivisionId = UserManager.getUserParam().DivisionId;


         public LedgerMonthlyModel(ILedgerMonthlyView ledgerMonthlyView)
        {
            objILedgerMonthlyView = ledgerMonthlyView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objILedgerMonthlyView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar  , 0, objILedgerMonthlyView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0,objILedgerMonthlyView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@StartDate ",SqlDbType.DateTime,0,objILedgerMonthlyView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objILedgerMonthlyView.EndDate),
                                          objDAL.MakeInParams("@LedgerId",SqlDbType.Int,0,objILedgerMonthlyView.Ledger_Id)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Ledger_Monthly]", objSqlParam, ref objDS);
            return objDS;

        }

    }

}
