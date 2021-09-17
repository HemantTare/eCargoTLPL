using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;

using Raj.EC.ControlsView;

/// <summary>
/// Summary description for AUSView
/// </summary>




namespace Raj.EC.OperationView
{
    public interface IAUSView : IView
    {
        IAUSUnloadingDetailsView AUSUnloadingDetailsView { get;}
        IAUSExcessDetailsView AUSExcessDetailsView { get;}
        void ClearVariables();
        string Flag { get;}

    }
}




