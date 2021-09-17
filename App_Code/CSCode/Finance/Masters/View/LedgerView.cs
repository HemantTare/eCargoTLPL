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
using Raj.EC.FinanceView;
using Raj.EC.ControlsView;

namespace Raj.EC.FinanceView
{
    public interface ILedgerView : IView         
    {
        ILedgerGeneralView getLedgerGeneralView{ get;}
        IDivisionView getDivisionView{ get;}
        IAddressView getAddressView { get;}

        string Name { get;set;}
        string ContactPerson { get;set;}
        string Note { get;set;}
    }
}
