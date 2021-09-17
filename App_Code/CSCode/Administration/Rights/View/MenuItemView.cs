using System;
using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.AdminView;
//using Raj.EC.ControlsView;


/// <summary>
/// Summary description for MenuItemView
/// </summary>
namespace Raj.EC.AdminView
{
    public interface IMenuItemView : IView
    {
      
        int SerialNo { get;set;}
        string MenuItemName { get;set;}
        int MenuSystemId { get;set; }
        DataSet BindMenuSystem { set;}
        int MenuHeadId{ get;set; }
        DataSet BindMenuHead { set; }
        int MenuGroupId { get;set;}
        DataSet BindMenuGroup { set;}
        string MenuItemLink { get;set;}
        string Description { get;set;}
        string ViewUrl { get;set;}
        string LinkUrl { get;set;}
        string AddUrl { get;set;}
        string EditUrl { get;set;}
        string DeleteUrl { get;set;}
        string ReportUrl { get;set;}
        string QueryString { get;set;}
        string TableName { get;set;}
        string KeyColumnName { get;set;}
        Boolean Is_Active { get;set;}
        Boolean Is_PopUp_From_Link { get;set;}
        string MenuItemCode { get;set;}
        string MenuitemType { get;}
    }
}
