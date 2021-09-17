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
/// Summary description for FreightView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IFreightView : IView
    {
        int CityID { get;set;}
        int ToCityID { get;set;}
        decimal DistanceInKM { get;set;}         
        int StateID { get;set;}
        decimal FTLRate { get;set;}
        decimal NormalRate { get;set;}
        decimal SpecialRate { get;set;}
        int FreightID { get;set;}

        DataTable Bind_ddl_City { set;}
        DataTable Bind_ddl_State { set;}
        DataTable Bind_dg_Freight { set;}
        DataTable SessionFreightGrid { set;get;}
    }    
}