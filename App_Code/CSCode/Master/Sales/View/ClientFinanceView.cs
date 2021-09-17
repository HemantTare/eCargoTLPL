using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;
/// <summary>
/// Summary description for ClientFinanceView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IClientFinanceView : IView
    {
        bool Is_ExistingLedger { get;set;}
        int ClientGroupId { get;}
        int LedgerID { get;}
        int CreditDays { get;set;}
        decimal CreditLimit { get;set;}
        decimal IntrestPercent { get;set;}
        decimal MinimumBalance { get;set;}
        int GraceDays { get;set;}
        bool IsServiceTaxPayByClient { get;set;}
        bool IseCargoUser { get;set;}
        int UserProfileId { get;set;}
        string BusinessHours { get;set;}
        bool Is_LoadingTypeId { get;set;}
        DateTime RegistrationDate { get;set;}

        int MarketingExcutiveId { get;}

        void SetLedgerId(string text, string value);
        void SetMarketingExcutiveId(string text, string value);

        //DataTable BindLedger { set;}
        DataTable BindUserProfile { set;}
        //DataTable BindMarketingExecutive { set;}
        bool Is_CreditParty { get;set;}
        bool IsPrintFrtOnLR { get;set;}
    }
}

