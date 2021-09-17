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
/// Summary description for VehiclePermitTaxPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehiclePermitTaxPresenter : Presenter
    {
        private IVehiclePermitTaxView objIVehiclePermitTaxView;
        private VehiclePermitTaxModel objVehiclePermitTaxModel;
        private DataSet objDS;

        public VehiclePermitTaxPresenter(IVehiclePermitTaxView vehiclePermitTaxView, bool isPostBack)
        {
            objIVehiclePermitTaxView = vehiclePermitTaxView;
            objVehiclePermitTaxModel = new VehiclePermitTaxModel(objIVehiclePermitTaxView);
            base.Init(objIVehiclePermitTaxView, objVehiclePermitTaxModel);

            if (!isPostBack)
            {

                objIVehiclePermitTaxView.BindVehiclePermitTaxGrid = objVehiclePermitTaxModel.FillGrid();

            }

        }




        public void Save()
        {

            base.DBSave();

        }
    }
}


