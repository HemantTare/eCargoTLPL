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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Summary description for MRCashChequePresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class MRCashChequePresenter:Presenter
    {
        private IMRCashChequeDetailsView _MRCashChequeDetailsView;
        private MRCashChequeDetailsModel _MRCashChequeDetailsModel;
        private int _Menu_Item_ID = Common.GetMenuItemId();

        public MRCashChequePresenter(IMRCashChequeDetailsView MRCashChequeDetailsView,bool isPostBack)
        {
            _MRCashChequeDetailsView = MRCashChequeDetailsView;
            _MRCashChequeDetailsModel = new MRCashChequeDetailsModel(_MRCashChequeDetailsView);
            base.Init(_MRCashChequeDetailsView, _MRCashChequeDetailsModel);

            if (!isPostBack)
            {
                initvalues();
            }


        }

        public void initvalues()
        {

           
                DataSet ds = new DataSet();
                ds = _MRCashChequeDetailsModel.ReadValues();
                if (_Menu_Item_ID == 106 || _Menu_Item_ID == 108 || _Menu_Item_ID == 11131 || _Menu_Item_ID == 83)
                {
                    _MRCashChequeDetailsView.Bind_ddlCashLedger = ds.Tables[2];
                }
                _MRCashChequeDetailsView.Session_ddl_DepositIn = ds.Tables[1];
                _MRCashChequeDetailsView.Bind_ChequeDetailsGrid = ds.Tables[0];
           
           

        }
    }
}
