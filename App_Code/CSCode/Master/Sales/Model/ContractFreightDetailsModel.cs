using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ContractFreightDetailsModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class ContractFreightDetailsModel : IModel
    {
        private IContractFreightDetailsView objIContractFreightDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public ContractFreightDetailsModel(IContractFreightDetailsView contractFreightDetailsView)
        {
            objIContractFreightDetailsView = contractFreightDetailsView;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Mst_ContractFreightDetails_FillValues", ref objDS);

            SetTableName(new string[]{"UnitOfFreight", "FreightBasis", "SubUnit"});
            return objDS;

        }
        public DataSet FillServiceLocation()
        {
            objDAL.RunProc("EC_Mst_ContractFreightDetails_FillServiceLocation", ref objDS);
            return objDS;

        }
        
        private void SetTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }
       
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Contract_ID", SqlDbType.Int, 0, objIContractFreightDetailsView.keyID)
                                           };
            objDAL.RunProc("EC_Mst_ContractFreightDetails_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Contract_ID", SqlDbType.Int, 0, objIContractFreightDetailsView.keyID) 
                                            
                                           };
            objDAL.RunProc("EC_Mst_ContractFreightDetails_FillGrid", objSqlParam, ref objDS);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "SrNo" }, objDS.Tables[0]); 
            return objDS;

        }
        public DataSet GetOtherChargesDetails()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Contract_ID", SqlDbType.Int, 0, objIContractFreightDetailsView.keyID)
                                        };
            objDAL.RunProc("EC_Mst_ContractFreightDetails_GetOtherChargesForFreightRateDetails", objSqlParam, ref objDS);
            return objDS;

        }
        
        public DataSet FillUnitFreightDropdown()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Freight_Unit_ID", SqlDbType.Int, 0, objIContractFreightDetailsView.UnitOfFreightID)
                                           };
            objDAL.RunProc("EC_Mst_ContractFreightDetails_FillFreightDetailsOnUnitofFreightChanged", objSqlParam, ref objDS);
            return objDS;

        }

        public DataSet FillSubUnitFreightDropdown()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Freight_Sub_Unit_ID", SqlDbType.Int, 0, objIContractFreightDetailsView.SubUnitID)
                                           };
            objDAL.RunProc("EC_Mst_ContractFreightDetails_FillFreightSubUnitOnSubUnitChanged", objSqlParam, ref objDS);
            return objDS;

        }
        
        
        
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                   objDAL.MakeInParams("@Freight_Id", SqlDbType.Int, 0,objIContractFreightDetailsView.keyID),                                   
                                   objDAL.MakeInParams("@VTrans_Special_Rate", SqlDbType.VarChar, 0, objIContractFreightDetailsView.FreightBasisID)                                   
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