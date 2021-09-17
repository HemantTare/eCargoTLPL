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
/// Summary description for InsuranceCompanyBranchView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IInsuranceCompanyBranchView : IView
    {
        string BranchName { set;get;}
        string ContactPerson { set;get;}
        int InsuranceCompanyId{ set;get;}
        IAddressView AddressView { get;}
        DataSet BindInsuranceCompany { set;}


    }
}

