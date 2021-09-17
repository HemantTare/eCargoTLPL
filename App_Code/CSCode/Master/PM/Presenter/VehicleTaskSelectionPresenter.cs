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
/// Summary description for VehicleTaskSelectionPresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{

    public class VehicleTaskSelectionPresenter:Presenter 
    {
        private IVehicleTaskSelectionView objIVehicleTaskSelectionView;
        private VehicleTaskSelectionModel objVehicleTaskSelectionModel;
        private DataSet objDS;

        public VehicleTaskSelectionPresenter(IVehicleTaskSelectionView vehicleTaskSelectionView, bool isPostBack)
        {
            objIVehicleTaskSelectionView = vehicleTaskSelectionView;
            objVehicleTaskSelectionModel = new VehicleTaskSelectionModel(objIVehicleTaskSelectionView);
            base.Init(objIVehicleTaskSelectionView, objVehicleTaskSelectionModel);

            if (!isPostBack)
            {
                objIVehicleTaskSelectionView.Bind_dg_TaskSelection = objVehicleTaskSelectionModel.FillGrid();
                
            }

        }
        public void Save()
        {

            objVehicleTaskSelectionModel.Save();
        }
    }
}