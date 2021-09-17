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
using Raj.EF.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

namespace Raj.EF.MasterModel
{
    public class IReligionModel:IModel 
    {
        private IReligionView objIReligionView;
        private DAL objDal = new DAL();
        private DataSet objDS;
        //private int _userID=1;
        private int _userID = UserManager.getUserParam().UserId;
        public IReligionModel(IReligionView IReligionView)
        {
            objIReligionView = IReligionView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlPara ={ objDal.MakeInParams("@Religion_ID", SqlDbType.Int, 0, objIReligionView.keyID) };
            objDal.RunProc("[rstil22].[EF_Mst_Religion_ReadValues]", objSqlPara, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlPara ={ objDal.MakeOutParams("@ERROR_CODE",SqlDbType.Int,0),
                                         objDal.MakeOutParams("@ERROR_DESC",SqlDbType.VarChar,4000),
                                         objDal.MakeInParams("@Religion_ID",SqlDbType.Int,0,objIReligionView.keyID),
                                         objDal.MakeInParams("@Religion",SqlDbType.VarChar,50,objIReligionView.Religion),
                                         objDal.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)};
                                       
                                         objDal.RunProc("[rstil22].[EF_Mst_Religion_Save]", objSqlPara);
                                         objMessage.messageID = Convert.ToInt32(objSqlPara[0].Value);
                                         objMessage.message = Convert.ToString(objSqlPara[1].Value);
                                         return objMessage;
                 

        }


    }
}
