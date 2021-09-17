using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ContractGeneralModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class ContractGeneralModel:IModel 
    {
        private IContractGeneralView objIContractGeneralView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
       // private int _userID = UserManager.getUserParam().UserId;
      //  private int _divisionID = UserManager.getUserParam().DivisionId;


        public ContractGeneralModel(IContractGeneralView contractGeneralView)
        {
            objIContractGeneralView = contractGeneralView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Contract_ID", SqlDbType.Int, 0, objIContractGeneralView.keyID)
                                           };
            objDAL.RunProc("EC_Mst_ContractFreightGeneral_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_ContractGeneral_FillValues", ref objDS);
            SetTableName(new string[] { "BranchMaster"
                                         }
                                       );
            return objDS;

        }
        public DataSet FillGCRiskType()
        {
            objDAL.RunProc("dbo.EC_Mst_ContractGeneral_FillGCRiskType", ref objDS);
            return objDS;
        }
        public DataSet FillClientOnBranchChanged()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, objIContractGeneralView.BranchID)
                                           };
            objDAL.RunProc("EC_Mst_ContractGeneral_FillClientsOnBranchChanged", objSqlParam, ref objDS);
            return objDS;

        }
       
        private void SetTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }
        //Added :Anita On: 05 Feb 09
        public bool IsContractNameDuplicate()
        {
            bool IsContractNameDuplicate;
            SqlParameter[] objSqlParam = {
                                    objDAL.MakeInParams("@Contract_ID", SqlDbType.Int,0,objIContractGeneralView.keyID),
                                    objDAL.MakeInParams("@Contract_Name",SqlDbType.VarChar,0,objIContractGeneralView.ContractName),
                                    objDAL.MakeOutParams("@Is_Duplicate", SqlDbType.Bit, 1)};

            objDAL.RunProc("dbo.EC_Mst_Check_Duplicate_ContractNames", objSqlParam);

            IsContractNameDuplicate = Util.String2Bool(objSqlParam[2].Value.ToString());
            return IsContractNameDuplicate;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                   objDAL.MakeInParams("@Freight_Id", SqlDbType.Int, 0,objIContractGeneralView.keyID),                                   
                                   objDAL.MakeInParams("@VTrans_Special_Rate", SqlDbType.Decimal, 0, objIContractGeneralView.ContractNo),
                                   objDAL.MakeInParams("@VTrans_Normal_Rate", SqlDbType.Decimal, 0, objIContractGeneralView.Weight),
                                   objDAL.MakeInParams("@VTrans_FTL_Rate", SqlDbType.Decimal, 0, objIContractGeneralView.ValidUptoDate), 
                                   objDAL.MakeInParams("@From_City_Id", SqlDbType.Int, 0,objIContractGeneralView.ValidFromDate),
                                   objDAL.MakeInParams("@To_City_Id", SqlDbType.Int, 0,objIContractGeneralView.POMaxLimit),                                   
                                   objDAL.MakeInParams("@Distance_In_Km", SqlDbType.Decimal, 0,objIContractGeneralView.PODate),                                    
                                  // objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                 //  objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID)
                                   };


            objDAL.RunProc("EC_Mst_Freight_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}