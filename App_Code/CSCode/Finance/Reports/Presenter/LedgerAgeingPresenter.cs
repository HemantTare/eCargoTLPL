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
/// Summary description for LedgerAgeingPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class LedgerAgeingPresenter : Presenter
    {
        private ILedgerAgeingView objILedgerAgeingView;
        private LedgerAgeingModel objLedgerAgeingModel;
        private DataSet objds, objdsNew;

        public LedgerAgeingPresenter(ILedgerAgeingView ledgerAgeingView, bool IsPostBack)
        {
            objILedgerAgeingView = ledgerAgeingView;
            objLedgerAgeingModel = new LedgerAgeingModel(objILedgerAgeingView);

            base.Init(objILedgerAgeingView, objLedgerAgeingModel);
            objds = objILedgerAgeingView.FillGrid();

            if (IsPostBack == false)
            {
                initValues();
            }
        }

        public void initValues()
        {

            objdsNew = objLedgerAgeingModel.ReadValues();
            objdsNew.Merge(objds);
           // objds.Merge(objdsNew);
            objILedgerAgeingView.SessionLedgerAgeingGrid = objdsNew;
           // objILedgerAgeingView.BindLedgerAgeing = objds.Tables[0];
           

        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
