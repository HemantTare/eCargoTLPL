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
/// Summary description for AreaPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class AreaPresenter : Presenter
    {
        private IAreaView objIAreaView;
        private AreaModel objAreaModel;

        public AreaPresenter(IAreaView areaView, bool IsPostback)
        {
            objIAreaView = areaView;
            objAreaModel = new AreaModel(objIAreaView);

            base.Init(objIAreaView, objAreaModel);
        }


        public void Save()
        {
            base.DBSave();
            //objAreaModel.Save();
        }
    }
}