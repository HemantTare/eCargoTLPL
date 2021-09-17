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
/// Summary description for ClientGeneralModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class ClientGeneralModel : IModel
    {
        private IClientGeneralView objIClientGeneralView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public ClientGeneralModel(IClientGeneralView ClientGeneralView)
        {
            objIClientGeneralView = ClientGeneralView;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@flag", SqlDbType.VarChar, 20, "General") };

            objDAL.RunProc("dbo.EC_Master_Client_FillValues",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@flag", SqlDbType.VarChar, 20, "General") ,
                objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, objIClientGeneralView.keyID) };
            objDAL.RunProc("dbo.EC_Master_Client_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public DataSet CopyRegClient()
        {
            SqlParameter[] objSqlParam ={
                objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, objIClientGeneralView.Regular_Client_Id  ) 
            };

            objDAL.RunProc("EC_Mst_Regular_Client_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }
        public bool IsCheck_Duplicate()
        {
            bool Is_DuplicateName;
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@is_duplicate", SqlDbType.Bit, 1),
                                          objDAL.MakeInParams("@ClientName", SqlDbType.VarChar, 100,objIClientGeneralView.ClientName),
                                    objDAL.MakeInParams("@Regular_Client_ID",SqlDbType.Int,1,objIClientGeneralView.Regular_Client_Id )};

            objDAL.RunProc("dbo.EC_Mst_Check_Duplicate_Client", objSqlParam);

            Is_DuplicateName = Util.String2Bool(objSqlParam[0].Value.ToString());
            return Is_DuplicateName;
        }
        public bool IsValidateGeneralDiscount()
        {
            bool is_valid;
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@is_valid", SqlDbType.Bit, 1),  
                                    objDAL.MakeInParams("@Client_ID",SqlDbType.Int,1,objIClientGeneralView.keyID)};

            objDAL.RunProc("dbo.EC_Mst_Check_IsValidateGeneralDiscount", objSqlParam);

            is_valid = Util.String2Bool(objSqlParam[0].Value.ToString());
            return is_valid;
        }


        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}
