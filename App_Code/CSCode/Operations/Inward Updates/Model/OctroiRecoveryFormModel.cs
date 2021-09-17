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
using Raj.EC.OperationView;

/// <summary>
/// Summary description for OctroiRecoveryFormModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class OctroiRecoveryFormModel : IModel
    {
        private IOctroiRecoveryFormView objIOctroiRecoveryFormView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public OctroiRecoveryFormModel(IOctroiRecoveryFormView octroiRecoveryFormView)
        {
            objIOctroiRecoveryFormView = octroiRecoveryFormView;
        }
  
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIOctroiRecoveryFormView.GetGCNoXML) 
            };

            objDAL.RunProc("[dbo].[EC_Opr_OctroiRecoveryForm_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),            
            objDAL.MakeInParams("@OctroiRecoveryFormDetailsXML",SqlDbType.Xml,0,objIOctroiRecoveryFormView.OctroiRecoveryFormDetailsXML)};

            objDAL.RunProc("[dbo].[EC_Opr_OctroiRecoveryForm_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIOctroiRecoveryFormView.ClearVariables();
            return objMessage;
            

        }



    }
}
