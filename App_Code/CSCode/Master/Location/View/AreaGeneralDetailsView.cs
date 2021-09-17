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
/// Summary description for AreaGeneralDetailsView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IAreaGeneralDetailsView : IView
    {
        string AreaCode { set;get;}
        string AreaName { set;get;}
        string ContactPerson { set;get;}
        IAddressView AddressView { get;}
        int ChkListDivision { set;get;}
        string SessionChkListDivisionDetails { get;}
        DateTime StartedOn { set;get;}
        DataSet SessionChkListDivision { set;get;}
        DataSet BindChkListDivision { set;}
        EventHandler OnCityChanged { set;}
        int AreaID { get;set;}
        //bool VisibleChkList { set;get;}
    }
}
