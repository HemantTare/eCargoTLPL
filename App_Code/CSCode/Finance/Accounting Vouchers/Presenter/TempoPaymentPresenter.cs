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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
namespace Raj.EC.FinancePresenter
{
    public class TempoPaymentPresenter : Presenter
    {
    
        private ITempoPaymentView objITempoPaymentView;
        private TempoPaymentModel objTempoPaymentModel;
        private DataSet objDS;
        public TempoPaymentPresenter(ITempoPaymentView TempoPaymentView, bool IsPostBack)
        {
            objITempoPaymentView = TempoPaymentView;
            objTempoPaymentModel = new TempoPaymentModel(objITempoPaymentView);
            base.Init(objITempoPaymentView, objTempoPaymentModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        public void initValues()
        {
            objITempoPaymentView.ChequeDate = DateTime.Now.Date;
            fillValues(); 
            readValues();             
        }


        private void fillValues()
        {   
        }


        public void GetVendorLedgerDetails(int Vehicle_Id)
        {
            objDS = objTempoPaymentModel.GetVendorLedgerDetails(Vehicle_Id);

            if (objDS.Tables[0].Rows.Count > 0)

            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objITempoPaymentView.SetLedgerId(objDR["Ledger_Id"].ToString(), objDR["Ledger_Name"].ToString());

                objITempoPaymentView.ClosingBalance = Convert.ToInt32(objDR["ClosingBalance"].ToString());
                objITempoPaymentView.ClosingBalanceText = Convert.ToString(objDR["ClosingBalanceText"].ToString());
                objITempoPaymentView.DebitBalLimmit = Convert.ToInt32(objDR["DebitBalLimmit"].ToString());
            }
            else
            {
                objITempoPaymentView.ClosingBalance = 0;
                objITempoPaymentView.ClosingBalanceText = "0";
                objITempoPaymentView.DebitBalLimmit = 0;

            }


        }

        public void readValues()
        {
            objDS = objTempoPaymentModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objITempoPaymentView.VoucherDate  = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
                objITempoPaymentView.RefNo  = objDR["Ref_No"].ToString();
                objITempoPaymentView.PaidToWhom  = objDR["Paid_To"].ToString();
                objITempoPaymentView.Details  = objDR["Narration"].ToString();
                objITempoPaymentView.Amount  = Convert.ToDecimal(objDR["Amount"].ToString());
                objITempoPaymentView.ChequeNo = objDR["Cheque_No"].ToString();
                objITempoPaymentView.ChequeDate  = Convert.ToDateTime(objDR["Cheque_Date"].ToString());
                objITempoPaymentView.Is_CashCheque  = Convert.ToInt32  (objDR["IsCheque"].ToString());

                objITempoPaymentView.SetLedgerId(objDR["Ledger_Id"].ToString(), objDR["Ledger_Name"].ToString());



            }


            objDS = objTempoPaymentModel.FillDetails();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objITempoPaymentView.ClosingBalance = Convert.ToInt32(objDR["ClosingBalance"].ToString());
                objITempoPaymentView.ClosingBalanceText = Convert.ToString(objDR["ClosingBalanceText"].ToString());
                objITempoPaymentView.DebitBalLimmit = Convert.ToInt32(objDR["DebitBalLimmit"].ToString());
            }
            else
            {
                objITempoPaymentView.ClosingBalance = 0;
                objITempoPaymentView.ClosingBalanceText = "0";
                objITempoPaymentView.DebitBalLimmit = 0;

            }
        }
        
        public void Save()
        {
           base.DBSave();
          
        }


  
    }
}
