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
/// Summary description for VehicleLoanDetailsView
/// </summary>
namespace Raj.EF.MasterView
{
    
    public interface IVehicleLoanDetailsView : IView
    {
        string LoanAcctNo { set;get;}
        string Comments { get;set;}
        string VehicleLoanDetailsXML { get;}

        int BankID { get;set;}
        int TermsInMonths { set;get;}
        int InterestTypeID { get;set;}
        int PaymentModeID { get;set;}
        int PaymentBankID { get;set;}
        int StartChequeNo { get;set;}

        Decimal LoanAmount { set;get;}
        Decimal RateOfInterest { set;get;}
        Decimal EMIAmount { set;get;}

        DateTime FirstPaymentDue { get;set;}
        DateTime LastPaymentDue { get;set;}

        DataTable Bind_ddl_InterestType { set; }
        DataTable Bind_ddl_Bank_Name { set; }
        DataTable Bind_ddl_PaymentMode { set; }
        DataTable Bind_ddl_PaymentBank_Name { set; }
        DataTable SesssionPaymentDetailsDT { get;set;}
        DataTable SesssionBankNameDT{ get;set;}
        DataTable Bind_dg_Payment_Details { set; }


        
        
    }

}
	