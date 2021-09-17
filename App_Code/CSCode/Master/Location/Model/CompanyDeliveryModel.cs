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
/// Summary description for CompanyDeliveryModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CompanyDeliveryModel : IModel
    {
        private ICompanyDeliveryView objICompanyDeliveryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public CompanyDeliveryModel(ICompanyDeliveryView companyDeliveryView)
        {
            objICompanyDeliveryView = companyDeliveryView;
        }   
            
       

        public DataSet ReadValues()
        {
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }
    }
}
