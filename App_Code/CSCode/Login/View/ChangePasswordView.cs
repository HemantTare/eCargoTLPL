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
namespace Raj.EC.LoginView
{
    public interface IChangePasswordView:IView 
    {
        string Login { get;set;}
        string OldPassword { get;}
        string NewPassword { get;}
        string Msg { set;}
        int IsTrue {set;get; }
    
    }
}
