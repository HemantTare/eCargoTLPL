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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for CreditMemoReceiptPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{

    public class CreditMemoReceiptPresenter : ClassLibraryMVP.General.Presenter
    {

        private ICreditMemoReceiptView objICreditMemoReceiptView;
        private CreditMemoReceiptModel objCreditMemoReceiptModel;
        DataSet objDS;

        public CreditMemoReceiptPresenter(ICreditMemoReceiptView creditMemoReceiptView, bool IsPostBack)
        {
            objICreditMemoReceiptView = creditMemoReceiptView;
            objCreditMemoReceiptModel = new CreditMemoReceiptModel(objICreditMemoReceiptView);

            base.Init(objICreditMemoReceiptView, objCreditMemoReceiptModel);

            if (!IsPostBack)
            {
                initValues();
                FillGrid();

            }
        }
        private void initValues()
        {
            if (objICreditMemoReceiptView.keyID > 0)
            {
                objDS = objCreditMemoReceiptModel.ReadValues();
                DataRow DR = objDS.Tables[0].Rows[0];


                objICreditMemoReceiptView.ReceiptNo = DR["Credit_Memo_Receipt_No_For_Print"].ToString();
                objICreditMemoReceiptView.ReceiptDate = Convert.ToDateTime(DR["Credit_Memo_Receipt_Date"]);
                objICreditMemoReceiptView.CashAmount = Util.String2Decimal(DR["Cash_Amount"].ToString());
                objICreditMemoReceiptView.ChequeDate = Convert.ToDateTime(DR["Cheque_Date"].ToString());
                objICreditMemoReceiptView.BankName = DR["Cheque_Bank"].ToString();
                objICreditMemoReceiptView.ChequeAmount = Util.String2Decimal(DR["Cheque_Amount"].ToString());
                objICreditMemoReceiptView.TotalAmount = Util.String2Decimal(DR["Total_Receipt_Amount"].ToString());
                objICreditMemoReceiptView.ChequeNo = DR["Cheque_No"].ToString();
                objICreditMemoReceiptView.Remarks = DR["Remarks"].ToString();
                objICreditMemoReceiptView.SetPartyNameId(DR["Ledger_Name"].ToString(), DR["Ledger_ID"].ToString());

            }
        }
        public void Save()
        {
            base.DBSave();
            //objVoucherModel.Save();
        }
        public void FillGrid()
        {
           objDS=objCreditMemoReceiptModel.FillGrid();
           objICreditMemoReceiptView.SessionCreditMemoDetails = objDS.Tables[0];
           objICreditMemoReceiptView.Bind_dg_CreditMemoDetails = objICreditMemoReceiptView.SessionCreditMemoDetails;
        }
    }
}