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
/// Summary description for FBTHelperView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface IFBTHelperView : ClassLibraryMVP.General.IView
    {
        int FBTLedger { get;set;}
        int CashBankAc { get;set;}
        string TypeOfPayment { get;set;}
        DateTime FromDateValue { get;set;}
        DateTime ToDateValue { get;set;}
        DataSet BindFBTLedger { set;}
        DataSet BindCashBankAc { set;}

    }
}
