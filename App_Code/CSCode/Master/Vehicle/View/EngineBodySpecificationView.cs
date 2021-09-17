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
/// Summary description for EngineBodySpecificationView
/// </summary>
/// 
namespace Raj.EF.MasterView
{
    public interface IEngineBodySpecificationView : IView
    {
        string ChasisNo { set;get;}
        string TrollyChasisNo { set;get;}
        string EngineNo { set;get;}
        string Power { set;get;}
        string PaintCode { set;get;}
        string PaintColor { set;get;}
        string IgnitionKeyCode { set;get;}
        string DoorKeyCode { set;get;}
        string VehicleSpecificationDetailsXML { get;}

        int FuelTypeID { set;get;}
        int GrossVehicleWt { set; get;}
        int UnladenWt { set;get;}
        int VehicleCapacity { set;get;}

        decimal WheelBase { set;get;}
        decimal Length { set;get;}
        decimal Height { set;get;}
        decimal Width { set;get;}
        decimal FuelTankCapacity { set;get;}

        DataSet BindFuelType { set;}
        DataTable BindEngineBodyGrid { set;}
        //string EngineBodyDetails { get;}
    }
}

