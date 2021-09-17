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
using Raj.EC.MasterView;


/// <summary>
/// Summary description for CityModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CityModel : IModel
    {
        private ICityView objICityView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;
        //private int _userID = UserManager.getUserParam().UserId;

        public CityModel(ICityView cityView)
        {
            objICityView = cityView;
        }
        public DataSet GetStateValues()
        {

            objDAL.RunProc("[dbo].[EC_Mst_City_FillStateValues]", ref objDS);
            return objDS;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@CityId", SqlDbType.Int, 0, objICityView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_City_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetLabelValueOnStateSelection()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@StateId", SqlDbType.Int, 0, objICityView.StateId)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_City_FillLabelValues]", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objICityView.keyID),
                                               objDAL.MakeInParams("@CityName", SqlDbType.VarChar, 50,objICityView.CityName), 
                                               objDAL.MakeInParams("@StateId", SqlDbType.Int,0, objICityView.StateId),                                              
                                               objDAL.MakeInParams("@UserId", SqlDbType.Int,0,  _userID)
                                              
                                         };


            objDAL.RunProc("EC_Mst_City_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
       
	}
}
