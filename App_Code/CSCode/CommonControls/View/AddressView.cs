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
namespace Raj.EC.ControlsView
{
    public interface IAddressView
    {
        string AddressLine1 { get;set;}
        string AddressLine2 { get;set;}
        int CityId { get;set;}
        string GSTStateCode { get;set;}
        string PinCode { get;set;}
        string StdCode { get;set;}
        string Phone1 { get;set;}
        string Phone2 { get;set;}
        string MobileNo { get;set;}
        string FaxNo { get;set;}
        string EmailId { get;set;} 
        int KeyId { get;set;}
        bool VisibleMobileNo { set;}
        void SetCityId(string text, string value);
        string Header { set;}
        int RegionDetailID { get;set;}
        bool SMSAlert { get;set;}
        bool eMailAlert { get;set;}

        DataTable SetLables { set;}
        DataTable BindCity { set;}

    }
       
}
