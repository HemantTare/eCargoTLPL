using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
/// <summary>
/// Summary description for TransitDaysStateToStateModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class TransitDaysStateToStateModel:IModel 
    {
        private ITransitDaysStateToStateView objITransitDaysStateToStateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public TransitDaysStateToStateModel(ITransitDaysStateToStateView transitDaysStateToStateView)
        {
            objITransitDaysStateToStateView = transitDaysStateToStateView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objITransitDaysStateToStateView.keyID)
                                           };
            objDAL.RunProc("EF_Master_PM_Template_Read", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_TransitDaysStateToState_FillValues", ref objDS);
            SetTableName(new string[] { "StateMaster",                                        
                                        "VehicleTypeMaster"                                        
                                         }
                                       );
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
                                   objDAL.MakeInParams("@Transit_Days_ID", SqlDbType.Int, 0, objITransitDaysStateToStateView.keyID),
                                   objDAL.MakeInParams("@From_State_ID", SqlDbType.Int, 0,objITransitDaysStateToStateView.FromStateID ),
                                   objDAL.MakeInParams("@To_State_ID", SqlDbType.Int, 0,objITransitDaysStateToStateView.ToStateID),
                                   objDAL.MakeInParams("@Transit_Days", SqlDbType.Int, 0,objITransitDaysStateToStateView.TransitDays),
                                   objDAL.MakeInParams("@Distance_In_Km", SqlDbType.Int, 0,objITransitDaysStateToStateView.DistanceInKM), 
                                   objDAL.MakeInParams("@Vehicle_Type_ID", SqlDbType.Int, 0,objITransitDaysStateToStateView.VehicleID),                                                                        
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};

             objDAL.RunProc("EC_Mst_TransitDaysStateToState_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

        
    }
}