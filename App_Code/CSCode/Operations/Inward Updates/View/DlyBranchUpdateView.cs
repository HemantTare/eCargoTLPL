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
    public interface   IDlyBranchUpdateView : IView 
    {
        DataTable BindDDLDeliveryBranch { set;}
        DataTable BindDDLServiceLocation { set;}  //added : 20-02-09 5.30 pm  by Ankit
        //DataTable BindDGDeliveryBranch { set;}
        DataTable SessionDGDeliveryBranch { get; set;}
        string GetBranchXML { get;set;}
        int BranchId { get;}
        int NewDlyBranchId { get;}
        int ServiceLocationId { get;}
        string Reason { get;}
        DateTime TransactionDate { get;}

        void ClearVariables();
    }
}