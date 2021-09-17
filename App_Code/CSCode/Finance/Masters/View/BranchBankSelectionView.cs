using System;
using System.Data;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// Description   : This Page is For  branch bank selection 
/// Summary description for BranchBankSelectionView
/// </summary>
/// 

namespace Raj.EC.FinanceView
{
    public interface IBranchBankSelectionView : ClassLibraryMVP.General.IView
    {
        int BranchID { get;set;}
        DataSet Bind_ddl_Branch_Name { set;}
        int ChkLedgers { set;get;}
        DataSet Bind_Chk_Ledgers { set;}
        DataSet SessionChkLedgers { get;set;}
        string SelectedHerch { get;}
        void ClearVariables();
        //int Is_HO { get;set; }
    }

}
