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
/// Summary description for Complaint_AssignmentView
/// </summary>
namespace Raj.CRM.TransactionView
{
    public interface IComplaint_AssignmentView : ClassLibraryMVP.General.IView
    {
        String XML_User { get; }

        DataTable Bind_dg_AssginUser { set;}
        DataTable Bind_ddl_FilterBy1 { set;}

        DataTable SetHeadingCaption { set;}

        string SearchById { set;get; }
        int FilterById {get; }
        string XMLAllUser { get; }
    }
}