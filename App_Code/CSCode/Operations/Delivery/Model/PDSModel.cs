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
    public class PDSModel : IModel 
    {

        private IPDSView objIPDSView;
        private DAL objDAL = new DAL();
        private Common objCommon = new Common();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        public PDSModel(IPDSView PDSView)
        {
            objIPDSView = PDSView;
        }

        public DataSet CheckVehicleStr()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Vehicle_id", SqlDbType.Int, 0, objIPDSView.VehicleID) ,
            objDAL.MakeInParams("@PDS_Id", SqlDbType.Int, 0, objIPDSView.keyID)};
            objDAL.RunProc("dbo.EC_Opr_PDS_Validate_Vehicle", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID) };
            objDAL.RunProc("dbo.EC_Opr_PDS_FillValues",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, _yearCode) ,
            objDAL.MakeInParams("@PDS_Id", SqlDbType.Int, 0, objIPDSView.keyID) ,
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIPDSView.GetGCNoXML) ,
            objDAL.MakeInParams("@PDS_Date", SqlDbType.DateTime, 0, objIPDSView.PDSDate)};

            objDAL.RunProc("dbo.EC_Opr_PDS_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public bool ValidateConsigneeClient()
        {
            Boolean Is_ValidateConsigneeList;
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@PDSDetailsXML",SqlDbType.Xml,0,objIPDSView.PDSDetailsXML),
            objDAL.MakeInParams("@Delivery_Mode_ID",SqlDbType.Int,0,objIPDSView.DeliveryModeID),
            objDAL.MakeOutParams("@ValidateConsigneeClient",SqlDbType.Bit,0)
            };

            objDAL.RunProc("dbo.EC_Opr_PDS_Validate_ConsigneeClient", objSqlParam, ref objDS);

            Is_ValidateConsigneeList = Convert.ToBoolean(objSqlParam[2].Value.ToString());
            return Is_ValidateConsigneeList;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCode),    
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@PDS_Branch_ID", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0,objIPDSView.keyID),
            objDAL.MakeInParams("@PDS_Date", SqlDbType.DateTime,0,objIPDSView.PDSDate),
            objDAL.MakeInParams("@VA_Id", SqlDbType.Int, 0,objIPDSView.VAID),
            objDAL.MakeInParams("@Delivery_Mode_ID", SqlDbType.Int, 0,objIPDSView.DeliveryModeID),
            objDAL.MakeInParams("@Delivery_Mode_Description", SqlDbType.VarChar, 50,objIPDSView.DeliveryModeDescription),
            objDAL.MakeInParams("@Godown_Supervisor_ID", SqlDbType.Int, 0,objIPDSView.SupervisorID),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIPDSView.Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@PDSDetailsXML",SqlDbType.Xml,0,objIPDSView.PDSDetailsXML),
            objDAL.MakeInParams("@DiverName", SqlDbType.VarChar, 50,objIPDSView.DiverName),
            objDAL.MakeInParams("@MobileNumber", SqlDbType.VarChar, 50,objIPDSView.MobileNumber),
            objDAL.MakeInParams("@VendorID", SqlDbType.Int, 0,objIPDSView.VendorID),
            objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0,objIPDSView.VehicleID),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)};

            objDAL.RunProc("dbo.EC_Opr_PDS_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[19].Value);
            objMessage.message = Convert.ToString(objSqlParam[20].Value);
            

            if (objMessage.messageID == 0)
            {
                if (objIPDSView.keyID == -1)
                {
                    get_GCIDs_PDSID(Convert.ToInt32(objSqlParam[18].Value));
                }
               

                string _Msg;
                _Msg = "Saved SuccessFully";
                objIPDSView.ClearVariables();
                if (objIPDSView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmPDS.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIPDSView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIPDSView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    //int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    int Document_ID = Convert.ToInt32(objSqlParam[18].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }

            return objMessage;
        }

        public void get_GCIDs_PDSID(int PDSID)
        {
            DataTable SessionDT = null;

            SessionDT = objIPDSView.SessionBindPDSGrid;

            int i, DocumentID;
            DocumentID = PDSID;
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