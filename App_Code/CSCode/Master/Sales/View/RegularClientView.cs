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
using Raj.EC.ControlsView;



/// <summary>
/// Summary description for RegularClientView
/// </summary>
namespace Raj.EC.SalesView
{
    public interface IRegularClientView : IView
    {
        int ClientId { set;get;}
        int ContractualClientId { set;get;}
        int DeliveryAreaId { get;set;}
        int ClientGroupID { get;set;}
        int ClientCategoryID { get;set;}
        int DeliveryTypeID { get;set;}

        string RegularClientName { set;get;}
        string ContactPerson { set;get;}
        bool IsServiceTaxPayable { set;get;}
        bool Is_ToPay_Allowed { set;get;}
        bool Is_Casual_Taxable { set;get;}
        string CSTNo { set;get;}
        string ServiceTaxNo { set;get;}
        string CreatedBy { set;get;}
        string UpdatedBy { set;get;}
        IAddressView AddressView { get;}
        DataTable BindClientGroup { set;}
        DataTable BindClientCategory { set;}
        DataTable BindDeliveryType { set;}
        string Remarks { set;get;}

        int Landmark1ID { get;set;}
        int Landmark2ID { get;set;}

        string GSTName { set;get;}

        bool IsWithCompleteDetails { set;get;}
	}
}
