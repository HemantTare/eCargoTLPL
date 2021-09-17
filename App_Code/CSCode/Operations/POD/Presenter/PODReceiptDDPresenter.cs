using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Created : ANKIT CHAMPANERIYA
/// DATE : 1/12/08
/// Summary description for PODReceiptDDPresenter
/// </summary>
/// 

namespace Raj.EC.OperationPresenter
{
    public class PODReceiptDDPresenter : ClassLibraryMVP.General.Presenter
    {
        private IPODReceiptDDView _iPODReceiptDDView;
        private PODReceiptDDModel _PODReceiptDDModel;
        private DataSet objDS =new DataSet();
        private DataSet objDS1;

        public PODReceiptDDPresenter(IPODReceiptDDView iPODReceiptDDView, bool isPostBack)
        {
            _iPODReceiptDDView = iPODReceiptDDView;
            _PODReceiptDDModel = new PODReceiptDDModel(_iPODReceiptDDView);

            base.Init(_iPODReceiptDDView, _PODReceiptDDModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            objDS = _PODReceiptDDModel.ReadValues();      
            if (_iPODReceiptDDView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = objDS.Tables[0].Rows[0];
                    _iPODReceiptDDView.PODReceiptDate = Convert.ToDateTime(dr["Cover_received_date"].ToString());
                    _iPODReceiptDDView.SetGCNo(dr["gc_no"].ToString(), dr["gc_id"].ToString());
                    _iPODReceiptDDView.PODSentByView.Bind_ddl_SentBy = objDS.Tables[1];
                    _iPODReceiptDDView.PODSentByView.IsDllSentByAlreadyBinded = true;
                    _iPODReceiptDDView.PODSentByView.SentByID = Util.String2Int(dr["Received_Through_ID"].ToString());
                    _iPODReceiptDDView.PODSentByView.CourierDocketNo = dr["Courier_Docket_No"].ToString();
                    _iPODReceiptDDView.PODSentByView.CourierName = dr["Courier_Name"].ToString();
                    _iPODReceiptDDView.PODSentByView.VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                    _iPODReceiptDDView.PODSentByView.SetEmployeeId(dr["Emp_Name"].ToString(), dr["Emp_ID"].ToString());
                    _iPODReceiptDDView.Remarks = dr["Remarks"].ToString();
                    FillGCDetails();
                }
            }
        }

        public void FillGCDetails()
        {
            objDS = _PODReceiptDDModel.FillValuesGCNo();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                _iPODReceiptDDView.BookingType = objDR["Booking_Type"].ToString();
                _iPODReceiptDDView.BookingDate = objDR["booking_date"].ToString();
                _iPODReceiptDDView.BookingBranch = objDR["Booking_Branch_Name"].ToString();
                _iPODReceiptDDView.PaymentType = objDR["Payment_Type"].ToString();
                _iPODReceiptDDView.DeliveredDate = objDR["Delivery_Date"].ToString();
                _iPODReceiptDDView.DeliveredBranch = objDR["Delivery_Branch_Name"].ToString();
                _iPODReceiptDDView.DeliveredRemark = objDR["Remarks"].ToString();
            }
        }
              

        public void Save()
        {
            base.DBSave();
        }
    }
}