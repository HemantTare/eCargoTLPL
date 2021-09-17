using System;
using System.Data;

using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

/// <summary>
/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// Description   : This Page is For  branch bank selection 
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{

    public class BranchBankSelectionPresenter : ClassLibraryMVP.General.Presenter 
    {

        private IBranchBankSelectionView objIBranchBankSelectionView;
        private BranchBankSelectionModel objBranchBankSelectionModel;

        public BranchBankSelectionPresenter(IBranchBankSelectionView branchBankSelectionView, bool IsPostBack)
        {
            objIBranchBankSelectionView = branchBankSelectionView;
            objBranchBankSelectionModel = new BranchBankSelectionModel(objIBranchBankSelectionView);

            base.Init(objIBranchBankSelectionView, objBranchBankSelectionModel);

            if (!IsPostBack)
            {
                objIBranchBankSelectionView.Bind_ddl_Branch_Name = objBranchBankSelectionModel.FillValues();
                FillOnBranchNameChanged();
            }
        }
        public void save()
        {
            base.DBSave();
        }
        public void FillOnBranchNameChanged()
        {

            objIBranchBankSelectionView.Bind_Chk_Ledgers = objBranchBankSelectionModel.GetLedgersOnBranchNameChanged();

        }
    }
}