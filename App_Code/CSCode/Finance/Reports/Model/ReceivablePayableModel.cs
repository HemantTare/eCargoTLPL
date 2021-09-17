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
/// Summary description for ReceivablePayableModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class ReceivablePayableModel : IModel
    {
        private IReceivablePayableView objIReceivablePayableView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;
       


        public ReceivablePayableModel(IReceivablePayableView receivablePayableView)
        {
            objIReceivablePayableView = receivablePayableView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objIReceivablePayableView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 0,objIReceivablePayableView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0, objIReceivablePayableView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
					  objDAL.MakeInParams("@StartDate",SqlDbType.DateTime,0,objIReceivablePayableView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objIReceivablePayableView.EndDate),
                                          objDAL.MakeInParams("@LedgerGroupId",SqlDbType.Int,0,objIReceivablePayableView.LedgerGroupId),
                                          objDAL.MakeInParams("@IsReceivables",SqlDbType.Bit,1,objIReceivablePayableView.IsReceivable)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Outstanding_Receivables_Payables]", objSqlParam, ref objDS);

         
            return objDS;


        }
       

    }
}

