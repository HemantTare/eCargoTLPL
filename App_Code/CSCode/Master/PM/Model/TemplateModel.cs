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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;


/// <summary>
/// Summary description for TemplateModel
/// </summary>
namespace Raj.EF.MasterModel
{
    public class TemplateModel : IModel
    {
        private ITemplateView objITemplateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public TemplateModel(ITemplateView templateView)
        {
            objITemplateView = templateView;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objITemplateView.keyID)
                                           };
           objDAL.RunProc("rstil43.EF_Master_PM_Template_Read", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objITemplateView.keyID),
                                   objDAL.MakeInParams("@TemplateName", SqlDbType.VarChar, 50,objITemplateView.TemplateName),
                                   objDAL.MakeInParams("@Description", SqlDbType.VarChar, 50,objITemplateView.Description), 
                                   objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};


            objDAL.RunProc("rstil43.EF_Master_PM_Template_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}
