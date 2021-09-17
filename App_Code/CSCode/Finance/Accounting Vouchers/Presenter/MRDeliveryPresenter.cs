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
/// Summary description for MRDeliveryPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class MRDeliveryPresenter : Presenter
    {
        private IMRDeliveryView _MRDeliveryView;
        private MRDeliveryModel _MRDeliveryModel;
        private DataSet _ds = new DataSet();

        public MRDeliveryPresenter(IMRDeliveryView MRDeliveryView, bool isPostBack)
        {
            _MRDeliveryView = MRDeliveryView;
            _MRDeliveryModel = new MRDeliveryModel(_MRDeliveryView);

            base.Init(_MRDeliveryView, _MRDeliveryModel);

            if (!isPostBack)
            {
                _MRDeliveryView.MRGeneralDetailsView.MRDate = DateTime.Now;
                initvalues();
                FillDeliveryDetails();
                if (_MRDeliveryView.Document_ID == 8)
                {
                    Fill_Values();
                }               
         
                
            }
        }

        public void initvalues()
        {
            if (_MRDeliveryView.keyID > 0)
            {

                _ds = _MRDeliveryModel.Get_Details_For_Delivery();

                if (_ds.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = _ds.Tables[0].Rows[0];
                    DataRow DR1 = _ds.Tables[1].Rows[0];

                    _MRDeliveryView.MRGeneralDetailsView.MRNo = DR["MR_No_For_Print"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.MRDate = Convert.ToDateTime(DR["MR_Date"]);
                    _MRDeliveryView.MRGeneralDetailsView.GCNo = DR["GC_No"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.BookingDate = DR["GC_Date"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.BookingBranch = DR["Booking_Branch"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.DeliveryBranch = DR["Delivery_Branch"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.Consignor = DR["Consignor_Name"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.Consignee = DR["Consignee_Name"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.BookingType = DR["Booking_Type"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.PaymentType = DR["Payment_Type"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.GCAmount = DR["Sub_Total"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.ServiceTax = DR["Service_Tax"].ToString();
                    _MRDeliveryView.MRGeneralDetailsView.ServiceTaxBy = DR["Tax_Payable_By"].ToString();
                    _MRDeliveryView.TotalReceivables = Util.String2Decimal(DR["Total_MR_Amount"].ToString());
                    _MRDeliveryView.MRGeneralDetailsView.Is_MR_FirstTime = Convert.ToBoolean(DR["Is_MR_FirstTime"]);
                    _MRDeliveryView.RoundOff = Util.String2Int(DR["Round_Off"].ToString());

                    if (_MRDeliveryView.Document_ID == 3)
                    {
                        _MRDeliveryView.MRCashChequeDetailsView.CashAmount = Util.String2Decimal(DR["Cash_Amount"].ToString());
                        _MRDeliveryView.MRCashChequeDetailsView.ChequeAmount = Util.String2Decimal(DR["Cheque_Amount"].ToString());
                        _MRDeliveryView.MRCashChequeDetailsView.CashLedgerID = Util.String2Int(DR["Cash_Ledger_ID"].ToString());
                        _MRDeliveryView.Credit_Memo_ForID = 3;
                        _MRDeliveryView.MRCashChequeDetailsView.Total_ChequeAmount = Util.String2Decimal(DR["Cheque_Amount"].ToString());
                        _MRDeliveryView.ReceivedBy = Util.String2Int(DR["Recd_By"].ToString());

                        if (_MRDeliveryView.ReceivedBy == 2)
                        {
                            _MRDeliveryView.Set_DebitTo_LedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_Id"].ToString());
                            _MRDeliveryView.Set_DebitTo_BranchID(DR["Branch_Name"].ToString(), DR["Branch_Id"].ToString());

                        }

                    }
                    else
                    {
                        _MRDeliveryView.Credit_Memo_ForID = Util.String2Int(DR["Credit_Memo_for_ID"].ToString());
                        _MRDeliveryView.MRGeneralDetailsView.Is_CreditMemoOctroi_FirstTime = Convert.ToBoolean(DR["Is_CreditMemo_Octroi_FirstTime"]);

                        _MRDeliveryView.Set_DebitTo_LedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_Id"].ToString());
                        _MRDeliveryView.Set_DebitTo_BranchID(DR["Branch_Name"].ToString(), DR["Branch_Id"].ToString());
                        _MRDeliveryView.Is_Credit_For_Consignee = Util.String2Bool(DR["Is_Credit_For_Consignee"].ToString());
                        
                    }
                    if (DR["Del_Date"].ToString() != "")
                    {
                        _MRDeliveryView.DeliveryDate = Convert.ToDateTime(DR["Del_Date"]);
                    }
                    _MRDeliveryView.OctrFormCharges = Util.String2Decimal(DR["Octroi_Form_Charges"].ToString());
                    _MRDeliveryView.OctrServiceCharges = Util.String2Decimal(DR["Octroi_Service_Charges"].ToString());
                    _MRDeliveryView.GICharges = Util.String2Decimal(DR["GI_Charges"].ToString());
                    _MRDeliveryView.DetentionCharges = Util.String2Decimal(DR["Detention_Charges"].ToString());
                    _MRDeliveryView.HamaliCharges = Util.String2Decimal(DR["Hamali_Charges"].ToString());
                    _MRDeliveryView.LocalCharges = Util.String2Decimal(DR["Local_Charges"].ToString());
                    _MRDeliveryView.DemurageCharges = Util.String2Decimal(DR["Demurage_Charges"].ToString());
                    _MRDeliveryView.AdditionalCharges = Util.String2Decimal(DR["Additional_Charges"].ToString());
                    _MRDeliveryView.DiscountAmount = Util.String2Decimal(DR["Discount_Amount"].ToString());
                    _MRDeliveryView.DiscountRemark = DR["Discount_Amount_Remarks"].ToString();
                    _MRDeliveryView.DeliveryCommission = Util.String2Decimal(DR["Dly_Commision"].ToString());

                    _MRDeliveryView.SubTotal = Util.String2Decimal(DR["Tax_Payable_By"].ToString()); //sss

                    _MRDeliveryView.TaxAbate = Util.String2Decimal(DR["Tax_Abatement"].ToString());
                    _MRDeliveryView.AmountTaxable = Util.String2Decimal(DR["Amount_Taxable"].ToString());
                    _MRDeliveryView.ServiceTax = Util.String2Decimal(DR["Service_Tax_Amount"].ToString());
                    _MRDeliveryView.Service_Tax_Percent = Util.String2Decimal(DR["Service_Tax_Percent"].ToString());
                    _MRDeliveryView.RebookedCharges = Util.String2Decimal(DR["Rebooked_Charges"].ToString());
                    _MRDeliveryView.DemurageDays = Util.String2Int(DR["Demurage_Days"].ToString());
                    _MRDeliveryView.AddChrgRemark = DR["Additional_Charges_Remarks"].ToString();

                    _MRDeliveryView.Std_Octroi_Form_Charges = Util.String2Decimal(DR["Std_Octroi_Form_Charges"].ToString());
                    _MRDeliveryView.Std_Octroi_Service_Charges = Util.String2Decimal(DR["Std_Octroi_Service_Charges"].ToString());
                    _MRDeliveryView.Std_GI_Charges = Util.String2Decimal(DR["Std_GI_Charges"].ToString());
                    _MRDeliveryView.Std_Hamali_Charges = Util.String2Decimal(DR["Std_Hamali_Charges"].ToString());
                    _MRDeliveryView.Std_Demurage_Charges = Util.String2Decimal(DR["Std_Demurage_Charges"].ToString());

                    _MRDeliveryView.Set_Values_Delivery_Add_Edit(_ds);
                    _MRDeliveryView.Calculate_Grand_Total();
                    _MRDeliveryView.OctrAmount = Util.String2Decimal(DR["Octroi_Amount"].ToString());
                   

                }
            }
        }

        public void Save()
        {
            base.DBSave();
            
        }

        public DataSet Get_Delivery_Details()
        {
            return _MRDeliveryModel.Get_Details_For_Delivery();
        }

        public void Fill_Values()
        {
            _MRDeliveryView.BindMemoFor = _MRDeliveryModel.Fill_Credit_Memo_For();
               
        }
        public void FillDeliveryDetails()
        {
            _ds = _MRDeliveryModel.FillValues();
            _MRDeliveryView.SessionDeliveredTo = _ds.Tables["EC_Master_Delivery_To"];
            //_MRDeliveryView.MRDeliveryDetailsView.SessionDeliveredTo = _ds.Tables["EC_Master_Delivery_To"];
           // _MRDeliveryView.MRDeliveryDetailsView.SessionDeliveredAgainst = _ds.Tables["EC_Master_Delivery_Against"];
            _MRDeliveryView.SessionDeliveredAgainst = _ds.Tables["EC_Master_Delivery_Against"];  
        }
       

    }
}
