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

namespace Raj.EC.FinanceView
{
    public interface IMRDeliveryView : IView
    {
        IMRCashChequeDetailsView MRCashChequeDetailsView { get;}
        IMRGeneralDetailsView MRGeneralDetailsView { get;}
        IMRDeliveryDetailsView MRDeliveryDetailsView { get;}
        decimal RebookedCharges { get;set;}
        decimal TotalReceivables { get;set;}
        string GCTotal { get;set;}
        int RoundOff { get;set;}
        decimal SubTotal { get;set;}
        decimal TaxAbate { get;set;}
        decimal AmountTaxable { get;set;}
        decimal ServiceTax { get;set;}
        decimal OctrAmount { get;set;}
        string ServiceTaxBy { get;set;}
        int DemurageDays { get;set;}
        string OctrRecNo { get;set;}
        string OctrFormType { get;set;}
        DateTime OctrRecDate{get;set;}
        DateTime DeliveryDate { get;set;}
        string OctrPaidBy { get;set;}
        string AddChrgRemark { get;set;}
        string DiscountRemark { get;set;}
        decimal OctrFormCharges { get;set;}
        decimal OctrServiceCharges { get;set;}
        decimal GICharges { get;set;}
        decimal DetentionCharges { get;set;}
        decimal HamaliCharges { get;set;}
        decimal LocalCharges { get;set;}
        decimal DemurageCharges { get;set;}
        decimal AdditionalCharges { get;set;}
        decimal DiscountAmount { get;set;}
        decimal TotalAmount { get;set;}
        decimal DeliveryCommission { get;set; }

        int Payment_Type_ID { get;set;}
        int Booking_Type_ID { get;set;}
        decimal GCSubTotal { get;set;}
        decimal GC_Total_Amount { get;set;}
        decimal Charged_Wt { get;set;}

        decimal Std_Octroi_Form_Charges{get;set;}
        decimal Std_Octroi_Service_Charges{get;set;}
        decimal Std_GI_Charges{get;set;}
        decimal Std_Hamali_Charges{get;set;}
        decimal Std_Demurage_Charges { get;set;}
        decimal Service_Tax_Percent { get;set;}
        int Service_Pay_By_ID { get;set;}
        int Document_ID { get;set;}
        int Debit_To_Ledger_ID { get;}
        int Debit_To_Branch_ID { get;}
        int Credit_Memo_ForID { get;set;}
        bool Is_Credit_For_Consignee { get;set;}

        void Set_DebitTo_LedgerID(string txt,string val);
        void Set_DebitTo_BranchID(string txt, string val);

        void Set_Values_Delivery_Add_Edit(DataSet ds);
        void Calculate_Grand_Total();

        DataTable BindMemoFor { set;}
        string Flag { get;}
        int ReceivedBy { get;set;}

        void ClearVariables();
        DataTable SessionDeliveredTo { set;get;}
        DataTable SessionDeliveredAgainst { set;get;}



    }
}