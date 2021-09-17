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
/// <summary>
/// Summary description for CreditMemoReceiptView
/// </summary>
/// 
namespace Raj.EC.FinanceView
{
    public interface ICreditMemoReceiptView : IView
    {

        decimal CashAmount { get;set;}
        decimal ChequeAmount { get;set;}
        decimal TotalAmount { get;set;}
        
        string ChequeNo { get;set;}
        string GetDetailsXML { get;set;}
        
        int PartyNameID { get;}
        string BankName { get;set;}
        string Remarks { get;set;}
        string ReceiptNo { set;get;}

        DateTime ReceiptDate { get;set;}
        DateTime ChequeDate { get;set;}

        void SetPartyNameId(string text, string value);
        DataTable SessionCreditMemoDetails { get;set;}
        DataTable Bind_dg_CreditMemoDetails { set;}
          

    }

}

