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
/// Summary description for LabelPrintingView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface ILabelPrintingView : IView
    {
        int Total_No_Of_GC { get;set;} 
        string Flag { get;} 
     
        
        DataTable SessionBindLabelPrintingGrid { set;}

        string LabelPrintingDetailsXML { get;}
        string GetGCNoXML { get;set;}

        void ClearVariables();
    }
}
