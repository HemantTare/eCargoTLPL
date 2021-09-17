using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for PODCoverRecieptModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class PODCoverRecieptModel : IModel
    {
        private IPODCoverRecieptView objIPODCoverRecieptView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public PODCoverRecieptModel(IPODCoverRecieptView pODCoverRecieptView)
        {
            objIPODCoverRecieptView = pODCoverRecieptView;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@Receipt_Id", SqlDbType.Int, 0, objIPODCoverRecieptView.keyID) ,
                objDAL.MakeInParams("@ReceiptDate", SqlDbType.DateTime, 0, objIPODCoverRecieptView.ReceiptDate) ,
                objDAL.MakeInParams("@MainID", SqlDbType.Int, 0, _branchID),
                objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 5, _hierarchyCode),
                objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionID)};

            objDAL.RunProc("EC_Opr_PODCoverReceipt_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Cover_Received_ID", SqlDbType.Int, 0, objIPODCoverRecieptView.keyID),
                                           objDAL.MakeInParams("@Cover_ID", SqlDbType.Int, 0, objIPODCoverRecieptView.CoverNo) };
            objDAL.RunProc("EC_Opr_PODCoverReceipt_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {
                objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                             
                objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),                                            
                objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,_branchID),
                objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Raj.EC.Common.GetMenuItemId()),   
                objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,0,_hierarchyCode),                                            
                objDAL.MakeInParams("@Cover_Received_ID",SqlDbType.Int,0,objIPODCoverRecieptView.keyID),
                objDAL.MakeInParams("@Cover_Received_Hierarchy_Code",SqlDbType.VarChar,2,objIPODCoverRecieptView.HierachyCode),
                objDAL.MakeInParams("@Cover_Received_Main_ID",SqlDbType.Int,0,objIPODCoverRecieptView.MainId),
                objDAL.MakeInParams("@Cover_Received_Date",SqlDbType.DateTime,0,objIPODCoverRecieptView.ReceiptDate),
                objDAL.MakeInParams("@Cover_ID",SqlDbType.Int,0,objIPODCoverRecieptView.CoverNo),
                objDAL.MakeInParams("@Sent_Type_ID", SqlDbType.Int, 0,objIPODCoverRecieptView.PODSentByView.SentByID),
                objDAL.MakeInParams("@Courier_Name", SqlDbType.VarChar, 100, objIPODCoverRecieptView.PODSentByView.CourierName),
                objDAL.MakeInParams("@Courier_Docket_No", SqlDbType.VarChar, 20, objIPODCoverRecieptView.PODSentByView.CourierDocketNo),
                objDAL.MakeInParams("@Emp_ID", SqlDbType.Int,0,objIPODCoverRecieptView.PODSentByView.EmployeeID),
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0,objIPODCoverRecieptView.PODSentByView.VehicleID),
                objDAL.MakeInParams("@CoverReceivedDetailsXML",SqlDbType.Xml,0,objIPODCoverRecieptView.CoverReceivedDetailsXML),                                            
                objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250,objIPODCoverRecieptView.Remark),                                                                                        
                objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId)};

            objDAL.RunProc("EC_Opr_PODCoverReceipt_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIPODCoverRecieptView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objIPODCoverRecieptView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/POD/FrmPODCoverReciept.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIPODCoverRecieptView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }
            return objMessage;
        }
    }
}