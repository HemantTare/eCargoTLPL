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
/// Summary description for OctroiFormTypeModel
/// </summary>
namespace Raj.EC.GeneralModel
{
    class OctroiFormTypeModel : IModel
    {
        private IOctroiFormTypeView objIOctroiFormTypeView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public OctroiFormTypeModel(IOctroiFormTypeView octroiFormTypeView)
        {
            objIOctroiFormTypeView = octroiFormTypeView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@OctroiFormTypeId", SqlDbType.Int, 0,objIOctroiFormTypeView.keyID)
                                         };
            objDAL.RunProc("Ec_Mst_OctroiUpdateFormType_ReadValue", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objIOctroiFormTypeView.keyID),
                                               objDAL.MakeInParams("@OctroiFormType", SqlDbType.VarChar, 50,objIOctroiFormTypeView.OctroiFormType)
                                               
                                              
                                         };


             objDAL.RunProc("EC_Mst_OctroiUpdateFormType_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

    }
}

