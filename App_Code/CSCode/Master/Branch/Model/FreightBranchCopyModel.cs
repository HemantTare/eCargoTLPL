using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
/// <summary>
/// Summary description for FreightBranchCopyModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class FreightBranchCopyModel : IModel
    {
        private IFreightBranchCopyView objIFreightBranchCopyView;
        private DAL objDAL = new DAL();
        private DataSet objDS;        

        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;

        public FreightBranchCopyModel(IFreightBranchCopyView freightBranchCopyView)
        {
            objIFreightBranchCopyView = freightBranchCopyView;
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
            objDAL.RunProc("dbo.EC_Mst_FreightBranchCopy_FillValues", ref objDS);
            SetTableName(new string[] { "BranchMaster",                                        
                                        "AreaMaster",                                        
                                        "CommodityMaster"
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
                                   objDAL.MakeInParams("@Area_Id", SqlDbType.Int, 0,objIFreightBranchCopyView.AreaID ),
                                   objDAL.MakeInParams("@Copy_From_Branch_Id", SqlDbType.Int, 0,objIFreightBranchCopyView.CopyFromBranchID),
                                   objDAL.MakeInParams("@Commodity_ID", SqlDbType.Int, 0,objIFreightBranchCopyView.CommodityID),
                                   objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0,objIFreightBranchCopyView.FromBranchID),
                                   objDAL.MakeInParams("@VTrans_Normal_Rate", SqlDbType.Decimal, 0,objIFreightBranchCopyView.FreightRate),                                    
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                    objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID)           
                        };

            objDAL.RunProc("EC_Mst_FreightBranchCopy_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

    }
}