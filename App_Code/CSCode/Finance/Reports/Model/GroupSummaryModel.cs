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
/// Summary description for GroupSummaryModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class GroupSummaryModel : IModel
    {
        private IGroupSummaryView objIGroupSummaryView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;
        private string _CompanyName = UserManager.getUserParam().CompanyName;


        public GroupSummaryModel(IGroupSummaryView groupSummaryView)
        {
            objIGroupSummaryView = groupSummaryView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objIGroupSummaryView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar  , 0, objIGroupSummaryView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0, objIGroupSummaryView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@StartDate ",SqlDbType.DateTime,0,objIGroupSummaryView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objIGroupSummaryView.EndDate),
                                          objDAL.MakeInParams("@MainLedgerGroupId",SqlDbType.Int,0,objIGroupSummaryView.Ledger_Id)
                                          };
            objDAL.RunProc("FA_Rpt_Group_Summary ", objSqlParam, ref objDS);
            return objDS;

        }

	}
}
