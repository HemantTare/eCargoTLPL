using System;
using System.Data;

using ClassLibraryMVP.General;
using ClassLibraryMVP;

using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 23-10-08
/// Summary description for Balance_Sheet_Presenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class BalanceSheetPresenter : Presenter
    {
        private IBalanceSheetView _Balance_Sheet_View;

        private BalanceSheetModel _Balance_Sheet_Model;
        private DataSet ds = null;


        public void save()
        {
            // base.DBSave();
        }

        public BalanceSheetPresenter(IBalanceSheetView Balance_Sheet_View, bool isPostBack)
        {
            _Balance_Sheet_View = Balance_Sheet_View;
            _Balance_Sheet_Model = new BalanceSheetModel(_Balance_Sheet_View);

            base.Init(_Balance_Sheet_View, _Balance_Sheet_Model);

            if (!isPostBack)
                _Balance_Sheet_View.CompanyName = UserManager.getUserParam().CompanyName;// "V-Trans (India) Limited";

            initValues();
        }

        public void initValues()
        {
                ds = _Balance_Sheet_Model.ReadValues();
         
            _Balance_Sheet_View.BS_DS = ds;

            if (_Balance_Sheet_View.Is_Details)
            {
                _Balance_Sheet_View.BindLiabliltyGrid = ds.Tables[2];
                _Balance_Sheet_View.BindAssetsGrid = ds.Tables[3];
            }
            else
            {
                _Balance_Sheet_View.BindLiabliltyGrid = ds.Tables[0];
                _Balance_Sheet_View.BindAssetsGrid = ds.Tables[1];
            }
            //_Balance_Sheet_View.CompanyName = UserManager.getUserParam().CompanyName;
        }

    }
}