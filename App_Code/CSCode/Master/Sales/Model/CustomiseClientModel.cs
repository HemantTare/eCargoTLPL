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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.SalesView;
/// <summary>
/// Summary description for CustomiseClientModel
/// </summary>
/// 
namespace Raj.EC.SalesModel
{
    public class CustomiseClientModel : IModel
    {
        private ICustomiseClientView objIMergeClientView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public CustomiseClientModel(ICustomiseClientView CustomiseClientView)
        {
            objIMergeClientView = CustomiseClientView;
        }

        public DataSet ReadValues()
        {
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

        SqlParameter[] objSqlParam = { objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                       objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                       objDAL.MakeInParams("@Client_Id", SqlDbType.Int,0, objIMergeClientView.ClientToBeKeptId),
                                       objDAL.MakeInParams("@MergedClient_XML",SqlDbType.Xml,0,objIMergeClientView.MergeClientXML),
                                       objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID)};

        objDAL.RunProc("[dbo].[EC_Mst_MergeClient_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if(objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            else
            {
                Common.DisplayErrors(objMessage.messageID);
            }
            return objMessage;
        }

        [AjaxPro.AjaxMethod()]
        public static DataTable Get_CustomiseClient(string SearchFor, string TableName, string KeyName, string KeyID, string othercolumns)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { objdal.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
            objdal.MakeInParams("@ClientToKeep", SqlDbType.VarChar, 20, othercolumns)};

            objdal.RunProc("EC_Mst_Get_MergeClient", sqlPara, ref ds);
            return ds.Tables[0];
        }
    }
}