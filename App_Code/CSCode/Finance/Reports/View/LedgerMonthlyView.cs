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
/// Summary description for LedgerMonthlyView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ILedgerMonthlyView : IView
    {
        int Ledger_Id { get; }
        string CompanyName { set;}
        DateTime StartDate { get; }
        DateTime EndDate { get;}
        Decimal Total_Debit { set;get;}
        Decimal Total_Credit { set;get;}
        String Closing_Balance { set;get;}
        Boolean Is_Consol { get;set;}
        String Hierarchy_Code { get;set;}
        int Main_Id { get;set;}
        DataSet BindLedgerMonthlyGrid { set; }
		
	}
}
