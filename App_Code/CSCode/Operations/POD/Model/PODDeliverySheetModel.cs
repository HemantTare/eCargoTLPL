using System;
using System.Data;
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
using System.Data.SqlClient;

/// <summary>
/// Summary description for PODDeliverySheetModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class PODDeliverySheetModel:IModel
    {
        private IPODDeliverySheetView objIPODDeliverySheet;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _Main_ID = UserManager.getUserParam().MainId;
        private int _divisionID = UserManager.getUserParam().DivisionId;

        public PODDeliverySheetModel(IPODDeliverySheetView PODDeliverySheet)
        {
            objIPODDeliverySheet = PODDeliverySheet;
        }  
        
        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = { 
            objDAL.MakeInParams("@DeliveryDate",SqlDbType.DateTime,0,objIPODDeliverySheet.PODDeliveryDate),
            objDAL.MakeInParams("@Main_ID",SqlDbType.Int,0,_Main_ID),
            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,2,_hierarchyCode),
            objDAL.MakeInParams("@POD_Delivery_Sheet_ID",SqlDbType.Int,0,objIPODDeliverySheet.keyID),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionID)};

            objDAL.RunProc("EC_Opr_PODDeliverySheet_ReadValues", sqlpara, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@ERROR_CODE", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@POD_Delivery_Sheet_ID", SqlDbType.Int, 0,objIPODDeliverySheet.keyID),
            objDAL.MakeInParams("@POD_Delivery_Sheet_Date", SqlDbType.DateTime,0,objIPODDeliverySheet.PODDeliveryDate),
            objDAL.MakeInParams("@Delivery_Made_Hierarchy_Code", SqlDbType.VarChar, 2,_hierarchyCode),
            objDAL.MakeInParams("@Delivery_Maid_Main_ID", SqlDbType.Int, 0,_Main_ID),
            objDAL.MakeInParams("@Delivery_Sent_Type_ID", SqlDbType.Int, 0,objIPODDeliverySheet.PODSentByView.SentByID),
            objDAL.MakeInParams("@Courier_Name", SqlDbType.VarChar, 100, objIPODDeliverySheet.PODSentByView.CourierName),
            objDAL.MakeInParams("@Courier_Docket_No", SqlDbType.VarChar, 20, objIPODDeliverySheet.PODSentByView.CourierDocketNo),
            objDAL.MakeInParams("@Emp_ID", SqlDbType.Int,0,objIPODDeliverySheet.PODSentByView.EmployeeID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0,objIPODDeliverySheet.PODSentByView.VehicleID),
            objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIPODDeliverySheet.Remark),
            objDAL.MakeInParams("@PODDeliverySheetDetailsXML",SqlDbType.Xml,0,objIPODDeliverySheet.PODDeliverySheetDetailsXML),
            objDAL.MakeInParams("@PODDeliveredTo",SqlDbType.VarChar,20,objIPODDeliverySheet.PODDeliveredTo)
            };

            objDAL.RunProc("EC_Opr_PODDeliverySheet_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIPODDeliverySheet.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";

                 if (objIPODDeliverySheet.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/POD/FrmPODDeliverySheet.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIPODDeliverySheet.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }
            
            return objMessage;
        }
    }
}
