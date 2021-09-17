using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.LoginModel;
using Raj.EC.LoginView;





/// <summary>
/// Summary description for StatePresenter
/// </summary>
/// 
namespace Raj.EC.LoginPresenter
{
    public class cLoginPresenter :Presenter
    {

        private ILoginView _loginView;
        private cLoginModel _loginModel;

        public cLoginPresenter(ILoginView LoginView, bool isPostBack)
        {
            _loginView = LoginView;
            _loginModel = new cLoginModel(_loginView);

            if (!isPostBack)
            {
                _loginView.BindYearDdl = _loginModel.FillYearInDdl();
                _loginView.BindDivisionDdl = _loginModel.FillDivisionInDdl().Tables[0];
                _loginView.IsDivisionReq = Convert.ToBoolean(_loginModel.FillDivisionInDdl().Tables[1].Rows[0][0].ToString());
            }
            
        }

        public DataSet GetLoginDetails()
        {
            return _loginModel.CheckLogin();
        }


    }
}