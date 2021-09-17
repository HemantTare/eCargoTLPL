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
/// Summary description for ContainerTypeModel
/// </summary>
namespace Raj.EC.GeneralModel
{
    class ContainerTypeModel : IModel
    {
        private IContainerTypeView objIContainerTypeView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public ContainerTypeModel(IContainerTypeView containerTypeView)
        {
            objIContainerTypeView = containerTypeView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ContainerTypeId", SqlDbType.Int, 0,objIContainerTypeView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_ContainerType_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objIContainerTypeView.keyID),
                                               objDAL.MakeInParams("@ContainerTypeName", SqlDbType.VarChar, 50,objIContainerTypeView.ContainerTypeName)
                                               
                                              
                                         };


            objDAL.RunProc("[dbo].[EC_Mst_ContainerType_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

    }
}

