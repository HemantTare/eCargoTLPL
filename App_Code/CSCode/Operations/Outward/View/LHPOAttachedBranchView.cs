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
/// Summary description for LHPOAttachedBranchView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface ILHPOAttachedBranchView:IView 
    {
        int BranchID { get;}
        DataSet Bind_dg_AttachedLHPOBranches { set;}
        DataSet SessionAttachedLHPOBranchesGrid { set;get;}
        string AttachedLHPOBranchesXML { get;}  
    }
}