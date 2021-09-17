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
/// Summary description for StateView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IStateView : IView
    {
        string StateName { set;get;}
        string NsdlCode { set;get;}
        string StateCode { set;get;}
        DataSet BindRegion { set;}       
        int RegionId { set;get;}      
        string CountryName { set;}
        string SessionStateFormDetails { get;}
        int ChkFormType { set;get;}
        DataSet BindChkListFormType { set;}
        DataSet ChkListSessionForm { get;set;}
       

    }
}
