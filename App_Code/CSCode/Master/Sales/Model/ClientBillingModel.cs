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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for ClientBillingModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class ClientBillingModel : IModel
    {
        private IClientBillingView objIClientBillingView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public ClientBillingModel(IClientBillingView ClientBillingView)
        {
            objIClientBillingView = ClientBillingView;
        }


        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@flag", SqlDbType.VarChar, 20, "Billing") };

            objDAL.RunProc("dbo.EC_Master_Client_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@flag", SqlDbType.VarChar, 20, "Billing"),
                objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, objIClientBillingView.keyID) };
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
