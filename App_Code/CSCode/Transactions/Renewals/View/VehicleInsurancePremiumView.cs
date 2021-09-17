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
/// Summary description for VehicleInsurancePremiumView
/// </summary>
/// 
namespace Raj.EF.TransactionsView
{
    public interface IVehicleInsurancePremiumView : IView
    {
        int VehicleID { set;get;}
        int VehicleTypeID { get;}
        int VehicleNo { set;}
        int VehicleInsuranceID { set;get;}
        int InsuranceCompanyID { set;get;}
        int IssuingBranchID { set;get;}
        string PolicyNo { set;get;}
        int AgentID { set; get;}
        int Bank_ID { get;set;}
        DateTime InsuranceDate { get;set;}
        DateTime CommenceDate { set;get;}
        DateTime ExpiryDate { set;get;}
        DateTime ChequeDate { get;set;}
        decimal IDV { set;get;}
        string Insurance_No { get;set;}
        string ChequeNo { get;set;}
        string EngineNo { set;get;}
        string ChasisNo { set;get;}
        bool Is_Cheque { set; get;}
        //bool SetVisibleTrueFalse { set;}
        decimal FirstPartyPremium { set;get;}
        decimal ThirdPartyPremium { set;get;}
        decimal LoadingPercentTPP { set;get;}
        decimal LoadingAmountTPP { set;get;}
        decimal LoadingPercentFPP { set;get;}
        decimal LoadingAmountFPP { set;get;}
        decimal NCBPercentFPP { set;get;}
        decimal NCBAmount { set;get;}
        decimal NetPremium { set;get;}
        decimal ServiceTaxPercentage { set;get;}
        decimal ServiceTaxAmount { set;get;}
        decimal NetPayable { set;get;}

        DataTable BindBankName { set;}
        DataTable BindInsuranceCompany { set;}
        DataTable BindIssuingBranch { set;}
        DataTable BindAgent { set;}
        DataTable BindPremiumType { set;}
        DataTable SessionPremiumTypeDropdown { set;get;}
        DataTable BindPremiumDetailsGrid { set;}
        string InsurancePremiumDetailsXML { get;}
    }
}
