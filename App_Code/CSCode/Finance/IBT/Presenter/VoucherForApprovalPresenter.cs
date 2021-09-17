using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
/// <summary>
/// Summary description for VoucherForApprovalPresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class VoucherForApprovalPresenter:Presenter
    {
        private IVoucherForApprovalView objIVoucherForApprovalView;
        private VoucherForApprovalModel objVoucherForApprovalModel;
        private DataSet objDS;

        public VoucherForApprovalPresenter(IVoucherForApprovalView VoucherForApprovalView,bool isPostBack)
        {
            objIVoucherForApprovalView = VoucherForApprovalView;
            objVoucherForApprovalModel = new VoucherForApprovalModel(objIVoucherForApprovalView);

            base.Init(objIVoucherForApprovalView, objVoucherForApprovalModel);

            if (!isPostBack)
            {
                initvalues();
            }
        }


        public void initvalues()
        {
            DataSet ds = objVoucherForApprovalModel.ReadValues();

            objIVoucherForApprovalView.Set_LabelTextBox = ds;
            objIVoucherForApprovalView.Bind_VoucherGrid = ds.Tables[0];

        }
               
    }
}
