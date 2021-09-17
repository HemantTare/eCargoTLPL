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
/// Summary description for AccountTransferView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IAccountTransferView : IView
    {
        int VAId { get;set;}
        string Flag { get;}
        string Remarks { get;set;}
        string AccountTransferNo { set;}
        int Total_GC { set;}
        int Total_Article { set;}
        Decimal Total_Weight { set;}
        Decimal Total_Freight { set;}
        Decimal Total_ServiceTax { set;}
        Decimal Total_GCAmount { set;}

        DateTime AccountTransferDate { get;set;}

        DataTable BindVA { set;}
        DataTable SessionBindAccountTransferGrid { set;}
        string AccountTransferDetailsXML { get;}

        void ClearVariables();
    }
}
