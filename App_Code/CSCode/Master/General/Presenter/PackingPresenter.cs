using System;
using System.Data;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP.General;

/// <summary>
/// Name: Ankit champaneriya
/// date : 20/11/08
/// Summary description for PackingPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{
    public class PackingPresenter:Presenter 
    {
        private IPackingView _iPackingView;
        private PackingModel _PackingModel;
        private DataSet objDS;

        public PackingPresenter(IPackingView iPackingView,bool IsPostBack)
        {
            _iPackingView = iPackingView;
            _PackingModel = new PackingModel(_iPackingView);
            base.Init(_iPackingView, _PackingModel);
            if (!IsPostBack)
            {
                initValues();
            }

        }
        private void initValues()
        {
            if (_iPackingView.keyID > 0)
            {
                ReadValues();
            }
        }

        public void ReadValues()
        {
            objDS = _PackingModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                _iPackingView .PackingType  = Convert.ToString(objDS.Tables[0].Rows[0]["Packing_Type"]);
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}