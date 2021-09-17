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
/// Summary description for TrialBalancePresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class TrialBalanceLedgerPresenter : Presenter
    {
        private ITrialBalanceLedgerView objITrialBalanceLedgerView;
        private TrialBalanceLedgerModel objTrialBalanceLedgerModel;
        private DataSet objDS;

        public TrialBalanceLedgerPresenter(ITrialBalanceLedgerView trialBalanceLedgerView, bool IsPostBack)
        {
            objITrialBalanceLedgerView = trialBalanceLedgerView;
            objTrialBalanceLedgerModel = new TrialBalanceLedgerModel(objITrialBalanceLedgerView);

            base.Init(objITrialBalanceLedgerView, objTrialBalanceLedgerModel);
            if (!IsPostBack)
            {
                initValues();
            }
        }

        public void initValues()
        {
            objDS = objTrialBalanceLedgerModel.ReadValues();
            objITrialBalanceLedgerView.TB_DS = objDS;            
        }        
	}
}
