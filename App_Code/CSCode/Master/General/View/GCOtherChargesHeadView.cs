using System;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Created : Ankit champaneriya
/// Date    : 18/12/08
/// Summary description for GCOtherChargesHeadView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IGCOtherChargesHeadView : ClassLibraryMVP.General.IView
    {
        string GC_Other_Charges_Head { get;set;}
    }
}