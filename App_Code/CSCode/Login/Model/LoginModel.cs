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
using Raj.EC.LoginView;


namespace Raj.EC.LoginModel
{
     public class cLoginModel
    {

        private ILoginView _loginView;
        private DAL objDAL = new DAL();
        private DataSet _ds;

         public cLoginModel(ILoginView iView)
        {
            _loginView = iView;
        }

        
         public DataSet FillYearInDdl()
         {
             objDAL.RunProc("dbo.EC_Mst_Year_Fill", ref _ds);
             return _ds;
         }

         public DataSet FillDivisionInDdl()
         {
             objDAL.RunProc("dbo.EC_Mst_Division_Fill", ref _ds);
             return _ds;
         }

         public DataSet CheckLogin()
         {
            // SqlParameter[] param ={objDAL.MakeInParams("@YearCode",SqlDbType.Int,0,_loginView.YearCode),
            //                        objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_loginView.DivisionId),
            //                         objDAL.MakeInParams("@UserName",SqlDbType.VarChar,25,_loginView.UserName),
            //                         objDAL.MakeInParams("@Password",SqlDbType.VarChar,25,_loginView.Password)};
                          
            //objDAL.RunProc("dbo.EC_Login_Check", param,ref _ds);
            //return _ds;


             SqlParameter[] param ={
            objDAL.MakeOutParams("@Is_Valid_User",SqlDbType.Bit,0),
            objDAL.MakeOutParams("@Is_User_Active",SqlDbType.Bit,0),
            objDAL.MakeOutParams("@IsEmpDivValid",SqlDbType.Bit,0),
            objDAL.MakeOutParams("@Is_Activate_Divisions",SqlDbType.Bit,0),
            objDAL.MakeOutParams("@Is_Mac_Id_Found",SqlDbType.Bit,0),
            objDAL.MakeInParams("@YearCode",SqlDbType.Int,0,_loginView.YearCode),
            objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_loginView.DivisionId),
            objDAL.MakeInParams("@UserName",SqlDbType.VarChar,25,_loginView.UserName),
            objDAL.MakeInParams("@Password",SqlDbType.VarChar,25,_loginView.Password),
            objDAL.MakeInParams("@Mac_Id",SqlDbType.VarChar,25,_loginView.Mac_Id)
             };

            objDAL.RunProc("dbo.EC_Login_Check", param, ref _ds);

            if (Convert.ToBoolean(param[0].Value.ToString()) == false)
                _loginView.ErrorMessage = "Invalid UserName or Password";
            else if (Convert.ToBoolean(param[1].Value.ToString()) == false)
                _loginView.ErrorMessage = "User Not Active";
            else if (Convert.ToBoolean(param[3].Value.ToString()) == true && Convert.ToBoolean(param[2].Value.ToString()) == false)
                _loginView.ErrorMessage = _loginView.ErrorMessageWrongdivisionLogin;
            else if (Convert.ToBoolean(param[4].Value.ToString()) == false)
                _loginView.ErrorMessage = "You are not authorised to login from this machine.";

            return _ds;
         }
        

     
    }
}
