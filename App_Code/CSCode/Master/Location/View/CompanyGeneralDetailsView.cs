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
using Raj.EC.ControlsView;


/// <summary>
/// Summary description for CompanyGeneralDetailsView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICompanyGeneralDetailsView : IView
    {
        int CompanyId { set;get;}
        string CompanyName { set;get;}
        string MailingName { set;get;}
        int HOLedger { get;}
        int PFALedger { get;}
        void SetHOLedgerId(string text, string value);
        void SetPFALedgerId(string text, string value);
        IAddressView AddressView { get;}
        string ClientCode { set;get;}
        int HOCashLedger { get;}
        int HOBankLedger { get;}
        void SetHOCashLedger_Id(string text, string value);
        void SetHOBankLedger_Id(string text, string value);


	}
}
