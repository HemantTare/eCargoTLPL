using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using System.Data.SqlClient;
using Raj.EC;
/// <summary>
/// Summary description for VoucherBillByBill
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class VoucherBillByBillModel : IModel
    {
        private DataSet objDS;
        private IVoucherBillByBillView objIVoucherBillByBillView;
        private DAL objDAL = new DAL();

        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;


        public VoucherBillByBillModel(IVoucherBillByBillView voucherBillByBillView)
        {
            objIVoucherBillByBillView = voucherBillByBillView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIVoucherBillByBillView.LedgerId)
                                         };

            objDAL.RunProc("EC_FA_Mst_VoucherBillByBill_FillValues", objSqlParam, ref objDS);

            return objDS;
        }
        public DataSet  SetCreditDaysAmount()
        {

             SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIVoucherBillByBillView.LedgerId),
                                            objDAL.MakeInParams("@Ref_No", SqlDbType.VarChar,50,objIVoucherBillByBillView.Name)

                                         };

            objDAL.RunProc("EC_FA_Mst_VoucherBillByBill_FillOnBillChanged", objSqlParam, ref objDS);


            //DataSet Ds = objCommon.EC_Common_Pass_Query("select Bill_Date,Credit_Days,Amount from FA_Bill_Wise_Details where Details_ID="+ Name);
            return objDS;
        }
        public Message Save()
        {
            Message objMsg = new Message();
            return objMsg;

        }
        public bool IsDuplicateRef_No()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(objIVoucherBillByBillView.SessionBillByBill_New.Copy());
            string errMsg = "";
            SqlParameter[] objSqlParam = { 
                                        // objDAL.MakeOutParams("@IsDuplicate", SqlDbType.Bit, 0), 
                                        //objDAL.MakeOutParams("@duplicate_Refno", SqlDbType.VarChar, 2000),                                         
                                       objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,objIVoucherBillByBillView.VoucherId), 
                                       objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,0,_hierarchyCode),
                                       objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId ),
                                       objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                       objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIVoucherBillByBillView.LedgerId),
                                       objDAL.MakeInParams("@RefNo_XML", SqlDbType.Xml,0,ds.GetXml().ToLower())               
                                         };

            objDAL.RunProc("FA_Opr_Voucher_IsDuplicateBill", objSqlParam,ref objDS);

            if (Convert.ToBoolean(objDS.Tables[0].Rows[0]["IsDuplicate"]) == true)
            {
                errMsg = objDS.Tables[0].Rows[0]["DuplicateRefNo"].ToString();                
                errMsg = errMsg.Remove(errMsg.Length - 1);
                objIVoucherBillByBillView.errorMessage = "Duplicate Ref No :  " + errMsg;                
                
                return false;
            }
            return true;
            

        }
        

    }
}