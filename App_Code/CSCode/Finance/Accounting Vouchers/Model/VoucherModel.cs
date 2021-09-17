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
using Raj.EC.FinanceView;

namespace Raj.EC.FinanceModel
{
    class VoucherModel : IModel
    {
        private IVoucherView objIVoucherView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;

        //private int _userID = 1;
        //private int _yearCode = 8;
        //private string _hierarchyCode = "HO";
        //private int _mainId = 1;
        //private int _divisionId = 1;

        
        
        public VoucherModel(IVoucherView VoucherView)
        {
            objIVoucherView = VoucherView;
        }

        public DataSet FillValues()
        {

            objDAL.RunProc("[EC_FA_Mst_Voucher_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,objIVoucherView.keyID),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,_yearCode)
                                         };


            if (objIVoucherView.IBTVoucherFlag == "View" || objIVoucherView.IBTVoucherFlag == "Edit")
            {
                objDAL.RunProc("[FA_Opr_IBT_Voucher_ReadValues]", objSqlParam, ref objDS);
            }
            else if (objIVoucherView.IBTVoucherFlag == "Accept")
            {
                objDAL.RunProc("[FA_Opr_IBT_Voucher_ForAccept_ReadValues]", objSqlParam, ref objDS);
            }
            else
            {
                objDAL.RunProc("[EC_FA_Mst_Voucher_ReadValues]", objSqlParam, ref objDS);
            }

            Common.SetTableName(new string[] { "Voucher", "VoucherDetails", "VoucherCostCentre", "VoucherBillByBill", "MstCostCentre", "MstRefType" }, objDS);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS.Tables["VoucherDetails"]);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Cost_Centre_ID" }, objDS.Tables["VoucherCostCentre"]);

            Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Ref_No" }, objDS.Tables["VoucherBillByBill"]);

            return objDS;
        }
            

        public Message Save()
        {
            Message objMessage = new Message();
            int _menuItemId=Common.GetMenuItemId();
            int _menuItemCode = Common.GetMenuItemCode();

            //string _detailsXML;
            
            //if (objIVoucherView.IsBankReco)
            //{ _detailsXML = objIVoucherView.GetBankRecoXML; }
            //else if (objIVoucherView.IsBillWise)
            //{ _detailsXML = objIVoucherView.GetBillWiseXML; }
            //else { _detailsXML = "<NewDataSet></NewDataSet>"; }

            int _firstLedgerId = Convert.ToInt32(objIVoucherView.SessionVoucherDT.Rows[0]["Ledger_Id"]);

          int _voucherTypeId;

          if (objIVoucherView.VoucherTypeID == 2222)
          {
              _voucherTypeId =2;
          }
          else if (objIVoucherView.VoucherTypeID ==3333)
          {
              _voucherTypeId =3;
          }
          else 
          { 
              _voucherTypeId = objIVoucherView.VoucherTypeID;
          }
 

             

            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),

                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode),
                                            objDAL.MakeInParams("@User_Id", SqlDbType.Int,0,_userID),

                                            objDAL.MakeInParams("@Voucher_Id",SqlDbType.Int,0,objIVoucherView.keyID),
                                            objDAL.MakeInParams("@Voucher_No",SqlDbType.VarChar,20,objIVoucherView.VoucherNo),
                                            objDAL.MakeInParams("@Voucher_Type_Id",SqlDbType.Int,0,_voucherTypeId),
                                            
                                            objDAL.MakeInParams("@Voucher_Date",SqlDbType.DateTime,0,objIVoucherView.VoucherDate),
                                            objDAL.MakeInParams("@Ref_No",SqlDbType.VarChar,20,objIVoucherView.RefNo),
                                            objDAL.MakeInParams("@Ledger_Id",SqlDbType.Int,0,_firstLedgerId),
                                            objDAL.MakeInParams("@Total_Debit",SqlDbType.Decimal,0,objIVoucherView.TotalDebit),
                                            objDAL.MakeInParams("@Total_Credit",SqlDbType.Decimal,0,objIVoucherView.TotalCredit),
                                            objDAL.MakeInParams("@Narration",SqlDbType.VarChar,200,objIVoucherView.Narration),
                                            objDAL.MakeInParams("@FBT_Payment_Type",SqlDbType.VarChar,100,objIVoucherView.FBTPaymentType),
                                            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),

                                            objDAL.MakeInParams("@VoucherXML", SqlDbType.Xml,0,objIVoucherView.GetVoucherXML),
                                            objDAL.MakeInParams("@VoucherCostCentreXML", SqlDbType.Xml,0,objIVoucherView.GetVoucherCostCentreXML),
                                            objDAL.MakeInParams("@VoucherBillByBillXML", SqlDbType.Xml,0,objIVoucherView.GetVoucherBillByBillXML),
                                           };


            if (objIVoucherView.IBTVoucherFlag == "Add" || objIVoucherView.IBTVoucherFlag == "Edit")
            {
                objDAL.RunProc("[FA_Opr_IBT_VoucherUnapprove_Save]", objSqlParam);
            }
            else if (objIVoucherView.IBTVoucherFlag == "Accept")
            {
                objDAL.RunProc("[FA_Opr_IBT_VoucherApprove_Save]", objSqlParam,ref objDS);
            }
            else if (_menuItemCode == 94 || _menuItemCode == 96 || _menuItemCode == 97 || _menuItemCode == 98 || _menuItemCode == 99 || _menuItemCode == 100)
            {
                objDAL.RunProc("[EC_FA_Mst_Voucher_ManualSave]", objSqlParam);
            }

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            //objIVoucherView.errorMessage = objMessage.message;
            string _Msg="";
            if (objMessage.messageID !=0)
            {
                objIVoucherView.errorMessage = objMessage.message;
            }
            else if (objMessage.messageID == 0)
            {
                objIVoucherView.ClearVariables();
                _Msg = "Saved SuccessFully";
                objIVoucherView.errorMessage = _Msg;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }
             
            return objMessage;
        }

        public DataSet GetLedgerParam()
        {
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIVoucherView.LedgerId),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId)

                                         };

            objDAL.RunProc("[EC_FA_Mst_Voucher_GetLedgerParam]", objSqlParam, ref objDS);


            return objDS;

        }
        public bool IsLedgerCashInHand()
        {
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeOutParams("@IsCashInHand", SqlDbType.Bit, 0), 
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIVoucherView.LedgerId) 
                                         };
            objDAL.RunProc("EC_FA_Mst_Voucher_IsCashInHand", objSqlParam, ref objDS);
            return Convert.ToBoolean(objSqlParam[0].Value);
        } 
    }
}
