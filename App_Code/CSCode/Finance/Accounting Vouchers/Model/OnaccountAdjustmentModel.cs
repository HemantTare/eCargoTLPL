using System;
using System.Data;
using System.Data.SqlClient ;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess ;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;



namespace Raj.EC.FinanceModel
{
    public class OnaccountAdjustmentModel : ClassLibraryMVP.General.IModel 
    {
        private OnaccountAdjustmentView _OnAccAdjView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();

        public OnaccountAdjustmentModel(OnaccountAdjustmentView OnAccAdjView)
        {
            _OnAccAdjView = OnAccAdjView ;
        }

        public DataSet ReadValues()
        {
            return _ds;
        }

        public DataTable   Get_OnAccount_Data()
        {
            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5,UserManager.getUserParam().HierarchyCode),
                                       _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, _OnAccAdjView.LedgerId),
                                        _objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 1,UserManager.getUserParam().DivisionId)
                                    };

            _objDAL.RunProc("FA_Opr_OnAccount_UnAdjustment_Filling", sqlParam, ref _ds);

            _ds.Tables[0].TableName = "OnAccountbills";

            return _ds.Tables[0];

        }

        public DataTable Get_ONAccount_Adjusted_Data()
        {
            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode) , 
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,UserManager.getUserParam().MainId) , 
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, _OnAccAdjView.LedgerId) , 
                                        _objDAL.MakeInParams("@UnAdjustVoucherDate", SqlDbType.DateTime, 0, _OnAccAdjView.UnAdjustVoucherDate) , 
                                        _objDAL.MakeInParams("@UnAdjustVoucherId", SqlDbType.Int, 0, _OnAccAdjView.UnAdjustedVoucherId),
                                        _objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 1,UserManager.getUserParam().DivisionId),
                                      };

            _objDAL.RunProc("FA_Opr_OnAccount_Adjustment_Filling", sqlParam, ref _ds);

            _ds.Tables[0].TableName = "PendingBills";

            return _ds.Tables[0];

        }

        public DataTable Get_Ledger_Group()
        {

             _objDAL.RunProc("FA_Opr_OnAccount_FillLedgerGroup", ref _ds);


            //_ds.Tables[0].TableName = "LedgerGroup";

            return _ds.Tables[0];
            
        }

        public DataTable Blank_AdjustmentTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SrNo", typeof(Int32));
            dt.Columns.Add("Voucher_Id", typeof(Int32));
            dt.Columns.Add("Voucher_No", typeof(String));
            dt.Columns.Add("Voucher_Date", typeof(DateTime));
            dt.Columns.Add("BillAmount", typeof(double));
            dt.Columns.Add("PendingAmount", typeof(double));
            dt.Columns.Add("Process_Name", typeof(String));
            dt.Columns.Add("AdjustedAmount", typeof(double));
            dt.Columns.Add("Ledger_Id", typeof(Int32));

            return dt;
        }

        public DataTable Blank_UnAdjustmentTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SrNo", typeof(Int32));
            dt.Columns.Add("Voucher_Id", typeof(Int32));
            dt.Columns.Add("Voucher_No", typeof(String));
            dt.Columns.Add("Voucher_Date", typeof(DateTime));
            dt.Columns.Add("BillAmount", typeof(double));
            dt.Columns.Add("PendingAmount", typeof(double));
            //dt.Columns.Add("Process_Name", typeof(String));
            //dt.Columns.Add("AdjustedAmount", typeof(double));
            dt.Columns.Add("Ledger_Id", typeof(Int32));
            //dt.Columns.Add("Sr_No", typeof(Int32));
            //dt.Columns.Add("Voucher_Id", typeof(Int32));

            return dt;
        }

        public DataSet  Blank_LedgerData()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            dt.Columns.Add("Ledger_Name", typeof(string));
            dt.Columns.Add("Ledger_Id", typeof(Int32));

            ds.Tables.Add(dt.Copy());
            return ds;
        }

        public Message Save()
        {
            Message objMsg = new Message();
            
            DataSet ds = new DataSet();

            ds.Tables.Add(_OnAccAdjView.SessionOnAccountAdjusted.Copy());

            SqlParameter[] sqlParam = { _objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,0),
                                        _objDAL.MakeOutParams("@Error_Desc",SqlDbType.VarChar,4000),                                        
                                        _objDAL.MakeInParams("@UnadjustedVoucherID",SqlDbType.Int,0,_OnAccAdjView.UnAdjustedVoucherId),
                                        _objDAL.MakeInParams("@UnAdjustedSrNo",SqlDbType.Int,0,_OnAccAdjView.UnAdjustedSrNo) ,
                                        _objDAL.MakeInParams("@UnadjustedAmount",SqlDbType.Decimal,0,_OnAccAdjView.UnAdjustedAmount) , 
                                        _objDAL.MakeInParams("@Ledger_ID",SqlDbType.Int,0,_OnAccAdjView.LedgerId) , 
                                        _objDAL.MakeInParams("@PendingBills",SqlDbType.Xml,0,ds.GetXml())};

            _objDAL.RunProc("FA_Opr_OnAccount_Adjustment_Save", sqlParam);
            
            objMsg.messageID = Util.String2Int(sqlParam[0].Value.ToString()) ;
            objMsg.message = sqlParam[1].Value.ToString();

            if(objMsg.messageID ==0)
            {
                _OnAccAdjView.ClearVariables();
            }

            return objMsg;

        }
    }
}
