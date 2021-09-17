using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;


/// <summary>
/// Summary description for ServiceCategoryModel
/// </summary>
/// 
namespace Raj.EF.MasterModel
{
    public class ServiceCategoryModel:IModel
    {
        private IServiceCategoryView objIServiceCategoryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;


        public ServiceCategoryModel(IServiceCategoryView ServiceCategoryView)
        {
            objIServiceCategoryView = ServiceCategoryView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ServiceCategoryID", SqlDbType.Int, 0, objIServiceCategoryView.keyID) };
            objDAL.RunProc("rstil41.EF_Mst_Service_Category_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@Service_Category_ID", SqlDbType.Int, 0, objIServiceCategoryView.keyID),
                                   objDAL.MakeInParams("@Service_Category", SqlDbType.VarChar, 50,objIServiceCategoryView.ServiceCategory), 
                                   objDAL.MakeInParams("@Service_Description", SqlDbType.VarChar, 255,objIServiceCategoryView.ServiceDescription), 
                                   objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};

            objDAL.RunProc("rstil41.EF_Mst_Service_Category_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}