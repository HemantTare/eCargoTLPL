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
/// Summary description for FuelTypePresenter
/// </summary>

namespace Raj.EF.MasterPresenter
{
    public class FuelTypePresenter : Presenter
    {
        private IFuelTypeView objIFuelTypeView;
        private FuelTypeModel objFuelTypeModel;
        private DataSet objDS;

        public FuelTypePresenter(IFuelTypeView fuelTypeView, bool isPostBack)
        {
            objIFuelTypeView = fuelTypeView;
            objFuelTypeModel = new FuelTypeModel(objIFuelTypeView);
            base.Init(objIFuelTypeView, objFuelTypeModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            if (objIFuelTypeView.keyID > 0)
            {
                objDS = objFuelTypeModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIFuelTypeView.FuelTypeName = objDS.Tables[0].Rows[0]["Fuel_Type"].ToString();
                }

            }

        }
        public void Save()
        {
            base.DBSave();
            //objFuelTypeModel.Save();
        }

    }

}

