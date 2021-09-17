using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;
/// <summary>
/// Summary description for ClientFinanceModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class ClientFinanceModel : IModel
    {
        private IClientFinanceView objIClientFinanceView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public ClientFinanceModel(IClientFinanceView ClientFinanceView)
        {
            objIClientFinanceView = ClientFinanceView;
        }


        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@flag", SqlDbType.VarChar, 20, "Finance") };

            objDAL.RunProc("dbo.EC_Master_Client_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillLedgerOnClientGroupSelection()
        {
            SqlParameter[] objSqlParam ={
               objDAL.MakeInParams("@Client_Group_ID", SqlDbType.Int, 0, objIClientFinanceView.ClientGroupId)};

            objDAL.RunProc("dbo.EC_Master_Client_FillLedgerOnClientGroupSelection", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillLedgerDetails()
        {
            SqlParameter[] objSqlParam ={
               objDAL.MakeInParams("@Ledger_ID", SqlDbType.Int, 0, objIClientFinanceView.LedgerID)};

            objDAL.RunProc("dbo.EC_Master_Client_FillLedgerDetails", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@flag", SqlDbType.VarChar, 20, "Finance"),
                objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, objIClientFinanceView.keyID) };
            objDAL.RunProc("dbo.EC_Master_Client_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}