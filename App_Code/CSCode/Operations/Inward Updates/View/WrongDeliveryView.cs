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


namespace Raj.EC.FinanceView
{
    public interface IWrongDeliveryView : IView
    { 
        string Flag { get;} 
        int GC_ID { get;set;}
        string GCNo { get;set;}
        int Booking_Branch_ID { get;set;} 
        string BookingBranch { get;set;}
        int Delivery_Branch_Id { get;set;}
        string DeliveryBranch { get;set;}
        string InformedBy { get;set;}
        string InformedContactNo { get;set;}
        string CollectedBy { get;set;}
        string CollectedContactNo { get;set;}
        int VehicleID { get;set;}

        int Received_Condition_ID { get;set;}
        string Received_ConditionDescription { get;set;}
        DataTable BindReceived_Condition { set;}
        Boolean IsToPay { get;set;}
        Boolean IsCheque { get;set;}
        int ChequeNo { get;set;}
        DateTime ChequeDate { get;set;}
 
        string Description { get;set;} 
    }
}