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
/// Summary description for LedgerBillWiseDetailView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ILedgerBillWiseDetailView : IView
    {
       
        DataTable BindGrid { set;}
        DateTime EndDate { set;get;}
        string Hierarchy_Code { set;get;}
        int Main_Id { set;get;}
        Boolean Is_Consol { set;get;}
        int LedgerGroupId { get;}
        int LedgerId { get;}
        string LedgerName { get;set;}
        DataSet SessionLedgerBillWiseGrid{get;set;}      
        Boolean IsCondensed { set;get;}


    }
}
