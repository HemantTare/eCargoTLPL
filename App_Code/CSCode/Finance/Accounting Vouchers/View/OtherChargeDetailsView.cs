using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

namespace Raj.EC.FinanceView
{
    public interface IOtherChargeDetailsView : ClassLibraryMVP.General.IView
    {
        decimal TotalAmount { get;set;}
        String OtherDetailsXML { get;}
        DataTable Bind_OtherDetailsGrid { set;}
        DataTable Session_OtherDetailsGrid { get;set;}
        //DataTable Session_ddl_Ledger { get;set;}
        //DataTable Bind_ddlLedger { set;}

    }
}