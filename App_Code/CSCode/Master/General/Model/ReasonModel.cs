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
using Raj.EC.GeneralView;

/// <summary>
/// Summary description for ReasonModel
/// </summary>
namespace Raj.EC.GeneralModel
{
    class ReasonModel : IModel
    {
        private IReasonView objIReasonView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public ReasonModel(IReasonView reasonView)
        {
            objIReasonView = reasonView;
        }

        public DataSet GetProcessValues()
        {

            objDAL.RunProc("[dbo].[EC_Mst_Reason_FillValues]", ref objDS);
            return objDS;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ReasonId", SqlDbType.Int, 0, objIReasonView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_Reason_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }


        public DataSet ReadValues1()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ReasonId", SqlDbType.Int, 0, objIReasonView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_Reason_ReadCheckedValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@ReasonId",SqlDbType.Int,0, objIReasonView.keyID),
                                               objDAL.MakeInParams("@Reason", SqlDbType.VarChar,100,objIReasonView.Reason), 
                                               objDAL.MakeInParams("@Description", SqlDbType.VarChar,100, objIReasonView.Description),                                               
                                               objDAL.MakeInParams("@SessionReasonProcessDetails",SqlDbType.Xml,0,objIReasonView.SessionReasonProcessDetails),
                                               objDAL.MakeInParams("@UserId", SqlDbType.Int,0,  _userID)
                                         };


            objDAL.RunProc("EC_Mst_Reason_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIReasonView.ClearVariables();                
            }

            return objMessage;
        }

	}
}
