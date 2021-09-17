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
    public class FrghtDisVoucherModel : IModel
    {
        private IFrghtDisVoucherView objIFrghtDisVoucherView;
        private DAL objDAL = new DAL();
        private Common objCommon = new Common();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _yearCode = UserManager.getUserParam().YearCode;

        public FrghtDisVoucherModel(IFrghtDisVoucherView FrghtDisVoucherView)
        {
            objIFrghtDisVoucherView = FrghtDisVoucherView;
        }
        

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int, 0, objIFrghtDisVoucherView.keyID), 
            objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime, 0, objIFrghtDisVoucherView.VoucherDate)};
            objDAL.RunProc("dbo.EC_Opr_FDV_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

       

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, _yearCode),
            objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0, objIFrghtDisVoucherView.keyID),
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIFrghtDisVoucherView.GetGCNoXML) ,
            objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime, 0, objIFrghtDisVoucherView.VoucherDate)};

            objDAL.RunProc("dbo.EC_Opr_FDV_ReadValues", objSqlParam, ref objDS);
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
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0,objIFrghtDisVoucherView.keyID),
            objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime, 0,objIFrghtDisVoucherView.VoucherDate),   
            objDAL.MakeInParams("@TotalDiscountAmt", SqlDbType.Decimal, 0,objIFrghtDisVoucherView.Total_DiscountAmt),
            objDAL.MakeInParams("@TotalGCAmount", SqlDbType.Decimal, 0,objIFrghtDisVoucherView.Total_Total_GC_Amount),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIFrghtDisVoucherView.Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@FDVDetailsXML",SqlDbType.Xml,0,objIFrghtDisVoucherView.FDVDetailsXML)};

            objDAL.RunProc("dbo.EC_Opr_FDV_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            
            if (objMessage.messageID == 0)
            { 
                string _Msg;
                _Msg = "Saved SuccessFully";

               
                objIFrghtDisVoucherView.ClearVariables();
                if (objIFrghtDisVoucherView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmFrghtDisVoucher.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIFrghtDisVoucherView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIFrghtDisVoucherView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }

            return objMessage;
        }

       
    }
}