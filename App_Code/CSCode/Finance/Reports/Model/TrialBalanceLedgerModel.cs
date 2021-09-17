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
/// Summary description for TrialBalanceModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class TrialBalanceLedgerModel : IModel
    {
        private ITrialBalanceLedgerView objITrialBalanceView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;
        private string _CompanyName = UserManager.getUserParam().CompanyName;


        public TrialBalanceLedgerModel(ITrialBalanceLedgerView trialBalanceView)
        {
            objITrialBalanceView = trialBalanceView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objITrialBalanceView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar  , 0,objITrialBalanceView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0, objITrialBalanceView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@StartDate ",SqlDbType.DateTime,0,objITrialBalanceView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objITrialBalanceView.EndDate)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Trial_Balance_Ledger]", objSqlParam, ref objDS);
            return objDS;


         }

    }
}
