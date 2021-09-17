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
/// Summary description for LedgerBillWiseDetailPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class LedgerBillWiseDetailPresenter : Presenter
    {
        private ILedgerBillWiseDetailView objILedgerBillWiseDetailView;
        private LedgerBillWiseDetailModel objLedgerBillWiseDetailModel;
        private DataSet objds,objdsNew;

        public LedgerBillWiseDetailPresenter(ILedgerBillWiseDetailView ledgerBillWiseDetailView, bool IsPostBack)
        {
            objILedgerBillWiseDetailView = ledgerBillWiseDetailView;
            objLedgerBillWiseDetailModel = new LedgerBillWiseDetailModel(objILedgerBillWiseDetailView);

            base.Init(objILedgerBillWiseDetailView, objLedgerBillWiseDetailModel);


            if (IsPostBack == false)
            {
                initValues();
            }
        }

        public void initValues()
        {

            objds = objLedgerBillWiseDetailModel.ReadValues();
            objILedgerBillWiseDetailView.SessionLedgerBillWiseGrid = objds;
            objILedgerBillWiseDetailView.BindGrid = objds.Tables[0];
            
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
