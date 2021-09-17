using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP.General;

namespace Raj.EC.FinanceView
{
    public interface IAdvancePaymentView : IView
    {
        string PaymentNo { set;}
        DateTime PaymentDate { set;get;}
        string RefNo { get;set;}
        string ChequeNo { get;set;}
        string RefNoPartyLedger { get;set;}
        decimal Amount { get;set;}
        string RefNoTDSLedger { get;set;}
        string Narration { get;set;}
        int CashBankLedgerId { get;set;}
        int PartyLedgerId { get;}
        int TDSLedgerId { get;set;}

        ITDSPaymentDeductionView GetITDSPaymentDeductionView { get; }

        void SetPartyLedgerId(string value, string text);
        DataTable bind_ddl_CashBankLedger { set;}
        DataTable bind_ddl_TDSLedger { set;}
    }
}