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
    public class TruckBharaiPresenter : Presenter
    {
        private ITruckBharaiView objITruckBharaiView;
        private TruckBharaiModel objTruckBharaiModel;
        private DataSet objDS;

        public TruckBharaiPresenter(ITruckBharaiView TruckBharaiView, bool isPostBack)
        {
            objITruckBharaiView = TruckBharaiView;
            objTruckBharaiModel = new TruckBharaiModel(objITruckBharaiView);
            base.Init(objITruckBharaiView, objTruckBharaiModel);

            if (!isPostBack)
            {
                objITruckBharaiView.TransactionDate = DateTime.Now.Date; 
                initValues(); 
            }
        }

        private void initValues()
        {
            objDS = objTruckBharaiModel.ReadValues();
            
            if (objITruckBharaiView.keyID > 0)
            {
                objITruckBharaiView.SessionBindSelectedMemoGrid = objDS.Tables[0];
            }
            else
            {
                objITruckBharaiView.SessionBindTruckBharaiGrid = objDS.Tables[0];
            }
            if (objITruckBharaiView.keyID > 0)
            {  
                    DataRow objDR;
                    if (objDS.Tables[1].Rows.Count > 0)
                    {
                        objDR = objDS.Tables[1].Rows[0];
                    }
                    else
                    { return; }
                    objITruckBharaiView.TransactionDate = Convert.ToDateTime(objDR["Transaction_Date"].ToString());
                    objITruckBharaiView.TransactionNo = objDR["Transaction_No_For_Print"].ToString();

                    objITruckBharaiView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());

                    objITruckBharaiView.SetSuperviserId(objDR["Loaded_By_Name"].ToString(), objDR["Loaded_By_Id"].ToString());
                    objITruckBharaiView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objITruckBharaiView.Total_SelectedMemo = objDS.Tables[1].Rows.Count;       
                    objITruckBharaiView.Total_Hamali_Charges = Util.String2Decimal(objDR["Total_Hamali_LR"].ToString());
                    objITruckBharaiView.Total_Hamali_Paid = Util.String2Decimal(objDR["Total_Hamali_Paid"].ToString());
                    objITruckBharaiView.Remarks = objDR["Remarks"].ToString();
                
            }
        } 

        public void fillgrid()
        {
            objDS = objTruckBharaiModel.ReadValues();
            objITruckBharaiView.SessionBindTruckBharaiGrid = objDS.Tables[0];
            if (objITruckBharaiView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objITruckBharaiView.SetSuperviserId(objDR["Loaded_By_Name"].ToString(), objDR["Loaded_By_Id"].ToString());
                    objITruckBharaiView.Total_No_Of_GC = Util.String2Int(objDR["Total_TruckBharai_GC"].ToString());

                    objITruckBharaiView.Remarks = objDR["Remarks"].ToString();
                }
            }
        } 

        public void fillSelectedMemoGrid()
        {
            objDS = objTruckBharaiModel.FillSelectedMemoValues();
            objITruckBharaiView.SessionBindSelectedMemoGrid = objDS.Tables[0]; 
        } 

        public void CheckVehicle()
        {
            objDS = objTruckBharaiModel.CheckVehicleStr();
            DataRow objDR;
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objDR = objDS.Tables[0].Rows[0];
                objITruckBharaiView.strValidate_Vehicle = objDR["strMessage"].ToString();
            }
            else
            {
                objITruckBharaiView.strValidate_Vehicle = "";
            }
        }

        public void save()
        {
            base.DBSave(); 
        }
    }
}