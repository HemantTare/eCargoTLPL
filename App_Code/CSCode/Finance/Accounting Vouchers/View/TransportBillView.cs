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
/// Summary description for TransportBillView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ITransportBillView : IView
    {

        int BkgDlyBranchId { get;}
        int BkgDlyAreaId { get;}
        int BkgDlyRegionId { get;}
        int IsBookingWise { get;}

        int BillTypeID { get;set;}
        int ClientID { get;}
        int LedgerId { get;}
        int Next_No { get;set;}
        int Total_No_Of_GC { set;get;}
        int Document_Allocation_ID { set;get; }
        int Bill_ForID { get;set;}

        string ReferenceNo { get;set;}
        string Remarks { get;set;}
        string BillNo { get;set;}
        string TotalSubTotal { set;}
        string TotalLRSerTax { set;}
        string TotalRound_Off { set;}
        string TotalLRTotal { set;}
        string TotalOtherCharge { set;}
        string TotalOctroiFormCharge{set;}
        string TotalOctroiServiceCharge { set;}
        string TotalServiceTax { set;}
        string TotalOctAmount { set;}
        string TotalGCAmount { get;set;}
        decimal Less_Amount { get;set;}
        decimal TotalAmount { get;set;}
        decimal Total_Additional_Charges { get;set;}

        DataTable BindBillFor { set;}

        String Flag { get;}
        String BillingName { get;set;}        
        String ContactPerson { get;set;}
        String BillingAddress { get;set;}                
        String ContactNo { get;set;}
        String Email { get;set;}
        DateTime BillDate { get;set;}
        DataTable BindBillType { set;}
        DataTable SessionBillGrid { get;set;}
        DataTable SessionBillOtherChargeGrid { get; set;}

        DataTable Bind_dg_Voucher { set;}
        DataTable SessionLedgerDT { set; get;}

        string BillDetailsXML { get;}
        string BillOtherChargeGridXML { get;}
        string LedgerDetailsXML { get;}
        void SetClientId(string text, string value);
        void ClearVariables();
    }
}

