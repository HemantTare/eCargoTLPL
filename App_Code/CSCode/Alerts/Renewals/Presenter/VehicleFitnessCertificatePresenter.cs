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
/// Summary description for VehicleFitnessCertificatePresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VehicleFitnessCertificatePresenter : Presenter
    {
        private IVehicleFitnessCertificateView objIVehicleFitnessCertificateView;
        private VehicleFitnessCertificateModel objVehicleFitnessCertificateModel;
        private DataSet objDS;

        public VehicleFitnessCertificatePresenter(IVehicleFitnessCertificateView vehicleFitnessCertificateView, bool isPostBack)
        {
            objIVehicleFitnessCertificateView = vehicleFitnessCertificateView;
            objVehicleFitnessCertificateModel = new VehicleFitnessCertificateModel(objIVehicleFitnessCertificateView);
            base.Init(objIVehicleFitnessCertificateView, objVehicleFitnessCertificateModel);

            if (!isPostBack)
            {

                objIVehicleFitnessCertificateView.BindVehicleFitnessCertificateGrid = objVehicleFitnessCertificateModel.FillGrid();

            }

        }




        public void Save()
        {

            base.DBSave();

        }
    }
}

