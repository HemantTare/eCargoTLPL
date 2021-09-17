using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Summary description for WrongDeliveryPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class WrongDeliveryPresenter : Presenter
    {
        private IWrongDeliveryView _WrongDeliveryView;
        private WrongDeliveryModel _WrongDeliveryModel;
        private DataSet _ds = new DataSet();

        public WrongDeliveryPresenter(IWrongDeliveryView WrongDeliveryView, bool isPostBack)
        {
            _WrongDeliveryView = WrongDeliveryView;
            _WrongDeliveryModel = new WrongDeliveryModel(_WrongDeliveryView);

            base.Init(_WrongDeliveryView, _WrongDeliveryModel);
            if (!isPostBack)
            {
                _WrongDeliveryView.ChequeDate = DateTime.Now.Date;
                FillValues(); 
            }
        }
        public void FillValues()
        {
            _ds = _WrongDeliveryModel.FillValues();
            _WrongDeliveryView.BindReceived_Condition = _ds.Tables[0]; 
        }

        public void Get_GC_Details()
        {
            _ds = _WrongDeliveryModel.ReadValues();

            if (_ds.Tables[0].Rows.Count > 0)
            {
                _WrongDeliveryView.errorMessage = "";

                _WrongDeliveryView.GC_ID = Util.String2Int(_ds.Tables[0].Rows[0]["GC_ID"].ToString());

                _WrongDeliveryView.Booking_Branch_ID = Convert.ToInt32(_ds.Tables[0].Rows[0]["Booking_Branch_ID"].ToString());
                _WrongDeliveryView.BookingBranch = _ds.Tables[0].Rows[0]["Booking_Branch"].ToString();
                _WrongDeliveryView.Delivery_Branch_Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Delivery_Branch_Id"].ToString());
                _WrongDeliveryView.DeliveryBranch = _ds.Tables[0].Rows[0]["Delivery_Branch"].ToString();
                _WrongDeliveryView.IsToPay = Convert.ToBoolean(_ds.Tables[0].Rows[0]["IsToPay"]);
                
            }
            else
            {
                _WrongDeliveryView.errorMessage = CompanyManager.getCompanyParam().GcCaption +" No Not Found";
                _WrongDeliveryView.GC_ID = 0;
                
                _WrongDeliveryView.BookingBranch = "";
                _WrongDeliveryView.DeliveryBranch = "";
                
            }
        }
        public void save()
        {
            base.DBSave();
        }
    }
}