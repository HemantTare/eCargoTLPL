using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Raj.EC.FinanceView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;


/// <summary>
/// Author  : Ankit champaneriya
/// Date    : 12/12/08
/// Summary description for VoucherViewModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class VoucherViewModel : IModel
    {
        private IVoucherDetailsView  objIVoucherView;
        private IUnAppVoucherCancellationView objIUnAppVoucherCancellationView ;
        private DataSet objDS;
        private DAL objDAL = new DAL();

        public VoucherViewModel(IVoucherDetailsView  VoucherView)
        {
            objIVoucherView = VoucherView;
        }

        public DataSet ReadValues()
        {
            return objDS;
        }

        public DataSet FillValues()
        {
            return objDS;
        }

        public DataSet Fill_dv_UnApprovedVoucher()
        {

            SqlParameter[] sqlpara ={objDAL.MakeInParams("@Type", SqlDbType.VarChar, 50, "UnApproved Vouchers"  ) ,
                            objDAL.MakeInParams("@Id", SqlDbType.VarChar, 15,"2171")};
            objDAL.RunProc("EC_FA_Get_Details_To_View", sqlpara, ref objDS);
            return objDS;
        }

        public DataSet Fill_dg_Details()
        {
            SqlParameter[] sqlpara ={objDAL.MakeInParams("@Type", SqlDbType.VarChar, 50, "UnApproved Vouchers" ) ,
                            objDAL.MakeInParams("@Id", SqlDbType.VarChar, 15,"2171")};

            objDAL.RunProc("EC_FA_Get_Grid_Details_To_View", sqlpara, ref objDS);
            return objDS;
        }

        public Boolean Is_Cost_Centre(int ledger_Id)
        {

            SqlParameter[] sqlParam = { objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, "AO"),
                       objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,21 ) ,
                       objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, ledger_Id )};
            objDAL.RunProc("FA_Is_Cost_CentreApplicable", sqlParam, ref  objDS);

            return Convert.ToBoolean(objDS.Tables[0].Rows[0][0]);
        }

        public DataSet getData(string HCode, string voucherId, int MainId)
        {
            SqlParameter[] sqlParam = { objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int, 0, voucherId),
                                            objDAL .MakeInParams("@Main_id", SqlDbType.Int, 0, MainId),
                                            objDAL .MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, HCode)};

            objDAL.RunProc("FA_Get_UnApproved_Voucher_Details", sqlParam, ref  objDS);  //change this sp it's not working properly
            nameDataset();
            setPrimaryKey();
            populateVoucherProperties();
            return objDS;
        }

        private void nameDataset()
        {
            objDS.Tables[0].TableName = "Cost_Centre";
        }

        private void setPrimaryKey()
        {
            //DataColumn dc = new DataColumn(0);
            //dc(0) = objDS.Tables["Voucher_Details"].Columns["Ledger_Name"];

            //objDS.Tables["Voucher_Details"].PrimaryKey = dc;
            //objDS.Tables["Voucher_Details"].Columns("Ledger_Name").AllowDBNull = true;
        }

        private void populateVoucherProperties()
        {
            //  voucherDetails = _ds.Tables("Voucher_Details")

            //  if ( objds .Tables("voucher").Rows.Count > 0 )
            //  {
            //      VoucherNo = Convert.ToString(_ds.Tables("Voucher").Rows(0)("Voucher_No"))
            //      VoucherDate = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("Voucher_Date"))
            //      RefNo = Convert.ToString(_ds.Tables("Voucher").Rows(0)("ref_No"))
            //      ChequeNo = _ds.Tables("Voucher").Rows(0)("cheque_No")
            //      ChequeDate = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("Cheque_Date"))
            //      Narration = Convert.ToString(_ds.Tables("Voucher").Rows(0)("Narration"))
            //      TotalDebit = Convert.ToDouble(_ds.Tables("Voucher").Rows(0)("total_debit"))
            //      TotalCredit = Convert.ToDouble(_ds.Tables("Voucher").Rows(0)("total_credit"))
            //      voucherTypeId = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("voucher_type_id"))
            //      VoucherTypeIdMain = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("voucher_type_id_main"))
            //      CreatedOn = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("created_on"))
            //      CreatedBy = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("created_by"))
            //      UpdatedOn = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("updated_on"))
            //      UpdatedBy = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("updated_by"))

            //}

        }


        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}