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
/// Summary description for DateSettingsView
/// </summary>
namespace Raj.EC.AdminView
{
    public interface IDateSettingsView : IView
    {

      
        string ProcessName { get;set;}
        string Code { get;set;}
        int MinHrs { get;set;}
        int MaxHrs { get;set;}
	}
}
