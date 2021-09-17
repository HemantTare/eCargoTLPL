using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface IGDCView : IView
    {
        int SupervisorID { get;}
        DateTime GDCDate { get;set;}
        string Flag { get;}
        string Remarks { get;set;}
        string GDCNo { set;}

        int Total_Delivered_Articles { set;}
        decimal Total_Delivered_Weight { set;}
        int Total_No_Of_GC { get;set;}

        DataTable SessionBindGDCGrid { get;set;}
        DataTable SessionBindDDLDeliveryMode { set;}
        DataTable SessionBindDlyStatus { set;}
        DataTable SessionBindDDLUndelReason { set;}

        void SetSuperviserId(string text, string value);
        string GDCDetailsXML { get;}
        string GetGCNoXML { get;set;}

        void ClearVariables();

        DataTable BindPhotoType { set;}
        DataTable BindVehicleType { set;}

        string DeliveredTo { get;set;}
        string DeliveredToMobile { get;set;}

        int PhotoIDType { get;set;}
        string PhotoIDNo { get;set;}

        int VehicleType { get;set;}

        string VehicleNoPart1 { get;set;}
        string VehicleNoPart2 { get;set;}
        string VehicleNoPart3 { get;set;}
        string VehicleNoPart4 { get;set;}
    }
}