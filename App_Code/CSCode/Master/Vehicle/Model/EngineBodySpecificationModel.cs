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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

/// <summary>
/// Summary description for EngineBodySpecificationModel
/// </summary>
namespace Raj.EF.MasterModel
{

    public class EngineBodySpecificationModel : IModel
    {
        private IEngineBodySpecificationView objIEngineBodySpecificationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
       // private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public EngineBodySpecificationModel(IEngineBodySpecificationView engionBodySpecificationView)
        {
            objIEngineBodySpecificationView = engionBodySpecificationView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("rstil41.EF_Mst_Fuel_Type_FillValues", ref objDS);
            return objDS;

        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, objIEngineBodySpecificationView .keyID)};
            objDAL.RunProc("rstil41.EF_Mst_Driver_EngineBodySpecification_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            return objMessage;
        }
    }
}