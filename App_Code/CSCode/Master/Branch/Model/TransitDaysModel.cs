using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
 
/// <summary>
/// Summary description for TransitDaysModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class TransitDaysModel:IModel 
    {
        private ITransitDaysView objITransitDaysView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public TransitDaysModel(ITransitDaysView TransitDaysView)
        {
            objITransitDaysView = TransitDaysView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objITransitDaysView.keyID)
                                           };
            objDAL.RunProc("EF_Master_PM_Template_Read", objSqlParam, ref objDS);
            return objDS;            
             
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_TransitDays_FillValues", ref objDS);
            SetTableName(new string[] { "CityMaster",
                                        "StateMaster",                                                        
                                        "VehicleTypeMaster",
                                        "TransitDaysGrid"
                                         }
                                       );
            return objDS;

        }

        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@State_ID", SqlDbType.Int, 0, objITransitDaysView.StateID),
                                           objDAL.MakeInParams("@Vehicle_Type_ID", SqlDbType.Int, 0, objITransitDaysView.VehicleID),
                                           objDAL.MakeInParams("@From_City_ID", SqlDbType.Int, 0, objITransitDaysView.CityID) 
                                           };
            objDAL.RunProc("EC_Mst_TransitDays_FillGrid", objSqlParam, ref objDS);
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
                                   objDAL.MakeInParams("@Transit_Days_ID", SqlDbType.Int, 0, objITransitDaysView.TransitDaysID),
                                   objDAL.MakeInParams("@From_City_ID", SqlDbType.Int, 0,objITransitDaysView.CityID),
                                   objDAL.MakeInParams("@To_City_ID", SqlDbType.Int, 0,objITransitDaysView.ToCityID),
                                   objDAL.MakeInParams("@Transit_Days", SqlDbType.Int, 0,objITransitDaysView.TransitDays),
                                   objDAL.MakeInParams("@Distance_In_Km", SqlDbType.Int, 0,objITransitDaysView.DistanceInKM), 
                                   objDAL.MakeInParams("@Vehicle_Type_ID", SqlDbType.Int, 0,objITransitDaysView.VehicleID),                                      
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};


            objDAL.RunProc("EC_Mst_TransitDays_Save", objSqlParam);

            objMessage.messageID  = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message  = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }

}