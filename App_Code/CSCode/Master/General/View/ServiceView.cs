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
/// Summary description for ServiceView
/// </summary>

namespace Raj.EF.MasterView
{
    public interface IServiceView : IView
    {
        string ServiceName { get;set;}
        int ServiceCategoryID { get;set;}
        string ServiceDescription { get;set;}
        int ParentServiceID { get;set;}
        decimal EstCheckingTime { get;set;}
        decimal EstRepairTime { get;set;}

        DataTable BindServiceCategory { set;}
        DataTable BindParentService { set;}

        DataTable BindServiceTask { set;}
        DataTable SessionServiceTaskDropdown { set;get;}
        DataTable BindServiceTaskDetailsGrid { set;}
        string ServiceTaskDetailsXML { get;}
    }
}
