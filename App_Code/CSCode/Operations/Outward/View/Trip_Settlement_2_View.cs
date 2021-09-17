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

/// <summary>
/// Summary description for Trip_Settlement_2_View
/// </summary>
namespace Raj.EC.OperationView
{
    public interface ITrip_Settlement_2_View : IView
    {
        string Vehicle_Trip_No { get;set;}
        DateTime Vehicle_Trip_Date { get;set;}
        int Vehicle_ID { get;set;}
        DateTime Trip_Start_Date { get;set;}
        DateTime Trip_End_Date { get;set;}
        int Driver_ID { get;}
        decimal Total_Actual_Wt { get;set;}
        decimal Total_Hire_Amount { get;set;}
        decimal Total_Advance { get;set;}
        decimal Total_Fuel_Qty { get;set;}
        decimal Total_Fuel_Amount { get;set;}
        decimal Total_Oil_Amount { get;set;}
        decimal Total_Fuel_Oil_Cost { get;set;}
        decimal Total_Trip_Expense { get;set;}
        decimal Total_Trip_Cost { get;set;}
        decimal Driver_Closing_Balance { get;set;}
        string Remarks { get;set;}
        
        // TripDetails

        string OBDrCr { set;}

        decimal DriverOpeningBalance { get;set;}
        decimal TotalDieselCash { get;set;}
        decimal TotalDieselCredit { get;set;}

        int Mode { get;}

        void SetDriverID(string text, string value);

        int Total_KM_Run { set;get;}
        DataTable SessionFromBranchDropdown { set;}
        DataTable SessionToBranchDropdown { set;}
        DataTable SessionTripHireChallansGrid { set;}
        DataTable SessionPumpDropDown { get;set;}
        string TripHireChallansDetailsXML { get;}

        // TripExpenses
        DataTable SessionExpenseHeadDropdown { set;}
        DataTable SessionTripExpenseGrid { set;}
        string TripExpenseDetailsXML { get;}

        // FuelDetails 
        DataTable SessionTripFuelDetailsGrid { set;}
        string TripFuelDetailsXML { get;}

    }
}