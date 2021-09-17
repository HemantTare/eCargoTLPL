
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Raj.EC.MasterView;
using Raj.EC.FinanceView;
 using Raj.EC.ControlsView;

 
/// <summary>
/// Summary description for DirectDeliveryView
/// </summary>

namespace Raj.EC.OperationView
{
    public interface IDirectDeliveryView : ClassLibraryMVP.General.IView
    {
        int Vehicle_Id { get;set;}
        int Vehicle_Category_Id { get;set;}
        int LHPO_Id { set; get;}
        int Memo_Id { get;set;}
        int GC_Id { get;}
        string Flag { get;}
        int NoofMinuteDifferenceForLate { get;set;}
        int Delivery_Condition { get;set;}
        int Reason_For_Late_Uploading { get;set;}
        int Booking_Branch_Id { get;set;}    
        int Delivery_Location_Id { get;set;}      
        int Payment_Type_Id { get;set;}    
        int Booking_Articles { get;set;}   
        int Loaded_Articles { get;set;}
        int Delivered_Articles { get;set;}   
        int Damage_Leakage_Articles { get;set;}    
        int Short_Articles { get;set;}
        
        int Previous_Article_ID { get;set;}
        int Previous_Status_ID { get;set;}
        int Previous_Document_ID { get;set;}

        int DeliveryToID{ get;set;}
        int ConsigneeCopyID{ get;set;}
        int DeliveryAgainstID{ get;set;}

        Boolean Is_PODReceived { get;set;}

        Boolean Is_OctroiApplicable { get;set;}
        Boolean Is_OctroiUpdated { get;set;}
        
        String Previous_Document_No_For_Print { get;set;}        
        String DDC_No_For_Print { get;set;}
        String BookingDate { get;set;}     
        String Payment_Type { get;set;}
        String Delivery_Location { get;set;}
        String Booking_Branch { get;set;}
        String LHPO_From { get;set;}
        String LHPO_To { get;set;}
        String Memo_From { get;set;}
        String Memo_To { get;set;}
        String Delivery_Taken_By { get;set;}
        String ScheduledArivalDate { get;set;}
        String ScheduledArivalTime { get;set;}
        String LHPO_Date { get;set;}
        String Memo_Date { get;set;}
        String Memo_No { get;set;}
        String Vehicle_Category { get;set;}
        String Remarks { get;set;}

        DateTime ActualDeliveryDate { get;set;}        
        DateTime DDC_Date { get;set;}
        DateTime Previous_Document_Date { get;set;}

        String Delivery_Time { get;set;}
                
        DataSet BindLHPO { set;}

        DataTable  BindDeliveryCondition  { set;}
        DataTable BindResionForLateDelivery { set;}
        
        Decimal Total_GC_Amount { get;set;}
        Decimal Damage_Leakage_Articles_Value { get;set;}
        Decimal Delivered_Articles_Weight { get;set;}
        Decimal Loaded_Articles_Weight { get;set;}
        Decimal Booking_Articles_Weight { get;set;}
        
        //common adress control   
        IVehicleSearchView VehicleSearchView { get;}
        IMRCashChequeDetailsView MRCashChequeDetailsView { get;}

        void SetGC(string text, string value);
        void SetLHPO(string text, string value);

        void ClearVariables();

        Boolean IsFreightReceived { get;set;}

        int ReceivedBy { get;set;}
        int Debit_To_Ledger_ID { get;}
        int Debit_To_Branch_ID { get;}

        void Set_DebitTo_LedgerID(string txt, string val);
        void Set_DebitTo_BranchID(string txt, string val);

        Decimal TDS { get;set;}

    }
}