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
/// Summary description for BankRecoStatementPresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class BankRecoStatementPresenter : Presenter
    {
        private IBankRecoStatementView _objIBankRecoStatementView;
        private BankRecoStatementModel _objBankRecoStatementModel;
        private DataSet Ds;
        

        public BankRecoStatementPresenter(IBankRecoStatementView objIBankRecoStatementView, bool isPostBack)
        {
            _objIBankRecoStatementView = objIBankRecoStatementView;
            _objBankRecoStatementModel = new BankRecoStatementModel(_objIBankRecoStatementView);
            base.Init(_objIBankRecoStatementView, _objBankRecoStatementModel);
            object o = new object();
            EventArgs e = new EventArgs();
            if (!isPostBack)
            {
                initControl(o,e);
            }
        }

        public void initControl(object o, EventArgs e)
        {
           
                Ds = _objBankRecoStatementModel.ReadValues();
                _objIBankRecoStatementView.SetLables = Ds.Tables[1];

                DataView objDV1 = new DataView(Ds.Tables[0].Copy(), "Credit > 0", "", DataViewRowState.CurrentRows);
                _objIBankRecoStatementView.Session_Dt_AddBankRecoStatement = objDV1.ToTable();
                _objIBankRecoStatementView.bind_dg_AddBankRecoStatement = objDV1.ToTable();

                DataView objDV2 = new DataView(Ds.Tables[0].Copy(), "Debit > 0", "", DataViewRowState.CurrentRows);
                _objIBankRecoStatementView.Session_Dt_LessBankRecoStatement = objDV2.ToTable();
                _objIBankRecoStatementView.bind_dg_LessBankRecoStatement = objDV2.ToTable();

           

        }
        public void Save()
        {

        }
    }
}