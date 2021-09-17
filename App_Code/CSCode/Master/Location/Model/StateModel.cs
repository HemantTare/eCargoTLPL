using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for StateModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class  StateModel : IModel
    {
        private IStateView objIStateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;
        //private int _userID = UserManager.getUserParam().UserId;

        public StateModel(IStateView stateView)
        {
            objIStateView = stateView;
        }
        public DataSet GetRegionValues()
        {

            objDAL.RunProc("[dbo].[EC_Mst_State_FillValues]", ref objDS);
            return objDS;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@StateId", SqlDbType.Int, 0, objIStateView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_State_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
       

        public DataSet ReadValues1()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@StateId", SqlDbType.Int, 0, objIStateView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_State_ReadCheckedValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetFormType()
        {
            objDAL.RunProc("[dbo].[EC_Mst_State_FillGridValues]", ref objDS);
            return objDS;
        }

        public DataSet GetCountryNameOnRegionSelection()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIStateView.RegionId)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_State_FillCountryLabelValue]", objSqlParam, ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@StateId",SqlDbType.Int,0, objIStateView.keyID),
                                               objDAL.MakeInParams("@StateName", SqlDbType.VarChar, 50,objIStateView.StateName), 
                                               objDAL.MakeInParams("@NsdlCode", SqlDbType.NVarChar,15, objIStateView.NsdlCode),
                                                objDAL.MakeInParams("@StateCode", SqlDbType.VarChar,5, objIStateView.StateCode),
                                               objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIStateView.RegionId),
                                               objDAL.MakeInParams("@SessionStateFormDetails",SqlDbType.Xml,0,objIStateView.SessionStateFormDetails),
                                               objDAL.MakeInParams("@UserId", SqlDbType.Int,0,  _userID)
                                              
                                         };


            objDAL.RunProc("EC_Mst_State_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
       
    }

}
