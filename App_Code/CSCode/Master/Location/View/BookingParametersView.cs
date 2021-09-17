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
/// Summary description for BookingParametersView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IBookingParametersView : IView
    {
        bool IsTreatBookingIncomeAdvIncome { set;get;}
        bool IsToBilledAccountingGCWise { set;get;}
        bool IsBookingMoneyReceiptRequired { set;get;}
        bool IsDebitTodelivery { set;get;}
        int DivisionId { set;get;}

        DataTable SessionBookingParametersGrid { set;get;}
        int AdvanceBookingIncomeLedgerId { get;}
        int BookingIncomeLedgerId { get;}
        int ServiceTaxLedgerNameId { get;}
        int OtherChargeLedgerId{ get;}
        int UpcountryCostAC_ID { get;}
        string BookingParametersDetails { get;}
        DataTable BindAdvanceBookingIncomeLedger { set;}
        DataTable SessionAdvanceBookingIncomeDropDown { set;get;}
        DataTable BindBookingIncomeLedger { set;}
        DataTable SessionBookingIncomeDropDown { set;get;}
        DataTable BindSessionTaxLedger { set;}
        DataTable BindSessionOtherChargeLedger{ set;}
        DataTable SessionServiceTaxLedgerDropDown { set;get;}
        DataTable SessionOtherChargeLedgerDropDown{ set;get;}
        DataTable BindDivision { set;}
        DataTable SessionDivision { set;get;}
        DataTable BindBookingType { set;}
        DataTable SessionBookingType { set;get;}
        DataTable BindPaymentType { set;}
        DataTable SessionPaymentType { set;get;}
        DataTable BindBookingParametersGrid { set;}
        int SrNo { set;get;}
        int ShortTermBillLedgerID { get; }
        void SetShortTermBillLedgerID(string text, string value);

        int PayforBookigBranchID { get;set; }
        void SetPayforBookigBranchID(string text, string value);
        int PayforCrossingBranchID { get;set; }
        void SetPayforCrossingBranchID(string text, string value);
        int PayforDeliveryBranchID { get;set; }
        void SetPayforDeliveryBranchID(string text, string value);
        int DeliveryCommisionIncomeID{get;}
        int DeliveryCommisionExpenseID{get;}
        int LHPOOtherChargesExpenseID{get;}
        int LHPOOtherChargesPaybleID{get;}
        int LorryPayble_ATH_BTH_ID {get;}
        void SetDeliveryCommisionIncomeID(string text, string value);
        void SetDeliveryCommisionExpenseID(string text, string value);
        void SetLHPOOtherChargesExpenseID(string text, string value);
        void SetLHPOOtherChargesPaybleID(string text, string value);
        void SetLorryPayble_ATH_BTH_ID(string text, string value);
        void SetUpcountryCostAC_ID(string text, string value);
	}
}
