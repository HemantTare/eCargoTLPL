using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface IFrghtDisVoucherView : IView
    { 
       
        DateTime VoucherDate { get;set;}
        string Flag { get;}
        string Remarks { get;set;}
        string VoucherNo { set;} 

        int Total_No_Of_GC { get;set;}  
        
        decimal Total_Total_GC_Amount { get;set;}  
        decimal Total_DiscountAmt { get;set;} 
       
        DataTable SessionBindDDLUndelReason { set;}
        DataTable SessionBindFDVGrid { get;set;} 

        string FDVDetailsXML { get;}
        string GetGCNoXML { get;set;}

        void ClearVariables();
    }
}