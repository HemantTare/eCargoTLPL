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
/// Summary description for ServicePresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{
    public class ServicePresenter : Presenter
    {
        private IServiceView objIServiceView;
        private ServiceModel objServiceModel;
        private DataSet objDS;

        public ServicePresenter(IServiceView ServiceView, bool isPostback)
        {
            objIServiceView = ServiceView;
            objServiceModel = new ServiceModel(objIServiceView);
            base.Init(objIServiceView, objServiceModel);

            if (!isPostback)
            {
                fillValues();
                initValues();
            }
        }
        private void fillValues()
        {
            objDS = objServiceModel.FillValues();
            objIServiceView.BindServiceCategory = objDS.Tables[0];
            objIServiceView.BindParentService = objDS.Tables[1];
            objIServiceView.SessionServiceTaskDropdown= objDS.Tables[2];

        }

        private void initValues()
        {

            objDS = objServiceModel.ReadValues();

            objIServiceView.BindServiceTaskDetailsGrid = objDS.Tables[1];

            if (objIServiceView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIServiceView.ServiceName = objDR["Service_Name"].ToString();
                    objIServiceView.ServiceCategoryID = Util.String2Int(objDR["Service_Category_ID"].ToString());
                    objIServiceView.ServiceDescription = objDR["Service_Description"].ToString();
                    objIServiceView.ParentServiceID = Util.String2Int(objDR["Parent_Service_ID"].ToString());
                    objIServiceView.EstCheckingTime = Util.String2Decimal (objDR["Est_Checking_Time"].ToString());
                    objIServiceView.EstRepairTime = Util.String2Decimal (objDR["Est_Repair_Time"].ToString());
                }
            }
        }

        public void Save()
        {
            base.DBSave();
            //objServiceModel.Save();
        }

    }
}
