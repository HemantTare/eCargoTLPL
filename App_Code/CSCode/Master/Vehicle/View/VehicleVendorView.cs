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
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for VehicleVendorView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IVehicleVendorView : IView
    {
        string  VehicleVendorName { set;get;}
        int TdsId { set;get;}
        int VendorTypeId { set;get;}
        bool IsTdsApplicable { set;get;}
        string TdsExemptionLimit { set;get;}
        string ReferenceName { set;get;}
        string TdsRatePercent { get;}
        string ReferencePhone { set;get;}
        string ReferenceMobile{set;get;}
        string PanNo { set;get;}
        IAddressView AddressView { get;}
        DataSet BindTds { set;}
        DataSet BindVendorType { set;}


    }
}
