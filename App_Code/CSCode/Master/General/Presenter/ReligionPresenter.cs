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
using Raj.EF.MasterModel;
using Raj.EF.MasterView;


namespace Raj.EF.MasterPresenter
{
    public class ReligionPresenter:Presenter 
    {
        private IReligionModel objIReligionModel;
        private IReligionView objIReligionView;
        private DataSet objDS;
        public ReligionPresenter(IReligionView ReligionView,bool Ispostback)
        {
            objIReligionView = ReligionView;
            objIReligionModel = new IReligionModel(objIReligionView);
            base.Init(objIReligionView, objIReligionModel);
            if (!Ispostback)
            {
                initValues();
            }

        }
        public void Save()
        {
            base.DBSave();
            //objIReligionModel.Save();
        }

        private void initValues()
        {
            if (objIReligionView.keyID > 0)
            {
                objDS = objIReligionModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIReligionView.Religion = objDS.Tables[0].Rows[0]["Religion"].ToString();
                }
            }
        }
    }
}
