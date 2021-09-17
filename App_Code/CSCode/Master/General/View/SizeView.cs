using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.MasterView;
/// <summary>
/// Summary description for SizeView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface ISizeView : ClassLibraryMVP.General.IView
    {
        String SizeName { get;set;}
        decimal ApproxChargeWieght { get;set;}
        int Function { get;set;}
        decimal FactorAmount { get;set;}
        decimal MinChrgQty { get;set;}
        String Description { get;set;}
        bool IsDefault { get;set;}

        DataTable BindFunction { set;}
    }
}