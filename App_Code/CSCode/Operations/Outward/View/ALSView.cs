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
/// Summary description for ALSView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IALSView : IView
    {
        int VehicleCotegoryID { get;set;}
        int VehicleID { get;set;}
        int SupervisorID { get;}

        int Total_Loded_Articles { get;set;}
        int Total_No_Of_GC { get;set;}
        string Flag { get;}
        string Remarks { get;set;}
        string ALSNo { set;}

        DateTime ALSDate { get;set;}

        decimal Total_Loded_Weight { get;set;}

        DataTable BindVehicleCotegory { set;}
        DataTable SessionBindALSGrid { set;}

        void SetSupervisorID(string text, string value);

        string ALSDetailsXML { get;}
        string GetGCNoXML { get;set;}

        void ClearVariables();
    }
}
