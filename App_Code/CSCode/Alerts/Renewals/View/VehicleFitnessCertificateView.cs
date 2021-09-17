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
/// Summary description for VehicleFitnessCertificate
/// </summary>
namespace Raj.EF.MasterView
{

    public interface IVehicleFitnessCertificateView : IView
    {
        int TaskDefinationId { get;}
        DataSet BindVehicleFitnessCertificateGrid { set; }
        DataSet SessionVehicleFitnessCertificateGrid { set; get;}
    }

}
