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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

namespace Raj.EC.CRM
{
    public class CRMTrackNTrace
    {
        private DataSet objDS;
        private DAL _objDAL = new DAL();

        public CRMTrackNTrace()
        {
          
        }
         public DataSet Fill_Details_Track_And_Trace(string NO_FOR_SEARCH, int Year_code)
         {
            SqlParameter[] sqlpar = {
            _objDAL.MakeInParams("@NO_FOR_SEARCH", SqlDbType.VarChar, 20, NO_FOR_SEARCH),
            _objDAL.MakeInParams("@YEAR_CODE", SqlDbType.Int, 0, Year_code)};
            _objDAL.RunProc("EC_CRM_TRACK_AND_TRACE", sqlpar, ref objDS);

            return objDS;
         }
    }
}