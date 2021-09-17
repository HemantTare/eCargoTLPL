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
 
namespace Raj.EC.FinanceView
{
    public interface IRecoAndBillDetailsView : IView         
    {
        decimal OpeningBalance { set; get;}
        decimal ClearedAmount  { set; get;}

        decimal TotalBillWise { get;}
        decimal TotalBankReco {get;}

        decimal SetTotalLable { set;}

        string GetBankRecoXML { get;}
        string GetBillWiseXML { get;}

        bool VisibleBillWise { set;}
        bool VisibleBankReco { set;}

        bool IsBankReco { set; get;}
        bool IsBillWise { set; get;}

        int OpeningDrCr { set; get;}

        DataTable Bind_dg_BankReco { set;}
        DataTable Bind_dg_BillWise { set;}

    }
}
