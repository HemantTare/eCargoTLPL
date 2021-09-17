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
using Raj.EC.FinanceView;
/// <summary>
/// Summary description for TransportBillModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class TransportBillModel : IModel
    {
        private ITransportBillView objITransportBillView;
        private DAL objDAL = new DAL();
        Common objComm = new Common();
        private DataSet objDS;

        private int _mainID = UserManager.getUserParam().MainId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _divisionId = UserManager.getUserParam().DivisionId;

        private int _yearCode = (int)UserManager.getUserParam().YearCode; 

        public TransportBillModel(ITransportBillView TransportBillView)
        {
            objITransportBillView = TransportBillView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_FA_Transport_Bill_FillValues", ref objDS);
            return objDS;
        }

        public DataSet FillGCDetails()
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0, objITransportBillView.keyID),
                objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0, objITransportBillView.ClientID),
                objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionId),
                objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _mainID),
                objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
                objDAL.MakeInParams("@transport_bill_type_id",SqlDbType.Int,0,objITransportBillView.Bill_ForID),
		        objDAL.MakeInParams("@transport_bill_Date",SqlDbType.DateTime,0,objITransportBillView.BillDate),
                objDAL.MakeInParams("@BkgDlyRegion_Id",SqlDbType.Int, 0,objITransportBillView.BkgDlyRegionId),
                objDAL.MakeInParams("@BkgDlyArea_Id",SqlDbType.Int, 0,objITransportBillView.BkgDlyAreaId),
                objDAL.MakeInParams("@BkgDlyBranch_Id",SqlDbType.Int, 0,objITransportBillView.BkgDlyBranchId),
                objDAL.MakeInParams("@IsBookingWise",SqlDbType.Int , 0,objITransportBillView.IsBookingWise)

            };

            //objDAL.RunProc("dbo.EC_FA_Transport_Bill_Fill_GCDetails_On_ClientSelection_New", objSqlParam, ref objDS);
            objDAL.RunProc("dbo.EC_FA_Transport_Bill_Fill_GCDetails_On_ClientSelection_DueDateWise", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0, objITransportBillView.keyID)};

            objDAL.RunProc("dbo.EC_FA_Transport_Bill_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadLedgerValues()
        {

            SqlParameter[] objSqlParam = {  objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainID),
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,-1),
                                            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,_yearCode)
                                         };

            objDAL.RunProc("EC_FA_Opr_TransportBill_Ledger_ReadValues", objSqlParam, ref objDS); 

            Common.SetTableName(new string[] { "LedgerDetails" }, objDS); 
            Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS.Tables["LedgerDetails"]);
 
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),    
            objDAL.MakeOutParams("@SpecialBillFormat", SqlDbType.Int, 0),    
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainID),
            objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0,objITransportBillView.keyID),
            objDAL.MakeInParams("@Bill_No",SqlDbType.Int,0,objITransportBillView.Next_No),
            objDAL.MakeInParams("@Bill_No_For_Print",SqlDbType.VarChar,20,objITransportBillView.BillNo),
            objDAL.MakeInParams("@Client_Id", SqlDbType.Int, 0,objITransportBillView.ClientID),
            objDAL.MakeInParams("@Bill_Type_ID", SqlDbType.Int, 0,objITransportBillView.BillTypeID),
            objDAL.MakeInParams("@Bill_Date", SqlDbType.DateTime,0,objITransportBillView.BillDate),
            objDAL.MakeInParams("@Bill_Ref_No", SqlDbType.NVarChar, 50,objITransportBillView.ReferenceNo),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objITransportBillView.Remarks),
            objDAL.MakeInParams("@BillDetailsXML",SqlDbType.Xml,0,objITransportBillView.BillDetailsXML),
            objDAL.MakeInParams("@BillOtherChargeGridXML",SqlDbType.Xml,0,objITransportBillView.BillOtherChargeGridXML),
            objDAL.MakeInParams("@LedgerDetailsXML",SqlDbType.Xml,0,objITransportBillView.LedgerDetailsXML),
            objDAL.MakeInParams("@Contact_Person", SqlDbType.VarChar  , 0,objITransportBillView.ContactPerson),
            objDAL.MakeInParams("@Billing_Name", SqlDbType.VarChar, 0,objITransportBillView.BillingName),
            objDAL.MakeInParams("@Billing_Address", SqlDbType.VarChar, 0,objITransportBillView.BillingAddress),            
            objDAL.MakeInParams("@Contact_No", SqlDbType.VarChar, 0,objITransportBillView.ContactNo),
            objDAL.MakeInParams("@Email", SqlDbType.VarChar, 0,objITransportBillView.Email),
            objDAL.MakeInParams("@Less_Amount", SqlDbType.Decimal,0,objITransportBillView.Less_Amount),
            objDAL.MakeInParams("@Total_Additional_Charges", SqlDbType.Decimal,0,objITransportBillView.TotalAmount),
            objDAL.MakeInParams("@Document_Allocation_ID",SqlDbType.Int,0,objITransportBillView.Document_Allocation_ID),
            objDAL.MakeInParams("@Trans_Bill_Type_Id",SqlDbType.Int,0,objITransportBillView.Bill_ForID),
            };

            objDAL.RunProc("dbo.EC_FA_Transport_Bill_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                objITransportBillView.ClearVariables();
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

                if (objITransportBillView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                            ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                            ClassLibraryMVP.Util.EncryptString("Operations/Booking/FrmGC.aspx?Menu_Item_Id=" +
                            ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objITransportBillView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                            ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objITransportBillView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);

                    string SpecialBillFormat =  objSqlParam[3].Value.ToString();

                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                            ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                            ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" 
                            + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode 
                            + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)
                            + "&SpecialBillFormat=" + SpecialBillFormat));
                }

            }
            return objMessage;
        }

        //public DataTable Fill_Bill_For()
        //{
        //    DataSet ds = new DataSet();

        //    ds = objComm.Get_Values_Where("ec_master_credit_memo_for", "Credit_Memo_For_ID,Credit_Memo_For", "", "Credit_Memo_For_ID", false);

        //    return ds.Tables[0];
        //}
    }
}

