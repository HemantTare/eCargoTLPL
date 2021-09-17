using System;
using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.AdminView;

/// <summary>
/// Summary description for MenuGroupView
/// </summary>
namespace Raj.EC.AdminView
{
    public interface IMenuGroupView : IView
    {
        string MenuGroupName { get;set;}
        int SerialNo { get;set;}
        bool Chk_IsTranMenuGroup { get;set;}
        int MenuTypeId { get;set;}
        string Description { get;set;}
        int MenuSystemId { get;set;}
        int MenuHeadId { get;set;}

        DataTable BindSystemName { set;}
        DataSet BindMenuHead { set;}
        DataTable BindMenuType { set;}
    }
}
