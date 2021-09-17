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
/// Summary description for UnAppVoucherCancellationModel
/// </summary>
/// 

namespace Raj.EC.FinanceModel
{
    public class ReverseVoucherModel : ClassLibraryMVP.General.IModel
    {
        private IReverseVoucherView objIReverseVoucherView;
        private DataSet objDS;
        private DAL objDAL = new DAL();

        public ReverseVoucherModel(IReverseVoucherView ReverseVoucherView)
        {
            objIReverseVoucherView = ReverseVoucherView;
        }

        public DataSet ReadValues()
        {
            return objDS;
        }

        public DataSet FillValues()
        {
            return objDS;
        }

        public DataSet VoucherData()
        {

            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,-1),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,"BO"),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,14),
                                            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,1),
                                            objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,8)
                                         };

            objDAL.RunProc("[EC_FA_Mst_Voucher_ReadValues]", objSqlParam, ref objDS);

            Common.SetTableName(new string[] { "Voucher", "VoucherDetails", "VoucherCostCentre", "VoucherBillByBill", "MstCostCentre", "MstRefType" }, objDS);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS.Tables["VoucherDetails"]);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Cost_Centre_ID" }, objDS.Tables["VoucherCostCentre"]);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Ref_No" }, objDS.Tables["VoucherBillByBill"]);

            return objDS;
        }

        public DataSet Get_LedgerParams()
        {

            SqlParameter[] sqlParam = { objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,UserManager.getUserParam().HierarchyCode),
                       objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
                       objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,objIReverseVoucherView.Ledger_Id )};
            objDAL.RunProc("EC_FA_Mst_Voucher_GetLedgerParam", sqlParam, ref  objDS);

            return objDS;
        }

        //public DataSet getData(string HCode, string voucherId, int MainId)
        //{
        //    SqlParameter[] sqlParam = { objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int, 0, voucherId),
        //                                    objDAL .MakeInParams("@Main_id", SqlDbType.Int, 0, MainId),
        //                                    objDAL .MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, HCode)};

        //    objDAL.RunProc("FA_Get_UnApproved_Voucher_Details", sqlParam, ref  objDS);  //change this sp it's not working properly
        //    nameDataset();
        //    setPrimaryKey();
        //    populateVoucherProperties();
        //    return objDS;
        //}

        //private void nameDataset()
        //{
        //    objDS.Tables[0].TableName = "Cost_Centre";
        //}

        //private void setPrimaryKey()
        //{
        //    //DataColumn dc = new DataColumn(0);
        //    //dc(0) = objDS.Tables["Voucher_Details"].Columns["Ledger_Name"];

        //    //objDS.Tables["Voucher_Details"].PrimaryKey = dc;
        //    //objDS.Tables["Voucher_Details"].Columns("Ledger_Name").AllowDBNull = true;
        //}

        //private void populateVoucherProperties()
        //{
        //    //  voucherDetails = _ds.Tables("Voucher_Details")

        //    //  if ( objds .Tables("voucher").Rows.Count > 0 )
        //    //  {
        //    //      VoucherNo = Convert.ToString(_ds.Tables("Voucher").Rows(0)("Voucher_No"))
        //    //      VoucherDate = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("Voucher_Date"))
        //    //      RefNo = Convert.ToString(_ds.Tables("Voucher").Rows(0)("ref_No"))
        //    //      ChequeNo = _ds.Tables("Voucher").Rows(0)("cheque_No")
        //    //      ChequeDate = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("Cheque_Date"))
        //    //      Narration = Convert.ToString(_ds.Tables("Voucher").Rows(0)("Narration"))
        //    //      TotalDebit = Convert.ToDouble(_ds.Tables("Voucher").Rows(0)("total_debit"))
        //    //      TotalCredit = Convert.ToDouble(_ds.Tables("Voucher").Rows(0)("total_credit"))
        //    //      voucherTypeId = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("voucher_type_id"))
        //    //      VoucherTypeIdMain = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("voucher_type_id_main"))
        //    //      CreatedOn = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("created_on"))
        //    //      CreatedBy = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("created_by"))
        //    //      UpdatedOn = Convert.ToDateTime(_ds.Tables("Voucher").Rows(0)("updated_on"))
        //    //      UpdatedBy = Convert.ToInt16(_ds.Tables("Voucher").Rows(0)("updated_by"))

        //    //}

        //}


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] sqlpara = 
                                    { 
                                    objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,0),
                                    objDAL.MakeOutParams("@ERROR_DESC",SqlDbType.VarChar,4000),
                                    objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,2,UserManager.getUserParam().HierarchyCode),
                                    objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                    objDAL.MakeInParams("@Voucher_Id",SqlDbType.Int,0,objIReverseVoucherView.Voucher_Id),
                                    objDAL.MakeInParams("@Reason",SqlDbType.VarChar,250,objIReverseVoucherView.Reason),
                                    objDAL.MakeInParams("@Ledger_Id",SqlDbType.Int,0,objIReverseVoucherView.Ledger_Id),
                                    objDAL.MakeInParams("@Branch_Ledger_Id",SqlDbType.Int,0,objIReverseVoucherView.BranchLedgerId),
                                    objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                                    objDAL.MakeInParams("@User_Id",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                    objDAL.MakeInParams("@Voucher_Type_Id",SqlDbType.Int,0,objIReverseVoucherView.VoucherTypeID),
                                    objDAL.MakeInParams("@VoucherCostCentreXML",SqlDbType.Xml,0,objIReverseVoucherView.CostCentreXML),
                                    objDAL.MakeInParams("@VoucherBillByBillXML",SqlDbType.Xml,0,objIReverseVoucherView.BillByBillXML)
                                    };

            objDAL.RunProc("FA_Opr_IBT_VoucherReverse_Save",sqlpara);

            objMessage.messageID = Convert.ToInt32(sqlpara[0].Value);
            objMessage.message = sqlpara[1].Value.ToString(); 

              
           if (objMessage.messageID == 0)
            {
                string _Msg = "";
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }

            return objMessage;
        }
    }
}