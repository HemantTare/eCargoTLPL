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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.LoginView;

/// <summary>
/// Summary description for LoginModel
/// </summary>

namespace Raj.EC.LoginModel
{
    public class LoginChangePwdModel : IModel
    {
        private ILoginChangePwdView _LoginChangePwdView;
        private DAL objDAL = new DAL();
        private DataSet _ds;

        public LoginChangePwdModel(ILoginChangePwdView  iView)
        {
            _LoginChangePwdView = iView;
        }

        public DataSet ReadValues()
        {
            return _ds;
        }
        
        public Message Save()
        {
            Message mObj = new Message();

            SqlParameter[] SqlPara = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                      objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                      objDAL.MakeInParams("@UserId", SqlDbType.Int, 0, _LoginChangePwdView.keyID),
                                      objDAL.MakeInParams("@Password", SqlDbType.VarChar, 25, _LoginChangePwdView.ConfirmPassword)};

            objDAL.RunProc("COM_Adm_User_ChangePassword", SqlPara);

            mObj.messageID = Convert.ToInt32(SqlPara[0].Value);
            mObj.message = Convert.ToString(SqlPara[1].Value);

            return mObj;

        }

    }
}