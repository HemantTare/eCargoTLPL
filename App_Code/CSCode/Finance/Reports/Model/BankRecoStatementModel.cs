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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
/// <summary>
/// Summary description for BankRecoStatementModel
/// </summary>
/// 

namespace Raj.EC.FinanceModel
{
    public class BankRecoStatementModel : IModel
    {
        private IBankRecoStatementView _objIBankRecoStatementView;
        private DAL _objDAL = new DAL();
        private DataSet Ds = new DataSet();
        private int _divisionId = UserManager.getUserParam().DivisionId;
        public BankRecoStatementModel(IBankRecoStatementView objIBankRecoStatementView)
        {
            _objIBankRecoStatementView = objIBankRecoStatementView;
        }



        public DataSet ReadValues()
        {
            SqlParameter[] param =   {      
                                        _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, _objIBankRecoStatementView.Hierarchy_Code),
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _objIBankRecoStatementView.Main_Id),
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, _objIBankRecoStatementView.keyID),
                                        _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0, _objIBankRecoStatementView.Start_Date),
                                        _objDAL.MakeInParams("@End_Date", SqlDbType.DateTime, 0, _objIBankRecoStatementView.End_Date),
                                         _objDAL.MakeInParams("@Is_Consol", SqlDbType.Bit, 0, _objIBankRecoStatementView.Is_Consol),
                                         _objDAL.MakeInParams("@Is_Uncleared", SqlDbType.Bit, 0, true)
                                  };

            _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill_Temp_Statement", param, ref Ds);
            return Ds;
        }
        //public DataSet ReadValues()
        //{
        //    SqlParameter[] param =   {      
        //                                _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,_objIBankRecoView.Hierarchy_Code),//"HO"),
        //                                _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _objIBankRecoView.Main_Id),//1),
        //                                _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,_objIBankRecoView.keyID ),//31527),
        //                                _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0,_objIBankRecoView.Start_Date),//Convert.ToDateTime("1/11/2009")),
        //                                _objDAL.MakeInParams("@End_Date", SqlDbType.DateTime, 0,_objIBankRecoView.End_Date),//Convert.ToDateTime("30/11/2009")),
        //                                 _objDAL.MakeInParams("@Is_Consol", SqlDbType.Bit, 0, _objIBankRecoView.Is_Consol),
        //                                 _objDAL.MakeInParams("@Is_Uncleared", SqlDbType.Bit, 0, _objIBankRecoView.Is_Uncleared)
        //                          };

        //    //_objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill_VT", param, ref Ds);
        //    _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill_Temp", param, ref Ds);

        //    return Ds;
        //}
        //public DataSet ReadValuesVX()
        //{
        //    SqlParameter[] param =   {      
        //                                _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, _objIBankRecoStatementView.Hierarchy_Code),
        //                                _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _objIBankRecoStatementView.Main_Id),
        //                                _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, _objIBankRecoStatementView.keyID),
        //                                _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0, _objIBankRecoStatementView.Start_Date),
        //                                _objDAL.MakeInParams("@End_Date", SqlDbType.DateTime, 0, _objIBankRecoStatementView.End_Date),
        //                          };

        //    _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill", param, ref Ds);
        //    return Ds;
        //}


        //public DataSet ReadValuesVT()
        //{
        //    SqlParameter[] param =   {      
        //                                _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, _objIBankRecoStatementView.Hierarchy_Code),
        //                                _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _objIBankRecoStatementView.Main_Id),
        //                                _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, _objIBankRecoStatementView.keyID),
        //                                _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0, _objIBankRecoStatementView.Start_Date),
        //                                _objDAL.MakeInParams("@End_Date", SqlDbType.DateTime, 0, _objIBankRecoStatementView.End_Date),
        //                          };

        //    _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill_VT", param, ref Ds);
        //    return Ds;
        //}

        public Message Save()
        {
            Message _objMessage = new Message();
            return _objMessage;

        }
    }
}