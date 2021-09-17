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
/// Summary description for MRBookingDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class MRBookingDetailsPresenter:Presenter
    {
        private IMRBookingDetailsView _MRBookingDetailsView;
        private MRBookingDetailsModel _MRBookingDetailsModel;
        private DataSet _ds = new DataSet();

        public MRBookingDetailsPresenter(IMRBookingDetailsView MRBookingDetailsView,bool isPostBack)
        {
            _MRBookingDetailsView = MRBookingDetailsView;
            _MRBookingDetailsModel = new MRBookingDetailsModel(_MRBookingDetailsView);

            base.Init(_MRBookingDetailsView, _MRBookingDetailsModel);

            if (!isPostBack)
            {
                _MRBookingDetailsView.MRGeneralDetailsView.MRDate = DateTime.Now;

                initvalues();
            }            
        }

        public void initvalues()
        {
            if (_MRBookingDetailsView.keyID > 0)
            {
            
                _ds = _MRBookingDetailsModel.ReadValues();

                if (_ds.Tables[0].Rows.Count > 0)
                {
                    _MRBookingDetailsView.MRGeneralDetailsView.MRNo = _ds.Tables[0].Rows[0]["MR_No_For_Print"].ToString();
                    //_MRBookingDetailsView.MRGeneralDetailsView.Next_No = Util.String2Int(_ds.Tables[0].Rows[0]["MR_No"].ToString());
                    //_MRBookingDetailsView.MRGeneralDetailsView.Padded_Next_No = _ds.Tables[0].Rows[0]["MR_No_For_Print"].ToString();
                    //_MRBookingDetailsView.MRGeneralDetailsView.Document_Allocation_ID = Util.String2Int(_ds.Tables[0].Rows[0]["Document_Series_Allocation_ID"].ToString());

                    _MRBookingDetailsView.MRGeneralDetailsView.MRDate = Convert.ToDateTime(_ds.Tables[0].Rows[0]["MR_Date"]);
                    _MRBookingDetailsView.MRGeneralDetailsView.GCNo = _ds.Tables[0].Rows[0]["GC_No"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.BookingDate = _ds.Tables[0].Rows[0]["GC_Date"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.BookingBranch = _ds.Tables[0].Rows[0]["Booking_Branch"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.DeliveryBranch = _ds.Tables[0].Rows[0]["Delivery_Branch"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.Consignor = _ds.Tables[0].Rows[0]["Consignor_Name"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.Consignee = _ds.Tables[0].Rows[0]["Consignee_Name"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.BookingType = _ds.Tables[0].Rows[0]["Booking_Type"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.PaymentType = _ds.Tables[0].Rows[0]["Payment_Type"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.GCAmount = _ds.Tables[0].Rows[0]["GC_Sub_Total"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.ServiceTax = _ds.Tables[0].Rows[0]["Service_Tax_Amount"].ToString();
                    _MRBookingDetailsView.MRGeneralDetailsView.ServiceTaxBy = _ds.Tables[0].Rows[0]["Tax_Payable_By"].ToString();
                    _MRBookingDetailsView.TotalReceivables = Util.String2Decimal(_ds.Tables[0].Rows[0]["Total_GC_Amount"].ToString());
                    _MRBookingDetailsView.MRCashChequeDetailsView.CashAmount = Util.String2Decimal(_ds.Tables[0].Rows[0]["Cash_Amount"].ToString());
                    _MRBookingDetailsView.MRCashChequeDetailsView.ChequeAmount = Util.String2Decimal(_ds.Tables[0].Rows[0]["Cheque_Amount"].ToString());
                    _MRBookingDetailsView.MRCashChequeDetailsView.CashLedgerID = Util.String2Int(_ds.Tables[0].Rows[0]["Cash_Ledger_ID"].ToString());

                    _MRBookingDetailsView.MRGeneralDetailsView.GC_ID = Util.String2Int(_ds.Tables[0].Rows[0]["GC_ID"].ToString());
                    _MRBookingDetailsView.MRGeneralDetailsView.Total_Receivables = Util.String2Decimal(_ds.Tables[0].Rows[0]["Total_GC_Amount"].ToString());
                    _MRBookingDetailsView.MRCashChequeDetailsView.Total_ChequeAmount = Util.String2Decimal(_ds.Tables[0].Rows[0]["Cheque_Amount"].ToString());
                    
                }            
            }        
        }

        public void Save()
        {
            base.DBSave();
        }      
    }
}
