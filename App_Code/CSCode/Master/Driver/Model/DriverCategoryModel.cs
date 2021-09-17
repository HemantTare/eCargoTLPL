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
using ClassLibraryMVP  ;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;


/// <summary>
/// Summary description for Driver Category Model
/// </summary>


namespace Raj.EF.MasterModel
{
    class DriverCategoryModel : IModel
    {
        private IDriverCategoryView objIDriverCategoryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public DriverCategoryModel(IDriverCategoryView driverCategoryView)
        {
            objIDriverCategoryView = driverCategoryView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Driver_Category_Id", SqlDbType.Int, 0, objIDriverCategoryView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_Driver_Category_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Driver_Category", SqlDbType.VarChar, 50,objIDriverCategoryView.DriverCategoryName), 
                                               objDAL.MakeInParams("@Driver_Category_Id", SqlDbType.Int, 0, objIDriverCategoryView.keyID ),
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
                                         };


            objDAL.RunProc("rstil43.EF_Mst_Driver_Category_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }




    }
}
