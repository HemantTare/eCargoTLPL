using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EF.MasterView;
using Raj.EF.MasterModel;

/// <summary>
/// Summary description for InsuranceCompanyPresenter
/// </summary>

namespace Raj.EF.MasterPresenter
{

    public class InsuranceCompanyPresenter :Presenter 
    {
        IInsuranceCompanyView objIInsuranceCompanyView;
        InsuranceCompanyModel objInsuranceCompanyModel;
        DataSet objDS;
        public InsuranceCompanyPresenter(IInsuranceCompanyView insuranceCompanyView ,bool isPostBack)
        {
            objIInsuranceCompanyView = insuranceCompanyView;
            objInsuranceCompanyModel = new InsuranceCompanyModel(objIInsuranceCompanyView);

            base.Init(objIInsuranceCompanyView, objInsuranceCompanyModel);

            if (!isPostBack)
            {
                initValues();
            }

        }

        private void initValues()
        {
            if (objIInsuranceCompanyView.keyID > 0)
            {
                objDS = objInsuranceCompanyModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIInsuranceCompanyView.InsuranceCompanyName = objDS.Tables[0].Rows[0]["Insurance_Company"].ToString();
                }
            }
        }

        public void Save()
        {

            base.DBSave();
            // objInsuranceCompanyModel.Save();
        }
 

    }
}
