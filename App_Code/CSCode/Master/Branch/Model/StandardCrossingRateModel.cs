using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for StandardCrossingRateModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class StandardCrossingRateModel:IModel 
    {
        private IStandardCrossingRateView objIStandardCrossingRateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public StandardCrossingRateModel(IStandardCrossingRateView standardCrossingRateView)
        {
            objIStandardCrossingRateView = standardCrossingRateView;
        }
        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objIFreightView.keyID)
            //                               };
            //objDAL.RunProc("EF_Master_PM_Template_Read", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_StandardCrossingRate_FillValues", ref objDS);
            SetTableName(new string[] { "BranchMaster",
                                        "AreaMaster",                                                                                                
                                        "StandardCrossingGrid"
                                         }
                                       );
            return objDS;

        }

        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Area_ID", SqlDbType.Int, 0, objIStandardCrossingRateView.AreaID),                                           
                                           objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0, objIStandardCrossingRateView.BranchID) 
                                           };
            objDAL.RunProc("EC_Mst_StandardCrossingRate_FillGrid", objSqlParam, ref objDS);
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
                                   objDAL.MakeInParams("@Freight_Id", SqlDbType.Int, 0,objIStandardCrossingRateView.FreightID),                                   
                                   objDAL.MakeInParams("@Hamali", SqlDbType.Decimal, 0, objIStandardCrossingRateView.Hamali),
                                   objDAL.MakeInParams("@Hire_Rate", SqlDbType.Decimal, 0, objIStandardCrossingRateView.HireRate),
                                   objDAL.MakeInParams("@Total", SqlDbType.Decimal, 0, objIStandardCrossingRateView.Total), 
                                   objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0,objIStandardCrossingRateView.BranchID),
                                   objDAL.MakeInParams("@To_Branch_Id", SqlDbType.Int, 0,objIStandardCrossingRateView.ToBranchID),                                   
                                   objDAL.MakeInParams("@Distance", SqlDbType.Int, 0,objIStandardCrossingRateView.DistanceInKM),                                    
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};


            objDAL.RunProc("EC_Mst_StandardCrossingRate_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}