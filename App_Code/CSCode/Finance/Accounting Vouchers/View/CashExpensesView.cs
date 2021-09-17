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
 
namespace Raj.EC.FinanceView
{
    public interface ICashExpensesView : IView         
    {
        DateTime VoucherDate { set; get;}
        int LedgerId { set;get;}
        string RefNo { set; get;}
        string PaidToWhom { set;get;}
        string Details { set;get;}
        string Flag { get;}

        Decimal Amount { set;get;}  
     
        
    }
}
