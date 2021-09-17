using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;

using Raj.EC.ControlsView;

/// <summary>
/// Name : Dinesh Mahajan
/// Date : 
/// Summary description for AUSOtherAgencyUnloadingDetailsView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface IAUSOtherAgencyUnloadingDetailsView : IView
    {
        int AgencyId { get;set;}
        int AgencyLedgerId { get;}
        int ArrivedFromLoacationId { get;set;}
        int ArrivedFromBranchId { get;set;}
        int Reason_For_Late_Uploading { get;set;}
        int Supervisor { get;}
        string Flag { get;}
        int Total_GC { get;set;}
        Decimal Total_Booking_Articles { get;set;}
        Decimal Total_Booking_Articles_Wt { get;set;}
        Decimal Total_Loaded_Articles { get;set;}
        Decimal Total_Loaded_Articles_Wt { get;set;}
        Decimal Total_Received_Articles { get;set;}
        Decimal Total_Received_Articles_Wt { get;set;}
        Decimal Total_Damage_Leakage_Articles { get;set;}
        Decimal Total_Damage_Leakage_Value { get;set;}
        Decimal Lorry_Hire { get;set;}
        Decimal Other_Payable { get;set;}
        Decimal Total_Goods_Dly_Rec { get;set;}
        Decimal Total_Upcountry_Rec { get;set;}
        Decimal Total_Service_charge_Payable { get;set;}
        Decimal Total_Upcountry_Crossing_Cost_Payable { get;set;}
        Decimal Total_Payable { get;set;}
        Decimal Total_Receivable { get;set;}

        String TURNo { get;set;}
        String VehicleNo { get;set;}
        String LHPO_No_For_Print { get;set;}
        String ScheduledArivalDate { get;set;}
        String ScheduledArivalTime { get;set;}
        String ActualArrivalTime { get;set;}
        String UnloadingTime { get;set;}
        String Remarks { get;set;}
        String Unloading_Details_Xml { get;}
        DateTime LHPO_Date { get;set;}
        DateTime  ActualArrivalDate { get;set;}
        DateTime AUS_Date { get;set;}
        void SetSupervisor(string text, string value);
        void SetArrived_From(string text, string value);
        void SetAgency_Ledger(string text, string value);
        DataTable BindResionForLateArrivalUnloading { set;}
        DataTable SessionReceivedCondition { get;set;}
        DataTable SessionUnloadingDetailsGrid { get;set;}
        DataTable BindAgency { set;}
    }
}