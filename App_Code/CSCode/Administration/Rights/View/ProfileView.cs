using System;
using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.UI;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ProfileView
/// </summary>

//public class ProfileView
//{
//    public ProfileView()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}
namespace Raj.EC.AdminView
{
    public interface IProfileView : IView
    {
        string Profile_Name { get;set;}
        string Description { get;set;}
        Boolean IsCSA { get;set;}
        string Hierarchy_Name { get;set;}
        string Hierarchy_Code { get;set;}
        DataSet BindHierarchy { set;}
    }
}

