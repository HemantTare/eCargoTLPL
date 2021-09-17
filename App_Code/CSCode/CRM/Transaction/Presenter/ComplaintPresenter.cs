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
using Raj.CRM.TransactionsView;
using Raj.CRM.TransactionsModel;
//using Raj.EC.CRM.Init;
namespace Raj.CRM.TransactionsPresenter
{
    public class ComplaintPresenter : Presenter
    {
        private IComplaintView objIComplaintView;
        private ComplaintModel objComplaintModel;
        private DataSet objDS;

        public ComplaintPresenter(IComplaintView complaintView, bool IsPostBack)
        {
            objIComplaintView = complaintView;
            objComplaintModel = new ComplaintModel(objIComplaintView);
            base.Init(objIComplaintView, objComplaintModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            if (objIComplaintView.keyID > 0)
            {
                readValues();
            }
        }

        private void fillValues()
        {
            objDS = objComplaintModel.FillValues();
            objIComplaintView.bind_ddl_NatureOfComplaint = objDS.Tables[0];
            objIComplaintView.bind_rdbl_Priority = objDS.Tables[1];
            objIComplaintView.SetAttachmentCount = 0;
        }

        private void readValues()
        {
            objDS = objComplaintModel.ReadValues();
            DataRow Dr = objDS.Tables[0].Rows[0];

            //if ((int)Dr["Current_Ticket_Status"] != 0)
            //{
            //    Raj.FA.CommonCs.DisplayErrors("Ticket Is Already Assigned To User");
            //    return;
            //}

            objIComplaintView.TicketNo = Dr["Ticket_No"].ToString();
            objIComplaintView.TicketDate = Convert.ToDateTime(Dr["Ticket_Date"]);
            objIComplaintView.UndeliveredReason = Dr["UnDelievered_Reason"].ToString();
            objIComplaintView.ComplaintDetails = Dr["Complaint_Description"].ToString();
            objIComplaintView.Name = Dr["Name"].ToString();
            objIComplaintView.TelephoneNo = Dr["Telephone_No"].ToString();
            objIComplaintView.MobileNo = Dr["Mobile_No"].ToString();
            objIComplaintView.Designation = Dr["Designation"].ToString();
            objIComplaintView.EMailID = Dr["Email"].ToString();
            objIComplaintView.CNeeNorID = Util.String2Int(Dr["Complaint_By"].ToString());
            objIComplaintView.ComplaintNatureId = Util.String2Int(Dr["Complaint_Nature_Id"].ToString());
            objIComplaintView.PriorityId = Util.String2Int(Dr["Priority_Id"].ToString());
            if (Convert.ToBoolean(Dr["Is_Query"].ToString()) == true)
            {
                objIComplaintView.IsQuery = true;
            }
            else
            {
                objIComplaintView.IsQuery = false;
              
            }
            objIComplaintView.SetAttachmentCount = Util.String2Int(Dr["AttachmentCount"].ToString());
            objIComplaintView.SetDocGcId(Dr["GC_Id"].ToString(), Dr["GC_No"].ToString());

            SetLablesOnGcDocChanged();
        }

        public void Save()
        {
            base.DBSave();
            //objComplaintModel.Save();
        }



        public void SetLablesOnGcDocChanged()
        {

            objDS = objComplaintModel.GetOnGcDocChanged();
            objIComplaintView.SetLables = objDS.Tables[0];
        }
    }
}
