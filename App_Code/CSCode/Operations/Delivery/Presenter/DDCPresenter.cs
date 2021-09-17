using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

namespace Raj.EC.OperationPresenter
{
    public class DDCPresenter : Presenter
    {
        private IDDCView objIDDCView;
        private DDCModel objDDCModel;
        private DataSet objDS;
        int _vehicleid;

        public int VehicleID
        {
            set { _vehicleid = value; }
            get { return _vehicleid; }
        }

        public DDCPresenter(IDDCView DDCView, bool isPostBack, int VehicleID)
        {
            objIDDCView = DDCView;
            objDDCModel = new DDCModel(objIDDCView);
            base.Init(objIDDCView, objDDCModel);

            if (!isPostBack)
            {
                objIDDCView.DDSDate = DateTime.Now.Date;
                FillDeliveryModeValues();
                FillValues();
                initValues();
            }
        }

        public void FillDeliveryModeValues()
        {
            objDS = objDDCModel.FillDeliveryModeValues();
            objIDDCView.SessionBindDDLDeliveryMode = objDS.Tables[0];
        }

        public void FillValues()
        {
            objDS = objDDCModel.FillValues();
            if (objIDDCView.keyID > 0)
            {
                objIDDCView.BindPreDeliverySheet = objDS.Tables[0];
            }
                objIDDCView.SessionBindDDLUndelReason = objDS.Tables[1];
                objIDDCView.BindDeliveryMode = objDS.Tables[2];
                objIDDCView.SessionBindDlyAreaGrid = objDS.Tables[3];
                objIDDCView.SessionBindDlyStatus = objDS.Tables[4];
            
        }

        public void FillPDSValues()
        {
            objDS = objDDCModel.FillPDSValues();
            objIDDCView.BindPreDeliverySheet = objDS.Tables[0];
        }


        public void FillGrid()
        {
            objDS = objDDCModel.ReadValues();
            objIDDCView.SessionBindDDSGrid = objDS.Tables[0];

            if (objDS.Tables[0].Rows.Count > 0)
            {
                //objIDDCView.PDSDate = objDS.Tables[0].Rows[0]["PDS_Date"].ToString();
                //objIDDCView.DiverName = objDS.Tables[0].Rows[0]["DiverName"].ToString();
                //objIDDCView.VendorName = objDS.Tables[0].Rows[0]["VendorName"].ToString();
                //objIDDCView.DeliveryModeDescription = objDS.Tables[0].Rows[0]["Delivery_Mode_Description"].ToString();
            }
            else
            {
                objIDDCView.PDSDate = string.Empty;
                objIDDCView.DiverName = string.Empty;
                objIDDCView.VendorName = string.Empty;
                //objIDDCView.DeliveryModeDescription = string.Empty;
            }

            if (objIDDCView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];
                  
                    objIDDCView.DiverName =  objDR["DiverName"].ToString();
                    objIDDCView.VendorName = objDR["Vendor_Name"].ToString();
                    objIDDCView.DeliveryModeDescription =  objDR["Delivery_Mode_Description"].ToString(); 
                    objIDDCView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIDDCView.Total_Delivered_Articles = Util.String2Int(objDR["Total_DDC_Articles"].ToString());
                    objIDDCView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_DDC_Actual_Wt"].ToString());
                    objIDDCView.Total_Local_Tempo_Freight = Util.String2Decimal(objDR["Total_Local_Tempo_Freight"].ToString());
                    objIDDCView.Total_TempoBonus = Util.String2Decimal(objDR["Total_Bonus"].ToString());
                    objIDDCView.Total_TempoAddTempoFrt = Util.String2Decimal(objDR["Total_AddTempoFrt"].ToString());
                }
            }
        }

        private void initValues()
        {
            objDS = objDDCModel.ReadValues();
            objIDDCView.SessionBindDDSGrid = objDS.Tables[0];

            if (objDS.Tables[0].Rows.Count > 0)
            {
                if (objIDDCView.keyID < 0)
                {
                    objIDDCView.PDSDate = objDS.Tables[0].Rows[0]["PDS_Date"].ToString();
                    objIDDCView.DiverName = objDS.Tables[0].Rows[0]["DiverName"].ToString();
                    objIDDCView.VendorName = objDS.Tables[0].Rows[0]["VendorName"].ToString();
                    objIDDCView.DeliveryModeDescription = objDS.Tables[0].Rows[0]["DeliveryModeDescription"].ToString();
                }
            }
            else
            {
                objIDDCView.PDSDate = string.Empty;
                objIDDCView.DiverName = string.Empty;
                objIDDCView.VendorName = string.Empty;
                //objIDDCView.DeliveryModeDescription = string.Empty;
            }
           

            if (objIDDCView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIDDCView.DDSDate = Convert.ToDateTime(objDR["DDC_Date"].ToString());
                    objIDDCView.DDSNo = objDR["DDC_No_For_Print"].ToString();
                    objIDDCView.PDSDate = objDR["PDS_Date"].ToString();
                    objIDDCView.DiverName = objDR["DiverName"].ToString();
                    objIDDCView.VendorID = Util.String2Int(objDR["VendorID"].ToString());
                    objIDDCView.VendorName = objDR["Vendor_Name"].ToString();
                    objIDDCView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIDDCView.DeliveryModeID = Util.String2Int(objDR["Delivery_Mode_ID"].ToString());
                    objIDDCView.DeliveryModeDescription = objDR["Delivery_Mode_Description"].ToString();
                    objIDDCView.SetSupervisor(objDR["Godown_Supervisor"].ToString(), objDR["Godown_Supervisor_ID"].ToString());
                    objIDDCView.Total_ChequeAmt = Util.String2Decimal(objDR["Total_Chq_Amount"].ToString());
                    objIDDCView.Total_CreditAmt = Util.String2Decimal(objDR["Total_Credit_Amount"].ToString());
                    objIDDCView.Total_GC_Amount = Util.String2Decimal(objDR["Total_Cash_Amount"].ToString());
                    objIDDCView.Total_Cash_Received = Util.String2Decimal(objDR["Cash_Received"].ToString());
                    objIDDCView.Total_Cash_Balance = Util.String2Decimal(objDR["Cash_Balance"].ToString());
                    objIDDCView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIDDCView.Total_Delivered_Articles = Util.String2Int(objDR["Total_DDC_Articles"].ToString());
                    objIDDCView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_DDC_Actual_Wt"].ToString());
                    objIDDCView.Total_Local_Tempo_Freight = Util.String2Decimal(objDR["Total_Local_Tempo_Freight"].ToString());
                    objIDDCView.Total_TempoBonus = Util.String2Decimal(objDR["Total_Bonus"].ToString());
                    objIDDCView.Total_TempoAddTempoFrt = Util.String2Decimal(objDR["Total_AddTempoFrt"].ToString());
                    objIDDCView.Remarks = objDR["Remarks"].ToString();
                    objIDDCView.Total_MobilePay = Util.String2Decimal(objDR["Total_MobilePay"].ToString());
                    objIDDCView.Total_SwipeCard = Util.String2Decimal(objDR["Total_SwipeCard"].ToString());
                    objIDDCView.Total_PendingFreight = Util.String2Decimal(objDR["Total_PendingFreight"].ToString());
                }
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}