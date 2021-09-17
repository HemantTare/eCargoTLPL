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
/// Summary description for VendorView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IVendorView : IView
    {
        string  VendorName { set;get;}
        //int TdsId { set;get;}
        int VendorTypeId { set;get;}
        //bool IsTdsApplicable { set;get;}
       //decimal TdsExemptionLimit { set;get;}
        string ReferenceName { set;get;}
       //decimal TdsRatePercent { get;}
        string ReferencePhone { set;get;}
        string ReferenceMobile { set;get;}
        int Credit_Days { set;get;}
        int Credit_Limit { set;get;}
        int Debit_BalLimmit { set;get;}
        string PanNo { set;get;}
        IAddressView AddressView { get;}
        //DataSet BindTds { set;}
        DataSet BindVendorType { set;}

        ITDSAppView TDSAppView { get;}

        string APMCBroker_To_City { set;get;}
    }
}
