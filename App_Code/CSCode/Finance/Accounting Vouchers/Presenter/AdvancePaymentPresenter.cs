using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using Raj.EC;

namespace Raj.EC.FinancePresenter
{
    public class AdvancePaymentPresenter : Presenter
    {
        private IAdvancePaymentView objIAdvancePaymentView;
        private AdvancePaymentModel objAdvancePaymentModel;
        private DataSet objDS;
        //private bool _IsVT = (bool)Param.getUserParam().Is_VT;
        private int _Division_Id = 1;// (int)UserManager.getUserParam().DivisionId;

        public AdvancePaymentPresenter(IAdvancePaymentView AdvancePaymentView, bool IsPostBack)
        {
            objIAdvancePaymentView = AdvancePaymentView;
            objAdvancePaymentModel = new AdvancePaymentModel(objIAdvancePaymentView);
            base.Init(objIAdvancePaymentView, objAdvancePaymentModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            if (objIAdvancePaymentView.keyID > 0)
            {
                readValues();
            }
        }

        private void fillValues()
        {
            objDS = objAdvancePaymentModel.FillValues();
            objIAdvancePaymentView.bind_ddl_TDSLedger = objDS.Tables[0];
            objIAdvancePaymentView.bind_ddl_CashBankLedger = objDS.Tables[1];
        }

        private void readValues()
        {
            objDS = objAdvancePaymentModel.ReadValues();
            DataRow Dr = objDS.Tables[0].Rows[0];

            //objIAdvancePaymentView.PaymentNo = Dr["Ticket_No"].ToString();
            //objIAdvancePaymentView.PaymentDate = Convert.ToDateTime(Dr["Ticket_Date"]);
            //objIAdvancePaymentView.RefNo = Dr["UnDelievered_Reason"].ToString();
            //objIAdvancePaymentView.CashBankLedgerId = Dr["AdvancePayment_Description"].ToString();
            //objIAdvancePaymentView.ChequeNo = Dr["Name"].ToString();
            //objIAdvancePaymentView.TDSLedgerId = Dr["Telephone_No"].ToString();
            //objIAdvancePaymentView.RefNoPartyLedger = Dr["Mobile_No"].ToString();
            //objIAdvancePaymentView.Amount = Dr["Designation"].ToString();
            //objIAdvancePaymentView.SetPartyLedgerId(Dr["Email"].ToString(),"");
            //objIAdvancePaymentView.RefNoTDSLedger = Util.String2Int(Dr["AdvancePayment_By"].ToString());
            //objIAdvancePaymentView.Narration = Util.String2Int(Dr["AdvancePayment_Nature_Id"].ToString());
        }



        public void Save()
        {
            base.DBSave();
            //objAdvancePaymentModel.Save();
        }

        public bool ValidateRefNo(string ledgerNo, int ledgerId)
        {
            return objAdvancePaymentModel.ValidateRefNo(ledgerNo, ledgerId);
        }

    }
}