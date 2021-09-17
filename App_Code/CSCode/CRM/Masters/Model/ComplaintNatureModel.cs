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

/// <summary>
/// Summary description for ComplaintNatureModel
/// </summary>
namespace Raj.CRM.MasterModel
{
    /// <summary>
    /// Summary description for LedgerModel
    /// </summary>
    public class ComplaintNatureModel : IModel
    {
        private IComplaintNatureView objIComplaintNatureView;
        private DataSet objDS;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);
         
        public ComplaintNatureModel(IComplaintNatureView complaintNatureView)
        {
            objIComplaintNatureView = complaintNatureView;
        }       

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = {
                                            _objDAL.MakeInParams("@ComplaintNatureId",SqlDbType.Int,0, objIComplaintNatureView.keyID)
                                           };
             _objDAL.RunProc("EC_CRM_Mst_ComplaintNature_ReadValues", objSqlParam, ref objDS);

            return objDS;


        }

        public Message Save()
        {
            Message objMsg = new Message();
            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
           _objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0,objIComplaintNatureView.keyID),
           _objDAL.MakeInParams("@ComplaintNatureName", SqlDbType.VarChar,100,objIComplaintNatureView.ComplaintNatureName),
           _objDAL.MakeInParams("@Description", SqlDbType.VarChar,250,objIComplaintNatureView.Description),
           _objDAL.MakeInParams("@UserId",SqlDbType.Int,0, _userId)};

            _objDAL.RunProc("EC_CRM_Mst_ComplaintNature_Save", objSqlParam);

            objMsg.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMsg.message = Convert.ToString(objSqlParam[1].Value.ToString());

            return objMsg;

        }
    }
}
