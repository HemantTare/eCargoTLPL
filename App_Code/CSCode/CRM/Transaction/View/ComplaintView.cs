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
    public interface IComplaintView : IView         
    {
        string TicketNo{get;set;}
        DateTime TicketDate{set;}
        string Name{get;set;}
        string TelephoneNo{get;set;}
        string MobileNo { get;set;}
        string Designation{get;set;}
        string EMailID{get;set;}
        string UndeliveredReason{get;set;}
        string ComplaintDetails{get;set;}

        int SetAttachmentCount {set;}
        int DocGcId { get;}
        int PriorityId { set; get;}
        int ComplaintNatureId { get;set;}
        void SetDocGcId(string value, string text);
        int DocGcNo { get;}
        int CNeeNorID { get;set;}
        bool IsQuery { get;set;}
        DataTable SetLables { set;}
        DataTable bind_ddl_DocGcNo { set;}
        DataTable bind_rdbl_Priority { set;}
        DataTable bind_ddl_CNeeNor { set;}
        DataTable bind_ddl_NatureOfComplaint { set;}
    }
}
