using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Author  : Ankit champaneriya
/// Date    : 12/12/08
/// Summary description for VoucherViewPresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class VoucherViewPresenter :Presenter 
    {
        private IVoucherDetailsView   objIVoucherView;
        private VoucherViewModel  objVoucherViewModel;
        private DataSet objDS;

        public VoucherViewPresenter(IVoucherDetailsView  VoucherView, bool isPostBack)
        {
            objIVoucherView = VoucherView;
            objVoucherViewModel  = new VoucherViewModel(objIVoucherView );

            base.Init(objIVoucherView, objVoucherViewModel);

            if (!isPostBack)
            {

            }
        }

        public DataSet Bind_dv_UnApprovedVoucher()
        {
            return objVoucherViewModel.Fill_dv_UnApprovedVoucher();
        }

        public DataSet Bind_dg_Details()
        {
            return objVoucherViewModel.Fill_dg_Details();
        }

        public Boolean Is_Cost_Centre(int Ledger_Id)
        {
            return objVoucherViewModel.Is_Cost_Centre(Ledger_Id);
        }
        public DataSet getData(string hcode, string voucherid, int mainid)
        {
            return objVoucherViewModel.getData(hcode, voucherid, mainid);
        }
        public void save()
        {
            base.DBSave();
        }
    }
}