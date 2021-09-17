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
    public class DDCModel : IModel
    {
        private IDDCView objIDDCView;
        private DAL objDAL = new DAL();
        private Common objCommon = new Common();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;


        public DDCModel(IDDCView DDCView)
        {
            objIDDCView = DDCView;
        }
        public DataSet FillDeliveryModeValues()
        {
            objDAL.RunProc("dbo.EC_Opr_GDC_FillValues", ref objDS);
            return objDS;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, objIDDCView.keyID) ,
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime, 0, objIDDCView.DDSDate) ,
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode)};
            objDAL.RunProc("dbo.EC_Opr_DDC_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillPDSValues()
        {

            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@DliveryMode", SqlDbType.Int, 0, objIDDCView.DeliveryModeID) ,
            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime, 0, objIDDCView.DDSDate) ,
            objDAL.MakeInParams("@VehicleID", SqlDbType.VarChar, 5, objIDDCView.VehicleID),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID)};
            objDAL.RunProc("dbo.EC_Opr_DDC_FillPDSValues", objSqlParam, ref objDS);
            return objDS; 
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, objIDDCView.keyID) ,
            objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0, objIDDCView.PDSId),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID)};

            objDAL.RunProc("dbo.EC_Opr_DDC_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@DDC_Branch_ID", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@DDC_ID", SqlDbType.Int, 0,objIDDCView.keyID),
            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime, 0,objIDDCView.DDSDate),
            objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0,objIDDCView.PDSId),
            objDAL.MakeInParams("@Delivery_Mode_ID", SqlDbType.Int, 0,objIDDCView.DeliveryModeID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,objIDDCView.VehicleID),
            objDAL.MakeInParams("@Driver_HandCartNo_PersonName", SqlDbType.VarChar, 100,objIDDCView.DiverName),
            objDAL.MakeInParams("@Total_Chq_Amount", SqlDbType.Decimal, 0,objIDDCView.Total_ChequeAmt),
            objDAL.MakeInParams("@Total_Credit_Amount", SqlDbType.Decimal, 0,objIDDCView.Total_CreditAmt),
            objDAL.MakeInParams("@Total_Cash_Amount", SqlDbType.Decimal, 0,objIDDCView.Total_GC_Amount), 
            objDAL.MakeInParams("@Cash_Received", SqlDbType.Decimal, 0,objIDDCView.Total_Cash_Received),
            objDAL.MakeInParams("@Cash_Balance", SqlDbType.Decimal, 0,objIDDCView.Total_Cash_Balance),  
            objDAL.MakeInParams("@Godown_Supervisor_ID", SqlDbType.Int, 0,objIDDCView.SupervisorID),
            objDAL.MakeInParams("@Total_Local_Tempo_Freight", SqlDbType.Decimal, 0,objIDDCView.Total_Local_Tempo_Freight),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIDDCView.Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@DDCDetailsXML",SqlDbType.Xml,0,objIDDCView.DDSDetailsXML),
            objDAL.MakeInParams("@Total_MobilePay", SqlDbType.Decimal, 0,objIDDCView.Total_MobilePay),
            objDAL.MakeInParams("@Total_SwipeCard", SqlDbType.Decimal, 0,objIDDCView.Total_SwipeCard),
            objDAL.MakeInParams("@Total_PendingFreight", SqlDbType.Decimal, 0,objIDDCView.Total_PendingFreight),
            objDAL.MakeInParams("@Total_Bonus", SqlDbType.Decimal, 0,objIDDCView.Total_TempoBonus),
            objDAL.MakeInParams("@Total_AddTempoFrt", SqlDbType.Decimal, 0,objIDDCView.Total_TempoAddTempoFrt)};

            objDAL.RunProc("dbo.EC_Opr_DDC_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            
            if (objMessage.messageID == 0)
            { 
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIDDCView.keyID == -1)
                {
                    get_GCIDs_DDCID(Convert.ToInt32(objSqlParam[2].Value));
                }
                objIDDCView.ClearVariables();
                if (objIDDCView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmDDC.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIDDCView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIDDCView.Flag == "SaveAndPrint")
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

        public void get_GCIDs_DDCID(int DDCID)
        {
            DataTable SessionDT = null;

            SessionDT = objIDDCView.SessionBindDDSGrid;

            int i, DocumentID;
            DocumentID = DDCID;
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