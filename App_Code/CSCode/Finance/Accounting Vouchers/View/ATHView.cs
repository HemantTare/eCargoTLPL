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
/// Summary description for ATHView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface IATHView : IView
    {
        int LHPOID { get;set;}
        int VehicleID { get;set;}
        int CreditToLedgerId { get;}
        int Allocation_Id { get;set;}
        string ReferenceNo { get;set;}
        string Remarks { get;set;}
        string ATHNo { set;}
        DateTime ATHDate { get;set;}

        decimal AdvancePayableAmount { get;set;}
        decimal TotalPaidAmount { get;set;}
        decimal TotalPetrolAmount { get;set;}
        decimal CreditAmountTo { get;set;}
        
        DataTable BindLHPONo { set;}
        DataTable SessionPetrolGrid { set;}

        string petrolDetailsXML { get;}
        IMRCashChequeDetailsView CashChequeDetailsView { get;}
        void ClearVariables();

        void SetCreditToLedgerID(string text, string value);

    }
}

