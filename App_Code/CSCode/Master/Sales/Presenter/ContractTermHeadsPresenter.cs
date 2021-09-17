using System;
using System.Data;

using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP.General;

/// <summary>
/// Author:		Ankit
/// Create date: 13-10-2008
/// Summary description for ContractTermHeadsPresenter
/// </summary>

namespace Raj.EC.MasterPresenter
{
    public class ContractTermHeadsPresenter : Presenter
    {
        private IContractTermHeadsView _iContractTermHeadsView;
        private ContractTermHeadsModel _ContractTermHeadsModel;

        public ContractTermHeadsPresenter(IContractTermHeadsView iContractTermHeadsView, bool isPostBack)
        {
            _iContractTermHeadsView = iContractTermHeadsView;
            _ContractTermHeadsModel = new ContractTermHeadsModel(_iContractTermHeadsView);
            base.Init(_iContractTermHeadsView, _ContractTermHeadsModel);

            if (!isPostBack)
            {
                initValues();
            }
        }
        private void initValues()
        {

            if (_iContractTermHeadsView.keyID > 0)
            {
                DataSet ds;
                ds = _ContractTermHeadsModel.ReadValues();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    _iContractTermHeadsView.TermHead = ds.Tables[0].Rows[0]["Term_Head"].ToString();
                    _iContractTermHeadsView.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
            }
        }

        public void Save()
        {
            //_ProfileModel.Save();
            base.DBSave();
        }

    }
}