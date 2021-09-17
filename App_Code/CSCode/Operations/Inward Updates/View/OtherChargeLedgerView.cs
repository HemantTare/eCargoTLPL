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

namespace Raj.EC.OperationView
{
    public interface IOtherChargeLedgerView : ClassLibraryMVP.General.IView
    {
        decimal TotalAmount { get;set;}
        String OtherDetailsXML { get;}
        DataTable Bind_OtherDetailsGrid { set;}
        DataTable Session_OtherDetailsGrid { get;set;}
      
    }
}