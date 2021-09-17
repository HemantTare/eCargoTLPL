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
/// Summary description for RegionDepartmentPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class RegionDepartmentPresenter : Presenter
    {
        private IRegionDepartmentView objIRegionDepartmentView;
        private RegionDepartmentModel objRegionDepartmentModel;
        private DataSet objDS;

        public RegionDepartmentPresenter(IRegionDepartmentView regionGeneralDetailsView, bool IsPostBack)
        {
            objIRegionDepartmentView = regionGeneralDetailsView;
            objRegionDepartmentModel = new RegionDepartmentModel(objIRegionDepartmentView);

            base.Init(objIRegionDepartmentView, objRegionDepartmentModel);

            if (!IsPostBack)
            {
                FillDepartment();
                initValues();
            }
        }

        public void FillDepartment()
        {
            objIRegionDepartmentView.BindChkListDepartment = objRegionDepartmentModel.GetDepartmentValues();
        }
        public void Save()
        {
            //base.DBSave();
            objRegionDepartmentModel.Save();
        }

        private void initValues()
        {


            if (objIRegionDepartmentView.keyID > 0)
            {
                objDS = objRegionDepartmentModel.ReadParameterValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];

                    objIRegionDepartmentView.CashLimit = DR["Cash_Limit"].ToString();
                    objIRegionDepartmentView.BankLimit = DR["Bank_Limit"].ToString();


                }
                objDS = objRegionDepartmentModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIRegionDepartmentView.BindChkListDepartment = objDS;
                }

            }



        }

    }
}

