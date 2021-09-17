using System;
using System.Data;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Name : Dinesh Mahajan
/// Date :  
/// Summary description for AUSOtherAgencyUnloadingDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class AUSOtherAgencyUnloadingDetailsPresenter : Presenter
    {
        private IAUSOtherAgencyUnloadingDetailsView objIAUSOtherAgencyUnloadingDetailsView;
        private AUSOtherAgencyUnloadingDetailsModel objAUSOtherAgencyUnloadingDetailsModel;
        private DataSet objDS;

        public AUSOtherAgencyUnloadingDetailsPresenter(IAUSOtherAgencyUnloadingDetailsView AUSOtherAgencyUnloadingDetailsView, bool isPostBack)
        {
            objIAUSOtherAgencyUnloadingDetailsView = AUSOtherAgencyUnloadingDetailsView;
            objAUSOtherAgencyUnloadingDetailsModel = new AUSOtherAgencyUnloadingDetailsModel(objIAUSOtherAgencyUnloadingDetailsView);
            base.Init(objIAUSOtherAgencyUnloadingDetailsView, objAUSOtherAgencyUnloadingDetailsModel);

            if (!isPostBack)
            {
                objIAUSOtherAgencyUnloadingDetailsView.AUS_Date = DateTime.Now.Date;
                objIAUSOtherAgencyUnloadingDetailsView.LHPO_Date = DateTime.Now.Date;
                objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalDate = DateTime.Now.Date;
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objAUSOtherAgencyUnloadingDetailsModel.FillValues();

            objIAUSOtherAgencyUnloadingDetailsView.BindResionForLateArrivalUnloading = objDS.Tables[0];
            objIAUSOtherAgencyUnloadingDetailsView.SessionReceivedCondition = objDS.Tables[1];
            objIAUSOtherAgencyUnloadingDetailsView.BindAgency = objDS.Tables[2];
        }
        public void Get_Agency_Ledger()
        {
            objDS = objAUSOtherAgencyUnloadingDetailsModel.Get_Agency_Ledger();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                objIAUSOtherAgencyUnloadingDetailsView.SetAgency_Ledger(objDR["Agency_Ledger_Name"].ToString(), objDR["Agency_Ledger_Id"].ToString());
            }
            else
            {
                objIAUSOtherAgencyUnloadingDetailsView.SetAgency_Ledger("", "0");
            }
        }
        private void initValues()
        {
            fillValues();
            ReadValues();
        }
        public void FillUnloadingDetails()
        {
            objDS = objAUSOtherAgencyUnloadingDetailsModel.ReadValues();
            objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid = objDS.Tables[0];
        }
        public void ReadValues()
        {
            objDS = objAUSOtherAgencyUnloadingDetailsModel.ReadValues();

            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[1].Rows[0];

                objIAUSOtherAgencyUnloadingDetailsView.TURNo = objDR["Actual_Unloading_Sheet_No_For_Print"].ToString();                
                objIAUSOtherAgencyUnloadingDetailsView.AUS_Date = Convert.ToDateTime(objDR["Actual_Unloading_Sheet_Date"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.VehicleNo = objDR["Vehicle_No"].ToString();
                objIAUSOtherAgencyUnloadingDetailsView.LHPO_No_For_Print = objDR["LHPO_No_For_Print"].ToString();
                objIAUSOtherAgencyUnloadingDetailsView.LHPO_Date = Convert.ToDateTime(objDR["LHPO_Date"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.SetArrived_From(objDR["Arrvied_From_Service_Location_Name"].ToString(), objDR["Arrived_From_Location_Id"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.SetAgency_Ledger(objDR["Agency_Ledger_Name"].ToString(), objDR["Agency_Ledger_ID"].ToString());

                objIAUSOtherAgencyUnloadingDetailsView.ArrivedFromBranchId = Util.String2Int(objDR["Arrived_From_Branch_Id"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.ArrivedFromLoacationId = Util.String2Int(objDR["Arrived_From_Location_Id"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.AgencyId = Util.String2Int(objDR["Agency_Branch_ID"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_GC = Util.String2Int(objDR["Total_Actual_GCs"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles = Util.String2Decimal(objDR["Total_Actual_Articles"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles_Wt = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles = Util.String2Int(objDR["Total_Loaded_Articles"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles_Wt = Util.String2Decimal(objDR["Total_Loaded_Weight"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles = Util.String2Decimal(objDR["Total_Received_Articles"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles_Wt = Util.String2Decimal(objDR["Total_Received_Weight"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Articles = Util.String2Decimal(objDR["Total_Damaged_Leakage_Articles"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Value = Util.String2Decimal(objDR["Total_Damaged_Leakage_Value"].ToString());

                objIAUSOtherAgencyUnloadingDetailsView.Lorry_Hire = Util.String2Decimal(objDR["Lorry_Hire"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Other_Payable = Util.String2Decimal(objDR["Other_Payable_Charges"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Goods_Dly_Rec = Util.String2Decimal(objDR["Total_To_Pay_Collection"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Upcountry_Rec = Util.String2Decimal(objDR["UpCountry_Receivable"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Receivable = Util.String2Decimal(objDR["Total_Receable"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Service_charge_Payable = Util.String2Decimal(objDR["Total_Delivery_Commision"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Upcountry_Crossing_Cost_Payable = Util.String2Decimal(objDR["UpCountry_Crossing_Cost"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Total_Payable = Util.String2Decimal(objDR["Total_Payable"].ToString());

                objIAUSOtherAgencyUnloadingDetailsView.ScheduledArivalDate = objDR["Scheduled_Arrival_Date"].ToString();
                objIAUSOtherAgencyUnloadingDetailsView.ScheduledArivalTime = objDR["Scheduled_Arrival_Time"].ToString();
                objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalDate = Convert.ToDateTime(objDR["Vehicle_Arrival_Date"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalTime = objDR["Vehicle_Arrival_Time"].ToString();
                objIAUSOtherAgencyUnloadingDetailsView.UnloadingTime = objDR["Truck_Unloaded_Time"].ToString();
                objIAUSOtherAgencyUnloadingDetailsView.Reason_For_Late_Uploading = Util.String2Int(objDR["Reason_For_Late_Unloading_ID"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.SetSupervisor(objDR["Unloaded_Supervisor_Name"].ToString(), objDR["Unloaded_Supervisor_ID"].ToString());
                objIAUSOtherAgencyUnloadingDetailsView.Remarks = objDR["Remarks"].ToString();
            }
            objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid = objDS.Tables[0];
        }
     
        public DataSet Get_ToLocationDetails()
        {
            objDS = objAUSOtherAgencyUnloadingDetailsModel.Get_ToLocationDetails();
            return objDS;
        }

        public void Save()
        {
            base.DBSave();
        }

        //public void Set_Actual_Unloading_Total_Details()
        //{
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles_Wt = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles_Wt = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles_Wt = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Articles = 0;
        //    objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Value = 0;

        //    if (objDS.Tables[0].Rows.Count > 0)
        //    {
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles = Util.String2Int(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Booking_Article)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles_Wt = Util.String2Decimal(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Booking_Article_Wt)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles = Util.String2Int(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Loaded_Articles)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles_Wt = Util.String2Decimal(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Loaded_Actual_Wt)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles = Util.String2Int(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Received_Articles)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles_Wt = Util.String2Decimal(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Received_Wt)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Articles = Util.String2Int(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(damaged_articles)", "").ToString());
        //        objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Value = Util.String2Decimal(objIAUSOtherAgencyUnloadingDetailsView.SessionUnloadingDetailsGrid.Compute("sum(Damaged_Value)", "").ToString());
        //    }
        //}
    }
}