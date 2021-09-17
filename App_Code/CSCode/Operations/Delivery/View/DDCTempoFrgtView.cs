using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    public interface IDDCTempoFrgtView : IView
    { 
        
        int VendorID { get;set;}
        int VehicleID { get;set;}
        DateTime DDCTempoFrgtDate { get;set;}
        DateTime FromDate { get;set;}
        DateTime ToDate { get;set;}
        string Flag { get;} 
        //string VendorName { get;set;}
        
        string strValidate_Vehicle { get;set;}
        
        string Remarks { get;set;}
        string DDCTempoFrgtNo { set;}

        int Total_No_Of_Records { get;set;}
        int Total_No_Of_GC { get;set;}
        int Total_DDC_Articles { get;set;}
        decimal Total_GC_Amount { get;set;}
        decimal Tempo_Freight_ToBePaid { get;set;}
        decimal Bonus { get;set;}
        decimal AddTempoFrt { get;set;}
        
        DataTable SessionBindDDCTempoFrgtGrid { get;set;} 
      
        string DDCTempoFrgtDetailsXML { get;}
        string GetGCNoXML { get;set;}

        void ClearVariables();

        int IsCashCheque { get;set;}
        decimal CashAmount { get;set;}
        decimal ChequeAmount { get;set;}
        int ChequeNo { get;set;}
        DateTime ChequeDate { get;set;}

        int FrgtSettlementTypeID { get;set;}
        string FrgtSettlementTypeName { get;set;}
        decimal TotalTempoFrgtTBPaid { get;set;}

    }
}