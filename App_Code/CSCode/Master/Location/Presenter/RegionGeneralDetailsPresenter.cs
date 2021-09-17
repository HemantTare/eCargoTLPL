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
/// Summary description for RegionGeneralDetailsPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class RegionGeneralDetailsPresenter : Presenter
    {
        private IRegionGeneralDetailsView objIRegionGeneralDetailsView;
        private RegionGeneralDetailsModel objRegionGeneralDetailsModel;
        private DataSet objDS;

        public RegionGeneralDetailsPresenter(IRegionGeneralDetailsView regionGeneralDetailsView, bool IsPostBack)
        {
            objIRegionGeneralDetailsView = regionGeneralDetailsView;
            objRegionGeneralDetailsModel = new RegionGeneralDetailsModel(objIRegionGeneralDetailsView);

            base.Init(objIRegionGeneralDetailsView, objRegionGeneralDetailsModel);

            if (!IsPostBack)
            {
                FillLabelvalues();
                FillDivision();                
                initValues();
            }
        }

        public void FillDivision()
        {
            objIRegionGeneralDetailsView.BindChkListDivision = objRegionGeneralDetailsModel.GetDivision();
        }
        public void Save()
        {
            base.DBSave();
            //objRegionGeneralDetailsModel.Save();
        }

        public void FillLabelvalues()
        {
            objDS = objRegionGeneralDetailsModel.GetLabelValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIRegionGeneralDetailsView.ZoneCode = objDS.Tables[0].Rows[0]["Region_Code"].ToString();
                objIRegionGeneralDetailsView.ZoneName = objDS.Tables[0].Rows[0]["Region_Name"].ToString();
            }
        }
        
        private void initValues()
        {


            if (objIRegionGeneralDetailsView.keyID > 0)
            {
                objDS = objRegionGeneralDetailsModel.ReadGeneralValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];

                    objIRegionGeneralDetailsView.ZoneCode = DR["Region_Code"].ToString();
                    objIRegionGeneralDetailsView.ZoneName = DR["Region_Name"].ToString();
                    objIRegionGeneralDetailsView.ContactPerson = DR["Contact_Person"].ToString();
                    objIRegionGeneralDetailsView.AddressView.AddressLine1 = DR["Adress_1"].ToString();
                    objIRegionGeneralDetailsView.AddressView.AddressLine2 = DR["Adress_2"].ToString();
                    objIRegionGeneralDetailsView.AddressView.PinCode = DR["Pin_Code"].ToString();
                    objIRegionGeneralDetailsView.AddressView.CityId = Util.String2Int(DR["City_Id"].ToString());
                    objIRegionGeneralDetailsView.AddressView.Phone1 = DR["Phone_1"].ToString();
                    objIRegionGeneralDetailsView.AddressView.Phone2 = DR["Phone_2"].ToString();
                    objIRegionGeneralDetailsView.AddressView.StdCode = DR["Std_Code"].ToString();
                    objIRegionGeneralDetailsView.AddressView.FaxNo = DR["Fax"].ToString();
                    objIRegionGeneralDetailsView.AddressView.EmailId = DR["Email_Id"].ToString();
                    objIRegionGeneralDetailsView.StartedOn=Convert.ToDateTime(DR["Started_On"].ToString());



                }
                objDS = objRegionGeneralDetailsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIRegionGeneralDetailsView.BindChkListDivision = objDS;
                }

            }



        }

    }
}
