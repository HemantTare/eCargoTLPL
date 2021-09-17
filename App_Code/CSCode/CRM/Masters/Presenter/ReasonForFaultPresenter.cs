using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Raj.CRM.MasterView;
using Raj.CRM.MasterModel;

namespace Raj.CRM.MasterPresenter{

    public class ReasonForFaultPresenter : ClassLibraryMVP.General.Presenter
    {

        private IReasonForFaultView objIReasonForFaultView;
        private ReasonForFaultModel objReasonForFaultModel;
        DataSet objDS;

        public ReasonForFaultPresenter(IReasonForFaultView reasonForFaultView, bool IsPostBack)
        {
            objIReasonForFaultView = reasonForFaultView;
            objReasonForFaultModel = new ReasonForFaultModel(objIReasonForFaultView);

            base.Init(objIReasonForFaultView, objReasonForFaultModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }
        public void save()
        {
            base.DBSave();
        }

        private void initValues()
        {
            if (objIReasonForFaultView.keyID > 0)
            {
                objDS = objReasonForFaultModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIReasonForFaultView.ReasonForFault = objDR["Reason_For_Fault"].ToString();
                    objIReasonForFaultView.Description = objDR["Description"].ToString();
                }
            }
        }
    }
}