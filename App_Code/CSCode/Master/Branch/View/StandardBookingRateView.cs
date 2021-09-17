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
/// Summary description for StandardBookingRateView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IStandardBookingRateView : IView
    {
        int BookingBranchId { get;}
        int DeliveryBranchId { get;}

        Decimal ProfitRatio { set;get;}
        Decimal BookingRate { set;get;}

        DateTime ApplicableFromDate { set;get;}

        void SetBookingBranchId(string text, string value);
        void SetDeliveryBranchId(string text, string value);

        DataTable SessionStandardRateGrid { set;}
        string CrossingPointXML { get;}

        void ClearVariables();
    }
}

