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
    public interface IPartyRecieptVoucherView : IView
    {
        string VoucherNo { get;set;}
        DateTime VoucherDate { get;set;}
        int CashBankLedgerID { get;}
        string ChequeBankName { get;set;}
        int ChequeNo { get;set;}
        DateTime ChequeDate { get;set;}
        int ClientLedgerID { get;}
        DataTable Bind_BillGrid { set;}
        DataTable Session_BillGrid { get;set;}
        decimal AmountRecieved { get;set;}
        decimal TotalAdjustedAmount { get;set;}
        DataTable Bind_OtherGrid { set;}
        DataTable Session_OtherGrid { get;set;}
        decimal TotalDeduction { get;set;}
        string Narration { get;set;}
        DataTable SessionDropDownRefType { get;set;}
        string RefNo { get;set; }
        string GetClientBillDetailXML { get;}
        string GetOtherDeductionXML { get;}
        int OtherLedgerId { get;}
        DataTable SessionBillByBillDT { get;set;}
        DataTable SessionCostCentreDT { get;set;}
        DataTable SessionDropDownCostCentre { get;set;}
        string BillLedgerName { set;}
        string GetBillByBillXML { get;}
        string GetCostCentreXML { get;}
        string ManualRefNo { get;set;}
        void SetCashBankLedger(string text, string value);
        void SetClientLedger(string text, string value);
    }
    
}

