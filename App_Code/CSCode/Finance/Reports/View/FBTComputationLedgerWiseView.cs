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
/// Summary description for FBTComputationLedgerWiseView
/// </summary>
namespace Raj.FA.ReportsView
{
    public interface IFBTComputationLedgerWiseView : IView
    {
        DateTime Start_Date { get;set;}
        DateTime End_Date { get;set;}
        string Hierarchy_Code { get;set;}
        int Main_Id { get;set;}
        Boolean Is_Consol { get;set;}
        DataTable BindFBTLedgerWiseGrid { set;}
        string FBTCategoryName { set;}
        string FBTSectionName { set;}
        int FBTCategoryId { get;}
        int Ledger_Id { get;}
        string TotExpenditureAmount { set;}
        string TotAmountRecovered { set;}
        string TotNetExpenditure{set;}
        string TotPercentageAsPerSec{set;}
        string TotValueOfFringeBenefit{set;}
        string TotalTax{set;}
        string TotalFBT{set;}
        string TotEducationCess{set;}
        string TotAddlEducationCess { set;}



	}
}
