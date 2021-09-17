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
    public class DDCTempoFrgtModel : IModel 
    {

        private IDDCTempoFrgtView objIDDCTempoFrgtView;
        private DAL objDAL = new DAL();
        private Common objCommon = new Common();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        public DDCTempoFrgtModel(IDDCTempoFrgtView DDCTempoFrgtView)
        {
            objIDDCTempoFrgtView = DDCTempoFrgtView;
        }

        public DataSet CheckVehicleStr()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Vehicle_id", SqlDbType.Int, 0, objIDDCTempoFrgtView.VehicleID) ,
            objDAL.MakeInParams("@PDS_Id", SqlDbType.Int, 0, objIDDCTempoFrgtView.keyID)};
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
            objDAL.MakeInParams("@Transaction_Id", SqlDbType.Int, 0, objIDDCTempoFrgtView.keyID) ,
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIDDCTempoFrgtView.GetGCNoXML) ,
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIDDCTempoFrgtView.VehicleID) ,
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0, objIDDCTempoFrgtView.FromDate),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, objIDDCTempoFrgtView.ToDate),
            objDAL.MakeInParams("@Transaction_Date", SqlDbType.DateTime, 0, objIDDCTempoFrgtView.DDCTempoFrgtDate)};

            objDAL.RunProc("dbo.EC_Opr_DDC_Tempo_Freight_ReadValues", objSqlParam, ref objDS);
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
            objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0,objIDDCTempoFrgtView.keyID),
            objDAL.MakeInParams("@Transaction_Date", SqlDbType.DateTime,0,objIDDCTempoFrgtView.DDCTempoFrgtDate),  
            objDAL.MakeInParams("@IsCashCheque", SqlDbType.Int,0,objIDDCTempoFrgtView.IsCashCheque),
            objDAL.MakeInParams("@CashAmount", SqlDbType.Decimal, 0,objIDDCTempoFrgtView.CashAmount),
            objDAL.MakeInParams("@ChequeAmount", SqlDbType.Decimal, 0,objIDDCTempoFrgtView.ChequeAmount),
            objDAL.MakeInParams("@ChequeNo", SqlDbType.Int,0,objIDDCTempoFrgtView.ChequeNo),
            objDAL.MakeInParams("@ChequeDate", SqlDbType.DateTime,0,objIDDCTempoFrgtView.ChequeDate),
            objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0,objIDDCTempoFrgtView.VehicleID),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIDDCTempoFrgtView.Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@DDCTempoFrgtXML",SqlDbType.Xml,0,objIDDCTempoFrgtView.DDCTempoFrgtDetailsXML),
            objDAL.MakeInParams("@TotalTempoFrgtTBPaid", SqlDbType.Decimal, 0,objIDDCTempoFrgtView.TotalTempoFrgtTBPaid)};

            objDAL.RunProc("dbo.EC_Opr_DDC_Tempo_Freight_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            

            if (objMessage.messageID == 0)
            {
                 
                string _Msg;
                _Msg = "Saved SuccessFully";
                objIDDCTempoFrgtView.ClearVariables();
                if (objIDDCTempoFrgtView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmPDS.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIDDCTempoFrgtView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIDDCTempoFrgtView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    //int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                   System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                
                }
                else if (objIDDCTempoFrgtView.Flag == "SaveAndPay")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    //int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Finance/Accounting Vouchers/FrmTempoPayment.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID) + "&Vehicle_Id=" + ClassLibraryMVP.Util.EncryptInteger(objIDDCTempoFrgtView.VehicleID)));
                    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

                }


            }

            return objMessage;
        }

         
    }
}