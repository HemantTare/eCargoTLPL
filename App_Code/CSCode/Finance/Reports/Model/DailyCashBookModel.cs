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
/// Summary description for DailyCashBookModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class DailyCashBookModel : IModel
    {
        private IDailyCashBookView objIDailyCashBookView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public DailyCashBookModel(IDailyCashBookView DailyCashBookView)
        {
            objIDailyCashBookView = DailyCashBookView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objIDailyCashBookView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar  , 0, objIDailyCashBookView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0,objIDailyCashBookView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@StartDate ",SqlDbType.DateTime,0,objIDailyCashBookView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objIDailyCashBookView.EndDate),
                                          objDAL.MakeInParams("@LedgerId",SqlDbType.Int,0,objIDailyCashBookView.Ledger_Id)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Ledger_Voucher]", objSqlParam, ref objDS);
            return objDS;

        }

	}
}
