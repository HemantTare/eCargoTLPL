
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Raj.CRM.TransactionView;

 
/// <summary>
/// Summary description for Complaint_AnalysisView
/// </summary>
namespace Raj.CRM.TransactionView
{
    public interface IComplaint_AnalysisView : ClassLibraryMVP.General.IView
    {
        int Ticket_ID { get;}
        int GC_Docket_Id { get;set;}

        String Ticket_No { get;set;}
        String GC_Docket_No { get;set;}
        
        String Person_Responsible{ get;set;}
        String Action_Taken{ get;set;}

        String Complaint_Analysis_Xml { get; }

        DataSet Bind_Complaint_Analysis_Details { set;}
        DataSet Session_Complaint_Analysis_Details { get;set;}
    }
}