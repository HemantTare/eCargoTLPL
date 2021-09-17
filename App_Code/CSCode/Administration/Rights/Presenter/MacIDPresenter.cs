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
using Raj.EC.AdminView;
using Raj.EC.AdminModel;
/// <summary>
/// Summary description for MacIDPresenter
/// </summary>
namespace Raj.EC.AdminPresenter
{
    public class MacIDPresenter : Presenter
    {
        private IMacIDView objIMacIDView;
        private MacIDModel objMacIDModel;
        private DataSet objDS;

        public MacIDPresenter(IMacIDView MacIDView, bool isPostBack)
        {
            objIMacIDView = MacIDView;
            objMacIDModel = new MacIDModel(objIMacIDView);
            base.Init(objIMacIDView, objMacIDModel);


            if (!isPostBack)
            {
                initValues();
            }

        }

        public void initValues()
        {
            objDS = objMacIDModel.ReadValues();
            objIMacIDView.Bind_Mac_Id = objDS.Tables[0];
        }


        public void Save()
        {
            base.DBSave();
            //objMacIDModel.Save();
        }
    }
}

