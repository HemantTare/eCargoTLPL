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
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Author       : Anita Gupta
/// Description  : Truck Arrival System Presenter
/// Date         : 16 Jan 09 
/// </summary>
/// 

namespace Raj.EC.OperationPresenter
{
    public class TASPresenter : ClassLibraryMVP.General.Presenter
    {
        private ITASView objTASView;
        private TASModel objTASModel;
        private DataSet objDS;

        public TASPresenter(ITASView TASView, bool isPostback)
        {
            objTASView = TASView;
            objTASModel = new TASModel(objTASView);

            base.Init(objTASView, objTASModel);

            if (!isPostback)
            {
                objTASView.TAS_Date = DateTime.Now.Date;
                initValues();
            }
        }

        public void Save()
        {
            //DBSave();
            objTASModel.Save();
        }

        private void fillValues()
        {
            objDS = objTASModel.FillValues();

            objTASView.BindReasionForLateTruckArrival = objDS.Tables[0];            
        }

        private void initValues()
        {
            fillValues();

            ReadValues();
        }

        public void ReadValues()
        {
            objDS = objTASModel.ReadValues();


            Object sender = "";
            System.EventArgs e = null;

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objTASView.TASNo = objDR["TAS_No_For_Print"].ToString();
                objTASView.TAS_Date = Convert.ToDateTime(objDR["TAS_Date"].ToString());
                objTASView.VehicleSearchView.VehicleID = Util.String2Int(objDR["Vehicle_Id"].ToString());
                objTASView.Vehicle_Id = Util.String2Int(objDR["Vehicle_Id"].ToString());
                
                objTASView.SetLHPO(objDR["LHPO_No_For_Print"].ToString(), objDR["LHPO_ID"].ToString());
                objTASView.LHPO_Date = objDR["LHPO_Date"].ToString();                   
             
                objTASView.ScheduledArrivalDate = objDR["Scheduled_Arrival_Date"].ToString();
                objTASView.ScheduledArrivalTime = objDR["Scheduled_Arrival_Time"].ToString();
                objTASView.LHPOFromLocation = objDR["From_Location"].ToString();
                objTASView.LHPOToLocation = objDR["To_Location"].ToString();
                
                objTASView.Reason_For_Late_Arrival = Util.String2Int(objDR["Reason_For_Late_Arrival_ID"].ToString());
                objTASView.Remarks = objDR["Remarks"].ToString();
            }

            objTASView.Bind_dg_TASDetails = objDS.Tables[1];       

            Get_VehicleDetails(sender, e);
        }

        public void Get_VehicleDetails(object sender, EventArgs e)
        {

            objTASModel.Get_VehicleDetails();

        }

        public void Get_LHPO(object sender, EventArgs e)
        {

            objTASView.BindLHPO = objTASModel.Get_LHPO();

        }

        public void Get_LHPODetails(object sender, EventArgs e)
        {
            objDS = objTASModel.Get_LHPODetails();

            objTASView.Bind_dg_TASDetails = objDS.Tables[0];           
        }        
    }
    
}
