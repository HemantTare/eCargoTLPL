using System;
using System.Data;

using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.AdminView;

/// <summary>
/// Modified : Ankit champaneriya
/// Date     : 05/12/08
/// Summary description for ProfileRightsModel
/// </summary>
/// 
namespace Raj.EC.AdminModel
{
    public class ProfileRightsModel:IModel 
    {
        private IProfileRightsView objIProfileRightsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;

        public ProfileRightsModel(IProfileRightsView profileRightsView)
        {
            objIProfileRightsView = profileRightsView;
        }      

        public DataSet ReadValues()
        { return objDS; }

        public Message Save()
        {
            Message objMessage = new Message();
          
            string ProfileRights = objIProfileRightsView.ProfileRightsDs.GetXml();

            SqlParameter[] param ={ objDAL.MakeInParams("@Login_hierarchy_Code",SqlDbType.VarChar  , 0,objIProfileRightsView.HierarchyID), 
                                   objDAL.MakeInParams("@hierarchy_Code",SqlDbType.VarChar  , 0,objIProfileRightsView.HierarchyID ), 
                                   objDAL.MakeInParams("@Profile_Id",SqlDbType.Int , 0,objIProfileRightsView.ProfileID  ), 
                                   objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0,objIProfileRightsView.MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,objIProfileRightsView.MenuHeadID ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,objIProfileRightsView.MenuGroupID ),
                                   objDAL.MakeInParams("@Applicable_To_All_Users",SqlDbType.Bit  ,0,objIProfileRightsView.IsApplicableToAll),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,7), 
                                   objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,ProfileRights )
                                  };

            objDAL.RunProc("[dbo].[Com_ADM_ProfileRights]", param);
            
        
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
           
            return objMessage;
        }

        public DataSet FillValues(int Flag)
        {
            SqlParameter[] param ={ objDAL.MakeInParams("@Login_hierarchy_Code",SqlDbType.VarChar  , 0,objIProfileRightsView.HierarchyID), 
                                   objDAL.MakeInParams("@hierarchy_Code",SqlDbType.VarChar  , 0,objIProfileRightsView.HierarchyID ), 
                                   objDAL.MakeInParams("@Profile_Id",SqlDbType.Int , 0,objIProfileRightsView.ProfileID  ), 
                                   objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0,objIProfileRightsView.MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,objIProfileRightsView.MenuHeadID ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,objIProfileRightsView.MenuGroupID ),
                                   objDAL.MakeInParams("@Applicable_To_All_Users",SqlDbType.Bit  ,0,objIProfileRightsView.IsApplicableToAll),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,Flag), 
                                   objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,""  )
                                  };

            objDAL.RunProc("[dbo].[Com_ADM_ProfileRights]", param, ref objDS);

            return objDS;
        }
    }
}