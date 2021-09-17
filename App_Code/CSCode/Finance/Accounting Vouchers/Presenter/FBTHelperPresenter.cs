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
/// Summary description for FBTHelperPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class  FBTHelperPresenter : ClassLibraryMVP.General.Presenter
    {

        private IFBTHelperView objIFBTHelperView;
        private FBTHelperModel objFBTHelperModel;
        DataSet objDS;

        public FBTHelperPresenter(IFBTHelperView fbtHelperView, bool IsPostBack)
        {
            objIFBTHelperView = fbtHelperView;
            objFBTHelperModel = new FBTHelperModel(objIFBTHelperView);

            base.Init(objIFBTHelperView, objFBTHelperModel);

            if (!IsPostBack)
            {

                FillCashBankValues();
                FillFBTLedgerValues();
            }
        }

        public void save()
        {
            objFBTHelperModel.Save();
        }
        public void FillCashBankValues()
        {
            objDS = objFBTHelperModel.FillCashBankValues();
           objIFBTHelperView.BindCashBankAc= objDS;
        }
      
        public void FillFBTLedgerValues()
        {
            objDS = objFBTHelperModel.FillFBTLedgerValues();
            objIFBTHelperView.BindFBTLedger = objDS;
        }

        public DataSet FillValues()
        {
            objDS = objFBTHelperModel.ReadValues();
            return objDS;
        }

    }

}

