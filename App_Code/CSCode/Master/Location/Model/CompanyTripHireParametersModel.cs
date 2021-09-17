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
/// Summary description for CompanyTripHireParametersModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class CompanyTripHireParametersModel:IModel 
    {
        private ICompanyTripHireParametersView objICompanyTripHireParametersView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public CompanyTripHireParametersModel(ICompanyTripHireParametersView companyTripHireParametersView)
        {
            objICompanyTripHireParametersView = companyTripHireParametersView;
        }
        public DataSet ReadValues()
        {
            objDAL.RunProc("[dbo].[EC_Mst_CompanyDetails_ReadParameterValues]", ref objDS);
            return objDS;
        }                

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}