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

/// <summary>
/// Summary description for CompanyView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICompanyView : IView
    {
        ICompanyGeneralDetailsView CompanyGeneralDetailsView { get;}
        ICompanyTDSFBTDetailsView CompanyTDSFBTDetailsView { get;}
        ICompanyParametersView CompanyParametersView { get;}
        IBookingParametersView CompanyBookingParametersView { get;}
        ICompanyDeliveryView CompanyDeliveryView { get;}
        ICompanyTripHireParametersView CompanyTripHireParametersView { get;}
        ILocalCollectionVoucherView LocalCollectionVoucherView { get;}
        ICompanyCaptionView CompanyCaptionView { get;}
        DataTable SessionBookingParametersGrid { set;get;}        
        DataTable SessionDivision { set;get;}       
        DataTable SessionBookingType { set;get;} 
        DataTable SessionPaymentType { set;get;} 
        DataTable SessionTripHireParametersGrid { set;get;}
        DataTable SessionATHGrid { set;get;}
        DataTable SessionCompanyDeliveryGrid { set;get;}
        DataTable SessionLocalCollectionVoucherGrid { set;get;}
        DataTable SessionDoorDeliveryExpenseVoucherGrid { set;get;}
        DataTable SessionLHPONatureOfPayment { set;get;}

        void ClearVariables();
    }
}
