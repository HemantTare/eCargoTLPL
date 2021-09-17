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
    public interface IDisplayInfoView
    {
        string Type { get;}
        string keyID { get;}
        int Ticket_Id { get;}
        DataTable bind_dg_DisplayInfo { set; }
    }
}
