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

namespace Raj.EC.FinanceView
{
    public interface IMRBookingDetailsView : ClassLibraryMVP.General.IView
    {
        IMRCashChequeDetailsView MRCashChequeDetailsView { get;}
        IMRGeneralDetailsView MRGeneralDetailsView { get;}
        decimal RebookedCharges { get;set;}
        decimal TotalReceivables { get;set;}
        string Flag { get;}
        void ClearVariables();
    }
}
