using System;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Author  : Ankit champaneriya
/// Date    : 12/12/08
/// Summary description for VoucherView
/// </summary>
/// 
namespace Raj.EC.FinanceView
{
    public interface IVoucherDetailsView : ClassLibraryMVP.General.IView
    {
        //String crypt_Type { get;}
        //int crypt_Id { get;set;}
        DataSet Session_DetailsVoucher { get;set;}
    }
}