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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

namespace Raj.EC.OperationModel
{
    public class DlyCreditApprovalModel : IModel
    {
        private IDlyCreditApprovalView objIDlyCreditApprovalView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public DlyCreditApprovalModel(IDlyCreditApprovalView DlyCreditApprovalView)
        {
            objIDlyCreditApprovalView = DlyCreditApprovalView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, objIDlyCreditApprovalView.GCNo)};

            objDAL.RunProc("dbo.EC_Opr_Search_GC_DlyCreditApproval", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0,objIDlyCreditApprovalView.PDSID),
            objDAL.MakeInParams("@GC_ID", SqlDbType.Int,0,objIDlyCreditApprovalView.GCID),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@Client_ID", SqlDbType.Int,0,objIDlyCreditApprovalView.BillingPartyId),
            objDAL.MakeInParams("@MPaymentTransaction_ID", SqlDbType.VarChar,50,objIDlyCreditApprovalView.MTransactionID),
            objDAL.MakeInParams("@Approved_Status", SqlDbType.Int, 0,objIDlyCreditApprovalView.IsApproved),
            objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 100,objIDlyCreditApprovalView.ReasonUnApproved)};

            objDAL.RunProc("dbo.EC_Opr_DlyCreditApproval_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;
        } 
    }
}