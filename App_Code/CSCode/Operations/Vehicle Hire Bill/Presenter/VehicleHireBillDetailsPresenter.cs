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
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;


/// <summary>
/// Summary description for VehicleHireBillDetailsPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{

    public class VehicleHireBillDetailsPresenter : Presenter
    {
        private IVehicleHireBillDetailsView objIVehicleHireBillDetailsView;
        private VehicleHireBillDetailsModel objVehicleHireBillDetailsModel;
        private DataSet objDS;
        int _keyID;
        public VehicleHireBillDetailsPresenter(IVehicleHireBillDetailsView vehicleHireBillDetailsView, bool isPostBack)
        {
            objIVehicleHireBillDetailsView = vehicleHireBillDetailsView;
            objVehicleHireBillDetailsModel = new VehicleHireBillDetailsModel(objIVehicleHireBillDetailsView);
            base.Init(objIVehicleHireBillDetailsView, objVehicleHireBillDetailsModel);           
            if (!isPostBack)
            {
                DataSet ds = new DataSet();
                objDS = objVehicleHireBillDetailsModel.FillValues();
                objIVehicleHireBillDetailsView.VehicleHireBillDate = DateTime.Now;
                objIVehicleHireBillDetailsView.Bind_ddl_BrokerName = objDS.Tables["BrokerName"];
                objIVehicleHireBillDetailsView.Bind_ddl_FreightType = objDS.Tables["FreightType"];
                objIVehicleHireBillDetailsView.IsHOBSeriesRequired = Util.String2Bool(objDS.Tables[2].Rows[0]["Is_HOB_Series_Req"].ToString());
                initValues();
                
                

            }

        }
        public void Save()
        {
            base.DBSave();
        }
        public void initValues()
        {
            objDS = objVehicleHireBillDetailsModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIVehicleHireBillDetailsView.VehicleHireBillDate = Convert.ToDateTime(objDR["Hire_Bill_Date"]);
                objIVehicleHireBillDetailsView.VehicleHireBillNo = objDR["Hire_Bill_No_For_Print"].ToString();
                objIVehicleHireBillDetailsView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                objIVehicleHireBillDetailsView.RefNo = objDR["Ref_No"].ToString();
                objIVehicleHireBillDetailsView.SetFromLocationID(objDR["From_Location"].ToString(), objDR["From_Location_ID"].ToString());
                objIVehicleHireBillDetailsView.SetToLocationID(objDR["To_Location"].ToString(), objDR["To_Location_ID"].ToString());
                objIVehicleHireBillDetailsView.BrokerID = Util.String2Int(objDR["Broker_ID"].ToString());
                objIVehicleHireBillDetailsView.SetDriver1ID(objDR["Driver1_Name"].ToString(), objDR["Driver1_Id"].ToString());
                objIVehicleHireBillDetailsView.SetDriver2ID(objDR["Driver2_Name"].ToString(), objDR["Driver2_Id"].ToString());
                objIVehicleHireBillDetailsView.SetCleanerID(objDR["Cleaner_Name"].ToString(), objDR["Cleaner_ID"].ToString());
                objIVehicleHireBillDetailsView.FreightTypeID = Util.String2Int(objDR["LHPO_Freight_Basis_ID"].ToString());
                objIVehicleHireBillDetailsView.WtGuarantee = Util.String2Decimal(objDR["Min_Wt_Guarantee"].ToString());
                objIVehicleHireBillDetailsView.RateKg = Util.String2Decimal(objDR["Rate"].ToString());
                objIVehicleHireBillDetailsView.ActualWtPayableValue = Util.String2Decimal(objDR["Wt_Kms_Articles_Payable"].ToString());

                if (objIVehicleHireBillDetailsView.FreightTypeID == 3)
                {
                    objIVehicleHireBillDetailsView.ActualKms = Util.String2Decimal(objDR["Actual_Kms"].ToString());
                }
                else if (objIVehicleHireBillDetailsView.FreightTypeID == 1)
                {
                    objIVehicleHireBillDetailsView.ActualKms = Util.String2Decimal(objDR["Actual_Wt"].ToString());
                }
                else if (objIVehicleHireBillDetailsView.FreightTypeID == 4)
                {
                    objIVehicleHireBillDetailsView.ActualKms = Util.String2Decimal(objDR["Actual_Articles"].ToString());
                }

                objIVehicleHireBillDetailsView.TDSPercentage = Util.String2Decimal(objDR["TDS_Percent"].ToString());
                objIVehicleHireBillDetailsView.TDSAmount = Util.String2Decimal(objDR["TDS_Amount"].ToString());
                objIVehicleHireBillDetailsView.TruckHireCharge = Util.String2Decimal(objDR["Truck_Hire_Charge"].ToString());
                objIVehicleHireBillDetailsView.TotalTruckHireCharge = Util.String2Decimal(objDR["Total_Truck_Hire"].ToString());
                objIVehicleHireBillDetailsView.AdvanceReceived = Util.String2Decimal(objDR["Advance_Received"].ToString());
                objIVehicleHireBillDetailsView.BrokeragePayable = Util.String2Decimal(objDR["Brokerage_Payable"].ToString());
                objIVehicleHireBillDetailsView.CollectionChargePayable = Util.String2Decimal(objDR["Collection_Charges"].ToString());
                objIVehicleHireBillDetailsView.CommittedDelDate = Convert.ToDateTime(objDR["Commited_Delivery_Date"].ToString());
                objIVehicleHireBillDetailsView.VehicleDepartureTime = objDR["Vehicle_Departure_Time"].ToString();
                objIVehicleHireBillDetailsView.TransitDays = Util.String2Int(objDR["Transit_Days"].ToString());
                objIVehicleHireBillDetailsView.Remark = objDR["Remarks"].ToString();

                if (objIVehicleHireBillDetailsView.VehicleID > 0)
                {
                    DataSet ds1 = objVehicleHireBillDetailsModel.GetVehicleInformationOnVehicleChanged();
                    objIVehicleHireBillDetailsView.VehicleCapacity = Util.String2Int(ds1.Tables[0].Rows[0]["Vehicle_Capacity"].ToString());
                }

            }     
           
        }
        public DataSet SetVehicleInfoOnVehicleChanged()
        {
            objDS = objVehicleHireBillDetailsModel.GetVehicleInformationOnVehicleChanged();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                objIVehicleHireBillDetailsView.SetDriver1ID(DR["Driver_Name"].ToString(), DR["Driver_ID"].ToString());
                objIVehicleHireBillDetailsView.SetDriver2ID(DR["Driver2_Name"].ToString(), DR["Driver2_ID"].ToString());
                objIVehicleHireBillDetailsView.SetCleanerID(DR["Cleaner_Name"].ToString(), DR["Cleaner_ID"].ToString());
                objIVehicleHireBillDetailsView.VehicleCapacity = Util.String2Int(DR["Vehicle_Capacity"].ToString());
               // objILHPOHireDetailsView.TDSPercentage = Convert.ToDecimal(DR["TDS_Percent"]);
            }
            return objDS;
        }


        public void GetTDSPercent()
        {
            objDS = objVehicleHireBillDetailsModel.GetTDSercent();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                objIVehicleHireBillDetailsView.TDSPercentage = Util.String2Decimal(DR["Tax_Rate"].ToString());
                objIVehicleHireBillDetailsView.TDSAmount = Util.String2Decimal(DR["Tax_amount"].ToString());
               
            }
        }       
       

    }
}
