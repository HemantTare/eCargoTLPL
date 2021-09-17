using System.Data;
using ClassLibraryMVP.General;
using Raj.EC;

/// <summary>
/// Author: Ankit champaneriya
/// Date  : 07-01-09
/// Summary description for GCConsigneeListView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IGCConsigneeListView : IView
    {
        DataTable SessionGConsigneeList { get; set;}
        string GetBranchXML { get;set;}
        int BranchId { get;}
        int NewClientId { get;set;}
        int isRegularClient { get;set;}

        ////string ClientId { get;}
        string ClientName { get;}
        string ContactPerson { set;}
        string Add1 { get; set;}
        string Add2 { get; set;}
        string pincode { get; set;}
        string stdcode { get; set;}
        string phone { get; set;}
        string mobile { get; set;}
        string csttinno { get; set;}
        string servicetaxNo { get; set;}
        string Reason { get;set;}

        int GC_ID { get;}

        void ClearVariables(); // added Ankit
        
    }
}