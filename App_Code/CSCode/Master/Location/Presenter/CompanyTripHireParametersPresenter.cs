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
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for WucCompanyTripHireParametersPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class CompanyTripHireParametersPresenter : Presenter
    {
        private ICompanyTripHireParametersView objICompanyTripHireParametersView;
        private CompanyTripHireParametersModel objCompanyTripHireParametersModel;
        private DataSet objDS;

        public CompanyTripHireParametersPresenter(ICompanyTripHireParametersView companyTripHireParametersView, bool IsPostBack)
        {
            objICompanyTripHireParametersView = companyTripHireParametersView;
            objCompanyTripHireParametersModel = new CompanyTripHireParametersModel(objICompanyTripHireParametersView);
            base.Init(objICompanyTripHireParametersView, objCompanyTripHireParametersModel);

            if (!IsPostBack)
            {
                
                initValues();
            }
        }
        public void Save()
        {
          
        }

        private void initValues()
        {
            objDS = objCompanyTripHireParametersModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                objICompanyTripHireParametersView.LHPONatureOfPaymentId = Util.String2Int(DR["LHPO_Nature_Of_Payemnt_ID_For_TDS_Deduction"].ToString());
                objICompanyTripHireParametersView.IsTreatAdvanceForOwnTruckAsExpense = Util.String2Bool(DR["Is_Treat_Advance_For_Own_Truck_As_Expense"].ToString());
                if (objICompanyTripHireParametersView.IsTreatAdvanceForOwnTruckAsExpense == true)
                {
                    objICompanyTripHireParametersView.SetExpenseLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_Id"].ToString());
                }
            }            
        }
    }
}