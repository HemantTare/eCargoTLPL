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

public class ChangePasswordModel :IModel 
{
    private IChangePasswordView objIChangePasswordView;
    private DAL objDAL = new DAL();
    private DataSet _ds;
    public ChangePasswordModel(IChangePasswordView ChangePasswordView)
	{
        objIChangePasswordView = ChangePasswordView;
	}
    public DataSet ReadValues()
    {
        return _ds;
    }

    public Message Save()
    {
        Message objMessage = new Message();
        SqlParameter[] sqlPara ={objDAL.MakeOutParams("@ERROR_CODE",SqlDbType.Int,0),
                                 objDAL.MakeOutParams("@ERROR_DESC",SqlDbType.VarChar,4000),
                                 objDAL.MakeInParams("@OldPassword",SqlDbType.VarChar,25,objIChangePasswordView.OldPassword),
                                 objDAL.MakeInParams("@NewPassword",SqlDbType.VarChar,25,objIChangePasswordView.NewPassword),
                                 objDAL.MakeInParams("@UserName",SqlDbType.VarChar,25,objIChangePasswordView.Login),
                                 objDAL.MakeOutParams("@IsPwdExists",SqlDbType.Int,0)};
                                 objDAL.RunProc("EC_Login_Change_Password",sqlPara);

                                 if (Convert.ToInt32(sqlPara[5].Value) == 1)
                                 {
                                     objIChangePasswordView.Msg = "Please Enter Different Password!";

                                 }
                                 else if (Convert.ToInt32(sqlPara[5].Value) == 2)
                                 {
                                     objIChangePasswordView.Msg = "Incorrect Old Password!";
                                 
                                 }
                                 else
                                 {
                                     objIChangePasswordView.IsTrue = 0;
                                 }
      
                                 
        return objMessage;
    }
}
