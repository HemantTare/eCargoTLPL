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
/// Summary description for ATHModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class ATHModel : IModel
    {
        private IATHView objIATHView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _mainID = UserManager.getUserParam().MainId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public ATHModel(IATHView ATHView)
        {
            objIATHView = ATHView;
        }

        public DataSet FillValues() 
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@ATH_ID", SqlDbType.Int, 0,objIATHView.keyID),
                objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0,_mainID),
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,objIATHView.VehicleID),
                objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
                objDAL.MakeInParams("@ATH_Date", SqlDbType.DateTime, 0,objIATHView.ATHDate),
                objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode)};

            objDAL.RunProc("dbo.EC_FA_ATH_Fill_LHPONo", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillLHPODetails()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0,objIATHView.LHPOID),
            objDAL.MakeInParams("@ATH_ID", SqlDbType.Int, 0,objIATHView.keyID),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0,_mainID),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode)};

            objDAL.RunProc("dbo.EC_FA_ATH_Fill_LHPODetails", objSqlParam, ref objDS);
            return objDS;
        }
        
        public DataSet FillVehicleDetails()
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIATHView.VehicleID) };

            objDAL.RunProc("dbo.EC_FA_ATH_Fill_VehicleDetails", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ATH_Id", SqlDbType.Int, 0, objIATHView.keyID) };

            objDAL.RunProc("dbo.EC_FA_ATH_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainID),
            objDAL.MakeInParams("@ATH_Id", SqlDbType.Int, 0,objIATHView.keyID),
            objDAL.MakeInParams("@ATH_Date", SqlDbType.DateTime,0,objIATHView.ATHDate),
            objDAL.MakeInParams("@Reference_No", SqlDbType.VarChar, 50,objIATHView.ReferenceNo),
            objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0,objIATHView.LHPOID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,objIATHView.VehicleID),
            objDAL.MakeInParams("@Allocation_Id", SqlDbType.Int, 0,objIATHView.Allocation_Id),
            objDAL.MakeInParams("@Advance_Payable_Amount", SqlDbType.Decimal, 0,objIATHView.AdvancePayableAmount),
            objDAL.MakeInParams("@Total_Paid_Amount", SqlDbType.Decimal, 0,objIATHView.TotalPaidAmount),
            objDAL.MakeInParams("@Total_Petrol_Slip_Amount", SqlDbType.Decimal, 0,objIATHView.TotalPetrolAmount),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIATHView.Remarks),
            objDAL.MakeInParams("@Cash_Amount", SqlDbType.Decimal, 0,objIATHView.CashChequeDetailsView.CashAmount),
            objDAL.MakeInParams("@Cheque_Amount", SqlDbType.Decimal, 0,objIATHView.CashChequeDetailsView.ChequeAmount),
            objDAL.MakeInParams("@Credit_To_Amount", SqlDbType.Decimal, 0,objIATHView.CreditAmountTo),
            objDAL.MakeInParams("@Credit_To_Ledger_Id", SqlDbType.Int, 0,objIATHView.CreditToLedgerId),
            objDAL.MakeInParams("@ChequeDetailsXML",SqlDbType.Xml,0,objIATHView.CashChequeDetailsView.MRChequeDetailsXML),
            objDAL.MakeInParams("@PetrolDetailsXML",SqlDbType.Xml,0,objIATHView.petrolDetailsXML)
            };

            objDAL.RunProc("dbo.EC_FA_ATH_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
                string _Msg;
                objIATHView.ClearVariables();

                _Msg = "Saved SuccessFully";
               
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;
        }
    }
}
