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
/// Summary description for NetReceivablePayableBillwiseModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class NetReceivablePayableBillwiseModel : IModel
    {
        private INetReceivablePayableBillwiseView objINetReceivablePayableBillwiseView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public NetReceivablePayableBillwiseModel(INetReceivablePayableBillwiseView netReceivablePayableBillwiseView)
        {
            objINetReceivablePayableBillwiseView = netReceivablePayableBillwiseView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objINetReceivablePayableBillwiseView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 0,objINetReceivablePayableBillwiseView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0,objINetReceivablePayableBillwiseView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objINetReceivablePayableBillwiseView.EndDate),
                                          objDAL.MakeInParams("@LedgerGroupId",SqlDbType.Int,0,objINetReceivablePayableBillwiseView.LedgerGroupId),
                                          objDAL.MakeInParams("@LedgerId",SqlDbType.Int,0,objINetReceivablePayableBillwiseView.LedgerId),
                                          objDAL.MakeInParams("@IsCondensed",SqlDbType.Bit,1,objINetReceivablePayableBillwiseView.IsCondensed)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Outstanding_Ledger_BillWise_Details]", objSqlParam, ref objDS);
            return objDS;


        }

    }
}
