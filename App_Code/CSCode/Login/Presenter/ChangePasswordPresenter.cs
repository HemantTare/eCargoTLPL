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
using ClassLibraryMVP .General;
using Raj.EC.LoginModel;
using Raj.EC.LoginView;


namespace Raj.EC.LoginPresenter
{
    public class ChangePasswordPresenter:Presenter 
    {
        private IChangePasswordView objIChangePasswordView;
        private ChangePasswordModel objChangePasswordModel;
        public ChangePasswordPresenter(IChangePasswordView ChangePasswordView, bool IsPostBack)
        {
            objIChangePasswordView = ChangePasswordView;
            objChangePasswordModel = new ChangePasswordModel(objIChangePasswordView);
            base.Init(objIChangePasswordView, objChangePasswordModel);
            if (!IsPostBack)
            {
                objIChangePasswordView.Login = UserManager.getUserParam().UserName;
            }
           
        }
        public void Save()
        {
            objChangePasswordModel.Save();
        }
    }
}
