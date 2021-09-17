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
/// Summary description for LedgerVoucherView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ILedgerVoucherView : IView
    {
        int Ledger_Id { get; }
        DateTime StartDate { get;set; }
        DateTime EndDate { get;set;}
        DataSet BindLedgerVoucherGrid { set; }
        DataSet SessionLedgerVoucher { get;set;}
        string Opening_Balance_Credit { set;}
        string Opening_Balance_Debit { set;}
        string Closing_Balance_Credit { set;}
        string Closing_Balance_Debit { set;}
        decimal Current_Total_Credit { set;}
        decimal Current_Total_Debit { set;}
        Boolean Is_Consol { get;set;}
        String Hierarchy_Code { get;set;}
        int Main_Id { get;set;}

	}
}
