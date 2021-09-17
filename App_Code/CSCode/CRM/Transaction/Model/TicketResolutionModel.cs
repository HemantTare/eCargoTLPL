using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.CRM.TransactionView;
//using Raj.eCargo.Init;


/// <summary>
/// Summary description for TicketResolutionModel
/// </summary>
namespace Raj.CRM.TransactionModel
{
  
    public class TicketResolutionModel : ClassLibraryMVP.General.IModel
    {
        private ITicketResolutionView objITicketResolutionView;
        private DataSet objDS;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);
  

        public TicketResolutionModel(ITicketResolutionView ticketResolutionView)
        {
            objITicketResolutionView = ticketResolutionView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = {
                                            _objDAL.MakeInParams("@Ticket_Id",SqlDbType.Int,0, objITicketResolutionView.TicketId)};
            
              _objDAL.RunProc("EC_CRM_Trn_TicketResolution_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillLabelValues()
        {
            SqlParameter[] objSqlParam = {
                                 _objDAL.MakeInParams("@Ticket_Id",SqlDbType.Int,0, objITicketResolutionView.TicketId)};

            _objDAL.RunProc("EC_CRM_Trn_TicketResolution_FillLabelValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMsg = new Message();
            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
           _objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0,objITicketResolutionView.TicketId),
           _objDAL.MakeInParams("@How_Resolved", SqlDbType.VarChar,100,objITicketResolutionView.HowResolved),
           _objDAL.MakeInParams("@Whether_Customer_Satisfied", SqlDbType.Bit,1,objITicketResolutionView.WhetherCustomerSatisfied),
           _objDAL.MakeInParams("@Reason", SqlDbType.VarChar,250,objITicketResolutionView.Reason),
           _objDAL.MakeInParams("@User_Id",SqlDbType.Int,0, _userId)};

            _objDAL.RunProc("EC_CRM_Trn_ResolutionSave", objSqlParam);

            objMsg.messageID = Util.String2Int(objSqlParam[0].Value.ToString());
            objMsg.message = objSqlParam[1].Value.ToString();

            if (objMsg.messageID == 0)
            {
                string _Msg;
                _Msg = "Ticket Closed SuccessFully";
                SenEMail();
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMsg;
        }

        public void SenEMail()
        {
            objDS = _objDAL.RunQuery("Select Email,Ticket_No From EC_CRM_Complaint Where Ticket_Id=" + objITicketResolutionView.TicketId.ToString());

            string emailId = objDS.Tables[0].Rows[0][0].ToString();
            string ticketNo = objDS.Tables[0].Rows[0][1].ToString();

            if (emailId.Trim() != "")
            {
                MailSender objMailSender = new MailSender(emailId.Trim(), ticketNo, 2);
            }
        }
    }
}

