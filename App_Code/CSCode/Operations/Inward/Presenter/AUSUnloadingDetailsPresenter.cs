using System;
using System.Data;

using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 27-10-08
/// Summary description for AUSUnloadingDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class AUSUnloadingDetailsPresenter : Presenter
    {
        private IAUSUnloadingDetailsView objIAUSUnloadingDetailsView;
        private AUSUnloadingDetailsModel objAUSUnloadingDetailsModel;
        private DataSet objDS;

        public AUSUnloadingDetailsPresenter(IAUSUnloadingDetailsView AUSUnloadingDetailsView, bool isPostBack)
        {
            objIAUSUnloadingDetailsView = AUSUnloadingDetailsView;
            objAUSUnloadingDetailsModel = new AUSUnloadingDetailsModel(objIAUSUnloadingDetailsView);
            base.Init(objIAUSUnloadingDetailsView, objAUSUnloadingDetailsModel);

            if (!isPostBack)
            {
                objIAUSUnloadingDetailsView.AUS_Date = DateTime.Now.Date;
                //objIAUSUnloadingDetailsView.TASDate = DateTime.Now.Date;
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objAUSUnloadingDetailsModel.FillValues();

            objIAUSUnloadingDetailsView.BindResionForLateArrivalUnloading = objDS.Tables[0];
            objIAUSUnloadingDetailsView.SessionReceivedCondition = objDS.Tables[1];

          //  objIAUSUnloadingDetailsView.BindSupervisor = objDS.Tables[2];

        }

        private void initValues()
        {
            fillValues();

           // if (objIPetrolPumpGeneralView.keyID > 0)
            {
                ReadValues();
            }

        }



        public void ReadValues()
        {
            objDS = objAUSUnloadingDetailsModel.ReadValues();


            Object sender = "";
            System.EventArgs e = null;

            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[1].Rows[0];

                objIAUSUnloadingDetailsView.TURNo = objDR["Actual_Unloading_Sheet_No_For_Print"].ToString();
                objIAUSUnloadingDetailsView.VehicleSearchView.VehicleID = Util.String2Int(objDR["Vehicle_Id"].ToString());

                objIAUSUnloadingDetailsView.Vehicle_Id = Util.String2Int(objDR["Vehicle_Id"].ToString());

                objIAUSUnloadingDetailsView.AUS_Date = Convert.ToDateTime(objDR["Actual_Unloading_Sheet_Date"].ToString());
                objIAUSUnloadingDetailsView.Manual_TUR_No = objDR["Manual_Tur_No"].ToString();
                //objIAUSUnloadingDetailsView.LHPO_Id = Util.String2Int(objDR["LHPO_ID"].ToString());

                objIAUSUnloadingDetailsView.SetLHPO(objDR["LHPO_No_For_Print"].ToString(), objDR["LHPO_ID"].ToString());
                objIAUSUnloadingDetailsView.LHPO_Date = objDR["LHPO_Date"].ToString();
                objIAUSUnloadingDetailsView.SetTAS(objDR["TAS_No_For_Print"].ToString(), objDR["TAS_ID"].ToString());

                objIAUSUnloadingDetailsView.Total_Loaded_Articles = Util.String2Int(objDR["Total_Actual_Articles"].ToString());

                objIAUSUnloadingDetailsView.Total_Booking_Articles = Util.String2Int(objDR["Total_Short_Articles"].ToString());
                objIAUSUnloadingDetailsView.Total_Booking_Articles_Wt = Util.String2Decimal(objDR["Total_Short_Articles"].ToString());

                objIAUSUnloadingDetailsView.Total_Loaded_Articles_Wt = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIAUSUnloadingDetailsView.Total_Received_Articles = Util.String2Int(objDR["Total_Received_Articles"].ToString());
                objIAUSUnloadingDetailsView.Total_Received_Articles_Wt = Util.String2Decimal(objDR["Total_Received_Weight"].ToString());
                objIAUSUnloadingDetailsView.Total_Damage_Leakage_Articles = Util.String2Int(objDR["Total_Damaged_Leakage_Articles"].ToString());
                objIAUSUnloadingDetailsView.Total_Damage_Leakage_Value = Util.String2Decimal(objDR["Total_Damaged_Leakage_Value"].ToString());

                objIAUSUnloadingDetailsView.Total_Additional_Freight = Util.String2Decimal(objDR["Total_Additional_Freight"].ToString());
                objIAUSUnloadingDetailsView.Total_Delivery_Commision = Util.String2Decimal(objDR["Total_Delivery_Commision"].ToString());
                objIAUSUnloadingDetailsView.Total_To_Pay_Collection = Util.String2Decimal(objDR["Total_To_Pay_Collection"].ToString());
                objIAUSUnloadingDetailsView.Lorry_Hire = Util.String2Decimal(objDR["Balance_payble_Amount"].ToString());

                objIAUSUnloadingDetailsView.Other_Receavable = Util.String2Decimal(objDR["Other_Receavable_Charges"].ToString());
                objIAUSUnloadingDetailsView.Other_Payable = Util.String2Decimal(objDR["Other_Payable_Charges"].ToString());

                objIAUSUnloadingDetailsView.Total_Receable = Util.String2Decimal(objDR["Total_Receable"].ToString());
                objIAUSUnloadingDetailsView.Total_Payable  = Util.String2Decimal(objDR["Total_Payable"].ToString());

                objIAUSUnloadingDetailsView.TotalShortArticlesValue = Util.String2Int(objDR["Total_Short_Articles"].ToString());
                objIAUSUnloadingDetailsView.TotalExcessArticlesValue = Util.String2Int(objDR["Total_Excess_Articles"].ToString());
                
                objIAUSUnloadingDetailsView.ScheduledArivalDate = objDR["Scheduled_Arrival_Date"].ToString();
                objIAUSUnloadingDetailsView.ScheduledArivalTime = objDR["Scheduled_Arrival_Time"].ToString();

                objIAUSUnloadingDetailsView.NoofMinuteDifferenceForLate=Util.String2Int(objDR["Scheduled_Arrival_Time"].ToString());
                
                objIAUSUnloadingDetailsView.TASTime = objDR["Vehicle_Arrival_Time"].ToString();
                objIAUSUnloadingDetailsView.TASDate = Convert.ToDateTime(objDR["Vehicle_Arrival_Date"].ToString());
                //objIAUSUnloadingDetailsView.TASDate = Convert.ToDateTime(objDR["TAS_Date"].ToString());
                
                //objIAUSUnloadingDetailsView.TASTime=ob
                objIAUSUnloadingDetailsView.Reason_For_Late_Arrival = Util.String2Int(objDR["Reason_For_Late_Arrival_ID"].ToString());
                objIAUSUnloadingDetailsView.Reason_For_Late_Arrival_Display = objDR["reason"].ToString();

                objIAUSUnloadingDetailsView.Reason_For_Late_Uploading = Util.String2Int(objDR["Reason_For_Late_Unloading_ID"].ToString());

                objIAUSUnloadingDetailsView.UnloadingTime = objDR["Truck_Unloaded_Time"].ToString();

                //objIAUSUnloadingDetailsView.Supervisor = Util.String2Int(objDR["Unloaded_Supervisor_ID"].ToString());

                objIAUSUnloadingDetailsView.SetSupervisor(objDR["Unloaded_Supervisor_Name"].ToString(), objDR["Unloaded_Supervisor_ID"].ToString());

                objIAUSUnloadingDetailsView.Remarks = objDR["Remarks"].ToString();
                objIAUSUnloadingDetailsView.LHPOFromLocation = objDR["From_Location"].ToString();
                objIAUSUnloadingDetailsView.LHPOToLocation = objDR["To_Location"].ToString();
                objIAUSUnloadingDetailsView.BTHAmount = Util.String2Decimal(objDR["Balance_payble_Amount"].ToString());
                objIAUSUnloadingDetailsView.UpCountryReceivable = Util.String2Decimal(objDR["UpCountry_Receivable"].ToString());
                objIAUSUnloadingDetailsView.UpcountryCrossingCost = Util.String2Decimal(objDR["UpCountry_Crossing_Cost"].ToString());

               
                
            }

            objIAUSUnloadingDetailsView.Bind_dg_UnloadingDetails = objDS.Tables[0];

            

            Set_Actual_Unloading_Total_Details();

            Get_VehicleDetails(sender, e);
        }

        public void Get_VehicleDetails(object sender, EventArgs e)
        {

             objAUSUnloadingDetailsModel.Get_VehicleDetails();         

        }

        public void  Get_LHPO(object sender, EventArgs e)
        {

            objIAUSUnloadingDetailsView.BindLHPO = objAUSUnloadingDetailsModel.Get_LHPO();

        }

        public void Get_TAS(object sender, EventArgs e)
        {
            objIAUSUnloadingDetailsView.BindTAS = objAUSUnloadingDetailsModel.Get_TAS();
        }

        public void Get_TASDetails(object sender, EventArgs e)
        {
            objDS = objAUSUnloadingDetailsModel.Get_TASDetails();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIAUSUnloadingDetailsView.TASTime = objDS.Tables[0].Rows[0]["TAS_Time"].ToString();
                objIAUSUnloadingDetailsView.TASDate = Convert.ToDateTime(objDS.Tables[0].Rows[0] ["TAS_Date"].ToString());
                objIAUSUnloadingDetailsView.Reason_For_Late_Arrival_Display = objDS.Tables[0].Rows[0]["reason"].ToString();
                //objIAUSUnloadingDetailsView.Reason_For_Late_Arrival = Util.String2Int(objDS.Tables[0].Rows[0]["Reason_For_Late_Arrival_ID"].ToString());                
           }
           objDS.Tables.Remove("table");

           objIAUSUnloadingDetailsView.BindLHPO = objDS;              
        }

        public void Get_LHPODetails(object sender, EventArgs e)
        {
            objDS = objAUSUnloadingDetailsModel.Get_LHPODetails();

            objIAUSUnloadingDetailsView.Bind_dg_UnloadingDetails = objDS.Tables[0];

            Set_Actual_Unloading_Total_Details();

           
        }


        public void Set_Actual_Unloading_Total_Details()
        {

            objIAUSUnloadingDetailsView.Total_Booking_Articles = 0;
            objIAUSUnloadingDetailsView.Total_Booking_Articles_Wt = 0;

            objIAUSUnloadingDetailsView.Total_Loaded_Articles = 0;
            objIAUSUnloadingDetailsView.Total_Loaded_Articles_Wt = 0;

            objIAUSUnloadingDetailsView.Total_Received_Articles = 0;
            objIAUSUnloadingDetailsView.Total_Received_Articles_Wt = 0;

            objIAUSUnloadingDetailsView.Total_Damage_Leakage_Articles = 0;
            objIAUSUnloadingDetailsView.Total_Damage_Leakage_Value = 0;

            objIAUSUnloadingDetailsView.Total_Additional_Freight = 0;

            if (objDS.Tables[0].Rows.Count > 0)
            {

                objIAUSUnloadingDetailsView.Total_Booking_Articles = Util.String2Int(objDS.Tables[0].Compute("sum(Booking_Article)", "").ToString());
                objIAUSUnloadingDetailsView.Total_Booking_Articles_Wt = Util.String2Decimal  (objDS.Tables[0].Compute("sum(Booking_Article_Wt)", "").ToString());

                objIAUSUnloadingDetailsView.Total_Loaded_Articles = Util.String2Int(objDS.Tables[0].Compute("sum(Loaded_Articles)", "").ToString());
                objIAUSUnloadingDetailsView.Total_Loaded_Articles_Wt = Util.String2Decimal(objDS.Tables[0].Compute("sum(Loaded_Actual_Wt)", "").ToString());

                objIAUSUnloadingDetailsView.Total_Received_Articles = Util.String2Int(objDS.Tables[0].Compute("sum(Received_Articles)", "").ToString());
                objIAUSUnloadingDetailsView.Total_Received_Articles_Wt = Util.String2Decimal(objDS.Tables[0].Compute("sum(Received_Wt)", "").ToString());

                objIAUSUnloadingDetailsView.Total_Damage_Leakage_Articles = Util.String2Int(objDS.Tables[0].Compute("sum(damaged_articles)", "").ToString());
                objIAUSUnloadingDetailsView.Total_Damage_Leakage_Value = Util.String2Decimal(objDS.Tables[0].Compute("sum(Damaged_Value)", "").ToString());

                objIAUSUnloadingDetailsView.Total_Additional_Freight = Util.String2Decimal(objDS.Tables[0].Compute("sum(Additional_Freight)", "").ToString());

                //objIAUSUnloadingDetailsView.Total_Booking_Articles = Util.String2Int(objSqlParam[3].Value.ToString());
                //objIAUSUnloadingDetailsView.Total_Booking_Articles_Wt = Util.String2Int(objSqlParam[4].Value.ToString());

                //objIAUSUnloadingDetailsView.Total_Loaded_Articles = Util.String2Int(objSqlParam[5].Value.ToString());
                //objIAUSUnloadingDetailsView.Total_Loaded_Articles_Wt = Util.String2Int(objSqlParam[6].Value.ToString());

                //objIAUSUnloadingDetailsView.Total_Received_Articles = Util.String2Int(objSqlParam[7].Value.ToString());
                //objIAUSUnloadingDetailsView.Total_Received_Articles_Wt = Util.String2Int(objSqlParam[8].Value.ToString());

                //objIAUSUnloadingDetailsView.Total_Damage_Leakage_Articles = Util.String2Int(objSqlParam[9].Value.ToString());
                //objIAUSUnloadingDetailsView.Total_Damage_Leakage_Value = Util.String2Int(objSqlParam[10].Value.ToString());
            }
        }
    }
}