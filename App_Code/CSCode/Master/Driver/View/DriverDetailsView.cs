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
/// Summary description for DriverView
/// </summary>
/// 

namespace Raj.EF.MasterView
{
    public interface IDriverDetailsView : IView
    {

        //strings
        string DriverName { set;get;}
        string DriverNickName { set;get;}

        string DriverMobile1 { set;get;}
        string DriverMobile2 { set;get;}

        string AadharNo { get;set;}
        string HistoryRemarks { get;set;}

        string ReferenceName2 { set;get;}
        string ReferencePhone2 { set;get;}
        string ReferenceMobile2 { set;get;}

        DateTime ReferenceDate { set;get;}
        DateTime ReferenceDate2 { set;get;}

        string DriverLicenseNo { set;get;}
        string DriverCode { set;get;}
        string ReferenceName { set;get;}
        string ReferencePhone { set;get;}
        string ReferenceMobile { set;get;}
        string DriverImage { set;get;}
        string NativeAddress1 { set;get;}
        string NativeAddress2 { set;get;}
        string NativeContactNo { set;get;}
        string Qualification { set;get;}
        string BloodGroup { set;get;} //added by Ankit : 30-12-08 : 5.00 pm
        string LicenseAuthenticatedBy { set;get;}

        string LicenseIssueStateCode { get;set;}

        //integers
        int LicenseIssueCityID { set;get;}
        int LicenseIssueStateID { set;get;}


        int LicenseCategoryID { set;get;}
        int DriverCategoryID { set;get;}
        int ReligionID { set;get;}
        int Driver_Type_ID { set;get;}


        //decimals
        decimal OpeningBalance { set;get;}

        //booleans
        bool IsReliable { set;get;}
        bool IsMarried { set;get;}
        bool IsCleaner { set; get;}
        bool IsCompanyDriver { get;}
        bool IsLicenseAuthenticated { set;get;}

        //datetimes
        DateTime LicenseExpiryDate { set;get;}
        DateTime BirthDate { set;get;}

        //dropdowns
        DataTable BindDDLDriverCategory { set;}
        DataTable BindDDLReligion { set;}
        DataTable BindDDLLicenseIssueCity { set;}
        DataTable BindDDLLicenseIssueState { set;}

        DataTable BindDDLLicenseCategory { set;}
        DataTable BindDDLLAccountEffectType { set;}

        //common adress control
        IAddressView AddressView { get;}

        bool IseCargoUser { get;set;}
    }
}