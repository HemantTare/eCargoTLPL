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
/// Summary description for VehicleHireDetailsPresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{

    public class VehicleHireDetailsPresenter:Presenter 
    {
        private IVehicleHireDetailsView objIVehicleHireDetailsView;
        private VehicleHireDetailsModel objVehicleHireDetailsModel;
        private DataSet objDS;

        public VehicleHireDetailsPresenter(IVehicleHireDetailsView vehicleHireDetailsView, bool isPostBack)
        {
            objIVehicleHireDetailsView = vehicleHireDetailsView;
            objVehicleHireDetailsModel = new VehicleHireDetailsModel(objIVehicleHireDetailsView);
            base.Init(objIVehicleHireDetailsView, objVehicleHireDetailsModel);

            if (!isPostBack)
            {
                FillDDLHireType();

                if (objIVehicleHireDetailsView.keyID > 0)
                {
                    initValues();
                }
                else
                {
                    objIVehicleHireDetailsView.MaintainedByID = 1;
                    FillDDLMaintainBy();
                }
            }
        }

        private void FillDDLHireType()
        {
            objIVehicleHireDetailsView.Bind_ddl_HireType = objVehicleHireDetailsModel.FillDDLHireType().Tables[0];
        }

        public void FillDDLMaintainBy()
        {
            objIVehicleHireDetailsView.Bind_ddl_MaintainedBy = objVehicleHireDetailsModel.FillDDLMaintainedBy().Tables[0];
        }

        private void initValues()
        {
            objDS = objVehicleHireDetailsModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                int RegionID;
                int AreaID;
                int BranchID;
                int MaintainedID = 0;

                DataRow objDR = objDS.Tables[0].Rows[0];

                objIVehicleHireDetailsView.HireTypeID = Util.String2Int((objDR["Truck_Hire_Type_Id"].ToString()));

                RegionID = Util.String2Int((objDR["Region_ID"].ToString()));
                AreaID = Util.String2Int((objDR["Area_ID"].ToString()));
                BranchID = Util.String2Int((objDR["Branch_ID"].ToString()));

                if (RegionID > 0) 
                { 
                    objIVehicleHireDetailsView.MaintainedByID = 1;
                    objIVehicleHireDetailsView.MaintainedBy_Caption = "Region Name : ";
                    MaintainedID = RegionID;
                }
                if (AreaID > 0) 
                { 
                    objIVehicleHireDetailsView.MaintainedByID = 2;
                    objIVehicleHireDetailsView.MaintainedBy_Caption = "Area Name : ";
                    MaintainedID = AreaID;
                }
                if (BranchID > 0) 
                { 
                    objIVehicleHireDetailsView.MaintainedByID = 3;
                    objIVehicleHireDetailsView.MaintainedBy_Caption = "Branch Name : ";
                    MaintainedID = BranchID;
                }

                //objIVehicleHireDetailsView.MaintainedByID = Util.String2Int((objDR["MaintainedBy_ID"].ToString()));
                FillDDLMaintainBy();
                objIVehicleHireDetailsView.MaintainedID = MaintainedID;

                objIVehicleHireDetailsView.HireAmount = Util.String2Decimal(objDR["Truck_Hire_Amount"].ToString());

                objIVehicleHireDetailsView.MultipleTripADay = Util.String2Bool((objDR["Multiple_Trip_Day"].ToString()));
            }
        }



        public void Save()
        {

            base.DBSave();
            //objVehicleHireDetailsModel.Save();
        }
    }
}