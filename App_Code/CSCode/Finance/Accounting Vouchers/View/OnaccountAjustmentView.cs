using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Raj.EC.FinanceView
{
    public interface OnaccountAdjustmentView : ClassLibraryMVP.General.IView 
    {
        //int LedgerId { get;set;}
        DataTable BindOnAccountUnAdjustedGrid { set;}
        DataTable BindOnAccountAdjustedGrid { set; }
        DataTable   SessionOnAccount { get;set;}
        DataTable SessionOnAccountAdjusted { get;set; }
        int UnAdjustedVoucherId { get;set; }
        decimal UnAdjustedAmount { get;set;}
        DateTime UnAdjustVoucherDate { get;set;}
        int UnAdjustedSrNo { get;set;}
        int SessionLedgerGroupId {set;}
        DataTable BindLedgerGroup { set;}
        DataTable BindLedger { set;}
        bool hideApprove { set;}
        bool hideAutoAdjust { set;}
        int LedgerId { get;}
        int LedgerGroupId { get; }
        decimal Balance_Amount { get;set;}
        void ClearVariables();
    }
}
