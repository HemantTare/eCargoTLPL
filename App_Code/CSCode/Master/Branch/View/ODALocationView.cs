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
/// Summary description for ODALocationView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IODALocationView : IView
    {
        string LocationName { set;get;}
        int BranchId { set;get;}
        int DistanceFromBranch { set;get;}
        int CityId { get;}

        int LocationId { set;get;}
        int DeliveryTypeID { get;set;}

        string PrimaryPinCode { set;get;}
        string SecondryPinCode { set;get;}
        bool IsBooking { set;get;}
        bool IsODALocation { set;get;}
        bool IsOctroiApplicable { set;get;}
        Decimal ODAChargeUpto { set;get;}
        Decimal ODAChargeAbove { set;get;}
        DataTable BindControllingBranch { set;}
        DataTable BindDeliveryType { set;}
        void SetCityId(string text, string value);       


    }
}
