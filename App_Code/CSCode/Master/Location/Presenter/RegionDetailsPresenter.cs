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
/// Summary description for RegionDetailsPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class RegionDetailsPresenter : Presenter
    {
        private IRegionDetailsView objIRegionDetailsView;
        private RegionDetailsModel objRegionDetailsModel;

        public RegionDetailsPresenter(IRegionDetailsView regionDetailsView, bool IsPostback)
        {
            objIRegionDetailsView = regionDetailsView;
            objRegionDetailsModel = new RegionDetailsModel(objIRegionDetailsView);

            base.Init(objIRegionDetailsView, objRegionDetailsModel);
        }


        public void Save()
        {
            base.DBSave();
            //objRegionDetailsModel.Save();
        }
    }
}