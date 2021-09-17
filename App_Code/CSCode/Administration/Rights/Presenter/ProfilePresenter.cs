using System;
using System.Data;

using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.AdminView ;
using Raj.EC.AdminModel;

/// <summary>
/// Summary description for ProfilePresenter
/// </summary>
//public class ProfilePresenter
//{
//    public ProfilePresenter()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}
namespace Raj.EC.AdminPresenter
{
    public class ProfilePresenter:ClassLibraryMVP.General.Presenter 
    {
        private IProfileView _ProfileView;
        private ProfileModel _ProfileModel;

        public ProfilePresenter(IProfileView ProfileView, bool isPostBack)
        {
            _ProfileView = ProfileView;
            _ProfileModel = new ProfileModel(_ProfileView);
            base.Init(_ProfileView, _ProfileModel);

            if (!isPostBack)
            {
                initValues();

            }
        }

        private void initValues()
        {
            _ProfileView.BindHierarchy = _ProfileModel.GetHierarchy();

            if (_ProfileView.keyID > 0)
            {
                DataSet ds;
                ds = _ProfileModel.ReadValues();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    _ProfileView.Profile_Name = ds.Tables[0].Rows[0]["Profile_Name"].ToString();
                    _ProfileView.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    _ProfileView.IsCSA = Convert.ToBoolean(ds.Tables[0].Rows[0]["Is_CSA"]);
                    //_ProfileView.Hierarchy_Name = ds.Tables[0].Rows[0]["Hierarchy_Name"].ToString();
                    _ProfileView.Hierarchy_Code = ds.Tables[0].Rows[0]["Hierarchy_Code"].ToString();
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