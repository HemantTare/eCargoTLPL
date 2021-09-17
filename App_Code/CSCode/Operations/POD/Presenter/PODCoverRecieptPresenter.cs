using System;
using System.Data;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for PODCoverRecieptPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class PODCoverRecieptPresenter : Presenter
    {
        private IPODCoverRecieptView objIPODCoverRecieptView;
        private PODCoverRecieptModel objPODCoverRecieptModel;
        private DataSet objDS;
        DataSet ds = new DataSet();

        public PODCoverRecieptPresenter(IPODCoverRecieptView pODCoverRecieptView, bool isPostBack)
        {
            objIPODCoverRecieptView = pODCoverRecieptView;
            objPODCoverRecieptModel = new PODCoverRecieptModel(objIPODCoverRecieptView);
            base.Init(objIPODCoverRecieptView, objPODCoverRecieptModel);

            if (!isPostBack)
            {
                objIPODCoverRecieptView.ReceiptDate = DateTime.Now;
                initValues();
            }
        }

        public void FillCoverNo()
        {
            objDS = objPODCoverRecieptModel.FillValues();
            objIPODCoverRecieptView.Bind_ddl_CoverNo = objDS.Tables[0];
        }

        public void fillgrid()
        {
            objDS = objPODCoverRecieptModel.ReadValues();
            objIPODCoverRecieptView.SessionPODCoverReciept = objDS.Tables[0];

            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow dr = objDS.Tables[1].Rows[0];
                objIPODCoverRecieptView.SetCoverDate = dr["Cover_Date"].ToString();
                objIPODCoverRecieptView.SentHierachy = dr["Hierarchy_Name"].ToString();
                objIPODCoverRecieptView.HierachyCode = dr["Cover_Send_Hierarchy_Code"].ToString();
                objIPODCoverRecieptView.MainId = Util.String2Int(dr["Cover_Sent_Main_ID"].ToString());
                objIPODCoverRecieptView.SentLocation = dr["Sent_Location"].ToString();

                objIPODCoverRecieptView.PODSentByView.SentByID = Util.String2Int(dr["Cover_Sent_Type_ID"].ToString());
                objIPODCoverRecieptView.PODSentByView.CourierDocketNo = dr["Courier_Docket_No"].ToString();
                objIPODCoverRecieptView.PODSentByView.CourierName = dr["Courier_Name"].ToString();
                objIPODCoverRecieptView.PODSentByView.VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                objIPODCoverRecieptView.PODSentByView.SetEmployeeId(dr["Emp_Name"].ToString(), dr["Emp_ID"].ToString());
            }
        }

        private void initValues()
        {
            FillCoverNo();

            objDS = objPODCoverRecieptModel.ReadValues();
            objIPODCoverRecieptView.SessionPODCoverReciept = objDS.Tables[0];

            if (objIPODCoverRecieptView.keyID > 0)
            {
                if(objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = objDS.Tables[1].Rows[0];
                    objIPODCoverRecieptView.ReceiptNo = dr["Cover_Received_No_For_Print"].ToString();
                    objIPODCoverRecieptView.ReceiptDate = Convert.ToDateTime(dr["Cover_Received_Date"].ToString());
                    objIPODCoverRecieptView.CoverNo = Util.String2Int(dr["Cover_ID"].ToString());
                    objIPODCoverRecieptView.SetCoverDate = dr["Cover_Date"].ToString();
                    objIPODCoverRecieptView.SentHierachy = dr["Hierarchy_Name"].ToString();
                    objIPODCoverRecieptView.HierachyCode = dr["Cover_Send_Hierarchy_Code"].ToString();
                    objIPODCoverRecieptView.MainId = Util.String2Int(dr["Cover_Sent_Main_ID"].ToString());
                    objIPODCoverRecieptView.SentLocation = dr["Sent_Location"].ToString();
                    objIPODCoverRecieptView.Remark = dr["Remarks"].ToString();

                    objIPODCoverRecieptView.PODSentByView.SentByID = Util.String2Int(dr["Received_Through_ID"].ToString());
                    objIPODCoverRecieptView.PODSentByView.CourierDocketNo = dr["Courier_Docket_No"].ToString();
                    objIPODCoverRecieptView.PODSentByView.CourierName = dr["Courier_Name"].ToString();
                    objIPODCoverRecieptView.PODSentByView.VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                    objIPODCoverRecieptView.PODSentByView.SetEmployeeId(dr["Emp_Name"].ToString(), dr["Emp_ID"].ToString());
                }
            }
        }            

        public void Save()
        {
            base.DBSave();
        } 
    }
}