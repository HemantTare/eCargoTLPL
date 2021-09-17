using System;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.SalesView;


/// <summary>
/// Summary description for ClientGroupModel
/// </summary>
namespace Raj.EC.SalesModel
{
    class ClientGroupModel : IModel
    {
        private IClientGroupView objIClientGroupView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public ClientGroupModel(IClientGroupView clientGroupView)
        {
            objIClientGroupView = clientGroupView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ClientGroupId", SqlDbType.Int, 0,objIClientGroupView.keyID)};
            objDAL.RunProc("[dbo].[EC_Mst_ClientGroup_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetParentGroupValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ClientGroupId", SqlDbType.Int, 0,objIClientGroupView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_Client_Group_Fill]",objSqlParam ,ref objDS);
            
            return objDS;
        }
        public DataSet GetLedgerGroupValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@parent_ledger_group_id", SqlDbType.Int, 0,objIClientGroupView.ParentGroupId)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_Debtors_Ledger_Group_Fill]",objSqlParam, ref objDS);
            return objDS;
        }
        public bool  IsClientGroupChanged()
        {
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Is_Changed", SqlDbType.Bit, 0), 
                                          objDAL.MakeInParams("@Client_Group_ID", SqlDbType.Int, 0,objIClientGroupView.keyID)
                            
                                         };
            objDAL.RunProc("EC_Mst_ClientGroup_IsClientGroupChanged", objSqlParam, ref objDS);
            if (objSqlParam[0].Value == DBNull.Value)
            {
                objSqlParam[0].Value = 0;
            }
            return Convert.ToBoolean(objSqlParam[0].Value);
        }
        
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                       objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                       objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                       objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objIClientGroupView.keyID),
                       objDAL.MakeInParams("@ClientGroupName", SqlDbType.VarChar, 50,objIClientGroupView.ClientGroupName), 
                       objDAL.MakeInParams("@ParentGroupId", SqlDbType.Int,0, objIClientGroupView.ParentGroupId), 
                       objDAL.MakeInParams("@UseExistingLedgerGroupId",SqlDbType.Bit,0,objIClientGroupView.LedgerGroupForRadio),                        
                       objDAL.MakeInParams("@LedgerGroupId", SqlDbType.Int,0,  objIClientGroupView.LedgerGroupId),
                       objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID)};

            objDAL.RunProc("[dbo].[EC_Mst_ClientGroup_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }		
	}
}
