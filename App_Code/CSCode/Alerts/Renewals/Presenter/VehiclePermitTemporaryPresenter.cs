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
/// Summary description for VehiclePermitTemporaryPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehiclePermitTemporaryPresenter : Presenter
    {
        private IVehiclePermitTemporaryView objIVehiclePermitTemporaryView;
        private VehiclePermitTemporaryModel objVehiclePermitTemporaryModel;
        private DataSet objDS;

        public VehiclePermitTemporaryPresenter(IVehiclePermitTemporaryView vehiclePermitTemporaryView, bool isPostBack)
        {
            objIVehiclePermitTemporaryView = vehiclePermitTemporaryView;
            objVehiclePermitTemporaryModel = new VehiclePermitTemporaryModel(objIVehiclePermitTemporaryView);
            base.Init(objIVehiclePermitTemporaryView, objVehiclePermitTemporaryModel);

            if (!isPostBack)
            {

                objIVehiclePermitTemporaryView.BindVehiclePermitTemporaryGrid = objVehiclePermitTemporaryModel.FillGrid();

            }

        }




        public void Save()
        {

            base.DBSave();

        }
    }
}

