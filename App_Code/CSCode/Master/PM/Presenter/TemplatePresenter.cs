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
/// Summary description for TemplatePresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class TemplatePresenter : Presenter
    {
        private ITemplateView objITemplateView;
        private TemplateModel objTemplateModel;
        private DataSet objDS;

        public TemplatePresenter(ITemplateView templateView, bool isPostBack)
        {
            objITemplateView = templateView;
            objTemplateModel = new TemplateModel(objITemplateView);
            base.Init(objITemplateView, objTemplateModel);

            if (!isPostBack)
            {
                initValues();
            }

        }

        private void initValues()
        {
            if (objITemplateView.keyID > 0)
            {
                objDS = objTemplateModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objITemplateView.TemplateName = objDR["Template_Name"].ToString();
                    objITemplateView.Description = objDR["Template_Description"].ToString();
                }
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}

