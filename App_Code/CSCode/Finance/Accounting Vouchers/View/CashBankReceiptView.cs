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
    public interface ICashBankReceiptView : IView         
    {
        DateTime VoucherDate { set; get;}
        DateTime ChequeDate { set; get;}

        int LedgerId { set;get;}
        int Is_CashCheque { get;set;} 

        string RefNo { set; get;}
        string PaidToWhom { set;get;}
        string Details { set;get;}
        string Flag { get;}
        string ChequeNo { set;get;}
        string ChequeBank { set;get;}
        
        Decimal Amount { set;get;}  
     
    }
}
