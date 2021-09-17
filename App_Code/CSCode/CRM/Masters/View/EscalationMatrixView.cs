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
/// Summary description for EscalationMatrixView
/// </summary>
namespace Raj.CRM.MasterView
{
    public interface IEscalationMatrixView : ClassLibraryMVP.General.IView
    {       
        int ComplaintNatureId {get;set;}
        DataTable BindComplaintNature { set;}
        DataSet SessionEscalationMatrixGrid {set;get;}      
        DataSet SessionUserDetails { get;set;}
        DataSet SessionProfile { set;get;}
    }
}