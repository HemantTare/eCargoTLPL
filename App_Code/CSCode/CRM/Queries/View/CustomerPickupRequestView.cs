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
/// Summary description for Customer_PickupRequestView
/// </summary>
/// 
namespace Raj.CRM.QueryView
{
    public interface ICustomerPickupRequestView:IView
    {
        int Packeges { get;set;}
        int BookingTypeModeId { get;set;}
        int PackingTypeId { get;set;}
        int CommodityId { get;set;}
        int ForwardBranchId { get;}
        int VendorId { get;set;}
        int GC_Id { get;}
        int GC_Docket_No { get;}
        long VAMobileNo { get;set;}

        decimal Weight { get;set;}

        String Orgin { get;set;}
        String PickUpNo { set;}
        String Destination { get;set;}
        String PickUpTime { get;set;}
        String Consigner { get;set;}
        String ContactName { get;set;}
        String ContactMobile { get;set;}
        String ContactAddress { get; set;}
        String ContactTelephone { get;set; }
        String ContactEmailId { get;set;}
        String ContactCity { get;set;}
        String ContactState { get;set;}
        //String ForwardTime { get;set;}
        String Reason { get;set;}
        

        String Commodity { get;}
        String PackingType { get;}

        DateTime PickUpDate { get;set;}
        DateTime PickUpDateAndTime { get;}

        void SetForwardBranchId(string value, string text);
        DataTable BindForwardVA { set;}
        DataTable BindBookingTypeMode { set;}
        DataTable BindPackingType { set;}
        DataTable BindCommodity { set;} 
    }
}