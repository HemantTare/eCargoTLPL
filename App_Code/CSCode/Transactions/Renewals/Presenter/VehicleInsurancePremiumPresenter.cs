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
using Raj.EF.TransactionsView;
using Raj.EF.TransactionsModel;

/// <summary>
/// Summary description for VehicleInsurancePremiumPresenter
/// </summary>
/// 
namespace Raj.EF.TransactionsPresenter
{
    public class VehicleInsurancePremiumPresenter:Presenter
    {
        private IVehicleInsurancePremiumView objIVehicleInsurancePremiumView;
        private VehicleInsurancePremiumModel objVehicleInsurancePremiumModel;
        private DataSet objDS;

        public VehicleInsurancePremiumPresenter(IVehicleInsurancePremiumView vehicleInsurancePremiumView, bool isPostBack)
        {
            objIVehicleInsurancePremiumView = vehicleInsurancePremiumView;
            objVehicleInsurancePremiumModel = new VehicleInsurancePremiumModel(objIVehicleInsurancePremiumView);
            base.Init(objIVehicleInsurancePremiumView, objVehicleInsurancePremiumModel);

            if (!isPostBack)
            {
                objIVehicleInsurancePremiumView.InsuranceDate = DateTime.Now;
                objIVehicleInsurancePremiumView.ChequeDate = DateTime.Now;
                objIVehicleInsurancePremiumView.CommenceDate = DateTime.Now;
                objIVehicleInsurancePremiumView.ExpiryDate = DateTime.Now;

                fillValues();
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objVehicleInsurancePremiumModel.FillValues();
            objIVehicleInsurancePremiumView.BindInsuranceCompany = objDS.Tables[0];
            objIVehicleInsurancePremiumView.BindAgent = objDS.Tables[1];
            objIVehicleInsurancePremiumView.SessionPremiumTypeDropdown = objDS.Tables[2];
            objIVehicleInsurancePremiumView.BindBankName = objDS.Tables[3];

            fillBranchOnInsuCompanyChange();
        }

        public void fillBranchOnInsuCompanyChange()
        {
            DataSet objds1;
            objds1 = objVehicleInsurancePremiumModel.fillBranchOnInsuCompanyChange();
            objIVehicleInsurancePremiumView.BindIssuingBranch = objds1.Tables[0];
        }

        public bool IsExpiryDateValid()
        {
            return objVehicleInsurancePremiumModel.IsExpiryDateValid();
        }

        public DataTable IsValidExpiryDate()
        {
            objDS = objVehicleInsurancePremiumModel.IsValidExpiryDate();
            return objDS.Tables[0];
        }

        private void initValues()
        {

            objDS = objVehicleInsurancePremiumModel.ReadValues();
            objIVehicleInsurancePremiumView.BindPremiumDetailsGrid  = objDS.Tables[1];

            if (objIVehicleInsurancePremiumView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    objIVehicleInsurancePremiumView.Insurance_No = objDR["Vehicle_Insurance_No"].ToString();
                    objIVehicleInsurancePremiumView.InsuranceDate = Convert.ToDateTime(objDR["Vehicle_Insurance_Date"].ToString());

                    objIVehicleInsurancePremiumView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIVehicleInsurancePremiumView.VehicleNo = Util.String2Int(objDR["Number_Part4"].ToString());
                    objIVehicleInsurancePremiumView.VehicleInsuranceID = Util.String2Int(objDR["Vehicle_Insurance_Id"].ToString());
                    objIVehicleInsurancePremiumView.InsuranceCompanyID = Util.String2Int(objDR["Insurance_Company_ID"].ToString());
                    fillBranchOnInsuCompanyChange();
                    objIVehicleInsurancePremiumView.IssuingBranchID = Util.String2Int(objDR["Insurance_Company_Branch_ID"].ToString());
                    objIVehicleInsurancePremiumView.PolicyNo = objDR["Policy_No"].ToString();
                    objIVehicleInsurancePremiumView.AgentID = Util.String2Int(objDR["Agent_ID"].ToString());
                    objIVehicleInsurancePremiumView.CommenceDate = Convert.ToDateTime(objDR["Commence_Date"].ToString());
                    objIVehicleInsurancePremiumView.ExpiryDate = Convert.ToDateTime(objDR["Expiry_Date"].ToString());
                    objIVehicleInsurancePremiumView.IDV = Util.String2Decimal(objDR["IDV"].ToString());
                    objIVehicleInsurancePremiumView.EngineNo = objDR["Engine_No"].ToString();
                    objIVehicleInsurancePremiumView.ChasisNo = objDR["Chasis_No"].ToString();
                    objIVehicleInsurancePremiumView.FirstPartyPremium = Util.String2Decimal(objDR["First_Party_Premium"].ToString());
                    objIVehicleInsurancePremiumView.ThirdPartyPremium = Util.String2Decimal(objDR["Third_Party_Premium"].ToString());
                    objIVehicleInsurancePremiumView.LoadingPercentTPP = Util.String2Decimal(objDR["Loading_Percent_TPP"].ToString());
                    objIVehicleInsurancePremiumView.LoadingAmountTPP = Util.String2Decimal(objDR["Loading_Amount_TPP"].ToString());
                    objIVehicleInsurancePremiumView.LoadingPercentFPP = Util.String2Decimal(objDR["Loading_Percent_FPP"].ToString());
                    objIVehicleInsurancePremiumView.LoadingAmountFPP = Util.String2Decimal(objDR["Loading_Amount_FPP"].ToString());
                    objIVehicleInsurancePremiumView.NCBPercentFPP = Util.String2Decimal(objDR["NCB_Percent_FPP"].ToString());
                    objIVehicleInsurancePremiumView.NCBAmount = Util.String2Decimal(objDR["NCB_Amount"].ToString());
                    objIVehicleInsurancePremiumView.NetPremium = Util.String2Decimal(objDR["Net_Premium"].ToString());
                    objIVehicleInsurancePremiumView.ServiceTaxAmount = Util.String2Decimal(objDR["Service_Tax_Amount"].ToString());
                    objIVehicleInsurancePremiumView.NetPayable = Util.String2Decimal(objDR["Net_Payable"].ToString());

                    objIVehicleInsurancePremiumView.Is_Cheque = Util.String2Bool(objDR["Is_Cheque"].ToString());
                    objIVehicleInsurancePremiumView.ChequeNo = objDR["Cheque_No"].ToString();
                    objIVehicleInsurancePremiumView.ChequeDate = Convert.ToDateTime(objDR["Cheque_Date"].ToString());
                    objIVehicleInsurancePremiumView.Bank_ID = Util.String2Int(objDR["Bank_ID"].ToString());

                  
                }

            }
        }

        public void Save()
        {

            base.DBSave();
            //objVehicleInsurancePremiumModel.Save();
        }
    }
}
