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
    public class IVehicleBodyModel:IModel 
    {
        private IVehicleBodyView _objIVehicleBodyView;
        private DAL objDal = new DAL();
        private DataSet objDS;
        //private int _userID=1;
        private int _userID = UserManager.getUserParam().UserId;
        public IVehicleBodyModel(IVehicleBodyView objIVehicleBodyView)
        {
            _objIVehicleBodyView = objIVehicleBodyView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlPara ={objDal.MakeInParams("@Vehicle_Body_ID",SqlDbType.Int,0,_objIVehicleBodyView.keyID)};
            objDal.RunProc("[rstil22].[EF_Mst_Vehicle_Body_ReadValues]", objSqlPara, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlPara ={objDal.MakeOutParams("@ERROR_CODE",SqlDbType.Int,0),
                                        objDal.MakeOutParams("@ERROR_DESC",SqlDbType.VarChar,4000),
                                        objDal.MakeInParams("@Vehicle_Body_ID",SqlDbType.Int,0,_objIVehicleBodyView.keyID),
                                        objDal.MakeInParams("@Vehicle_Body",SqlDbType.VarChar,25,_objIVehicleBodyView.VehicleBody),
                                        objDal.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)};

            objDal.RunProc("[rstil22].[EF_Mst_Vehicle_Body_Save]", objSqlPara);

            objMessage.messageID = Convert.ToInt32(objSqlPara[0].Value);
            objMessage.message = Convert.ToString(objSqlPara[1].Value);
            return objMessage;
            
        }
    }
}
