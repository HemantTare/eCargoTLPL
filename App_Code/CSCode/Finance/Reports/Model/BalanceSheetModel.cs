using System;
using System.Data;

using Raj.EC.FinanceView;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 23-10-08
/// Summary description for Balance_Sheet_Model
/// </summary>

namespace Raj.EC.FinanceModel
{
    public class BalanceSheetModel : IModel
    {
        private IBalanceSheetView _Balance_Sheet_View;

        private DAL _dalobj = new DAL();
        private DataSet _ds;

        public BalanceSheetModel(IBalanceSheetView BalanceSheetView)
        {
            _Balance_Sheet_View = BalanceSheetView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] sqlpar =
            {
                _dalobj.MakeInParams("@IsConsolidated",SqlDbType.Bit,0,_Balance_Sheet_View.Is_Consol),
                _dalobj.MakeInParams("@StartDate",SqlDbType.DateTime,0,_Balance_Sheet_View.Start_Date),
                _dalobj.MakeInParams("@EndDate",SqlDbType.DateTime,0,_Balance_Sheet_View.End_Date),
                _dalobj.MakeInParams("@HierarchyCode",SqlDbType.VarChar,5,_Balance_Sheet_View.Hierarchy_Code),
                _dalobj.MakeInParams("@MainId",SqlDbType.Int,0,_Balance_Sheet_View.Main_Id),
                _dalobj.MakeInParams("@DivisionId",SqlDbType.Int,0,UserManager.getUserParam().DivisionId )
            };
            _dalobj.RunProc("FA_Rpt_Balance_Sheet", sqlpar, ref _ds);
            return _ds;
        }
        
        public Message Save()
        {
            Message mObj = new Message();
            return mObj;
        }
    }
}