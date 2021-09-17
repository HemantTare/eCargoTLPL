using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ClientFinancePresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class ClientFinancePresenter : Presenter
    {
        private IClientFinanceView objIClientFinanceView;
        private ClientFinanceModel objClientFinanceModel;
        private DataSet objDS;

        public ClientFinancePresenter(IClientFinanceView ClientFinanceView, bool isPostback)
        {
            objIClientFinanceView = ClientFinanceView;
            objClientFinanceModel = new ClientFinanceModel(objIClientFinanceView);
            base.Init(objIClientFinanceView, objClientFinanceModel);

            if (!isPostback)
            {
                objIClientFinanceView.RegistrationDate = System.DateTime.Now;
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objClientFinanceModel.FillValues();
            objIClientFinanceView.BindUserProfile = objDS.Tables[0];
        }

        public void FillLedgerDetails()
        {
            objDS = objClientFinanceModel.FillLedgerDetails();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIClientFinanceView.CreditDays = Util.String2Int(objDS.Tables[0].Rows[0]["Credit_Days"].ToString());
                objIClientFinanceView.CreditLimit = Util.String2Decimal(objDS.Tables[0].Rows[0]["Credit_Limit"].ToString());
            }
        }

        private void initValues()
        {
            fillValues();

            if (objIClientFinanceView.keyID > 0)
            {
                objDS = objClientFinanceModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIClientFinanceView.Is_ExistingLedger = Util.String2Bool(objDR["Is_Existing_Ledger"].ToString());
                    objIClientFinanceView.CreditDays = Util.String2Int(objDR["Credit_Days"].ToString());
                    objIClientFinanceView.CreditLimit = Util.String2Decimal(objDR["Credit_Limit"].ToString());
                    objIClientFinanceView.IntrestPercent = Util.String2Decimal(objDR["Interest_Percent"].ToString());
                    objIClientFinanceView.MinimumBalance = Util.String2Decimal(objDR["MinimumBalance"].ToString());
                    objIClientFinanceView.GraceDays = Util.String2Int(objDR["Grace_Days"].ToString());
                    objIClientFinanceView.IsServiceTaxPayByClient = Util.String2Bool(objDR["Is_Service_Tax_Applicable"].ToString());
                    objIClientFinanceView.IseCargoUser = Util.String2Bool(objDR["Is_User"].ToString());
                    objIClientFinanceView.UserProfileId = Util.String2Int(objDR["Profile_Id"].ToString());
                    objIClientFinanceView.BusinessHours = objDR["Business_Hrs"].ToString();

                    if (objDR["Is_Mechanical_Loading"].ToString() != "")
                        objIClientFinanceView.Is_LoadingTypeId = Util.String2Bool(objDR["Is_Mechanical_Loading"].ToString());
                    if (objDR["Registration_Date"].ToString() != "")
                        objIClientFinanceView.RegistrationDate = Convert.ToDateTime(objDR["Registration_Date"].ToString());

                    objIClientFinanceView.SetLedgerId(objDR["Ledger_Name"].ToString(), objDR["Ledger_Id"].ToString());
                    objIClientFinanceView.SetMarketingExcutiveId(objDR["Marketing_Executive_Name"].ToString(), objDR["Marketing_Executive_ID"].ToString());

                    objIClientFinanceView.Is_CreditParty = Util.String2Bool(objDR["Is_CreditParty"].ToString());


                    objIClientFinanceView.IsPrintFrtOnLR = Util.String2Bool(objDR["IsPrintFrtOnLR"].ToString());

                }
            }
        }
    }
}