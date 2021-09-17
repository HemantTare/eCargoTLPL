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
using Raj.EC.OperationView; 

/// <summary>
/// Summary description for LHPOAlertsBranchesView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface ILHPOAlertsBranchesView : IView
    {
        int BranchID { get;}

        DateTime LHPODate { get;set;}

        decimal TotalAdvance { get;set;}
        decimal TotalAdvanceGrid { get;set;}
        
        DataSet Bind_dg_AlertBranches { set;}
        DataSet Bind_dg_ATHDetails { set;}
        DataSet SessionAlertBranchesGrid { set;get;}
        DataSet SessionATHDetailsGrid { set;get;}


        string ATHDetailsXML { get;}
        string AlertBranchesXML { get;}  
        
    }
    
}