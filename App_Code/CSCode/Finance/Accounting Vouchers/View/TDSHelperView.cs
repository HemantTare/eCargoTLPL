using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for TDSHelperView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ITDSHelperView : ClassLibraryMVP.General.IView
    {
        int CashBankAc { get;set;}
        int TDSLedgerAc { get;set;}
        string DeducteeStatus { get;set;}
        DateTime ToDateValue { get;set;}
        DataSet BindCashBankAc { set;}
        DataSet BindTDSLedgerAc { set;}

    }
}
