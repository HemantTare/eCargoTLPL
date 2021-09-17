using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.CRM.MasterView;

/// <summary>
/// Summary description for IncludeExcludeView
/// </summary>
namespace Raj.CRM.TransactionView
{
    public interface IIncludeExcludeView : ClassLibraryMVP.General.IView
    {
        DataSet SessionUserGrid { get;set;}
        DataSet SessionUserDetails { get;set;}
        string ProfileName { set;}
        int ProfileId { get;}
       // string IncludeExcludeUserGridDetails { get;}
        DataSet BindGrid { set;}
        int ComplaintNatureId { get;}
        string ComplaintNatureName { set;}
       // IEscalationMatrixView EscalationMatrixView { get;}
      
	}
}
