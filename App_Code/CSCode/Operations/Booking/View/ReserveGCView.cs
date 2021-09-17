using System;
using System.Data;
using System.Web.UI.WebControls;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ReserveGCView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IReserveGCView : IView
    {
        int GC_No_Length { get;set;}

        int GCTypeId { get;}
        int VAId { get;}
        int ReservedReasonId { get;}
        string ConsignorId { get;}
        string ConsignorName { get;}

        
        int DocumentSeriesAllocationId { get;set;}
        int StartGCNo { get;set;}
        int EndGCNo { get;set;}
        //Added: Anita Date: 13/01/08
        //*****************************************
        int GCNoFrom { get;set;}
        int GCNoTo { get;set;}
        //*****************************************
        int NoOfGC { get;set;}
        DataTable BindGCType { set;}
        DataTable BindVA { set;}
        DataTable BindReason { set;}
    }
}
