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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.CRM.MasterView;

namespace Raj.CRM.MasterModel
{
    public class ReasonForFaultModel : IModel
    {
        private IReasonForFaultView objIReasonForFaultView;
        private DataSet objDS;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);
      
        public ReasonForFaultModel(IReasonForFaultView reasonForFaultView)
        {
            objIReasonForFaultView = reasonForFaultView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@ReasonFaultId",SqlDbType.Int,0, objIReasonForFaultView.keyID)
                                           };
            _objDAL.RunProc("EC_CRM_Mst_ReasonForFault_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMsg = new Message();
            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
           _objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0,objIReasonForFaultView.keyID),
           _objDAL.MakeInParams("@ReasonForFault", SqlDbType.VarChar,100,objIReasonForFaultView.ReasonForFault),
           _objDAL.MakeInParams("@Description", SqlDbType.VarChar,250,objIReasonForFaultView.Description),
           _objDAL.MakeInParams("@UserId",SqlDbType.Int,0, _userId)};

              _objDAL.RunProc("EC_CRM_Mst_ReasonForFault_Save", objSqlParam);

            objMsg.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMsg.message = Convert.ToString(objSqlParam[1].Value.ToString());

            return objMsg;

        }
    }
}
