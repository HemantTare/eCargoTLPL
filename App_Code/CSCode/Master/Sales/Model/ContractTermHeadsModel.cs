using System;
using System.Data;
using System.Data.SqlClient;

using Raj.EC.MasterView;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
// =============================================
// Author:		<Author,,Ankit>
// Create date: <Create Date,,13-10-2008>
/// Summary description for ContractTermHeads Model
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class ContractTermHeadsModel :IModel 
    {
        private IContractTermHeadsView _iContractTermHeadsView;
        private DAL _objDal = new DAL();
        private DataSet _objDS;

        public ContractTermHeadsModel(IContractTermHeadsView iContractTermHeadsView)
        {
            _iContractTermHeadsView = iContractTermHeadsView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDal .MakeInParams("@Term_Id", SqlDbType.Int, 0, _iContractTermHeadsView.keyID )
            };
            _objDal.RunProc("EC_Master_Contract_TermHeads_ReadValues", objSqlParam, ref _objDS);
            return _objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {_objDal.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   _objDal.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                   _objDal.MakeInParams("@Term_Id", SqlDbType.Int, 0,_iContractTermHeadsView .keyID),                                   
                                   _objDal.MakeInParams("@Term_Head", SqlDbType.VarChar , 60, _iContractTermHeadsView .TermHead ),
                                   _objDal.MakeInParams("@Description ", SqlDbType.VarChar , 100, _iContractTermHeadsView.Description ),
                                   _objDal.MakeInParams("@Is_Active", SqlDbType.Bit  , 0, 1 ) ,
                                   _objDal.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
                                   };
            _objDal.RunProc("[EC_Master_Contract_TermHeads_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);          

            return objMessage;
        }
    }
}