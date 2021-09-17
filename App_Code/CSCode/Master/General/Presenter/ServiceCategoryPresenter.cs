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
/// Summary description for ServiceCategoryPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{
    public class ServiceCategoryPresenter:Presenter 
    {
        private IServiceCategoryView objIServiceCategoryView;
        private ServiceCategoryModel objServiceCategoryModel;
        private DataSet objDS;

        public ServiceCategoryPresenter(IServiceCategoryView ServiceCategoryView, bool isPostback)
        {
            objIServiceCategoryView = ServiceCategoryView;
            objServiceCategoryModel = new ServiceCategoryModel(objIServiceCategoryView);
            base.Init(objIServiceCategoryView, objServiceCategoryModel);

            if (!isPostback)
            {
                initValues();
            }
        }

        private void initValues()
        {
            if (objIServiceCategoryView.keyID > 0)
            {
                objDS = objServiceCategoryModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIServiceCategoryView.ServiceCategory = objDR["Service_Category"].ToString();
                    objIServiceCategoryView.ServiceDescription = objDR["Service_Description"].ToString();
                }
            }
        }

        public void Save()
        {
            base.DBSave();
            //objServiceCategoryModel.Save();
        }
    }
}