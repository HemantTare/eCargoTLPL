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
/// Summary description for DepartmentPresenter
/// </summary>
namespace Raj.EC.GeneralPresenter
{
    public class DepartmentPresenter : Presenter
    {
        private IDepartmentView objIDepartmentView;
        private DepartmentModel objDepartmentModel;
        private DataSet objDS;

        public DepartmentPresenter(IDepartmentView departmentView, bool IsPostBack)
        {
            objIDepartmentView = departmentView;
            objDepartmentModel = new DepartmentModel(objIDepartmentView);

            base.Init(objIDepartmentView, objDepartmentModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }
        private void initValues()
        {

            if (objIDepartmentView.keyID > 0)
            {
                objDS = objDepartmentModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIDepartmentView.DepartmentName = DR["Department_Name"].ToString();
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
