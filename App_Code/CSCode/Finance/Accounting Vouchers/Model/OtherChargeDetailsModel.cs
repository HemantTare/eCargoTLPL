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


namespace Raj.EC.FinanceModel
{
    public class OtherChargeDetailsModel:IModel
    {
        private DataSet _ds;
        private IOtherChargeDetailsView _OtherChargeDetailsView;
        private DAL _objDAL = new DAL();

        public OtherChargeDetailsModel(IOtherChargeDetailsView OtherChargeDetailsView)
        {
            _OtherChargeDetailsView = OtherChargeDetailsView;
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
                                        _objDAL.MakeInParams("@ID",SqlDbType.Int,0,_OtherChargeDetailsView.keyID)
                                        
                                    };

            _objDAL.RunProc("EC_FA_BTH_Other_Details_Read", sqlpara, ref _ds);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Ledger_ID" }, _ds.Tables[0]); 
            return _ds;
        }
    }
}