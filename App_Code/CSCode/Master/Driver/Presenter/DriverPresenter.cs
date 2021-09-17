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

using Raj.EF.MasterView;
using Raj.EF.MasterModel;
/// <summary>
/// Summary description for DriverPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{
    public class DriverPresenter : Presenter 
    {
        private IDriverView objIDriverView;
        private DriverModel objDriverModel;

        public DriverPresenter(IDriverView DriverView, bool isPostback)
        {
            objIDriverView = DriverView;
            objDriverModel = new DriverModel(objIDriverView);

            base.Init(objIDriverView, objDriverModel);
        }


        public void Save()
        {
            base.DBSave();
            //objDriverModel.Save();
        }
    }
}