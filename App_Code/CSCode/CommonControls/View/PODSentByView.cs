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
/// Summary description for PODSentByView
/// </summary>


namespace Raj.EC.ControlsView
{
    public interface IPODSentByView : IView
    {
        bool IsDllSentByAlreadyBinded { get;set;}
        string CourierName { get;set;}
        string CourierDocketNo { get;set;}
        int EmployeeID { get;}
        int SentByID { get;set;}
        DataTable Bind_ddl_SentBy { set;}
        void SetEmployeeId(string text, string value);
        IVehicleSearchView VehicleSearchView { get;}
        int VehicleID { set;get;}
    }
}