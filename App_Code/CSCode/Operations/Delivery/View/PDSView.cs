using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface IPDSView : IView
    {
        int DeliveryModeID { get;set;}
        int VAID { get;set;}
        int SupervisorID { get;}
        int VendorID { get;set;}
        int VehicleID { get;set;}
        DateTime PDSDate { get;set;}
        string Flag { get;}
        string DeliveryModeDescription { get;set;}
        string DiverName { get;set;}
        string VendorName { get;set;}
        string MobileNumber { get;set;}
        string strValidate_Vehicle { get;set;}
        
        string Remarks { get;set;}
        string PDSNo { set;}

        int Total_Bal_Articles { set;}
        decimal Total_Bal_Weight { set;}
        int Total_Delivered_Articles { set;}
        decimal Total_Delivered_Weight { set;}
        int Total_No_Of_GC { get;set;}
        decimal Total_GC_Amount { get;set;}

        DataTable BindDeliveryMode { set;}
        DataTable BindAssociates { set;}
        DataTable SessionBindPDSGrid { get;set;}

        void SetSuperviserId(string text, string value);
        string PDSDetailsXML { get;}
        string GetGCNoXML { get;set;}

        void ClearVariables();
    }
}