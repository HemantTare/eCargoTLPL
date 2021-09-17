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


/// <summary>
/// Summary description for VehicleVendorPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehicleVendorPresenter : Presenter
    {
        private IVehicleVendorView objIVehicleVendorView;
        private VehicleVendorModel objVehicleVendorModel;
        private DataSet objDS;

        public VehicleVendorPresenter(IVehicleVendorView vehicleVendorView, bool isPostBack)
        {
            objIVehicleVendorView = vehicleVendorView;
            objVehicleVendorModel = new VehicleVendorModel(objIVehicleVendorView);
            base.Init(objIVehicleVendorView, objVehicleVendorModel);

            if (!isPostBack)
            {
                FillTds();
                FillVendorType();
                initValues();
            }

        }
        public void FillTds()
        {

            objIVehicleVendorView.BindTds = objVehicleVendorModel.FillValues();

        }
        public void FillVendorType()
        {
            objIVehicleVendorView.BindVendorType = objVehicleVendorModel.FillVendorType();
        }


        private void initValues()
        {
            if (objIVehicleVendorView.keyID > 0)
            {
                objDS = objVehicleVendorModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIVehicleVendorView.VehicleVendorName = objDS.Tables[0].Rows[0]["Vendor_Name"].ToString();
                    objIVehicleVendorView.AddressView.AddressLine1 = objDS.Tables[0].Rows[0]["Address_1"].ToString();
                    objIVehicleVendorView.AddressView.AddressLine2 = objDS.Tables[0].Rows[0]["Address_2"].ToString();
                    objIVehicleVendorView.AddressView.CityId = Util.String2Int(objDS.Tables[0].Rows[0]["City_ID"].ToString());
                    objIVehicleVendorView.AddressView.PinCode = objDS.Tables[0].Rows[0]["Pin_Code"].ToString();
                    objIVehicleVendorView.AddressView.StdCode = objDS.Tables[0].Rows[0]["Std_Code"].ToString();
                    objIVehicleVendorView.AddressView.Phone1 = objDS.Tables[0].Rows[0]["Phone_1"].ToString();
                    objIVehicleVendorView.AddressView.Phone2 = objDS.Tables[0].Rows[0]["Phone_2"].ToString();
                    objIVehicleVendorView.AddressView.MobileNo = objDS.Tables[0].Rows[0]["Mobile_No"].ToString();
                    objIVehicleVendorView.AddressView.FaxNo = objDS.Tables[0].Rows[0]["Fax"].ToString();
                    objIVehicleVendorView.AddressView.EmailId = objDS.Tables[0].Rows[0]["Email_ID"].ToString();
                    objIVehicleVendorView.ReferenceName = objDS.Tables[0].Rows[0]["Reference_Name"].ToString();
                    objIVehicleVendorView.ReferencePhone = objDS.Tables[0].Rows[0]["Reference_phone"].ToString();
                    objIVehicleVendorView.ReferenceMobile = objDS.Tables[0].Rows[0]["Reference_Mobile"].ToString();
                    objIVehicleVendorView.IsTdsApplicable = Util.String2Bool(objDS.Tables[0].Rows[0]["Is_TDS_Applicable"].ToString());
                    objIVehicleVendorView.TdsId=Util.String2Int(objDS.Tables[0].Rows[0]["TDS_Id"].ToString());
                    objIVehicleVendorView.VendorTypeId=Util.String2Int(objDS.Tables[0].Rows[0]["Vendor_Type_ID"].ToString());
                    objIVehicleVendorView.TdsExemptionLimit=objDS.Tables[0].Rows[0]["TDs_Exemption_Limit"].ToString();
                    objIVehicleVendorView.PanNo = objDS.Tables[0].Rows[0]["Pan_No"].ToString();
                }
            }
        }

        public void Save()
        {

            base.DBSave();
            //objVehicleVendorModel.Save();
        }
    }
}


