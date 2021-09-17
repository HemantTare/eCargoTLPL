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
/// Summary description for VendorTypeSelectionView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IVendorTypeSelectionView : IView
    {


        int KeyNameId { set;get;}
        DataSet BindKeyName {set;}
        int ChkVendorType { set;get;}
        DataSet BindChkListVendorType { set;}
        DataSet SessionChkListVendorType { get;set;}
        string ChkListVendorTypeDetails { get;}


    }
}
