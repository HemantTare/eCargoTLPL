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

/// <summary>
/// Summary description for ProfitAndLossAccountPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class ProfitAndLossAccountPresenter : Presenter
    {
        private IProfitAndLossAccountView objIProfitAndLossAccountView;
        private ProfitAndLossAccountModel objProfitAndLossAccountModel;
        private DataSet objDS;

        public ProfitAndLossAccountPresenter(IProfitAndLossAccountView profitAndLossAccountView, bool IsPostBack)
        {
            objIProfitAndLossAccountView = profitAndLossAccountView;
            objProfitAndLossAccountModel = new ProfitAndLossAccountModel(objIProfitAndLossAccountView);

            base.Init(objIProfitAndLossAccountView, objProfitAndLossAccountModel);
            if (!IsPostBack)
            {
                //objITrialBalanceView.CompanyName = "Reach Cargo";
                initValues();
            }

        }


        public void initValues()
        {

            objDS = objProfitAndLossAccountModel.ReadValues();
            objIProfitAndLossAccountView.PnL_DS = objDS;  
            objIProfitAndLossAccountView.BindExpensesGrid=objDS.Tables[1];
            objIProfitAndLossAccountView.BindIncomeGrid=objDS.Tables[2];                
               
        }

	}
}
