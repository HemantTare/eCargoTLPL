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

/// <summary>
/// Summary description for LoginView
/// </summary>
//public class LoginView
//{
//    public LoginView()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}
namespace Raj.EC.LoginView
{
    public interface ILoginChangePwdView : IView
    {
        string Login { get;set;}
        string NewPassword { get;}
        string ConfirmPassword { get;}
    }
}