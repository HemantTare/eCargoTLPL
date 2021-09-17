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
/// Summary description for CompanyPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CompanyPresenter : Presenter
    {
        private ICompanyView objICompanyView;
        private CompanyModel objCompanyModel;
        private DataSet objDS;

        public CompanyPresenter(ICompanyView companyView, bool IsPostback)
        {
            objICompanyView = companyView;
            objCompanyModel = new CompanyModel(objICompanyView);

            base.Init(objICompanyView, objCompanyModel);
            if (!IsPostback)
            {
                FillValues();
                
            }
        }


        public void Save()
        {
            base.DBSave();
            //objCompanyModel.Save();
        }
        public void FillValues()
        {
            objDS = objCompanyModel.FillGrid();
            objICompanyView.SessionDivision = objDS.Tables[0];            
            objICompanyView.SessionBookingType=objDS.Tables[1];
            objICompanyView.SessionPaymentType=objDS.Tables[2];
            objICompanyView.SessionBookingParametersGrid = objDS.Tables[3];            
            objICompanyView.SessionTripHireParametersGrid = objDS.Tables[4];
            objICompanyView.SessionATHGrid = objDS.Tables[5];
            objICompanyView.SessionCompanyDeliveryGrid = objDS.Tables[6];
            objICompanyView.SessionLocalCollectionVoucherGrid = objDS.Tables[7];
            objICompanyView.SessionDoorDeliveryExpenseVoucherGrid = objDS.Tables[8];
            objICompanyView.SessionLHPONatureOfPayment=objDS.Tables[9];
            objICompanyView.CompanyTripHireParametersView.BindLHPONatureOfPayment = objICompanyView.SessionLHPONatureOfPayment;
            objICompanyView.CompanyBookingParametersView.BindDivision = objICompanyView.SessionDivision;
            objICompanyView.CompanyDeliveryView.BindDivision = objICompanyView.SessionDivision;
            objICompanyView.CompanyTripHireParametersView.BindTripHireDivision = objICompanyView.SessionDivision;
            objICompanyView.CompanyTripHireParametersView.BindATHDivision = objICompanyView.SessionDivision;
            objICompanyView.LocalCollectionVoucherView.BindLocalDivision = objICompanyView.SessionDivision;
            objICompanyView.LocalCollectionVoucherView.BindDoorDivision = objICompanyView.SessionDivision;
         
          

        }
    }
}