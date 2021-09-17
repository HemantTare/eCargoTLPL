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
/// Summary description for NetReceivablePayableBillwisePresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class NetReceivablePayableBillwisePresenter : Presenter
    {
        private INetReceivablePayableBillwiseView objINetReceivablePayableBillwiseView;
        private NetReceivablePayableBillwiseModel objNetReceivablePayableBillwiseModel;
        private DataSet objds;

        public NetReceivablePayableBillwisePresenter(INetReceivablePayableBillwiseView netReceivablePayableBillwiseView, bool IsPostBack)
        {
            objINetReceivablePayableBillwiseView = netReceivablePayableBillwiseView;
            objNetReceivablePayableBillwiseModel = new NetReceivablePayableBillwiseModel(objINetReceivablePayableBillwiseView);

            base.Init(objINetReceivablePayableBillwiseView, objNetReceivablePayableBillwiseModel);


            if (IsPostBack == false)
            {
                initValues();
            }
        }

        public void initValues()
        {

            objds = objNetReceivablePayableBillwiseModel.ReadValues();
            objINetReceivablePayableBillwiseView.SessionNetReceivablePayableBillWiseGrid = objds;
            objINetReceivablePayableBillwiseView.BindGrid = objds.Tables[0];
           
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
