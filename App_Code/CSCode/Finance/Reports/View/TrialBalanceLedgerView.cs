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
using Raj.EC.ControlsView;


/// <summary>
/// Summary description for TrialBalanceLedgerView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ITrialBalanceLedgerView : IView
    {
        DataSet TB_DS { set; }
        string CompanyName { set;}
        DateTime StartDate { set;get;}
        DateTime EndDate { set;get;}
        string Hierarchy_Code { set;get;}
        int Main_Id { set;get;}
        Boolean Is_Consol { set;get;}
        int DivisionId { get;}
    }
}
