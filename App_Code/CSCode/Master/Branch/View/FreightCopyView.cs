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
using Raj.EC.MasterView;


/// <summary>
/// Summary description for FreightCopyView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IFreightCopyView : IView
    {
        int FromCityID { get;set;}
        int CopyFromCityID { get;set;}        
        int StateID { get;set;}
        decimal Rate { get;set;}

        DataTable Bind_ddl_FromCityID { set;}
        DataTable Bind_ddl_CopyFromCityID { set;}
        DataTable Bind_ddl_State { set;}
     
    }
}