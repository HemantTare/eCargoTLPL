using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP.UI;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using System.Data.SqlClient;
using Raj.EC;

/// <summary>
/// Summary description for MRCashChequeDetails
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{


    public class MRCashChequeDetailsModel:IModel
    {

        private DataSet _ds;
        private IMRCashChequeDetailsView _MRCashChequeDetailsView;
        private DAL _objDAL = new DAL();
        private int MenuItemId = Common.GetMenuItemId();

        public MRCashChequeDetailsModel(IMRCashChequeDetailsView MRCashChequeDetailsView)
        {
            _MRCashChequeDetailsView = MRCashChequeDetailsView;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

        public DataSet ReadValues()
        {

            if (MenuItemId == 11131)
            {
                SqlParameter[] sqlpara = 
                                    {
                                        _objDAL.MakeInParams("@Receipt_ID",SqlDbType.Int,0,_MRCashChequeDetailsView.keyID),
                                        _objDAL.MakeInParams("@Menu_Item_Id",SqlDbType.Int,0,MenuItemId)};

                _objDAL.RunProc("EC_FA_Receipt_Fill_Ledger", sqlpara, ref _ds);
            }
            else
            {
                SqlParameter[] sqlpara = 
                                    {
                                        _objDAL.MakeInParams("@MR_ID",SqlDbType.Int,0,_MRCashChequeDetailsView.keyID),
                                        _objDAL.MakeInParams("@Menu_Item_Id",SqlDbType.Int,0,MenuItemId)};

                _objDAL.RunProc("EC_FA_MR_Fill_Ledger", sqlpara, ref _ds);
            }
            if (MenuItemId == 113 || MenuItemId == 114 || MenuItemId == 202)
            {
                Common.SetPrimaryKeys(new string[] { "Cheque_No" }, _ds.Tables[0]);
            }
            else
            {
                Common.SetPrimaryKeys(new string[] { "Cheque_No", "Cheque_Bank_Name" }, _ds.Tables[0]);
            }
            return _ds;
        }
    }
}
