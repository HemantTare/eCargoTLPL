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
using Raj.EC.AdminView;
/// <summary>
/// Summary description for MacIDView
/// </summary>
namespace Raj.EC.AdminView
{
    public interface IMacIDView:IView 
    {
        string Hierarchy_Code {get;set;}
        int Main_ID {get;set;}

        DataTable Bind_Mac_Id{ get;set;}
        string Mac_ID_XML { get;}
    }
}