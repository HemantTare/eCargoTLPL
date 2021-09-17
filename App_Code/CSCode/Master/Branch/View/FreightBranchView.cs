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
/// Summary description for FreightBranchView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IFreightBranchView : IView
    {
        int BranchID { get;set;}
        int ToBranchID { get;set;}        
        int AreaID { get;set;}
        decimal FreightRate { get;set;}
        int FreightID { get;set;}
        int CommodityID { get;set;}

        DataTable Bind_ddl_Branch { set;}
        DataTable Bind_ddl_Area { set;}
        DataTable Bind_ddl_Commodity { set;}

        DataTable Bind_dg_FreightBranch { set;}
        DataTable SessionFreightBranchGrid { set;get;}
    }    
}