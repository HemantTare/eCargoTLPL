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
    public class UnAppVoucherCancellationPresenter : ClassLibraryMVP.General.Presenter
    {
        private IUnAppVoucherCancellationView objIUnAppVoucherCancellationView;
        private UnAppVoucherCancellationModel objUnAppVoucherCancellationModel;
        private DataSet objDS;

        public UnAppVoucherCancellationPresenter(IUnAppVoucherCancellationView unAppVoucherCancellationView, bool isPostBack)
        {
            objIUnAppVoucherCancellationView = unAppVoucherCancellationView;
            objUnAppVoucherCancellationModel = new UnAppVoucherCancellationModel(objIUnAppVoucherCancellationView);

            base.Init(objIUnAppVoucherCancellationView, objUnAppVoucherCancellationModel);

        }

        public Boolean Is_Cost_Centre(int Ledger_Id)
        {
            return objUnAppVoucherCancellationModel.Is_Cost_Centre(Ledger_Id);
        }
      
        public void save()
        {
            base.DBSave();
        }

    }
}