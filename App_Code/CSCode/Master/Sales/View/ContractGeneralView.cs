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
using Raj.EC.MasterView;

/// <summary>
/// Summary description for ContractGeneralView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IContractGeneralView : IView
    {
        // Integer Property Declaration
       
        int BranchID { get;}
        int ClientNameID { get;}        
        int Days { get;set;}
        int BillingClientID { get;}
        int BillingBranchID { get;set;}
        string BillingHierarchy { get;set;}
        // Date Property Declaration
        DateTime ContractDate { get;set;}
        DateTime PODate { get;set;}
        DateTime ValidFromDate { get;set;}
        DateTime ValidUptoDate { get;set;}

        //String Property Declaration
        string ContractNo { get;set;}
        string ContractName { get;set;} //Added :Anita On: 05 Feb 09
        string ClientPONo { get;set;}
        string Remark { get;set;}

        //Decimal Property Declaration
        decimal POMaxLimit { get;set;}
        decimal Weight { get;set;}
        decimal Freight { get;set;}
        decimal Amount { get;set;}
        
        //void Property For DDLSearch Control(string text, string value);
        void SetBranchID(string Branch_Name, string BranchID);
        void SetClientID(string Client_Name, string ClientID);
        //void SetBillingBranchID(string Branch_Name, string BranchID);
        void SetBillingClientID(string Client_Name, string ClientID);
        int GCRiskId{set;get;}
        DataTable BindGCRiskType { set;}
        int ConsignmentTypeId { set;get;}
        DataTable BindConsignmentType { set;}


    }   
}