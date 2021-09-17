using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;
using Raj.EC;

namespace Raj.EC.CRM
{
    public class CallBack
    {

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchGcDoc(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = { 
                                        objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0, (int)UserManager.getUserParam().YearCode) ,
                                        objDAL.MakeInParams("@SearchText", SqlDbType.VarChar, 10, SearchFor)
                                     };
            objDAL.RunProc("EC_CRM_Trn_Complaint_FillGcDocNo", sqlParam, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchBranchCRMQueries(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
            {
            String key1 = KeyName + "," + KeyID;

            Common objCommon = new Common();
            DataSet ds = new DataSet();

            if (othercolumns == "BranchWise")
            {
                ds = objCommon.Get_Values_Where(TableName, key1, "is_diplomat = 0 and Is_Active = 1 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);
            }
            else if (othercolumns == "ODALocationWise")
            {
                ds = objCommon.Get_Values_Where(TableName, key1, "is_diplomat = 0 and Is_Active = 1 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);
            }
            else if (othercolumns == "DocumentRequired")
            {
                ds = objCommon.Get_Values_Where(TableName, key1, KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, false);
            }
            else if (othercolumns == "PickupRequest")
            {
                ds = objCommon.Get_Values_Where(TableName, key1, "Is_Active = 1 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);
            }

            return ds.Tables[0];
        }
                
        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchUser(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = { 
                                        objDAL.MakeInParams("@Flag", SqlDbType.VarChar,5,othercolumns),
                                        objDAL.MakeInParams("@SearchText", SqlDbType.VarChar, 10, SearchFor+"%"),
                                        objDAL.MakeInParams("@User_Id", SqlDbType.Int, 1, UserManager.getUserParam().UserId),
                                      };

            objDAL.RunProc("EC_CRM_Trn_Complaint_Assign_SearchUser", sqlParam, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchPickupGcDoc(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = { objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0, (int)UserManager.getUserParam().YearCode) ,
                                        objDAL.MakeInParams("@SearchText", SqlDbType.VarChar, 10, SearchFor),
                                        objDAL.MakeInParams("@PickUp_Date", SqlDbType.DateTime,0, othercolumns)};

            objDAL.RunProc("EC_CRM_Pickup_FillGcDocNo", sqlParam, ref ds);

            return ds.Tables[0];
        }

    }
}