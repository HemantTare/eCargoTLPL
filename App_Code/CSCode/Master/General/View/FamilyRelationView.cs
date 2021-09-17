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
/// Summary description for FamilyRelationView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IFamilyRelationView : IView
    {
        string FamilyRelationName { get;set;}
        int Gender_ID { get;set;}
        DataSet BindGender { set;}
       
    }
}