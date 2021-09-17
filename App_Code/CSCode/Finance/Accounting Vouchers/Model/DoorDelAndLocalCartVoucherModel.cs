using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DoorDelAndLocalCartVoucherModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{

    public class DoorDelAndLocalCartVoucherModel:IModel 
    {
        private DataSet objDS;
        private IDoorDelAndLocalCartVoucherView objIDoorDelAndLocalCartVoucherView;
        private DAL objDAL = new DAL();

        private int _userID = UserManager.getUserParam().UserId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _mainId = UserManager.getUserParam().MainId;
        private int _divisionId = UserManager.getUserParam().DivisionId;
        
        public DoorDelAndLocalCartVoucherModel(IDoorDelAndLocalCartVoucherView doorDelAndLocalCartVoucherView)
        {
            objIDoorDelAndLocalCartVoucherView = doorDelAndLocalCartVoucherView;
        }
      
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Id", SqlDbType.Int, 0,objIDoorDelAndLocalCartVoucherView.keyID)
                                            
                                         };

            if (objIDoorDelAndLocalCartVoucherView.VoucherType == "Local")
            {
                objDAL.RunProc("EC_FA_Opr_LocalCartage_ReadValues", objSqlParam, ref objDS);
            }
            else
            {
                objDAL.RunProc("EC_FA_Opr_DoorDelVoucher_ReadValues", objSqlParam, ref objDS);
            }

            return objDS;
        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = {  objDAL.MakeInParams("@Id", SqlDbType.Int, 0,objIDoorDelAndLocalCartVoucherView.keyID),
                                            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0,_mainId),
                                            objDAL.MakeInParams("@GcXML", SqlDbType.Xml, 0,objIDoorDelAndLocalCartVoucherView.GCXML)
                                            
                                         };

            if (objIDoorDelAndLocalCartVoucherView.VoucherType == "Local")
            {
                objDAL.RunProc("EC_FA_Opr_LocalCartage_FillGrid", objSqlParam, ref objDS);
            }
            else
            {
                objDAL.RunProc("EC_FA_Opr_DoorDelVoucher_FillGrid", objSqlParam, ref objDS);               
            }            
          
            return objDS;
            

        }
        public Message Save()
        {
            Message objMessage = new Message();
            
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode ),
                                            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,2,_hierarchyCode ),
                                            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int ,0,Raj.EC.Common.GetMenuItemId()),                                            
                                            objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,_mainId ),
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Id",SqlDbType.Int,0,objIDoorDelAndLocalCartVoucherView.keyID),
                                            objDAL.MakeInParams("@No",SqlDbType.Int,0,objIDoorDelAndLocalCartVoucherView.keyID),
                                            objDAL.MakeInParams("@No_For_Print",SqlDbType.NVarChar,40,objIDoorDelAndLocalCartVoucherView.keyID),
                                            objDAL.MakeInParams("@Date",SqlDbType.DateTime,0,objIDoorDelAndLocalCartVoucherView.VoucherDate),
                                            objDAL.MakeInParams("@Ref_No",SqlDbType.VarChar,20,objIDoorDelAndLocalCartVoucherView.RefNo),
                                            objDAL.MakeInParams("@Is_Cash",SqlDbType.Bit,0,objIDoorDelAndLocalCartVoucherView.IsCash),
                                            objDAL.MakeInParams("@Is_Cheque",SqlDbType.Bit,0,objIDoorDelAndLocalCartVoucherView.IsCheque),
                                            objDAL.MakeInParams("@Cheque_No",SqlDbType.NVarChar,40,objIDoorDelAndLocalCartVoucherView.ChequeNo),
                                            objDAL.MakeInParams("@Cheque_Date",SqlDbType.DateTime,0,objIDoorDelAndLocalCartVoucherView.ChequeDate),
                                            objDAL.MakeInParams("@Cheque_In_Favour",SqlDbType.VarChar,100,objIDoorDelAndLocalCartVoucherView.ChequeInFavour),
                                            objDAL.MakeInParams("@Credit_To_Ledger_ID",SqlDbType.Int,0,objIDoorDelAndLocalCartVoucherView.CreditToLedgerID),
                                            objDAL.MakeInParams("@Total_GC",SqlDbType.Int,0,objIDoorDelAndLocalCartVoucherView.TotalGC),
                                            objDAL.MakeInParams("@Total_Amount",SqlDbType.Decimal,0,objIDoorDelAndLocalCartVoucherView.TotalAmount),
                                            objDAL.MakeInParams("@Remark",SqlDbType.VarChar,250,objIDoorDelAndLocalCartVoucherView.Remark),
                                            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,objIDoorDelAndLocalCartVoucherView.GetDetailsXML),
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                                            };


            if (objIDoorDelAndLocalCartVoucherView.VoucherType == "Local")
            {
                objDAL.RunProc("EC_FA_Opr_LocalCartage_Save", objSqlParam);
            }
            else
            {
                objDAL.RunProc("EC_FA_Opr_DoorDelVoucher_Save", objSqlParam);                
            }

  


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
                string _Msg;
                objIDoorDelAndLocalCartVoucherView.ClearVariables();
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }


    }
}