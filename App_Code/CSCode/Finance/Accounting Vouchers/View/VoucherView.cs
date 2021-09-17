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
 
namespace Raj.EC.FinanceView
{
    public interface IVoucherView : IView         
    {
        string RefNo { set; get;}
        string VoucherNo { set; get;}

        string VoucherType { get;}
        string IBTVoucherFlag { get;}
        string Narration { set;get;}
        int VoucherTypeID { get;set;}
        int LedgerId { get;}
        string FBTPaymentType { set; get;}
        string VoucherTypeName { set;get; }

        DateTime VoucherDate { set; get;}

        decimal TotalDebit { set;get;}
        decimal TotalCredit { set;get;}

        string GetVoucherXML { get;}
        string GetVoucherCostCentreXML { get;}
        string GetVoucherBillByBillXML { get;}


        DataTable Bind_dg_Voucher { set;}
        DataTable SessionVoucherDT { set; get;}
        DataTable SessionVoucherBillByBillDT { set; get;}
        DataTable SessionVoucherCostCentreDT { set; get;}
        DataTable SessionDropDownCostCentre { set; get;}
        DataTable SessionDropDownRefType { set; get;}
        void ClearVariables();
}
}
