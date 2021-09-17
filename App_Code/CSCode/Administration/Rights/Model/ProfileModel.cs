using System;
using System.Data;
using System.Data.SqlClient;

using Raj.EC.AdminView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ProfileModel
/// </summary>
/// 
//public class ProfileModel
//{
//    public ProfileModel()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}
namespace Raj.EC.AdminModel
{
    public class ProfileModel : IModel
    {
        private IProfileView _ProfileView;
        private DAL _dalObj = new DAL();
        private DataSet _ds;
        private int _user_ID = UserManager.getUserParam().UserId;

        public ProfileModel(IProfileView iView)
        {
            _ProfileView = iView;
        }

        public DataSet GetHierarchy()
        {
            _dalObj.RunProc("[dbo].[EC_Adm_Profile_Fill]", ref _ds);
            return _ds;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] SqlPara = {_dalObj.MakeInParams("@ProfileId", SqlDbType.Int, 0, _ProfileView.keyID)
            };
            _dalObj.RunProc("dbo.EC_Admin_Profile_ReadValues", SqlPara, ref _ds);
            return _ds;
        }

        public Message Save()
        {
            Message mObj = new Message();

            SqlParameter[] sqlPara = { _dalObj.MakeInParams("@Profile_Id", SqlDbType.Int, 0, _ProfileView.keyID),
                                       _dalObj.MakeInParams("@Profile_Name",SqlDbType.VarChar, 50, _ProfileView.Profile_Name),
                                       _dalObj.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, _ProfileView.Hierarchy_Code),
                                       _dalObj.MakeInParams("@Description",SqlDbType.VarChar, 250, _ProfileView.Description),
                                       _dalObj.MakeInParams("@IsCSA",SqlDbType.Bit ,1,_ProfileView.IsCSA),
                                       _dalObj.MakeInParams("@Created_By", SqlDbType.Int, 0, _user_ID),
                                       _dalObj.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                       _dalObj.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)};

            _dalObj.RunProc("dbo.EC_Adm_Profile_Save", sqlPara);

            mObj.messageID = Convert.ToInt32(sqlPara[6].Value);
            mObj.message = Convert.ToString(sqlPara[7].Value);

            if (mObj.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return mObj;
        }
    }
}