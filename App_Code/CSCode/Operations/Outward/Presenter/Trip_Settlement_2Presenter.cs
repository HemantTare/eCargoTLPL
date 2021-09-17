using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP.General;
using ClassLibraryMVP;

/// <summary>
/// Summary description for Trip_Settlement_2Presenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class Trip_Settlement_2Presenter:Presenter
    {
        private ITrip_Settlement_2_View objITrip_Settlement_2_View;
        private Trip_Settlement_2_Model objTrip_Settlement_2_Model;
        private DataSet objDS;
        private DataRow objDR;

        public Trip_Settlement_2Presenter(ITrip_Settlement_2_View ITrip_Settlement_2_View, bool isPostBack)
        {
            objITrip_Settlement_2_View = ITrip_Settlement_2_View;
            objTrip_Settlement_2_Model = new Trip_Settlement_2_Model(objITrip_Settlement_2_View);
            base.Init(objITrip_Settlement_2_View, objTrip_Settlement_2_Model);
            if (!isPostBack)
            {
                FillValues();
                InitValues();
            }
        }

        private void FillValues()
        {
            objDS = objTrip_Settlement_2_Model.FillValues();
            objITrip_Settlement_2_View.SessionFromBranchDropdown = objDS.Tables[0];
            objITrip_Settlement_2_View.SessionToBranchDropdown = objDS.Tables[1];
            objITrip_Settlement_2_View.SessionExpenseHeadDropdown  = objDS.Tables[2];
            objITrip_Settlement_2_View.SessionPumpDropDown = objDS.Tables[3];

        }

        private void InitValues()
        {
            objDS = objTrip_Settlement_2_Model.ReadValues();
            objITrip_Settlement_2_View.SessionTripHireChallansGrid = objDS.Tables[1];
            objITrip_Settlement_2_View.SessionTripFuelDetailsGrid = objDS.Tables[2];
            objITrip_Settlement_2_View.SessionTripExpenseGrid = objDS.Tables[3];
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objDR = objDS.Tables[0].Rows[0];

                objITrip_Settlement_2_View.Vehicle_Trip_No  = objDR["Vehicle_Trip_No_For_Print"].ToString();
                objITrip_Settlement_2_View.Vehicle_Trip_Date  = Convert.ToDateTime(objDR["Vehicle_Trip_Date"].ToString());

                objITrip_Settlement_2_View.SetDriverID(objDR["Driver_Name"].ToString(), objDR["Driver_ID"].ToString());

                objITrip_Settlement_2_View.Trip_Start_Date  = Convert.ToDateTime(objDR["Trip_Start_Date"].ToString());
                objITrip_Settlement_2_View.Trip_End_Date  = Convert.ToDateTime(objDR["Trip_End_Date"].ToString());
                objITrip_Settlement_2_View.Vehicle_ID  = Util.String2Int(objDR["Vehicle_ID"].ToString());
                objITrip_Settlement_2_View.Total_Hire_Amount  = Util.String2Decimal(objDR["Total_Hire_Amount"].ToString());
                objITrip_Settlement_2_View.Total_Advance  = Util.String2Decimal(objDR["Total_Advance"].ToString());
                objITrip_Settlement_2_View.Total_Trip_Expense  = Util.String2Decimal(objDR["Total_Trip_Expense"].ToString());
                //objITrip_Settlement_2_View.TotalDieselCash = Util.String2Decimal(objDR["Total_Diesel_Cash"].ToString());
                //objITrip_Settlement_2_View.TotalDieselCredit = Util.String2Decimal(objDR["Total_Diesel_Credit"].ToString());
                objITrip_Settlement_2_View.Total_Trip_Cost = Util.String2Decimal(objDR["Total_Trip_Cost"].ToString());
                objITrip_Settlement_2_View.Driver_Closing_Balance  = Util.String2Decimal(objDR["Driver_Closing_Balance"].ToString());
                objITrip_Settlement_2_View.Remarks = objDR["Remarks"].ToString();
            }

        }


        public void Save()
        {
            base.DBSave();
        }

        public decimal GetDriverOpeningBalance()
        {
            decimal DriverOpeningBalance = objTrip_Settlement_2_Model.GetDriverOpeningBalance();

            objITrip_Settlement_2_View.DriverOpeningBalance = DriverOpeningBalance;

            if (objTrip_Settlement_2_Model.GetDriverOpeningBalance() <= 0)
            {
                objITrip_Settlement_2_View.OBDrCr = "Dr";
            }
            else
                objITrip_Settlement_2_View.OBDrCr = "Cr";

            return DriverOpeningBalance;
        }

        public DateTime GetVehicle_OnRoadDate()
        {
            DateTime onRoadDate;
            objDS = objTrip_Settlement_2_Model.GetVehicle_OnRoadDate();
            onRoadDate = Convert.ToDateTime(objDS.Tables[0].Rows[0]["On_Road_Date"].ToString());
            return onRoadDate;
        }
    }
}