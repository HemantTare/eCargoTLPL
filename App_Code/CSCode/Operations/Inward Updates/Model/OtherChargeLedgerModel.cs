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
/// Summary description for OtherChargeLedgerModel
/// </summary>
/// 
namespace Raj.EC.OperationModel
{
    public class OtherChargeLedgerModel:IModel
    {
        private DataSet _ds;
        private IOtherChargeLedgerView _OtherChargeLedgerView;
        private DAL _objDAL = new DAL();

        public OtherChargeLedgerModel(IOtherChargeLedgerView OtherChargeLedgerView)
        {
            _OtherChargeLedgerView = OtherChargeLedgerView;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = 
                                    {
                                        _objDAL.MakeInParams("@ID",SqlDbType.Int,0,_OtherChargeLedgerView.keyID)
                                        
                                    };

            _objDAL.RunProc("dbo.EC_Opr_Octroi_Update_Other_Details_Read", sqlpara, ref _ds);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Ledger_ID" }, _ds.Tables[0]);
            return _ds;
        }

    }
}