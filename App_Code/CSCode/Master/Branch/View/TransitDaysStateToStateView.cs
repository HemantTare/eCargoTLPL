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
/// Summary description for TransitDaysStateToStateView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface ITransitDaysStateToStateView : IView
    {
        int FromStateID { get;set;}
        int ToStateID { get;set;}
        int TransitDays { get;set;}
        int DistanceInKM { get;set;}
        int VehicleID { get;set;}

        DataTable Bind_ddl_FromState { set;}        
        DataTable Bind_ddl_ToState { set;}        
        DataTable Bind_ddl_Vehicle { set;}

    }   
}