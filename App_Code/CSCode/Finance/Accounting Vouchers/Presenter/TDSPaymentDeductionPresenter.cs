using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;


/// <summary>
/// Summary description for TDSPaymentDeductionPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class TDSPaymentDeductionPresenter : ClassLibraryMVP.General.Presenter
    {
        private ITDSPaymentDeductionView objITDSPaymentDeductionView;
        private TDSPaymentDeductionModel objTDSPaymentDeductionModel;
        DataSet objDS;

        public TDSPaymentDeductionPresenter(ITDSPaymentDeductionView tdsPaymentDeductionView, bool IsPostBack)
        {
            objITDSPaymentDeductionView = tdsPaymentDeductionView;
            objTDSPaymentDeductionModel = new TDSPaymentDeductionModel(objITDSPaymentDeductionView);

            base.Init(objITDSPaymentDeductionView, objTDSPaymentDeductionModel);

            if (!IsPostBack)
            {
               // ReadValues();
            }
        }

            public void ReadValues()
            {
                objDS = objTDSPaymentDeductionModel.GetTDSLedgerDetails();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objITDSPaymentDeductionView.PartyName = objDR["Party_Name"].ToString();
                    objITDSPaymentDeductionView.DeducteeTypeName = objDR["Deductee_Type"].ToString();
                    objITDSPaymentDeductionView.AmountOfThisVoucher = objDR["Amount_Of_Voucher"].ToString();
                    objITDSPaymentDeductionView.AmountPaidPayableTillDate = objDR["Amount_Paid_Till_Date"].ToString();
                    objITDSPaymentDeductionView.TotalAmountPaidPayable = objDR["Total_Amount_Payable"].ToString();
                    objITDSPaymentDeductionView.TaxPercent = objDR["Tax_Rate"].ToString();
                    objITDSPaymentDeductionView.TaxAmount = objDR["Tax_Amount"].ToString();
                    objITDSPaymentDeductionView.SurchargePercent = objDR["Surcharge_Rate"].ToString();
                    objITDSPaymentDeductionView.SurchargeAmount = objDR["Surcharge_Amount"].ToString();
                    objITDSPaymentDeductionView.AdditionalSurchargePercent = objDR["Addl_Surcharge_Rate"].ToString();
                    objITDSPaymentDeductionView.AdditionalSurchargeAmount = objDR["Addl_Surcharge_Amount"].ToString();
                    objITDSPaymentDeductionView.AdditionalCessPercent = objDR["Addl_Edu_Cess_Rate"].ToString();
                    objITDSPaymentDeductionView.AdditionalCessAmount = objDR["Addl_Edu_Cess_Amount"].ToString();
                    objITDSPaymentDeductionView.TotalTDS = objDR["Total_TDS_Amount"].ToString();
                    objITDSPaymentDeductionView.TDSDeductedTillDate = objDR["Less_TDS_Deducted"].ToString();
                    objITDSPaymentDeductionView.NetTDSDeducted = objDR["Net_TDS_To_Deduct"].ToString();

                }
                else
                {
                }
        }
	}
}
