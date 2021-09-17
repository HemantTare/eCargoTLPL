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
using Raj.EF.MasterModel;
using Raj.EF.MasterView;

namespace Raj.EF.MasterPresenter
{
    public class VehicleBodyPresenter : Presenter 
    {
        private IVehicleBodyModel objIVehicleBodyModel;
        private IVehicleBodyView objIVehicleBodyView;
        private DataSet objDs; 
        public VehicleBodyPresenter(IVehicleBodyView  VehicleBodyView, bool isPostBack)
        {

            objIVehicleBodyView = VehicleBodyView;
            objIVehicleBodyModel = new IVehicleBodyModel(objIVehicleBodyView);
            base.Init(objIVehicleBodyView, objIVehicleBodyModel);

            if (!isPostBack)
            {
                initValues();
            }

        }
        public void Save()
        {
            base.DBSave();
            //objIVehicleBodyModel.Save();
        }
        public void initValues()
        {
            if (objIVehicleBodyView.keyID > 0)
            {
                objDs = objIVehicleBodyModel.ReadValues();
                if (objDs.Tables[0].Rows.Count > 0)
                {
                    objIVehicleBodyView.VehicleBody = objDs.Tables[0].Rows[0]["Vehicle_Body"].ToString();
                }
            }
        }
    }
}
