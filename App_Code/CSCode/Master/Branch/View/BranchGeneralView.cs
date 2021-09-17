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
/// Summary description for BranchGeneralView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IBranchGeneralView : IView
    {
        string BranchCode { get;set;}
        string BranchName { get;set;}
        int Branch_ID { get;set;}
        string ContactPersonName { get;set;}
        string STRegistrationNo { get;set;}
        int BranchTypeID { get;set;}
        int AgencyAcountID { get;}
        int MemoDestinationID { get;}
        int DefaultHubID { get;}

        int DeliveryAtID { get;}
        int DeliveryTypeID { get;set;}
        int AreaID { get;}
        //int SetCityID { set;}
        int RegionId { get;}
        DateTime StartedOn { get;set;}

        void SetMemoDestinationID(string text, string value);
        void SetAgencyAcountID(string text, string value);
        void SetAreaID(string text, string value);
        void SetDefaultHubID(string text, string value);
        void SetDeliveryAtID(string text, string value);
        void SetRegionId(string text, string value);

        DataTable BindBranchType { set;}
        DataTable BindDeliveryType { set;}
        DataTable BindDivision { set;}
        IAddressView AddressView { get;}
        EventHandler OnCityChanged { set;}
        string BranchDivisionXML { get;}
    }
}
