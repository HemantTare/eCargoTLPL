using System;
using System.Data;
using System.Web.UI.WebControls;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ReBookGCView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IReBookGCView : IView
    {        
        int ReBook_GC_Id { get;set;}
        int GC_No_Length { get;set;}
        string GC_No_For_Print { get;set;}
        bool Is_ReBookGC_Octroi_Updated { get;set;}
        bool Is_ReBookGC_Octroi_Applicable { get;set;}
    }
}
