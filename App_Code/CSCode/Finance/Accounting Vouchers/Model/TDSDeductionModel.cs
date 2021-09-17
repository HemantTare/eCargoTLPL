using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP.UI;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC;


/// <summary>
/// Name : Ankit Champaneriya
/// Summary description for TDSDeductionModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class TDSDeductionModel : IModel
    {

        private DataSet _ds;
        private ITDSDeductionView _TDSDeductionView;
        private DAL _objDAL = new DAL();

        //private int _userID = 53;
        //private int _Year_Code = 8;
        //private string _hierarchy_Code = "HO";
        //private int _main_Id = 1;
        //private int _system_Id = 1;
        //private int _division_Id = 1;

        private int _userID = Convert.ToInt32(UserManager.getUserParam().UserId); 
        private int _Year_Code = Convert.ToInt32(UserManager.getUserParam().YearCode); // Convert.ToInt32(Param.getUserParam().YearCode);
        private string _hierarchy_Code = Convert.ToString(UserManager.getUserParam().HierarchyCode); //Convert.ToString(Param.getUserParam().HierarchyCode);
        private int _main_Id = Convert.ToInt32(UserManager.getUserParam().MainId);     // Convert.ToInt32(Param.getUserParam().MainId);
        private int _division_Id = Convert.ToInt32(UserManager.getUserParam().DivisionId);

        public TDSDeductionModel(ITDSDeductionView TDSDeductionView)
        {
            _TDSDeductionView = TDSDeductionView;
        }


        public Message Save()
        {
            Message objMessage = new Message();
                       

            //_objDAL.MakeInParams("@TDSDeduction_Id", SqlDbType.Int, 0, _TDSDeductionView.keyID),                                        

            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                        _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                        
                                        _objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_Year_Code ),
                                        _objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,_division_Id ),
                                        _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar , 0,_hierarchy_Code  ),
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,_main_Id ),
                                        _objDAL.MakeInParams("@Voucher_Type_Id", SqlDbType.Int, 0, 6),
                                        _objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime, 0,_TDSDeductionView.TDSDeduction_Date ),
                                        _objDAL.MakeInParams("@Ref_No", SqlDbType.VarChar , 0,_TDSDeductionView.Reference_No),                                        
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,_TDSDeductionView.Ledger_Account_Id    ),
                                        _objDAL.MakeInParams("@TDS_Ledger_Id", SqlDbType.Int, 0,_TDSDeductionView.TDS_Ledger_Id     ),
                                        _objDAL.MakeInParams("@Total_Amount_Payable", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Total_Amount_Paid_Amount      )),
                                        _objDAL.MakeInParams("@Tax_Rate", SqlDbType.Decimal , 0,Convert.ToDecimal( _TDSDeductionView.Tax_Percent  ) ),
                                        _objDAL.MakeInParams("@Tax_Amount", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Tax_Amount      )),
                                        _objDAL.MakeInParams("@Surcharge_Rate", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Surcharge_Percent       )),
                                        _objDAL.MakeInParams("@Surcharge_Amount", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Surcharge_Amount       )),
                                        _objDAL.MakeInParams("@Addl_Surcharge_Rate", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Addl_Ed_Cess_Percent )),
                                        _objDAL.MakeInParams("@Addl_Surcharge_Amount", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Addl_Ed_Cess_Amount  )), 
                                        _objDAL.MakeInParams("@Addl_Edu_Cess_Rate", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Addl_Ed_Cess_Percent  )),
                                        _objDAL.MakeInParams("@Addl_Edu_Cess_Amount", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Addl_Ed_Cess_Amount  )),
                                        _objDAL.MakeInParams("@Total_TDS_Amount", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Total_TDS_Amount  )),
                                        _objDAL.MakeInParams("@Less_TDS_Deducted", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Less_TDS_Deducted_Till_Date_Amount   )),
                                        _objDAL.MakeInParams("@Net_TDS_To_Deduct", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Net_TDS_To_Deduct_Amount   )),
                                        _objDAL.MakeInParams("@Bill_No", SqlDbType.VarChar  , 0,_TDSDeductionView.Bill_No  ), 
                                        _objDAL.MakeInParams("@Total_Debit", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Amount   )),
                                        _objDAL.MakeInParams("@Total_Credit", SqlDbType.Decimal , 0,Convert.ToDecimal(_TDSDeductionView.Amount  )),
                                        _objDAL.MakeInParams("@Narration", SqlDbType.VarChar , 0,_TDSDeductionView.Narration  ),
                                        _objDAL.MakeInParams("@Is_Manual_Entry", SqlDbType.Int , 0,0 ),                        
                                        _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};



            _objDAL.RunProc("FA_TDS_Deduction_Save", objSqlParam);


            System.Web.HttpContext.Current.Session["TDSDeductionOtherCharges"] = null;

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);



            if (objMessage.messageID == 0)
            {

                string _Msg;
                _Msg = "Saved SuccessFully";

                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }

            return objMessage;
        }


        public DataSet Fill_Values()
        {

            //SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _division_Id) };

            //_objDAL.RunProc("EC_FA_TDS_Deduction_Fill_Ledger_Group", sqlParam, ref _ds);
            _objDAL.RunProc("EC_FA_TDS_Deduction_Fill_Ledger_Group", ref _ds);

            return _ds;
        }

        public DataSet Fill_TDS_Ledger()
        {
            //_Is_VT=Convert.ToBoolean(Param.getUserParam().Is_VT;

            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Division_Id", SqlDbType.Int , 0, _division_Id),
                                        _objDAL.MakeInParams("@Ledger_Account_Id", SqlDbType.Int , 0, _TDSDeductionView.Ledger_Account_Id ) };

            _objDAL.RunProc("EC_FA_TDS_Deduction_Fill_TDS_Ledger", sqlParam, ref _ds);

            return _ds;
        }

        public DataSet Fill_Pending_Bills()
        {
            //_Is_VT=Convert.ToBoolean(Param.getUserParam().Is_VT;

            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Division_Id", SqlDbType.Int , 0, _division_Id),
                                        _objDAL.MakeInParams("@TDS_Ledger_Id", SqlDbType.Int , 0, _TDSDeductionView.TDS_Ledger_Id  ) ,
                                        _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar  , 0, _hierarchy_Code   ),
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0, _main_Id ),                                                                         
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int , 0, _TDSDeductionView.Ledger_Account_Id ) ,
                                        _objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int   , 0, _TDSDeductionView.Vendor_Id  ) };

            _objDAL.RunProc("FA_TDS_Deduction_Fill_Pending_Bills", sqlParam, ref _ds);

            return _ds;
        }

        public DataSet Get_TDS_Ledger_Details()
        {
            //_Is_VT=Convert.ToBoolean(Param.getUserParam().Is_VT;

            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Division_Id", SqlDbType.Int , 0, _division_Id ),
                                        _objDAL.MakeInParams("@TDS_Ledger_Id", SqlDbType.Int , 0, _TDSDeductionView.TDS_Ledger_Id  ) ,
                                        _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar  , 0, _hierarchy_Code   ),
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0, _main_Id ),                                                                         
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int , 0, _TDSDeductionView.Ledger_Account_Id ) ,
                                        _objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime  , 0, _TDSDeductionView.TDSDeduction_Date  ),
                                        _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime  , 0, UserManager.getUserParam().StartDate ),
                                        };

            _objDAL.RunProc("FA_Get_TDS_Deduction_Data", sqlParam, ref _ds);

            return _ds;
        }

        public DataSet Fill_Ledger_Account()
        {

            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Division_Id", SqlDbType.Int , 0, _division_Id ) ,
                                       _objDAL.MakeInParams("@Ledger_Group_Id", SqlDbType.Int , 0, _TDSDeductionView.Ledger_Group_Id) };

            _objDAL.RunProc("EC_FA_TDS_Deduction_Fill_Ledger_Account", sqlParam, ref _ds);
            return _ds;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Voucher_ID", SqlDbType.VarChar, 100, Convert.ToString( _TDSDeductionView.keyID )),
                                            _objDAL.MakeInParams("@Division_Id", SqlDbType.Int  , 0, _division_Id )};

            _objDAL.RunProc("FA_Opr_TDSDeduction_Get", objSqlParam, ref _ds);
            return _ds;
        }

        public string Generate_Voucher_No()
        {
            DataSet Ds = new DataSet();

            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar  , 0, _hierarchy_Code   ),
                                            _objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0, _main_Id ),
                                            _objDAL.MakeInParams("@Year_Code", SqlDbType.Int , 0, _Year_Code   ), 
                                            _objDAL.MakeInParams("@Voucher_Type_Id", SqlDbType.Int, 0, 6),                   
                                            _objDAL.MakeOutParams("@Voucher_No", SqlDbType.VarChar, 20),
                                            _objDAL.MakeInParams("@Update", SqlDbType.Bit, 1, 0)};

            //if (_Is_VT)                /// comment by Ankit
            //{
            //    _objDAL.RunProc("FA_VT_Generate_Voucher_No", objSqlParam,ref  Ds);
            //}
            //else
            //{
            //    _objDAL.RunProc("FA_VX_Generate_Voucher_No", objSqlParam, ref Ds);
            //}
            return Convert.ToString(objSqlParam[4].Value);
        }

    }
}



