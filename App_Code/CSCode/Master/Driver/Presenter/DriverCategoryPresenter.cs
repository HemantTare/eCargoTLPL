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
/// Summary description for Driver Category Presenter
/// </summary>


namespace Raj.EF.MasterPresenter
{
    public class DriverCategoryPresenter : Presenter
    {
        private IDriverCategoryView objIDriverCategoryView;
        private DriverCategoryModel objDriverCategoryModel;
        private DataSet objDS;

        public DriverCategoryPresenter(IDriverCategoryView driverCategoryView, bool isPostBack)
        {
            objIDriverCategoryView = driverCategoryView;
            objDriverCategoryModel = new DriverCategoryModel(objIDriverCategoryView);
            base.Init(objIDriverCategoryView, objDriverCategoryModel);

            if (!isPostBack)
            {
                initValues();
            }

        }

        private void initValues()
        {
            if (objIDriverCategoryView.keyID > 0)
            {
                objDS = objDriverCategoryModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIDriverCategoryView.DriverCategoryName = objDS.Tables[0].Rows[0]["Driver_Category"].ToString();
                }
            }
        }

        public void Save()
        {

            base.DBSave();
            //objDriverCategoryModel.Save();
        }
    }
}

