using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.FinanceView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for VoucherForApprovalModel
/// </summary>
/// 

namespace Raj.EC.FinanceModel
{
    public class VoucherForApprovalModel:IModel
    {
        private IVoucherForApprovalView objIVoucherForApprovalView;
        private DataSet objDS;
        private DAL objDAL = new DAL();

        public VoucherForApprovalModel(IVoucherForApprovalView VoucherForApprovalView)
        {
            objIVoucherForApprovalView = VoucherForApprovalView;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = 
                                    { 
                                    objDAL.MakeInParams("@Voucher_ID",SqlDbType.Int,0,objIVoucherForApprovalView.Voucher_ID)
                                    };
            objDAL.RunProc("EC_FA_Voucher_For_Approval_Get_Details",sqlpara,ref objDS);

            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}
