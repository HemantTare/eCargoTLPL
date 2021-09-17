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
/// <summary>
/// Summary description for AccountTransferModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class AccountTransferModel : IModel
    {
        private IAccountTransferView objIAccountTransferView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _branchID = UserManager.getUserParam().MainId;
        
        public AccountTransferModel(IAccountTransferView AccountTransferView)
        {
            objIAccountTransferView = AccountTransferView;
        }
        
        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID) };

            objDAL.RunProc("dbo.EC_Opr_Account_Transfer_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
                objDAL.MakeInParams("@Pickup_Sheet_Id", SqlDbType.Int, 0, objIAccountTransferView.keyID) ,
            objDAL.MakeInParams("@VA_Id", SqlDbType.Int, 0, objIAccountTransferView.VAId) ,
            objDAL.MakeInParams("@Pickup_Date", SqlDbType.DateTime, 0, objIAccountTransferView.AccountTransferDate)};

            objDAL.RunProc("dbo.EC_Opr_Account_Transfer_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Pickup_Sheet_Id", SqlDbType.Int, 0,objIAccountTransferView.keyID),
            objDAL.MakeInParams("@Pickup_Sheet_Date", SqlDbType.DateTime,0,objIAccountTransferView.AccountTransferDate),
            objDAL.MakeInParams("@Pickup_Sheet_Branch_Id", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@VA_Id", SqlDbType.Int, 0,objIAccountTransferView.VAId),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIAccountTransferView.Remarks),

            objDAL.MakeInParams("@ATDetailsXML",SqlDbType.Xml,0,objIAccountTransferView.AccountTransferDetailsXML)
            };

            objDAL.RunProc("dbo.EC_Opr_Account_Transfer_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            
            if (objMessage.messageID == 0)
            {
                objIAccountTransferView.ClearVariables();
                
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objIAccountTransferView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Booking/FrmAccountTransfer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIAccountTransferView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }

            return objMessage;
        }
    }
}

