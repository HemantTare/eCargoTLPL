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
using Raj.EF.MasterView;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for DriverView
/// </summary>
/// 

namespace Raj.EF.MasterView
{
    public interface IDriverView : IView
	{
        IDriverDetailsView DriverDetailsView {get;}
        IDriverInsuranceDependentView DriverInsuranceDependentView { get;}
        IAttachmentsView AttachmentsView { get;}
        void ClearVariables();
	}
}
