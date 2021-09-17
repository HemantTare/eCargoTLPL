using System;
using System.Data;
using System.Web.UI.WebControls;


/// <summary>
/// Author  : Ankit champaneriya
/// Date    : 12/12/08
/// 
/// Summary description for UnAppVoucherCancellation View
/// </summary>
/// 
namespace Raj.EC.FinanceView
{
    public interface IUnAppVoucherCancellationView : ClassLibraryMVP.General.IView
    {
        int crypt_id { get;set;}
        String crypt_Type { get;}
    }
}