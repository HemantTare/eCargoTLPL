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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

namespace Raj.EC.OperationPresenter
{
    public class PDSPresenter : Presenter
    {
        private IPDSView objIPDSView;
        private PDSModel objPDSModel;
        private DataSet objDS;

        public PDSPresenter(IPDSView PDSView, bool isPostBack)
        {
            objIPDSView = PDSView;
            objPDSModel = new PDSModel(objIPDSView);
            base.Init(objIPDSView, objPDSModel);

            if (!isPostBack)
            {
                objIPDSView.PDSDate = DateTime.Now.Date;
                FillValues();
                initValues();
            }
        }

        public void FillValues()
        {
            objDS = objPDSModel.FillValues();
            objIPDSView.BindDeliveryMode = objDS.Tables[0];
            objIPDSView.BindAssociates = objDS.Tables[1];
        }
        public bool ValidateConsigneeClient()
        {
            bool Is_ValidateConsigneeList = objPDSModel.ValidateConsigneeClient();
            return Is_ValidateConsigneeList;
        }

        public void fillgrid()
        {
            objDS = objPDSModel.ReadValues();
            objIPDSView.SessionBindPDSGrid = objDS.Tables[0];
            if (objIPDSView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIPDSView.SetSuperviserId(objDR["Supervisor_Name"].ToString(), objDR["Supervisor_ID"].ToString());
                    objIPDSView.Total_No_Of_GC = Util.String2Int(objDR["Total_PDS_GC"].ToString());
                    objIPDSView.Total_Bal_Articles = Util.String2Int(objDR["Total_Balance_Articles"].ToString());
                    objIPDSView.Total_Bal_Weight = Util.String2Decimal(objDR["Total_Balance_Actual_Wt"].ToString());
                    objIPDSView.Total_Delivered_Articles = Util.String2Int(objDR["Total_PDS_Articles"].ToString());
                    objIPDSView.Total_GC_Amount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                    objIPDSView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_PDS_Actual_Wt"].ToString());

                    objIPDSView.DeliveryModeDescription = objDR["Delivery_Mode_Description"].ToString();
                    objIPDSView.Remarks = objDR["Remarks"].ToString();
                }
            }
        }

        private void initValues()
        {
            objDS = objPDSModel.ReadValues();
            objIPDSView.SessionBindPDSGrid = objDS.Tables[0];

            if (objIPDSView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR; 
                    if (objDS.Tables[1].Rows.Count > 0)
                    {
                        objDR = objDS.Tables[1].Rows[0];
                    }
                    else
                    { return; }
                    objIPDSView.PDSDate = Convert.ToDateTime(objDR["PDS_Date"].ToString());
                    objIPDSView.PDSNo = objDR["PDS_No_For_Print"].ToString();
                    objIPDSView.DiverName = objDR["DiverName"].ToString();
                    objIPDSView.VendorName = objDR["Vendor_Name"].ToString();
                    objIPDSView.VendorID = Util.String2Int(objDR["VendorID"].ToString());
                    objIPDSView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIPDSView.MobileNumber = objDR["MobileNo"].ToString();
                    objIPDSView.DeliveryModeID = Util.String2Int(objDR["Delivery_Mode_ID"].ToString());

                    objIPDSView.SetSuperviserId(objDR["Supervisor_Name"].ToString(), objDR["Supervisor_ID"].ToString());
                    objIPDSView.Total_No_Of_GC = Util.String2Int(objDR["Total_PDS_GC"].ToString());
                    objIPDSView.Total_Bal_Articles = Util.String2Int(objDR["Total_Balance_Articles"].ToString());
                    objIPDSView.Total_Bal_Weight = Util.String2Decimal(objDR["Total_Balance_Actual_Wt"].ToString());
                    objIPDSView.Total_Delivered_Articles = Util.String2Int(objDR["Total_PDS_Articles"].ToString());
                    objIPDSView.Total_GC_Amount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                    objIPDSView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_PDS_Actual_Wt"].ToString());

                    objIPDSView.DeliveryModeDescription = objDR["Delivery_Mode_Description"].ToString();
                    objIPDSView.Remarks = objDR["Remarks"].ToString();
                }
            }
        }

        public void CheckVehicle()
        {
            objDS = objPDSModel.CheckVehicleStr();
            DataRow objDR;
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objDR = objDS.Tables[0].Rows[0];
                objIPDSView.strValidate_Vehicle = objDR["strMessage"].ToString();
            }
            else
            {
                objIPDSView.strValidate_Vehicle = "";
            }
        }

        public void save()
        {
            base.DBSave();
            //objPDSModel.Save();
        }
    }
}