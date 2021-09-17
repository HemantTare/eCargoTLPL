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
/// Summary description for TDSComputationView
/// </summary>
namespace Raj.FA.ReportsView
{
    public interface ITDSComputationView : IView
    {
        DataSet BindTDSGrid { set;}
        DateTime Start_Date { get;set;}
        DateTime End_Date { get;set;}
        string Hierarchy_Code { get;set;}
        int Main_Id { get;set;}
        Boolean Is_Consol { get;set;}
        int Ledger_Id { get;}
	}
}
