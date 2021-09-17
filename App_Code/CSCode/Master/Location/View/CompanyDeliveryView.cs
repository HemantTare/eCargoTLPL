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
/// Summary description for CompanyDeliveryView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICompanyDeliveryView : IView
    {
        int DivisionId { set;get;}
        DataTable SessionCompanyDeliveryGrid { set;get;}
        int DeliveryIncomeLedgerId { get;}
        int ServiceTaxLedgerNameId { get;}
        int OctroiReceivableLedgerId { get;}
        string CompanyDeliveryDetails { get;}
        DataTable BindDeliveryIncomeLedger { set;}
        DataTable SessionDeliveryIncomeDropDown{get;set;}
        DataTable BindOctroiReceivableLedger { set;}
        DataTable SessionOctroiReceivableDropDown { get;set;}
        DataTable BindSessionTaxLedger { set;}
        DataTable SessionServiceTaxLedgerDropDown { get;set;}
        DataTable BindDivision { set;}
        DataTable SessionDivision { get;set;}
        DataTable BindBookingType { set;}
        DataTable SessionBookingType { set;get;}
        DataTable BindCompanyDeliveryGrid { set;}
        int SrNo { set;get;}
	}
}
