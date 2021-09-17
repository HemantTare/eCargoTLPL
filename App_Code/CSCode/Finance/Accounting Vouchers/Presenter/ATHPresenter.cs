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
/// <summary>
/// Summary description for ATHPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class ATHPresenter : Presenter
    {
        private IATHView objIATHView;
        private ATHModel objATHModel;
        private DataSet objDS;

        public ATHPresenter(IATHView ATHView, bool IsPostBack)
        {
            objIATHView = ATHView;
            objATHModel = new ATHModel(objIATHView);
            base.Init(objIATHView, objATHModel);

            if (!IsPostBack)
            {
                objIATHView.ATHDate = DateTime.Now.Date;
                initValues();
            }
        }

        public void fillValues()
        {
            objDS = objATHModel.FillValues();
            objIATHView.BindLHPONo = objDS.Tables[0];
        }

        public DataSet FillLHPODetails()
        {
            objDS = objATHModel.FillLHPODetails();
            return objDS;
        }

        public DataSet FillVehicleDetails()
        {
            objDS = objATHModel.FillVehicleDetails();
            return objDS;
        }

        private void initValues()
        {
            fillValues();

            objDS = objATHModel.ReadValues();
            objIATHView.SessionPetrolGrid = objDS.Tables[0];
  

            if (objIATHView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIATHView.ATHNo = objDR["Voucher_No"].ToString();
                    objIATHView.LHPOID = Util.String2Int(objDR["LHPO_ID"].ToString());
                    objIATHView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIATHView.ATHDate = Convert.ToDateTime(objDR["ATH_Date"].ToString());
                    objIATHView.ReferenceNo = objDR["Reference_No"].ToString();
                    objIATHView.Remarks = objDR["Remarks"].ToString();
                    objIATHView.AdvancePayableAmount = Util.String2Decimal(objDR["Advance_Payable_Amount"].ToString());
                    objIATHView.TotalPaidAmount = Util.String2Decimal(objDR["Total_Paid_Amount"].ToString());
                    objIATHView.TotalPetrolAmount = Util.String2Decimal(objDR["Petrol_Slip_Amount"].ToString());
                    objIATHView.CashChequeDetailsView.CashAmount = Util.String2Decimal(objDR["Cash_Amount"].ToString());
                    objIATHView.CashChequeDetailsView.ChequeAmount = Util.String2Decimal(objDR["Cheque_Amount"].ToString());
                    objIATHView.SetCreditToLedgerID(objDR["Ledger_name"].ToString(), objDR["Ledger_Id"].ToString());
                    objIATHView.CreditAmountTo = Util.String2Decimal(objDR["Credit_To_Amount"].ToString());

                }
            }
        }      

        public void Save()
        {
            base.DBSave();
	    //objATHModel.Save();
        }
    }
}
