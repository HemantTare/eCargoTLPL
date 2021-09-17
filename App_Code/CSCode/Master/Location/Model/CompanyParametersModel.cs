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
/// Summary description for CompanyParametersModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CompanyParametersModel : IModel
    {
        private ICompanyParametersView objICompanyParametersView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public CompanyParametersModel(ICompanyParametersView companyParametersView)
        {
            objICompanyParametersView = companyParametersView;
        }

        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@CompanyParametersId", SqlDbType.Int, 0, objICompanyParametersView.keyID)
            //                             };
            objDAL.RunProc("dbo.EC_Mst_CompanyDetails_ReadParameterValues",  ref objDS);
            return objDS;
        }

        public  DataSet FillValues()
        {
            objDAL.RunProc("[dbo].[EC_Mst_CompanyDetails_FillParameterValues]", ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }
    }
}



