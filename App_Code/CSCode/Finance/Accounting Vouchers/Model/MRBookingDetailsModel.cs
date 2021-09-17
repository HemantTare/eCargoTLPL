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
/// Summary description for MRBookingDetailsModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class MRBookingDetailsModel : IModel
    {
        private DataSet _ds = new DataSet();
        private IMRBookingDetailsView _MRBookingDetailsView;
        private DAL _objDAL = new DAL();

        private int _userID = UserManager.getUserParam().UserId;
        private int _Year_Code = UserManager.getUserParam().YearCode;
        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _division_Id = UserManager.getUserParam().DivisionId;


        public MRBookingDetailsModel(IMRBookingDetailsView MRBookingDetailsView)
        {
            _MRBookingDetailsView = MRBookingDetailsView;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                _objDAL.MakeOutParams("@Print_Doc_Id", SqlDbType.Int, 0), 
                _objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_Year_Code),
                _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_division_Id),
                _objDAL.MakeInParams("@MR_Type_ID",SqlDbType.Int,0,1),
                _objDAL.MakeInParams("@MR_ID",SqlDbType.Int,0,_MRBookingDetailsView.keyID),
                _objDAL.MakeInParams("@MR_Date",SqlDbType.DateTime,0,_MRBookingDetailsView.MRGeneralDetailsView.MRDate),
                _objDAL.MakeInParams("@MR_Branch_ID",SqlDbType.Int,0,_main_Id),
                _objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,_MRBookingDetailsView.MRGeneralDetailsView.GC_ID),
                _objDAL.MakeInParams("@Total_MR_Amount",SqlDbType.Decimal,0,_MRBookingDetailsView.MRGeneralDetailsView.Total_Receivables),
                _objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,_MRBookingDetailsView.MRCashChequeDetailsView.CashAmount),
                _objDAL.MakeInParams("@Cash_Ledger_ID",SqlDbType.Int,0,_MRBookingDetailsView.MRCashChequeDetailsView.CashLedgerID),
                _objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,_MRBookingDetailsView.MRCashChequeDetailsView.ChequeAmount),
                _objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID),
                _objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,5,_hierarchy_Code),
                _objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Raj.EC.Common.GetMenuItemId()),
                _objDAL.MakeInParams("@Document_Allocation_ID",SqlDbType.Int,0,_MRBookingDetailsView.MRGeneralDetailsView.Document_Allocation_ID),
                _objDAL.MakeInParams("@MR_No",SqlDbType.Int,0,_MRBookingDetailsView.MRGeneralDetailsView.Next_No),
                _objDAL.MakeInParams("@MR_No_For_Print",SqlDbType.VarChar,20,_MRBookingDetailsView.MRGeneralDetailsView.MRNo),
                _objDAL.MakeInParams("@MRChequeDetailsXML",SqlDbType.Xml,0,_MRBookingDetailsView.MRCashChequeDetailsView.MRChequeDetailsXML),
                };


            _objDAL.RunProc("EC_FA_MR_Booking_Save", objSqlParam);
             

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _MRBookingDetailsView.ClearVariables();
                _Msg = "Saved SuccessFully";

                if (_MRBookingDetailsView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (_MRBookingDetailsView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Print_Doc_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Print_Doc_ID)));
                }

            }            
            return objMessage;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = 
                                    { 
                                    _objDAL.MakeInParams("@MR_ID",SqlDbType.Int,0,_MRBookingDetailsView.keyID)                                    
                                    };
            _objDAL.RunProc("EC_FA_MR_Booking_Read", sqlpara, ref _ds);

            return _ds;         
        }
    }
}
