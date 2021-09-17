using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for LoginView
/// </summary>
/// 

namespace Raj.EC.LoginView
{
   public interface ILoginView
    {

        int DivisionId{ get;}
        int YearCode {get;}
        string UserName{ get;}
        string Password { get;}
       string ErrorMessage { set;get;}
       string ErrorMessageWrongdivisionLogin { get;}
       string Mac_Id { get;}
        Boolean IsDivisionReq { set;}
        DataTable BindDivisionDdl { set;}
        DataSet BindYearDdl { set;}

    }
}