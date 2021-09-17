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
    public class TrialBalancePresenter : Presenter
    {
        private ITrialBalanceView objITrialBalanceView;
        private TrialBalanceModel objTrialBalanceModel;
        private DataSet objDS;

        public TrialBalancePresenter(ITrialBalanceView trialBalanceView, bool IsPostBack)
        {
            objITrialBalanceView = trialBalanceView;
            objTrialBalanceModel = new TrialBalanceModel(objITrialBalanceView);

            base.Init(objITrialBalanceView, objTrialBalanceModel);
            if (!IsPostBack)
            {
                //objITrialBalanceView.CompanyName = "Reach Cargo";
                initValues();
            }

        }
        public void initValues()
        {

            //if (HttpContext.Current.Session["TB_DS"] == null)
            //{

               objDS = objTrialBalanceModel.ReadValues();
                   

            //}
            //else
            //{
            //    objDS = (DataSet)HttpContext.Current.Session["TB_DS"];
            //}
            objITrialBalanceView.BindTrialBalanceGrid = objDS;
            
        }
        
	}
}
