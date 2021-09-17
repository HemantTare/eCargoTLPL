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
/// Summary description for VehicleSearchView
/// </summary>

namespace Raj.EC.ControlsView
{


    public interface IVehicleSearchView
    {
        int VehicleID { get;set;}

        String VehicleNumber {  get;}
      

    }
       
 

} 