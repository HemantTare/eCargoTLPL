using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EF.MasterView;
using Raj.EF.MasterModel;
using Raj.EC.ControlsModel;


/// <summary>
/// Summary description for VehicleInformationPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehicleInformationPresenter : Presenter
    {
        private IVehicleInformationView objIVehicleInformationView;
        private VehicleInformationModel objVehicleInformationModel;
      

        private DataSet objDS;

        public VehicleInformationPresenter(IVehicleInformationView vehicleInformationView, bool isPostBack)
        {
            objIVehicleInformationView = vehicleInformationView;
            objVehicleInformationModel = new VehicleInformationModel(objIVehicleInformationView);
            base.Init(objIVehicleInformationView, objVehicleInformationModel);

           

            if (!isPostBack)
            {
                FillValues();
                //FillTDSDetailsOnRadioButtonChanged();

                if (objIVehicleInformationView.keyID > 0)
                {
                    initValues();
                }
                else
                {
                    FillVehicleModelOnManufactureChange();
                }
            }

        }
        private void FillValues()
        {
            objDS = objVehicleInformationModel.FillValues();
            //objIVehicleInformationView.BindTds = objDS.Tables["FA_Master_TDS"];
            objIVehicleInformationView.BindVehicleType = objDS.Tables["EF_Master_Vehicle_Type"];
            objIVehicleInformationView.BindVehicleBody = objDS.Tables["EF_Master_Vehicle_Body"];
            objIVehicleInformationView.BindCarrierCategory = objDS.Tables["EF_Master_Carrier_Category"];
            objIVehicleInformationView.BindVehicleManufacturer = objDS.Tables["EF_Master_Vehicle_Manufacturer"];
        }
       
        public void FillVehicleModelOnManufactureChange()
        {
            objIVehicleInformationView.BindVehicleModel = objVehicleInformationModel.FillVehicleModelOnManufactureChange();
        }

        //public void FillTDSDetailsOnRadioButtonChanged()
        //{
        //    objDS = objTDSAppModel.ReadValues();

        //    //if (objDS.Tables[0].Rows.Count > 0)
        //    //{
        //    //    DataRow objDr = objDS.Tables[0].Rows[0];
        //    //    objIVehicleInformationView.IsTdsApplicable = Util.String2Bool(objDr["Is_TDS_Applicable"].ToString());
        //    //    objIVehicleInformationView.TdsId = Util.String2Int(objDr["TDS_Id"].ToString());
        //    //    objIVehicleInformationView.TdsExemptionLimit = Util.String2Decimal(objDr["TDS_Exemption_Limit"].ToString());
        //    //    objIVehicleInformationView.TdsRatePercent = Util.String2Decimal(objDr["TDS_Rate_Percent"].ToString());
        //    //}
        //    //else
        //    //{
        //    //    objIVehicleInformationView.IsTdsApplicable = false;
        //    //    objIVehicleInformationView.TdsId = 0;
        //    //    objIVehicleInformationView.TdsExemptionLimit = 0;
        //    //    objIVehicleInformationView.TdsRatePercent = 0;
        //    //}
        //}

             
        private void initValues()
        {
            objDS = objVehicleInformationModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDr = objDS.Tables[0].Rows[0];
                DataRow objDR1 = objDS.Tables[1].Rows[0];           

                objIVehicleInformationView.NumberPart1 = objDr["Number_Part1"].ToString();
                objIVehicleInformationView.NumberPart2 = objDr["Number_Part2"].ToString();
                objIVehicleInformationView.NumberPart3 = objDr["Number_Part3"].ToString();
                objIVehicleInformationView.NumberPart4 = objDr["Number_Part4"].ToString();
                objIVehicleInformationView.OwnerName = objDr["Market_Owener"].ToString();
                objIVehicleInformationView.Notes = objDr["General_Comments"].ToString();
                objIVehicleInformationView.GPSConnectivityId = objDr["GPS_Connectivity_ID"].ToString();
                objIVehicleInformationView.AddressView.AddressLine1 = objDr["Market_Address_1"].ToString();
                objIVehicleInformationView.AddressView.AddressLine2 = objDr["MarketAddress_2"].ToString();
                objIVehicleInformationView.AddressView.CityId = Util.String2Int(objDr["Market_City_ID"].ToString());
                objIVehicleInformationView.AddressView.PinCode = objDr["Market_Pin_Code"].ToString();
                objIVehicleInformationView.AddressView.StdCode = objDr["Market_Std_Code"].ToString();
                objIVehicleInformationView.AddressView.Phone1 = objDr["Market_Phone_1"].ToString();
                objIVehicleInformationView.AddressView.Phone2 = objDr["Market_Phone_2"].ToString();
                objIVehicleInformationView.AddressView.MobileNo = objDr["Market_Mobile_No"].ToString();
                objIVehicleInformationView.AddressView.FaxNo = objDr["Market_Fax"].ToString();
                objIVehicleInformationView.AddressView.EmailId = objDr["Market_Email_ID"].ToString();
                //objIVehicleInformationView.TdsId = Util.String2Int(objDr["TDS_Id"].ToString());

                objIVehicleInformationView.SetDriver1Id(objDR1["Driver1Name"].ToString(), objDR1["Driver1_ID"].ToString());
                objIVehicleInformationView.SetDriver2Id(objDR1["Driver2Name"].ToString(), objDR1["Driver2_ID"].ToString());

                objIVehicleInformationView.DriverMobile1 = objDr["DriverMobile1"].ToString();
                objIVehicleInformationView.DriverMobile2 = objDr["DriverMobile2"].ToString();

                objIVehicleInformationView.SetCleanerId(objDR1["CleanerName"].ToString(), objDR1["Cleaner_ID"].ToString());

                objIVehicleInformationView.SetVendorId(objDR1["VendorName"].ToString(), objDR1["Vendor_ID"].ToString());

                objIVehicleInformationView.VehicleTypeId = Util.String2Int(objDr["Vehicle_Type_ID"].ToString());
                objIVehicleInformationView.VehicleBodyId = Util.String2Int(objDr["Vehicle_Body_ID"].ToString());
                objIVehicleInformationView.CarrierCategoryId = Util.String2Int(objDr["Carrier_Category_ID"].ToString());
                objIVehicleInformationView.ManufacturerId = Util.String2Int(objDr["Manufacturer_ID"].ToString());
                FillVehicleModelOnManufactureChange();
                objIVehicleInformationView.VehicleModelId = Util.String2Int(objDr["Vehicle_Model_ID"].ToString());
                objIVehicleInformationView.YearOfManufacture = Util.String2Int(objDr["Year_Of_Manufacture"].ToString());
                objIVehicleInformationView.OpenOdometer = Util.String2Int(objDr["Opening_Odometer"].ToString());
                objIVehicleInformationView.CurrentOdometer = Util.String2Int(objDr["Current_Odometer"].ToString());
                objIVehicleInformationView.OldCurrentOdometer = Util.String2Int(objDr["Current_Odometer"].ToString());
                //objIVehicleInformationView.IsTdsApplicable = Util.String2Bool(objDr["Is_TDS_Applicable"].ToString());
                //objIVehicleInformationView.TdsExemptionLimit = Util.String2Decimal(objDr["TDS_Exemption_Limit"].ToString());
                //objIVehicleInformationView.TdsRatePercent = Util.String2Decimal(objDr["TDS_Rate_Percent"].ToString());
                if (Util.String2Bool(objDr["Is_TDS_To_Owner"].ToString()) == true)
                {
                    objIVehicleInformationView.TDSCertificateForOwner = true;
                }
                else
                {
                    objIVehicleInformationView.TDSCertificateForOwner = false;
                }
                objIVehicleInformationView.OnRoadDate = Convert.ToDateTime(objDr["On_Road_Date"].ToString());
            }
        }
       
        public void Save()
        {
            base.DBSave();
            //objVehicleInformationModel.Save();
        }
    }
}



