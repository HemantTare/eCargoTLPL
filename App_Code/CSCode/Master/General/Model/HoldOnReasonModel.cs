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
/// Summary description for HoldOnReasonModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class HoldOnReasonModel :IModel
    {
        private IHoldOnReasonView objIHoldOnReasonView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int User_Id = UserManager.getUserParam().UserId;
        public HoldOnReasonModel(IHoldOnReasonView HoldOnReasonView)
        {
            objIHoldOnReasonView = HoldOnReasonView;
        }

        #region IModel Members

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@HoldOnReasonId", SqlDbType.Int, 0,objIHoldOnReasonView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_HoldOnReason_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@HoldOnReasonId",SqlDbType.Int,0, objIHoldOnReasonView.keyID),
                                               objDAL.MakeInParams("@HoldOnReason", SqlDbType.VarChar,250,objIHoldOnReasonView.HoldOnReason)                                       
                                                        };


            objDAL.RunProc("EC_Mst_HoldOnReason_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value.ToString());

            return objMessage;
        }

        #endregion
    }
}