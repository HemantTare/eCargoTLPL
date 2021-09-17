using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface IDDCView : IView
    {
        int PDSId { get;set;}
        int SupervisorID { get;}
        void SetSupervisor(string text, string value); 
        int DeliveryModeID { get;set;}
        int DeliveryStatusId { get;set;}
        int Total_No_Of_GC { get;set;}
        int VehicleID { get;set;}
        int VendorID { get;set;}
        int Total_Delivered_Articles { set;}

        DateTime DDSDate { get;set;}
        string Flag { get;}
        string Remarks { get;set;}
        string DDSNo { set;}
        string PDSDate { get;set;}
        string DiverName { get;set;}
        string VendorName { get;set;}
        string DeliveryModeDescription { get;set;}


        decimal Total_GC_Amount { get;set;}
        decimal Total_ChequeAmt { get;set;}
        decimal Total_CreditAmt { get;set;}
        decimal Total_Cash_Received { get;set;}
        decimal Total_Cash_Balance { get;set;}
        decimal Total_MobilePay { get;set;}
        decimal Total_SwipeCard { get;set;}
        decimal Total_PendingFreight { get;set;}
        decimal Total_Local_Tempo_Freight { get;set;}

        decimal Total_TempoBonus { get;set;}
        decimal Total_TempoAddTempoFrt { get;set;}

        decimal Total_Delivered_Weight { get;set;}

        DataTable BindPreDeliverySheet { set;}
        DataTable BindDeliveryMode { set;}
        DataTable SessionBindDDLUndelReason { set;}
        DataTable SessionBindDDSGrid { get;set;}
        DataTable SessionBindDlyAreaGrid { set;}
        DataTable SessionBindDlyStatus { set;}

        DataTable SessionBindDDLDeliveryMode { set;}

        string DDSDetailsXML { get;}

        void ClearVariables();
    }
}