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
using Raj.EC.GeneralView;
using Raj.EC.GeneralModel;

/// <summary>
/// Summary description for OctroiFormTypePresenter
/// </summary>
namespace Raj.EC.GeneralPresenter
{
    public class OctroiFormTypePresenter : Presenter
    {
        private IOctroiFormTypeView objIOctroiFormTypeView;
        private OctroiFormTypeModel objOctroiFormTypeModel;
        private DataSet objDS;

        public OctroiFormTypePresenter(IOctroiFormTypeView octroiFormTypeView, bool IsPostBack)
        {
            objIOctroiFormTypeView = octroiFormTypeView;
            objOctroiFormTypeModel = new OctroiFormTypeModel(objIOctroiFormTypeView);

            base.Init(objIOctroiFormTypeView, objOctroiFormTypeModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }
        private void initValues()
        {

            if (objIOctroiFormTypeView.keyID > 0)
            {
                objDS = objOctroiFormTypeModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIOctroiFormTypeView.OctroiFormType = DR["Octroi_Form_Type"].ToString();
                }
            }
        }

        public void Save()
        {
            base.DBSave();
            //objDepartmentModel.Save();
        }
    }
}
