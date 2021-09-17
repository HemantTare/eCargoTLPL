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
using Raj.EF.MasterView;
using Raj.EF.MasterModel;
/// <summary>
/// Summary description for VehicleLoanDetailsPresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{

    public class VehicleLoanDetailsPresenter:Presenter 
    {
        private IVehicleLoanDetailsView objIVehicleLoanDetailsView;
        private VehicleLoanDetailsModel objVehicleLoanDetailsModel;
        private DataSet objDS;

        public VehicleLoanDetailsPresenter(IVehicleLoanDetailsView vehicleLoanDetailsView,bool isPostBack)
        {
            objIVehicleLoanDetailsView = vehicleLoanDetailsView;
            objVehicleLoanDetailsModel = new VehicleLoanDetailsModel(objIVehicleLoanDetailsView);
            base.Init(objIVehicleLoanDetailsView, objVehicleLoanDetailsModel);

            if (!isPostBack)
            {
                fillValues();
                if (objIVehicleLoanDetailsView.keyID > 0)
                {
                    initValues();
                }
            }
        }

       
        private void initValues()
        {
            objDS = objVehicleLoanDetailsModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIVehicleLoanDetailsView.LoanAcctNo = objDR["Loan_Acc_No"].ToString();
                objIVehicleLoanDetailsView.Comments = objDR["Loan_Comments"].ToString();

                objIVehicleLoanDetailsView.BankID = Util.String2Int(objDR["Loan_Bank_ID"].ToString());
                objIVehicleLoanDetailsView.TermsInMonths = Util.String2Int(objDR["Terms_Months"].ToString());
                objIVehicleLoanDetailsView.InterestTypeID = Util.String2Int(objDR["Interest_Type_ID"].ToString());
                objIVehicleLoanDetailsView.PaymentModeID = Util.String2Int(objDR["EMI_Payment_Mode_ID"].ToString());

                objIVehicleLoanDetailsView.PaymentBankID = Util.String2Int(objDR["Loan_Details_Bank_ID"].ToString());
                objIVehicleLoanDetailsView.StartChequeNo = Util.String2Int(objDR["Loan_Details_Cheque_No"].ToString());

                objIVehicleLoanDetailsView.LoanAmount = Util.String2Decimal(objDR["Loan_Amount"].ToString());
                objIVehicleLoanDetailsView.RateOfInterest = Util.String2Decimal(objDR["Interest_Rate"].ToString());
                objIVehicleLoanDetailsView.EMIAmount = Util.String2Decimal(objDR["EMI_Amount"].ToString());
              }
              objIVehicleLoanDetailsView.Bind_dg_Payment_Details = objDS.Tables[2];

        }

        private void fillValues()
        {
            objDS = objVehicleLoanDetailsModel.FillValues();

            objIVehicleLoanDetailsView.Bind_ddl_Bank_Name = objDS.Tables[0];
            objIVehicleLoanDetailsView.Bind_ddl_InterestType = objDS.Tables[1];
            objIVehicleLoanDetailsView.Bind_ddl_PaymentMode = objDS.Tables[2];
            objIVehicleLoanDetailsView.Bind_ddl_PaymentBank_Name = objDS.Tables[0];
            objIVehicleLoanDetailsView.SesssionBankNameDT = objDS.Tables[0];

            objDS = objVehicleLoanDetailsModel.ReadValues();
            objIVehicleLoanDetailsView.Bind_dg_Payment_Details = objDS.Tables[2];
        }



        public void Save()
        {

            base.DBSave();
            //objVehicleLoanDetailsModel.Save();
        }

    }
}