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
namespace Raj.CRM.TransactionsModel
{
    class TicketHistoryModel : IModel
    {
        private ITicketHistoryView objITicketHistoryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;

        public TicketHistoryModel(ITicketHistoryView TicketHistoryView)
        {
            objITicketHistoryView = TicketHistoryView;
        }   

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0, objITicketHistoryView.keyID) 
                                         };
            objDAL.RunProc("[EC_CRM_Trn_TicketHistory_FillValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues() 
        {
             return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                           objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                           objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),

                           objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, _userID),
                           objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0, objITicketHistoryView.keyID),
                           objDAL.MakeInParams("@Reply", SqlDbType.VarChar,200,objITicketHistoryView.Reply),
                           objDAL.MakeInParams("@Type", SqlDbType.VarChar,100,objITicketHistoryView.Type), 
                          };

            objDAL.RunProc("[EC_CRM_Trn_SentReplySave]", objSqlParam); 

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objITicketHistoryView.errorMessage = objMessage.message;

            if (objMessage.messageID == 0)
            {
                Message objMessage1 = new Message();
                AttachmentsModel objAttachmentsModel = new AttachmentsModel();
                objMessage1 = objAttachmentsModel.Save(objITicketHistoryView.keyID, _userID, false);  //parameter false added by Ankit champaneriya

                string _Msg;

                _Msg = "Reply Sent SuccessFully";

                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                //  objITicketHistoryView.errorMessage = _Msg;
            }

            return objMessage;
        }
    }
}
