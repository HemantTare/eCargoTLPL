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
using Raj.EC.MasterView;
/// <summary>
/// Summary description for ContractTermsView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IContractTermsView : IView
    {
        int TermsID { set;get;}
        string ContractTermsXML { get;}
        string TermsDescription{ set;get;}
        DataSet Bind_dg_ContractTerms { set;}
        DataSet SessionContractTermsGrid { set;get;}
        DataTable Bind_ddl_TermsHead{ set;}
        DataTable SessionTermsHead { set;get;}
    }    
}