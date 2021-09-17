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

namespace Raj.CRM.MasterPresenter
{
    public class EscalationMatrixPresenter : ClassLibraryMVP.General.Presenter
    {
        private IEscalationMatrixView objIEscalationMatrixView;
        private EscalationMatrixModel objEscalationMatrixModel;
        DataSet objDS;

        public EscalationMatrixPresenter(IEscalationMatrixView escalationMatrixView, bool IsPostBack)
        {
            objIEscalationMatrixView = escalationMatrixView;
            objEscalationMatrixModel = new EscalationMatrixModel(objIEscalationMatrixView);

            base.Init(objIEscalationMatrixView, objEscalationMatrixModel);

            if (!IsPostBack)
            {
                initValues();
                FillGrid();
                FillProfile();
            }
        }

        public void initValues()
        {
            objDS = objEscalationMatrixModel.FillValues();
            objIEscalationMatrixView.BindComplaintNature = objDS.Tables[0];
        }
        public void FillProfile()
        {
            objDS = objEscalationMatrixModel.FillProfileValues();
            objIEscalationMatrixView.SessionProfile= objDS;
        }

        public void FillGrid()
        {        
            objIEscalationMatrixView.SessionEscalationMatrixGrid = objEscalationMatrixModel.ReadValues();
        }

        public void FillUserValues()
        {
            objIEscalationMatrixView.SessionUserDetails = objEscalationMatrixModel.FillUserValues();            
        }
        public void save()
        {
            base.DBSave();
        }
	}
}
