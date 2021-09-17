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
/// Summary description for CompanyTripHireParametersView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface ICompanyTripHireParametersView:IView 
    {
        DataTable BindTripHireGrid { set;}
        DataTable BindATHGrid { set;}
        DataTable SessionTripHireParametersGrid { set;get;}
        DataTable SessionATHGrid { set;get;}

        int TripHireDivisionId { set;get;}
        int ATHDivisionId { set;get;}
        int LHPONatureOfPaymentId { set;get;}

        DataTable BindTripHireDivision { set;}
        DataTable BindATHDivision { set;}
        DataTable SessionBookingType { set;get;}
        //DataTable SessionATHDivision { set;get;}
        DataTable SessionDivision { set;get;}
        DataTable BindLHPONatureOfPayment { set;}
        DataTable SessionLHPONatureOfPayment { set;get;}


        string TripHireParametersDetailsXML { get;}
        string ATHDetailsXML { get;}
        int TripExpenseLedgerId { get;}
        bool IsTreatAdvanceForOwnTruckAsExpense { set;get;}
        //bool IsCheckBox { set;get;}
        void SetExpenseLedgerID(string Ledger_Name, string Ledger_ID);
    }
}