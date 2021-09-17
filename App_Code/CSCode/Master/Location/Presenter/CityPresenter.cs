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
/// Summary description for CityPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CityPresenter : Presenter
    {
        private ICityView objICityView;
        private CityModel objCityModel;
        private DataSet objDS;

        public CityPresenter(ICityView cityView, bool IsPostBack)
        {
            objICityView = cityView;
            objCityModel = new CityModel(objICityView);

            base.Init(objICityView, objCityModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }
        public void FillState()
        {
            objICityView.BindState = objCityModel.GetStateValues();
        }
        public void FillLabel()
        {
            objDS = objCityModel.GetLabelValueOnStateSelection();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objICityView.RegionName = objDS.Tables[0].Rows[0]["Region_Name"].ToString();
                objICityView.CountryName = objDS.Tables[0].Rows[0]["Country_Name"].ToString();
            }
        }
        private void initValues()
        {
            FillState();

            if (objICityView.keyID > 0)
            {
                objDS =objCityModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objICityView.StateId = Util.String2Int(DR["State_Id"].ToString());
                    objICityView.CityName = DR["City_Name"].ToString();
                    FillLabel();
                    objICityView.CountryName = DR["Country_Name"].ToString();
                    objICityView.RegionName = DR["Region_Name"].ToString();
                    

                }
            }
        }

        public void Save()
        {
            base.DBSave();
            //objCityModel.Save();
        }

	}
}
