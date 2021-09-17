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

/// <summary>
/// Summary description for SupplementaryBillModel
/// </summary>
/// 

namespace Raj.EC.FinanceModel
{
    public class SupplementaryBillModel : IModel
    {

        private DataSet _ds = new DataSet();
        private ISupplementaryBillView _ISupplementaryBillView;
        private DAL objDAL = new DAL();
        Common objComm = new Common();

        private int _userID = UserManager.getUserParam().UserId;
        private int _Year_Code = UserManager.getUserParam().YearCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _division_Id = UserManager.getUserParam().DivisionId;
        private string _hierarchy_code = UserManager.getUserParam().HierarchyCode;

        public SupplementaryBillModel(ISupplementaryBillView SupplementaryBillView)
        {
            _ISupplementaryBillView = SupplementaryBillView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam =
            { objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0,_ISupplementaryBillView.keyID) };

            objDAL.RunProc("EC_FA_Supplementary_Bill_ReadValues", objSqlParam, ref _ds);
            
            return _ds;
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_FA_Supplementary_Bill_FillValues", ref _ds);
            return _ds;
        }

        public DataSet FillDetails()
        {
            SqlParameter[] objSqlParam =
            { 
                objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0,_ISupplementaryBillView.keyID),
                objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,_ISupplementaryBillView.Client_ID),
                objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _division_Id),
                objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _main_Id),
                objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5,_hierarchy_code),
            };

            objDAL.RunProc("EC_FA_SupplementaryBill_Fill_Details", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet FillGCDetails()
        {
            SqlParameter[] objSqlParam =
            { 
                objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0,_ISupplementaryBillView.GetGCNoXML),
                objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0,_ISupplementaryBillView.keyID),
                objDAL.MakeInParams("@BillDate", SqlDbType.DateTime, 0, _ISupplementaryBillView.BillDate),
                objDAL.MakeInParams("@ClientID", SqlDbType.Int, 0, _ISupplementaryBillView.Client_ID),
                objDAL.MakeInParams("@Service_Type_ID", SqlDbType.Int, 0, _ISupplementaryBillView.Service_Type_ID),
                objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _main_Id)
            };

            objDAL.RunProc("EC_FA_SupplementaryBill_ReadValues", objSqlParam, ref _ds);
            return _ds;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_division_Id),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_Year_Code),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5,_hierarchy_code),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_main_Id),
            objDAL.MakeInParams("@Bill_Id", SqlDbType.Int, 0,_ISupplementaryBillView.keyID),
            objDAL.MakeInParams("@Bill_No",SqlDbType.Int,0,_ISupplementaryBillView.Next_No),
            objDAL.MakeInParams("@Bill_No_For_Print",SqlDbType.VarChar,20,_ISupplementaryBillView.BillNo),
            objDAL.MakeInParams("@Client_Id", SqlDbType.Int, 0,_ISupplementaryBillView.Client_ID),
            objDAL.MakeInParams("@Bill_Date", SqlDbType.DateTime,0,_ISupplementaryBillView.BillDate),
            objDAL.MakeInParams("@Bill_Ref_No", SqlDbType.NVarChar, 50,_ISupplementaryBillView.ReferenceNo),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,_ISupplementaryBillView.Remarks),
            objDAL.MakeInParams("@BillDetailsXML",SqlDbType.Xml,0,_ISupplementaryBillView.GetDetailGridXML),
            objDAL.MakeInParams("@BillOtherChargeGridXML",SqlDbType.Xml,0,_ISupplementaryBillView.GetOtherChargeGridXML),
            objDAL.MakeInParams("@Contact_Person", SqlDbType.VarChar,50,_ISupplementaryBillView.ContactPerson),
            objDAL.MakeInParams("@Billing_Name", SqlDbType.VarChar,50,_ISupplementaryBillView.BillingName),
            objDAL.MakeInParams("@Billing_Address", SqlDbType.VarChar,1000,_ISupplementaryBillView.BillingAddress),            
            objDAL.MakeInParams("@Contact_No", SqlDbType.VarChar,50,_ISupplementaryBillView.ContactNo),
            objDAL.MakeInParams("@Email", SqlDbType.VarChar,50,_ISupplementaryBillView.Email),
            objDAL.MakeInParams("@Document_Allocation_ID",SqlDbType.Int,0,_ISupplementaryBillView.Document_Allocation_ID),
            objDAL.MakeInParams("@Total_Additional_Charges",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Service_Type_ID",SqlDbType.Int,0,_ISupplementaryBillView.Service_Type_ID)
           };

            objDAL.RunProc("EC_FA_Supplementary_Bill_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _ISupplementaryBillView.ClearVariables();
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

                if (_ISupplementaryBillView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
                }
                else if (_ISupplementaryBillView.Flag == "SaveAndNew")
                {
                    _ISupplementaryBillView.Msg = 1;
                    System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.RawUrl);
                }
            }

            return objMessage;
        }
        public DataSet Check_BackDatedEntry()
        {
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime,0,_ISupplementaryBillView.BillDate),
                                            objDAL.MakeInParams("@User_Id", SqlDbType.VarChar,2,UserManager.getUserParam().UserId),
                                            objDAL.MakeInParams("@VoucheTypeCode", SqlDbType.Int,0,60)

                                         };

            objDAL.RunProc("FA_Voucher_CheckIsVoucherEdit", objSqlParam, ref _ds);
            return _ds;
        }
    }
}