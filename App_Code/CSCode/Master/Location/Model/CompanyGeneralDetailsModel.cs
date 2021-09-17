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
/// Summary description for CompanyGeneralDetailsModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CompanyGeneralDetailsModel : IModel
    {
        private ICompanyGeneralDetailsView objICompanyGeneralDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;
        private bool _IsActivateDivision = true;
        //private bool _IsActivateDivision = Convert.ToBoolean(Param.getUserParam().Is_Activate_Divisions);

        //private int _userID = UserManager.getUserParam().UserId;

        public CompanyGeneralDetailsModel(ICompanyGeneralDetailsView companyGeneralDetailsView)
        {
            objICompanyGeneralDetailsView = companyGeneralDetailsView;
        }

        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@CompanyId", SqlDbType.Int, 0, objICompanyGeneralDetailsView.keyID)
            //                             };
            objDAL.RunProc("dbo.EC_Mst_CompanyDetails_ReadValues",ref objDS);
            return objDS;
        }       
       

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }
    }
}
