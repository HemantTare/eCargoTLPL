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
using Raj.EC.MasterView;
using Raj.EC.ControlsView;
/// <summary>
/// Summary description for BranchView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IBranchView : IView
    {
        IBranchGeneralView BranchGeneralView { get;}
        IBranchDeptServiceView BranchDeptServiceView { get;}
        IBranchParametersView BranchParametersView { get;}
        IBranchRequiredFormsView BranchRequiredFormsView { get;}
        string Flag { get;}
    }
}
