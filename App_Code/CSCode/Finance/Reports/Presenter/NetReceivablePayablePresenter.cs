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
/// Summary description for NetReceivablePayablePresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class NetReceivablePayablePresenter : Presenter
    {
        private INetReceivablePayableView objINetReceivablePayableView;
        private NetReceivablePayableModel objNetReceivablePayableModel;
        private DataSet objds;

        public NetReceivablePayablePresenter(INetReceivablePayableView netReceivablePayableView, bool IsPostBack)
        {
            objINetReceivablePayableView = netReceivablePayableView;
            objNetReceivablePayableModel = new NetReceivablePayableModel(objINetReceivablePayableView);

            base.Init(objINetReceivablePayableView, objNetReceivablePayableModel);

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
            objds = objNetReceivablePayableModel.ReadValues1(ref OnAccount);
            objINetReceivablePayableView.SessionNetReceivablePayableGrid = objds;
            objINetReceivablePayableView.BindGrid = objds.Tables[0];

        }
    }
}
