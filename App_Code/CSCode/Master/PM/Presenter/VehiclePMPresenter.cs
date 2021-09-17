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
/// Summary description for VehiclePMPresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{

    public class VehiclePMPresenter:Presenter 
    {
        private IVehiclePMView objIVehiclePMView;
        private VehiclePMModel objVehiclePMModel;
        private DataSet objDS;

        public VehiclePMPresenter(IVehiclePMView vehiclePMView, bool isPostBack)
        {
            objIVehiclePMView = vehiclePMView;
            objVehiclePMModel = new VehiclePMModel(objIVehiclePMView);
            base.Init(objIVehiclePMView, objVehiclePMModel);

             if (!isPostBack)
            {
                objIVehiclePMView.Bind_dg_Grid = objVehiclePMModel.FillGrid();
              
            }

        }

       


        public void Save()
        {

            base.DBSave();
            //objVehicleModelModel.Save();
        }
    }
}