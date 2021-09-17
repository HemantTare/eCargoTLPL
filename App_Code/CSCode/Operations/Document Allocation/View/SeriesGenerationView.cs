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
/// Summary description for SeriesGenerationView
/// </summary>
/// 
namespace Raj.EC.OperationView
{

    public interface ISeriesGenerationView:IView 
    {
        
        int DocumentTypeID { get;set;}
        int StartNo { get;set;}
        int EndNo { get;set;}
        int Balance { get;}
        int MinStartNo { get;set;}
        int MaxEndNo { get;set;}
        DateTime GeneratedDate { get;set;}

        DataTable Bind_ddl_DocumentType { set;}
    }
}