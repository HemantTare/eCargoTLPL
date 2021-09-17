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
using Raj.EC.MasterView;

/// <summary>
/// Summary description for TransitDaysView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ITransitDaysView : IView
    {
        int CityID { get;set;}
        int ToCityID { get;set;}
        int TransitDays { get;set;}
        int DistanceInKM { get;set;}
        int TransitDaysID { get;set;}
        DataTable Bind_ddl_City { set;}
        int StateID { get;set;}
        DataTable Bind_ddl_State { set;}
        int VehicleID { get;set;}
        DataTable Bind_ddl_Vehicle { set;}
        DataTable Bind_dg_TransitDays { set;}
        DataTable SessionTransitDaysGrid { set;get;}
    }
}