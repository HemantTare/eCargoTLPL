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
    public interface IMRCashChequeDetailsView :IView
    {
        decimal CashAmount { get;set;}
        decimal ChequeAmount { get;set;}
        decimal Total_ChequeAmount { get;set;}

        int CashLedgerID { get;set;}
        int DepositInID { get;set;}

        DataTable Bind_ddlCashLedger { set;}
        DataTable Bind_ChequeDetailsGrid { set;}
        DataTable Bind_ddlDepositIn { set;}

        DataTable Session_ChequeDetailsGrid { get;set;}
        DataTable Session_ddl_DepositIn { get;set;}

        String MRChequeDetailsXML { get;}
        


    }
}