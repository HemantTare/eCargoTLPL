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
    public interface ITicketHistoryView : IView         
    {
        string Reply{get;set;}
        string HeaderLable{set;}
        string CompliantNature { set;}
        int SetAttachmentCount { set;}
        DataTable bind_rpt_History { set;}
        string Type { get;}
    }
}
