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
    class MergeTicketsModel : IModel
    {
        private IMergeTicketsView objIMergeTicketsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public MergeTicketsModel(IMergeTicketsView MergeTicketsView)
        {
            objIMergeTicketsView = MergeTicketsView;
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
                           objDAL.MakeInParams("@FromTicketId", SqlDbType.Int,0,objIMergeTicketsView.FromTicketId),
                           objDAL.MakeInParams("@ToTicketId", SqlDbType.Int,0,objIMergeTicketsView.ToTicketId)};

            objDAL.RunProc("[EC_CRM_Trn_MergeTickets_Save]", objSqlParam); 

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIMergeTicketsView.errorMessage = objMessage.message;

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Merge SuccessFully";
                objIMergeTicketsView.errorMessage = _Msg;
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }
            return objMessage;
        }

        public DataSet FillValues()        {

            objDAL.RunProc("[EC_CRM_Trn_MergeTickets_FillValues]",ref objDS);
            return objDS;
        }

        public DataSet GetFromTicket()
        {
            SqlParameter[] objSqlParam = { 
                                           objDAL.MakeInParams("@GcDocNo", SqlDbType.Int,0,objIMergeTicketsView.GcDocId)};
            objDAL.RunProc("[EC_CRM_Trn_MergeTickets_GetFromTicket]", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetToTicket()
        {
            SqlParameter[] objSqlParam = {     
                                               objDAL.MakeInParams("@GcDocNo", SqlDbType.Int,0,objIMergeTicketsView.GcDocId),
                                               objDAL.MakeInParams("@FromTicketId", SqlDbType.Int,0,objIMergeTicketsView.FromTicketId)};
            objDAL.RunProc("[EC_CRM_Trn_MergeTickets_GetToTicket]", objSqlParam, ref objDS);
            return objDS;
        } 
    }
}
