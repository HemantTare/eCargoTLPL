using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;


/// <summary>
/// Summary description for FreightBranchModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class FreightBranchModel : IModel
    {
        private IFreightBranchView objIFreightBranchView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public FreightBranchModel(IFreightBranchView freightBranchView)
        {
            objIFreightBranchView = freightBranchView;
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
            objDAL.RunProc("dbo.EC_Mst_FreightBranch_FillValues", ref objDS);
            SetTableName(new string[] { "BranchMaster",
                                        "AreaMaster",                                                                                                
                                        "FreightBranchGrid",                                                                                                
                                        "Commodity"
                                         }
                                       );
            return objDS;

        }

        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0, objIFreightBranchView.BranchID),
                objDAL.MakeInParams("@Area_ID", SqlDbType.Int, 0, objIFreightBranchView.AreaID),
                objDAL.MakeInParams("@Commodity_ID", SqlDbType.Int, 0, objIFreightBranchView.CommodityID),
                objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,_divisionID)  
                                        };
            objDAL.RunProc("EC_Mst_FreightBranch_FillGrid", objSqlParam, ref objDS);
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
                                   objDAL.MakeInParams("@Freight_Id", SqlDbType.Int, 0,objIFreightBranchView.FreightID),                                   
                                   objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0, objIFreightBranchView.BranchID),
                                   objDAL.MakeInParams("@To_Branch_Id", SqlDbType.Int, 0, objIFreightBranchView.ToBranchID),                                    
                                   objDAL.MakeInParams("@Commodity_ID", SqlDbType.Int, 0, objIFreightBranchView.CommodityID),                                    
                                   objDAL.MakeInParams("@VTrans_Normal_Rate", SqlDbType.Decimal, 0, objIFreightBranchView.FreightRate),
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                   objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) 
                
                            };


            objDAL.RunProc("EC_Mst_FreightBranch_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}