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
/// Summary description for ReasonView
/// </summary>
namespace Raj.EC.GeneralView
{
    public interface IReasonView : IView
    {
        string Reason { set;get;}
        string Description { set;get;}
        string SessionReasonProcessDetails { get;}
        int ChkProcess { set;get;}
        DataSet BindChkListReasonProcess { set;}
        DataSet ChkListSessionProcess { get;set;}

        void ClearVariables();

    }
}