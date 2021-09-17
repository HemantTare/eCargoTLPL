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
/// Summary description for VehiclePMView
/// </summary>
/// 

namespace Raj.EF.MasterView
{

    public interface IVehiclePMView : IView
    {
        int Vehicle_Id { set;get;}
        DataSet Bind_dg_Grid { set; }
        DataSet SessionVehicleGrid { set; get;}
    }

}