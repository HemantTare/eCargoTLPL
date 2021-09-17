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
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

namespace Raj.EF.MasterModel
{
     class DriverLicenseCategoryModel : IModel
    {
        private IDriverLicenseCategoryView objIDriverLicenseCategoryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        public DriverLicenseCategoryModel(IDriverLicenseCategoryView DriverLicenseCategoryView)
        {
            objIDriverLicenseCategoryView = DriverLicenseCategoryView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] SqlParam ={ objDAL.MakeInParams("@License_Category_ID", SqlDbType.Int, 0, objIDriverLicenseCategoryView.keyID) };
            objDAL.RunProc("[rstil22].[EF_Mst_DriverLicense_Category_ReadValues]", SqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] SqlParam ={ objDAL.MakeOutParams("@ERROR_CODE",SqlDbType.Int,0),
                                  objDAL.MakeOutParams("@ERROR_DESC",SqlDbType.VarChar,4000),
                                  objDAL.MakeInParams("@License_Category_ID",SqlDbType.Int,0,objIDriverLicenseCategoryView.keyID),
                                  objDAL.MakeInParams("@License_Category",SqlDbType.VarChar,50,objIDriverLicenseCategoryView.DriverLicenseCategory),
                                  objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)};
            objDAL.RunProc("[rstil22].[EF_Mst_Driver_License_Category_Save]", SqlParam);

            objMessage.messageID = Convert.ToInt32(SqlParam[0].Value);
            objMessage.message = Convert.ToString(SqlParam[1].Value);
            return objMessage;

        }
    }
}
