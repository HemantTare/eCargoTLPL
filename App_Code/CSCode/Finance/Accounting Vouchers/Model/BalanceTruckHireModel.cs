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
/// Summary description for BalanceTruckHireModel
/// </summary>
/// 

namespace Raj.EC.FinanceModel
{
    public class BalanceTruckHireModel:IModel
    {
        private DataSet _ds = new DataSet();
        private IBalanceTruckHireView _BalanceTruckHireView;
        private DAL objDAL = new DAL();
        
        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _division_Id = UserManager.getUserParam().DivisionId;

        public BalanceTruckHireModel(IBalanceTruckHireView BalanceTruckHireView)
        {
            _BalanceTruckHireView = BalanceTruckHireView;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_division_Id),
                                        objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                        objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,_BalanceTruckHireView.keyID),
                                        objDAL.MakeInParams("@BTH_Hierarchy_Code",SqlDbType.VarChar,2,_hierarchy_Code),
                                        objDAL.MakeInParams("@BTH_Main_ID",SqlDbType.Int,0,_main_Id),
                                        objDAL.MakeInParams("@BTH_Date",SqlDbType.DateTime,0,_BalanceTruckHireView.BTHVoucherDate),
                                        objDAL.MakeInParams("@Reference_No",SqlDbType.NVarChar,50,_BalanceTruckHireView.ReferenceNo),
                                        objDAL.MakeInParams("@Vehicle_Vendor_ID",SqlDbType.Int,0,_BalanceTruckHireView.BrokerOwnerID),
                                        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,_BalanceTruckHireView.VehicleSearchView.VehicleID),
                                        objDAL.MakeInParams("@Total_Payable_Amount",SqlDbType.Decimal,0,_BalanceTruckHireView.TotalPayableAmount),
                                        objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,_BalanceTruckHireView.MRCashChequeDetailsView.CashAmount),
                                        objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,_BalanceTruckHireView.MRCashChequeDetailsView.ChequeAmount),
                                        objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250,_BalanceTruckHireView.Remark),
                                        objDAL.MakeInParams("@Voucher_ID",SqlDbType.Int,0,0),
                                        objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                        objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),
                                        objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0,_BalanceTruckHireView.LHPONo_ID),
                                        objDAL.MakeInParams("@Balance_To_Be_Paid",SqlDbType.Decimal,0,_BalanceTruckHireView.Balance_To_Be_Paid),
                                        objDAL.MakeInParams("@OtherChargeDetailsXML",SqlDbType.Xml,0,_BalanceTruckHireView.OtherChargeDetailsView.OtherDetailsXML),
                                        objDAL.MakeInParams("@BankDetailsXML",SqlDbType.Xml,0,_BalanceTruckHireView.MRCashChequeDetailsView.MRChequeDetailsXML)};

            objDAL.RunProc("EC_FA_BTH_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _BalanceTruckHireView.ClearVariables();
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }

        public DataSet ReadValues()        
        {
            SqlParameter[] sqlpara = { objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,_BalanceTruckHireView.keyID)};

            objDAL.RunProc("EC_FA_BTH_Read",sqlpara, ref _ds);
            return _ds;
        }

        public DataSet Fill_LHPO_Dll()
        {
            SqlParameter[] sqlpara = {  objDAL.MakeInParams("@Main_ID",SqlDbType.Int,0,_main_Id),
                                        objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,5,_hierarchy_Code),
                                        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,_BalanceTruckHireView.VehicleSearchView.VehicleID),
                                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0, _division_Id),
                                        objDAL.MakeInParams("@BTH_Date",SqlDbType.DateTime,0,_BalanceTruckHireView.BTHVoucherDate),
                                        objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,_BalanceTruckHireView.keyID)};

            objDAL.RunProc("EC_FA_BTH_Fill_LHPO_Details", sqlpara, ref _ds);
            return _ds;
        }

        public DataSet Get_LHPO_Details()
        {
            SqlParameter[] sqlpara = {objDAL.MakeInParams("@Lhpo_ID",SqlDbType.Int,0,_BalanceTruckHireView.LHPONo_ID)};

            objDAL.RunProc("EC_FA_BTH_Get_LHPO_Details", sqlpara, ref _ds);
            return _ds;
        }
    }
}