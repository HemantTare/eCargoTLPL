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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using ClassLibraryMVP.Security; 
using Raj.EC; 

/// <summary>
/// Summary description for MarfatiyaPaymentReceipt
/// </summary>

namespace Raj.EC
{
    public class MarfatiyaPaymentReceipt
    {
        private DAL _objDal = new DAL();
        private DataSet _objDS = null;

        public MarfatiyaPaymentReceipt()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataSet Get_EC_Opr_Marfatiya_PayReceipt_ReadValues(Int32 Ledger_Id, Int32 Marfatiya_Receipt_ID, DateTime Receipt_Date)
        {
           
            SqlParameter[] objSqlParam = {
                _objDal.MakeInParams("@Branch_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                _objDal.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, Ledger_Id),
                _objDal.MakeInParams("@Marfatiya_Receipt_ID", SqlDbType.Int, 0, Marfatiya_Receipt_ID),
                _objDal.MakeInParams("@Receipt_Date", SqlDbType.DateTime, 0, Receipt_Date)};
            _objDal.RunProc("EC_Opr_Marfatiya_Pay_Receipt_ReadValues", objSqlParam, ref _objDS);
            return _objDS;
        }

 
    }


}