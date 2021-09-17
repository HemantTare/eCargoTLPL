using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView;
using System.Data.SqlClient;

namespace Raj.EC.FinanceModel
{
    public class PartyRecieptVoucherModel:IModel
    {

        private IPartyRecieptVoucherView objIPartyRecieptVoucherView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;


        public PartyRecieptVoucherModel(IPartyRecieptVoucherView PartyRecieptVoucherView)
        {
            objIPartyRecieptVoucherView = PartyRecieptVoucherView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = 
                                { 
                                    objDAL.MakeInParams("@Voucher_Id",SqlDbType.Int,0,objIPartyRecieptVoucherView.keyID)
                                };
            objDAL.RunProc("EC_FA_PartyRecieptVoucher_ReadValues",sqlpara,ref objDS);

            Common.SetPrimaryKeys(new string[] { "Bill_No" }, objDS.Tables[0]); //BillGrid
            Common.SetPrimaryKeys(new string[] { "OtherLedgerId" }, objDS.Tables[1]); //OtherGrid
            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Cost_Centre_ID" }, objDS.Tables[5]);
            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Ref_No" }, objDS.Tables[3]);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            int _menuItemId = Common.GetMenuItemId();
            int _menuItemCode = Common.GetMenuItemCode();

            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),

                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode),
                                            objDAL.MakeInParams("@User_Id", SqlDbType.Int,0,_userID),

                                            objDAL.MakeInParams("@Voucher_Id",SqlDbType.Int,0,objIPartyRecieptVoucherView.keyID),
                                            objDAL.MakeInParams("@Voucher_No",SqlDbType.VarChar,20,objIPartyRecieptVoucherView.VoucherNo),
                                            objDAL.MakeInParams("@Voucher_Type_Id",SqlDbType.Int,0,46),
                                            
                                            objDAL.MakeInParams("@Voucher_Date",SqlDbType.DateTime,0,objIPartyRecieptVoucherView.VoucherDate),
                                            objDAL.MakeInParams("@ClientLedger_Id",SqlDbType.Int,0,objIPartyRecieptVoucherView.ClientLedgerID),
                                            objDAL.MakeInParams("@CashBankLedgerId",SqlDbType.Int,0,objIPartyRecieptVoucherView.CashBankLedgerID),
                                            objDAL.MakeInParams("@AmountReceived",SqlDbType.Decimal,0,objIPartyRecieptVoucherView.AmountRecieved),
                                            objDAL.MakeInParams("@ChequeNo",SqlDbType.VarChar,20,objIPartyRecieptVoucherView.ChequeNo),
                                            objDAL.MakeInParams("@ChequeDate",SqlDbType.DateTime,0,objIPartyRecieptVoucherView.ChequeDate),
                                            objDAL.MakeInParams("@BankName",SqlDbType.VarChar,100,objIPartyRecieptVoucherView.ChequeBankName),
                                            objDAL.MakeInParams("@RefNo",SqlDbType.VarChar,100,objIPartyRecieptVoucherView.ManualRefNo),

                                            objDAL.MakeInParams("@Narration",SqlDbType.VarChar,200,objIPartyRecieptVoucherView.Narration),
                                            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),

                                            objDAL.MakeInParams("@OtherDeductionXML", SqlDbType.Xml,0,objIPartyRecieptVoucherView.GetOtherDeductionXML),
                                            objDAL.MakeInParams("@ClientBillDetailXML",SqlDbType.Xml,0,objIPartyRecieptVoucherView.GetClientBillDetailXML),
                                            objDAL.MakeInParams("@VoucherCostCentreXML", SqlDbType.Xml,0,objIPartyRecieptVoucherView.GetCostCentreXML),
                                            objDAL.MakeInParams("@VoucherBillByBillXML", SqlDbType.Xml,0,objIPartyRecieptVoucherView.GetBillByBillXML),
                                           };



            objDAL.RunProc("FA_Opr_PartyReceiptVoucher_Save", objSqlParam);
           

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIPartyRecieptVoucherView.errorMessage = objMessage.message;
            string _Msg = "";
            if (objMessage.messageID == 0)
            {
                _Msg = "Saved SuccessFully";
                objIPartyRecieptVoucherView.errorMessage = _Msg;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }

            return objMessage;
        }


        public DataSet SetCreditDaysAmount()
        {

            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIPartyRecieptVoucherView.ClientLedgerID),
                                            objDAL.MakeInParams("@Ref_No", SqlDbType.VarChar,50,objIPartyRecieptVoucherView.RefNo)

                                         };

            objDAL.RunProc("EC_FA_Mst_VoucherBillByBill_FillOnBillChanged", objSqlParam, ref objDS);


            //DataSet Ds = objCommon.EC_Common_Pass_Query("select Bill_Date,Credit_Days,Amount from FA_Bill_Wise_Details where Details_ID="+ Name);
            return objDS;
        }

        public DataSet GetLedgerParam()
        {
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIPartyRecieptVoucherView.OtherLedgerId),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId)

                                         };

            objDAL.RunProc("[EC_FA_Mst_Voucher_GetLedgerParam]", objSqlParam, ref objDS);


            return objDS;

        }

        public bool IsDuplicateRef_No()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(objIPartyRecieptVoucherView.Session_BillGrid.Copy());
            ds.Tables[0].TableName = "voucherbillbybill";
            ds.Tables[0].Columns["Bill_No"].ColumnName = "Ref_No";
            string errMsg = "";
            SqlParameter[] objSqlParam = { 
                                       objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,objIPartyRecieptVoucherView.keyID), 
                                       objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,0,_hierarchyCode),
                                       objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId ),
                                       objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                       objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIPartyRecieptVoucherView.ClientLedgerID),
                                       objDAL.MakeInParams("@RefNo_XML", SqlDbType.Xml,0,ds.GetXml().ToLower())               
                                         };

            objDAL.RunProc("FA_Opr_Voucher_IsDuplicateBill", objSqlParam, ref objDS);

            if (Convert.ToBoolean(objDS.Tables[0].Rows[0]["IsDuplicate"]) == true)
            {
                errMsg = objDS.Tables[0].Rows[0]["DuplicateRefNo"].ToString();
                errMsg = errMsg.Remove(errMsg.Length - 1);
                objIPartyRecieptVoucherView.errorMessage = "Duplicate Ref No :  " + errMsg;

                return false;
            }
            return true;


        }
    }
}