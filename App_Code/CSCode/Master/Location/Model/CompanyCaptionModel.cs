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
/// Summary description for CompanyCaptionModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CompanyCaptionModel : IModel
    {
        private ICompanyCaptionView objICompanyCaptionView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public CompanyCaptionModel(ICompanyCaptionView companyCaptionView)
        {
            objICompanyCaptionView = companyCaptionView;
        }

        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objICompanyTDSFBTDetailsView.keyID)
            //                             };
            objDAL.RunProc("EC_Mst_CompanyCaption_ReadValues", ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }

	}
}
