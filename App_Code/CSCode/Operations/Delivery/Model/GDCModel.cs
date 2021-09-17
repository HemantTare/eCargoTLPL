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
using Raj.EC.OperationView;

namespace Raj.EC.OperationModel
{
    public class GDCModel : IModel
    {

        private IGDCView objIGDCView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private Common objCommon = new Common();

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        public GDCModel(IGDCView GDCView)
        {
            objIGDCView = GDCView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_GDC_FillValues", ref objDS);
            return objDS;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, _yearCode) ,
            objDAL.MakeInParams("@GDC_Id", SqlDbType.Int, 0, objIGDCView.keyID) ,
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIGDCView.GetGCNoXML) ,
            objDAL.MakeInParams("@GDC_Date", SqlDbType.DateTime, 0, objIGDCView.GDCDate)};

            objDAL.RunProc("dbo.EC_Opr_GDC_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public bool ValidateConsigneeClient()
        {
            Boolean Is_ValidateConsigneeList;
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@GDCDetailsXML",SqlDbType.Xml,0,objIGDCView.GDCDetailsXML),
            objDAL.MakeOutParams("@ValidateConsigneeClient",SqlDbType.Bit,0)
            };

            objDAL.RunProc("dbo.EC_Opr_GDC_Validate_ConsigneeClient", objSqlParam, ref objDS);

            Is_ValidateConsigneeList = Convert.ToBoolean(objSqlParam[1].Value.ToString());
            return Is_ValidateConsigneeList;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@DDC_ID", SqlDbType.Int, 0,objIGDCView.keyID),
            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime,0,objIGDCView.GDCDate),
            objDAL.MakeInParams("@DDC_Branch_ID", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@Godown_Supervisor_ID", SqlDbType.Int, 0,objIGDCView.SupervisorID),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIGDCView.Remarks),
                
                objDAL.MakeInParams("@Delivered_To", SqlDbType.VarChar, 50,objIGDCView.DeliveredTo),
                objDAL.MakeInParams("@Delivered_to_Mobile", SqlDbType.VarChar, 15,objIGDCView.DeliveredToMobile),
                objDAL.MakeInParams("@PhotoID_Type_ID", SqlDbType.Int , 0,objIGDCView.PhotoIDType),
                objDAL.MakeInParams("@PhotoID_No", SqlDbType.VarChar, 20,objIGDCView.PhotoIDNo),
                objDAL.MakeInParams("@Vehicle_Type_ID", SqlDbType.Int, 0,objIGDCView.VehicleType),
                objDAL.MakeInParams("@Vehicle_No_Part1", SqlDbType.VarChar, 250,objIGDCView.VehicleNoPart1),
                objDAL.MakeInParams("@Vehicle_No_Part2", SqlDbType.VarChar, 250,objIGDCView.VehicleNoPart2),
                objDAL.MakeInParams("@Vehicle_No_Part3", SqlDbType.VarChar, 250,objIGDCView.VehicleNoPart3),
                objDAL.MakeInParams("@Vehicle_No_Part4", SqlDbType.VarChar, 250,objIGDCView.VehicleNoPart4),

            objDAL.MakeInParams("@GDCDetailsXML",SqlDbType.Xml,0,objIGDCView.GDCDetailsXML)};

            objDAL.RunProc("dbo.EC_Opr_GDC_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIGDCView.keyID == -1)
                {
                    get_GCIDs_GDCID(Convert.ToInt32(objSqlParam[2].Value));
                }
                objIGDCView.ClearVariables();
                if (objIGDCView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmGDC.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIGDCView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIGDCView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }

            return objMessage;
        } 

        public void get_GCIDs_GDCID(int GDCID)
        {
            DataTable SessionDT = null;

            SessionDT = objIGDCView.SessionBindGDCGrid;

            int i, DocumentID;
            DocumentID = GDCID;
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