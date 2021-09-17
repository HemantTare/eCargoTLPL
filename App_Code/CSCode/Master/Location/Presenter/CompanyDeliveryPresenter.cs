using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for CompanyDeliveryPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CompanyDeliveryPresenter : Presenter
    {
        private ICompanyDeliveryView objICompanyDeliveryView;
        private CompanyDeliveryModel objCompanyDeliveryModel;
        private DataSet objDS;

        public CompanyDeliveryPresenter(ICompanyDeliveryView companyDeliveryView, bool IsPostBack)
        {
            objICompanyDeliveryView = companyDeliveryView;
            objCompanyDeliveryModel = new CompanyDeliveryModel(objICompanyDeliveryView);

            base.Init(objICompanyDeliveryView, objCompanyDeliveryModel);

            if (!IsPostBack)
            {
               initValues();
            }
        }

        public void Save()
        {

         }
        
       
        private void initValues()
        {
                       
        }
    }
}

