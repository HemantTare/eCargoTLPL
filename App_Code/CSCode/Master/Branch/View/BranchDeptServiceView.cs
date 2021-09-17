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
/// Summary description for BranchDeptServiceView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IBranchDeptServiceView : IView
    {
        bool IsBookingAllowed { get;set;}
        bool IsDeliveryAllowed { get;set;}
        bool IsCrossingBranch { get;set;}
        bool IsFranchiseeBranch { get;set;}
        bool IsComputersiedBranch { get;set;}
        bool IsOctroiApplicable { get;set;}

        //bool hdnIsDeliveryAllowed { get;set;}
        //bool hdnIsCrossingBranch { get;set;}

        DataTable BindDepartment { set;}
        string BranchDepartmentXML { get;}

    }
}