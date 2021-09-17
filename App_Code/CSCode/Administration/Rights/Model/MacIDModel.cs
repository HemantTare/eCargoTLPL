using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.AdminView;
/// <summary>
/// Summary description for MacIDModel
/// </summary>

namespace Raj.EC.AdminModel
{
public class MacIDModel : IModel
    {
        private IMacIDView objIMacIDView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public MacIDModel(IMacIDView MacIDView)
        {
            objIMacIDView = MacIDView;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] SqlPara = { objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,objIMacIDView.Hierarchy_Code),
                                           objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0, objIMacIDView.Main_ID )};
            objDAL.RunProc("[dbo].[Com_Adm_MacID_ReadValues]", SqlPara, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,objIMacIDView.Hierarchy_Code),
                                           objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0, objIMacIDView.Main_ID ),
                                           objDAL.MakeInParams("@Mac_ID_XML", SqlDbType.Xml,0, objIMacIDView.Mac_ID_XML),
                                               objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)
                                         };
            objDAL.RunProc("[dbo].[Com_Adm__MacID_Save]", objSqlParam);

            objMessage.messageID  = Convert.ToInt32(objSqlParam[4].Value);
            objMessage.message = Convert.ToString(objSqlParam[5].Value);

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