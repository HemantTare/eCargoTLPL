using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
/// <summary>
/// Summary description for DocumentSeriesView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface IDocumentSeriesView : IView
    {
        int DocumentTypeID { get;set;}
        int VAID { get;set;}
        int StartNo { get;set;}
        int EndNo { get;set;}
        int Balance { get;}
        int MinStartNo { get;set;}
        int MaxEndNo { get;set;}
        int ParentStartNo { get;set;}
        int ParentEndNo { get;set;}
        int PrintedSeriesID { get;set;}
        DateTime DateofAllocation { get;set;}
        int BranchID { get;set;}
        int AreaID { get;set;}
        int RegionID { get;set;}
        bool Is_HO { get;set;}

        void SetBranchID(string Branch_Name, string BranchID);

        DataTable Bind_ddl_DocumentType { set;}
        DataTable Bind_dg_PrintedSeries { set;}       
        DataTable Bind_ddl_VA{ set;}       
    }
    
}