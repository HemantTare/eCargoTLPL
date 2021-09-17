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
/// Summary description for DateSettingsPresenter
/// </summary>



namespace Raj.EC.AdminPresenter
{
    public class DateSettingsPresenter : Presenter
    {
        private IDateSettingsView objIDateSettingsView;
        private DateSettingsModel objDateSettingsModel;
        private DataSet objDS;

        public DateSettingsPresenter(IDateSettingsView dateSettingsView, bool IsPostBack)
        {
            objIDateSettingsView = dateSettingsView;
            objDateSettingsModel = new DateSettingsModel(objIDateSettingsView);
            base.Init(objIDateSettingsView, objDateSettingsModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }
        private void initValues()
        {
            if (objIDateSettingsView.keyID > 0)
            {
                objDS = objDateSettingsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIDateSettingsView.ProcessName = DR["Process_Name"].ToString();
                    objIDateSettingsView.Code = DR["Code"].ToString();
                    objIDateSettingsView.MinHrs = Util.String2Int(DR["MinHrs"].ToString());
                    objIDateSettingsView.MaxHrs = Util.String2Int(DR["MaxHrs"].ToString());
                    
                }
            }
        }

        public void Save()
        {
            base.DBSave();
            //objMenuItemModel.Save();
        }
    }
}

