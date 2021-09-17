using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;


/// <summary>
/// Summary description for PODCoverGenerationModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class PODCoverGenerationModel : IModel
    {

        private IPODCoverGenerationView objIPODCoverGenerationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        public PODCoverGenerationModel(IPODCoverGenerationView PODCoverGenerationView)
        {
            objIPODCoverGenerationView = PODCoverGenerationView;
        }             

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Cover_Id", SqlDbType.Int, 0, objIPODCoverGenerationView.keyID),
            objDAL.MakeInParams("@Cover_Date", SqlDbType.DateTime, 0, objIPODCoverGenerationView.CoverDate),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2, _hierarchyCode),
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIPODCoverGenerationView.GetGCNoXML),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionID)};

            objDAL.RunProc("dbo.EC_Opr_PODCover_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Cover_ID", SqlDbType.Int, 0,objIPODCoverGenerationView.keyID),
            objDAL.MakeInParams("@Cover_Date", SqlDbType.DateTime,0,objIPODCoverGenerationView.CoverDate),
            objDAL.MakeInParams("@Cover_Made_Hierarchy_Code", SqlDbType.VarChar, 2, _hierarchyCode),
            objDAL.MakeInParams("@Cover_Maid_Main_ID", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Cover_Send_Hierarchy_Code", SqlDbType.VarChar, 2, objIPODCoverGenerationView.CoverSendHierarchyCode),
            objDAL.MakeInParams("@Cover_Sent_Main_ID", SqlDbType.Int, 0, objIPODCoverGenerationView.CoverSentMainID),
            objDAL.MakeInParams("@Cover_Sent_Type_ID", SqlDbType.Int, 0,objIPODCoverGenerationView.PODSentByView.SentByID),
            objDAL.MakeInParams("@Courier_Name", SqlDbType.VarChar, 100, objIPODCoverGenerationView.PODSentByView.CourierName),
            objDAL.MakeInParams("@Courier_Docket_No", SqlDbType.VarChar, 20, objIPODCoverGenerationView.PODSentByView.CourierDocketNo),
            objDAL.MakeInParams("@Emp_ID", SqlDbType.Int,0,objIPODCoverGenerationView.PODSentByView.EmployeeID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0,objIPODCoverGenerationView.PODSentByView.VehicleID),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIPODCoverGenerationView.Remarks),
            objDAL.MakeInParams("@PODCoverDetailsXML",SqlDbType.Xml,0,objIPODCoverGenerationView.PODCoverDetailsXML)};

            objDAL.RunProc("dbo.EC_Opr_PODCover_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIPODCoverGenerationView.ClearVariables();// added Ankit

                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objIPODCoverGenerationView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/POD/FrmPODCoverGeneration.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIPODCoverGenerationView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }           

            return objMessage;
        }
    }
}