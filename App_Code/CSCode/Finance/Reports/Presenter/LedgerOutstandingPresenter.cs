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
/// Summary description for LedgerOutstandingPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class LedgerOutstandingPresenter : Presenter
    {
        private ILedgerOutstandingView objILedgerOutstandingView;
        private LedgerOutstandingModel objLedgerOutstandingModel;
        private DataSet objds;

        public LedgerOutstandingPresenter(ILedgerOutstandingView ledgerOutstandingView, bool IsPostBack)
        {
            objILedgerOutstandingView = ledgerOutstandingView;
            objLedgerOutstandingModel = new LedgerOutstandingModel(objILedgerOutstandingView);

            base.Init(objILedgerOutstandingView, objLedgerOutstandingModel);

            if (IsPostBack == false)
            {
              //  initValues();
                //GetOnAccount(OnAccount);
            }
        }

        //private void initValues()
        //{
           
        //    objds = objLedgerOutstandingModel.ReadValues1(ref OnAccount);
        //    objILedgerOutstandingView.SessionLedgerOutstandingGrid = objds;
        //    objILedgerOutstandingView.BindGrid = objds.Tables[0];           
        //}

        public void Save()
        {
            base.DBSave();
        }
        public void GetOnAccount(ref decimal OnAccount)
        {
           objds= objLedgerOutstandingModel.ReadValues1(ref OnAccount);
           objILedgerOutstandingView.SessionLedgerOutstandingGrid = objds;
           objILedgerOutstandingView.BindGrid = objds.Tables[0];

        }
    }
}
