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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP.UI;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC;

namespace Raj.EC.FinanceModel
{
    public class MRDeliveryModel : IModel
    {
        private DataSet _ds = new DataSet();
        private IMRDeliveryView _MRDeliveryView;
        private DAL objDAL = new DAL();
        Common objComm = new Common();

        private int _userID = UserManager.getUserParam().UserId;
        private int _Year_Code = UserManager.getUserParam().YearCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _division_Id = UserManager.getUserParam().DivisionId;

        private int _Print_Doc_Id = 0;

        public MRDeliveryModel(IMRDeliveryView MRDeliveryView)
        {
            _MRDeliveryView = MRDeliveryView;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            if (Common.GetMenuItemId() == 108) //means MR DElivery
            {
                SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                        objDAL.MakeOutParams("@Print_Doc_Id", SqlDbType.Int, 0), 
                        objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_Year_Code),
                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_division_Id),
                        objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.Document_Allocation_ID),
                        objDAL.MakeInParams("@MR_Type_ID",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.MR_Type_ID),
                        objDAL.MakeInParams("@MR_ID",SqlDbType.Int,0,_MRDeliveryView.keyID),
                        objDAL.MakeInParams("@MR_No",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.Next_No),
                        objDAL.MakeInParams("@MR_No_For_Print",SqlDbType.VarChar,20,_MRDeliveryView.MRGeneralDetailsView.MRNo),
                        objDAL.MakeInParams("@MR_Date",SqlDbType.DateTime,0,_MRDeliveryView.MRGeneralDetailsView.MRDate),
                        objDAL.MakeInParams("@MR_Branch_ID",SqlDbType.Int,0,_main_Id),
                        objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.GC_ID),
                        objDAL.MakeInParams("@GC_Sub_Total",SqlDbType.Decimal,0,_MRDeliveryView.GCSubTotal),
                        objDAL.MakeInParams("@Octroi_Form_Charges",SqlDbType.Decimal,0,_MRDeliveryView.OctrFormCharges),
                        objDAL.MakeInParams("@Octroi_Service_Charges",SqlDbType.Decimal,0,_MRDeliveryView.OctrServiceCharges),
                        objDAL.MakeInParams("@GI_Charges",SqlDbType.Decimal,0,_MRDeliveryView.GICharges),
                        objDAL.MakeInParams("@Detention_Charges",SqlDbType.Decimal,0,_MRDeliveryView.DetentionCharges),
                        objDAL.MakeInParams("@Hamali_Charges",SqlDbType.Decimal,0,_MRDeliveryView.HamaliCharges),
                        objDAL.MakeInParams("@Local_Charges",SqlDbType.Decimal,0,_MRDeliveryView.LocalCharges),
                        objDAL.MakeInParams("@Demurage_Days",SqlDbType.Int,0,_MRDeliveryView.DemurageDays),
                        objDAL.MakeInParams("@Demurage_Charges",SqlDbType.Decimal,0,_MRDeliveryView.DemurageCharges),
                        objDAL.MakeInParams("@Additional_Charges",SqlDbType.Decimal,0,_MRDeliveryView.AdditionalCharges),
                        objDAL.MakeInParams("@Additional_Charges_Remarks",SqlDbType.VarChar,200,_MRDeliveryView.AddChrgRemark),
                        objDAL.MakeInParams("@Discount_Amount",SqlDbType.Decimal,0,_MRDeliveryView.DiscountAmount),
                        objDAL.MakeInParams("@Discount_Amount_Remarks",SqlDbType.VarChar,100,_MRDeliveryView.DiscountRemark),
                        objDAL.MakeInParams("@Tax_Abatement",SqlDbType.Decimal,0,_MRDeliveryView.TaxAbate),
                        objDAL.MakeInParams("@Amount_Taxable",SqlDbType.Decimal,0,_MRDeliveryView.AmountTaxable),
                        objDAL.MakeInParams("@Service_Tax_Percent",SqlDbType.Decimal,0,_MRDeliveryView.Service_Tax_Percent),
                        objDAL.MakeInParams("@Service_Tax_Amount",SqlDbType.Decimal,0,_MRDeliveryView.ServiceTax),
                        objDAL.MakeInParams("@Service_Tax_Payable_By",SqlDbType.Int,0,_MRDeliveryView.Service_Pay_By_ID),
                        objDAL.MakeInParams("@Rebooked_Charges",SqlDbType.Decimal,0,_MRDeliveryView.RebookedCharges),
                        objDAL.MakeInParams("@Octroi_Amount",SqlDbType.Decimal,0,_MRDeliveryView.OctrAmount),
                        objDAL.MakeInParams("@Octroi_Receipt_No",SqlDbType.NVarChar,50,_MRDeliveryView.OctrRecNo),
                        objDAL.MakeInParams("@Octroi_Receipt_Date",SqlDbType.DateTime,0,_MRDeliveryView.OctrRecDate),
                        objDAL.MakeInParams("@Octroi_Form_Type_ID",SqlDbType.Int,0,Util.String2Int(_MRDeliveryView.OctrFormType)),
                        objDAL.MakeInParams("@Octroi_Paid_By_ID",SqlDbType.Int,0,Util.String2Int(_MRDeliveryView.OctrPaidBy)),
                        objDAL.MakeInParams("@Total_MR_Amount",SqlDbType.Decimal,0,_MRDeliveryView.TotalReceivables),
                        objDAL.MakeInParams("@Std_Octroi_Form_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Octroi_Form_Charges),
                        objDAL.MakeInParams("@Std_Octroi_Service_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Octroi_Service_Charges),
                        objDAL.MakeInParams("@Std_GI_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_GI_Charges),
                        objDAL.MakeInParams("@Std_Detention_Charges",SqlDbType.Decimal,0,_MRDeliveryView.DetentionCharges),
                        objDAL.MakeInParams("@Std_Hamali_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Hamali_Charges),
                        objDAL.MakeInParams("@Std_Local_Charges",SqlDbType.Decimal,0,_MRDeliveryView.LocalCharges),
                        objDAL.MakeInParams("@Std_Demurage_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Demurage_Charges),
                        objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,_MRDeliveryView.MRCashChequeDetailsView.CashAmount),
                        objDAL.MakeInParams("@Cash_Ledger_ID",SqlDbType.Int,0,_MRDeliveryView.MRCashChequeDetailsView.CashLedgerID),
                        objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,_MRDeliveryView.MRCashChequeDetailsView.ChequeAmount),
                        objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID),
                        objDAL.MakeInParams("@MRChequeDetailsXML",SqlDbType.Xml,0,_MRDeliveryView.MRCashChequeDetailsView.MRChequeDetailsXML),
                        objDAL.MakeInParams("@DeliveryCommission",SqlDbType.Decimal,0,_MRDeliveryView.DeliveryCommission),
                        objDAL.MakeInParams("@Debit_To_Ledger_ID",SqlDbType.Int,0,_MRDeliveryView.Debit_To_Ledger_ID),
                        objDAL.MakeInParams("@ReceivedBy",SqlDbType.Int,0,_MRDeliveryView.ReceivedBy),
                        objDAL.MakeInParams("@billing_branch_id",SqlDbType.Int,0,_MRDeliveryView.Debit_To_Branch_ID),
                        objDAL.MakeInParams("@ThroughMR",SqlDbType.VarChar,100,_MRDeliveryView.MRDeliveryDetailsView.ThroughMr),
                        objDAL.MakeInParams("@DeliveredToId",SqlDbType.Int,0,_MRDeliveryView.MRDeliveryDetailsView.DeliveredToID),
                        objDAL.MakeInParams("@DeliveryAgainstId",SqlDbType.Int,0,_MRDeliveryView.MRDeliveryDetailsView.DeliveryAgainstID)
                        ,objDAL.MakeInParams("@Round_Off",SqlDbType.Int,0,_MRDeliveryView.RoundOff)
                     };

                objDAL.RunProc("EC_FA_MR_Delivery_Save", objSqlParam);

                objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
                objMessage.message = Convert.ToString(objSqlParam[1].Value);
                _Print_Doc_Id = Convert.ToInt32(objSqlParam[2].Value);
            }
            else if (Common.GetMenuItemId() == 195) //means Credit Memo
            {
                SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                        objDAL.MakeOutParams("@Print_Doc_Id", SqlDbType.Int, 0), 
                        objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_Year_Code),
                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_division_Id),
                        objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.Document_Allocation_ID),
                        objDAL.MakeInParams("@CreditMemo_For_ID",SqlDbType.Int,0,_MRDeliveryView.Credit_Memo_ForID),
                        objDAL.MakeInParams("@CreditMemo_ID",SqlDbType.Int,0,_MRDeliveryView.keyID),
                        objDAL.MakeInParams("@CreditMemo_No",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.Next_No),
                        objDAL.MakeInParams("@CreditMemo_No_For_Print",SqlDbType.VarChar,20,_MRDeliveryView.MRGeneralDetailsView.MRNo),
                        objDAL.MakeInParams("@CreditMemo_Date",SqlDbType.DateTime,0,_MRDeliveryView.MRGeneralDetailsView.MRDate),
                        objDAL.MakeInParams("@CreditMemo_Branch_ID",SqlDbType.Int,0,_main_Id),
                        objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.GC_ID),
                        objDAL.MakeInParams("@GC_Sub_Total",SqlDbType.Decimal,0,_MRDeliveryView.GCSubTotal),
                        objDAL.MakeInParams("@Octroi_Form_Charges",SqlDbType.Decimal,0,_MRDeliveryView.OctrFormCharges),
                        objDAL.MakeInParams("@Octroi_Service_Charges",SqlDbType.Decimal,0,_MRDeliveryView.OctrServiceCharges),
                        objDAL.MakeInParams("@GI_Charges",SqlDbType.Decimal,0,_MRDeliveryView.GICharges),
                        objDAL.MakeInParams("@Detention_Charges",SqlDbType.Decimal,0,_MRDeliveryView.DetentionCharges),
                        objDAL.MakeInParams("@Hamali_Charges",SqlDbType.Decimal,0,_MRDeliveryView.HamaliCharges),
                        objDAL.MakeInParams("@Local_Charges",SqlDbType.Decimal,0,_MRDeliveryView.LocalCharges),
                        objDAL.MakeInParams("@Demurage_Days",SqlDbType.Int,0,_MRDeliveryView.DemurageDays),
                        objDAL.MakeInParams("@Demurage_Charges",SqlDbType.Decimal,0,_MRDeliveryView.DemurageCharges),
                        objDAL.MakeInParams("@Additional_Charges",SqlDbType.Decimal,0,_MRDeliveryView.AdditionalCharges),
                        objDAL.MakeInParams("@Additional_Charges_Remarks",SqlDbType.VarChar,200,_MRDeliveryView.AddChrgRemark),
                        objDAL.MakeInParams("@Discount_Amount",SqlDbType.Decimal,0,_MRDeliveryView.DiscountAmount),
                        objDAL.MakeInParams("@Discount_Amount_Remarks",SqlDbType.VarChar,100,_MRDeliveryView.DiscountRemark),
                        objDAL.MakeInParams("@Tax_Abatement",SqlDbType.Decimal,0,_MRDeliveryView.TaxAbate),
                        objDAL.MakeInParams("@Amount_Taxable",SqlDbType.Decimal,0,_MRDeliveryView.AmountTaxable),
                        objDAL.MakeInParams("@Service_Tax_Percent",SqlDbType.Decimal,0,_MRDeliveryView.Service_Tax_Percent),
                        objDAL.MakeInParams("@Service_Tax_Amount",SqlDbType.Decimal,0,_MRDeliveryView.ServiceTax),
                        objDAL.MakeInParams("@Service_Tax_Payable_By",SqlDbType.Int,0,_MRDeliveryView.Service_Pay_By_ID),
                        objDAL.MakeInParams("@Rebooked_Charges",SqlDbType.Decimal,0,_MRDeliveryView.RebookedCharges),
                        objDAL.MakeInParams("@Octroi_Amount",SqlDbType.Decimal,0,_MRDeliveryView.OctrAmount),
                        objDAL.MakeInParams("@Octroi_Receipt_No",SqlDbType.NVarChar,50,_MRDeliveryView.OctrRecNo),
                        objDAL.MakeInParams("@Octroi_Receipt_Date",SqlDbType.DateTime,0,_MRDeliveryView.OctrRecDate),
                        objDAL.MakeInParams("@Octroi_Form_Type_ID",SqlDbType.Int,0,Util.String2Int(_MRDeliveryView.OctrFormType)),
                        objDAL.MakeInParams("@Octroi_Paid_By_ID",SqlDbType.Int,0,Util.String2Int(_MRDeliveryView.OctrPaidBy)),
                        objDAL.MakeInParams("@Total_MR_Amount",SqlDbType.Decimal,0,_MRDeliveryView.TotalReceivables),
                        objDAL.MakeInParams("@Std_Octroi_Form_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Octroi_Form_Charges),
                        objDAL.MakeInParams("@Std_Octroi_Service_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Octroi_Service_Charges),
                        objDAL.MakeInParams("@Std_GI_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_GI_Charges),
                        objDAL.MakeInParams("@Std_Detention_Charges",SqlDbType.Decimal,0,_MRDeliveryView.DetentionCharges),
                        objDAL.MakeInParams("@Std_Hamali_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Hamali_Charges),
                        objDAL.MakeInParams("@Std_Local_Charges",SqlDbType.Decimal,0,_MRDeliveryView.LocalCharges),
                        objDAL.MakeInParams("@Std_Demurage_Charges",SqlDbType.Decimal,0,_MRDeliveryView.Std_Demurage_Charges),
                        objDAL.MakeInParams("@Debit_To_Ledger_ID",SqlDbType.Int,0,_MRDeliveryView.Debit_To_Ledger_ID),
                        objDAL.MakeInParams("@Debit_To_Branch_ID",SqlDbType.Int,0,_MRDeliveryView.Debit_To_Branch_ID),
                        objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID),
                        objDAL.MakeInParams("@Is_Credit_For_Consignee",SqlDbType.Bit,0,_MRDeliveryView.Is_Credit_For_Consignee),
                        objDAL.MakeInParams("@DeliveryCommission",SqlDbType.Decimal,0,_MRDeliveryView.DeliveryCommission)
                        ,objDAL.MakeInParams("@Round_Off",SqlDbType.Int,0,_MRDeliveryView.RoundOff)    
                    };

                objDAL.RunProc("EC_FA_Credit_Memo_Save", objSqlParam);

                objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
                objMessage.message = Convert.ToString(objSqlParam[1].Value);
                _Print_Doc_Id = Convert.ToInt32(objSqlParam[2].Value);
            }

            if (objMessage.messageID == 0)
            {
                string _Msg;

                _MRDeliveryView.ClearVariables(); 
                _Msg = "Saved SuccessFully";

                if (_MRDeliveryView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (_MRDeliveryView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(_Print_Doc_Id)));
                    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }
            return objMessage;
        }

        public DataSet ReadValues()
        {
            return _ds;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetCreditToBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            int _mainID = UserManager.getUserParam().MainId;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
              _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _mainID)};
            _objDAL.RunProc("EC_Opr_MemoToBranch_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)};
            _objDAL.RunProc("EC_FA_Opr_Ledger_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        public DataSet Get_Details_For_Delivery()
        {
           SqlParameter[] sqlpara = 
                                {
                                objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,_main_Id),
                                objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,_MRDeliveryView.MRGeneralDetailsView.GC_ID),
                                objDAL.MakeInParams("@MR_ID",SqlDbType.Int,0,_MRDeliveryView.keyID),
                                objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,_MRDeliveryView.Document_ID)};

                   objDAL.RunProc("EC_FA_MR_Get_Details_for_Delivery", sqlpara, ref _ds);

            return _ds;
        }

        public DataTable Fill_Credit_Memo_For()
        {
            _ds = objComm.Get_Values_Where("ec_master_credit_memo_for", "Credit_Memo_For_ID,Credit_Memo_For", "", "Credit_Memo_For_ID", false);

            return _ds.Tables[0];
        }

        private void _setTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                _ds.Tables[i].TableName = nameList[i];
            }

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_FA_MRDeliveryDetails_FillDropdown", ref _ds);
            _setTableName(new string[] { "EC_Master_Delivery_To",
                                         "EC_Master_Delivery_Against", 
                                         }
                                         );
            return _ds;
        }

    }
}