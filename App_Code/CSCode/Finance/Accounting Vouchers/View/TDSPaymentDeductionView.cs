using System;
using System.Data;
using System.Web.UI.WebControls;



/// <summary>
/// Summary description for TDSPaymentDeductionView
/// </summary>
/// 

namespace Raj.EC.FinanceView
{
    public interface ITDSPaymentDeductionView : ClassLibraryMVP.General.IView
    {

        string PartyName { set;}
        string DeducteeTypeName { set;}
        string AmountOfThisVoucher { set;}
        string AmountPaidPayableTillDate { set;}
        string TotalAmountPaidPayable { set;}
        string TaxPercent { set;}
        string TaxAmount { set;}
        string SurchargePercent { set;}
        string SurchargeAmount { set;}
        string AdditionalSurchargePercent { set;}
        string AdditionalSurchargeAmount { set;}
        string AdditionalCessPercent { set;}
        string AdditionalCessAmount { set;}
        string TotalTDS { set;}
        string TDSDeductedTillDate { set;}
        string NetTDSDeducted { set;get;}


        decimal Amount { get;set;}
        int TDS_Ledger_Id { get;set;}
        int Ledger_Id { get;set;}
        DateTime Voucher_Date { get;set;}
    }
}