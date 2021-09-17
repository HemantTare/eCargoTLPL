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
/// Summary description for LocalCollectionVoucherView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface ILocalCollectionVoucherView:IView 
    {
        DataTable Bind_dg_LocalCollectionVoucher { set;}
        DataTable Bind_dg_DoorDeliveryExpenseVoucher { set;}
        DataTable SessionLocalCollectionVoucherGrid { set;get;}
        DataTable SessionDoorDeliveryExpenseVoucherGrid { set;get;}

        int LocalDivisionId { set;get;}
        int DoorDivisionId { set;get;}

        DataTable BindLocalDivision { set;}
        DataTable BindDoorDivision { set;}
        DataTable SessionBookingType { set;get;}
        DataTable SessionDivision { set;get;}


        string LocalCollectionVoucherXML { get;}
        string DoorDeliveryExpenseVoucherXML { get;}     
    }
}