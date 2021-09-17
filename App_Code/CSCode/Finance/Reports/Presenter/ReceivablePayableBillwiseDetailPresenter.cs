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
/// Summary description for ReceivablePayableBillwiseDetailPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class ReceivablePayableBillwiseDetailPresenter : Presenter
    {
        private IReceivablePayableBillwiseDetailView objIReceivablePayableBillwiseDetailView;
        private ReceivablePayableBillwiseDetailModel objReceivablePayableBillwiseDetailModel;
        private DataSet objds;

        public ReceivablePayableBillwiseDetailPresenter(IReceivablePayableBillwiseDetailView receivablePayableBillwiseDetailView, bool IsPostBack)
        {
            objIReceivablePayableBillwiseDetailView = receivablePayableBillwiseDetailView;
            objReceivablePayableBillwiseDetailModel = new ReceivablePayableBillwiseDetailModel(objIReceivablePayableBillwiseDetailView);

            base.Init(objIReceivablePayableBillwiseDetailView, objReceivablePayableBillwiseDetailModel);


            if (IsPostBack == false)
            {
                initValues();
            }
        }

        public void initValues()
        {

            objds = objReceivablePayableBillwiseDetailModel.ReadValues();
            objIReceivablePayableBillwiseDetailView.SessionReceivablePayableBillWiseGrid = objds;
            objIReceivablePayableBillwiseDetailView.BindGrid = objds.Tables[0];
            

        }

        public void Save()
        {
            base.DBSave();
        }
    }
}

