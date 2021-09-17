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
/// Summary description for RegionView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IRegionView : IView
    {
        string RegionCode { set;get;}
        string RegionName { set;get;}
        DataSet BindCountry { set;}
        int CountryId { set;get;}
        int BankLedgerId { set;get;}
        int CashLedgerId { set;get;}
        DataTable BindCashLedger { set;}
        DataTable BindBankLedger { set;}

    }
}

