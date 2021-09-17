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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

/// <summary>
/// Summary description for OctroiUpdateModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class OctroiUpdateModel : IModel
    {
        private IOctroiUpdateView objIOctroiUpdateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
         private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainID = UserManager.getUserParam().MainId;
      

        public OctroiUpdateModel(IOctroiUpdateView octroiUpdateView)
        {
            objIOctroiUpdateView = octroiUpdateView; 
        }       

        public DataSet FillValues()
        {
           
            objDAL.RunProc("[dbo].[EC_Opr_OctroiUpdate_FillValues]", ref objDS);
            //_setTableName(new string[] { "Ec_Master_Octroi_Form_Type", "Ec_Master_Octroi_Paid_By" }
            //                              );
            return objDS;
        }
        public DataSet GetLedgerGroupId()
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@LedgerId",SqlDbType.Int,0,objIOctroiUpdateView.LedgerID)
           };
            objDAL.RunProc("EC_Opr_OctroiUpdate_GetLedgerGroupID", objSqlParam, ref objDS);
           return objDS;

        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeOutParams("@Gcs_Octroi_Already_updated",SqlDbType.VarChar,1000),
            objDAL.MakeInParams("@YearCode",SqlDbType.Int,0,_yearCode),
            objDAL.MakeInParams("@OctroiUpdateId", SqlDbType.Int, 0, objIOctroiUpdateView.keyID) ,
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0, _divisionID) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIOctroiUpdateView.GetGCNoXML)
            };

            objDAL.RunProc("EC_Opr_OctroiUpdate_ReadValues", objSqlParam, ref objDS);

            objIOctroiUpdateView.GCAlreadyUpdated = Convert.ToString(objSqlParam[0].Value);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode), 
            objDAL.MakeInParams("@MainId",SqlDbType.Int,0,_MainID),
            objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
            objDAL.MakeInParams("@OctroiUpdateId", SqlDbType.Int, 0,objIOctroiUpdateView.keyID),
            objDAL.MakeInParams("@OctroiUpdateDate", SqlDbType.DateTime,0,objIOctroiUpdateView.TransactionDate),
            objDAL.MakeInParams("@BillNo",SqlDbType.VarChar,20,objIOctroiUpdateView.BillNo),
            objDAL.MakeInParams("@BillDate",SqlDbType.DateTime,0,objIOctroiUpdateView.BillDate),
            objDAL.MakeInParams("@LedgerId",SqlDbType.Int,0,objIOctroiUpdateView.LedgerID),  
            objDAL.MakeInParams("@TotalGC",SqlDbType.Int,0,objIOctroiUpdateView.Total_No_Of_GC),
            objDAL.MakeInParams("@TotalAmount",SqlDbType.Decimal,0,objIOctroiUpdateView.Total_Amount),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIOctroiUpdateView.Remarks),
            objDAL.MakeInParams("@OctroiUpdateDetailsXML",SqlDbType.Xml,0,objIOctroiUpdateView.OctroiUpdateDetailsXML),
            objDAL.MakeInParams("@ChequeNo",SqlDbType.NVarChar,10,objIOctroiUpdateView.ChequeNo),
            objDAL.MakeInParams("@ChequeDate",SqlDbType.DateTime,0,objIOctroiUpdateView.ChequeDate),
            objDAL.MakeInParams("@BankName",SqlDbType.VarChar,100,objIOctroiUpdateView.NameOfBank),
            objDAL.MakeInParams("@OtherChargeDetailsXML",SqlDbType.Xml,0,objIOctroiUpdateView.OtherChargeLedgerView.OtherDetailsXML),
            objDAL.MakeInParams("@Grand_Total",SqlDbType.Decimal,0,objIOctroiUpdateView.Grand_Total),
            objDAL.MakeInParams("@TotalOtherChargeAmount",SqlDbType.Decimal,0,objIOctroiUpdateView.OtherChargeAmount)
            };

            objDAL.RunProc("[dbo].[EC_Opr_OctroiUpdate_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIOctroiUpdateView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;

        }



	}
}
