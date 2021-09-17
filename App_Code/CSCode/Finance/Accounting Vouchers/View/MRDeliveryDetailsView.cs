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

/// <summary>
/// Summary description for MRDeliveryDetailsView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface IMRDeliveryDetailsView : IView
    {
        int DeliveredToID {set;get;}
        int DeliveryAgainstID {set;get;}
        string ThroughMr {set;get;}
        DataTable SessionDeliveredTo {get;}
        DataTable SessionDeliveredAgainst {get;}
        void BindDeliveredAgainst();




	}
}
