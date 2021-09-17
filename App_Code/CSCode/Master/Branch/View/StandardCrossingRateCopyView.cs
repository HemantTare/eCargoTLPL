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
/// Summary description for StandardCrossingRateCopyView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IStandardCrossingRateCopyView : IView
    {
        int FromBranchID { get;set;}
        int CopyFromBranchID { get;set;}
        int AreaID { get;set;}
        decimal HamaliRate { get;set;}
        decimal HireRate { get;set;}

        DataTable Bind_ddl_FromBranchID { set;}
        DataTable Bind_ddl_CopyFromBranchID { set;}
        DataTable Bind_ddl_Area { set;}

    }    
}