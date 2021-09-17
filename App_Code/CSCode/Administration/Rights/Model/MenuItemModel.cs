using System;
using System.Data;
using System.Data.SqlClient;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.AdminView;

/// <summary>
/// Summary description for MenuItemModel
/// </summary>
/// 

namespace Raj.EC.AdminModel
{
    class MenuItemModel : IModel
    {
        private IMenuItemView objIMenuItemView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;

        public MenuItemModel(IMenuItemView menuItemView)
        {
            objIMenuItemView = menuItemView;
        }


        public DataSet GetSystemName()
        {
            objDAL.RunProc("[dbo].[EC_Admin_MenuItem_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet GetMenuHead()
        {
            SqlParameter[] SqlPara = { objDAL.MakeInParams("@MenuSystemId", SqlDbType.Int, 0, objIMenuItemView.MenuSystemId),
            objDAL.MakeInParams("@Menuitem_Type", SqlDbType.Int, 0, objIMenuItemView.MenuitemType)};
            objDAL.RunProc("[dbo].[EC_Admin_MenuItem_FillMenuHeadOnSystemNameChanged]", SqlPara, ref  objDS);
            return objDS;
        }
        public DataSet GetMenuGroup()
        {

            SqlParameter[] SqlPara ={
               objDAL.MakeInParams("@MenuSystemId",SqlDbType.Int,0,objIMenuItemView.MenuSystemId),
               objDAL.MakeInParams("@MenuHeadId",SqlDbType.Int,0,objIMenuItemView.MenuHeadId)};
            objDAL.RunProc("[dbo].[EC_Admin_MenuItem_FillMenuGroupOnMenuHeadChanged]", SqlPara, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] SqlPara ={
            objDAL.MakeInParams("@MenuItemId",SqlDbType.Int,0,objIMenuItemView.keyID)};
            objDAL.RunProc("[dbo].[EC_Admin_MenuItem_ReadValues]", SqlPara, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam =

           {
               objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,0),
               objDAL.MakeOutParams("@Error_Desc",SqlDbType.VarChar,4000),
               objDAL.MakeOutParams("@MenuItemId",SqlDbType.Int,0),
               objDAL.MakeInParams("@keyId",SqlDbType.Int,0,objIMenuItemView.keyID),
               objDAL.MakeInParams("@SerialNo",SqlDbType.Int,0,objIMenuItemView.SerialNo),
               objDAL.MakeInParams("@MenuItemName",SqlDbType.VarChar,100,objIMenuItemView.MenuItemName),
               objDAL.MakeInParams("@MenuSystemId",SqlDbType.Int,0,objIMenuItemView.MenuSystemId),
               objDAL.MakeInParams("@MenuHeadId",SqlDbType.Int,0,objIMenuItemView.MenuHeadId),
               objDAL.MakeInParams("@MenuGroupId",SqlDbType.Int,0,objIMenuItemView.MenuGroupId),
               objDAL.MakeInParams("@MenuItemLink",SqlDbType.VarChar,255,objIMenuItemView.MenuItemLink),
               objDAL.MakeInParams("@Description",SqlDbType.VarChar,255,objIMenuItemView.Description),
               objDAL.MakeInParams("@LinkUrl",SqlDbType.VarChar,255,objIMenuItemView.LinkUrl),
               objDAL.MakeInParams("@ViewUrl",SqlDbType.VarChar,255,objIMenuItemView.ViewUrl),
               objDAL.MakeInParams("@AddUrl",SqlDbType.VarChar,255,objIMenuItemView.AddUrl),
               objDAL.MakeInParams("@EditUrl",SqlDbType.VarChar,255,objIMenuItemView.EditUrl),
               objDAL.MakeInParams("@DeleteUrl",SqlDbType.VarChar,255,objIMenuItemView.DeleteUrl),
               objDAL.MakeInParams("@ReportUrl",SqlDbType.VarChar,255,objIMenuItemView.ReportUrl),
               objDAL.MakeInParams("@QueryString",SqlDbType.VarChar,100,objIMenuItemView.QueryString),
               objDAL.MakeInParams("@Table_Name",SqlDbType.VarChar,50,objIMenuItemView.TableName ),
               objDAL.MakeInParams("@Key_Column_Name",SqlDbType.VarChar,50,objIMenuItemView.KeyColumnName),
               objDAL.MakeInParams("@Is_Active",SqlDbType.Bit ,1,objIMenuItemView.Is_Active),
               objDAL.MakeInParams("@Is_PopUp_From_Link",SqlDbType.Bit ,1,objIMenuItemView.Is_PopUp_From_Link ),
               objDAL.MakeInParams("@MenuItem_Code",SqlDbType.VarChar ,10,objIMenuItemView.MenuItemCode)
           };

            objDAL.RunProc("[dbo].[EC_Admin_MenuItem_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message  = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                int MenuItemId = Convert.ToInt32(objSqlParam[0].Value);

                if (objIMenuItemView.keyID < 0)
                {
                    string Msg = "Menu Item Id is : " + MenuItemId;
                    string Url = "Administration/Rights/FrmMenuItem.aspx";
                    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString(Url));
                }
            }

            return objMessage;
        }

    }
}
