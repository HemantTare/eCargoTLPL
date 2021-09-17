using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.ControlsView;

using ClassLibraryMVP.General;


/// <summary>
/// Summary description for Driver Category View
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IDriverCategoryView : IView
    {
        string DriverCategoryName { set;get;}
             
       

    }
}