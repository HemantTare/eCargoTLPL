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
using Raj.CRM.TransactionsView;
namespace Raj.CRM.TransactionsView
{
    public interface IMergeTicketsView : IView         
    {
        int GcDocId { get;}
        int FromTicketId { set; get;}
        int ToTicketId { get;set;}
        
        DataTable bind_ddl_GcDoc { set;}
        
        DataTable bind_ddl_FromTicket { set;}
        DataTable bind_ddl_ToTicket { set;}
    }
}
