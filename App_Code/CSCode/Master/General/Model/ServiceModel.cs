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
/// Summary description for ServiceModel
/// </summary>
/// 
namespace Raj.EF.MasterModel
{
    public class ServiceModel:IModel
    {
        private IServiceView objIServiceView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public ServiceModel(IServiceView ServiceView)
        {
            objIServiceView = ServiceView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ServiceID", SqlDbType.Int, 0, objIServiceView.keyID) };
            objDAL.RunProc("rstil41.EF_Mst_Service_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ServiceID", SqlDbType.Int, 0, objIServiceView.keyID) };
            objDAL.RunProc("rstil41.EF_Mst_Service_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@Service_ID", SqlDbType.Int, 0, objIServiceView.keyID),
                                   objDAL.MakeInParams("@Service_Name", SqlDbType.VarChar, 50,objIServiceView.ServiceName), 
                                   objDAL.MakeInParams("@Service_Cotegory_ID", SqlDbType.Int, 0,objIServiceView.ServiceCategoryID), 
                                   objDAL.MakeInParams("@Service_Description", SqlDbType.VarChar, 255,objIServiceView.ServiceDescription ), 
                                   objDAL.MakeInParams("@Parent_Service_ID", SqlDbType.Int, 0,objIServiceView.ParentServiceID ), 
                                   objDAL.MakeInParams("@Est_Checking_Time", SqlDbType.Decimal, 0,objIServiceView.EstCheckingTime), 
                                   objDAL.MakeInParams("@Est_Repair_Time", SqlDbType.Decimal, 0,objIServiceView.EstRepairTime), 
                                   objDAL.MakeInParams("@ServiceTask_Details", SqlDbType.Xml,0, objIServiceView.ServiceTaskDetailsXML),
                                   objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};

            objDAL.RunProc("rstil41.EF_Mst_Service_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}
