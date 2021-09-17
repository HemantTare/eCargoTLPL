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
/// Summary description for VehicleGridPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{

    public class VehicleGridPresenter : Presenter
    {
        private IVehicleGridView objIVehicleGridView;
        private VehicleGridModel objVehicleGridModel;
        private DataSet objDS;

        public VehicleGridPresenter(IVehicleGridView vehicleGridView, bool isPostBack)
        {
            objIVehicleGridView = vehicleGridView;
            objVehicleGridModel = new VehicleGridModel(objIVehicleGridView);
            base.Init(objIVehicleGridView, objVehicleGridModel);

            if (!isPostBack)
            {
                objIVehicleGridView.Bind_dg_Vehicle = objVehicleGridModel.FillGrid();
                initValues();
                //objIVehicleModelView.Bind_ddl_Manufacturer = objVehicleModelModel.GetManufacturer();
            }

        }

        private void initValues()
        {
            if (objIVehicleGridView.keyID > 0)
            {
                objDS = objVehicleGridModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                   // objIVehicleGridView.VehicleModelName = objDS.Tables[0].Rows[0]["Vehicle_Model"].ToString();
                   // objIVehicleGridView.ManufacturerID = Convert.ToInt32(objDS.Tables[0].Rows[0]["Manufacturer_ID"]);
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