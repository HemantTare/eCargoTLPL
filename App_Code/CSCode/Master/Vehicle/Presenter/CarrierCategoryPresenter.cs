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
/// Summary description for CarrierPresenter
/// </summary>


namespace Raj.EF.MasterPresenter
{
    public class CarrierCategoryPresenter : Presenter
    {
        private ICarrierCategoryView objICarrierCategoryView;
        private CarrierCategoryModel objCarrierCategoryModel;
        private DataSet objDS;

        public CarrierCategoryPresenter(ICarrierCategoryView carrierView, bool isPostBack)
        {
            objICarrierCategoryView = carrierView;
            objCarrierCategoryModel = new CarrierCategoryModel(objICarrierCategoryView);
            base.Init(objICarrierCategoryView, objCarrierCategoryModel);

            if (!isPostBack)
            {
                initValues();
            }

        }

        private void initValues()
        {
            if (objICarrierCategoryView.keyID > 0)
            {
                objDS = objCarrierCategoryModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objICarrierCategoryView.CarrierCategoryName = objDS.Tables[0].Rows[0]["Carrier_Category"].ToString();
                }
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}

