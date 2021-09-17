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
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for UserMasterView
/// </summary>
namespace Raj.EC.GeneralView
{
    public interface IUserMasterView : IView
    {


        string NonEmpUserName { set;get;}
        int ProfileId { get;set;}
        int BranchId { get;set;}
        DataSet BindProfile { set;}
        DataSet BindBranch { set;}
        IAddressView AddressView { get;}
        bool IsActive { set;get;}
    }
}