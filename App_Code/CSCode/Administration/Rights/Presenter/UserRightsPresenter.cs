using System;
using System.Data;

using ClassLibraryMVP ;
using ClassLibraryMVP.General;
using Raj.EC.AdminView;
using Raj.EC.AdminModel;

/// <summary>
/// Summary description for UserRightsPresenter
/// </summary>
/// 
namespace Raj.EC.AdminPresenter
{
    public class UserRightsPresenter : Presenter
    {
        private IUserRightsView objIUserRightsView;
        private UserRightsModel objUserRightsModel;
        private DataSet objDS;

        public UserRightsPresenter(IUserRightsView userRightsView, bool isPostBack)
        {
            objIUserRightsView = userRightsView;
            objUserRightsModel = new UserRightsModel(objIUserRightsView);
            base.Init(objIUserRightsView, objUserRightsModel);

            if (!isPostBack)
            {
                Get_All_DropDown();
                initValues();
            }
        }
        private void initValues()
        {
            if (objIUserRightsView.keyID > 0)
            {
                objDS = objUserRightsModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    //objIProfileRightsView.TyreSize = objDS.Tables[0].Rows[0]["Tyre_Size"].ToString();
                    //objIProfileRightsView.TyreDescription = objDS.Tables[0].Rows[0]["Description"].ToString();
                    //objIProfileRightsView.BindTyreSizeGrid = objTyreSizeModel.BindGrid();
                }
            }
        }

        private void Get_All_DropDown()
        {
            objDS = objUserRightsModel.FillValues();
            objIUserRightsView.Bind_ddl_Hierarchy = objDS.Tables["Hierarchy"];
            objIUserRightsView.Bind_ddl_Profile = objDS.Tables["Profile"];
            objIUserRightsView.Bind_ddl_User = objDS.Tables["User_Name"];
            objIUserRightsView.Bind_ddl_MenuSystem = objDS.Tables["Menu_System"];
            objIUserRightsView.Bind_ddl_MenuHead = objDS.Tables["Menu_Head"];
            objIUserRightsView.Bind_ddl_MenuGroup = objDS.Tables["Menu_Group"];
            objIUserRightsView.Bind_dg_MenuItemsRights = objDS.Tables["Menu_Items"];
            objIUserRightsView.Bind_dg_TransacationRights = objDS.Tables["Transacation_Items"];
        }

        public void Get_OnHierarchyChanged()
        {
            objDS = objUserRightsModel.FillProfile();
            objIUserRightsView.Bind_ddl_Profile = objDS.Tables["Profile"];

        }

        public void Get_OnProfileChanged()
        {
            objDS = objUserRightsModel.FillUser();
            objIUserRightsView.Bind_ddl_User = objDS.Tables["User_Name"];

        }



        public void Get_OnMenuSystemChanged()
        {
            objDS = objUserRightsModel.FillOnMenuSystemChanged();
            objIUserRightsView.Bind_ddl_MenuHead = objDS.Tables["Menu_Head"];
            objIUserRightsView.Bind_ddl_MenuGroup = objDS.Tables["Menu_Group"];
            objIUserRightsView.Bind_dg_MenuItemsRights = objDS.Tables["Menu_Items"];
            objIUserRightsView.Bind_dg_TransacationRights = objDS.Tables["Transacation_Items"];
        }

        public void Get_OnMenuHeadChanged()
        {
            objDS = objUserRightsModel.FillOnMenuHeadChanged();
            objIUserRightsView.Bind_ddl_MenuGroup = objDS.Tables["Menu_Group"];
            //   objIProfileRightsView.Bind_dg_MenuItemsRights = objDS.Tables["Menu_Items"];
            //   objIProfileRightsView.Bind_dg_TransacationRights = objDS.Tables["Transacation_Items"];

        }

        public void Get_OnMenuGroupChanged()
        {
            objDS = objUserRightsModel.FillMenuItems();
            objIUserRightsView.Bind_dg_MenuItemsRights = objDS.Tables["Menu_Items"];
            objDS = objUserRightsModel.FillTransacationItems();
            objIUserRightsView.Bind_dg_TransacationRights = objDS.Tables["Transacation_Items"];
        }
        public void Save()
        {
             base.DBSave();
            //objUserRightsModel.Save();
        }
    }
}