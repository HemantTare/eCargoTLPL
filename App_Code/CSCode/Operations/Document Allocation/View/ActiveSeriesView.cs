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
/// Summary description for ActiveSeriesView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface IActiveSeriesView : IView
    {
        int DocumentTypeID { get;set;}
        int VAID { get;set;}
        int DocumentSeriesID { get;set;}
        int MainID { get;set;}
        string HierarchyCode { get;set;}

        DataTable Bind_ddl_DocumentType { set;}
        DataTable Bind_dg_DocumentSeries { set;}
        DataTable Bind_ddl_VA { set;} 
    }

}