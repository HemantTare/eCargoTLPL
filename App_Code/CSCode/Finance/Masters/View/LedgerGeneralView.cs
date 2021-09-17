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
 
namespace Raj.EC.FinanceView
{
    public interface ILedgerGeneralView : IView         
    {
        string LedgerName { set; get;}
        string Alias { set; get;}

        bool IsMaintainBillByBill { get;set;}
        int DefaultCreditPeriod { get;set;}
        decimal CreditLimit { get;set;}
        string TypeOfDutyTax { get;set;}
        int NatureOfPaymentId { get;set;}
        bool IsTDSApplicable { get;set;}
        int TDSDeducteeTypeId { get;set;}
        string TDSDeducteeTypeName { get;set;}
        bool IsLowerDeduction { get;set;}
        string SectionNumber { get;set;}
        decimal TDSLowerRate { get;set;}
        bool IsIgnoreExemptionLimit { get;set;}
        int ServiceTaxCategoryId { get;set;}
        bool IsServiceTaxApplicable { get;set;}
        bool IsExempted { get;set;}
        bool IsFBTApplicable { get;set;}
        int FBTCategoryId { get;set;}
        string NotificationDetail { get;set;}
        string ServiceTaxNo { get;set;}
        string ACNo { get;set;}
        string Income_Tax_No { get;set;}
        string TIN_Sales_Tax_No { get;set;}

        string LedgerUnderId { get;set;}
        DateTime ServiceTaxRegDate { get;set;}
        DateTime DateOfBankReco { get;set;}
        int MainId { get;set;}


        string SelectedHierarchy { get;set;}
        DataTable bind_ddl_Under { set;}
        DataTable bind_ddl_NatureOfPayment { set;}
        DataTable bind_ddl_ServiceTaxCategory { set;}
        DataTable bind_ddl_FBTCategory { set;}
        DataTable bind_ddl_Deductee_Type { set;}
        bool EnableControls { set; }
    }
}
