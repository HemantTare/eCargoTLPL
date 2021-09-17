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
/// Summary description for VehicleTypePresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{

    public class VehicleModelPresenter:Presenter 
    {
        private IVehicleModelView objIVehicleModelView;
        private VehicleModelModel objVehicleModelModel;
        private DataSet objDS;

        public VehicleModelPresenter(IVehicleModelView vehicleModelView, bool isPostBack)
        {
            objIVehicleModelView = vehicleModelView;
            objVehicleModelModel = new VehicleModelModel(objIVehicleModelView);
            base.Init(objIVehicleModelView, objVehicleModelModel);

            if (!isPostBack)
            {
                initValues();
                objIVehicleModelView.Bind_ddl_Manufacturer = objVehicleModelModel.GetManufacturer();               
            }

        }

        private void initValues()
        {
            if (objIVehicleModelView.keyID > 0)
            {
                objDS = objVehicleModelModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIVehicleModelView.VehicleModelName = objDS.Tables[0].Rows[0]["Vehicle_Model"].ToString();
                    objIVehicleModelView.ManufacturerID =Convert.ToInt32(objDS.Tables[0].Rows[0]["Manufacturer_ID"]);
                    objIVehicleModelView.ThappiWeight  = Convert.ToInt32(objDS.Tables[0].Rows[0]["ThappiWeight"]);
                    

                }
            }
        }

    

        public void Save()
        {

            base.DBSave();
            //objVehicleModelModel.Save();
        }
 
    }
}
