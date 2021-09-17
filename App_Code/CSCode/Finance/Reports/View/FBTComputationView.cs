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
/// Summary description for FBTComputationView
/// </summary>
namespace Raj.FA.ReportsView
{
    public interface IFBTComputationView : IView
    {
        DataTable BindFBTGrid{set;}
        DateTime Start_Date{get;set;}
        DateTime End_Date { get;set;}
        string Hierarchy_Code{get;set;}
        int Main_Id { get;set;}
        Boolean Is_Consol { get;set;}
        string FringeBenefitPercent{set;}
        string FringeBenefitAmount{set;}
        string SurchargePercent{set;}
        string SurchargeAmount{set;}
        string EducationCessPercent{set;}
        string EducationCessAmount { set;}
        string AddlEducationCessPercent{set;}
        string AddlEducationCessAmount{set;}
        string TotExpenditureAmount{set;}
        string TotAmountRecovered{set;}
        string TotNetExpenditure{set;}
        string TotPercentageAsPerSec{set;}
        string TotValueOfFringeBenefit{set;}
        string TotalTaxPayable { set;}

	}
}
