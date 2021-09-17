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
/// Summary description for VehiclePermitPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehiclePermitPresenter : Presenter
    {
        private IVehiclePermitView objIVehiclePermitView;
        private VehiclePermitModel objVehiclePermitModel;
        private DataSet objDS;

        public VehiclePermitPresenter(IVehiclePermitView vehiclePermitView, bool isPostBack)
        {
            objIVehiclePermitView = vehiclePermitView;
            objVehiclePermitModel = new VehiclePermitModel(objIVehiclePermitView);
            base.Init(objIVehiclePermitView, objVehiclePermitModel);

            if (!isPostBack)
            {

                objIVehiclePermitView.BindVehiclePermitGrid = objVehiclePermitModel.FillGrid();

            }

        }




        public void Save()
        {

            base.DBSave();

        }
    }
}


