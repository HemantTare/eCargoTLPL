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
using Raj.EC.ControlsView;


/// <summary>
/// Summary description for FilterationView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface IFilterationView : IView
    {
        DataSet BindLedgerGroupName { set; }
        int MenuItemId { get;}
        DataSet BindVoucherType { set;}
        int MenuItemCode { get;}

	}
}
