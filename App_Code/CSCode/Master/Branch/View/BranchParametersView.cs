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
/// Summary description for BranchParametersView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IBranchParametersView : IView
    {
        int GCMinStock { get;set;}
        int CRMinStock { get;set;}
        int MemoMinStock { get;set;}
        int LHPOMinStock { get;set;}

        int DefaultBankLedgerId { get;set;}
        int DefaultCashLedgerId { get;set;}

        string BookingStartTime { get;set;}
        string BookingEndTime { get;set;}

        DataTable BindDefaultCashLedger { set;}
        DataTable BindDefaultBankLedger { set;}
    }
}