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
/// Summary description for ContainerTypePresenter
/// </summary>
namespace Raj.EC.GeneralPresenter
{
    public class ContainerTypePresenter : Presenter
    {
        private IContainerTypeView objIContainerTypeView;
        private ContainerTypeModel objContainerTypeModel;
        private DataSet objDS;

        public ContainerTypePresenter(IContainerTypeView containerTypeView, bool IsPostBack)
        {
            objIContainerTypeView = containerTypeView;
            objContainerTypeModel = new ContainerTypeModel(objIContainerTypeView);

            base.Init(objIContainerTypeView, objContainerTypeModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }
        private void initValues()
        {

            if (objIContainerTypeView.keyID > 0)
            {
                objDS = objContainerTypeModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIContainerTypeView.ContainerTypeName = DR["Container_Type"].ToString();
                }
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
