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
/// Summary description for CityView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICityView : IView
    {
        string CityName { set;get;}
        int StateId { set;get;}
        string CountryName { set;}
        string RegionName { set;}
        DataSet BindState { set;}
        
        

    }
}
