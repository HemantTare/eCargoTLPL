using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.AdminView;

/// <summary>
/// Summary description for MenuGroupModel
/// </summary>
///  

// =============================================
// Author:		<Author,,Ankit>
// Create date: <Create Date,,04-10-2008>
// Description:	<Description,,To Save/update the Admin Menugroup details>
// =============================================

namespace Raj.EC.AdminModel
{

    class MenuGroupModel : IModel
    {
        private IMenuGroupView objIMenuGroupView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public MenuGroupModel(IMenuGroupView menuGroupView)
        {
            objIMenuGroupView = menuGroupView;
        }

        private void _setTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }

        public DataSet FillValues()
        {
            objDAL.RunProc("[dbo].[EC_Admin_MenuGroup_FillValues]", ref objDS);
            _setTableName(new string[] { "COM_Adm_Menu_System",
                                         "COM_Adm_Menu_Type"                                                        
                                         }
                                         );
            return objDS;
        }

        public DataSet GetMenuHead()
        {
            SqlParameter[] SqlPara = { objDAL.MakeInParams("@MenuSystemId", SqlDbType.Int, 0, objIMenuGroupView.MenuSystemId) };
            objDAL.RunProc("[dbo].[EC_Admin_MenuItem_FillMenuHeadOnSystemNameChanged]", SqlPara, ref  objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] SqlPara = { objDAL.MakeInParams("@MenuGroupId", SqlDbType.Int, 0, objIMenuGroupView.keyID) };
            objDAL.RunProc("[dbo].[EC_Admin_MenuGroup_ReadValues]", SqlPara, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIMenuGroupView.keyID),
            //                                   objDAL.MakeInParams("@MenuGroupName", SqlDbType.VarChar, 50,objIMenuGroupView.MenuGroupName),
            //                                   objDAL.MakeInParams("@SerialNo", SqlDbType.Int, 0, objIMenuGroupView.SerialNo),
            //                                   objDAL.MakeInParams("@IsTransactionMenuGroup", SqlDbType.Bit, 1, objIMenuGroupView.Chk_IsTranMenuGroup),
            //                                   objDAL.MakeInParams("@MenuHeadId", SqlDbType.Int, 0, objIMenuGroupView.MenuHeadId),
            //                                   objDAL.MakeInParams("@MenuTypeId", SqlDbType.Int, 0, objIMenuGroupView.MenuTypeId),
            //                                   objDAL.MakeInParams("@MenuSystemId", SqlDbType.Int, 0, objIMenuGroupView.MenuSystemId),
            //                                   objDAL.MakeInParams("@Description", SqlDbType.VarChar, 250, objIMenuGroupView.Description),
            //                                   objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            //                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)
            //                             };

            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@MenuGroupId", SqlDbType.Int, 0, objIMenuGroupView.keyID),
                                               objDAL.MakeInParams("@MenuGroupName", SqlDbType.VarChar, 50,objIMenuGroupView.MenuGroupName),
                                               objDAL.MakeInParams("@SerialNo", SqlDbType.Int, 0, objIMenuGroupView.SerialNo),
                                               objDAL.MakeInParams("@IsTransactionMenuGroup", SqlDbType.Bit, 1, objIMenuGroupView.Chk_IsTranMenuGroup),
                                               objDAL.MakeInParams("@MenuHeadId", SqlDbType.Int, 0, objIMenuGroupView.MenuHeadId),
                                               objDAL.MakeInParams("@MenuTypeId", SqlDbType.Int, 0, objIMenuGroupView.MenuTypeId),
                                               objDAL.MakeInParams("@MenuSystemId", SqlDbType.Int, 0, objIMenuGroupView.MenuSystemId),
                                               objDAL.MakeInParams("@Description", SqlDbType.VarChar, 250, objIMenuGroupView.Description),
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)
                                         };
            objDAL.RunProc("[dbo].[EC_Admin_MenuGroup_Save]", objSqlParam);

            objMessage.messageID  = Convert.ToInt32(objSqlParam[8].Value);
            objMessage.message = Convert.ToString(objSqlParam[9].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;
        }
    }
}