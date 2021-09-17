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
/// Summary description for FilterationPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class FilterationPresenter : Presenter
    {
        private IFilterationView objIFilterationView;
        private FilterationModel objFilterationModel;
        private DataSet objDS;

        public FilterationPresenter(IFilterationView filterationView, bool IsPostBack)
        {
            objIFilterationView = filterationView;
            objFilterationModel = new FilterationModel(objIFilterationView);

            base.Init(objIFilterationView, objFilterationModel);
            if (!IsPostBack)
            {
                initValues();
            }

        }
        private void initValues()
        {
            objDS = objFilterationModel.FillLedgerGroup();
            objIFilterationView.BindLedgerGroupName = objDS;

        }
        public void Bind_Voucher()
        {
            objIFilterationView.BindVoucherType = objFilterationModel.Get_Voucher_Type();
        }

    }
}
