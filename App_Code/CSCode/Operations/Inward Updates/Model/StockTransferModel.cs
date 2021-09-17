using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

/// <summary>
/// <summary>
/// Author : Ankit champaneriya
/// Date   : 07-01-09
/// Summary description for StockTransferModel
/// </summary>
/// 
namespace Raj.EC.OperationModel
{
    public class StockTransferModel : IModel
    {
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private IStockTransferView objIStockTransferView;

        public StockTransferModel(IStockTransferView StockTransferView)
        {
            objIStockTransferView = StockTransferView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Branch_Id", SqlDbType.Int , 0,objIStockTransferView.BranchId ),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int , 0, UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@GC_XML", SqlDbType.Xml, 0, objIStockTransferView.GCXML ),
            objDAL.MakeInParams("@New_Current_Branch_ID ", SqlDbType.Int , 0, objIStockTransferView.NewCurrentBranchId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int , 0, UserManager.getUserParam().YearCode  )};

            objDAL.RunProc("dbo.EC_Opr_GC_Stock_Transfer_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public DataSet Fill_Values()
        {
            objDAL.RunProc("dbo.EC_Opr_DeliveryBranchUpdate_FillValues", ref objDS);
            return objDS;
        }

        public Message Save()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = objIStockTransferView.SessionDGStockTransfer;
            ds.Tables.Add(dt.Copy());
            string xmldata = ds.GetXml().ToString().ToLower();

            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams ("@TransactionDate", SqlDbType.DateTime  ,10,objIStockTransferView.TransactionDate ),
            objDAL.MakeInParams ("@xmldata", SqlDbType.Xml  ,0,xmldata ),
            objDAL.MakeInParams("@New_Current_Branch_Id",SqlDbType.Int ,0, objIStockTransferView.NewCurrentBranchId ),
            objDAL.MakeInParams("@Reason",SqlDbType.VarChar ,250, objIStockTransferView.Reason  ),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  )};

            objDAL.RunProc("EC_Opr_StockTransfer_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            if (objMessage.messageID == 0)
            {
                objIStockTransferView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

                //script for redirecting form on same page with save dialog box.
                int MenuItemId = Common.GetMenuItemId();
                string Mode = System.Web.HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward Updates/FrmStockTransfer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
            }
            return objMessage;
        }
    }
}