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
/// Created : ANKIT CHAMPANERIYA
/// DATE : 1/12/08
/// Summary description for PODReceiptDDModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class PODReceiptDDModel : IModel
    {
        private IPODReceiptDDView _iPODReceiptDDView;
        private DataSet objDS;
        private DAL objDAL = new DAL();
        
        public PODReceiptDDModel(IPODReceiptDDView iPODReceiptDDView)
        {
            _iPODReceiptDDView = iPODReceiptDDView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = { 
            objDAL.MakeInParams("@Cover_Received_ID",SqlDbType.Int  ,0,_iPODReceiptDDView.keyID )};

            objDAL.RunProc("EC_Opr_POD_CoverReceipt_DD_ReadValues", sqlpara, ref objDS);

            return objDS;
        }

        public DataSet FillValuesGCNo()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, _iPODReceiptDDView.GCNo) };
            objDAL.RunProc("EC_Opr_Get_POD_GCDetails", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@ERROR_CODE", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),
                objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
                objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
                objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),
                objDAL.MakeInParams("@Cover_Received_ID", SqlDbType.Int, 0,_iPODReceiptDDView.keyID  ),
                objDAL.MakeInParams("@Cover_Received_Hierarchy_Code",SqlDbType.VarChar,2,UserManager.getUserParam().HierarchyCode),
                objDAL.MakeInParams("@Cover_Received_Main_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId),
                objDAL.MakeInParams("@Cover_Received_Date",SqlDbType.DateTime,0,_iPODReceiptDDView.PODReceiptDate ),
                objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,_iPODReceiptDDView.GCNo),
                objDAL.MakeInParams("@Received_Through_ID",SqlDbType.Int,0,_iPODReceiptDDView.PODSentByView.SentByID ),
                objDAL.MakeInParams("@Courier_Name",SqlDbType.VarChar,100,_iPODReceiptDDView.PODSentByView.CourierName),
                objDAL.MakeInParams("@Courier_Docket_No",SqlDbType.VarChar,20,_iPODReceiptDDView.PODSentByView.CourierDocketNo),
                objDAL.MakeInParams("@Emp_ID",SqlDbType.Int,0,_iPODReceiptDDView.PODSentByView.EmployeeID),
                objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,_iPODReceiptDDView.PODSentByView.VehicleID),
                objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250,_iPODReceiptDDView.Remarks  ),
                objDAL.MakeInParams("@Created_By",SqlDbType.Int ,0,UserManager.getUserParam().UserId)
            };

            objDAL.RunProc("EC_Opr_POD_Receipt_DD_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                //_iPODReceiptDDView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (_iPODReceiptDDView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/POD/FrmPODReceiptDD.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (_iPODReceiptDDView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }    

            return objMessage;
        }
    }
}