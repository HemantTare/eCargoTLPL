using System;
using System.Data;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 05-01-08
/// Summary description for DlyBranchUpdateView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface IStockTransferView : IView
    {
        DataTable BindDDLStockTransfer { set;}
        DataTable SessionDGStockTransfer { get; set;}
        string GCXML { get;set;}
        int BranchId { get;}
        int NewCurrentBranchId { get;}
        string Reason { get;}
        DateTime TransactionDate { get;}

        void ClearVariables(); // added Ankit
        
    }
}