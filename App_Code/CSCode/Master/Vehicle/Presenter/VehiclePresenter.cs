using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClassLibraryMVP.General;

using Raj.EF.MasterView;
using Raj.EF.MasterModel;
/// <summary>
/// Summary description for VehiclePresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{
    public class VehiclePresenter : Presenter 
    {
        private IVehicleView objIVehicleView;
        private VehicleModel objVehicleModel;

        public VehiclePresenter(IVehicleView VehicleView, bool isPostback)
        {
            objIVehicleView = VehicleView;
            objVehicleModel = new VehicleModel(objIVehicleView);
            base.Init(objIVehicleView, objVehicleModel);
        }

        public void Save()
        {
            base.DBSave();
            //objVehicleModel.Save();
        }
    }
}