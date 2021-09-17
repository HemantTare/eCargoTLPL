using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;

using Raj.EC.ControlsView;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 27-10-08
/// Summary description for AUSUnloadingDetailsView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface IAUSUnloadingDetailsView : IView
    {
         
        int LHPO_Id { get;}

        //Added by: Anita on :24 Jan 09
        int TAS_Id { set; get;}
        int IsTAS { set; get;}
        //
        int Total_GC { get;}
        int Vehicle_Id { get;set;}
        int Vehicle_Category_Id { get;set;}
        int NoofMinuteDifferenceForLate{ get;set;}
        int TotalShortArticlesValue { get;set;}
        int TotalExcessArticlesValue { get;set;}

        int Total_Booking_Articles { get;set;}
        int Total_Loaded_Articles { get;set;}
        int Total_Received_Articles { get;set;}      
        int Total_Damage_Leakage_Articles { get;set;}
       
        int Reason_For_Late_Arrival { get;set;}
        //int Reason_For_Late_Arrival_ForDisplay { get;set;}
        int Reason_For_Late_Uploading { get;set;}
        int Supervisor { get;}


        Decimal Total_Booking_Articles_Wt { get;set;}
        Decimal Total_Loaded_Articles_Wt { get;set;}
        Decimal Total_Received_Articles_Wt { get;set;}
        Decimal Total_Damage_Leakage_Value { get;set;}

        Decimal Total_Additional_Freight { get;set;}
        Decimal Total_Delivery_Commision { get;set;}


        Decimal Total_To_Pay_Collection { get;set;}
        Decimal Lorry_Hire{ get;set;}
        Decimal Other_Receavable { get;set;}
        Decimal Other_Payable { get;set;}
        Decimal Total_Receable { get;set;}
        Decimal Total_Payable { get;set;}


        Decimal To_Pay_Collection { get;set;}
                
        String TURNo { get;set;}
        String Manual_TUR_No { get;set;}
        String LHPO_Date { get;set;}
        String ScheduledArivalDate { get;set;}
        String ScheduledArivalTime { get;set;}
        string Reason_For_Late_Arrival_Display { set;}

        //String ActualArrivalTime { get;set;}
        string TASTime { get;set;}
        //DateTime ActualArrivalDate { get;set;}  
        DateTime TASDate { get;set;}

        String UnloadingTime { get;set;}

        void SetLHPO(string text, string value);
        void SetTAS(string text, string value);
        void SetSupervisor(string text, string value); 
        DateTime AUS_Date { get;set;}     

        String Vehicle_Category { get;set;}
        String Remarks { get;set;}

        String Unloading_Details_Xml { get;}

        DataTable BindResionForLateArrivalUnloading { set;}
        DataSet BindSupervisor { set;}
       
        DataTable SessionReceivedCondition { get;set;}

        DataTable Bind_dg_UnloadingDetails { set;}
        DataSet BindLHPO { set; }
        DataSet BindTAS { set; }

        DataTable SessionUnloadingDetailsGrid { get;set;}  

        //common adress control   
        IVehicleSearchView VehicleSearchView { get;}
        int MenuItemId { get; } 
        string LHPOFromLocation{set;get;}
        string LHPOToLocation{set;get;}
        Decimal BTHAmount { set;get;}
        Decimal UpCountryReceivable { set;get;}
        Decimal UpcountryCrossingCost { set;get;}


    }
}