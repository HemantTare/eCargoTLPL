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
using Raj.EC.GeneralView;


/// <summary>
/// Summary description for DepartmentModel
/// </summary>
namespace Raj.EC.GeneralModel
{
    class DepartmentModel : IModel
    {
        private IDepartmentView objIDepartmentView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public DepartmentModel(IDepartmentView departmentView)
        {
            objIDepartmentView = departmentView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@DepartmentId", SqlDbType.Int, 0,objIDepartmentView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_Department_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objIDepartmentView.keyID),
                                               objDAL.MakeInParams("@DepartmentName", SqlDbType.VarChar, 50,objIDepartmentView.DepartmentName)
                                               
                                              
                                         };


            objDAL.RunProc("[dbo].[EC_Mst_Department_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

	}
}
