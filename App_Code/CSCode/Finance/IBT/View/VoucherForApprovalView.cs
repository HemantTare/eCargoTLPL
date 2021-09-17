using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Raj.EC.FinanceView
{
    public interface IVoucherForApprovalView : ClassLibraryMVP.General.IView
    {
        int Voucher_ID { get;}
        DataTable Bind_VoucherGrid { set;}
        int Branch_Id { get;}
        string Branch_Name { get;}
        decimal Total_Amount { get;}
        DataSet Set_LabelTextBox { set;}
    }
    
}
