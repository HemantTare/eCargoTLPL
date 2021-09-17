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
/// Summary description for OperationMusterDailyEntryView
/// </summary>
/// 


namespace Raj.eCargo.Operation.Muster.View
{
    public interface IOperationMusterDailyEntryView:IView
    {
       
        DataTable Bind_MusterEntryDaily { set;}
        DataSet SessionMusterDaily { get;set;}
        DataSet SessionCalculatedDS { get;set;}
        bool check { get;set; }

    }
}