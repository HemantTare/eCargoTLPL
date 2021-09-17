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
using Raj.EC.OperationView;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

namespace Raj.EC.OperationModel
{
    public class TruckBharaiModel : IModel 
    {

        private ITruckBharaiView objITruckBharaiView;
        private DAL objDAL = new DAL();
        private Common objCommon = new Common();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        public TruckBharaiModel(ITruckBharaiView TruckBharaiView)
        {
            objITruckBharaiView = TruckBharaiView;
        }

        public DataSet CheckVehicleStr()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Vehicle_id", SqlDbType.Int, 0, objITruckBharaiView.VehicleID) ,
            objDAL.MakeInParams("@PDS_Id", SqlDbType.Int, 0, objITruckBharaiView.keyID)};
            objDAL.RunProc("dbo.EC_Opr_PDS_Validate_Vehicle", objSqlParam, ref objDS);
            return objDS;
        }
 

        public DataSet ReadValues()
        {  
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Transaction_Id", SqlDbType.Int, 0, objITruckBharaiView.keyID),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objITruckBharaiView.VehicleID), 
            objDAL.MakeInParams("@Memo_Date", SqlDbType.DateTime, 0, objITruckBharaiView.TransactionDate)}; 

            objDAL.RunProc("EC_Opr_Truck_Bharai_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillSelectedMemoValues()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Transaction_Id", SqlDbType.Int, 0, objITruckBharaiView.keyID),
            objDAL.MakeInParams("@Memo_Id", SqlDbType.VarChar, 500, objITruckBharaiView.strMemo_Ids)};

            objDAL.RunProc("EC_Opr_Truck_Bharai_Selected_Memos", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0), 
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCode),    
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@Transaction_Id", SqlDbType.Int, 0,objITruckBharaiView.keyID),
            objDAL.MakeInParams("@Transaction_Date", SqlDbType.DateTime,0,objITruckBharaiView.TransactionDate),
            objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0,objITruckBharaiView.VehicleID),
            objDAL.MakeInParams("@TruckBharaiDetailsXML",SqlDbType.Xml,0,objITruckBharaiView.TruckBharaiDetailsXML),
            objDAL.MakeInParams("@Loaded_By_Id", SqlDbType.Int, 0,objITruckBharaiView.SupervisorID),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objITruckBharaiView.Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};

            objDAL.RunProc("dbo.EC_Opr_TruckBharai_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            

            if (objMessage.messageID == 0)
            { 

                string _Msg;
                _Msg = "Saved SuccessFully";
                objITruckBharaiView.ClearVariables();
                if (objITruckBharaiView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Outward/Frm_Opr_TruckBharai.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objITruckBharaiView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objITruckBharaiView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                
                }
            }

            return objMessage;
        }

        public void get_GCIDs_TruckBharaiID(int TruckBharaiID)
        {
            DataTable SessionDT = null;

            SessionDT = objITruckBharaiView.SessionBindTruckBharaiGrid;

            int i, DocumentID;
            DocumentID = TruckBharaiID;
            int MenuItemId = Common.GetMenuItemId();
            if (SessionDT.Rows.Count > 0)
            {
                for (i = 0; i <= SessionDT.Rows.Count - 1; i++)
                {
                    int GC_ID = 0;
                    GC_ID = Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]);
                    objCommon.Get_Consignor_Conginee_SMS(GC_ID, MenuItemId, DocumentID);
                }
            }
        }        
    }
}