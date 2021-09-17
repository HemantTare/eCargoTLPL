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
/// Summary description for ProfitAndLossAccountModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class ProfitAndLossAccountModel : IModel
    {
        private IProfitAndLossAccountView objIProfitAndLossAccountView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();       
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public ProfitAndLossAccountModel(IProfitAndLossAccountView profitAndLossAccountView)
        {
            objIProfitAndLossAccountView = profitAndLossAccountView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objIProfitAndLossAccountView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar  , 5,objIProfitAndLossAccountView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0,objIProfitAndLossAccountView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@StartDate ",SqlDbType.DateTime,0,objIProfitAndLossAccountView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objIProfitAndLossAccountView.EndDate)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Profit_And_Loss]", objSqlParam, ref objDS);
            return objDS;


        }

    }
}
