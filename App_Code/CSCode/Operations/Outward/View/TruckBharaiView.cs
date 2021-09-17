using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface ITruckBharaiView : IView
    {
       
        int SupervisorID { get;}
        
        int VehicleID { get;set;}
        DateTime TransactionDate { get;set;}
        string Flag { get;} 
        string strValidate_Vehicle { get;set;} 
        string Remarks { get;set;}
        string TransactionNo { set;}
        string strMemo_Ids { get;set;}
        int Total_No_Of_GC { get;set;}
        int Total_SelectedMemo { get;set;}
        decimal Total_Hamali_Charges { get;set;}
        decimal Total_Hamali_Paid { get;set;} 
        DataTable SessionBindTruckBharaiGrid { get;set;}
        DataTable SessionBindSelectedMemoGrid { get;set;}

        void SetSuperviserId(string text, string value);
        string TruckBharaiDetailsXML { get;}
        

        void ClearVariables();
    }
}