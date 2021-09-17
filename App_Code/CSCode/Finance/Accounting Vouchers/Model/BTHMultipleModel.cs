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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP.UI;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC;
/// <summary>
/// Summary description for BTHMultipleModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class BTHMultipleModel : IModel
    {
        private DataSet _ds = new DataSet();
        private IBTHMultipleView _BTHMultipleView;
        private DAL objDAL = new DAL();

        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _division_Id = UserManager.getUserParam().DivisionId;
          
        public BTHMultipleModel(IBTHMultipleView BTHMultipleView)
        {
            _BTHMultipleView = BTHMultipleView;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                        objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
                                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_division_Id),
                                        objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                        objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,_BTHMultipleView.keyID),
                                        objDAL.MakeInParams("@BTH_Hierarchy_Code",SqlDbType.VarChar,2,_hierarchy_Code),
                                        objDAL.MakeInParams("@BTH_Main_ID",SqlDbType.Int,0,_main_Id),
                                        objDAL.MakeInParams("@BTH_Date",SqlDbType.DateTime,0,_BTHMultipleView.BTHVoucherDate),
                                        objDAL.MakeInParams("@Reference_No",SqlDbType.NVarChar,50,_BTHMultipleView.ReferenceNo),
                                        objDAL.MakeInParams("@Vehicle_Vendor_ID",SqlDbType.Int,0,_BTHMultipleView.BrokerOwnerID),
                                        objDAL.MakeInParams("@Total_Other_Amount",SqlDbType.Decimal,0,_BTHMultipleView.TotalOtherAmount),
                                        objDAL.MakeInParams("@Total_Payable_Amount",SqlDbType.Decimal,0,_BTHMultipleView.TotalPayableAmount),
                                        objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,_BTHMultipleView.MRCashChequeDetailsView.CashAmount),
                                        objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,_BTHMultipleView.MRCashChequeDetailsView.ChequeAmount),
                                        objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250,_BTHMultipleView.Remark),
                                        objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                        objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),
                                        objDAL.MakeInParams("@LHPODetailsXML",SqlDbType.Xml,0,_BTHMultipleView.LHPODetailsXML),
                                        objDAL.MakeInParams("@BankDetailsXML",SqlDbType.Xml,0,_BTHMultipleView.MRCashChequeDetailsView.MRChequeDetailsXML)};

            objDAL.RunProc("EC_FA_BTH_Multiple_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _BTHMultipleView.ClearVariables();
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

                if (_BTHMultipleView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
                }
                else if (_BTHMultipleView.Flag == "SaveAndNew")
                {
                    _BTHMultipleView.Msg = 1;
                    System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.RawUrl);
                }
                else if (_BTHMultipleView.Flag == "SaveAndPrint")
                {
                    //int Document_ID = _BTHMultipleView.keyID;
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    int Document_Type_ID = 10;
                    System.Web.HttpContext.Current.Response.Redirect("~/Reports/Direct_Printing/Frm_VoucherPrintingViewer.aspx?Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID) + "&Voucher_ID=" + ClassLibraryMVP.Util.EncryptInteger(_BTHMultipleView.keyID) + "&Document_Type_Id=" + ClassLibraryMVP.Util.EncryptInteger(Document_Type_ID));                    
                }
            }
            return objMessage;
        }              

        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = {  objDAL.MakeInParams("@Main_ID",SqlDbType.Int,0,_main_Id),
                                        objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,5,_hierarchy_Code),
                                        objDAL.MakeInParams("@Vendor_ID",SqlDbType.Int,0,_BTHMultipleView.BrokerOwnerID),
                                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0, _division_Id),
                                        objDAL.MakeInParams("@BTH_Date",SqlDbType.DateTime,0,_BTHMultipleView.BTHVoucherDate),
                                        objDAL.MakeInParams("@LHPOFrom_Date",SqlDbType.DateTime,0,_BTHMultipleView.LHPOFromDate),
                                        objDAL.MakeInParams("@LHPOTo_Date",SqlDbType.DateTime,0,_BTHMultipleView.LHPOToDate),
                                        objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,_BTHMultipleView.keyID)};

            objDAL.RunProc("EC_FA_BTH_Multiple_ReadValues", sqlpara, ref _ds);
            return _ds;
        }
       
    }

}