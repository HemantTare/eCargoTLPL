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
/// Summary description for VehicleInformationView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface IVehicleInformationView : IView
    {
        string NumberPart1 { set;get;}
        string NumberPart2 { set;get;}
        string NumberPart3 { set;get;}
        string NumberPart4 { set;get;}

        string VehicleNo { get;}
        string OwnerName { set;get;}
        string Notes { set;get;}
        string GPSConnectivityId { set;get;}

        int BrokerId { get;}
        //int TdsId { set;get;}
        int DriverId1 { get;}
        int DriverId2 { get;}
        int CleanerId { get;}
        int VehicleTypeId { set;get;}
        int VehicleBodyId { set;get;}
        int CarrierCategoryId { set;get;}
        int ManufacturerId { set;get;}
        int VehicleModelId { set;get;}
        int VehicleCategoryId { set;get;}
        int YearOfManufacture { set;get;}
        int OpenOdometer { set;get;}
        int CurrentOdometer { set;get;}
        int OldCurrentOdometer { set;}

        string DriverMobile1 { set;get;}
        string DriverMobile2 { set;get;}
        //decimal TdsExemptionLimit { set;get;}
        //decimal TdsRatePercent { set;get;}

        //bool IsTdsApplicable { set;get;}
        bool TDSCertificateForOwner { set;get;}
        void SetDriver1Id(string text, string value);
        void SetDriver2Id(string text, string value);
        void SetCleanerId(string text, string value);
        void SetVendorId(string text, string value);
        DateTime OnRoadDate { get;set;}

        IAddressView AddressView { get;}

        //DataTable BindTds {set;}
        DataTable BindVehicleType{set;}
        DataTable BindCarrierCategory{set;}
        DataTable BindVehicleManufacturer{set;}
        DataTable BindVehicleBody { set;}

        DataSet BindVehicleModel { set;}
        ITDSAppView TDSAppView { get;}
    }
}
