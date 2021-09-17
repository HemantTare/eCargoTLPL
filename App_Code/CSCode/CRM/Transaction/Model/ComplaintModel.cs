using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.CRM.TransactionsView;
using System.Net.Mail;
using System.IO;
using ClassLibraryMVP;

namespace Raj.CRM.TransactionsModel
{
    class ComplaintModel : IModel
    {       
        private IComplaintView objIComplaintView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;

        public ComplaintModel(IComplaintView complaintView)
        {
            objIComplaintView = complaintView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Ticket_ID", SqlDbType.Int, 0, objIComplaintView.keyID) 
                                         };
            objDAL.RunProc("[EC_CRM_Trn_Complaint_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                           objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                           objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),

                           objDAL.MakeOutParams("@Ticket_No_For_Print", SqlDbType.VarChar,20), 
                           objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, _userID),
                           objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, (string)UserManager.getUserParam().HierarchyCode),
                           objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, (int)UserManager.getUserParam().MainId),
                           objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0, (int)UserManager.getUserParam().DivisionId),
                           objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, (int)Raj.EC.Common.GetMenuItemId()),

                           objDAL.MakeInParams("@Gc_No", SqlDbType.Int,0,objIComplaintView.DocGcNo),
                           objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,(int)UserManager.getUserParam().YearCode), 
                           objDAL.MakeInParams("@Key_Id", SqlDbType.Int, 0, objIComplaintView.keyID),

                           objDAL.MakeInParams("@UnDelievered_Reason", SqlDbType.VarChar,200,objIComplaintView.UndeliveredReason),
                           objDAL.MakeInParams("@Complaint_Nature_ID", SqlDbType.Int,0,objIComplaintView.ComplaintNatureId),

                           objDAL.MakeInParams("@Name", SqlDbType.VarChar,100,objIComplaintView.Name),
                           objDAL.MakeInParams("@Telephone_No", SqlDbType.VarChar,25,objIComplaintView.TelephoneNo),
                           objDAL.MakeInParams("@Mobile_No", SqlDbType.VarChar,25,objIComplaintView.MobileNo),
                           objDAL.MakeInParams("@Priority_Id", SqlDbType.Int,0,objIComplaintView.PriorityId),

                           objDAL.MakeInParams("@Designation", SqlDbType.VarChar,50,objIComplaintView.Designation), 
                           objDAL.MakeInParams("@Email", SqlDbType.VarChar,50, objIComplaintView.EMailID),
                           objDAL.MakeInParams("@CNeeNorId", SqlDbType.Int,0,objIComplaintView.CNeeNorID), 
                           objDAL.MakeInParams("@Complaint_Description", SqlDbType.VarChar,1000,objIComplaintView.ComplaintDetails),
                           objDAL.MakeInParams("@IsQuery",SqlDbType.Bit,1,objIComplaintView.IsQuery)};

            objDAL.RunProc("[EC_CRM_Trn_Complaint_Save]", objSqlParam,ref objDS); 

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIComplaintView.errorMessage = objMessage.message;
            
            if (objMessage.messageID == 0)
            {
                Message objMessage1 = new Message();
                AttachmentsModel objAttachmentsModel = new AttachmentsModel();

                int GenTicketId = Util.String2Int(objDS.Tables[0].Rows[0][0].ToString());
                objMessage1 = objAttachmentsModel.Save(GenTicketId, _userID, objIComplaintView.IsQuery);

                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIComplaintView.IsQuery == false)
                {
                    string Ticket_No = Convert.ToString(objSqlParam[2].Value);

                    if (objIComplaintView.keyID <= 0)
                    {
                        _Msg = "Ticket No " + Ticket_No + " Generated SuccessFully";
                        if (objIComplaintView.EMailID.Trim() != "")
                        {
                            MailSender objMailSender = new MailSender(objIComplaintView.EMailID.Trim(), Ticket_No, 1);
                        }
                    }
                }
              System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        } 
        private void setTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("[EC_CRM_Trn_Complaint_FillValues]",ref objDS);
            return objDS;
        }

        public DataSet GetOnGcDocChanged()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@GcDoc_Id", SqlDbType.Int,0,objIComplaintView.DocGcId),
                                           objDAL.MakeInParams("@GcDoc_No", SqlDbType.Int,0,objIComplaintView.DocGcNo),
                                           objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int,0,objIComplaintView.keyID)
                                         };
            objDAL.RunProc("[EC_CRM_Trn_Complaint_SetLablesOnGcDocChanged]", objSqlParam, ref objDS);
            return objDS;
        }
    }
}
