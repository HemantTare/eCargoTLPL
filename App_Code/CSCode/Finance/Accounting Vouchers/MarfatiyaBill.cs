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
/// Summary description for MarfatiyaBill
/// </summary>

namespace Raj.EC
{
    public class MarfatiyaBill
    {
        private DAL _objDal = new DAL();
        private DataSet _objDS = null;

        public MarfatiyaBill()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataSet Get_EC_Opr_Marfatiya_ReadValues(Int32 Ledger_Id, Int32 Bill_ID, DateTime Bill_Date)
        {
           
            SqlParameter[] objSqlParam = {
                _objDal.MakeInParams("@Branch_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                _objDal.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, Ledger_Id),
                _objDal.MakeInParams("@Bill_ID", SqlDbType.Int, 0, Bill_ID),
                _objDal.MakeInParams("@Bill_Date", SqlDbType.DateTime, 0, Bill_Date)};
            _objDal.RunProc("EC_Opr_Marfatiya_ReadValues", objSqlParam, ref _objDS);
            return _objDS;
        }

 
    }


}