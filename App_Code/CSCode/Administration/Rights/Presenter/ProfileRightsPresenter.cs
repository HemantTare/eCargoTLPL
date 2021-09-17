using System;
using System.Data;

using ClassLibraryMVP ;
using ClassLibraryMVP .General;
using Raj.EC.AdminView;
using Raj.EC.AdminModel;

/// <summary>
/// Modified  : Ankit champaneriya
/// Date      : 05/12/08
/// Summary description for ProfileRightsPresenter
/// </summary>
/// 
namespace Raj.EC.AdminPresenter
{
    public class ProfileRightsPresenter: Presenter 
    {
        private IProfileRightsView objIProfileRightsView;
        private ProfileRightsModel objProfileRightsModel;
        private DataSet objDS;

        public ProfileRightsPresenter(IProfileRightsView profileRightsView, bool isPostBack)
        {
            objIProfileRightsView = profileRightsView;
            objProfileRightsModel = new ProfileRightsModel(objIProfileRightsView);
            base.Init(objIProfileRightsView, objProfileRightsModel);

            if (!isPostBack)
            {
                Get_All_DropDown();
                initValues();
            }
        }
        private void initValues()
        {
            if (objIProfileRightsView.keyID > 0)
            {
                objDS = objProfileRightsModel.ReadValues();
            }
        }

        private void Get_All_DropDown()
        {
            objDS = objProfileRightsModel.FillValues(1);
            objIProfileRightsView.Bind_ddl_Hierarchy = objDS.Tables[0];
            objDS = objProfileRightsModel.FillValues(2);
            objIProfileRightsView.Bind_ddl_Profile = objDS.Tables[0];
            objDS = objProfileRightsModel.FillValues(3);
            objIProfileRightsView.Bind_ddl_MenuSystem = objDS.Tables[0];
            objDS = objProfileRightsModel.FillValues(4);
            objIProfileRightsView.Bind_ddl_MenuHead = objDS.Tables[0];
            objDS = objProfileRightsModel.FillValues(5);
            objIProfileRightsView.Bind_ddl_MenuGroup = objDS.Tables[0];
            objDS = objProfileRightsModel.FillValues(6);
            objIProfileRightsView.Bind_dg_ProfileRights  = objDS.Tables[0];
        }

        public void Get_OnHierarchyChanged()
        {
            objDS = objProfileRightsModel.FillValues(2);
            objIProfileRightsView.Bind_ddl_Profile = objDS.Tables[0];
        }

        public void Get_OnMenuSystemChanged()
        {
            objDS = objProfileRightsModel.FillValues(4);
            objIProfileRightsView.Bind_ddl_MenuHead = objDS.Tables[0];

            objDS = objProfileRightsModel.FillValues(5);
            objIProfileRightsView.Bind_ddl_MenuGroup = objDS.Tables[0];
            objDS = objProfileRightsModel.FillValues(6);
            objIProfileRightsView.Bind_dg_ProfileRights = objDS.Tables[0];
        }

        public void Get_OnMenuHeadChanged()
        {
            objDS = objProfileRightsModel.FillValues(5);
            objIProfileRightsView.Bind_ddl_MenuGroup = objDS.Tables[0];
        }

        public void Get_OnMenuGroupChanged()
        {
            objDS = objProfileRightsModel.FillValues(6);
            objIProfileRightsView.Bind_dg_ProfileRights = objDS.Tables[0];
        }

        public void Save()
        {
            base.DBSave();
        }

    }
}