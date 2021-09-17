using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.LoginView;
using Raj.EC.LoginModel;

/// <summary>
/// Summary description for LoginPresenter
/// </summary>

namespace Raj.EC.LoginPresenter
{
    public class LoginChangePwdPresenters : ClassLibraryMVP.General.Presenter
    {
        private ILoginChangePwdView _LoginChangePwdView;
        private LoginChangePwdModel _LoginChangePwdModel;

        public LoginChangePwdPresenters(ILoginChangePwdView LoginChangePwdView, bool isPostBack)
        {
            _LoginChangePwdView = LoginChangePwdView;
            _LoginChangePwdModel = new LoginChangePwdModel(_LoginChangePwdView);
            base.Init(_LoginChangePwdView, _LoginChangePwdModel);
            if (!isPostBack)
            {
                _LoginChangePwdView.Login = UserManager.getUserParam().UserName;
            }


        }

        public void Save()
        {
            _LoginChangePwdModel.Save();
        }

    }
}