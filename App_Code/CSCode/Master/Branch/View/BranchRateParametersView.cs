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
/// Summary description for BranchRateParametersView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IBranchRateParametersView : IView
    {
        int CFTFactor { get;set;}
        int FirstNoticeDays { get;set;}
        int SecontNoticeDays { get;set;}
        int ThirdNoticeDays { get;set;}
        int DemurrageDays { get;set;}
        int ToBranchID { get;}
        int FromBranchID { get;}

        decimal MinChargeWt { get;set;}
        decimal BiltyCharges { get;set;}
        decimal FOVPercent { get;set;}
        decimal MinFOV { get;set;}
        decimal Hamali { get;set;}
        decimal MinHamali { get;set;}
        decimal DoorDeliveryCharges { get;set;}
        decimal ToPayCharges { get;set;}
        decimal DACCCharges { get;set;}
        decimal ServiceTax { get;set;}
        decimal DemurrageRate { get;set;}
        decimal OctroiFormCharges { get;set;}
        decimal OctroiServiceCharge { get;set;}
        decimal GICharges { get;set;}
        decimal DeliveryCommission { get;set;}
        decimal CashLimit { get;set;}
        decimal BankLimit { get;set;}
        string ToBranchName { set;}


        decimal Bkg_Freight { get;set;}
        decimal Bkg_HamaliofBooking { get;set;}
        decimal Bkg_FovofBooking { get;set;}
        decimal Bkg_TpCharge { get;set;}
        decimal Bkg_Ddcharge { get;set;}

        decimal Dly_Octroiformchargepercent { get;set;}
        decimal Dly_Octroiservicechargepercent { get;set;}
        decimal Dly_GichargesofDel { get;set;}
        decimal Dly_HamaliofDel { get;set;}
        decimal Dly_Demurrage { get;set;}
        decimal HamaliArticle { get;set;}
        decimal FOVRate { get;set;}
        decimal InvoiceRate { get;set;}
        decimal InvoicePerHowManyRs { get;set;}
        decimal MaxBiltyCharges { get;set;}
        decimal AOCPercent { get;set;}
        Boolean IsFOVCalculatedAsPerStandard { set;get;}
    }
}
