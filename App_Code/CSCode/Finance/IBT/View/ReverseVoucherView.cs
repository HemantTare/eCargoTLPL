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
    public interface IReverseVoucherView : ClassLibraryMVP.General.IView
    {
        int Ledger_Id { get;set;}
        DataTable SessionVoucherBillByBillDT{get;set;}
        DataTable SessionVoucherCostCentreDT{get;set;}
        DataTable SessionDropDownCostCentre{get;set;}
        DataTable SessionDropDownRefType { get;set;}
        string Reason {get;set;}
        int Menu_Item_Id {get; }
        int VoucherTypeID {get;}
        int Voucher_Id {get;}
        int BranchLedgerId {get;}
        string CostCentreXML {get;}
        string BillByBillXML {get;}
         
    }
}