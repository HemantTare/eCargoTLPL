using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for PODCoverGenerationPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class PODCoverGenerationPresenter : Presenter
    {
        private IPODCoverGenerationView objIPODCoverGenerationView;
        private PODCoverGenerationModel objPODCoverGenerationModel;
        private DataSet objDS;

        public PODCoverGenerationPresenter(IPODCoverGenerationView PODCoverGenerationView, bool isPostBack)
        {
            objIPODCoverGenerationView = PODCoverGenerationView;
            objPODCoverGenerationModel = new PODCoverGenerationModel(objIPODCoverGenerationView);
            base.Init(objIPODCoverGenerationView, objPODCoverGenerationModel);

            if (!isPostBack)
            {
                objIPODCoverGenerationView.CoverDate = DateTime.Now.Date;
                initValues();
            }
        }
             
        public void fillgrid()
        {
            objDS = objPODCoverGenerationModel.ReadValues();
            objIPODCoverGenerationView.SessionBindPODCoverGrid = objDS.Tables[0];           
        }

        public void initValues()
        {
            objDS = objPODCoverGenerationModel.ReadValues();
            objIPODCoverGenerationView.SessionBindPODCoverGrid = objDS.Tables[0];

            if (objIPODCoverGenerationView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIPODCoverGenerationView.CoverDate = Convert.ToDateTime(objDR["Cover_Date"].ToString());
                    objIPODCoverGenerationView.CoverNo = objDR["Cover_No_For_Print"].ToString();
                    objIPODCoverGenerationView.PODSentByView.SentByID = Util.String2Int(objDR["Cover_Sent_Type_ID"].ToString());
                    objIPODCoverGenerationView.PODSentByView.CourierDocketNo = objDR["Courier_Docket_No"].ToString();
                    objIPODCoverGenerationView.PODSentByView.CourierName = objDR["Courier_Name"].ToString();
                    objIPODCoverGenerationView.PODSentByView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIPODCoverGenerationView.PODSentByView.SetEmployeeId(objDR["Emp_Name"].ToString(), objDR["Emp_ID"].ToString());
                    objIPODCoverGenerationView.CoverSendHierarchyCode = objDR["Cover_Send_Hierarchy_Code"].ToString();
                    objIPODCoverGenerationView.CoverSentMainID = Util.String2Int(objDR["Cover_Sent_Main_ID"].ToString());
                    objIPODCoverGenerationView.Total_GC = Util.String2Int(objDR["Total_GC"].ToString());
                    objIPODCoverGenerationView.Remarks = objDR["Remarks"].ToString();
                }
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}