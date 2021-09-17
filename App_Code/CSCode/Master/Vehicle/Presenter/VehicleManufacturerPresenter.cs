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
/// Summary description for VehicleManufacturerPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehicleManufacturerPresenter : Presenter
    {
        private IVehicleManufacturerView objIVehicleManufacturerView;
        private VehicleManufacturerModel objVehicleManufacturerModel;
        private DataSet objDS;

        public VehicleManufacturerPresenter(IVehicleManufacturerView vehicleManufacturerView, bool isPostBack)
        {
            objIVehicleManufacturerView = vehicleManufacturerView;
            objVehicleManufacturerModel = new VehicleManufacturerModel(objIVehicleManufacturerView);
            base.Init(objIVehicleManufacturerView, objVehicleManufacturerModel);

            if (!isPostBack)
            {
                initValues();
            }

        }

        private void initValues()
        {
            if (objIVehicleManufacturerView.keyID > 0)
            {
                objDS = objVehicleManufacturerModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIVehicleManufacturerView.VehicleManufacturerName = objDS.Tables[0].Rows[0]["Manufacturer"].ToString();
                }
            }
        }

        public void Save()
        {

            base.DBSave();
            //objVehicleManufacturerModel.Save();
        }
    }
}

