using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Author  : Ankit champaneriya
/// Date    : 12/12/08
/// Summary description for UnAppVoucherCancellationPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class ReverseVoucherPresenter : ClassLibraryMVP.General.Presenter
    {
        private IReverseVoucherView objIReverseVoucherView;
        private ReverseVoucherModel objReverseVoucherModel;
        private DataSet objDS;

        public ReverseVoucherPresenter(IReverseVoucherView ReverseVoucherView, bool isPostBack)
        {
            objIReverseVoucherView = ReverseVoucherView;
            objReverseVoucherModel = new ReverseVoucherModel(objIReverseVoucherView);

            base.Init(objIReverseVoucherView, objReverseVoucherModel);

            if (!isPostBack)
            {
                SetVoucherData();
            }

        }

        public DataSet GetLedgerParams()
        {
            return objReverseVoucherModel.Get_LedgerParams();
        }

        public void SetVoucherData()
        {
            DataSet ds = new DataSet();
            ds = objReverseVoucherModel.VoucherData();
            objIReverseVoucherView.SessionVoucherCostCentreDT = ds.Tables["VoucherCostCentre"];
            objIReverseVoucherView.SessionVoucherBillByBillDT = ds.Tables["VoucherBillByBill"];
            objIReverseVoucherView.SessionDropDownCostCentre = ds.Tables["MstCostCentre"];
            objIReverseVoucherView.SessionDropDownRefType = ds.Tables["MstRefType"];

        }
      
        public void save()
        {
            base.DBSave();
        }

    }
}