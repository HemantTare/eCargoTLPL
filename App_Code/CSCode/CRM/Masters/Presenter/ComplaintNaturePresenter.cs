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

/// <summary>
/// Summary description for ComplaintNaturePresenter
/// </summary>
namespace Raj.CRM.MasterPresenter
{

    public class ComplaintNaturePresenter: ClassLibraryMVP.General.Presenter
    {
        private IComplaintNatureView objIComplaintNatureView;
        private ComplaintNatureModel objComplaintNatureModel;
        DataSet objDS;

        public ComplaintNaturePresenter(IComplaintNatureView complaintNatureView, bool IsPostBack)
        {
            objIComplaintNatureView = complaintNatureView;
            objComplaintNatureModel = new ComplaintNatureModel(objIComplaintNatureView);

            base.Init(objIComplaintNatureView, objComplaintNatureModel);

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
            if (objIComplaintNatureView.keyID > 0)
            {
                objDS = objComplaintNatureModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIComplaintNatureView.ComplaintNatureName = objDR["Complaint_Nature"].ToString();
                    objIComplaintNatureView.Description = objDR["Description"].ToString();
                 
                }
            }
        }
    }
}