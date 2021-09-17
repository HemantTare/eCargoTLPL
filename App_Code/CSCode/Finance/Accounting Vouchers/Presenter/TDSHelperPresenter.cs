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
using ClassLibraryMVP;

/// <summary>
/// Summary description for TDSHelperPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class TDSHelperPresenter : ClassLibraryMVP.General.Presenter
    {

        private ITDSHelperView objITDSHelperView;
        private TDSHelperModel objTDSHelperModel;
        DataSet objDS;

        public TDSHelperPresenter(ITDSHelperView tdsHelperView, bool IsPostBack)
        {
            objITDSHelperView = tdsHelperView;
            objTDSHelperModel = new TDSHelperModel(objITDSHelperView);

            base.Init(objITDSHelperView, objTDSHelperModel);

            if (!IsPostBack)
            {
                objITDSHelperView.ToDateValue = System.DateTime.Now;    
                FillCashBankValues();
                FillTDSLedgerValues();
            }
        }

          public void save()
        {
            objTDSHelperModel.Save();
        }

        public void FillCashBankValues()
        {
            objDS = objTDSHelperModel.FillCashBankValues();
            objITDSHelperView.BindCashBankAc = objDS;
        }

        public void FillTDSLedgerValues()
        {
            objDS = objTDSHelperModel.FillTdsLedgerValues();
            objITDSHelperView.BindTDSLedgerAc = objDS;
        }

        public DataSet FillValues()
        {
            objDS = objTDSHelperModel.ReadValues();
            return objDS;
        }


    }
   
}
