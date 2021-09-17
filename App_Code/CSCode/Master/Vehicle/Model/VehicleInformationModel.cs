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
/// Summary description for VehicleInformationModel
/// </summary>
namespace Raj.EF.MasterModel
{
    class VehicleInformationModel : IModel
    {
        private IVehicleInformationView objIVehicleInformationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
 
        public VehicleInformationModel(IVehicleInformationView vehicleInformationView)
        {
            objIVehicleInformationView = vehicleInformationView;
        }
       
        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam = {objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIVehicleInformationView.keyID) 
                                              };
            objDAL.RunProc("rstil43.EF_Mst_Vehicle_Information_FillValues",ref objDS);
            _setTableName(new string[] { "FA_Master_TDS",
                                         "EF_Master_Vehicle_Type", 
                                         "EF_Master_Vehicle_Body", 
                                         "EF_Master_Carrier_Category", 
                                         "EF_Master_Vehicle_Manufacturer"}                                          
                                          );
            return objDS;
        }
        public DataSet FillVehicleModelOnManufactureChange()
        {
            SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Manufacturer_Id", SqlDbType.Int, 0, objIVehicleInformationView.ManufacturerId) 
                                              };
            objDAL.RunProc("rstil43.EF_Mst_Vehicle_Information_FillVehicleModelOnManufactureChange", objSqlParam, ref objDS);
            return objDS;
        }

        //public DataSet FillTDSDetailsOnRadioButtonChanged()
        //{
        //    SqlParameter[] objSqlParam = { objDAL.MakeInParams("@BrokerId", SqlDbType.Int, 0, objIVehicleInformationView.BrokerId) };
        //    objDAL.RunProc("rstil43.EF_Mst_Vehicle_Information_FillTDSDetailsOnRadioButtonChanged", objSqlParam, ref objDS);
        //    return objDS;
        //}

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIVehicleInformationView.keyID) 
                                              };
            objDAL.RunProc("rstil7.EF_Master_Vehicle_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            return objMessage;
        }

        private void _setTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }
    }
    }
