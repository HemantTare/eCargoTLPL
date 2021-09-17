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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using Raj.EC;

/// <summary>
/// Summary description for CompanyCallBackFunction
/// </summary>
/// 
namespace Raj.EC.CompanyCallBackFunction
{
    public class CallBack
    {
        [AjaxPro.AjaxMethod]
        public static DataTable GetCompanyLedgers(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            if (othercolumns == "")
            {
                othercolumns = "0";
            }


            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
                                       _objDAL.MakeInParams("@CallFrom", SqlDbType.Int,0,othercolumns)
                                      };
            _objDAL.RunProc("[dbo].[EC_Mst_CompanyDetails_GetLedgers]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
    }
}
