using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for TDSDeductionView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ITDSDeductionView : ClassLibraryMVP.General.IView
    {
        int Ledger_Account_Id { get;set;}
        int TDS_Ledger_Id { get;set;}


        int Ledger_Group_Id { get;set;}
        //int Voucher_Type_Id { get;set;}
        int Vendor_Id { get;set;}


        String TDS_Ledger_Id_View {  set;}

        String Ledger_Group_Id_View { set;}
        String Ledger_Account_Id_View { set;}

        String Total_Amount_Paid_Amount { get;set;}
        String Tax_Percent { get;set;}
        String Tax_Amount { get;set;}
        String Bill_No { get;set;}
        String Journal_No { get;set;}
        String Surcharge_Percent { get;set;}
        String Surcharge_Amount { get;set;}
        String Addl_Ed_Cess_Percent { get;set;}
        String Addl_Ed_Cess_Amount { get;set;}
        String Addtional_Surcharge_Percent { get;set;}
        String Addtional_Surcharge_Amount { get;set;}
        String Total_TDS_Amount { get;set;}
        String Less_TDS_Deducted_Till_Date_Amount { get;set;}
        String Net_TDS_To_Deduct_Amount { get;set;}
        String Credit_Days { get;set;}
        String Gross_Amount { get;set;}
        String Amount { get;set;}      
        
       // String TDSDeductionNo { get;set;}

        String Reference_No { get;set;}
        String Narration { get;set;}       

        DataTable BindLedgerGroup { set;}
        DataTable BindTDSLedger { set;}
        DataTable BindLedgerAccount { set;}
        DataTable BindName { set;}     

        DateTime TDSDeduction_Date { get;set;}
       
    }
}