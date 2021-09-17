using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface IDlyCreditApprovalView : IView
    {
        int GCNo { get;}
        bool Status { get;set;}
        int IsApproved { get;}
        int BillingPartyId { get;}
        string MTransactionID { get;}
        bool PaymentReceivedbyCredit { get;}
        string ReasonUnApproved { get;}
        
        int PDSID { get;set;}
        int GCID { get;set;}

        string Consignor {set;}
        string Consignee { set;}
        string PaymentMode { set;}
        string TotalArticles { set;}
        string TotalGCAmount { set;}
        string errorMessage { set;}
    }
}