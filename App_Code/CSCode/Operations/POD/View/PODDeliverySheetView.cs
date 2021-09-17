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
using Raj.EC.OperationView;
using Raj.EC.ControlsView; 

/// <summary>
/// Summary description for PODDeliverySheetView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IPODDeliverySheetView : IView
    {
        string PODDeliverySheetNo { get;set;}
        string Remark { get;set;}
        DateTime PODDeliveryDate { get;set;}
        string Flag { get;}
        //Bind Property Declaration
        DataTable SessionPODDeliverySheet { set;get;}
        String PODDeliverySheetDetailsXML { get;}
        IPODSentByView PODSentByView { get;}
        string PODDeliveredTo { set;get;}

        void ClearVariables();
    }
}