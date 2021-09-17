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
using Raj.EC.ControlsView; 

/// <summary>
/// Summary description for PODCoverRecieptView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface IPODCoverRecieptView : IView
    {
        int CoverNo { get;set;}
        int MainId { get;set;}
        string Flag { get;}
        string HierachyCode { get;set;}
        string ReceiptNo { get;set;}
        string SentHierachy { set;}
        string SentLocation { set;}
        string Remark { get;set;}
        string SetCoverDate { set;}
        IPODSentByView PODSentByView { get;}

        string CoverReceivedDetailsXML { get;}      

        DateTime ReceiptDate { get;set;}
        DateTime CoverDate { get;}
        
        //Bind Property Declaration
        DataTable Bind_ddl_CoverNo { set;}
        DataTable SessionPODCoverReciept { set;get;}

        void ClearVariables();  //added Ankit
    }

}