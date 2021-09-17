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
/// Summary description for ContractView
/// </summary>
/// 
namespace Raj.EC.MasterView
{

    public interface IContractView : IView
    {
        IContractGeneralView ContractGeneralView { get;}
        IContractTermsView ContractTermsView { get;}
        IContractFreightDetailsView ContractFreightDetailsView { get;}
        IAttachmentsView AttachmentsView { get;}

        void ClearVariables();
        string Flag { get;}
    }
}