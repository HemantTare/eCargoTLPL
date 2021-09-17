using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ContractTermsModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class ContractTermsModel : IModel
    {
        private IContractTermsView objIContractTermsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public ContractTermsModel(IContractTermsView contractTermsView)
        {
            objIContractTermsView = contractTermsView;
            
        }       
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Mst_ContractTerms_FillValues", ref objDS);
            SetTableName(new string[] { "TermsGrid",
                                        "TermsMaster"
                                         }
                                       );
            return objDS;

        }
        
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Contract_ID", SqlDbType.Int, 0, objIContractTermsView.keyID)
                                           };
            objDAL.RunProc("EC_Mst_ContractTerms_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet GetTermDescription()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Term_ID", SqlDbType.Int, 0, objIContractTermsView.TermsID)
                                           };
            objDAL.RunProc("EC_Mst_ContractTerms_GetDescriptionOnTermChanged", objSqlParam, ref objDS);
            return objDS;

        }
        private void SetTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                   objDAL.MakeInParams("@Freight_Id", SqlDbType.Int, 0,objIContractTermsView.keyID),                                   
                                   objDAL.MakeInParams("@VTrans_Special_Rate", SqlDbType.VarChar, 0, objIContractTermsView.SessionContractTermsGrid)                                   
                                  // objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                 //  objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID)
                                   };


           // objDAL.RunProc("EC_Mst_Freight_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}