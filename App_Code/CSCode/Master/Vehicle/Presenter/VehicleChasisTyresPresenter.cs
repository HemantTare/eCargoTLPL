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
/// Summary description for VehicleChasisTyresPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{
    public class VehicleChasisTyresPresenter:Presenter 
    {
        private IVehicleChasisTyresView objIVehicleChasisTyresView;
        private VehicleChasisTyresModel objVehicleChasisTyresModel;
        private DataSet objDS;
        //private DataSet objDS1;

        public VehicleChasisTyresPresenter(IVehicleChasisTyresView vehicleChasisTyresView, bool isPostBack)
        {
            objIVehicleChasisTyresView = vehicleChasisTyresView;
            objVehicleChasisTyresModel = new VehicleChasisTyresModel(objIVehicleChasisTyresView);
            base.Init(objIVehicleChasisTyresView, objVehicleChasisTyresModel);

            if (!isPostBack)
            {
                fillValues();
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objVehicleChasisTyresModel.FillValues();
            objIVehicleChasisTyresView.BindFrontWheelSize = objDS.Tables[0];
            objIVehicleChasisTyresView.BindFrontTyreSize = objDS.Tables[2];
            objIVehicleChasisTyresView.BindRearWheelSize = objDS.Tables[1];
            objIVehicleChasisTyresView.BindRearTyreSize = objDS.Tables[3];
            objIVehicleChasisTyresView.SessionDualType = objDS.Tables[4];
        }

        private void initValues()
        {
            objDS = objVehicleChasisTyresModel.ReadValues();
            objIVehicleChasisTyresView.BindChasisTyresGrid = objDS.Tables[1];
            if (objIVehicleChasisTyresView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIVehicleChasisTyresView.FrontWheelSizeID = Util.String2Int(objDR["Front_Wheel_Size_ID"].ToString());
                    objIVehicleChasisTyresView.FrontTyreSizeID = Util.String2Int(objDR["Front_Tire_Size_ID"].ToString());
                    objIVehicleChasisTyresView.FrontPSI = Util.String2Int(objDR["Front_PSI"].ToString());
                    objIVehicleChasisTyresView.RearWheelSizeID  = Util.String2Int(objDR["Rear_Wheel_Size_ID"].ToString());
                    objIVehicleChasisTyresView.RearTyreSizeID = Util.String2Int(objDR["Rear_Tire_Size_ID"].ToString());
                    objIVehicleChasisTyresView.RearPSI  = Util.String2Int(objDR["Rear_PSI"].ToString());
                    objIVehicleChasisTyresView.NoOfStephaney = Util.String2Int(objDR["No_Of_Stepheany"].ToString());
                    objIVehicleChasisTyresView.OldNoOfStephaney = Util.String2Int(objDR["No_Of_Stepheany"].ToString());
                }
            }
        }
    }
}