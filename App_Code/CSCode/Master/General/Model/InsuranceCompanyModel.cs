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
using Raj.EF.MasterView;

/// <summary>
/// Summary description for InsuranceCompanyModel
/// </summary>
namespace Raj.EF.MasterModel 
{
    class InsuranceCompanyModel:IModel 
    {
        private IInsuranceCompanyView objIInsuranceCompanyView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public InsuranceCompanyModel(IInsuranceCompanyView InsuranceCompanyView)
	    {
            objIInsuranceCompanyView = InsuranceCompanyView;
	    }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParameter ={ 
                                            objDAL.MakeInParams("@Insurance_Company_Id",SqlDbType.Int,0,objIInsuranceCompanyView.keyID)
                                            };
            objDAL.RunProc("rstil42.EF_Mst_Insurance_Company_ReadValues", objSqlParameter, ref objDS);
            return objDS; 
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParameter ={ 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Insurance_Company_Name", SqlDbType.VarChar, 100,objIInsuranceCompanyView.InsuranceCompanyName), 
                                               objDAL.MakeInParams("@Insurance_Company_Id", SqlDbType.Int, 0, objIInsuranceCompanyView.keyID ),
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
         
                                            };

            objDAL.RunProc("rstil42.EF_Mst_Insurance_Company_Save", objSqlParameter);

            objMessage.messageID = Convert.ToInt32(objSqlParameter[0].Value);
            objMessage.message = Convert.ToString(objSqlParameter[1].Value);

            return objMessage;
        }
    }
}
