using System;
using System.Data;
using ClassLibraryMVP.General;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for PODCoverGenerationView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IPODCoverGenerationView : IView
    {
        string Flag { get;}
        string CoverSendHierarchyCode { get;set;}
        int CoverSentMainID { get;set;}      
        string Remarks { get;set;}
        string CoverNo { set;}
        int Total_GC { get;set;}
        DateTime CoverDate { get;set;}      
        DataTable SessionBindPODCoverGrid { set;}      
        string PODCoverDetailsXML { get;}
        string GetGCNoXML { get;set;}

        IPODSentByView PODSentByView { get; }

        void ClearVariables();

    }
}
