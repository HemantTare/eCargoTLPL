using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for StandardCrossingRateCopyModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class StandardCrossingRateCopyModel : IModel
    {
        private IStandardCrossingRateCopyView objIStandardCrossingRateCopyView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public StandardCrossingRateCopyModel(IStandardCrossingRateCopyView standardCrossingRateCopyView)
        {
            objIStandardCrossingRateCopyView = standardCrossingRateCopyView;
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
            objDAL.RunProc("dbo.EC_Mst_StandardCrossingRateCopy_FillValues", ref objDS);
            SetTableName(new string[] { "BranchMaster",                                        
                                        "AreaMaster"                                        
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
                                   objDAL.MakeInParams("@Area_Id", SqlDbType.Int, 0,objIStandardCrossingRateCopyView.AreaID ),
                                   objDAL.MakeInParams("@Copy_From_Branch_Id", SqlDbType.Int, 0,objIStandardCrossingRateCopyView.CopyFromBranchID),
                                   objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0,objIStandardCrossingRateCopyView.FromBranchID),
                                   objDAL.MakeInParams("@Hamali", SqlDbType.Decimal, 0,objIStandardCrossingRateCopyView.HamaliRate),
                                   objDAL.MakeInParams("@HireRate", SqlDbType.Decimal, 0,objIStandardCrossingRateCopyView.HireRate), 
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};

            objDAL.RunProc("EC_Mst_StandardCrossingRateCopy_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

    }
}