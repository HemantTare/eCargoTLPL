using System;
using System.Data;

using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.AdminView;

/// <summary>
/// Summary description for UserRightsModel
/// </summary>
/// 
namespace Raj.EC.AdminModel
{

    public class UserRightsModel:IModel 
    {
        private IUserRightsView objIUserRightsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public UserRightsModel(IUserRightsView userRightsView)
        {
            objIUserRightsView = userRightsView;
        }
        public DataSet ReadValues()
        { return objDS; }

        public Message Save()
        {

            Message objMessage = new Message();
            string ProfileRights = objIUserRightsView.TransacationRightsDs.GetXml();

            string ProfileRights_XML = objIUserRightsView.MenuItemsRightsDs.GetXml();

            SqlParameter[] param ={objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,objIUserRightsView.UserID), 
                                   objDAL.MakeInParams("@Menu_Group_ID",SqlDbType.Int,0,objIUserRightsView.MenuGroupID),
                                   objDAL.MakeInParams("@Profile_Rights_XML",SqlDbType.Xml,0,ProfileRights_XML),
                                   objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)
                                  };

            objDAL.RunProc("[dbo].[EC_Adm_User_Rights_Save]", param);
            objMessage.messageID = Convert.ToInt32(param[3].Value);
            objMessage.message = Convert.ToString(param[4].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("[dbo].[EC_Adm_User_Rights_FillValues]", ref objDS);
            setTableName(new string[] { "Hierarchy", "Profile","User_Name", "Menu_System", "Menu_Head", "Menu_Group", "Menu_Items", "Transacation_Items" });
            return objDS;
        }

        public DataSet FillProfile()
        {
            SqlParameter[] param ={ objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, objIUserRightsView.HierarchyID) };
            objDAL.RunProc("[DBO].[EC_Adm_User_Rights_FillProfileOnHierarchyNameChanged]", param, ref objDS);
            setTableName(new string[] { "Profile" });
            return objDS;
        }

        public DataSet FillUser()
        {
            SqlParameter[] param ={ objDAL.MakeInParams("@ProfileID", SqlDbType.Int, 0, objIUserRightsView.ProfileID) };
            objDAL.RunProc("[DBO].[EC_Adm_User_Rights_FillUserOnProfileNameChanged]", param, ref objDS);
            setTableName(new string[] { "User_Name" });
            return objDS;
        }

        public DataSet FillOnMenuSystemChanged()
        {
            SqlParameter[] param ={  objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,objIUserRightsView.UserID), 
                                    objDAL.MakeInParams("@Menu_System_ID", SqlDbType.Int, 0, objIUserRightsView.MenuSystemID) };
            objDAL.RunProc("[dbo].[EC_Adm_User_Rights_FillMenuHeadandMenuGroupOnMenuSystemChanged]", param, ref objDS);
            setTableName(new string[] { "Menu_Head", "Menu_Group", "Menu_Items", "Transacation_Items" });
            return objDS;
        }

        public DataSet FillOnMenuHeadChanged()
        {
            SqlParameter[] param ={objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,objIUserRightsView.UserID), 
                                   objDAL.MakeInParams("@Menu_System_ID",SqlDbType.Int,0,objIUserRightsView.MenuSystemID),
                                   objDAL.MakeInParams("@Menu_Head_ID",SqlDbType.Int,0,objIUserRightsView.MenuHeadID)};
            objDAL.RunProc("[dbo].[EC_Adm_User_Rights_FillMenuGroupOnMenuHeadChanged]", param, ref objDS);
            setTableName(new string[] { "Menu_Group", "Menu_Items" });
            return objDS;
        }

        public DataSet FillMenuItems()
        {
            SqlParameter[] param ={ 
                                   objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,objIUserRightsView.UserID),
                                   objDAL.MakeInParams("@Menu_Group_ID",SqlDbType.Int,0,objIUserRightsView.MenuGroupID)
                                   };
            objDAL.RunProc("[dbo].[EC_Adm_User_Rights_FillGrid]", param, ref objDS);
            setTableName(new string[] { "Menu_Items" });
            return objDS;
        }

        public DataSet FillTransacationItems()
        {
            SqlParameter[] param ={ 
                                    objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,objIUserRightsView.UserID),
                                   objDAL.MakeInParams("@Menu_Group_ID",SqlDbType.Int,0,objIUserRightsView.MenuGroupID)
                                   };
            objDAL.RunProc("[dbo].[EC_Adm_User_Rights_FillTransacationGrid]", param, ref objDS);
            setTableName(new string[] { "Transacation_Items" });
            return objDS;
        }

        private void setTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }
        }
    }
}