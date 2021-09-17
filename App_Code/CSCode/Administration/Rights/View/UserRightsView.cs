using System;
using System.Data;

using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ProfileRightsView
/// </summary>
/// 

namespace Raj.EC.AdminView
{
    public interface IUserRightsView : IView
    {
        string HierarchyID { get;set;}
        int ProfileID { get;set;}
        int MenuSystemID { get;set;}
        int MenuHeadID { get;set;}
        int MenuGroupID { get;set;}
        int UserID { get;set;}
        DataSet MenuItemsRightsDs { get;}
        DataSet TransacationRightsDs { get;}
        DataTable Bind_ddl_Hierarchy { set;}
        DataTable Bind_ddl_Profile { set;}
        DataTable Bind_ddl_User { set;}
        DataTable Bind_ddl_MenuSystem { set;}
        DataTable Bind_ddl_MenuHead { set;}
        DataTable Bind_ddl_MenuGroup { set;}
        DataTable Bind_dg_MenuItemsRights { set;}
        DataTable Bind_dg_TransacationRights { set;}

    }
}