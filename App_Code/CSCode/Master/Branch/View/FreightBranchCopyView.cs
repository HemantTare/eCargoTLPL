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
/// Summary description for FreightBranchCopyView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IFreightBranchCopyView : IView
    {
        int FromBranchID { get;set;}
        int CopyFromBranchID { get;set;}
        int AreaID { get;set;}
        int CommodityID { get;set;}
        decimal FreightRate { get;set;}

        DataTable Bind_ddl_FromBranchID { set;}
        DataTable Bind_ddl_CopyFromBranchID { set;}
        DataTable Bind_ddl_Area { set;}
        DataTable Bind_ddl_Commodity { set;}
    }
    
}