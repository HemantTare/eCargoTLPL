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
using Raj.EC.OperationView; 

/// <summary>
/// Summary description for OctroiUpdateView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IOctroiUpdateView : IView
    {
        string TransactionNo { set;}
        DateTime TransactionDate { set;get;}
        int LedgerID { set;get;}
        string BillNo { set;get;}
        DateTime BillDate { set;get;}
        string Remarks { set;get;}
        DataTable SessionBindOctroiUpdateGrid { set;get;}
        String OctroiUpdateDetailsXML { get;}
        String GetGCNoXML { set;get;}
        DataTable BindOctroiFormType { set;}
        DataTable SessionOctroiFormType { set;get;}
        DataTable BindOctroiPaidBy { set;}
        DataTable SessionOctroiPaidBy { set;get;}
        int Total_No_Of_GC { set;get;}
        decimal Total_Amount { set;get;}
        void SetLedgerId(string Ledger_Name, string Ledger_Id);
        DataTable BindOctroiUpdateGrid { set;}
        string GCAlreadyUpdated{set;}

        void ClearVariables();// added Ankit
        string ChequeNo { set;get;}
        DateTime ChequeDate { set;get;}
        string NameOfBank { set;get;}
        int LedgerGroupId { set;get;}
        IOtherChargeLedgerView OtherChargeLedgerView { get;}
        decimal Grand_Total { get;set; }
        decimal OtherChargeAmount { get;set;}
	}
}
