using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TDSPaymentDeductionModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class TDSPaymentDeductionModel : IModel
    {
        private DataSet objDS;
        private ITDSPaymentDeductionView objTDSPaymentDeductionView;
        private DAL _objDAL = new DAL();


      //  private int _userID = 53;

        private string _hierarchy_Code =Convert.ToString(UserManager.getUserParam().HierarchyCode);
        private int _main_Id =Convert.ToInt32(UserManager.getUserParam().MainId);
        private int _Division_Id = Convert.ToInt32(UserManager.getUserParam().DivisionId);

        public TDSPaymentDeductionModel(ITDSPaymentDeductionView tdsPaymentDeductionView)
        {
            objTDSPaymentDeductionView = tdsPaymentDeductionView;
        }

        public Message Save()
        {
            Message objMsg = new Message();
            return objMsg;

        }
        public DataSet GetTDSLedgerDetails()
        {

            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Division_Id", SqlDbType.Bit, 1, _Division_Id ),
                                        _objDAL.MakeInParams("@TDS_Ledger_Id", SqlDbType.Int , 0, objTDSPaymentDeductionView.TDS_Ledger_Id  ) ,
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int , 0, objTDSPaymentDeductionView.Ledger_Id  ) ,
                                        _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar  , 0, _hierarchy_Code   ),
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0, _main_Id ),                                                                    
                                       _objDAL.MakeInParams("@Amount", SqlDbType.Decimal , 0, objTDSPaymentDeductionView.Amount ) ,
                                        _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime  , 0, UserManager.getUserParam().StartDate ),
                                        _objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime  , 0, objTDSPaymentDeductionView.Voucher_Date )};

            _objDAL.RunProc("FA_Get_TDS_Deduction_AdvancePaymentData", sqlParam, ref objDS);

            return objDS;
        }

        public DataSet ReadValues()
        {
            return objDS;
        }

    }
}