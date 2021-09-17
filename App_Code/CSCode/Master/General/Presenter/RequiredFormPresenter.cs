using System;
using System.Data;

using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

using Raj.EC.MasterModel;
using Raj.EC.MasterView;

/// <summary>
/// Name: Ankit champaneriya
/// date : 10-11-08
/// Summary description for FormPresenter
/// </summary>
/// 

namespace Raj.EC.MasterPresenter
{
    public class RequiredFormPresenter : Presenter
    {
        private IRequiredFormView _iRequiredFormView;
        private RequiredFormModel _RequiredFormModel;
        private DataSet objDS;

        public RequiredFormPresenter(IRequiredFormView iRequiredFormView, bool isPostBack)
        {
            _iRequiredFormView = iRequiredFormView;
            _RequiredFormModel = new RequiredFormModel(_iRequiredFormView);

            base.Init(_iRequiredFormView, _RequiredFormModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {


            if (_iRequiredFormView.keyID > 0)
            {
                ReadValues();
            }
        }

        public void ReadValues()
        {
            objDS = _RequiredFormModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                _iRequiredFormView.FormName = Convert.ToString(objDS.Tables[0].Rows[0]["Form_Name"]);
                _iRequiredFormView.Description = Convert.ToString(objDS.Tables[0].Rows[0]["Description"]);
            }
        }

        public void save()
        {
            base.DBSave();
            // objItemModel.Save();
        }
    }
}