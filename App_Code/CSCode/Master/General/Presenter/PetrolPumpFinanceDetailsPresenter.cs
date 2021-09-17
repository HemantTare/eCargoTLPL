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
//using Raj.eCargo.Init;

/// <summary> 
/// Summary description for PetrolPumpFinanceDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class PetrolPumpFinanceDetailsPresenter : ClassLibraryMVP.General.Presenter 
    {

        private IPetrolPumpFinanceDetailsView objIPetrolPumpFinanceDetailsView;
        private PetrolPumpFinanceDetailsModel objPetrolPumpFinanceDetailsModel;
        private DataSet objDS;

        public PetrolPumpFinanceDetailsPresenter(IPetrolPumpFinanceDetailsView PetrolPumpFinanceDetailsView, bool IsPostBack)
        {
            objIPetrolPumpFinanceDetailsView = PetrolPumpFinanceDetailsView;
            objPetrolPumpFinanceDetailsModel = new PetrolPumpFinanceDetailsModel(objIPetrolPumpFinanceDetailsView);

            base.Init(objIPetrolPumpFinanceDetailsView, objPetrolPumpFinanceDetailsModel);

            if (!IsPostBack)
            {               
                    
                initValues();                 
            }
        }

        public void Fill_Values()
        {
            objDS = objPetrolPumpFinanceDetailsModel.Fill_Values();

            objIPetrolPumpFinanceDetailsView.BindLedgerGroup   = objDS.Tables[0];
       //     objIPetrolPumpFinanceDetailsView.BindLedger  = objDS.Tables[1];
            //objIPetrolPumpFinanceDetailsView.BindTDSDeducteeType = objDS.Tables[1];


            objIPetrolPumpFinanceDetailsView.BindDivision   = objDS.Tables[2];
          


        }

        public void ReadValues()
        {

            DataSet objDS_PetrolPumpFinanceDetails = new DataSet();

            objDS_PetrolPumpFinanceDetails = objPetrolPumpFinanceDetailsModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {

                //objIPetrolPumpGeneralView.AddressView.FaxNo = Convert.ToString(objDS.Tables[0].Rows[0]["FaxNo"]);
                //                if (objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Use_Existing_Ledger"] != null)
                objIPetrolPumpFinanceDetailsView.Use_Existing_Ledger = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Use_Existing_Ledger"]);

                objIPetrolPumpFinanceDetailsView.Is_Use_Existing_Ledger = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Use_Existing_Ledger"]);

                objIPetrolPumpFinanceDetailsView.LedgerGroupId = Convert.ToInt32(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Ledger_Group_ID"]);
                Fill_Ledger();
                objIPetrolPumpFinanceDetailsView.LedgerId = Convert.ToInt32(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Ledger_ID"]);

                objIPetrolPumpFinanceDetailsView.CreditDays = Convert.ToInt32(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Credit_Days"]);
                objIPetrolPumpFinanceDetailsView.CreditLimit = Convert.ToInt32(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Credit_Limit"]);
                objIPetrolPumpFinanceDetailsView.Is_Service_Tax_Applicable = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Is_Service_Tax_Applicable"]);
                objIPetrolPumpFinanceDetailsView.Is_Exempted = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Is_Service_Tax_Exempted"]);
                objIPetrolPumpFinanceDetailsView.Notification_Detail = Convert.ToString(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Notification_Details"]);



                //objIPetrolPumpFinanceDetailsView.Is_TDS_Applicable = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Is_TDS_Applicable"]);

                //objIPetrolPumpFinanceDetailsView.TDS_Deductee_Type_ID = Convert.ToInt32(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["TDS_Deductee_Type_ID"]);

                //objIPetrolPumpFinanceDetailsView.Is_Lower_No_Deduction_Applicable = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Is_Lower_Deduction"]);

                //objIPetrolPumpFinanceDetailsView.Section_Number = Convert.ToString(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Section_Number"]);

                //objIPetrolPumpFinanceDetailsView.TDS_Lower_Rate = Convert.ToDecimal(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["TDS_Lower_Rate"]);



                //objIPetrolPumpFinanceDetailsView.Is_Ignore_Exemption_Limit = Convert.ToBoolean(objDS_PetrolPumpFinanceDetails.Tables[0].Rows[0]["Ignore_Surcharge_Exemption_Limit"]);
            }
        }

        public void Fill_Ledger()
        {
            objDS = objPetrolPumpFinanceDetailsModel.Fill_Ledger();
            objIPetrolPumpFinanceDetailsView.BindLedger = objDS.Tables[0];      
        }

        public DataSet  Get_LedgerDetails()
        {
            objDS = objPetrolPumpFinanceDetailsModel.Get_LedgerDetails();
            return objDS;
        }
 
        private void initValues()
        {
            Fill_Values();

            if (objIPetrolPumpFinanceDetailsView.keyID > 0)
            {
                ReadValues();
            }
        }

        public DataSet ReadDivisionsValues()
        {
            objDS = objPetrolPumpFinanceDetailsModel.ReadDivisionsValues ();
            return objDS;
        }
        
        public void save()
        {
            
            //base.DBSave();
            objPetrolPumpFinanceDetailsModel.Save();
        }


        //public void GetCityDetails()
        //{

        //    //base.DBSave();
        //    objDS = objPetrolPumpFinanceDetailsModel.GetCityDetails();

        //    if (objDS.Tables[0].Rows.Count > 0)
        //    {

        //      objIPetrolPumpFinanceDetailsView.State   = Convert.ToString(objDS.Tables[0].Rows[0]["State"]);
        //      objIPetrolPumpFinanceDetailsView.State = Convert.ToString(objDS.Tables[0].Rows[0]["State"]);
        //      objIPetrolPumpFinanceDetailsView.State = Convert.ToString(objDS.Tables[0].Rows[0]["State"]);
        //      objIPetrolPumpFinanceDetailsView.State = Convert.ToString(objDS.Tables[0].Rows[0]["State"]);

        //    }
               

        //}


      

    }
}