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

namespace Raj.EF.MasterPresenter
{
    public class DriverLicenseCategoryPresenter:Presenter 
    {
        private IDriverLicenseCategoryView objIDriverLicenseCategoryView;
        private DriverLicenseCategoryModel objDriverLicenseCategoryModel;
        private DataSet objDS;

        public DriverLicenseCategoryPresenter(IDriverLicenseCategoryView DriverLicenseCategoryView, bool isPostBack)
        {
            objIDriverLicenseCategoryView = DriverLicenseCategoryView;
            objDriverLicenseCategoryModel = new DriverLicenseCategoryModel(objIDriverLicenseCategoryView);
            base.Init(objIDriverLicenseCategoryView, objDriverLicenseCategoryModel);

            if (!isPostBack)
            {
                initValues();
            }


        }
        public void initValues()
        {
            if (objIDriverLicenseCategoryView.keyID > 0)
            {
                objDS = objDriverLicenseCategoryModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIDriverLicenseCategoryView.DriverLicenseCategory = objDS.Tables[0].Rows[0]["License_Category"].ToString();
                }
            }
        }
        public void Save()
        {
            base.DBSave();
            //objDriverLicenseCategoryModel.Save();
        }

    }
}
