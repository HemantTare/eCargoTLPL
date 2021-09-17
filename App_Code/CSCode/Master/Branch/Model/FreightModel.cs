using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;


/// <summary>
/// Summary description for FreightModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class FreightModel:IModel 
    {
        private IFreightView objIFreightView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public FreightModel(IFreightView freightView)
        {
            objIFreightView = freightView;
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
            objDAL.RunProc("dbo.EC_Mst_Freight_FillValues", ref objDS);
            SetTableName(new string[] { "CityMaster",
                                        "StateMaster",                                                                                                
                                        "FreightGrid"
                                         }
                                       );
            return objDS;

        }

        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@State_ID", SqlDbType.Int, 0, objIFreightView.StateID),                                           
                                           objDAL.MakeInParams("@From_City_ID", SqlDbType.Int, 0, objIFreightView.CityID),
                                           objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) 
                                            
                                           };
            objDAL.RunProc("EC_Mst_Freight_FillGrid", objSqlParam, ref objDS);
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
                                   objDAL.MakeInParams("@Freight_Id", SqlDbType.Int, 0,objIFreightView.FreightID),                                   
                                   objDAL.MakeInParams("@VTrans_Special_Rate", SqlDbType.Decimal, 0, objIFreightView.SpecialRate),
                                   objDAL.MakeInParams("@VTrans_Normal_Rate", SqlDbType.Decimal, 0, objIFreightView.NormalRate),
                                   objDAL.MakeInParams("@VTrans_FTL_Rate", SqlDbType.Decimal, 0, objIFreightView.FTLRate), 
                                   objDAL.MakeInParams("@From_City_Id", SqlDbType.Int, 0,objIFreightView.CityID),
                                   objDAL.MakeInParams("@To_City_Id", SqlDbType.Int, 0,objIFreightView.ToCityID),                                   
                                   objDAL.MakeInParams("@Distance_In_Km", SqlDbType.Decimal, 0,objIFreightView.DistanceInKM),                                    
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                   objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID)
                                   };


            objDAL.RunProc("EC_Mst_Freight_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

         
            return objMessage;
        }
    }
}