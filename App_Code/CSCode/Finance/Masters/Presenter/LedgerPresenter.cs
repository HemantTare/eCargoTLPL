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
    public class LedgerPresenter : Presenter
    {
    
        private ILedgerView objILedgerView;
        private LedgerModel objLedgerModel;
        private DataSet objDS;
         public LedgerPresenter(ILedgerView LedgerView, bool IsPostBack)
        {
            objILedgerView = LedgerView;
            objLedgerModel = new LedgerModel(objILedgerView);
            base.Init(objILedgerView, objLedgerModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
           // fillValues();

            if (objILedgerView.keyID > 0)
            {
               readValues();
            }
        }

        //private void fillValues()
        //{
        //    objDS = objLedgerModel.FillValues();
        //    objILedgerView.bind_ddl_Under = objDS.Tables[0];
        //    objILedgerView.bind_ddl_ServiceTaxCategory = objDS.Tables[1];
        //    objILedgerView.bind_ddl_Deductee_Type = objDS.Tables[2];
        //    objILedgerView.bind_ddl_NatureOfPayment = objDS.Tables[3];
        //    objILedgerView.bind_ddl_FBTCategory = objDS.Tables[4];
        //}

        private void readValues()
        {
            objDS = objLedgerModel.ReadValues();

            DataRow Dr = objDS.Tables[0].Rows[0];

            objILedgerView.getLedgerGeneralView.ACNo = Dr["Bank_Ac_No"].ToString();
            objILedgerView.getLedgerGeneralView.Alias = Dr["Alias"].ToString();
            objILedgerView.getLedgerGeneralView.LedgerName = Dr["Ledger_Name"].ToString();
            objILedgerView.getLedgerGeneralView.SectionNumber = Dr["Section_Number"].ToString();
            objILedgerView.getLedgerGeneralView.Income_Tax_No = Dr["Income_Tax_No"].ToString();
            objILedgerView.getLedgerGeneralView.NotificationDetail = Dr["Notification_Detail"].ToString();
            objILedgerView.getLedgerGeneralView.ServiceTaxNo = Dr["Service_Tax_No"].ToString();
            objILedgerView.getLedgerGeneralView.TIN_Sales_Tax_No = Dr["TIN_Sales_Tax_No"].ToString();
            objILedgerView.getLedgerGeneralView.TypeOfDutyTax = Dr["Type_Of_Duty_Tax"].ToString();


            objILedgerView.getLedgerGeneralView.DefaultCreditPeriod = Convert.ToInt32(Dr["Default_Credit_Period"]);
            objILedgerView.getLedgerGeneralView.FBTCategoryId = Convert.ToInt32(Dr["FBT_Category_Id"]);
            objILedgerView.getLedgerGeneralView.LedgerUnderId = Dr["Ledger_Group_Id"].ToString();
            objILedgerView.getLedgerGeneralView.NatureOfPaymentId = Convert.ToInt32(Dr["Nature_Of_Payment_Id"]);
            objILedgerView.getLedgerGeneralView.ServiceTaxCategoryId = Convert.ToInt32(Dr["Service_Tax_Category_Id"]);
            objILedgerView.getLedgerGeneralView.TDSDeducteeTypeId = Convert.ToInt32(Dr["TDS_Deductee_Type_Id"]);


            objILedgerView.getLedgerGeneralView.CreditLimit = Convert.ToDecimal(Dr["Credit_Limit"]);
            objILedgerView.getLedgerGeneralView.TDSLowerRate = Convert.ToDecimal(Dr["TDS_Lower_Rate"]);


            objILedgerView.getLedgerGeneralView.IsExempted = Convert.ToBoolean(Dr["Is_Exempted"]);
            objILedgerView.getLedgerGeneralView.IsFBTApplicable = Convert.ToBoolean(Dr["Is_FBT_Applicable"]);
            objILedgerView.getLedgerGeneralView.IsIgnoreExemptionLimit = Convert.ToBoolean(Dr["Ignore_Exemption_Limit"]);
            objILedgerView.getLedgerGeneralView.IsLowerDeduction = Convert.ToBoolean(Dr["Is_Lower_Deduction"]);
            objILedgerView.getLedgerGeneralView.IsMaintainBillByBill = Convert.ToBoolean(Dr["Maintain_Bill_By_Bill"]);
            objILedgerView.getLedgerGeneralView.IsServiceTaxApplicable = Convert.ToBoolean(Dr["Is_Service_Tax_Applicable"]);
            objILedgerView.getLedgerGeneralView.IsTDSApplicable = Convert.ToBoolean(Dr["Is_TDS_Applicable"]);

            objILedgerView.getLedgerGeneralView.ServiceTaxRegDate = Convert.ToDateTime(Dr["Service_Tax_Reg_Date"]);
            objILedgerView.getLedgerGeneralView.DateOfBankReco = Convert.ToDateTime(Dr["Effective_Date_Of_Bank_Reco"]);
            objILedgerView.getLedgerGeneralView.SelectedHierarchy = Dr["Bank_Reco_Hierarchy_Code"].ToString();
            objILedgerView.getLedgerGeneralView.MainId =  Convert.ToInt32(Dr["Bank_Reco_Main_Id"]);
            //objILedgerView.getLedgerGeneralView.ba = Convert.ToDateTime(Dr["Ledger_Group_Name"]);
            objILedgerView.getLedgerGeneralView.EnableControls = (Util.String2Bool(Dr["ExistsReserved"].ToString()) == true ? false : true);


            objILedgerView.Name = Dr["Mailing_Name"].ToString();
            objILedgerView.ContactPerson = Dr["Contact_Person"].ToString();
            objILedgerView.Note = Dr["Notes"].ToString();




            objILedgerView.getAddressView.AddressLine1 = Dr["Add1"].ToString();
            objILedgerView.getAddressView.AddressLine2 = Dr["Add2"].ToString();
            objILedgerView.getAddressView.EmailId = Dr["Email"].ToString();
            objILedgerView.getAddressView.FaxNo = Dr["Fax"].ToString();
            objILedgerView.getAddressView.Phone1 = Dr["Phone"].ToString();
            objILedgerView.getAddressView.PinCode = Dr["Pin_Code"].ToString();
            objILedgerView.getAddressView.CityId = Convert.ToInt32(Dr["City_Id"]);

            objILedgerView.getDivisionView.SetDivisions = objDS.Tables[1];    
        }


        public void Save()
        {
            base.DBSave();
            //objLedgerModel.Save();
        }



        //public void FillLocation()
        //{
        //    objDS = objLedgerModel.FillLocation();
        //    objILedgerView.bind_ddl_Location = objDS.Tables[0];
        //}
    }
}
