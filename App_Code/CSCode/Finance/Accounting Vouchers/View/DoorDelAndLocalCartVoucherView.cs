using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for DoorDelAndLocalCartVoucherView
/// </summary>
/// 
namespace Raj.EC.FinanceView
{
    public interface IDoorDelAndLocalCartVoucherView : ClassLibraryMVP.General.IView
    {
        
        string VoucherNo { get;set;}
        string RefNo { get;set;}
        string GCXML { get;set;}
        string GetDetailsXML { get;set;}

        string VoucherType { get;set;}  
        DateTime VoucherDate { get;set;}
        DateTime ChequeDate { get;set;}
        bool IsCash { get;set;}
        bool IsCheque {get;set;}
        bool IsCreditTo { get;set;}

        string ChequeNo{ get;set;}
        string ChequeInFavour{ get;set;}
        int CreditToLedgerID{ get;set;}
        int TotalGC{ get;set;}
        decimal TotalAmount{ get;set;}
        string Remark{ get;set;}

        void SetLedgerID(string Ledger_Name, string LedgerID);

        DataTable Bind_dg_Voucher { set;}
        DataTable SessionVoucherGrid { set;get;}
        void ClearVariables();
    }    
}