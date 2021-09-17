using System;
using System.Data;
using System.Web.UI.WebControls;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for GCRectificationView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IGCRectificationView : IView
    { 
        string GC_No_For_Print { get;set;}
        int Rectification_GC_Id { get;set;}
        int GC_No_Length { get;set;}
        bool Is_GCRectification_Octroi_Updated { get;set;}
        bool Is_GCRectification_Octroi_Applicable { get;set;}
    }
}
