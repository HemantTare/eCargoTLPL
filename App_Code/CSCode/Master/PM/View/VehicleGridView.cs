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
/// Summary description for VehicleGridView
/// </summary>
/// 
namespace Raj.EF.MasterView
{
    public interface IVehicleGridView : IView
    {
        //string VehicleModelName { set;get;}
        //int ManufacturerID { get;set;}
        DataSet Bind_dg_Vehicle { set; }
        DataSet SessionVehicleGrid { set; get;}
    }
    
}