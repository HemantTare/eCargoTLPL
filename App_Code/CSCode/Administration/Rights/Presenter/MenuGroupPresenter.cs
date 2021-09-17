using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.AdminView;
using Raj.EC.AdminModel;

/// <summary>
/// Summary description for MenuGroupPresenter
/// </summary>
/// 
namespace Raj.EC.AdminPresenter
{
    public class MenuGroupPresenter : Presenter
    {
        private IMenuGroupView objIMenuGroupView;
        private MenuGroupModel objMenuGroupModel;
        private DataSet objDS;

        public MenuGroupPresenter(IMenuGroupView menuGroupView, bool isPostBack)
        {
            objIMenuGroupView = menuGroupView;
            objMenuGroupModel = new MenuGroupModel(objIMenuGroupView);
            base.Init(objIMenuGroupView, objMenuGroupModel);


            if (!isPostBack)
            {
                FillValues();
                initValues();
            }

        }
        public void FillValues()
        {
            objDS = objMenuGroupModel.FillValues();
            objIMenuGroupView.BindSystemName = objDS.Tables["COM_Adm_Menu_System"];
            objIMenuGroupView.BindMenuType = objDS.Tables["COM_Adm_Menu_Type"];
         }

        public void FillMenuHead()
        {
            objIMenuGroupView.BindMenuHead = objMenuGroupModel.GetMenuHead();
        }

        private void initValues()
        {

            FillMenuHead();
            if (objIMenuGroupView.keyID > 0)
            {
                objDS = objMenuGroupModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIMenuGroupView.Chk_IsTranMenuGroup = Convert.ToBoolean(DR["Is_Transaction_Menu_Group"]);
                    objIMenuGroupView.SerialNo = Util.String2Int(DR["Serial_No"].ToString());
                    objIMenuGroupView.MenuTypeId  = Util.String2Int(DR["Menu_Type_ID"].ToString());
                    objIMenuGroupView.MenuGroupName = DR["MenuGroup_Name"].ToString();
                    objIMenuGroupView.Description = DR["Description"].ToString();
                    objIMenuGroupView.MenuSystemId = Util.String2Int(DR["Menu_System_Id"].ToString());
                    objIMenuGroupView.MenuHeadId = Util.String2Int(DR["Menu_Head_Id"].ToString());
                }
            }
        }

        public void Save()
        {
            base.DBSave();
            //objMenuGroupModel.Save();
        }
    }
}

