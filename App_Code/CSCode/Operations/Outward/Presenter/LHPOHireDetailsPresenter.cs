using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for LHPOHireDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOHireDetailsPresenter : Presenter
    {
        private ILHPOHireDetailsView objILHPOHireDetailsView;
        private LHPOHireDetailsModel objLHPOHireDetailsModel;
        private DataSet objDS;
        int _keyID;
        public LHPOHireDetailsPresenter(ILHPOHireDetailsView lHPOHireDetailsView, bool isPostBack)
        {
            objILHPOHireDetailsView = lHPOHireDetailsView;
            objLHPOHireDetailsModel = new LHPOHireDetailsModel(objILHPOHireDetailsView);
            base.Init(objILHPOHireDetailsView, objLHPOHireDetailsModel);
            _keyID = objILHPOHireDetailsView.keyID;
            if (objILHPOHireDetailsView.LHPOTypeID == 2 && objILHPOHireDetailsView.keyID <= 0)
            {
                _keyID = objILHPOHireDetailsView.LHPONo;
            }

            if (!isPostBack)
            {
                objILHPOHireDetailsView.LHPODate = DateTime.Now;
                FillValues();
                initValues();
            }
        }

        private void FillValues()
        {
            DataSet ds = new DataSet();

            objDS = objLHPOHireDetailsModel.FillValues();
            objILHPOHireDetailsView.Bind_ddl_VehicleCategory = objDS.Tables["VehicleCategory"];
            objILHPOHireDetailsView.Bind_ddl_BrokerName = objDS.Tables["BrokerName"];
            objILHPOHireDetailsView.Bind_ddl_LHPOType = objDS.Tables["LhpoType"];
            objILHPOHireDetailsView.Bind_ddl_FreightType = objDS.Tables["FreightType"];
            //ds.Tables.Add(objDS.Tables["MEMOGRID"].Copy());

            ds = objLHPOHireDetailsModel.FillGrid();

            objILHPOHireDetailsView.SessionLHPOHireDetailsGrid = ds;
            objILHPOHireDetailsView.Bind_dg_LHPOHireDetails = ds;

        }

        public void Save()
        {
            base.DBSave();
        }
        public void initValues()
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            if (_keyID > 0)
            {
                objDS = objLHPOHireDetailsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];

                    objILHPOHireDetailsView.AttachedLHPODate = Convert.ToDateTime(DR["LHPO_Date"]);                    

                    ds = objLHPOHireDetailsModel.FillGrid();
                    objILHPOHireDetailsView.SessionLHPOHireDetailsGrid = ds;
                    objILHPOHireDetailsView.Bind_dg_LHPOHireDetails = ds;

                    if (objILHPOHireDetailsView.LHPOTypeID != 2 && objILHPOHireDetailsView.keyID > 0)
                    {
                        objILHPOHireDetailsView.LHPOTypeID = Util.String2Int(DR["LHPO_Type_ID"].ToString());
                        objILHPOHireDetailsView.VehicleCategoryID = Util.String2Int(DR["Vehicle_Category_ID"].ToString());
                        objILHPOHireDetailsView.VehicleID = Util.String2Int(DR["Vehicle_ID"].ToString());
                        objILHPOHireDetailsView.LHPODate = Convert.ToDateTime(DR["LHPO_Date"]);
                    }
                    
                    objILHPOHireDetailsView.LHPONOForPrint = DR["LHPO_No_For_Print"].ToString();                    
                    objILHPOHireDetailsView.ManualRefNo  = DR["Manual_Ref_No"].ToString();                    
                    objILHPOHireDetailsView.SetFromLocationID(DR["From_Location"].ToString(), DR["From_Location_ID"].ToString());
                    objILHPOHireDetailsView.SetToLocationID(DR["To_Location"].ToString(), DR["To_Location_ID"].ToString());
                    objILHPOHireDetailsView.BrokerID = Util.String2Int(DR["Broker_ID"].ToString());
                    objILHPOHireDetailsView.TDSCertificateToID = Util.String2Bool(DR["Is_TDS_Certificate_Broker"].ToString());
                    objILHPOHireDetailsView.TotalMemos = Util.String2Int(DR["Total_No_Of_Memo"].ToString());
                    objILHPOHireDetailsView.TotalArticle = Util.String2Int(DR["Total_Articles"].ToString());
                    objILHPOHireDetailsView.TotalArticleWT = Util.String2Decimal(DR["Total_Actual_Weight"].ToString());
                    objILHPOHireDetailsView.TotalGC = Util.String2Int(DR["Total_No_Of_GCs"].ToString());                    
                    objILHPOHireDetailsView.SetDriver1ID(DR["Driver1_Name"].ToString(), DR["Driver1_Id"].ToString());
                    objILHPOHireDetailsView.SetDriver2ID(DR["Driver2_Name"].ToString(), DR["Driver2_Id"].ToString());
                    objILHPOHireDetailsView.SetCleanerID(DR["Cleaner_Name"].ToString(), DR["Cleaner_ID"].ToString());                    
                    
                    objILHPOHireDetailsView.FreightTypeID = Util.String2Int(DR["LHPO_Freight_Basis_ID"].ToString());
                    objILHPOHireDetailsView.FreightTypeID_Edit  = Util.String2Int(DR["LHPO_Freight_Basis_ID"].ToString());

                    objILHPOHireDetailsView.WtGuarantee = Util.String2Decimal(DR["Min_Wt_Guarantee"].ToString());
                    objILHPOHireDetailsView.WtGuarantee_Edit  = Util.String2Decimal(DR["Min_Wt_Guarantee"].ToString());

                    objILHPOHireDetailsView.RateKg = Util.String2Decimal(DR["Rate"].ToString());
                    objILHPOHireDetailsView.RateKg_Edit = Util.String2Decimal(DR["Rate"].ToString());

                    objILHPOHireDetailsView.ActualWtPayableValue = Util.String2Decimal(DR["Wt_Kms_Articles_Payable"].ToString());
                    //objILHPOHireDetailsView.ActualWtPayableValue_Edit = Util.String2Decimal(DR["Wt_Kms_Articles_Payable"].ToString());

                    objILHPOHireDetailsView.TDSPercentage = Util.String2Decimal(DR["TDS_Percent"].ToString());
                    objILHPOHireDetailsView.TDSAmount = Util.String2Decimal(DR["TDS_Amount"].ToString());
                    objILHPOHireDetailsView.TotalAdvancePaid = Util.String2Decimal(DR["Total_Advance_To_Be_Paid"].ToString());
                    objILHPOHireDetailsView.BalanceAmount = Util.String2Decimal(DR["Balance_Payble_Amount"].ToString());
                    objILHPOHireDetailsView.CrossingCostPayble = Util.String2Decimal(DR["Crossing_Cost_Payable"].ToString());
                    objILHPOHireDetailsView.ToPayCollection = Util.String2Decimal(DR["To_Pay_Collection"].ToString());                    
                    objILHPOHireDetailsView.ActualKms = Util.String2Decimal(DR["Actual_Kms"].ToString());
                    objILHPOHireDetailsView.DeliveryCommission = Util.String2Decimal(DR["Delivery_Commission_Payable"].ToString());
                    objILHPOHireDetailsView.OthersPayble = Util.String2Decimal(DR["Other_Payable"].ToString());
                    objILHPOHireDetailsView.NetAmount = Util.String2Decimal(DR["Net_Amount"].ToString());
                    objILHPOHireDetailsView.TotalPayable = objILHPOHireDetailsView.CrossingCostPayble + objILHPOHireDetailsView.DeliveryCommission + objILHPOHireDetailsView.OthersPayble;
                    objILHPOHireDetailsView.TotalTruckHireCharge = Util.String2Decimal(DR["Total_Truck_Hire_Payable"].ToString());
                    objILHPOHireDetailsView.TruckHireCharge = Util.String2Decimal(DR["Truck_Hire_Charge"].ToString());
                    objILHPOHireDetailsView.OtherCharges=Util.String2Decimal(DR["Other_Charges"].ToString());
                    objILHPOHireDetailsView.LoadingCharge = Util.String2Decimal(DR["Loading_Charges"].ToString());                                        
                    objILHPOHireDetailsView.CommitedDelDate =Convert.ToDateTime(DR["Commited_Delivery_Date"].ToString());
                    objILHPOHireDetailsView.HierarchyCode = DR["Balance_Payable_Hierarchy_Code"].ToString();
                    objILHPOHireDetailsView.MainID = Util.String2Int(DR["Balance_Payable_Main_ID"].ToString());
                    objILHPOHireDetailsView.VehicleDepartureTime = DR["Vehicle_Departure_Time"].ToString();
                    objILHPOHireDetailsView.TransitDays = Util.String2Int(DR["Transit_Days"].ToString());
                    objILHPOHireDetailsView.CommitedDelDate = Convert.ToDateTime(DR["Commited_Delivery_Date"]);
                    objILHPOHireDetailsView.Remark = DR["Remarks"].ToString();                   
                    objILHPOHireDetailsView.CharityAmount = Util.String2Decimal(DR["Charity_Amt"].ToString());
                    objILHPOHireDetailsView.TotalAfterTDSDeduction = Util.String2Decimal(DR["Total_After_TDS_Ded"].ToString());
                    objILHPOHireDetailsView.IsLHCTerminatedByReceivedCash = Util.String2Bool(DR["Is_LHC_Terminated_By_Recieving_Cash"].ToString());
                    if (objILHPOHireDetailsView.IsLHCTerminatedByReceivedCash == true)
                    {
                        objILHPOHireDetailsView.TerminatedLHCReceivedCash = Util.String2Decimal(DR["Recieved_Cash_Amount_For_Terminated_LHC"].ToString());
                    }
                    objILHPOHireDetailsView.IsLHCTerminatedByDebitToLedger = Util.String2Bool(DR["Is_LHC_Terminated_By_Debit_To_Ledger_ID"].ToString());

                    if (objILHPOHireDetailsView.IsLHCTerminatedByDebitToLedger == true)
                    {
                        objILHPOHireDetailsView.SetLedgerForTerminatedLHC(DR["Ledger_Name"].ToString(), DR["Ledger_Id"].ToString());

                    }
                    
                    objILHPOHireDetailsView.SetLoadingSupervisorID(DR["Supervisor"].ToString(), DR["Loading_Supervisor_ID"].ToString());
                    objILHPOHireDetailsView.ToLocationBranchId =Util.String2Int(objDS.Tables[1].Rows[0]["ToLocation_BranchId"].ToString());
                    objILHPOHireDetailsView.SetCharityLedger(DR["CharityLedger_Name"].ToString(), DR["CharityLedger_Id"].ToString());   
                    if(objILHPOHireDetailsView.VehicleID>0)
                    {
                        ds1=objLHPOHireDetailsModel.GetVehicleInformationOnVehicleChanged();
                        objILHPOHireDetailsView.VehicleOwner = ds1.Tables[0].Rows[0]["Owner_Name"].ToString();
                        objILHPOHireDetailsView.VehicleCapacity = Util.String2Int(ds1.Tables[0].Rows[0]["Vehicle_Capacity"].ToString());
                    }
                    
                }
            }
        }
        public void SetVehicleInfoOnVehicleChanged()
        {
            objDS = objLHPOHireDetailsModel.GetVehicleInformationOnVehicleChanged();
            if(objDS.Tables[0].Rows.Count>0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                objILHPOHireDetailsView.SetDriver1ID(DR["Driver_Name"].ToString(), DR["Driver_ID"].ToString());
                objILHPOHireDetailsView.SetDriver2ID(DR["Driver2_Name"].ToString(), DR["Driver2_ID"].ToString());
                objILHPOHireDetailsView.SetCleanerID(DR["Cleaner_Name"].ToString(), DR["Cleaner_ID"].ToString());
                objILHPOHireDetailsView.VehicleOwner = DR["Owner_Name"].ToString();
                objILHPOHireDetailsView.VehicleCapacity =Util.String2Int(DR["Vehicle_Capacity"].ToString());
                objILHPOHireDetailsView.TDSPercentage = Convert.ToDecimal(DR["TDS_Percent"]);                
            }
        }
        public void FillGrid()
        {
            objDS = objLHPOHireDetailsModel.FillGrid();
            objILHPOHireDetailsView.SessionLHPOHireDetailsGrid = objDS;
            objILHPOHireDetailsView.Bind_dg_LHPOHireDetails = objDS;
        }
        public void GetKMS()
        {
            objLHPOHireDetailsModel.GetKMS();
        }
        //public void GetTDSPercentage()
        //{
        //    objLHPOHireDetailsModel.GetTDSPercentage();
        //}
        public void GetTDSPercent()
        {
            objDS = objLHPOHireDetailsModel.GetDVLPValuesForLHPO();

            System.Web.HttpContext.Current.Session["DVLPValues"] = objDS;

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                objILHPOHireDetailsView.DVLPID = DR["DVLPID"].ToString();
                objILHPOHireDetailsView.DVLPFromBranchID = DR["FromBranchID"].ToString();
                objILHPOHireDetailsView.SetDriver1ID(DR["DriverName"].ToString(), DR["DriverID"].ToString());
                objILHPOHireDetailsView.BrokerID = Util.String2Int(DR["BrokerID"].ToString());
                objILHPOHireDetailsView.TDSCertificateToID = Util.String2Bool(DR["TDSCertificateTo"].ToString());
                objILHPOHireDetailsView.TruckHireCharge = Util.String2Decimal(DR["HireAmount"].ToString());
                objILHPOHireDetailsView.TDSPercentage = Util.String2Decimal(DR["TDSPercent"].ToString());
                objILHPOHireDetailsView.TDSAmount = Util.String2Decimal(DR["TDSAmount"].ToString());
                objILHPOHireDetailsView.Surcharge = Util.String2Decimal(DR["SurchargePercent"].ToString());
                objILHPOHireDetailsView.SurchargeAmount = Util.String2Decimal(DR["SurchargeAmount"].ToString());
                objILHPOHireDetailsView.AddlSurchargeCess = Util.String2Decimal(DR["AdditionalSurchargeCessPercent"].ToString());
                objILHPOHireDetailsView.AddlSurchargeCessAmount = Util.String2Decimal(DR["AdditionalSurchargeCessAmount"].ToString());
                objILHPOHireDetailsView.AddlEducationCess = Util.String2Decimal(DR["AdditionalEducationCessPercent"].ToString());
                objILHPOHireDetailsView.AddlEducationCessAmount = Util.String2Decimal(DR["AdditionalEducationCessAmount"].ToString());
                objILHPOHireDetailsView.TotalTDSAmount = Util.String2Decimal(DR["TotalTDSAmount"].ToString());
                objILHPOHireDetailsView.TotalTruckHireCharge = Util.String2Decimal(DR["TruckHirePayable"].ToString());
                objILHPOHireDetailsView.TotalAdvancePaid = Util.String2Decimal(DR["AdvanceAmount"].ToString());
                objILHPOHireDetailsView.BalanceAmount = Util.String2Decimal(DR["BalanceAmount"].ToString());
                objILHPOHireDetailsView.HierarchyCode = DR["BTHPayableHierarchyCode"].ToString();
                objILHPOHireDetailsView.MainID = Util.String2Int(DR["BTHPayableLocationID"].ToString());
            }
        }
        public void FillAttachedLHPONo()
        {
          objDS=objLHPOHireDetailsModel.FillAttachedLHPONo();
          objILHPOHireDetailsView.Bind_ddl_LHPONo = objDS.Tables[0];          
        }

        public DataSet FillLHPOParameters()
        {
            return objLHPOHireDetailsModel.FillLHPOParameters();
        }

        public DataSet GetFromLocationBranchId()
        {
            return objLHPOHireDetailsModel.GetFromLocationBranchId();
        }

        public DataSet GetToLocationBranchId()
        {
            return objLHPOHireDetailsModel.GetToLocationBranchId();
        }
    }
}