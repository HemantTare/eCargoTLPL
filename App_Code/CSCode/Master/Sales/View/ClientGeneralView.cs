using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ClientGeneralView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IClientGeneralView:IView
    {
        string ClientCode { get;set;}
        string ClientName { get;set;}
        string ContactPersonName { get;set;}
        int BranchId { get;}
        int DeliveryAreaId { get;set;}
        int ClientGroupID { get;set;}
        int ClientCategoryID { get;set;}
        int DeliveryTypeID { get;set;}

        bool Is_Casual_Taxable { set;get;}
        string CSTTINNo { get;set;}
        string ServiceTaxNo { get;set;}
        bool IsServiceTaxPay { get;set;}
        void SetBranchId(string text, string value);
        void SetClientId(string text, string value); 

        //DataTable BindBranch { set;}
        DataTable BindClientGroup { set;}
        DataTable BindClientCategory { set;}
        DataTable BindDeliveryArea { set;}
        DataTable BindDeliveryType { set;}       

        IAddressView AddressView { get;}

        int Regular_Client_Id { get;}

        string CreatedBy { set;get;}
        string UpdatedBy { set;get;}
        string Remarks { set;get;}

        int Landmark1ID { get;set;}
        int Landmark2ID { get;set;}

        bool Is_OutwardBilling { set;get;}
        bool Is_InwardBilling { set;get;}

    }
}
