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
using System.Globalization;

namespace Raj.CRM.TransactionsPresenter
{
    public class TicketHistoryPresenter : Presenter
    {
        private ITicketHistoryView objITicketHistoryView;
        private TicketHistoryModel objTicketHistoryModel;
        private DataSet objDS;

        public TicketHistoryPresenter(ITicketHistoryView TicketHistoryView, bool IsPostBack)
        {
            objITicketHistoryView = TicketHistoryView;
            objTicketHistoryModel = new TicketHistoryModel(objITicketHistoryView);
            base.Init(objITicketHistoryView, objTicketHistoryModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        { 
            if (objITicketHistoryView.keyID > 0)
            {
                fillValues();
            }
        }

        private void fillValues()
        {
            objDS =objTicketHistoryModel.FillValues();

            objDS.Tables[0].Columns.Add("CommentPostedBy");
            objDS.Tables[0].Columns.Add("Branch");
            objDS.Tables[0].Columns.Add("Ticket_Sr_No1");
            
            string branchText="";
            System.Text.StringBuilder commentText;
            int historyCount = objDS.Tables[0].Rows.Count;
            int i=0;
            foreach (DataRow Dr in objDS.Tables[0].Rows)
            {
                branchText = "";
                commentText = new System.Text.StringBuilder();
                if (Util.String2Int(Dr["Branch_ID"].ToString()) > 0)
                {
                    branchText = "Branch : " + Dr["Branch_Name"].ToString();
                }
                else if (Util.String2Int(Dr["Region_ID"].ToString()) > 0)
                {
                    branchText = "Region : " + Dr["Region_Name"].ToString();
                }
                else if (Util.String2Int(Dr["Area_ID"].ToString()) > 0)
                {
                    branchText = "Area : " + Dr["Area_Name"].ToString();
                }
                else
                {
                    branchText = "HO User";
                }

                commentText.Append("Comment Posted By ");
                commentText.Append("'");
                commentText.Append(Dr["First_Name"].ToString() + "  ");
                commentText.Append(Dr["Middle_Name"].ToString()+"  ");
                commentText.Append(Dr["Last_Name"].ToString());
                commentText.Append("' On ");
                commentText.Append(Convert.ToDateTime(Dr["Status_Update_Date"]).ToString("dd/MM/yyyy hh:mm tt"));
                commentText.Append(" ");

                Dr["CommentPostedBy"] = commentText.ToString();
                Dr["Branch"] = branchText;
                Dr["Profile_Name"]= "Profile : "+Dr["Profile_Name"].ToString();
                Dr["Ticket_Sr_No1"] = historyCount - i++;
            }
            objDS.AcceptChanges();  

            objITicketHistoryView.bind_rpt_History =objDS.Tables[0];
            System.Text.StringBuilder headerLableText = new System.Text.StringBuilder(); 

            DataRow Dr1= objDS.Tables[1].Rows[0];
            headerLableText.Append("Ticket No: ");
            headerLableText.Append(Dr1["Ticket_No"].ToString());
            headerLableText.Append("/");
            headerLableText.Append(CompanyManager.getCompanyParam().GcCaption + " No: ");
            headerLableText.Append(Dr1["GC_No"].ToString());
            objITicketHistoryView.HeaderLable = headerLableText.ToString();
            objITicketHistoryView.CompliantNature = Dr1["Complaint_Nature"].ToString();
            objITicketHistoryView.SetAttachmentCount = Util.String2Int(objDS.Tables[2].Rows[0][0].ToString());
        }       
 
        public void Save()
        {
            //base.DBSave();
            objTicketHistoryModel.Save();
        }         
    }
}
