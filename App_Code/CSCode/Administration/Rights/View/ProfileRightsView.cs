using System;
using System.Data;

using ClassLibraryMVP.General;

/// <summary>
/// Modified : Ankit champaneriya
/// Date     : 05/12/08
/// Summary description for ProfileRightsView
/// </summary>
/// 

namespace Raj.EC.AdminView
{
    public interface IProfileRightsView : IView
    {
        string HierarchyID { get;set;}
        int ProfileID { get;set;}
        int MenuSystemID { get;set;}
        int MenuHeadID { get;set;}
        int MenuGroupID { get;set;}
        bool IsApplicableToAll { get;set;}

        DataSet ProfileRightsDs { get;}
        DataTable Bind_ddl_Hierarchy { set;}
        DataTable Bind_ddl_Profile { set;}
        DataTable Bind_ddl_MenuSystem { set;}
        DataTable Bind_ddl_MenuHead { set;}
        DataTable Bind_ddl_MenuGroup { set;}
        DataTable Bind_dg_ProfileRights { set;}
        
    }  
}