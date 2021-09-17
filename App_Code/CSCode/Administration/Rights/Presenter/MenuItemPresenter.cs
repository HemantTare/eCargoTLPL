using System;
using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using ClassLibrary;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.AdminView;
using Raj.EC.AdminModel;

/// <summary>
/// Summary description for MenuItemPresenter
/// </summary>
namespace Raj.EC.AdminPresenter
{
    public class MenuItemPresenter : Presenter
    {
        private IMenuItemView objIMenuItemView;
        private MenuItemModel objMenuItemModel;
        private DataSet objDS;

        public MenuItemPresenter(IMenuItemView menuItemView, bool isPostBack)
        {
            objIMenuItemView = menuItemView;
            objMenuItemModel = new MenuItemModel(objIMenuItemView);
            base.Init(objIMenuItemView, objMenuItemModel);

            if (!isPostBack)
            {
                initValues();
            }
        }
        public void FillMenuSystem()
        {
            objIMenuItemView.BindMenuSystem = objMenuItemModel.GetSystemName();
        }

        public void FillMenuHead()
        {
            objIMenuItemView.BindMenuHead = objMenuItemModel.GetMenuHead();
        }

        public void FillMenuGroup()
        {
            objIMenuItemView.BindMenuGroup = objMenuItemModel.GetMenuGroup();
        }

        private void initValues()
        {
            if (objIMenuItemView.keyID > 0)
            {
                objDS = objMenuItemModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIMenuItemView.SerialNo = Util.String2Int(DR["Serial_No"].ToString());
                    objIMenuItemView.MenuItemName = DR["MenuItem_Name"].ToString();
                    FillMenuSystem();
                    objIMenuItemView.MenuSystemId = Util.String2Int(DR["Menu_System_Id"].ToString());
                    FillMenuHead();
                    objIMenuItemView.MenuHeadId = Util.String2Int(DR["Menu_Head_Id"].ToString());
                    FillMenuGroup();
                    objIMenuItemView.MenuGroupId = Util.String2Int(DR["MenuGroup_ID"].ToString());
                    objIMenuItemView.MenuItemLink = DR["MenuItem_Link"].ToString();
                    objIMenuItemView.Description = DR["Description"].ToString();
                    objIMenuItemView.ViewUrl = DR["viewurl"].ToString();
                    objIMenuItemView.LinkUrl = DR["linkurl"].ToString();
                    objIMenuItemView.AddUrl = DR["addurl"].ToString();
                    objIMenuItemView.EditUrl = DR["editurl"].ToString();
                    objIMenuItemView.DeleteUrl = DR["deleteurl"].ToString();
                    objIMenuItemView.ReportUrl = DR["reporturl"].ToString();
                    objIMenuItemView.QueryString = DR["querystring"].ToString();
                    objIMenuItemView.TableName = DR["Table_Name"].ToString();
                    objIMenuItemView.KeyColumnName = DR["Key_Column_Name"].ToString();
                    objIMenuItemView.Is_Active  = (Boolean)DR["Is_Active"];
                    objIMenuItemView.Is_PopUp_From_Link = (Boolean)DR["Is_PopUp_From_Link"];
                    objIMenuItemView.MenuItemCode = DR["MenuItem_Code"].ToString();
                }
            }
            else
            {
                FillMenuSystem();
                FillMenuHead();
                FillMenuGroup();
            }
        }

        public void Save()
        {
            base.DBSave();
            //objMenuItemModel.Save();
        }
    }
}

