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
/// Summary description for VehicleChasisTyresView
/// </summary>
/// 
namespace Raj.EF.MasterView
{
        public interface IVehicleChasisTyresView : IView
        {
            string VehicleTyreConfigurationDetailsXML { get;}
            int FrontWheelSizeID { set;get;}
            int FrontTyreSizeID { set;get;}
            int RearWheelSizeID { set;get;}
            int RearTyreSizeID { set;get;}
            int FrontPSI { set;get;}
            int RearPSI { set;get;}
            int NoOfStephaney { set;get;}
            int OldNoOfStephaney { set;get;}

            DataTable BindFrontWheelSize { set;}
            DataTable BindFrontTyreSize { set;}
            DataTable BindRearWheelSize { set;}
            DataTable BindRearTyreSize { set;}
            DataTable BindDualType { set;}
            DataTable SessionDualType { set;get;}
            DataTable BindChasisTyresGrid { set;}
        }
   
}
