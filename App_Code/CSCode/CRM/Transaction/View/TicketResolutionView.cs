using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for TicketResolutionView
/// </summary>
namespace Raj.CRM.TransactionView
{
    public interface ITicketResolutionView : ClassLibraryMVP.General.IView
    {

       int WhetherCustomerSatisfied{ get;set;}
       // DataSet BindTicketId {set;}       
        string HowResolved{ get;set;}
        //bool SearchBy{ get;set;}
        string Reason { get;set;}
        string TicketNo {set;}
        int TicketId { get;}
        string GCDocketNo{set;}
        int GCDocketNoId { get;set;}
        bool Save { set;}
        bool HowResolve_1{ set;}
        bool Reason_1 { set;}
        bool CustomerSatisfied { set;}
    }
}
