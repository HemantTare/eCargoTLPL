using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

namespace Raj.EC.FinancePresenter
{
    public class OnaccountAdjustmentPresenter : ClassLibraryMVP.General.Presenter 
    {
        private OnaccountAdjustmentView _OnAcctAdjView;
        private OnaccountAdjustmentModel _OnAcctAdjModel;


        public OnaccountAdjustmentPresenter(OnaccountAdjustmentView OnAcctAdjView, bool IsPostBack)
        {
            _OnAcctAdjView = OnAcctAdjView;
            _OnAcctAdjModel = new OnaccountAdjustmentModel(_OnAcctAdjView);
            
           base.Init(_OnAcctAdjView,_OnAcctAdjModel);

           if (!IsPostBack)
           {
               _OnAcctAdjView.hideApprove = false;
               _OnAcctAdjView.hideAutoAdjust = false;
               bindLedgerGroup();
               //bindLedger();
               bindUnAdjustedGrid();
               bindBlankAdjustedGrid();
           }
        }

        public void bindBlankAdjustedGrid()
        {
            _OnAcctAdjView.BindOnAccountAdjustedGrid = _OnAcctAdjModel.Blank_AdjustmentTable();
            _OnAcctAdjView.hideApprove = false;
            _OnAcctAdjView.hideAutoAdjust = false;
            _OnAcctAdjView.Balance_Amount = 0;
        }

        private void bindLedgerGroup()
        {
            _OnAcctAdjView.BindLedgerGroup = _OnAcctAdjModel.Get_Ledger_Group();
        }



        public  void bindUnAdjustedGrid()
        {
            _OnAcctAdjView.SessionOnAccount = _OnAcctAdjModel.Get_OnAccount_Data();
            _OnAcctAdjView.BindOnAccountUnAdjustedGrid = _OnAcctAdjView.SessionOnAccount;
        }

        public void bindAdjustedGrid()
        {
            DataTable dt = new DataTable();
 
           dt = _OnAcctAdjModel.Get_ONAccount_Adjusted_Data();
           _OnAcctAdjView.SessionOnAccountAdjusted = dt;
           _OnAcctAdjView.BindOnAccountAdjustedGrid = dt;

           if (dt.Rows.Count > 0)
           {
               _OnAcctAdjView.hideApprove = true;
               _OnAcctAdjView.hideAutoAdjust = true;
           }
           else
           {
               _OnAcctAdjView.hideApprove = false;
               _OnAcctAdjView.hideAutoAdjust = false;
           }
        }

      

        public void bindBlankUnadjustedGrid()
        {
            _OnAcctAdjView.BindOnAccountUnAdjustedGrid = _OnAcctAdjModel.Blank_UnAdjustmentTable();
        }

        public void save()
        {
            //_OnAcctAdjModel.Save();
            base.DBSave();
        }
    }
}
