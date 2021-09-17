using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ShortExcessSettlementModel
/// </summary>

namespace Raj.EC.OperationModel
{
    public class ShortExcessSettlementModel : IModel
    {
        private IShortExcessSettlementView objIShortExcessSettlementView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

        public ShortExcessSettlementModel(IShortExcessSettlementView ShortExcessSettlementView)
        {
            objIShortExcessSettlementView = ShortExcessSettlementView;
        }

        public DataSet Fill_GCDetails(int flag, int gcid, int branchid)
        {
            SqlParameter[] objSqlParam = {
                objDAL.MakeInParams("@flag", SqlDbType.Int, 0, flag),
                objDAL.MakeInParams("@gc_Id", SqlDbType.Int, 0, gcid),
                objDAL.MakeInParams("@main_Id", SqlDbType.Int, 0, branchid)};

            objDAL.RunProc("EC_Opr_Short_Excess_GetGCNo", objSqlParam, ref objDS);
            return objDS;
        }
      
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIShortExcessSettlementView.keyID)};
            objDAL.RunProc("EC_Opr_Short_Excess_Settlement_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {
                objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                             
                objDAL.MakeInParams("@Found_Branch_ID", SqlDbType.Int,0,objIShortExcessSettlementView.keyID),
                objDAL.MakeInParams("@ShortExcessXML",SqlDbType.Xml,0,objIShortExcessSettlementView.ShortExcessXML),                                            
                objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId)};

            objDAL.RunProc("EC_Opr_Short_Excess_Settlement_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIShortExcessSettlementView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                int MenuItemId = Common.GetMenuItemId();
                string Mode = HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/ShortExcess/FrmShortExcessSettelment.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
            }

            return objMessage;
        }
    }

}