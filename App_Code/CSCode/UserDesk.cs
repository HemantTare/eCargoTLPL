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

/// <summary>
/// Summary description for User_Desc
/// </summary>
namespace Raj.EC.UserDesk
{
    public class UserDesk
    {
        private DAL _objDal = new DAL();
        private DataSet _objDS = null;

        public UserDesk()
        {
        }
        public DataSet get_Notice_Alerts()
        {
            SqlParameter[] sqlpar = { _objDal.MakeInParams("@Branch_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId) };
            _objDal.RunProc("Get_Notice_Count", sqlpar, ref _objDS);
            return _objDS;
        }
        public DataSet Get_Notice_Values(string NoticeType, int Login_Branch_ID)
        {
            SqlParameter[] sqlpar = {
            _objDal.MakeInParams("@Notice_Type", SqlDbType.VarChar,20, NoticeType),
            _objDal.MakeInParams("@Login_Branch_ID", SqlDbType.Int, 0, Login_Branch_ID)};

            _objDal.RunProc("EC_Fill_Unclaim_Notice_Details", sqlpar, ref _objDS);

            return _objDS;
        }

        public DataSet Get_eWayBills(int RegionId, int AreaId, int BranchId, DateTime FromDate, DateTime ToDate)
        {
            SqlParameter[] sqlpar = {
            _objDal.MakeInParams("@RegionID", SqlDbType.Int ,0, RegionId),
            _objDal.MakeInParams("@AreaID", SqlDbType.Int,0, AreaId),
            _objDal.MakeInParams("@BranchID", SqlDbType.Int,0, BranchId),
            _objDal.MakeInParams("@FromDate", SqlDbType.DateTime ,0, FromDate),
            _objDal.MakeInParams("@ToDate", SqlDbType.DateTime ,0, ToDate)};

            _objDal.RunProc("EC_Rpt_eWayBillUpdation", sqlpar, ref _objDS);
            //Change on 29-06-2015 
            //_objDal.RunProc("EC_Fill_Unclaim_Notice_Details", sqlpar, ref _objDS);

            return _objDS;
        }

        public void Edit(string Type, string xml, int Created_By)
        {
            SqlParameter[] sqlpar = {
            _objDal.MakeInParams("@Type", SqlDbType.VarChar, 10, Type),
            _objDal.MakeInParams("@xml", SqlDbType.Xml, 0, xml),
            _objDal.MakeInParams("@Created_By", SqlDbType.Int, 0, Created_By)
            };

            _objDal.RunProc("EC_Unclaim_VT_Notice_Edit", sqlpar);
        }

        public void EditeWayBill(string xml, int Created_By)
        {
            SqlParameter[] sqlpar = {
            _objDal.MakeInParams("@xml", SqlDbType.Xml, 0, xml),
            _objDal.MakeInParams("@Created_By", SqlDbType.Int, 0, Created_By)
            };

            _objDal.RunProc("EC_eWayBillVerification_Save", sqlpar);
        }

        public void Save(String Type, String xml)
        {
        SqlParameter[] sqlpar = {
            _objDal.MakeInParams("@Type", SqlDbType.VarChar, 20, Type),
            _objDal.MakeInParams("@xml", SqlDbType.Xml, 0, xml),
            _objDal.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};

        _objDal.RunProc("EC_Unclaim_Notice_Save", sqlpar);
        }
    }
}