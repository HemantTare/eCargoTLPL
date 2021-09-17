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
/// Summary description for VoucherBillByBillView
/// </summary>
/// 
namespace Raj.EC.FinanceView
{
    public interface IVoucherBillByBillView : ClassLibraryMVP.General.IView
    {
        decimal Amount { set;get;}
        decimal Credit { get;}
        decimal Debit { get;}
        int CreditDays { set;get;}
        bool IsTDSApplicable { set;get;}
        DateTime BillDate { set;get;}
        int LedgerId { get;}
        int VoucherId { get;}
        string Name { set;get;}
        DataTable SessionBillByBillDT { set; get;}
        DataTable SessionBillByBill_New { set; get;}
        DataTable SessionDropDownRefType { set; get;}
        DataTable SessionTDSLedger { set; get;}

        DataTable Bind_ddl_RefType { set;}

        DataTable Bind_ddl_TDSLedger { set;}
        DataTable Bind_dg_BillByBill { set;}


        
    }

}
    