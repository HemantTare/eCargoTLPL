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
/// Summary description for VehicleModelView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IVehicleModelView : IView
    {
        string VehicleModelName { set;get;}
        int ManufacturerID { get;set;}
        DataSet Bind_ddl_Manufacturer { set; }

        decimal ThappiWeight { get;set;}
    }
}   