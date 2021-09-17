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
using Raj.EC.OperationView; 

/// <summary>
/// Summary description for VehicleHireBillDetailsView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IVehicleHireBillDetailsView : IView
    {
        int VehicleID { set;get;}
        int BrokerID { set;get;}
        int FreightTypeID { set;get;}
        int FromLocationID {get;}
        int ToLocationID { get;}
        int Driver1ID { get;}
        int Driver2ID {get;}
        int CleanerID {get;}
        int VehicleCapacity { set;get;}
        int TransitDays { set;get;}

        decimal TruckHireCharge { set;get;}
        decimal TotalTruckHireCharge { set;get;}
        decimal AdvanceReceived { set;get;}
        decimal BrokeragePayable { set;get;}
        decimal CollectionChargePayable { set;get;}
        decimal WtGuarantee { set;get;}
        decimal RateKg { set;get;}
        decimal ActualWtPayableValue { set;get;}
        decimal ActualKms { set;get;}
        decimal TDSPercentage { set;get;}
        decimal TDSAmount { set;get;}
        string RefNo { set;get;}
        string Remark { set;get;}
        string VehicleDepartureTime { set;get;}
        DateTime VehicleHireBillDate { set;get;}
        DateTime CommittedDelDate { set;get;}

        string HireBillNoForPrint {set;}
        string VehicleHireBillNo { set;get;}

        void SetFromLocationID(string FromLocationName, string FromLocationID);
        void SetToLocationID(string ToLocationName, string ToLocationID);
        void SetDriver1ID(string Driver1Name, string Driver1ID);
        void SetDriver2ID(string Driver2Name, string Driver2ID);
        void SetCleanerID(string CleanerName, string CleanerID);

        int Next_No { get;set;}
        int Document_Series_Allocation_ID { get;set;}

        DataTable Bind_ddl_BrokerName { set;}
        DataTable Bind_ddl_FreightType { set;}
        Boolean IsHOBSeriesRequired { set;get;}

        
    }
}
