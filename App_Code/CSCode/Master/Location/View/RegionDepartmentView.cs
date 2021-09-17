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
/// Summary description for RegionDepartmentView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IRegionDepartmentView : IView
    {
        string CashLimit { set;get;}
        string BankLimit { set;get;}
        int ChkListDepartment { set;get;}
        string SessionChkListDepartmentDetails { get;}
        DataSet SessionChkListDepartment { set;get;}
        DataSet BindChkListDepartment { set;}



    }
}
