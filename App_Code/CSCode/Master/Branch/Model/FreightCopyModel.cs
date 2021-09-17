using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
/// <summary>
/// Summary description for FreightCopyModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class FreightCopyModel:IModel 
    {
        private IFreightCopyView objIFreightCopyView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public FreightCopyModel(IFreightCopyView freightCopyView)
        {
            objIFreightCopyView = freightCopyView;
        }
        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objITransitDaysStateToStateView.keyID)
            //                               };
            //objDAL.RunProc("EF_Master_PM_Template_Read", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_FreightCopy_FillValues", ref objDS);
            SetTableName(new string[] { "StateMaster",                                        
                                        "CityMaster"                                        
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
                                   objDAL.MakeInParams("@State_Id", SqlDbType.Int, 0,objIFreightCopyView.StateID ),
                                   objDAL.MakeInParams("@Copy_From_City_Id", SqlDbType.Int, 0,objIFreightCopyView.CopyFromCityID),
                                   objDAL.MakeInParams("@From_City_Id", SqlDbType.Int, 0,objIFreightCopyView.FromCityID),
                                   objDAL.MakeInParams("@Rate", SqlDbType.Decimal, 0,objIFreightCopyView.Rate),                                    
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                   objDAL.MakeInParams("@Division_Id ", SqlDbType.Int, 0, _divisionID) 
                                   
                            };

              objDAL.RunProc("EC_Mst_FreightCopy_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

    }
}