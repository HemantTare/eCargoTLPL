using System;
using System.Data;

using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP ;

/// <summary>
/// Summary description for WeightRangePresenter
/// </summary>
/// 
/// <summary>
/// Author        : Ankit
/// Created On    : 11/10/2008
/// Description   : This Page is For General Contract range
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{
    public class ContractRangePresenter : ClassLibraryMVP.General.Presenter
    {
        private IContractRangeView _iContractRangeView;
        private ContractRangeModel _ContractRangeModel;

        public ContractRangePresenter(IContractRangeView  iContractRangeView, bool isPostBack)
        {
            _iContractRangeView = iContractRangeView;
            _ContractRangeModel = new ContractRangeModel(_iContractRangeView);
            base.Init(_iContractRangeView , _ContractRangeModel );

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            if (_iContractRangeView.keyID > 0)
            {
                DataSet ds;
                ds = _ContractRangeModel.ReadValues();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    _iContractRangeView.From_Unit = Convert.ToInt32(ds.Tables[0].Rows[0]["From_Unit"].ToString());
                    _iContractRangeView.To_Unit = Convert.ToInt32(ds.Tables[0].Rows[0]["To_Unit"].ToString());
                }
            }
        }

        public void Save()
        {
                base.DBSave();
        }

    }
}