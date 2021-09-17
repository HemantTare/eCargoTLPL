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
using Raj.EC.OperationView;
using Raj.EC.ControlsView;

/// <summary>
/// Author       : Anita Gupta
/// Description  : Truck Arrival System View
/// Date         : 16 Jan 09 
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface ITASView : IView
    {
        String TASNo { get;set;}
        DateTime TAS_Date { get;set;}
        String TAS_Time { get;set;}
        
        int Vehicle_Id { get;set;}
        int Vehicle_Category_Id { get;set;}
        String Vehicle_Category { get;set;}
        //common adress control   
        IVehicleSearchView VehicleSearchView { get;}
        string Flag { get;}
        int LHPO_Id { get;}
        void SetLHPO(string text, string value);
        String LHPO_Date { get;set;}
        DataSet BindLHPO { set;}
        int TAS_Rec_Count { get;}

        String ScheduledArrivalDate { get;set;}
        String ScheduledArrivalTime { get;set;}

        int NoofMinuteDifferenceForLate { get;set;}
        //DateTime ActualArrivalDate { get;set;}
        int Reason_For_Late_Arrival { get;set;}
        String Remarks { get;set;}

        DataTable BindReasionForLateTruckArrival { set;}
        DataTable Bind_dg_TASDetails { set;}
        String TAS_Details_Xml { get;}
        int MenuItemId { get; }
        string LHPOFromLocation { get;set;}
        string LHPOToLocation { set;get;}

        void ClearVariables();
    }
}
