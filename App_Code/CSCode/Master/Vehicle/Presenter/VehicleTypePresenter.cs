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
    public class VehicleTypePresenter:Presenter 
    {
        private IVehicleTypeView objIVehicleTypeView;
        private VehicleTypeModel objVehicleTypeModel; 
        private DataSet objDS;

        public VehicleTypePresenter(IVehicleTypeView vehicleTypeView, bool isPostBack)
        {
            objIVehicleTypeView = vehicleTypeView;
            objVehicleTypeModel  = new VehicleTypeModel(objIVehicleTypeView);
            base.Init(objIVehicleTypeView, objVehicleTypeModel);

            if (!isPostBack)
            {
                initValues();
            }

        }

        private void initValues()
        {
            if (objIVehicleTypeView.keyID > 0)
            {
                objDS = objVehicleTypeModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIVehicleTypeView.VehicleTypeName = objDS.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                }
            }
        }

        public void Save()
        {

            base.DBSave();
            //objVehicleTypeModel.Save();
        }
    }
}