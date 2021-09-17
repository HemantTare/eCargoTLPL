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
/// Summary description for MRGeneralDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class MRGeneralDetailsPresenter : Presenter
    {
        private IMRGeneralDetailsView _MRGeneralDetailsView;
        private MRGeneralDetailsModel _MRGeneralDetailsModel;
        private DataSet _ds = new DataSet();

        public MRGeneralDetailsPresenter(IMRGeneralDetailsView MRGeneralDetailsView, bool isPostBack)
        {
            _MRGeneralDetailsView = MRGeneralDetailsView;
            _MRGeneralDetailsModel = new MRGeneralDetailsModel(_MRGeneralDetailsView);

            base.Init(_MRGeneralDetailsView, _MRGeneralDetailsModel);            
        }       

        public void Get_GC_Details()
        {
            _ds = _MRGeneralDetailsModel.ReadValues();

            if (_ds.Tables[0].Rows.Count > 0)
            {
                _MRGeneralDetailsView.errorMessage = "";

                _MRGeneralDetailsView.GC_ID = Util.String2Int(_ds.Tables[0].Rows[0]["GC_ID"].ToString());
                _MRGeneralDetailsView.BookingDate = _ds.Tables[0].Rows[0]["GC_Date"].ToString();
                _MRGeneralDetailsView.BookingBranch = _ds.Tables[0].Rows[0]["Booking_Branch"].ToString();
                _MRGeneralDetailsView.DeliveryBranch = _ds.Tables[0].Rows[0]["Delivery_Branch"].ToString();
                _MRGeneralDetailsView.Consignor = _ds.Tables[0].Rows[0]["Consignor_Name"].ToString();
                _MRGeneralDetailsView.Consignee = _ds.Tables[0].Rows[0]["Consignee_Name"].ToString();
                _MRGeneralDetailsView.BookingType = _ds.Tables[0].Rows[0]["Booking_Type"].ToString();
                _MRGeneralDetailsView.PaymentType = _ds.Tables[0].Rows[0]["Payment_Type"].ToString();
                _MRGeneralDetailsView.GCAmount = _ds.Tables[0].Rows[0]["Sub_Total"].ToString();
                _MRGeneralDetailsView.ServiceTax = _ds.Tables[0].Rows[0]["Service_Tax_Amount"].ToString();
                _MRGeneralDetailsView.Total_Receivables = Util.String2Decimal(_ds.Tables[0].Rows[0]["Total_GC_Amount"].ToString());

                _MRGeneralDetailsView.ServiceTaxBy = _ds.Tables[0].Rows[0]["Tax_Payable_By"].ToString();
                _MRGeneralDetailsView.Is_MR_FirstTime = Convert.ToBoolean(_ds.Tables[0].Rows[0]["Is_MR_FirstTime"]);
                _MRGeneralDetailsView.Is_CreditMemoOctroi_FirstTime = Convert.ToBoolean(_ds.Tables[0].Rows[0]["Is_CreditMemo_Octroi_FirstTime"]);
                //_MRGeneralDetailsView.RoundOff = Util.String2Int(_ds.Tables[0].Rows[0]["RoundOff"].ToString());
            }
            else
            {
                _MRGeneralDetailsView.errorMessage = CompanyManager.getCompanyParam().GcCaption +" No Not Found";
                _MRGeneralDetailsView.GC_ID = 0;
                _MRGeneralDetailsView.BookingDate = "";
                _MRGeneralDetailsView.BookingBranch = "";
                _MRGeneralDetailsView.DeliveryBranch = "";
                _MRGeneralDetailsView.Consignor = "";
                _MRGeneralDetailsView.Consignee = "";
                _MRGeneralDetailsView.BookingType = "";
                _MRGeneralDetailsView.PaymentType = "";
                _MRGeneralDetailsView.GCAmount = "";
                _MRGeneralDetailsView.ServiceTax = "";
                _MRGeneralDetailsView.ServiceTaxBy = "";
                _MRGeneralDetailsView.Total_Receivables = 0;
            }
        }
    }
}