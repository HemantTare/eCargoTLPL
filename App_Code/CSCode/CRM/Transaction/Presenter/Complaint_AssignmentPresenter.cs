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
using Raj.CRM.TransactionView;
using Raj.CRM.TransactionModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for Complaint_AssignmentPresenter
/// </summary>
/// 
//namespace Raj.FA.ReportsPresenter
    namespace Raj.CRM.TransactionPresenter
{
    public class Complaint_AssignmentPresenter : Presenter
    {
        private IComplaint_AssignmentView objComplaint_AssignmentView;
        private Complaint_AssignmentModel objComplaint_AssignmentModel;
        private DataSet objDS = new DataSet();
       
 
        
        public Complaint_AssignmentPresenter(IComplaint_AssignmentView Complaint_AssignmentView, bool isPostBack)
        {
            objComplaint_AssignmentView = Complaint_AssignmentView;
            objComplaint_AssignmentModel = new Complaint_AssignmentModel(objComplaint_AssignmentView);
            base.Init(objComplaint_AssignmentView, objComplaint_AssignmentModel);

            if (isPostBack == false)
            {
                initValues();
            }
        }


        private void initValues()
        {
            //if (objComplaint_AssignmentView.keyID > 0)
            //{
                objDS = objComplaint_AssignmentModel.ReadValues();
                objComplaint_AssignmentView.Bind_dg_AssginUser = objDS.Tables[0];
                objComplaint_AssignmentView.SetHeadingCaption = objDS.Tables[1];

            //}
        }


        public void FillOnSearchByChanged()
        {
            objDS = objComplaint_AssignmentModel.FillOnSearchByChanged();
            objComplaint_AssignmentView.Bind_ddl_FilterBy1 = objDS.Tables[0];
        }


        public DataSet FillOnAddClick()
        {
           return objComplaint_AssignmentModel.FillOnAddClick();
        }


        public void Save()
        {
            // base.DBSave();

            objComplaint_AssignmentModel.Save();
        }
    }
}






