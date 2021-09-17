using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

/// <summary>
/// Summary description for VehicleVendorModel
/// </summary>
namespace Raj.EF.MasterModel
{
    class VehicleVendorModel : IModel
    {
        private IVehicleVendorView objIVehicleVendorView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public VehicleVendorModel(IVehicleVendorView vehicleVendorView)
        {
            objIVehicleVendorView = vehicleVendorView;
        }
        
        public DataSet FillValues()

        {          
            objDAL.RunProc("rstil43.EF_Mst_Vehicle_Vendor_FillValues", ref objDS);
            return objDS;
        }

        public DataSet FillVendorType()
        {
            objDAL.RunProc("rstil43.EF_Mst_Vehicle_Vendor_FillVendorType", ref objDS);
            return objDS;
        }

        
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vendor_Id", SqlDbType.Int, 0, objIVehicleVendorView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_Vehicle_Vendor_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Vendor_Name", SqlDbType.VarChar, 50,objIVehicleVendorView.VehicleVendorName), 
                                               objDAL.MakeInParams("@Vendor_Id", SqlDbType.Int, 0, objIVehicleVendorView.keyID),
                                               objDAL.MakeInParams("@Address_Line1", SqlDbType.VarChar,100,objIVehicleVendorView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@Address_Line2", SqlDbType.VarChar,100,objIVehicleVendorView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@City_Id", SqlDbType.Int,0,objIVehicleVendorView.AddressView.CityId),
                                               objDAL.MakeInParams("@Pin_Code", SqlDbType.NVarChar,15,objIVehicleVendorView.AddressView.PinCode),
                                               objDAL.MakeInParams("@Std_Code", SqlDbType.NVarChar,15,objIVehicleVendorView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone_1", SqlDbType.NVarChar,20,objIVehicleVendorView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone_2", SqlDbType.NVarChar,20,objIVehicleVendorView.AddressView.Phone2),
                                               objDAL.MakeInParams("@Mobile_No", SqlDbType.NVarChar,25,objIVehicleVendorView.AddressView.MobileNo),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIVehicleVendorView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@Email_Id", SqlDbType.VarChar,100,objIVehicleVendorView.AddressView.EmailId),
                                               objDAL.MakeInParams("@Reference_Name",SqlDbType.VarChar,100,objIVehicleVendorView.ReferenceName),
                                               objDAL.MakeInParams("@Reference_Phone", SqlDbType.VarChar,25,objIVehicleVendorView.ReferencePhone),
                                               objDAL.MakeInParams("@Reference_Mobile", SqlDbType.VarChar,25,objIVehicleVendorView.ReferenceMobile),
                                               objDAL.MakeInParams("@Is_Tds_Applicable", SqlDbType.Bit,1,objIVehicleVendorView.IsTdsApplicable),
                                               objDAL.MakeInParams("@Tds_Rate_Percent",SqlDbType.Decimal,0,objIVehicleVendorView.TdsRatePercent),
                                               objDAL.MakeInParams("@Tds_Exemption_Limit",SqlDbType.Decimal,0,objIVehicleVendorView.TdsExemptionLimit),
                                               objDAL.MakeInParams("@Pan_No",SqlDbType.VarChar,20,objIVehicleVendorView.PanNo),
                                               objDAL.MakeInParams("@Tds_Id",SqlDbType.Int,0,objIVehicleVendorView.TdsId), 
                                               objDAL.MakeInParams("@Vendor_Type_Id",SqlDbType.Int,0,objIVehicleVendorView.VendorTypeId),                              
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
                                         };


            objDAL.RunProc("rstil43.EF_Mst_Vehicle_Vendor_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}
